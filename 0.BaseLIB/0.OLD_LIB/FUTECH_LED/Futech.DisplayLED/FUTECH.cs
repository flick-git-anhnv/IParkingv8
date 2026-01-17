using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO.Ports;
using Futech.Tools;

namespace Futech.DisplayLED
{
    internal class FUTECH:IDisplayLED
    {
        private int _RowNumber = 1;

        public int RowNumber { get => _RowNumber; set => _RowNumber = value; }
        private System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();
        Socket UdpServer;
        //UdpClient udp;
        IPEndPoint ipEpBroadcast;

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
        public FUTECH()
        {
            timer.Enabled = false;
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
        }
        public void SetParkingSlot(int slotNo, byte arrow, byte color)
        {

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
        void Timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            count++;
            if (count > 20)
            {
                count = 0;
                RunMessage();
                timer.Stop();
            }
        }

        public void SendToLED(string dtimein, string dtimout, string plate, string money, int cardtype, string cardstate)
        {
            try
            {
                if (money == "0" || money == "")
                    return;
                int m = int.Parse(money.Replace(",", ""));
                if (m == 0)
                    return;
                //create buffer
                if (CommunicationType == 0)
                {
                   

                    byte[] temp = new byte[20];

                    temp[0] = 0x01;//header
                    temp[1] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[0]));//add1
                    temp[2] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[1]));//add2
                    temp[3] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[2]));//add3
                    temp[4] = 0x31;

                    money = int.Parse(money.Replace(",","")).ToString("00000000");

                    for (int i = 0; i < 8; i++)
                        temp[i + 5] = Convert.ToByte(ByteUI.Asc(money[i]));
                    for (int i = 0; i < 7; i++)
                        temp[13 + i] = 0x30;
                    temp[19] = ByteUI.CRC(temp, 20, 1);

                    if (port.IsOpen)
                    {
                        port.Write(temp, 0, temp.Length);
                    }
                   

                }
                else if (CommunicationType == 1)
                {
                    byte[] temp = new byte[20];

                    temp[0] = 0x01;//header
                    temp[1] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[0]));//add1
                    temp[2] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[1]));//add2
                    temp[3] = Convert.ToByte(ByteUI.Asc(address.ToString("000")[2]));//add3
                    temp[4] = 0x31;

                    money = int.Parse(money.Replace(",", "")).ToString("00000000");

                    for (int i = 0; i < 8; i++)
                        temp[i + 5] = Convert.ToByte(ByteUI.Asc(money[i]));
                    for (int i = 0; i < 7; i++)
                        temp[13 + i] = 0x30;
                    temp[19] = ByteUI.CRC(temp, 20, 1);

                    if (port.IsOpen)
                    {
                        port.Write(temp, 0, temp.Length);
                    }

                    UdpServer.SendTo(temp, ipEpBroadcast);
                    UdpServer.SendTo(temp, ipEpBroadcast);
                    UdpServer.SendTo(temp, ipEpBroadcast);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(logType: LogHelper.EmLogType.ERROR,
                   objectLogType: LogHelper.EmObjectLogType.System,
                   obj: ex);
            }
        }

        public byte[] ByteToSend(string address, byte ctrl, DateTime dtin, DateTime dtout, string money)
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
        public byte[] ByteToSend(string address, byte ctrl, string str)
        {
            try
            {
                byte[] temp = new byte[40];
                if (str.Length > 20)
                    str = str.Substring(0, 20);
                int n = (20 - str.Length) / 2;
                //space
                for (int i = 0; i < n; i++)
                {
                    temp[i] = Convert.ToByte(ByteUI.Asc(" "));
                }
                //string
                for (int i = 0; i < str.Length; i++)
                {
                    temp[n + i] = Convert.ToByte(ByteUI.Asc(str[i]));
                }
                //space
                for (int i = n + str.Length; i < 20; i++)
                {
                    temp[i] = Convert.ToByte(ByteUI.Asc(" "));
                }
                //20 byte hang duoi
                for (int i = 20; i < 40; i++)
                {
                    temp[i] = Convert.ToByte(ByteUI.Asc(" "));
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


    }
}
