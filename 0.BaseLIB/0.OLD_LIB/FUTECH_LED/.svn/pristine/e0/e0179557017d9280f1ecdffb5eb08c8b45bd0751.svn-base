using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using Futech.Tools;

namespace Futech.Objects
{
    public class PFP3700
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
        public PFP3700()
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

        private int downloadTime = 300;
        public int DownloadTime
        {
            set { downloadTime = value; }
        }

        // Controller Address
        private byte address = 0x00;
        public int Address
        {
            set { address = (byte)value; }
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
                MessageBox.Show("Error while opening COM Port: " + ex.Message);
                return false;
            }
        }

        // Close ComPort
        public bool CommPortClose()
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // request to reader
        public void Request(byte[] buffer)
        {
            try
            {
                serialPort.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // answer from reader
        public string[] Answer()
        {
            try
            {
                string temp = null;
                while (serialPort.BytesToRead > 0)
                {
                    if (temp == null)
                        temp = ByteUntils.DecimalToBase(serialPort.ReadByte(), 16);
                    else
                        temp = temp + "|" + ByteUntils.DecimalToBase(serialPort.ReadByte(), 16);
                }
                if (temp != null)
                {
                    string[] message = temp.Split('|');
                    return message;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        // Start
        public void PollingStart(ControllerCollection controllers)
        {
            if (thread == null)
            {
                this.controllers = controllers;

                foreach (Controller controller in this.controllers)
                {
                    controller.isFirstPackage = true;
                }

                // create events
                stopEvent = new ManualResetEvent(false);

                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
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

        // Worker thread
        public void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        foreach (Futech.Objects.Controller controller in controllers)
                        {
                            byte address = (byte)controller.Address;

                            if (controller.isFirstPackage)
                            {
                                Thread.Sleep(downloadTime * 1000);
                                controller.eventCount =  EventCount(address);
                                controller.nextPackage = 0;
                                controller.isFirstPackage = false;
                            }

                            byte nextPackageLow = (byte)((controller.nextPackage + 1) & 0xff);
                            byte nextPackagedHi = (byte)((controller.nextPackage + 1) >> 8);

                            if (controller.eventCount > controller.nextPackage)
                            {
                                // get controller
                                byte[] bytes = new byte[] { 0xF6, address, 0x83, 0x00, nextPackagedHi, nextPackageLow, XOR(new byte[] { address, 0x83, 0x00, nextPackagedHi, nextPackageLow }), 0xF6 };
                                // request to reader
                                Request(bytes);
                                // wait to reply from reader
                                Thread.Sleep(delaytime);
                                Thread.Sleep(1000);
                                string[] message = Answer();
                                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                                {
                                    string[] temp = new string[message.Length - 11];
                                    Array.Copy(message, 9, temp, 0, temp.Length);
                                    int index = 0;
                                    while (index < temp.Length)
                                    {
                                        if (temp[index + 1] == "00" && temp[index + 2] == "00")
                                        {
                                            break;
                                        }

                                        CardEventArgs e = new CardEventArgs();
                                        e.LineID = controller.LineID;
                                        e.ControllerAddress = controller.Address;
                                        e.ReaderIndex = ByteUntils.BaseToDecimal(temp[index + 11], 16);
                                        int id = ByteUntils.BaseToDecimal(temp[index + 3], 16) * 256 + ByteUntils.BaseToDecimal(temp[index + 4],  16);
                                        e.CardNumber = id.ToString("0000");
                                        e.Date = "20" + ByteUntils.BaseToDecimal(temp[index + 6], 16).ToString("00") + "/" + ByteUntils.BaseToDecimal(temp[index + 7], 16).ToString("00") + "/" + ByteUntils.BaseToDecimal(temp[index + 8], 16).ToString("00");
                                        e.Time = ByteUntils.BaseToDecimal(temp[index + 9], 16).ToString("00") + ":" + ByteUntils.BaseToDecimal(temp[index + 10], 16).ToString("00");
                                        e.EventStatus = "Access Granted";
                                        CardEvent(this, e);
                                        index += 12;
                                    }
                                }
                                controller.nextPackage += 25;
                            }
                            else
                            {
                                if (controller.eventCount > 0)
                                {
                                    ClearEvent(address);
                                    controller.eventCount = 0;
                                    controller.nextPackage = 0;
                                }

                                controller.isFirstPackage = true;
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
        
        // Test connection
        public bool TestConnection()
        {

            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x80, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x80}), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                    return true;
            }
            return false;
        }

        // Upload DateTime
        public string UploadDateTime()
        {

            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x87, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x87 }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    string yyyy = "20" + ByteUntils.BaseToDecimal(message[10], 16);
                    string MM = ByteUntils.BaseToDecimal(message[11], 16).ToString("00");
                    string dd = ByteUntils.BaseToDecimal(message[12], 16).ToString("00");
                    string HH = ByteUntils.BaseToDecimal(message[13], 16).ToString("00");
                    string mm = ByteUntils.BaseToDecimal(message[14], 16).ToString("00");
                    string ss = ByteUntils.BaseToDecimal(message[15], 16).ToString("00");

                    return yyyy + "/" + MM + "/" + dd + " " + HH + ":" + mm + ":" + ss;
                }
            }
            return null;
        }
        
        // Download clock
        public bool DownloadDateTime(DateTime datetime)
        {
            if (serialPort.IsOpen)
            {
                byte wd = (byte)DateUI.GetDayOfWeekInNumber2(datetime);
                byte yy = Convert.ToByte(datetime.Year - 2000);
                byte MM = (byte)datetime.Month;
                byte dd = (byte)datetime.Day;
                byte HH = (byte)datetime.Hour;
                byte mm = (byte)datetime.Minute;
                byte ss = (byte)datetime.Second;
                byte[] bytes = new byte[] { 0xF6, address, 0x93, 0x00, 0x07, 0x00, XOR(new byte[] { address, 0x93, 0x07 }), 0xF6, 0xF6, wd, yy, MM, dd, HH, mm, ss, XOR(new byte[] { wd, yy, MM, dd, HH, mm, ss }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                    return true;
            }
            return false;
        }

        // Upload Finger
        public string UploadFinger(int id, int fingerID)
        {
            if (serialPort.IsOpen)
            {
                byte idLow = (byte)(id & 0xff);
                byte idHi = (byte)(id >> 8);
                byte[] bytes = new byte[] { 0xF6, address, 0x81, idHi, idLow, (byte)fingerID, XOR(new byte[] { address, 0x81, idHi, idLow, (byte)fingerID }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    string[] temp = new string[message.Length - 8];
                    Array.Copy(message, 8, temp, 0, temp.Length);
                    return String.Join(",", temp);
                }
            }
            return "";
        }
       
        // Download Finger
        public bool DownloadFinger(string fingers, int fingerID)
        {
            if (serialPort.IsOpen)
            {
                string[] s = fingers.Split(',');
                byte[] finger = new byte[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    finger[i] = (byte)ByteUntils.BaseToDecimal(s[i], 16);
                }

                byte[] temp = new byte[] { 0xF6, address, 0x91, 0x01, 0x55, (byte)fingerID, XOR(new byte[] { address, 0x91, 0x01, 0x55, (byte)fingerID }), 0xF6 };
                byte[] bytes = new byte[finger.Length + temp.Length];

                Array.Copy(temp, 0, bytes, 0, temp.Length);
                Array.Copy(finger, 0, bytes, temp.Length, finger.Length);

                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return true;
                }
            }
            return false;
        }

        // Delete Finger
        public bool DeleteFinger(int id, int fingerID)
        {
            if (serialPort.IsOpen)
            {
                byte idLow = (byte)(id & 0xff);
                byte idHi = (byte)(id >> 8);
                byte[] bytes = new byte[] { 0xF6, address, 0x82, idHi, idLow, (byte)fingerID, XOR(new byte[] { address, 0x82, idHi, idLow, (byte)fingerID }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return true;
                }
            }
            return false;
        }

        // Get number of event
        public int EventCount()
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x85, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x85 }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return ByteUntils.BaseToDecimal(message[16], 16) * 256 + ByteUntils.BaseToDecimal(message[17], 16);
                }
            }
            return 0;
        }

        // Get number of event
        private int EventCount(byte address)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x85, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x85 }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return ByteUntils.BaseToDecimal(message[16], 16) * 256 + ByteUntils.BaseToDecimal(message[17], 16);
                }
            }
            return 0;
        }

        // Clear Event
        private bool ClearEvent()
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x84, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x84 }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return true;
                }
            }
            return true;
        }

        // Clear Event
        private bool ClearEvent(byte address)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0xF6, address, 0x84, 0x00, 0x00, 0x00, XOR(new byte[] { address, 0x84 }), 0xF6 };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && (byte)ByteUntils.BaseToDecimal(message[1], 16) == address)
                {
                    return true;
                }
            }
            return true;
        }

        // PFP 3700
        private byte XOR(byte[] buffer)
        {
            byte temp = 0x00;
            for (int i = 0; i < buffer.Length; i++)
            {
                temp ^= buffer[i];
            }
            return temp;
        }
    }
}
