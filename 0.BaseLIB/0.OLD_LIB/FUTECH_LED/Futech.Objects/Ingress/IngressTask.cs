using Futech.Tools;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Futech.Objects.Ingress
{
    public class IngressTask
    {
        #region: Properties
        //--private
        private Thread thread = null;
        private Ingressus.Ingressus sdk = null;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private bool isbusy = false;
        private string lineID = "";
        private string comport = "COM14";
        private int baudrate = 9600;
        private int communicationtype = 0;
        private int delaytime = 300;
        private byte address = 0x00;
        private bool isconnect = false;
        private ControllerCollection controllers = new ControllerCollection();
        private int count = 0;

        private int timeout = 0;
        public bool IsReadingEvent { get; set; } = false;
        public SerialPort serialPort = null;
        public string LineID
        {
            set { lineID = value; }
        }
        public string ComPort
        {
            get { return comport; }
            set { comport = value; }
        }
        public int BaudRate
        {
            get { return baudrate; }
            set { baudrate = value; }
        }
        public int CommunicationType
        {
            get { return communicationtype; }
            set { communicationtype = value; }
        }
        public int DelayTime
        {
            get { return delaytime; }
            set { delaytime = value; }
        }
        public int Address
        {
            set { address = Convert.ToByte(ByteUI.DecimalToBase(value, 16), 16); }
        }
        public bool IsConnect
        {
            get { return isconnect; }
            set
            {
                if (isconnect != value)
                {
                    isconnect = value;
                    if (this.controllers != null)
                    {
                        if (this.controllers.Count > 0)
                        {
                        }
                    }

                }
            }
        }
        public ControllerCollection Controllers
        {
            set { controllers = value; }
        }
        public bool IsStopGetEvent { get; set; } = false;

        //--event
        private CancellationTokenSource cts;
        private ManualResetEvent ForceLoopIteration;

        public event InputEventHandler InputEvent;
        public event CardEventHandler CardEvent;

        #endregion: End Properties

        #region: Constructor
        #endregion: End Constructor

        #region: Private Function
        private long ConvertCard(string cardumber, int cardType)
        {
            return int.Parse(cardumber);

            ////Convert from 3.5D -> 2.4H
            //try
            //{
            //    if (cardType == (int)EM_CardType.PROXIMITY_LEN10)
            //    {
            //        return int.Parse(cardumber);
            //    }
            //    else if (cardType == (int)EM_CardType.PROXIMITY_LEN8)
            //    {
            //        try
            //        {
            //            string temp = Convert.ToInt32(cardumber).ToString("00000000");
            //            string s1 = Convert.ToInt32(temp.Substring(0, 3)).ToString("X");
            //            string s2 = Convert.ToInt32(temp.Substring(3)).ToString("X");
            //            while (s2.Length < 4)
            //                s2 = "0" + s2;
            //            int card = int.Parse(s1 + s2, System.Globalization.NumberStyles.HexNumber);
            //            return card;
            //        }
            //        catch
            //        {
            //            return 0;
            //        }
            //    }
            //    else
            //    {
            //        return Convert.ToInt64(cardumber, 16);
            //    }
            //}
            //catch
            //{
            //    return 0;
            //}
        }
        #endregion: End Private Function

        #region: Public Function
        #region: Relay
        public bool Unlock(int doorNo)
        {
            if (isconnect)
            {
                //sdk.RemoteNormalOpenDoor(doorNo);
                sdk.RemoteOpenDoor(1, 3);
                sdk.RemoteCloseDoor(1);
                return true;
            }
            return false;
        }

        public bool OpenfileAlarm(int doorIndex)
        {
            if (isconnect)
            {
                return sdk.RemoteNormalOpenDoor(doorIndex);
            }
            return false;
        }
        public bool CloseFireAlarm(int doorIndex)
        {
            if (isconnect)
            {
                return sdk.RemoteNormalCloseDoor(doorIndex);
            }
            return false;
        }
        #endregion: End Relay

        #region: Connect
        public bool Connect(string _ComPort, int _BaudRate, int _CommunicationType)
        {
            try
            {
                if (this.sdk != null)
                {
                    try
                    {
                        this.sdk.Disconnect();
                        this.sdk = null;
                        this.sdk = new Ingressus.Ingressus();
                        this.sdk.SetConnectionTimeout(5);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    this.sdk = new Ingressus.Ingressus();
                    this.sdk.SetConnectionTimeout(5);
                }
                ComPort = _ComPort;
                BaudRate = _BaudRate;
                CommunicationType = _CommunicationType;

                if (communicationtype == 0)
                {
                    if (sdk.Connect_COMM(int.Parse(ComPort.Replace("COM", "").Trim()), BaudRate, address))
                    {
                        IsConnect = true;
                        return true;
                    }
                }
                else if (communicationtype == 1)
                {
                    if (IsPingSuccess() == false)
                    {
                        IsConnect = false;
                        return false;
                    }
                    if (sdk.Connect_TCPIP(ComPort, BaudRate) == true)
                    {
                        IsConnect = true;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public bool DisConnect()
        {
            try
            {
                bool isDisconnectSuccess = sdk.Disconnect();
                sdk = null;
                return isDisconnectSuccess;
                //return false;
            }
            catch
            { }
            return false;
        }
        public bool Reconnect()
        {
            DisConnect();
            return Connect(comport, baudrate, communicationtype);
            //return false;
        }
        public bool IsPingSuccess()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = null;
                reply = ping.Send(comport, 200);
                if (reply != null && reply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion: End Connect


        #endregion: End Public Function

        #region: Event
        public void PollingStart(ControllerCollection controllers)
        {

            this.controllers = controllers;
            this.controllers = controllers;
            cts = new CancellationTokenSource();
            ForceLoopIteration = new ManualResetEvent(false);

            StartTimer();

            Task.Run(() =>
                DoWork(cts.Token), cts.Token
            );
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
        public void SignalToStop()
        {
            // stop thread
            //if (thread != null)
            //{
            //    // signal to stop
            //    stopEvent.Set();
            //}
        }
        public void WaitForStop()
        {
            //if (thread != null)
            //{
            //    // wait for thread stop
            //    //thread.Join();
            //    try
            //    {
            //        thread.Join(5000);
            //    }
            //    catch
            //    { }

            //    Free();
            //}
        }
        private void Free()
        {
            //thread = null;


            //stopEvent.Close();
            //stopEvent = null;
        }
        public void PollingStop()
        {

            //if (this.Running)
            //{
            //    SignalToStop();
            //    while (thread.IsAlive)
            //    {
            //        if (WaitHandle.WaitAll(
            //            (new ManualResetEvent[] { stopEvent }),
            //            100,
            //            true))
            //        {
            //            WaitForStop();
            //            break;
            //        }

            //        Application.DoEvents();
            //    }
            //}

            timer.Stop();
            timer.Tick -= Timer_Tick;
            cts?.Cancel();
            WaitHandle.WaitAny(
                        new[] { cts.Token.WaitHandle, ForceLoopIteration },
                        TimeSpan.FromMilliseconds(50));

            //WaitHandle.WaitAny(new WaitHandle[] { mre, token.WaitHandle },
            //                       new TimeSpan(0, 0, 20));


        }
        private int disconnectIndex;

        private async Task DoWork(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (IsConnect)
                    {
                        foreach (Controller controller in controllers)
                        {
                            if (!this.sdk.ReadRealTimeLog())
                            {
                                //LogHelperv2.SaveErrorLog("ReadRealTimeLog: ERROR: " + this.sdk.GetLastError());
                                disconnectIndex++;
                            }
                            else
                            {
                                disconnectIndex = 0;
                                string datetime = string.Empty;
                                string userId = string.Empty;
                                string cardNum = string.Empty;
                                string doorNumber = string.Empty;
                                string realtimeEventCode = string.Empty;
                                string status = string.Empty;
                                string verify = string.Empty;

                                const int UnregisterCard = 27;
                                while (this.sdk.GetRealTimeLog(out datetime, out userId, out cardNum, out doorNumber, out realtimeEventCode, out status, out verify))
                                {
                                    string cardNumber = "";
                                    if (verify == "1")
                                    {
                                        cardNumber = userId;
                                        //LogHelperv2.SaveErrorLog("CardEVent: " + verify);
                                    }
                                    else
                                    {
                                        if (verify == "6")
                                        {
                                            if (cardNum == "" || cardNum != "0")
                                            {
                                                cardNumber = cardNum;
                                                //LogHelperv2.SaveErrorLog("CardEVent: " + verify);
                                            }
                                            else
                                            {
                                                cardNumber = userId;
                                                // LogHelperv2.SaveErrorLog("CardEVent: " + verify);
                                            }
                                        }
                                        else
                                        {
                                            if (verify == "4" || verify == "11")
                                            {
                                                cardNumber = cardNum;
                                                //LogHelperv2.SaveErrorLog("CardEVent: " + verify);
                                            }
                                            else
                                            {
                                                cardNumber = cardNum;
                                                //SystemUI.SaveLogFile("CardEVent: " + verify);
                                            }
                                        }
                                        long num = 0L;
                                    }
                                    //SystemUI.SaveLogFile("CardEVent: " + cardNumber);
                                    ShowCardEvent(controller, 0, cardNumber, int.Parse(doorNumber), DateTime.Parse(datetime));
                                }
                            }
                            if (this.disconnectIndex >= 7)
                            {
                                //SystemUI.SaveLogFile("Recoonect: ");
                                IsConnect = false;
                                this.disconnectIndex = 0;
                                Reconnect();
                            }
                            await Task.Delay(100);
                        }
                    }
                    else
                    {
                        //SystemUI.SaveLogFile("Reconnect: ");
                        Connect(comport, baudrate, communicationtype);
                        await Task.Delay(500);
                    }
                }
            }
            catch
            {
                IsConnect = false;
                Reconnect();
                await Task.Delay(500);
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// Hiển thị thông tin card event  <br/>
        /// <param name="controller"></param>
        /// <param name="userid"></param>
        /// <param name="cardNumber"><paramref name="cardNumber"/> Thông tin mã thẻ của sự kiện. Nếu là thẻ proxi sẽ là mã trước, nếu là thẻ mifare sẽ là mã dec</param>
        /// <param name="doorNo"></param>
        /// <param name="eventType"></param>
        /// <param name="timesec"></param>
        /// </summary>
        private void ShowCardEvent(Controller controller, int userid, string cardNumber, int doorNo, DateTime timesec)
        {
            if (cardNumber == "0" || string.IsNullOrEmpty(cardNumber))
            {
                return;
            }
            //cardNumber = "3213371024";
            CardEventArgs e = new CardEventArgs();
            e.LineID = controller.LineID;
            e.LineCode = controller.LineCode;
            e.ControllerAddress = controller.Address;
            e.CardNumber = "";

            //áp dụng cho xuân cương
            int a = (int)(Convert.ToInt64(cardNumber, 10) / 65536);
            int b = (int)(Convert.ToInt64(cardNumber, 10) - a * 65536);
            e.CardNumber = a + ":" + b;

            e.CardNumber = a.ToString("D3") + b.ToString("D5");

            e.ReaderIndex = doorNo;

            //int t = timesec;
            //int second = timesec % 60;
            //t /= 60;
            //int minute = t % 60;
            //t /= 60;
            //int hour = t % 24;
            //t /= 24;
            //int day = t % 31 + 1;
            //t /= 31;
            //int month = t % 12 + 1;
            //t /= 12;
            //int year = t + 2000;
            e.Date = DateTime.Now.ToString("yyyy/MM/dd");// year.ToString("0000") + "/" + month.ToString("00") + "/" + day.ToString("00");
            e.Time = DateTime.Now.ToString("HH/mm/ss"); //hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");

            sdk.DeleteUserGeneralLog(userid);
            CardEvent?.Invoke(this, e);
        }

        private void ShowInputEvent(Controller controller, int doorNo)
        {
            InputEventArgs ie = new InputEventArgs();

            ie.LineID = controller.LineID;
            ie.LineCode = controller.LineCode;
            ie.ControllerAddress = controller.Address;
            // YYMMDD
            ie.EventDate = DateTime.Now.ToString("yyyy/MM/dd");
            // HHMMSS
            ie.EventTime = DateTime.Now.ToString("HH:mm:ss");
            ie.IsOpenDoor = false;
            ie.Inputport = doorNo + "";
            if (ie.Inputport == "1")
            {
                ie.EventType = "ExitB1";
            }
            else if (ie.Inputport == "2")
            {
                ie.EventType = "MSGA";
                //ie.EventType = "ExitB2";
            }
            else if (ie.Inputport == "3")
            {
                ie.EventType = "MSGA";
            }
            else if (ie.Inputport == "4")
            {
                ie.EventType = "MSGB";
            }
            ie.EventType = "MSGA";
            InputEvent?.Invoke(this, ie);
        }
        #endregion: End Event

        #region: Timer

        public void StartTimer()
        {
            timer.Interval = 1000;// 1 minute
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        //bool isTaskrunning = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (count >= 10)
            {

                this.isbusy = false;
                count = 0;

            }
            count++;
        }
        #endregion: End Timer

        public bool Reboot()
        {
            return sdk.RestartDevice();
        }
        public async Task OpenDOor()
        {
            sdk.RemoteNormalOpenDoor(1);
            await Task.Delay(100);
            sdk.RemoteNormalCloseDoor(1);
        }


        public enum EM_EventType
        {
            NORMAL_PUNCH_OPEN = 0,
            PUNCH_DURING_NORMAL_OPEN_TIMEZONE = 1,
            FIRST_CARD_NORMAL_OPEN_PUNCH_CARD = 2,
            MULTI_CARD_OPEN_PUNCH_CARD = 3,
            EMERGENCY_PASSWORD_OPEN = 4,
            OPEN_DURING_NORMAL_OPEN_TIMEZONE = 5,
            LINKAGE_EVENT_TRIGGERED = 6,
            CANCEL_ALARM = 7,
            REMOTE_OPENING = 8,
            REMOTE_CLOSING = 9,
            DISABLE_INTRADAY_NORMAL_OPEN_TIMEZONE = 10,
            ENABLE_INTRADAY_NORMAL_OPEN_TIMEZONE = 11,
            OPEN_AUXILIARY_OUTPUT = 12,
            CLOSE_AUXILIARY_OUTPUT = 13,
            PRESS_FINGERPRINT_OPEN = 14,
            MULTI_CARD_OPEN_PRESS_FINGER = 15,
            PRESS_FINGER_DURING_NORMAL_OPEN_TIMEZONE = 16,
            CARD_PLUS_FINGERPRINT_OPEN = 17,
            FIRST_CARD_NORMAL_OPEN_PRESS_FINGER = 18,
            FIRST_CARD_NORMAL_OPEN_CARD_AND_FINGER = 19,
            TOO_SHORT_PUNCH_INTERVAL = 20,
            DOOR_INACTIVE_TIMEZONE_PUNCH_CARD = 21,
            ILLEGAL_TIMEZONE = 22,
            ACCESS_DENINED = 23,
            ANTI_PASSBACK = 24,
            INTERLOCK = 25,
            MULTICARD_AUTHENTICATION_PUNCH_CARD = 26,
            UNREGISTER_CARD = 27,
            OPENING_TIMEOUT = 28,
            CARD_EXPIRED = 29,
            PASSWORD_ERROR = 30,
            TOO_SHORT_PRESS_FINGERPRINT_INTERVAL = 31,
            MULTICARD_AUTHENTICATION_PRESSFINGERPRINT = 32,
            FINGERPRINT_EXPIRED = 33,
            UNREGISTER_FINGERRINT = 34,
            DOOR_INACTIVE_TIMEZONE_PRESS_FINGERPRINT = 35,
            DOOR_INACTIVE_TIMEZONE_EXITBUTTON = 36,
            FAIL_CLOSE_DURING_NORMAL_OPENTIMEZONE = 37,
            DURESS_PASSWORD_OPNE = 101,
            OPENED_ACCIDENTALLY = 102,
            DURESS_FINGERPRINT_OPEN = 103,
            DOOR_OPENED_CORRECTLY = 200,
            DOOR_CLOSED_CORRECTLY = 201,
            EXIT_BUTTON_OPEN = 202,
            MULTI_CARD_OPEN_CARD_PLUS_FINGER = 203,
            NOMAL_OPNE_TIMEZONE_OVER = 204,
            REMOTE_NORMAL_OPENING = 205,
            DEVICE_START = 206,
            AUXILIARY_INPUT_DISCONNECTED = 220,
            AUXILIARY_INPUT_SHORTED = 221,
            ACTUALLY_OBTAIN_DOOR_AND_ALARM_STATUS = 225
        }
        private static Dictionary<EM_EventType, string> EventType_str = new Dictionary<EM_EventType, string>()
        {
            {EM_EventType.NORMAL_PUNCH_OPEN," Normal Punch Open" },
            {EM_EventType.PUNCH_DURING_NORMAL_OPEN_TIMEZONE," Punch during Normal Open TimeZone" },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_PUNCH_CARD," First Card Normal Open (PunchCard)" },
            {EM_EventType.MULTI_CARD_OPEN_PUNCH_CARD,"  Multi-Card Open (Punching Card)" },
            {EM_EventType.EMERGENCY_PASSWORD_OPEN,"Emergency Password Open " },
            {EM_EventType.OPEN_DURING_NORMAL_OPEN_TIMEZONE,"Open during Normal Open TimeZone" },
            {EM_EventType.LINKAGE_EVENT_TRIGGERED," Linkage Event Triggered" },
            {EM_EventType.CANCEL_ALARM,"Cancel Alarm " },
            {EM_EventType.REMOTE_OPENING,"Remote Opening " },
            {EM_EventType.REMOTE_CLOSING,"Remote Closing " },
            {EM_EventType.DISABLE_INTRADAY_NORMAL_OPEN_TIMEZONE,"Disable Intraday Normal Open TimeZone " },
            {EM_EventType.ENABLE_INTRADAY_NORMAL_OPEN_TIMEZONE," Enable Intraday Normal Open TimeZone" },
            {EM_EventType.OPEN_AUXILIARY_OUTPUT,"Open Auxiliary Output " },
            {EM_EventType.CLOSE_AUXILIARY_OUTPUT,"Close Auxiliary Output " },
            {EM_EventType.PRESS_FINGERPRINT_OPEN," Press Fingerprint Open" },
            {EM_EventType.MULTI_CARD_OPEN_PRESS_FINGER,"lti-Card Open (Press Fingerprint) " },
            {EM_EventType.PRESS_FINGER_DURING_NORMAL_OPEN_TIMEZONE,"Press Fingerprint during NormalOpen Time Zone " },
            {EM_EventType.CARD_PLUS_FINGERPRINT_OPEN,"Card plus Fingerprint Open " },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_PRESS_FINGER," First Card Normal Open (PressFingerprint)" },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_CARD_AND_FINGER," First Card Normal Open (Card plusFingerprint)" },
            {EM_EventType.TOO_SHORT_PUNCH_INTERVAL,"Too Short Punch Interval " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_PUNCH_CARD," Door Inactive Time Zone (PunchCard)" },
            {EM_EventType.ILLEGAL_TIMEZONE," Illegal Time Zone" },
            {EM_EventType.ACCESS_DENINED,"Access Denied " },
            {EM_EventType.ANTI_PASSBACK,"Anti-Passback " },
            {EM_EventType.INTERLOCK,"Interlock " },
            {EM_EventType.MULTICARD_AUTHENTICATION_PUNCH_CARD,"Multi-Card Authentication (PunchingCard) " },
            {EM_EventType.UNREGISTER_CARD," Unregistered Card" },
            {EM_EventType.OPENING_TIMEOUT," Opening Timeout:" },
            {EM_EventType.CARD_EXPIRED,"Card Expired " },
            {EM_EventType.PASSWORD_ERROR,"Password Error " },
            {EM_EventType.TOO_SHORT_PRESS_FINGERPRINT_INTERVAL," Too Short Fingerprint PressingInterval" },
            {EM_EventType.MULTICARD_AUTHENTICATION_PRESSFINGERPRINT," Multi-Card Authentication (PressFingerprint)" },
            {EM_EventType.FINGERPRINT_EXPIRED,"Fingerprint Expired " },
            {EM_EventType.UNREGISTER_FINGERRINT,"Unregistered Fingerprint " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_PRESS_FINGERPRINT,"Door Inactive Time Zone (PressFingerprint) " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_EXITBUTTON,"Door Inactive Time Zone (ExitButton) " },
            {EM_EventType.FAIL_CLOSE_DURING_NORMAL_OPENTIMEZONE,"Failed to Close during Normal OpenTime Zone " },
            {EM_EventType.DURESS_PASSWORD_OPNE,"Duress Password Open " },
            {EM_EventType.OPENED_ACCIDENTALLY,"Opened Accidentally " },
            {EM_EventType.DURESS_FINGERPRINT_OPEN,"Duress Fingerprint Open " },
            {EM_EventType.DOOR_OPENED_CORRECTLY,"Door Opened Correctly " },
            {EM_EventType.DOOR_CLOSED_CORRECTLY,"Door Closed Correctly " },
            {EM_EventType.EXIT_BUTTON_OPEN,"Exit button Open " },
            {EM_EventType.MULTI_CARD_OPEN_CARD_PLUS_FINGER,"Multi-Card Open (Card plusFingerprint) " },
            {EM_EventType.NOMAL_OPNE_TIMEZONE_OVER,"Normal Open Time Zone Over " },
            {EM_EventType.REMOTE_NORMAL_OPENING,"Remote Normal Opening " },
            {EM_EventType.DEVICE_START,"Device Start " },
            {EM_EventType.AUXILIARY_INPUT_DISCONNECTED,"Auxiliary Input Disconnected" },

            {EM_EventType.AUXILIARY_INPUT_SHORTED,"Auxiliary Input Shorted " },
            {EM_EventType.ACTUALLY_OBTAIN_DOOR_AND_ALARM_STATUS,"Actually that obtain door statusand alarm status " }
        };
    }
}
