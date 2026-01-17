using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO.Ports;
using Futech.Tools;

namespace Futech.Objects
{
    public class Barriers
    {
        // Serial
        public SerialPort serialPort = null;

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
            set { isconnect = value; }
        }

        // ComPort
        private string comport = "COM14";
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

        // Ket noi den thiet bi
        public bool Connect(string _ComPort, int _BaudRate, int _CommunicationType)
        {
            try
            {
                ComPort = _ComPort;
                BaudRate = _BaudRate;
                CommunicationType = _CommunicationType;

                if (CommunicationType == 0)
                {
                    serialPort = new SerialPort();
                    serialPort.PortName = ComPort;
                    serialPort.BaudRate = BaudRate;
                    serialPort.ReadBufferSize = 4096;
                    serialPort.WriteBufferSize = 4096;
                    serialPort.DataBits = 8;
                    serialPort.ReadTimeout = -1;
                    serialPort.WriteTimeout = -1;
                    serialPort.DtrEnable = true;
                    serialPort.RtsEnable = true;
                    serialPort.InitializeLifetimeService();
                    serialPort.Open();
                    IsConnect = true;

                    return true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ket noi den thiet bi\n" + ex.Message);
            }
            IsConnect = false;
            return false;
        }

        // Ngung ket noi den thiet bi
        public bool Disconnect()
        {
            try
            {
                if (CommunicationType == 0)
                {
                    if (serialPort.IsOpen)
                        serialPort.Close();
                    return true;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ngung ket noi den thiet bi\n" + ex.Message);
            }
            return false;
        }


        // Thuc hien lenh den thiet bi (pc <-> host)
        // Response Function 06 - ACK Acknowledge, 15 - NAK Negative Acknowledge, 12 - EVN Event Message    
        private string ExecuteCommand(byte[] buffer, string command, ref string viewraw, ref string[] message)
        {
            try
            {
                viewraw = "";
                message = null;
                if (CommunicationType == 0)
                {
                    if (serialPort.IsOpen)
                    {
                        // pc to host
                        serialPort.Write(buffer, 0, buffer.Length);

                        Thread.Sleep(100);

                        // host to pc
                        buffer = new byte[serialPort.BytesToRead];
                        viewraw = "";
                        message = null;
                        int index = 0;
                        while (serialPort.BytesToRead > 0)
                        {
                            buffer[index] = (Byte)serialPort.ReadByte();
                            if (viewraw == "")
                                viewraw = ByteUI.DecimalToBase(buffer[index], 16);
                            else
                                viewraw = viewraw + " " + ByteUI.DecimalToBase(buffer[index], 16);
                            index = index + 1;
                        }
                        if (viewraw != "" && viewraw.Contains(" "))
                        {
                            message = viewraw.Split(' ');

                            return System.Text.Encoding.UTF8.GetString(buffer);
                        }
                    }
                    else
                        Thread.Sleep(1000);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thuc hien lenh den thiet bi (pc <-> host)\n" + ex.Message);
            }
            return "";
        }

        // Open
        public bool Open(int delay, string provider)
        {
            try
            {
                switch (provider)
                {
                    case "":
                    case "Futech":
                        {
                            // Request
                            // AA 55 30 0A 00 00 32 6C  :  
                            // 0xAA, 0x55, 0x30, 0x0A  , 0x00, 0x00, 0x32  , 0xCheckSum + 1.
                            // Answer
                            // 0xAA, 0x55, 0x30, 0x0A  , 0x00, 0x00, 0x32  , 0xCheckSum + 1.

                            string viewraw = "";
                            string[] message = null;
                            byte[] bytes = new byte[] { 0xAA, 0x55, 0x30, 0x0A, 0x00, 0x00, 0x32, 0x6C };

                            // Thuc hien lenh den thiet bi (pc <-> host)
                            if (ExecuteCommand(bytes, "", ref viewraw, ref message) != "" && message[0] == "AA" && message[1] == "55" && message[7] == "6C")
                            {
                                // Mo barie thanh cong
                                return true;
                            }

                            break;
                        }

                    case "Came":
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Mo barie khong thanh cong
            return false;
        }

        // Stop
        public bool Stop()
        {
            return false;
        }

        // Close
        public bool Close()
        {
            return false;
        }

    }
}
