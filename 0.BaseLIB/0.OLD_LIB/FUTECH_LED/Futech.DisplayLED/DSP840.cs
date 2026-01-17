using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using Futech.Tools;

namespace Futech.DisplayLED
{
    internal class DSP840 : IDisplayLED
    {
        private SerialPort port = new SerialPort();
        private int _RowNumber = 1;
        public int RowNumber { get => _RowNumber; set => _RowNumber = value; }
        Socket UdpServer;

        //UdpClient udp;
        private IPEndPoint ipEpBroadcast;

        private int communicationtype = 0;
        public int CommunicationType
        {
            get { return communicationtype; }
            set { communicationtype = value; }
        }

        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }

        private Timer timer = new Timer();

        public DSP840()
        {
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
        }

        public bool Connect(string comport, int baudrate)
        {
            try
            {
                if (comport.Contains("COM") == true)
                {
                    communicationtype = 0;
                    port.PortName = comport;
                    port.BaudRate = baudrate;
                    port.ReadBufferSize = 4096;
                    port.WriteBufferSize = 4096;
                    port.DataBits = 8;
                    port.ReadTimeout = -1;
                    port.WriteTimeout = -1;
                    port.DtrEnable = true;
                    port.RtsEnable = true;
                    port.InitializeLifetimeService();

                    port.Open();
                    return true;
                }
                else
                {
                    CommunicationType = 1;
                    UdpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    ipEpBroadcast = new IPEndPoint(IPAddress.Parse(comport), baudrate);

                    UdpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        int count = 0;
        public void SendToLED(string dtimein, string dtimout, string plate, string money, int cardtype, string cardstate)
        {
            try
            {
                //create buffer
                if (CommunicationType == 0)
                {
                    Delete();
                    if (dtimout != "")
                    {
                        byte[] buff = ByteToSend(DateTime.Parse(dtimein), DateTime.Parse(dtimout), money);
                        if (port.IsOpen)
                        {
                            port.Write(buff, 0, buff.Length);
                            timer.Start();
                        }
                    }
                }
                else if (CommunicationType == 1)
                {

                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logType: LogHelper.EmLogType.ERROR,
                  objectLogType: LogHelper.EmObjectLogType.System,
                  obj: ex);
            }
        }
        public byte[] ByteToSend(DateTime dtin, DateTime dtout, string money)
        {
            try
            {
                byte[] temp = new byte[40];
                //temp[0]-temp[3] VAO:
                temp[0] = Convert.ToByte(ByteUI.Asc("V"));
                temp[1] = Convert.ToByte(ByteUI.Asc("A"));
                temp[2] = Convert.ToByte(ByteUI.Asc("O"));
                temp[3] = Convert.ToByte(ByteUI.Asc("-"));
                //thoi gian vao
                string sdtin = dtin.ToString("HH:mm dd/MM/yyyy");
                for (int i = 0; i < sdtin.Length; i++)
                {
                    temp[4 + i] = Convert.ToByte(ByteUI.Asc(sdtin[i].ToString()));
                }
                temp[20] = Convert.ToByte(ByteUI.Asc("R"));
                temp[21] = Convert.ToByte(ByteUI.Asc("A"));
                temp[22] = Convert.ToByte(ByteUI.Asc(" "));
                temp[23] = Convert.ToByte(ByteUI.Asc("-"));
                //thoi gian ra
                string sdtout = dtout.ToString("HH:mm");
                for (int i = 0; i < sdtout.Length; i++)
                {
                    temp[24 + i] = Convert.ToByte(ByteUI.Asc(sdtout[i].ToString()));
                }
                temp[29] = Convert.ToByte(ByteUI.Asc(" "));
                //so tien
                //money = string.Format("{0:0,0}", Convert.ToInt32(money));
                if (money.Length < 10)
                {
                    int n = 40 - money.Length;
                    for (int i = 30; i < n; i++)
                    {
                        temp[i] = Convert.ToByte(ByteUI.Asc(" "));
                    }
                    for (int i = 0; i < money.Length; i++)
                    {
                        temp[n + i] = Convert.ToByte(ByteUI.Asc(money[i].ToString()));
                    }
                }
                else
                {
                    money = money.Substring(0, 10);
                    for (int i = 0; i < 10; i++)
                    {
                        temp[30 + i] = Convert.ToByte(ByteUI.Asc(money[i].ToString()));
                    }
                }
                return temp;
            }
            catch
            {
                return null;
            }

        }

        private void Delete()
        {
            try
            {
                byte[] buff = new byte[] { 0x0C };
                if (port.IsOpen)
                {
                    port.Write(buff, 0, buff.Length);
                }
            }
            catch
            { }

        }
        public void SetParkingSlot(int slotNo, byte arrow, byte color)
        {

        }
        private void RunMessage()
        {
            try
            {
                Delete();
                byte[] buff = new byte[] { 0x04, 0x01, 0x44, 0x31, 0x31, 0x17 };
                if (port.IsOpen)
                {
                    port.Write(buff, 0, buff.Length);
                }
            }
            catch
            { }
        }
        public void Close()
        {
            try
            {
                if (communicationtype == 0)
                    port.Close();
                else
                {
                    UdpServer.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logType: LogHelper.EmLogType.ERROR,
                   objectLogType: LogHelper.EmObjectLogType.System,
                   obj: ex);
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count > 20)
            {
                count = 0;
                RunMessage();
                timer.Stop();
            }
        }


        //public byte[] ByteToSend(string address, byte ctrl, string str)
        //{
        //    try
        //    {
        //        byte[] temp = new byte[40];
        //        if (str.Length > 20)
        //            str = str.Substring(0, 20);
        //        int n = (20 - str.Length) / 2;
        //        //space
        //        for (int i = 0; i < n; i++)
        //        {
        //            temp[i] = Convert.ToByte(ByteUI.Asc(" "));
        //        }
        //        //string
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            temp[n + i] = Convert.ToByte(ByteUI.Asc(str[i]));
        //        }
        //        //space
        //        for (int i = n + str.Length; i < 20; i++)
        //        {
        //            temp[i] = Convert.ToByte(ByteUI.Asc(" "));
        //        }
        //        //20 byte hang duoi
        //        for (int i = 20; i < 40; i++)
        //        {
        //            temp[i] = Convert.ToByte(ByteUI.Asc(" "));
        //        }
        //        return temp;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
