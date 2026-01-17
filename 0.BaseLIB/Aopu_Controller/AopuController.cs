using iParkingv5.Objects.Enums;
using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using System.Text.RegularExpressions;
using TcpipIntface.Code.Client;
using static iParkingv5.Objects.Enums.CommunicationTypes;
using iParkingv5.Objects;
using Kztek.Tool;
using System;

namespace iParkingv5.Controller.Aopu
{
    public class AopuController : IController
    {
        public Bdk ControllerInfo { get; set; }
        public bool IsBusy = false;
        private Thread? thread = null;
        private ManualResetEvent? stopEvent = null;
        public event CancelEventHandler? CancelEvent;

        public CancellationTokenSource? cts { get; set; }
        public ManualResetEvent? ForceLoopIteration { get; set; }

        private AcsTcpClass TcpipObj = new AcsTcpClass(true);
        #region Event
        public event CardEventHandler? CardEvent;
        public event ControllerErrorEventHandler? ErrorEvent;
        public event InputEventHandler? InputEvent;
        public event ConnectStatusChangeEventHandler? ConnectStatusChangeEvent;
        public event DeviceInfoChangeEventHandler? DeviceInfoChangeEvent;
        public event FingerEventHandler? FingerEvent;

        // Signal thread to stop work
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent?.Set();
            }
        }
        // Wait for thread stop
        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();

                Free();
            }
        }
        private void Free()
        {
            thread = null;
            // release events
            stopEvent?.Close();
            stopEvent = null;
        }
        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, so free resources
                    Free();
                }
                return false;
            }
        }
        public void PollingStart()
        {
            try
            {
                TcpipObj.OnEventHandler += TcpipObj_OnEventHandler;
                TcpipObj.OnDisconnect += TcpipObj_OnDisconnect;
            }
            catch (Exception ex)
            {
            }
        }

        private void TcpipObj_OnDisconnect()
        {
            ControllerInfo.IsConnect = false;
            _ = ConnectAsync();
        }

        DateTime _olddatetime = DateTime.Now.AddMinutes(-1);
        DateTime _olddatetimecard = DateTime.Now.AddDays(-1);
        string _oldcardnumber = "";
        byte _olddoor = 0;
        byte _oldeventtype = 0;

        Dictionary<string, DateTime> lastCardEvent = new Dictionary<string, DateTime>();
        void TcpipObj_OnEventHandler(string SerialNo, byte EType,
                           DateTime Datetime, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year,
                           byte DoorStatus,
                           byte Ver, byte FuntionByte, bool Online, byte CardsofPackage, string CardNo, string QRCode, byte Door, byte EventType,
                           ushort CardIndex, byte CardStatus, byte reader, out byte relay, out bool OpenDoor, out bool Ack)

        {
            //CallCardEvent(ControllerInfo, "21118815", "1");

            this.ControllerInfo.IsConnect = true;
            Ack = true;
            OpenDoor = false;
            relay = 0;
            string dt = Datetime.ToString();
            foreach (Futech.Objects.Controller controller in controllers)
            {
                controller.IsConnect = true;
                switch (EType)
                {
                    case 0:
                        break;
                    case 1:
                        try
                        {
                            if (lastCardEvent.ContainsKey(CardNo))
                            {
                                if (Math.Abs((lastCardEvent[CardNo] - Datetime).TotalSeconds) <= StaticPool.appOption.MinDelayCardTime)
                                {
                                    return;
                                }
                                else
                                {
                                    lastCardEvent[CardNo] = Datetime;
                                }
                            }
                            else
                            {
                                lastCardEvent.Add(CardNo, Datetime);
                                _olddatetimecard = Datetime;
                                _olddoor = Door;
                                _oldeventtype = EventType;
                                _oldcardnumber = CardNo;
                            }
                        }
                        catch
                        { }
                        CallCardEvent(ControllerInfo, CardNo, Door.ToString());
                        break;
                    case 2:
                        try
                        {
                            if (_olddatetime == Datetime &&
                                _olddoor == Door &&
                                _oldeventtype == EventType)
                            {
                                return;
                            }
                            else
                            {
                                _olddatetime = Datetime;
                                _olddoor = Door;
                                _oldeventtype = EventType;
                            }
                        }
                        catch { }

                        switch (EventType)
                        {
                            //Sự kiện alarm
                            case 41:
                                break;
                            //Sự kiện Loop
                            case 56:
                                //MessageBox.Show("INput");
                                CallInputEvent(ControllerInfo, Door.ToString());
                                break;
                            //Sự kiện nút nhấn EXIT
                            case 59:
                                //MessageBox.Show("Ẽit");
                                CallExitEvent(ControllerInfo, Door.ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        public void PollingStop()
        {
            try
            {
                TcpipObj.OnEventHandler -= TcpipObj_OnEventHandler;
                TcpipObj.OnDisconnect -= TcpipObj_OnDisconnect;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion End Event

        #region: CONNECT
        public async Task<bool> TestConnectionAsync()
        {
            if (IS_TCP((EM_CommunicationType)(ControllerInfo.CommunicationType)))
            {
                return NetWorkTools.IsPingSuccess(ControllerInfo.Comport, 500);
            }
            return false;
        }
        public async Task<bool> ConnectAsync()
        {
            if (await TestConnectionAsync())
            {
                bool result;
                try
                {
                    result = TcpipObj.OpenIP(ControllerInfo.Comport, int.Parse(ControllerInfo.Baudrate), 23456);
                    ControllerInfo.IsConnect = result;
                }
                catch (Exception ex)
                {
                    ErrorEvent?.Invoke(this, new ControllerErrorEventArgs()
                    {
                        ErrorString = ex.Message
                    });
                    result = false;
                }
                return result;
            }
            return false;
        }
        public async Task<bool> DisconnectAsync()
        {
            try
            {
                TcpipObj.EventDisConnect();
                TcpipObj.Restart();
                TcpipObj.CloseTcpip();
            }
            catch (Exception)
            {
            }
            return true;
        }
        #endregion: END CONNECT

        #region DATE - TIME
        public async Task<DateTime> GetDateTime()
        {
            return DateTime.Now;
        }
        public async Task<bool> SetDateTime(DateTime time)
        {
            return false;
        }
        public async Task<bool> SyncDateTime()
        {
            return false;
        }
        #endregion END DATE - TIME

        #region:TCP_IP
        //GET
        public async Task<string> GetIPAsync()
        {
            return string.Empty;
        }
        public async Task<string> GetMacAsync()
        {
            return string.Empty;
        }
        public async Task<string> GetDefaultGatewayAsync()
        {
            return string.Empty;
        }

        public async Task<int> GetPortAsync()
        {
            return 8000;
        }
        public async Task<string> GetComkeyAsync()
        {
            return string.Empty;
        }
        //SET
        public async Task<bool> SetMacAsync(string macAddr)
        {
            return false;
        }
        public async Task<bool> SetNetWorkInforAsync(string ip, string subnetMask, string defaultGateway, string macAddr)
        {
            return false;
        }
        public async Task<bool> SetComKeyAsync(string comKey)
        {
            return false;
        }
        #endregion: END TCP_IP

        #region System
        public async Task<bool> ClearMemory()
        {
            return false;
        }
        public async Task<bool> RestartDevice()
        {
            return false;
        }
        public async Task<bool> ResetDefault()
        {
            return false;
        }
        #endregion End System

        protected void OnConnectStatusChangeEvent(ConnectStatusCHangeEventArgs e)
        {
            ConnectStatusChangeEvent?.Invoke(this, e);
        }
        protected void OnErrorEvent(ControllerErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }
        protected void OnCardEvent(CardEventArgs e)
        {
            CardEvent?.Invoke(this, e);
        }
        protected void OnInputEvent(InputEventArgs e)
        {
            InputEvent?.Invoke(this, e);
        }

        private void CallInputEvent(Bdk controller, string doorNo)
        {
            InputEventArgs ie = new()
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
            };
            ie.InputIndex = int.Parse(doorNo);
            ie.InputType = InputTupe.EmInputType.Loop;
            OnInputEvent(ie);
        }
        private void CallExitEvent(Bdk controller, string doorNo)
        {
            InputEventArgs ie = new()
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
            };
            ie.InputIndex = int.Parse(doorNo);
            ie.InputType = InputTupe.EmInputType.Exit;

            OnInputEvent(ie);
        }
        private void CallCardEvent(Bdk controller, string cardNumberInt, string readerIndex)
        {
            CardEventArgs e = new()
            {
                DeviceId = controller.Id,
                DeviceName = controller.Name,
                AllCardFormats = new List<string>(),
            };
            string cardNumberHex = Convert.ToInt64(cardNumberInt).ToString("X");
            e.PreferCard = cardNumberHex;
            e.AllCardFormats.Add(cardNumberHex);
            e.AllCardFormats.Add(cardNumberInt);
            e.ReaderIndex = Regex.IsMatch(readerIndex, @"^\d+$") ? Convert.ToInt32(readerIndex) : -1;
            OnCardEvent(e);
        }

        public async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            if (relayIndex == 1) return OpenDoor1();
            else if (relayIndex == 2) return OpenDoor2();
            return false;
        }
        public bool OpenDoor1()
        {
            return TcpipObj.Opendoor(0);
        }
        public bool OpenDoor2()
        {
            return TcpipObj.Opendoor(1);
        }

        public Task<bool> AddFinger(List<string> fingerDatas)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyFinger(string userId, int fingerIndex, string fingerData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFinger(string userId, int fingerIndex)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddFinger(List<string> fingerDatas, string customerName, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyFinger(List<string> fingerDatas, string customerName, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CollectCard()
        {
            return false;
        }

        public async Task<bool> DispenseCard()
        {
            return false;
        }

        public Task<bool> RejectCard()
        {
            throw new NotImplementedException();
        }
    }
}