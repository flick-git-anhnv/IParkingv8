using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Futech.Tools;

namespace Futech.Objects
{
    public class NStar
    {
        // Serial Port
        public SerialPort serialPort = null;

        // card event 
        public event CardEventHandler CardEvent;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;

        // all controller in line
        private ControllerCollection controllers = null;

        // Constructor
        public NStar()
        {
 
        }

        // Line ID
        private int lineID = 0;
        public int LineID
        {
            set { lineID = value; }
        }

        // delay time to receive data from reader
        private int delaytime = 300;
        public int DelayTime
        {
            get { return delaytime; }
            set { delaytime = value; }
        }

        // Controller Address
        private int address = 0;
        public int Address
        {
            set { address = value; }
        }

        // Open ComPort
        public bool CommPortOpen(string portName, int baudRate)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = portName;
                serialPort.BaudRate = baudRate;
                serialPort.ReadBufferSize = 4096;
                serialPort.WriteBufferSize = 4096;
                serialPort.DataBits = 8;
                serialPort.ReadTimeout = -1;
                serialPort.WriteTimeout = -1;
                serialPort.DtrEnable = true;
                serialPort.RtsEnable = true;
                serialPort.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while opening COM port: " + ex.Message);
                return false;
            }
        }

        // Close ComPort
        public bool CommPortClose()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Request to reader
        private void SendCommand(string command)
        {
            try
            {
                byte[] buffer = new byte[command.Length + 2];
                char[] cm = command.ToCharArray();
                buffer[0] = 0x20;
                buffer[buffer.Length - 1] = 0x0D;
                for (int i = 1; i < buffer.Length - 1; i++)
                {
                    buffer[i] = Convert.ToByte(cm[i - 1]);
                }
                serialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while send a command to COM port: " + ex.Message);
            }
        }

        // Get reply from reader
        public string GetReply()
        {
            try
            {
                string reply = "";
                while (serialPort.BytesToRead > 0)
                {
                    reply += Convert.ToChar(serialPort.ReadByte());
                }
                return reply;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        // Get reply from reader
        public string GetData()
        {
            string reply = "";
            while (serialPort.BytesToRead > 0)
            {
                char c = Convert.ToChar(serialPort.ReadByte());
                if (c == Convert.ToChar(0x03))
                    return reply + Convert.ToChar(0x03);
                else
                    reply += c;
            }
            return reply;
        }

        // Start
        public void PollingStart(ControllerCollection controllers)
        {
            if (thread == null)
            {
                this.controllers = controllers;
                // create events
                stopEvent = new ManualResetEvent(false);

                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
                // UnBuffer all controller
                UnBufferAllController();
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
            // Buffer all controller
            BufferAllController();
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
                thread.Join(0);

                Free();
            }
        }

        // Free resources
        private void Free()
        {
            thread = null;

            // release events
            stopEvent.Close();
            stopEvent = null;
        }

        // Stop
        public void PollingStop()
        {
            if (this.Running)
            {
                thread.Abort();
                WaitForStop();
            }
        }

        public void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    Thread.Sleep(delaytime);
                    if (serialPort.IsOpen)
                    {
                        string temp = GetData();
                        if (temp != "" && temp != null)
                        {
                            if (temp.Contains(Convert.ToChar(0x02).ToString()))
                            {
                                temp = temp.Substring(temp.IndexOf(Convert.ToChar(0x02)), temp.IndexOf(Convert.ToChar(0x03)) - temp.IndexOf(Convert.ToChar(0x02)) + 1);
                                if (temp.Length == 44 || temp.Length == 41)
                                {
                                    temp = temp.Substring(1, temp.Length - 2);// Bỏ các ký tự chặn đầu và chặn cuối
                                    temp = temp.Remove(6, 2);
                                    string[] answer = temp.Split(new string[] { "X", " ", "\n", "\r", "\a" }, System.StringSplitOptions.RemoveEmptyEntries);
                                    //
                                    CardEventArgs e = new CardEventArgs();
                                    e.LineID = lineID;

                                    e.ControllerAddress = int.Parse(answer[2].Substring(3));

                                    try
                                    {
                                        Controller _controller = controllers.GetControllerByAddress(e.ControllerAddress);
                                        if (_controller != null)
                                            e.LineCode = _controller.LineCode;
                                    }
                                    catch
                                    { }

                                    if (temp.Contains("X"))
                                        e.ReaderIndex = 2;
                                    else
                                        e.ReaderIndex = 1;
                                    e.CardNumber = answer[3];
                                    e.Date = Convert.ToDateTime(DateTime.Now.Year + "-" + answer[0]).ToString("yyyy/MM/dd");
                                    e.Time = answer[1];
                                    if (answer.Length == 5)
                                    {
                                        if (answer[4].ToUpper() == "NF")
                                            e.EventStatus = "Access Denied";
                                        else if (answer[4].ToUpper() == "TZ")
                                            e.EventStatus = "Timezone Error";
                                        else if (answer[4].ToUpper() == "AP")
                                            e.EventStatus = "Anti-passback Error";
                                        else
                                            e.EventStatus = "Access Granted";

                                    }
                                    else
                                        e.EventStatus = "Access Granted";
                                    CardEvent(this, e);
                                }
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        // Download DateTime
        public bool DownloadDateTime(DateTime datetime)
        {
            if (serialPort.IsOpen)
            {
                string cm = "D=" + address.ToString() + " " + datetime.ToString("M/d") + " " + DateUI.GetDayOfWeekInNumber1(datetime).ToString() + " " + datetime.ToString("yyyy");
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                {
                    SendCommand("T=" + address.ToString() + " " + datetime.ToString("HH:mm") + ":00");
                    Thread.Sleep(delaytime);
                    if (GetReply().Contains("OK"))
                        return true;
                }
                return false;
            }
            else
                return false;
        }

        // Lock
        public bool Lock(int relayNo)
        {
            if (serialPort.IsOpen)
            {
                string cm = "O=" + address.ToString() + " O " + relayNo.ToString() + " D";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // UnLock
        public bool UnLock(int relayNo)
        {
            if (serialPort.IsOpen)
            {
                string cm = "O=" + address.ToString() + " O " + relayNo.ToString() + " E";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // Pulse
        public bool Pulse(int relayNo)
        {
            if (serialPort.IsOpen)
            {
                string cm = "O=" + address.ToString() + " O " + relayNo.ToString() + " P";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // Shunt
        public bool Shunt()
        {
            if (serialPort.IsOpen)
            {
                string cm = "O=" + address.ToString() + " I 2 E";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // UnShunt
        public bool UnShunt()
        {
            if (serialPort.IsOpen)
            {
                string cm = "O=" + address.ToString() + " I 2 D";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // Download Configuration
        public bool DownloadConfiguration(bool antiPassback, bool[] cardFormat)
        {
            if (serialPort.IsOpen)
            {
                SendCommand("I=" + address.ToString() + " N");
                Thread.Sleep(delaytime * 10);
                if (GetReply().Contains("OK"))
                {
                    SendCommand("I=" + address.ToString() + " 0");
                    Thread.Sleep(delaytime);
                    if (GetReply().Contains("OK"))
                    {
                        // AntiPassback Mode
                        string cm = antiPassback ? "I=" + address.ToString() + " B M A Z 0" : "I=" + address.ToString() + " B M Z 0";
                        SendCommand(cm);
                        Thread.Sleep(delaytime * 10);
                        if (GetReply().Contains("OK"))
                        {
                            // Card Format
                            if (cardFormat[0])
                            {
                                // CR-1 Wiegand Card Swipe/26 bit-generic
                                cm = "F=" + address.ToString() + " 1 26 S 1 D 1 B1 B2 B3 B4";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            else
                            {
                                cm = "F=" + address.ToString() + " 1 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            if (cardFormat[1])
                            {
                                // NR-1 Magstripe Swipe, NR5/32 bit
                                // PR-1-280 Cotag Proximity/32 bit
                                // HG-1 Hand Geometry/32 bit
                                // 5 Conductor Keypad/32 bit
                                // Sielox Proximity Cards/32 bit
                                cm = "F=" + address.ToString() + " 2 32 S 0 D 0 B1 B2 B3 B4";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            else
                            {
                                cm = "F=" + address.ToString() + " 2 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            if (cardFormat[2])
                            {
                                // HID/34 bit
                                // Sielox Wiegand Cards/34 bit
                                cm = "F=" + address.ToString() + " 3 34 S 1 D 1 B1 B2 B3 B4";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            else
                            {
                                cm = "F=" + address.ToString() + " 3 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            if (cardFormat[3])
                            {
                                // CI-1 Wiegand Card Insert/26 bit
                                cm = "F=" + address.ToString() + " 4 26 I 1 D 1 B1 B2 B3 B4";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            else
                            {
                                cm = "F=" + address.ToString() + " 4 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            if (cardFormat[4])
                            {
                                // Dorado Magstripe Cards/34 bit
                                cm = "F=" + address.ToString() + " 5 34 S 1 D 0 B1 B2 B3 B4";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            else
                            {
                                cm = "F=" + address.ToString() + " 5 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            for (int i = 6; i <= 32; i++)
                            {
                                cm = "F=" + address.ToString() + " " + i.ToString() + " 0 S 0 D 0 B0 B0 B0 B0";
                                SendCommand(cm);
                                Thread.Sleep(delaytime);
                                GetReply();
                            }
                            //
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        // Get status of controller
        public void GetStatus()
        {
            if (serialPort.IsOpen)
            {
                SendCommand("R=" + address.ToString() + " p");
                Thread.Sleep(delaytime);
                GetReply();
                SendCommand("R=" + address.ToString() + " o");
                Thread.Sleep(delaytime);
                GetReply();
            }
        }

        // Buffer
        private void BufferAllController()
        {
            if (serialPort.IsOpen)
            {
                foreach (Controller controller in controllers)
                {
                    string cm = "M=" + Convert.ToInt32(controller.Address).ToString() + " P";
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    //GetReply();
                }
            }
        }

        // UnBuffer
        private void UnBufferAllController()
        {
            if (serialPort.IsOpen)
            {
                foreach (Controller controller in controllers)
                {
                    string cm = "M=" + Convert.ToInt32(controller.Address).ToString() + " R";
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    //GetReply();
                }
            }
        }

        // Download Timezone
        public void DownloadTimezone(Timezone timezone)
        {
            if (serialPort.IsOpen)
            {
                int timezoneID = timezone.ID + 1;
                if (timezoneID == 1 || timezoneID == 2)
                {
                    string cm = "L=" + address.ToString() + " " + timezoneID.ToString() + " " + timezone.Mon + " 1 2 3 4 5 6 7;0";
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                }
                else
                {
                    int linkToEnd = 0;
                    // Monday
                    linkToEnd = 63 - (timezoneID - 3) * 6;
                    string cm = "L=" + address.ToString() + " " + timezoneID.ToString() + " " + timezone.Mon + " 1;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Tuesday
                    linkToEnd -= 1;
                    cm = "L=" + address.ToString() + " " + Convert.ToString(linkToEnd + 1) + " " + timezone.Tue + " 2;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Wednesday
                    linkToEnd -= 1;
                    cm = "L=" + address.ToString() + " " + Convert.ToString(linkToEnd + 1) + " " + timezone.Wed + " 3;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Thursday
                    linkToEnd -= 1;
                    cm = "L=" + address.ToString() + " " + Convert.ToString(linkToEnd + 1) + " " + timezone.Thu + " 4;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Friday
                    linkToEnd -= 1;
                    cm = "L=" + address.ToString() + " " + Convert.ToString(linkToEnd + 1) + " " + timezone.Fri + " 5;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Saturday
                    linkToEnd -= 1;
                    cm = "L=" + address.ToString() + " " + Convert.ToString(linkToEnd + 1) + " " + timezone.Sat + " 6;" + linkToEnd.ToString();
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                    //Sunday
                    cm = "L=" + address.ToString() + " " + linkToEnd.ToString() + " " + timezone.Sun + " 7;0";
                    SendCommand(cm);
                    Thread.Sleep(delaytime);
                    GetReply();
                }
            }
        }

        // Download Card
        public bool DownloadCard(string cardNumber, string password, int timezoneID)
        {
            if (serialPort.IsOpen)
            {
                timezoneID += 1;
                string cm = "C=" + address.ToString() + " " + cardNumber + " " + timezoneID.ToString() + " " + timezoneID.ToString() + " 0 0 1 2";
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // Delete Card
        public bool DeleteCard(string cardNumber, string password)
        {
            if (serialPort.IsOpen)
            {
                string cm = "C=" + address.ToString() + " " + cardNumber;
                SendCommand(cm);
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                    return true;
                return false;
            }
            else
                return false;
        }

        // Download Output
        public bool DownloadOutput(int readerIndex, int relayIndex, int delay)
        {
            if (serialPort.IsOpen)
            {
                SendCommand("A=" + address.ToString() + " " + readerIndex.ToString() + " O " + relayIndex.ToString() + " T 0 0 0");
                Thread.Sleep(delaytime);
                if (GetReply().Contains("OK"))
                {
                    SendCommand("A=" + address.ToString() + " " + readerIndex.ToString() + " D 0");
                    Thread.Sleep(delaytime);
                    if (GetReply().Contains("OK"))
                    {
                        SendCommand("P=" + address.ToString() + " O " + relayIndex.ToString() + " C");
                        Thread.Sleep(delaytime);
                        if (GetReply().Contains("OK"))
                        {
                            SendCommand("P=" + address.ToString() + " O " + relayIndex.ToString() + " I " + ((readerIndex == 1) ? "2" : "4") + " F N");
                            Thread.Sleep(delaytime);
                            if (GetReply().Contains("OK"))
                            {
                                SendCommand("V=" + address.ToString() + " O " + relayIndex.ToString() + " S 0 0 C");
                                Thread.Sleep(delaytime);
                                if (GetReply().Contains("OK"))
                                {
                                    SendCommand("V=" + address.ToString() + " O " + relayIndex.ToString() + " S " + delay + " 0");
                                    Thread.Sleep(delaytime);
                                    if (GetReply().Contains("OK"))
                                    {
                                        return true;
                                    }
                                    else
                                        return false;
                                }
                                else
                                    return false;
                            }
                            else 
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
