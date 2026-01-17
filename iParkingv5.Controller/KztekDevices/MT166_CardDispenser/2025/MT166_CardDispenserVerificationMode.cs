using iParkingv5.Controller.KztekDevices.MT166_CardDispenser._2025;
using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using Kztek.Object;
using Kztek.Tool;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static Kztek.Object.InputTupe;

namespace iParkingv5.Controller.KztekDevices.MT166_CardDispenser
{
    public class LastSwipeCardInfo
    {
        public DateTime EventTime { get; set; }
        public string LastCard { get; set; } = string.Empty;
    }

    public class MT166_CardDispenserVerificationMode : BaseDispenserDevice, ICardDispenserv2
    {
        #region Properties
        private string _lastDispenserStatusHash = null;
        private DateTime _lastEventSentTime = DateTime.MinValue;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;

        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    Free();
                }
                return false;
            }
        }

        //--EVENT
        public event CardOnRFEventHandler? OnCardInRFEvent;
        public event OnCardNotSupportEventHandler? OnCardNotSupportEvent;

        private LastSwipeCardInfo? LastSwipeCardInfo;

        #endregion

        #region Base
        public void SignalToStop()
        {
            if (thread != null)
            {
                stopEvent.Set();
            }
        }
        public void WaitForStop()
        {
            if (thread != null)
            {
                thread.Join();
                Free();
            }
        }
        private void Free()
        {
            thread = null;
            // release events
            stopEvent.Close();
            stopEvent = null;
        }

        public override void PollingStart()
        {
            if (thread == null)
            {
                stopEvent = new ManualResetEvent(false);
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
            }
        }
        public override void PollingStop()
        {
            if (this.Running)
            {
                SignalToStop();
                while (thread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { stopEvent }),
                        100,
                        true))
                    {
                        WaitForStop();
                        break;
                    }
                    Application.DoEvents();
                }
            }
        }

        public async void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    string comport = this.ControllerInfo.Comport;
                    int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                    string getEventCmd = KZTEK_CMD.GetEventCMD();
                    string response = await GetEvent(comport, baudrate, getEventCmd);

                    this.ControllerInfo.IsConnect = response != "" && !response.Contains("exception", StringComparison.CurrentCultureIgnoreCase);

                    if (string.IsNullOrWhiteSpace(response) || response.ToUpper().Contains("ERROR"))
                    {
                        if (this.ControllerInfo.IsConnect)
                        {
                            this.ControllerInfo.IsConnect = false;
                            ConnectStatusCHangeEventArgs statusChange = new ConnectStatusCHangeEventArgs
                            {
                                CurrentStatus = false,
                                Reason = "Không nhận phản hồi",
                                DeviceId = this.ControllerInfo.Id,
                                DeviceName = this.ControllerInfo.Name,
                                DeviceType = EmParkingControllerType.Card_Dispenser,
                                DispenserOnChange = new CardDispenserOnChange()
                            };

                            OnConnectStatusChangeEvent(statusChange);
                        }
                        continue;
                    }

                    if (!response.Contains("GetEvent?/"))
                    {
                        continue;
                    }

                    string[] data = response.Split('/');
                    Dictionary<string, string> map = GetEventContent(data);

                    /// Code mới gộp -> chỉ bắn 1 event onChange
                    CallStatusAll(this.ControllerInfo, map);

                    // Kiểm tra Sự kiện Reader/Input/Loop
                    if (response.Contains("NotEvent"))
                    {
                        continue;
                    }

                    await CheckNewEvent(response, map);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    await Task.Delay(300);
                }
            }
        }
        private async Task<string> GetEvent(string comport, int baudrate, string getEventCmd)
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                if (!await NetWorkTools.IsPingSuccessAsync(comport, 500))
                {
                    if (this.ControllerInfo.IsConnect)
                    {
                        this.ControllerInfo.IsConnect = false;
                        ConnectStatusCHangeEventArgs statusChange = new ConnectStatusCHangeEventArgs
                        {
                            CurrentStatus = false,
                            Reason = "Ping Error",
                            DeviceId = this.ControllerInfo.Id,
                            DeviceName = this.ControllerInfo.Name,
                            DeviceType = EmParkingControllerType.Card_Dispenser,
                            DispenserOnChange = new CardDispenserOnChange()
                        };

                        OnConnectStatusChangeEvent(statusChange);
                    }
                    return string.Empty;
                }

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, getEventCmd, false);
                return response;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
        public override async Task DeleteCardEvent()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string cmd = KZTEK_CMD.DeleteEventCMD();
                await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, cmd, false);

            }
            catch (Exception)
            {
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task CheckNewEvent(string response, Dictionary<string, string> map)
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceName = this.ControllerInfo.Id,
                Cmd = response,
                Description = "New Event",
                Response = "",
            });

            string eventTypeStr = map.ContainsKey("input") ? map["input"] : "";

            if (string.IsNullOrEmpty(eventTypeStr))
            {
                await DeleteCardEvent();
            }

            MT166V8EventType eventType = (MT166V8EventType)int.Parse(eventTypeStr);

            switch (eventType)
            {
                case MT166V8EventType.CardOnRFPosition:
                    if (LastSwipeCardInfo == null)
                    {
                        OnCardNotSupportEvent?.Invoke(this, new CardNotSupportEventArgs() { DeviceId = this.ControllerInfo.Id });
                    }
                    else
                    {
                        if (Math.Abs((LastSwipeCardInfo.EventTime - DateTime.Now).TotalSeconds) >= 5)
                        {
                            OnCardNotSupportEvent?.Invoke(this, new CardNotSupportEventArgs() { DeviceId = this.ControllerInfo.Id });
                        }
                    }
                    break;
                case MT166V8EventType.WaitingCard:
                    CallCardOnRFEvent(this.ControllerInfo, map, eventType);
                    break;
                case MT166V8EventType.Button1:
                case MT166V8EventType.Button2:
                case MT166V8EventType.Reader1:
                case MT166V8EventType.Reader2:
                case MT166V8EventType.BTN1_LOOP1:
                case MT166V8EventType.BTN2_LOOP1:
                    CallCardEvent(this.ControllerInfo, map, eventType);
                    break;
                case MT166V8EventType.Loop1:
                case MT166V8EventType.Loop2:
                case MT166V8EventType.Loop3:
                case MT166V8EventType.Loop4:
                case MT166V8EventType.Stop_Start:
                case MT166V8EventType.Stop_End:
                    CallInputEvent(this.ControllerInfo, map, eventType);
                    break;
                case MT166V8EventType.Spare:
                    break;
                case MT166V8EventType.CardbeTaken:
                    CallCardBeTakenEvent(this.ControllerInfo, "1");
                    break;
                case MT166V8EventType.BTN1_ABNORMAL:
                case MT166V8EventType.BTN2_ABNORMAL:
                    break;
                case MT166V8EventType.BTN1_STOP:
                    break;
                case MT166V8EventType.BTN2_STOP:
                    break;
                case MT166V8EventType.BTN1_PAUSE:
                    break;
                case MT166V8EventType.BTN2_PAUSE:
                    break;
                case MT166V8EventType.CardRevertedInTray1:
                case MT166V8EventType.CardRevertedInTray2:
                case MT166V8EventType.CardRevertedInTray:
                    CallCardEventCancel(this.ControllerInfo, map, eventType);
                    break;
                case MT166V8EventType.Open:
                    CallExitEvent(this.ControllerInfo, "1");
                    break;
                case MT166V8EventType.Close:
                    break;
                case MT166V8EventType.CardOut:
                    break;
                default:
                    break;
            }
            await DeleteCardEvent();

            //bool isCardEvent = eventType == MT166V8EventType.Reader1 || eventType == MT166V8EventType.Reader2 ||
            //                   eventType == MT166V8EventType.Button1 || eventType == MT166V8EventType.Button2 ||
            //                   eventType == MT166V8EventType.BTN1_ABNORMAL || eventType == MT166V8EventType.BTN2_ABNORMAL ||
            //                   eventType == MT166V8EventType.CardOut;

            //bool isLoopEvent =
            //                   eventType == MT166V8EventType.Loop1 || eventType == MT166V8EventType.Loop2 ||
            //                   eventType == MT166V8EventType.Loop3 || eventType == MT166V8EventType.Loop4 ||
            //                   eventType == MT166V8EventType.Stop_Start || eventType == MT166V8EventType.Stop_End;

            //bool isCardCancel = eventType == MT166V8EventType.CardRevertedInTray1 || eventType == MT166V8EventType.CardRevertedInTray2;

            //bool isExitEvent = eventType == MT166V8EventType.Open;

            //bool isCardBeTaken = eventType == MT166V8EventType.CardbeTaken;
            //else if (isExitEvent)
            //{
            //}
            //else if (isCardBeTaken)
            //{
            //    CallCardBeTakenEvent(this.ControllerInfo, "1");
            //}
            //else
            //{
            //    DeleteCardEvent();
            //}
        }

        /// <summary>
        /// Audio có giá trị từ 1 -> 6
        /// </summary>
        /// <param name="readerIndex"></param>
        /// <returns></returns>
        public async Task<bool> SetAudio(int relayIndex)
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string setAudioCmd = $"SetAudio?/Audio={relayIndex}/State=ON/";

                string response = await UdpTools.ReceiveSocketResponseAsync(ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, setAudioCmd, true);
                if (UdpTools.IsSuccess(response, "OK"))
                {
                    return true;
                }
                else if (UdpTools.IsSuccess(response, "ERROR"))
                {
                    return false;
                }
                OnErrorEvent(new ControllerErrorEventArgs()
                {
                    ErrorString = response,
                    ErrorFunc = "SetAudio",
                    CMD = setAudioCmd
                });
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
        public override async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = KZTEK_CMD.OpenRelayCMD_MT166(relayIndex);

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
                if (UdpTools.IsSuccess(response, "OK"))
                {
                    return true;
                }
                else if (UdpTools.IsSuccess(response, "ERR"))
                {
                    return false;
                }
                OnErrorEvent(new ControllerErrorEventArgs()
                {
                    ErrorString = response,
                    ErrorFunc = "OpenDoor",
                    CMD = $"{response} - {openRelayCmd}"
                });
                return false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
        #endregion

        #region Máy nhả thẻ
        /// <summary>
        /// Lệnh thực hiện chức năng nhả thẻ tới các vị khe nhả thẻ
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DispenseCard()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = "DispenseCard?/ToBezel/";

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
                return UdpTools.IsSuccess(response, "DispenseCard?/DispensingToBezel/");
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
        private async Task<bool> DispenseCardToRecycleInternal()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = "DispenseCard?/ToRecycle/";
                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
                return UdpTools.IsSuccess(response, "DispenseCard?/DispensingToBezel/");
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
        public async Task<bool> DispenseCardToRecycle()
        {
            await DispenseCard();
            await Task.Delay(500);
            await DispenseCardToRecycleInternal();
            return true;
        }
        #endregion

        #region Máy nuốt thẻ

        /// <summary>
        /// Sử dụng lệnh điều khiển Relay cho phép nuốt thẻ trong cơ cấu nuốt thẻ, được sử dụng cho cấu cơ nuốt thẻ của máy nuốt thẻ
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CollectCard()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string collectCardCmd = $"CollectCard?/";

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, collectCardCmd, true);
                if (UdpTools.IsSuccess(response, "OK"))
                {
                    return true;
                }
                else if (UdpTools.IsSuccess(response, "ERROR"))
                {
                    return false;
                }
                OnErrorEvent(new ControllerErrorEventArgs()
                {
                    ErrorString = response,
                    ErrorFunc = "CollectCard",
                    CMD = collectCardCmd
                });
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Sử dụng lệnh điều khiển Relay cho phép thẻ trả lại do không hợp lệ.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RejectCard()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string rejectCardCmd = $"RejectCard?/";

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, rejectCardCmd, true);
                if (UdpTools.IsSuccess(response, "OK"))
                {
                    return true;
                }
                else if (UdpTools.IsSuccess(response, "ERROR"))
                {
                    return false;
                }
                OnErrorEvent(new ControllerErrorEventArgs()
                {
                    ErrorString = response,
                    ErrorFunc = "RejectCard",
                    CMD = rejectCardCmd
                });
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
        #endregion

        #region SDK Functions
        /// <summary>
        /// Thực hiện hành động cho phép máy nhả thẻ đang trong trạng thái dừng hoạt động: Sẽ không thực hiện thao tác nhả thẻ nữa
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public async Task<bool> SetStateWorkCardDispenser(string state)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string openRelayCmd = "SetStateWorkCardDispenser?/State=" + state;

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, openRelayCmd, 500);
            });
            //SetStateWorkCardDispenser?/OK/
            //SetStateWorkCardDispenser?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetStateWorkCardDispenser",
                CMD = openRelayCmd
            });
            return false;
        }
        public async Task<string> GetStateWorkCardDispenser()
        {
            string GetStateWorkCardDispenserCMD = "GetStateWorkCardDispenser?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, GetStateWorkCardDispenserCMD, 500));
            //GetStateWorkCardDispenser?/State=Working/
            //GetStateWorkCardDispenser?/State=Stop/
            if (UdpTools.IsSuccess(response, "GetStateWorkCardDispenser?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string state = map.ContainsKey("state") ? map["state"] : "";
                return state;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetStateWorkCardDispenser",
                CMD = GetStateWorkCardDispenserCMD
            });
            return "";
        }

        /// <summary>
        /// Giá trị tối đa hỗ trợ: 900000
        /// </summary>
        /// <param name="relay"></param>
        /// <param name="delayInMilisecond"></param>
        /// <returns></returns>
        public async Task<bool> SetDelayRelay(int relay, int delayInMilisecond)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string setDelayRelayCmd = $"SetDelayRelay?/Relay={relay}/Delay={delayInMilisecond}/";

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, setDelayRelayCmd, 500);
            });
            //SetDelayRelay?/OK/
            //SetDelayRelay?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetDelayRelay",
                CMD = setDelayRelayCmd
            });
            return false;
        }

        /// <summary>
        /// Nếu sử dụng đầu đọc ngoài lắp trên cơ cấu nhả thẻ ( bỏ đầu đọc tích hợp sẵn bên trong ) thì chế độ ExternalReader sẽ được kích hoạt. <br/>
        /// Chế độ này được kích hoạt bằng cách gạt dipswitch 2 lên On.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeExternalReader()
        {
            string getModeExternalReaderCMD = "GetModeExternalReader?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getModeExternalReaderCMD, 500));
            //GetModeExternalReader?/ModeExternalReader=1/
            //GetModeExternalReader?/ModeExternalReader=0/
            if (UdpTools.IsSuccess(response, "GetModeExternalReader?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string modeExternalReader = map.ContainsKey("modeexternalreader") ? map["modeexternalreader"] : "";
                return modeExternalReader;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetModeExternalReader",
                CMD = getModeExternalReaderCMD
            });
            return "";
        }

        /// <summary>
        /// Mode Key được quy định bởi dipswitch 3
        /// Dipswitch 3 = OFF : Không sử dụng chế độ xác thực key. (ModeKey=0)
        /// ON: Sử dụng chế độ xác thực Key. (ModeKey=1)
        /// Lưu ý: Khi đặt ở chế độ xác thực key, thì nó chỉ có ý nghĩa với đầu đọc thẻ vé ngày gắn liền với
        /// cơ cấu nhả thẻ.Với đầu đọc vé tháng thì phải thiết lập riêng
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeKey()
        {
            string getModeKeyCmd = "GetModeKey?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getModeKeyCmd, 500));
            //GetModeKey?/ModeKey=1/
            //GetModeKey?/ ModeKey=0/
            if (UdpTools.IsSuccess(response, "GetModeKey?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string modeKey = map.ContainsKey("modekey") ? map["modekey"] : "";
                return modeKey;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetModeKey",
                CMD = getModeKeyCmd
            });
            return "";
        }

        /// <summary>
        /// Mode Key được quy định bởi dipswitch 4
        /// Dipswitch 4 = OFF : Không sử dụng chế độ tự động nuốt thẻ (ModeAutoCollectCard=0)
        ///               ON : Sử dụng chế độ tự động nuốt thẻ. (ModeAutoCollectCard=1)
        /// ModeAutoCollectCard=0 : Không có chế độ tự động nuốt thẻ khi người dùng ấn nhả thẻ nhưng không rút thẻ.
        /// ModeAutoCollectCard=1 : Có chế độ tự động nuốt thẻ khi người dùng ấn nhả thẻ nhưng không rút thẻ sau một khoảng thời gian nào đó.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeAutoCollectCard()
        {
            string getModeAutoCollectCardCmd = "GetModeAutoCollectCard?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getModeAutoCollectCardCmd, 500));
            //GetModeAutoCollectCard?/ModeAutoCollectCard=0/
            if (UdpTools.IsSuccess(response, "GetModeAutoCollectCard?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string modeAutoCollectCard = map.ContainsKey("modeautocollectcard") ? map["modeautocollectcard"] : "";
                return modeAutoCollectCard;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetModeAutoCollectCard",
                CMD = getModeAutoCollectCardCmd
            });
            return "";
        }

        /// <summary>
        /// Thiết lập ngưỡng thời gian tối đa, người dùng buộc phải rút thẻ ra khỏi miệng máy nhả thẻ, tính
        /// bằng giây. Quá thời gian này, máy nhả thẻ sẽ tự nuốt thẻ vào khay thẻ lỗi và hủy thẻ trên máy tính
        /// </summary>
        /// <param name="timeInSecond"></param>
        /// <returns></returns>
        public async Task<bool> SetTimeOutAutoCollectCard(int timeInSecond)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string setTimeOutAutoCollectCardCmd = $"SetTimeOutAutoCollectCard?/TimeOutAutoCollectCard={timeInSecond}/";

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, setTimeOutAutoCollectCardCmd, 500);
            });
            //SetTimeOutAutoCollectCard?/OK/
            //SetTimeOutAutoCollectCard?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetTimeOutAutoCollectCard",
                CMD = setTimeOutAutoCollectCardCmd
            });
            return false;
        }

        public async Task<string> GetTimeOutAutoCollectCard()
        {
            string getTimeOutAutoCollectCardCmd = "GetTimeOutAutoCollectCard?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getTimeOutAutoCollectCardCmd, 500));
            //GetTimeOutAutoCollectCard?/TimeOutAutoCollectCard=20/
            if (UdpTools.IsSuccess(response, "GetTimeOutAutoCollectCard?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string timeOutAutoCollectCard = map.ContainsKey("timeoutautocollectcard") ? map["timeoutautocollectcard"] : "";
                return timeOutAutoCollectCard;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetTimeOutAutoCollectCard",
                CMD = getTimeOutAutoCollectCardCmd
            });
            return "";
        }

        /// <summary>
        /// Với bộ MT166, chỉ có thể dùng chế độ xác thực KeyA, mặc định là KeyA sẽ được nạp vào sector0.
        /// Đầu đọc thẻ chỉ xác thực đúng KeyA trên sector0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> SetKeyA(string key)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string setKeyA = $"SetKeyA?/KeyA={key}/";

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, setKeyA, 500);
            });
            //SetKeyA?/OK/
            //SetKeyA?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetKeyA",
                CMD = setKeyA
            });
            return false;
        }

        /// <summary>
        /// Các chế độ của Wiegand có thể là 26 ,34,58,66 bit.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task<bool> SetWiegandMode(int mode)
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string setWiegandModeCmd = $"SetWiegandMode?/WiegandMode={mode}/";

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, setWiegandModeCmd, 500);
            });
            //SetWiegandMode?/OK/
            //SetWiegandMode?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetWiegandMode",
                CMD = setWiegandModeCmd
            });
            return false;
        }

        public async Task<string> GetWiegandMode()
        {
            string getWiegandModeCmd = "GetWiegandMode?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getWiegandModeCmd, 500));
            //GetWiegandMode?/WiegandMode=26/
            if (UdpTools.IsSuccess(response, "GetWiegandMode?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string wiegandMode = map.ContainsKey("wiegandmode") ? map["wiegandmode"] : "";
                return wiegandMode;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetWiegandMode",
                CMD = getWiegandModeCmd
            });
            return "";
        }

        /// <summary>
        /// ModeButton = 0 : sử dụng 1 Nút Bấm ( Sử dụng BTN1), 1 cơ cấu nhả thẻ ( Sử dụng COM1) , với chế độ này thì LoopMode chỉ có 2 tình huống: ModeLoop = 0, hoặc ModeLoop =1. Giá trị khác của ModeLoop khác sẽ tương đương với ModeLoop = 0 ( Không sử dụng vòng loop)<br/>
        /// ModeButton = 1 : sử dụng 1 Nút Bấm ( Sử dụng BTN1), 2 cơ cấu nhả thẻ ( Sử dụng cả COM1 và COM2), với chế độ này thì LoopMode chỉ có 2 tình huống: ModeLoop = 0, hoặc ModeLoop =1. Giá trị khác của ModeLoop khác sẽ tương đương với ModeLoop = 0 ( Không sử dụng vòng loop) <br/>
        /// ModeButton = 2 : Sử dụng 2 nút Bấm : 1 nút bấm xe máy và 1 nút bấm oto, 1 máy nhả thẻ COM1( Thẻ nhả ra là 1 trong 2 loại : oto hoặc xe máy, phụ thuộc vào nút bấm là loại gì )<br/>
        /// Có thể sử dụng kết hợp với 3 chế độ: ModeLoop =0,1,2<br/>
        /// ModeButton = 3 :sử dụng 2 nút bấm : 1 nút bấm xe máy nhả thẻ trên một máy, 1 nút bấm oto nhả thẻ trên 1 máy. Có thể sử dụng kết hợp với 4 chế độ: ModeLoop =0,1,2 <br/>
        /// Lưu ý: ModeButton được quy định bởi dipswitch 7,8 </summary>br>
        /// ModeButton = (dipswitch7 )*2 + dipswitch8 <br/>
        /// Khi gạt nên ON thì dipswitch7 = 1, Khi gạt OFF thì dipswitch7 = 0. Tương tự với dipswitch8 <br/>
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeButton()
        {
            string getModeButtonCmd = "GetModeButton?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getModeButtonCmd, 500));
            //GetWiegandMode?/ModeButton=0/
            if (UdpTools.IsSuccess(response, "GetModeButton?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string modeButton = map.ContainsKey("modebutton") ? map["modebutton"] : "";
                return modeButton;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetModeButton",
                CMD = getModeButtonCmd
            });
            return "";
        }

        /// <summary>
        /// ModeLoop = 0 : Không sử dụng vòng Loop nào cả
        /// ModeLoop = 1 : Sử dụng vòng Loop1 để cho phép nhấn thẻ 1 lần trên BTN1 và BTN2
        /// ModeLoop = 2 : sử dụng Loop1 để cho phép một lần nhả thẻ trên BTN1 ( xe máy ), Kết hợp Loop1 và Loop2 nhả thẻ 1 lần trên BTN2 – Oto ( bắt sườn sau của Loop1 để cho phép nhấn thẻ lần 2
        /// LoopMode = 3 : dự phòng.
        /// Lưu ý: ModeLoop được quy định bởi dipswitch 5,6: ModeLoop = (dipswitch5 )*2 + dipswitch6
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetModeLoop()
        {
            string getModeLoopCmd = "GetModeLoop?/";

            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string response = string.Empty;
            await Task.Run(() => response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, getModeLoopCmd, 500));
            //GetWiegandMode?/ModeLoop=0/
            if (UdpTools.IsSuccess(response, "GetModeLoop?/"))
            {
                string[] data = response.Split('/');
                Dictionary<string, string> map = GetEventContent(data);
                string modeLoop = map.ContainsKey("modeloop") ? map["modeloop"] : "";
                return modeLoop;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "GetModeLoop",
                CMD = getModeLoopCmd
            });
            return "";
        }

        public async Task<bool> SetPauseDispenseCard()
        {
            string comport = this.ControllerInfo.Comport;
            int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
            string setPauseDispenseCardCmd = $"SetPauseDispenseCard?/";

            string response = string.Empty;
            await Task.Run(() =>
            {
                response = UdpTools.ExecuteCommand_Ascii(comport, baudrate, setPauseDispenseCardCmd, 500);
            });
            //SetPauseDispenseCard?/OK/
            //SetPauseDispenseCard?/ERROR/
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            else if (UdpTools.IsSuccess(response, "ERROR"))
            {
                return false;
            }
            OnErrorEvent(new ControllerErrorEventArgs()
            {
                ErrorString = response,
                ErrorFunc = "SetPauseDispenseCard",
                CMD = setPauseDispenseCardCmd
            });
            return false;
        }
        #endregion

        #region Event
        private async void CallStatusAll(Bdk controller, Dictionary<string, string> map)
        {
            ConnectStatusCHangeEventArgs statusChange = new ConnectStatusCHangeEventArgs
            {
                CurrentStatus = this.ControllerInfo.IsConnect,
                DeviceId = this.ControllerInfo.Id,
                DeviceName = this.ControllerInfo.Name,
                DeviceType = EmParkingControllerType.Card_Dispenser,
                DispenserOnChange = new CardDispenserOnChange()
            };

            // Kiểm tra trạng thái hoạt động working / stop
            if (map.ContainsKey("statework"))
            {
                string status = map["statework"];

                if (status.ToLower() == "working")
                {
                    statusChange.DispenserOnChange.StatusProcessing = true;
                    this.cardDispenserStatus.IsWorking = true;
                }
                else if (status.ToLower() == "stop")
                {
                    statusChange.DispenserOnChange.StatusProcessing = false;
                }
            }

            // Kiểm tra Cảnh báo máy nhả thẻ
            if (map.ContainsKey("statecarddispensercom1") || map.ContainsKey("statecarddispensercom2"))
            {
                string dispenser1_title = "statecarddispensercom1";
                string dispenser2_title = "statecarddispensercom2";

                var items = new[] { dispenser1_title, dispenser2_title };
                foreach (var item in items)
                {
                    if (!map.ContainsKey(item))
                    {
                        continue;
                    }

                    string Dispenserstr = map[item];
                    int dispenserIndex = item == dispenser1_title ? 1 : 2;

                    if (Dispenserstr == "" || Dispenserstr == "NotConnect")
                    {
                        continue;
                    }

                    string binaryValue = MT166Helper.HexToBinary(Dispenserstr);
                    bool isHetThe = binaryValue[0] == '1';          // b7
                    bool isTheTrenBezel = binaryValue[1] == '1';    // b6
                    bool isTheODoc = binaryValue[2] == '1';         // b5
                    bool isSapHetThe = binaryValue[3] == '1';       // b4
                    bool isPhatThe = binaryValue[4] == '1';         // b3
                    bool isThuThapThe = binaryValue[5] == '1';      // b2
                    bool isNhaTheBiLoi = binaryValue[6] == '1';     // b1
                    bool isTaiCheNgoaiGio = binaryValue[7] == '1';  // b0

                    StateCardDispenser dis = new StateCardDispenser()
                    {
                        DispenserIndex = dispenserIndex,
                        IsCardEmptyDispenser = isHetThe,
                        IsLessCardDispenser = isSapHetThe,
                        IsCardErrorDispenser = isNhaTheBiLoi,
                    };
                    EmCardDispenserStatus cardDispenserStatus = EmCardDispenserStatus.ConThe;
                    if (isHetThe)
                    {
                        cardDispenserStatus = EmCardDispenserStatus.HetThe;
                    }
                    else if (isSapHetThe)
                    {
                        cardDispenserStatus = EmCardDispenserStatus.SapHetThe;
                    }
                    else if (isNhaTheBiLoi)
                    {
                        cardDispenserStatus = EmCardDispenserStatus.LoiNhaThe;
                    }
                    if (this.cardDispenserStatus.DispensersStatus.ContainsKey(dispenserIndex))
                    {
                        this.cardDispenserStatus.DispensersStatus[dispenserIndex] = cardDispenserStatus;
                    }
                    else
                    {
                        this.cardDispenserStatus.DispensersStatus.Add(dispenserIndex, cardDispenserStatus);
                    }
                    statusChange.DispenserOnChange.ListStatusDispenser.Add(dis);
                }
            }


            // Kiểm tra Trạng thái sử dụng các chân input
            if (map.ContainsKey("arrayinput") || map.ContainsKey("ArrayInput"))
            {
                string arrayinput = map["arrayinput"];

                bool[] states = arrayinput.Select(c => c == '1').ToArray();

                bool D_In1 = states.Length > 0 ? states[0] : false;
                bool D_In2 = states.Length > 1 ? states[1] : false;
                bool D_In3 = states.Length > 2 ? states[2] : false;
                bool D_In4 = states.Length > 3 ? states[3] : false;

                bool BTN1 = states.Length > 4 ? states[4] : false;
                bool BTN2 = states.Length > 5 ? states[5] : false;

                bool Open = states.Length > 6 ? states[6] : false;
                bool Stop = states.Length > 7 ? states[7] : false;
                bool Close = states.Length > 8 ? states[8] : false;

                bool loop1 = states.Length > 9 ? states[9] : false;
                bool loop2 = states.Length > 10 ? states[10] : false;
                bool loop3 = states.Length > 11 ? states[11] : false;

                if (D_In1 == true || D_In2 == true || D_In3 == true || D_In4 == true)
                {
                    if (D_In2 == true)
                    {

                    }
                    // Chân Input độc lập với GetEvent
                    ArrayInputDispenser array = new ArrayInputDispenser()
                    {
                        Alarm = 1,    // Gán alarm = 1 /web để dùng nút gọi hỗ trợ 
                        DIn1 = D_In1,
                        DIn2 = D_In2,
                        DIn3 = D_In3,
                        DIn4 = D_In4,
                    };

                    statusChange.DispenserOnChange.ArrayInputDispenser = array;
                }
            }

            string newHash = ComputeDispenserStatusHash(statusChange);

            // Có thay đổi mới gửi và gửi định kỳ sau 5p
            if (newHash != _lastDispenserStatusHash || (DateTime.Now - _lastEventSentTime) >= TimeSpan.FromMinutes(5))
            {
                _lastDispenserStatusHash = newHash;
                _lastEventSentTime = DateTime.Now;

                OnConnectStatusChangeEvent(statusChange);
            }

        }
        private string ComputeDispenserStatusHash(ConnectStatusCHangeEventArgs statusChange)
        {
            var sb = new StringBuilder();

            sb.Append(statusChange.CurrentStatus);
            sb.Append(statusChange.DeviceId);
            sb.Append(statusChange.DeviceName);
            sb.Append(statusChange.DeviceType);

            if (statusChange.DispenserOnChange != null)
            {
                sb.Append(statusChange.DispenserOnChange.StatusProcessing);

                if (statusChange.DispenserOnChange.ListStatusDispenser != null)
                {
                    foreach (var state in statusChange.DispenserOnChange.ListStatusDispenser)
                    {
                        sb.Append(state.DispenserIndex);
                        sb.Append(state.IsCardEmptyDispenser);
                        sb.Append(state.IsLessCardDispenser);
                        sb.Append(state.IsCardErrorDispenser);
                    }
                }

                if (statusChange.DispenserOnChange.ArrayInputDispenser != null)
                {
                    var array = statusChange.DispenserOnChange.ArrayInputDispenser;
                    sb.Append(array.Alarm);
                    sb.Append(array.DIn1);
                    sb.Append(array.DIn2);
                    sb.Append(array.DIn3);
                    sb.Append(array.DIn4);
                }
            }

            using (var sha = SHA256.Create())
            {
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
                return Convert.ToBase64String(hashBytes);
            }
        }

        private void CallCardOnRFEvent(Bdk controller, Dictionary<string, string> map, MT166V8EventType eventType)
        {
            CardOnRFEventArgs e = new CardOnRFEventArgs
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                AllCardFormats = new List<string>(),
                DeviceType = EmParkingControllerType.Card_Dispenser,
            };
            string cardNumberHEX = map.ContainsKey("card") ? map["card"] : "";
            string lenCard = map.ContainsKey("lencard") ? map["lencard"] : "";

            e.PreferCard = cardNumberHEX;
            e.AllCardFormats.Add(e.PreferCard);
            string str_readerIndex = map.ContainsKey("input") ? map["input"] : "";

            e.ReaderIndex = Regex.IsMatch(str_readerIndex, @"^\d+$") ? Convert.ToInt32(str_readerIndex) : -1;

            OnCardInRFEvent?.Invoke(this, e);
        }

        private void CallCardEvent(Bdk controller, Dictionary<string, string> map, MT166V8EventType eventType)
        {
            CardEventArgs e = new CardEventArgs
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                AllCardFormats = new List<string>(),
                DeviceType = EmParkingControllerType.Card_Dispenser,
            };
            string cardNumberHEX = map.ContainsKey("card") ? map["card"] : "";
            string lenCard = map.ContainsKey("lencard") ? map["lencard"] : "";

            e.PreferCard = cardNumberHEX;

            LastSwipeCardInfo = new LastSwipeCardInfo()
            {
                EventTime = DateTime.Now,
                LastCard = cardNumberHEX
            };
            e.AllCardFormats.Add(e.PreferCard);
            string str_readerIndex = map.ContainsKey("input") ? map["input"] : "";

            e.ReaderIndex = Regex.IsMatch(str_readerIndex, @"^\d+$") ? Convert.ToInt32(str_readerIndex) : -1;
            if (e.ReaderIndex == (int)MT166V8EventType.BTN1_LOOP1)
            {
                e.ReaderIndex = 1;
                e.InputType = EmInputType.Button;
            }
            else if (e.ReaderIndex == (int)MT166V8EventType.BTN2_LOOP1)
            {
                e.ReaderIndex = 2;
                e.InputType = EmInputType.Button;
            }
            else if (eventType == MT166V8EventType.Reader1 || eventType == MT166V8EventType.Reader2)
            {
                e.ReaderIndex = e.ReaderIndex == 3 ? 1 : (e.ReaderIndex == 4 ? 2 : 3);
            }
            if ((eventType == MT166V8EventType.Button1) ||
                (eventType == MT166V8EventType.Button2) ||
                (eventType == MT166V8EventType.BTN1_LOOP1) ||
                (eventType == MT166V8EventType.BTN2_LOOP1)
                )
            {
                e.InputType = EmInputType.Button;
            }
            else if ((eventType == MT166V8EventType.BTN1_ABNORMAL) || (eventType == MT166V8EventType.BTN2_ABNORMAL))
            {
                e.InputType = EmInputType.ButtonAbnormal;
            }
            else if (eventType == MT166V8EventType.CardOut)
            {
                e.InputType = EmInputType.CardOut;
            }
            else
            {
                // Card + CardOut
                e.InputType = EmInputType.Card;
            }

            if (e.InputType == EmInputType.Button || e.InputType == EmInputType.ButtonAbnormal)
            {
                if ((int)eventType == 1 || (int)eventType == 15)
                {
                    e.ButtonIndex = 1;
                }
                else if ((int)eventType == 2 || (int)eventType == 16)
                {
                    e.ButtonIndex = 2;
                }
                else
                {
                    e.ButtonIndex = (int)eventType;
                }
            }

            OnCardEvent(e);
        }
        private void CallInputEvent(Bdk controller, Dictionary<string, string> map, MT166V8EventType eventType)
        {
            InputEventArgs ie = new InputEventArgs
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                DeviceType = EmParkingControllerType.Card_Dispenser,
            };
            string str_inputName = map.ContainsKey("input") ? map["input"] : "";
            if (!string.IsNullOrEmpty(str_inputName))
            {
                string str_inputIndex = str_inputName.Replace("input", "");
                ie.InputIndex = Regex.IsMatch(str_inputIndex, @"^\d+$") ? int.Parse(str_inputIndex) : -1;
            }
            if (ie.InputIndex == (int)MT166V8EventType.Loop1) ie.InputIndex = 3;
            if (ie.InputIndex == (int)MT166V8EventType.Loop2) ie.InputIndex = 4;
            if (ie.InputIndex == (int)MT166V8EventType.Loop3) ie.InputIndex = 5;

            ie.InputType = eventType == MT166V8EventType.CardbeTaken ? EmInputType.CardbeTaken : InputTupe.EmInputType.Loop;

            OnInputEvent(ie);
        }

        private void CallCardEventCancel(Bdk controller, Dictionary<string, string> map, MT166V8EventType eventType)
        {
            CardCancelEventArgs ce = new CardCancelEventArgs
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                DeviceType = EmParkingControllerType.Card_Dispenser,
            };

            string cardHex = map.ContainsKey("card") ? map["card"] : "";
            string lenCard = map.ContainsKey("lencard") ? map["lencard"] : "";

            if (lenCard == "4") cardHex = cardHex.PadLeft(8, '0');
            else if (lenCard == "3") cardHex = cardHex.PadLeft(6, '0');
            else if (lenCard == "7") cardHex = cardHex.PadLeft(14, '0');

            ce.PreferCard = cardHex;
            ce.FunctionKey = (int)eventType;

            OnCardEventCancel(ce);
        }
        private void CallExitEvent(Bdk controller, string doorNo)
        {
            InputEventArgs ie = new()
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                DeviceType = EmParkingControllerType.Card_Dispenser
            };
            ie.InputIndex = int.Parse(doorNo);
            ie.InputType = EmInputType.Exit;
            OnInputEvent(ie);
        }
        private void CallCardBeTakenEvent(Bdk controller, string doorNo)
        {
            CardBeTakenEventArgs ie = new()
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                DeviceType = EmParkingControllerType.Card_Dispenser
            };
            ie.InputIndex = int.Parse(doorNo);
            ie.InputType = EmInputType.CardbeTaken;

            OnCardBeTakenEvent(ie);
        }
        #endregion   
    }
}

// Fixme: Fix cung response
//if (Environment.MachineName == "PC-KIEN")
//{
//    response = "GetEvent?/LenCard=4/Card=0/Input=0/ArrayInput=000000000000/Com=Com1/StateCardDispenserCom1=0/StateCardDispenserCom2=2/StateWork=working/ArrayInputCur=000000000000/";
//}
//response = "GetEvent?/LenCard=4/Card=7C19F640/Input=9/ArrayInput=X/Com=Com1/StateCardDispenserCom1=90/StateCardDispenserCom2=Z/StateWork=Working/ArrayInputCur=P/";
//response = "GetEvent?/LenCard=0/Card=0/Input=9/ArrayInput=001000/Com=NotCom/StateCardDispenserCom1=10/StateCardDispenserCom2=90/StateWork=Working/";
