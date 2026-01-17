using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using Kztek.Tool;
using System.Text;
using System.IO.Ports;
using static Kztek.Object.CommunicationType;
using Kztek.Object;
using static Kztek.Object.InputTupe;

namespace iParkingv5.Controller.SC200Devices
{
    public class SC200 : IController
    {
        public CardDispenserStatus cardDispenserStatus { get; set; }
        public Dictionary<int, EmCardDispenserStatus> DispensersStatus { get; set; } = new Dictionary<int, EmCardDispenserStatus>();

        public event CancelEventHandler? CancelEvent;
        SerialPort serialPort;
        private const byte NO_EVENT = 0x30;
        private const byte EVENT_READER1 = 0x31;
        private const byte EVENT_READER2 = 0x32;
        private const byte EVENT_READER3 = 0x33;
        private const byte EVENT_READER4 = 0x34;
        private const byte BUTTON_EXIT1 = 0x35;
        private const byte BUTTON_EXIT2 = 0x36;
        private const byte BUTTON_MSG1 = 0x37;
        private const byte BUTTON_MSG2 = 0x38;
        // Format card
        private const byte _3Bytes = 0x33;
        private const byte _4Bytes = 0x34;
        private const byte _7Bytes = 0x37;


        const string TimeFormat = "yyyyMMddHHmmss";
        public Bdk ControllerInfo { get; set; }
        public bool IsBusy = false;

        public CancellationTokenSource? cts { get; set; }
        public ManualResetEvent? ForceLoopIteration { get; set; }
        #region Event
        public event CardEventHandler? CardEvent;
        public event FingerEventHandler? FingerEvent;
        public event ControllerErrorEventHandler? ErrorEvent;
        public event InputEventHandler? InputEvent;
        public event ConnectStatusChangeEventHandler? ConnectStatusChangeEvent;
        public event DeviceInfoChangeEventHandler? DeviceInfoChangeEvent;
        private Thread thread = null;
        private ManualResetEvent stopEvent = null;
        public void PollingStart()
        {
            //if (thread == null)
            //{
            //    CheckConnect();
            //    // create events
            //    stopEvent = new ManualResetEvent(false);
            //    // start thread
            //    thread = new Thread(new ThreadStart(WorkerThread));
            //    thread.Start();
            //}
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
        public void PollingStop()
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

        // Signal thread to stop work
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
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
            stopEvent.Close();
            stopEvent = null;
        }
        private byte add1 = 0x30, add2 = 0x30, add3 = 0x31;
        private int address = 0;
        public int Address
        {
            get { return address; }
            set
            {
                address = value;
                add1 = Convert.ToByte(ByteUI.Asc(address.ToString("000").Substring(0, 1)));
                add2 = Convert.ToByte(ByteUI.Asc(address.ToString("000").Substring(1, 1)));
                add3 = Convert.ToByte(ByteUI.Asc(address.ToString("000").Substring(2, 1)));
            }
        }

        public async void WorkerThread()
        {
            //string card = "41743867";
            //while (!stopEvent.WaitOne(0, true))
            //{
            //    try
            //    {
            //        string viewraw = "";
            //        string[] message = null;

            //        if (this.ControllerInfo.communicationType == 1)
            //        {
            //            if (serialPort.IsOpen)
            //            {
            //                this.Address = 1;
            //                byte[] bytes = new byte[] { 0x01, add1, add2, add3, 0x31,
            //                        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x00,
            //                        0x0D };
            //                bytes[34] = ByteUI.CRC(bytes, 35, 1);

            //                string ret_Execute = ExecuteCommand(bytes, "", ref viewraw, ref message);

            //                // Thuc hien lenh den thiet bi (pc <-> host)
            //                if (ret_Execute != "" && message.Length == 36 && message[0] == "01" && message[35] == "0D")
            //                {
            //                    this.ControllerInfo.isConnect = true; // Thiet bi online
            //                                                          // State = 0x31 - co su kien, 0x32 - khong co su kien
            //                    if (message[5] == "31" && CardEvent != null)
            //                    {
            //                        // Hien thi su kien
            //                        string CardNumber = "";
            //                        int i = 0;
            //                        //for (i = 6; i <= 13; i++)
            //                        //    e.CardNumber += ByteUI.AscToStr(message[i]);

            //                        //Sua dung chung cho ca the mifare
            //                        for (i = 6; i <= 13; i++)
            //                        {
            //                            CardNumber += Convert.ToChar(ByteUI.BaseToDecimal(message[i], 16)).ToString();
            //                        }

            //                        CallCardEvent(ControllerInfo, CardNumber);


            //                        // 01 30 30 31 32 30   30 30 30 30 30 30   30 30 30 30 30 30   30 30 30 30 30 30   30 30 30 30 30 30 30 30 30 30 30   0D
            //                        bytes = new byte[] { 0x01, add1, add2, add3, 0x32,
            //                                0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                                0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                                0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x00,
            //                                0x0D };
            //                        bytes[34] = ByteUI.CRC(bytes, 35, 1);

            //                        // Thuc hien lenh den thiet bi (pc <-> host)
            //                        // State = 0x31 - thanh cong, 0x32 - khong thanh cong
            //                        if (ExecuteCommand(bytes, "", ref viewraw, ref message) != "" && message[5] == "31")
            //                        {
            //                            // Xoa du lieu thanh cong
            //                            //CardEvent(this, e);
            //                        }
            //                    }
            //                    else if (message[5] == "30" && InputEvent != null)
            //                    {
            //                        //InputEventArgs _ie = new InputEventArgs();
            //                        //_ie.EventDate = DateTime.Now.ToString("yyyy/MM/dd");
            //                        //_ie.EventTime = DateTime.Now.ToString("HH:mm:ss");
            //                        //if (message[27] == "31")
            //                        //    _ie.Inputport = "1";
            //                        //else
            //                        //    _ie.Inputport = "2";
            //                        ////_ie.BoardIndex=
            //                        //bytes = new byte[] { 0x01, add1, add2, add3, 0x32,
            //                        //        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                        //        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
            //                        //        0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x00,
            //                        //        0x0D };
            //                        //bytes[34] = ByteUI.CRC(bytes, 35, 1);
            //                        //if (ExecuteCommand(bytes, "", ref viewraw, ref message) != "" && message[5] == "31")
            //                        //{
            //                        //    // Xoa du lieu thanh cong
            //                        //    InputEvent(this, _ie);
            //                        //}
            //                    }
            //                    else
            //                    {
            //                    }

            //                }
            //                else
            //                    this.ControllerInfo.isConnect = false; // Thiet bi offline     
            //            }
            //            else
            //            {
            //                //LogHelper.Logger_CONTROLLER_Infor($"Hàm workerThread chưa Open", LogHelper.SaveLogFolder);
            //                Application.DoEvents();
            //                Thread.Sleep(100);
            //            }
            //        }
            //        else
            //        {
            //        }

            //        GC.Collect();
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
        private void CallInputEvent(Bdk controller, byte inputIndex)
        {
            InputEventArgs ie = new InputEventArgs
            {
                DeviceId = controller.Id
            };
            switch (inputIndex)
            {
                case BUTTON_EXIT1:
                    ie.InputIndex = 1;
                    ie.InputType = EmInputType.Exit;
                    break;
                case BUTTON_EXIT2:
                    ie.InputIndex = 2;
                    ie.InputType = InputTupe.EmInputType.Exit;
                    break;
                case BUTTON_MSG1:
                    ie.InputIndex = 3;
                    ie.InputType = InputTupe.EmInputType.Loop;
                    break;
                case BUTTON_MSG2:
                    ie.InputType = InputTupe.EmInputType.Loop;
                    ie.InputIndex = 4;
                    break;
                default:
                    ie.InputIndex = 3;
                    ie.InputType = InputTupe.EmInputType.Loop;
                    break;
            }
            
            OnInputEvent(ie);
        }
        private void CallCardEvent(Bdk controller, string cardNumber, int readerIndex)
        {
            CardEventArgs e = new CardEventArgs
            {
                DeviceId = controller.Id,
                AllCardFormats = new List<string>(),
            };
            string cardNumberHEX = cardNumber;
            e.PreferCard = cardNumberHEX;

            //if (!string.IsNullOrEmpty(cardNumberHEX))
            //{
            //    e.AllCardFormats.Add(cardNumberHEX);

            //    if (cardNumberHEX.Length == 6)
            //    {
            //        string maTruocToiGian = long.Parse(cardNumberHEX, NumberStyles.HexNumber).ToString();
            //        string maTruocFull = Convert.ToInt64(cardNumberHEX, 16).ToString("0000000000");

            //        string maSauFormat1 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString("000") +
            //                              int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("00000");

            //        string maSauFormat2 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString("000") + ":" +
            //                              int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("00000");

            //        string maSauFormat3 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString() + ":" +
            //          int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("");

            //        e.PreferCard = maSauFormat3;

            //        e.AllCardFormats.Add(maTruocToiGian);
            //        if (maTruocToiGian != maTruocFull)
            //        {
            //            e.AllCardFormats.Add(maTruocFull);
            //        }
            //        e.AllCardFormats.Add(maSauFormat1);
            //        e.AllCardFormats.Add(maSauFormat2);
            //    }
            //    else
            //    {
            //        string maInt = Convert.ToInt64(cardNumberHEX, 16).ToString();
            //        e.PreferCard = maInt;
            //        e.AllCardFormats.Add(maInt);
            //    }
            //}
            //string str_readerIndex = map.ContainsKey("reader") ? map["reader"] : "";
            e.ReaderIndex = readerIndex;
            OnCardEvent(e);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {
                Thread.Sleep(100);
                while (serialPort.BytesToRead >= 40)
                {
                    byte[] response = new byte[40];
                    serialPort.Read(response, 0, 40);
                    StringBuilder rawData = new StringBuilder();
                    for (int i = 0; i < response.Length; i++)
                    {
                        rawData.Append(ByteUI.DecimalToBase(response[i], 16));
                    }

                    //foreach (Controller controller in controllers)
                    //{
                    this.ControllerInfo.IsConnect = true; // Thiet bi online

                    byte ctrl_Str = response[4];
                    byte subCtrl_Str = response[5];

                    switch (ctrl_Str)
                    {
                        //Sự kiện quẹt thẻ
                        case EVENT_READER1:
                        case EVENT_READER2:
                        case EVENT_READER3:
                        case EVENT_READER4:
                            int readerIndex = 1;
                            switch (ctrl_Str)
                            {
                                case EVENT_READER1:
                                    readerIndex = 1;
                                    break;
                                case EVENT_READER2:
                                    readerIndex = 2;
                                    break;
                                case EVENT_READER3:
                                    readerIndex = 3;
                                    break;
                                case EVENT_READER4:
                                    readerIndex = 4;
                                    break;
                                default:
                                    readerIndex = 1;
                                    break;
                            }
                            string cardNumber = "";
                            int _b1 = response[6];
                            int _b2 = response[7];
                            int _b3 = response[8];
                            int _b4 = response[9];
                            int _b5 = response[10];
                            int _b6 = response[11];
                            int _b7 = response[12];
                            int _b8 = response[13];
                            int _b9 = response[14];
                            int _b10 = response[15];
                            int _b11 = response[16];
                            int _b12 = response[17];
                            int _b13 = response[18];
                            int _b14 = response[19];
                            string str_Byte1 = Convert.ToChar(_b1).ToString() + Convert.ToChar(_b2).ToString();
                            string str_Byte2 = Convert.ToChar(_b3).ToString() + Convert.ToChar(_b4).ToString();
                            string str_Byte3 = Convert.ToChar(_b5).ToString() + Convert.ToChar(_b6).ToString();
                            string str_Byte4 = Convert.ToChar(_b7).ToString() + Convert.ToChar(_b8).ToString();
                            string str_Byte5 = Convert.ToChar(_b9).ToString() + Convert.ToChar(_b10).ToString();
                            string str_Byte6 = Convert.ToChar(_b11).ToString() + Convert.ToChar(_b12).ToString();
                            string str_Byte7 = Convert.ToChar(_b13).ToString() + Convert.ToChar(_b14).ToString();
                            if (subCtrl_Str == _3Bytes)
                            {
                                cardNumber = str_Byte1 + str_Byte2 + str_Byte3;
                            }
                            else if (subCtrl_Str == _4Bytes)
                            {
                                if (str_Byte1 == "00")
                                {
                                    cardNumber =/* str_Byte1 + */str_Byte2 + str_Byte3 + str_Byte4;
                                }
                                else
                                {
                                    cardNumber = str_Byte1 + str_Byte2 + str_Byte3 + str_Byte4;
                                }
                            }
                            else if (subCtrl_Str == _7Bytes)
                            {
                                cardNumber = str_Byte1 + str_Byte2 + str_Byte3 + str_Byte5 + str_Byte6;
                            }
                            else
                            {
                                cardNumber = str_Byte1 + str_Byte2 + str_Byte3;
                            }

                            CallCardEvent(ControllerInfo, cardNumber, readerIndex);
                            break;
                        //Sự kiện inout
                        case 0x35:
                        case 0x36:
                        case 0x37:
                        case 0x38:
                        case 0x39:
                            //int inputIndex = ctrl_Str;
                            CallInputEvent(ControllerInfo, ctrl_Str);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }


        }
        private string ExecuteCommand(byte[] buffer, string command, ref string viewraw, ref string[] message)
        {
            try
            {
                viewraw = "";
                message = null;
                //if (this.ControllerInfo.communicationType == 0)
                {
                    if (serialPort.IsOpen)
                    {
                        // pc to host
                        serialPort.Write(buffer, 0, buffer.Length);

                        Thread.Sleep(100);

                    }
                    else
                        Thread.Sleep(100);
                }
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thuc hien lenh den thiet bi (pc <-> host)\n" + ex.Message);
            }
            return "";
        }
        public async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            string viewraw = "";
            string[] message = null;
            // Lay su kien
            byte ctrl = 0x33; // Xoa sự kiện

            if (relayIndex == 1) ctrl = 0x33;
            else
                ctrl = 0x34;


            add3 = 0x31;
            byte subCtrl = 0x30;
            byte[] bytes = new byte[] {
                                            0x01,
                                            add1, add2, add3,
                                            ctrl,
                                            subCtrl,
                                            0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                                            0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                                            0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30,
                                            0x00,
                                            0x0D
                };
            byte Sum = ByteUI.CRC(bytes, 39, 1);
            bytes[38] = Sum;
            // Thuc hien lenh den thiet bi (pc <-> host)
            ExecuteCommand(bytes, "", ref viewraw, ref message);
            return true;
        }
        #endregion End Event

        #region: CONNECT
        public async Task<bool> TestConnectionAsync()
        {
            if (!CommunicationType.IS_TCP((EmCommunicationType)(this.ControllerInfo.CommunicationType)))
            {
                try
                {
                    if (this.ControllerInfo.CommunicationType == 1)
                    {
                        serialPort = new SerialPort();
                        serialPort.PortName = this.ControllerInfo.Comport;
                        serialPort.BaudRate = int.Parse(this.ControllerInfo.Baudrate);
                        serialPort.ReadBufferSize = 4096;
                        serialPort.WriteBufferSize = 4096;
                        serialPort.DataBits = 8;
                        serialPort.ReadTimeout = -1;
                        serialPort.WriteTimeout = -1;
                        serialPort.DtrEnable = true;
                        serialPort.RtsEnable = true;
                        serialPort.DataReceived += SerialPort_DataReceived;
                        serialPort.Open();
                        this.ControllerInfo.IsConnect = true;
                        return true;
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }
                this.ControllerInfo.IsConnect = false;
                return false;
            }
            return false;
        }
        public async Task<bool> ConnectAsync()
        {
            return await this.TestConnectionAsync();
        }
        public async Task<bool> DisconnectAsync()
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion: END CONNECT

        #region DATE - TIME
        public async Task<DateTime> GetDateTime()
        {
            return DateTime.MinValue;
        }
        public async Task<bool> SetDateTime(DateTime time)
        {
            return false;
        }
        public async Task<bool> SyncDateTime()
        {
            return await SetDateTime(DateTime.Now);
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
            return GetBaudrate(this.ControllerInfo.Baudrate);
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
            return true;
        }
        public async Task<bool> RestartDevice()
        {
            return true;
        }
        public async Task<bool> ResetDefault()
        {
            return true;
        }
        #endregion End System

        protected void OnCardEvent(CardEventArgs e)
        {
            CardEvent?.Invoke(this, e);
        }
        protected void OnConnectStatusChangeEvent(ConnectStatusCHangeEventArgs e)
        {
            ConnectStatusChangeEvent?.Invoke(this, e);
        }
        protected void OnErrorEvent(ControllerErrorEventArgs e)
        {
            ErrorEvent?.Invoke(this, e);
        }
        protected void OnInputEvent(InputEventArgs e)
        {
            InputEvent?.Invoke(this, e);
        }
        public int GetBaudrate(string GetDateTimeCMD)
        {
            int baudrate = 0;
            if (!string.IsNullOrEmpty(this.ControllerInfo.Baudrate))
            {
                try
                {
                    baudrate = int.Parse(this.ControllerInfo.Baudrate);
                }
                catch (Exception ex)
                {
                    string errorMessage = $@"Controller {this.ControllerInfo.Comport} Got Baudrate Error: " + ex.Message;
                    ErrorEvent?.Invoke(this, new ControllerErrorEventArgs()
                    {
                        ErrorString = errorMessage,
                        ErrorFunc = "GetDateTime",
                        CMD = GetDateTimeCMD
                    });
                    throw new Exception(errorMessage);
                }
            }
            return baudrate;
        }
        public static Dictionary<string, string> GetEventContent(string[] datas)
        {
            Dictionary<string, string> output = new();
            foreach (string data in datas)
            {
                if (data.Contains("="))
                {
                    string[] subData = data.Split('=');
                    output.Add(subData[0].ToLower().Trim(), subData[1].Trim());
                }
            }
            return output;
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

        public Task<bool> CollectCard()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RejectCard()
        {
            throw new NotImplementedException();
        }
    }
}
