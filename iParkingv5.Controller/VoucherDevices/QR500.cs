using iParkingv5.Objects.Events;
using Kztek.Tool;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace iParkingv5.Controller.VoucherDevices
{
    public class QR500 : IVoucherController
    { // Serial
        public SerialPort serialPort = null!;

        // TCP/IP
        private TcpClient tcpclient = null!;

        // CardEvent 
        public event CardEventHandler? CardEvent;
        public event CardEventHandler? VoucherEvent;
        // InputEvent 
        public event InputEventHandler? InputEvent;

        private Thread thread = null!;
        private ManualResetEvent stopEvent = null!;

        Socket UdpServer = default!;
        IPEndPoint ipEpBroadcast = default!;
        #region Property

        // Line ID
        private int lineId;
        public int LineId
        {
            set { lineId = value; }
        }

        // Thoi gian tre lay du lieu
        private int delaytime = 250;
        public int DelayTime
        {
            get { return delaytime; }
            set { delaytime = value; }
        }

        // Communication Type
        private int communicationtype = 0;
        public int CommunicationType
        {
            get { return communicationtype; }
            set { communicationtype = value; }
        }

        private bool isconnect = false;
        public bool IsConnect
        {
            get { return isconnect; }
            set
            {
                if (isconnect != value)
                {
                    isconnect = value;
                }
            }
        }

        // ComPort
        private string comport = "COM4";
        public string ComPort
        {
            get { return comport; }
            set { comport = value; }
        }

        // BaudRate
        private int baudrate = 9600;
        public int BaudRate
        {
            get { return baudrate; }
            set { baudrate = value; }
        }

        // Dia chi thiet bi
        private int address = 0;
        private byte add1 = 0x30, add2 = 0x30, add3 = 0x30;
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
        private bool[] currentStatus = new bool[8];
        #endregion

        // Ket noi den thiet bi
        public bool Connect(string comport, int baudrate, int communicationType)
        {
            try
            {
                if (string.IsNullOrEmpty(comport)) return false;
                this.communicationtype = communicationType;
                this.comport = comport;
                this.baudrate = baudrate;
                if (this.serialPort == null)
                {
                    this.serialPort = new SerialPort(ComPort, BaudRate)
                    {
                        ReadTimeout = 500,
                        WriteTimeout = 500
                    };
                }

                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }

                serialPort.Open();
                Thread.Sleep(500);
                IsConnect = true;
                if (IsConnect)
                {
                    SettingUp();
                }
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Connect", ex));
                return false;
            }
            return false;
        }
        private void SettingUp()
        {
            byte[] command = { 0xAA, 0x01, 0x97, 0x01, 0x00, 0x00, 0x40, 0x74 };
            serialPort.Write(command, 0, command.Length);

            byte[] command1 = { 0xAA, 0xFF, 0x97, 0x01, 0x00, 0x22, 0xE9, 0xB9 };
            serialPort.Write(command1, 0, command.Length);
            Thread.Sleep(200);
        }

        // Ngung ket noi den thiet bi
        public bool Disconnect()
        {
            try
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "DisConnect"));

                if (serialPort.IsOpen)
                    serialPort.Close();
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "DisConnect", ex));
                return false;
            }
        }

        // Start
        public void PollingStart()
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Start"));
            if (thread == null)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Start", "New"));
                // create events
                stopEvent = new ManualResetEvent(false);

                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
            }
            else
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Start", "VALID"));
            }
        }

        // is Running
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

        // Free resources
        private void Free()
        {
            thread = null!;

            // release events
            stopEvent.Close();
            stopEvent = null!;
        }

        // Stop
        public void PollingStop()
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Stop"));
            if (this.Running)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Stop", "Running"));
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
            else
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog("", comport, "Polling Stop", "Not Running"));
            }
        }

        string _olddispenserstate = "";
        // Worker thread
        public async void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    //if (!this.IsConnect)
                    //{
                    //    Connect(this.comport, this.baudrate, 0);
                    //}
                    SendGetEventCmd();
                    GetEvent();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Lỗi " + ex.Message);
                    this.isconnect = false;
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceName = this.ComPort,
                        Cmd = "PollingEvent",
                        Response = Newtonsoft.Json.JsonConvert.SerializeObject(ex)
                    });
                    //return;
                }
                finally
                {
                    await Task.Delay(300);
                }
            }
        }
        public void SendGetEventCmd()
        {
            try
            {
                if (serialPort == null)
                {
                    return;
                }
                

                // host to pc
                var buffer = new byte[serialPort.BytesToRead];
                int index = 0;
                while (serialPort.BytesToRead > 0)
                {
                    buffer[index] = (Byte)serialPort.ReadByte();
                    index = index + 1;
                }

                if (buffer.Length == 15)
                {
                    var a = BitConverter.ToString(buffer).Replace("-", "");
                    var cardNumber = a.Substring(18, a.Length - 18 - 4);
                    CardEventArgs ce = new();
                    ce.PreferCard = reverseHex(cardNumber);
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceName = this.ComPort,
                        Cmd = "GetEvent",
                        Response = "REC CARD" + ce.PreferCard,
                    });
                    CardEvent?.Invoke(this, ce);
                }
                else if (buffer.Length > 15)
                {
                    List<byte> displayVoucher = new List<byte>();
                    for (int i = 9; i < buffer.Length - 2; i++)
                    {
                        displayVoucher.Add(buffer[i]);
                    }
                    var a = BitConverter.ToString(buffer);
                    string voucherData = Encoding.ASCII.GetString(buffer.ToArray());
                    CardEventArgs ce = new();
                    ce.PreferCard = voucherData;
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceName = this.ComPort,
                        Cmd = "GetEvent",
                        Response = "REC VOUCHER" + ce.PreferCard,
                    });
                    VoucherEvent?.Invoke(this, ce);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }

        }
        static string reverseHex(string baseHex)
        {
            if (string.IsNullOrEmpty(baseHex))
            {
                return "";
            }
            while (baseHex.Length % 2 != 0)
            {
                baseHex = "0" + baseHex;
            }
            int count = baseHex.Length / 2;
            string output = "";
            for (int i = count - 1; i >= 0; i--)
            {
                output += baseHex.Substring(i * 2, 2);
            }
            return output;
        }
        private void GetEvent()
        {
            try
            {
                //SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                //{
                //    DeviceName = this.ComPort,
                //    Cmd = "GetEvent",
                //    Response = "REC START",
                //});
                if (serialPort == null)
                {
                    return;
                }
                if (this.serialPort.BytesToRead < 8)
                {
                    return;
                }

                byte[] buffer = new byte[serialPort.BytesToRead];
                serialPort.Read(buffer, 0, buffer.Length);

                string strData = Encoding.ASCII.GetString(buffer);
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceName = this.ComPort,
                    Cmd = "GetEvent",
                    Response = "REC " + strData,
                });
                CardEventArgs ce = new();
                ce.PreferCard = strData;
                CardEvent?.Invoke(this, ce);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceName = this.ComPort,
                    Cmd = "GetEvent",
                    Response = Newtonsoft.Json.JsonConvert.SerializeObject(ex)
                });
            }
        }
    }
}