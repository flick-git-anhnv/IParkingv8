using iParkingv5.Controller.KztekDevices.BR_CTRL;
using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using Kztek.Tool;
using System.Text.RegularExpressions;
using static Kztek.Object.InputTupe;

namespace iParkingv5.Controller.KztekDevices.KZE02NETController
{
    public class KzE02Net : BaseKzDevice, IBollardCtrl, ICardDispenser
    {
        private Thread? thread = null;
        private ManualResetEvent? stopEvent = null;
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
        public event OnBarrieChangeStatusEventHandler? OnBollardChangeStatusChangeEvent;
        private bool[] currentStatus = new bool[8];

        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
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
        int last = 1;
        public async void WorkerThread()
        {
            while (stopEvent != null)
            {
                if (stopEvent.WaitOne(0, true))
                {
                    return;
                }
                try
                {
                    string response = await GetEvent();
                    //if (last == 1)
                    //{
                    //    last = 2;
                    //}
                    //else
                    //{
                    //    last = 1;
                    //}
                    //string response = $"GetEvent?/Style=Card/UserID=100/LenCard=4/Card=7C19F640/Reader=0{last}/DateTime=YYYYMMDDhhmmss/CardState=U/AccessState=1/Door=00/StateMSG=00";
                    //response = "GetEvent?/Style=Card/UserID=NULL/LenCard=12/Card=3416214B9400A15F0105F05F/Reader=01/DateTime=20001110224853/CardState=U/AccessState=0/Door=00/StateMSG=02";
                    this.ControllerInfo.IsConnect = response != "" && !response.Contains("exception", StringComparison.CurrentCultureIgnoreCase);

                    //AccessCardGrant: Char(2) + GetEvent?/Style=Card/UserID=100/LenCard=4/Card=7C19F640/Reader=01/DateTime=YYYYMMDDhhmmss/CardState=U/AccessState=1/Door=00/StateMSG=00 + char(3)
                    //AccessCardDenie: Char(2) + GetEvent?/Style=Card/UserID=Null/LenCard=4/Card=7C19F640/Reader=01/DateTime=YYYYMMDDhhmmss/CardState=U/AccessState=1/Door=00/StateMSG=00 + char(3)
                    //InputEvent     : Char(2) + GetEvent?/Style=input/Input=INPUT1/DateTime=YYYYMMDDhhmmss + char(3)
                    //NoEvent        : Char(2) + GetEvent?/NotEvent + char(3)
                    if (response != "" && response.Contains("GetEvent?/") && !response.Contains("NotEvent"))
                    {
                        string[] data = response.Split('/');
                        Dictionary<string, string> map = GetEventContent(data);
                        bool isCardEvent = response.Contains("Card");
                        if (isCardEvent)
                        {
                            await CallCardEvent(this.ControllerInfo, map);
                        }
                        else
                        {
                            await CallInputEvent(this.ControllerInfo, map);
                        }
                    }
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

        private async Task<string> GetEvent()
        {
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string getEventCmd = KZTEK_CMD.GetEventCMD();
                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name,
                                                                            comport, baudrate, getEventCmd, false);
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
                await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name,
                                                          comport, baudrate, cmd, false);
            }
            catch (Exception)
            {
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task CallInputEvent(Bdk controller, Dictionary<string, string> map)
        {
            InputEventArgs ie = new InputEventArgs
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                DeviceType = EmParkingControllerType.NomalController,
            };
            string str_inputName = map.ContainsKey("input") ? map["input"] : "";
            if (!string.IsNullOrEmpty(str_inputName))
            {
                string str_inputIndex = str_inputName.Replace("INPUT", "");
                ie.InputIndex = Regex.IsMatch(str_inputIndex, @"^\d+$") ? int.Parse(str_inputIndex) : -1;
            }
            if (ie.InputIndex == 1 || ie.InputIndex == 2)
            {
                ie.InputType = EmInputType.Exit;
            }
            else if (ie.InputIndex == 3 || ie.InputIndex == 4)
            {
                ie.InputType = EmInputType.Loop;
            }
            this.ControllerInfo.SaveDeviceLog("Receive Exit Event", $"ExitIndex: {ie.InputIndex}");
            await DeleteCardEvent();
            OnInputEvent(ie);
        }
        private async Task CallCardEvent(Bdk controller, Dictionary<string, string> map)
        {
            try
            {
                CardEventArgs e = new CardEventArgs
                {
                    DeviceId = controller.Id,
                    AllCardFormats = new List<string>(),
                    DeviceName = controller.Name,
                    DeviceType = EmParkingControllerType.NomalController,
                };
                string cardNumberHEX = map.ContainsKey("card") ? map["card"] : "";
                e.PreferCard = cardNumberHEX;
                string str_readerIndex = map.ContainsKey("reader") ? map["reader"] : "";
                e.ReaderIndex = Regex.IsMatch(str_readerIndex, @"^\d+$") ? Convert.ToInt32(str_readerIndex) : 0;
                string cardState = map.ContainsKey("cardstate") ? map["cardstate"] : "";
                if (cardState == "R")
                {
                    string door = map.ContainsKey("door") ? map["door"] : "";
                    if (!string.IsNullOrEmpty(door))
                    {
                        if (door == "01")
                        {
                            e.Doors = "1";
                        }
                        if (door == "02")
                        {
                            e.Doors = "2";
                        }
                    }
                }
                else
                {
                    e.Doors = "";
                }
                this.ControllerInfo.SaveDeviceLog("Receive Card Event", $"Card: {e.PreferCard} - Reader: {e.ReaderIndex}");
                OnCardEvent(e);
                await DeleteCardEvent();
            }
            catch (Exception)
            {
            }

        }

        public override async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            try
            {
                await semaphoreSlim.WaitAsync();

                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = KZTEK_CMD.OpenRelayCMD(relayIndex);

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name,
                                                                            comport, baudrate, openRelayCmd, true);
                Task.Run(async () =>
                {
                    await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
                    await Task.Delay(300);
                    await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name, comport, baudrate, openRelayCmd, true);
                });
                if (string.IsNullOrEmpty(response))
                {
                    response = "";
                }

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
                    DeviceId = this.ControllerInfo?.Id ?? "",
                    DeviceName = this.ControllerInfo?.Name ?? "",
                    ErrorString = response,
                    ErrorFunc = openRelayCmd,
                    CMD = openRelayCmd,
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

        public async Task<bool> CollectCard()
        {
            //return true;
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = KZTEK_CMD.OpenRelayCMD(3);

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name,
                                                                            comport, baudrate, openRelayCmd, true);
                if (string.IsNullOrEmpty(response))
                {
                    response = "";
                }

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
                    DeviceId = this.ControllerInfo?.Id ?? "",
                    DeviceName = this.ControllerInfo?.Name ?? "",
                    ErrorString = response,
                    ErrorFunc = openRelayCmd,
                    CMD = openRelayCmd,
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
        public async Task<bool> RejectCard()
        {
            //return true;
            try
            {
                await semaphoreSlim.WaitAsync();
                string comport = this.ControllerInfo.Comport;
                int baudrate = GetBaudrate(this.ControllerInfo.Baudrate);
                string openRelayCmd = KZTEK_CMD.OpenRelayCMD(4);

                string response = await UdpTools.ReceiveSocketResponseAsync(this.ControllerInfo.Id, this.ControllerInfo.Name,
                                                                            comport, baudrate, openRelayCmd, true);
                if (string.IsNullOrEmpty(response))
                {
                    response = "";
                }

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
                    DeviceId = this.ControllerInfo?.Id ?? "",
                    DeviceName = this.ControllerInfo?.Name ?? "",
                    ErrorString = response,
                    ErrorFunc = openRelayCmd,
                    CMD = openRelayCmd,
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
        public async Task<bool> DispenseCard()
        {
            return false;
        }

        public async Task<bool> SetAudio(int relayIndex)
        {
            return false;
        }
        public bool CloseBarrie()
        {
            return false;
        }
        public bool OpenBarrie()
        {
            return false;
        }
        public bool StopBarrie()
        {
            return false;
        }
    }
}