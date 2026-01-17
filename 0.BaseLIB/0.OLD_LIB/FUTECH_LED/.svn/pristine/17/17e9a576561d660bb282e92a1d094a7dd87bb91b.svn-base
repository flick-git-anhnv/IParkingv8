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
    public class PP3760
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
        public PP3760()
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
        private string address = "00";
        public int Address
        {
            set { address = value.ToString("00"); }
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
                            // get controller
                            byte[] bytes = new byte[] { 0x05, 0x44, 0x4F, GetHighByte(controller.Address.ToString("00")), GetLowByte(controller.Address.ToString("00")) };
                            // request to reader
                            Request(bytes);
                            // wait to reply from reader
                            Thread.Sleep(delaytime);
                            string[] message = Answer();
                            if (message != null)
                            {
                                if (message.Length == 39 && CardEvent != null)
                                {
                                    CardEventArgs e = new CardEventArgs();
                                    e.LineID = controller.LineID;
                                    e.ControllerAddress = controller.Address;
                                    e.CardNumber = GetValue(message[4]) + GetValue(message[5]) + GetValue(message[6]) + GetValue(message[7]) + GetValue(message[8]) + GetValue(message[9]);
                                    e.Date = "20" + GetValue(message[12]) + GetValue(message[13]) + "/" + GetValue(message[14]) + GetValue(message[15]) + "/" + GetValue(message[16]) + GetValue(message[17]);
                                    e.Time = GetValue(message[20]) + GetValue(message[21]) + ":" + GetValue(message[22]) + GetValue(message[23]);
                                    e.ReaderIndex = 1;

                                    if (GetValue(message[10]) == "8")
                                    {
                                        if (GetValue(message[11]) == "2")
                                            e.EventStatus = "Access Denied";
                                        else if (GetValue(message[11]) == "3")
                                            e.EventStatus = "Timezone Error";
                                        else if (GetValue(message[11]) == "6")
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

        // Open Door
        public bool OpenDoor()
        {

            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x06, 0x44, 0x4F, GetHighByte(address), GetLowByte(address) };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download clock
        public bool DownloadDate(DateTime datetime)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x07, 0x59, 0x53, GetHighByte(address), GetLowByte(address), GetHighByte(datetime.ToString("yy")), GetLowByte(datetime.ToString("yy")), GetHighByte(datetime.ToString("MM")), GetLowByte(datetime.ToString("MM")), GetHighByte(datetime.ToString("dd")), GetLowByte(datetime.ToString("dd")), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download clock
        public bool DownloadTime(DateTime datetime)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x07, 0x57, 0x53, GetHighByte(address), GetLowByte(address), GetByte(DateUI.GetDayOfWeekInNumber1(datetime).ToString()), 0x30, GetHighByte(datetime.ToString("HH")), GetLowByte(datetime.ToString("HH")), GetHighByte(datetime.ToString("mm")), GetLowByte(datetime.ToString("mm")), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Init
        public bool Init()
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x31, 0x37, 0x30, 0x31, 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Init1
        public bool OnlineMode(bool isOnline)
        {
            if (serialPort.IsOpen)
            {
                string n = "0";
                if (isOnline)
                    n = "3";
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x38, 0x39, 0x30, GetByte(n), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // System Initial
        public bool SystemInitial(bool isOnline)
        {
            if (serialPort.IsOpen)
            {
                string n = "8";
                if (isOnline)
                    n = "7";
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x30, GetByte(n), 0x35, 0x30, 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Card Compare Mode
        public bool DownloadCardCompareMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x31, 0x35, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Personal Mixed Mode
        public bool DownloadDisplayPersonalNameMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x39, 0x36, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Fobidden Repeat Reading
        public bool DownloadForbiddenRepeatReading(int minutes)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x38, 0x33, 0x30, GetByte(minutes.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Error Code Mode
        public bool DownloadErrorCodeMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x38, 0x34, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Timezone Comparing Mode
        public bool DownloadTimezoneComparingMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x31, 0x36, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download AntiPassback Mode
        public bool DownloadAntiPassbackMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x39, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Door Monitoring Period
        public bool DownloadDoorMonitoringPeriod(int seconds)
        {
            if (serialPort.IsOpen)
            {
                seconds = seconds * 2;
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x32, 0x33, GetHighByte(seconds.ToString()), GetLowByte(seconds.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Automatic Operation Mode
        public bool DownloadAutomaticOperationMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x38, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Reader Mode
        public bool DownloadReaderMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] byte1 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x32, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                byte[] byte2 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x34, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                //
                if (mode == 0) // Card Only
                {
                    byte1 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x32, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                    byte2 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x34, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                }
                else if (mode == 1) // Card and PIN
                {
                    byte1 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x33, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                    byte2 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x34, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                }
                else if (mode == 2) // Card Or PIN
                {
                    byte1 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x34, 0x30, 0x31, 0x47, 0x0D, 0x0A };
                    byte2 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x33, 0x32, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                }
                // request to reader
                Request(byte1);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    Thread.Sleep(1000);
                    // request to reader
                    Request(byte2);
                    // wait to reply from reader
                    Thread.Sleep(delaytime);
                    if (serialPort.BytesToRead > 0)
                    {
                        while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Door Monitoring Mode
        public bool DownloadDoorMonitoringMode(int mode)
        {
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x31, 0x31, 0x30, GetByte(mode.ToString()), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Door Release Time
        public bool DownloadDoorReleaseTime(int seconds)
        {
            if (serialPort.IsOpen)
            {
                seconds = seconds * 2;
                byte[] bytes = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x32, 0x31, GetHighByte(seconds.ToString("00")), GetLowByte(seconds.ToString("00")), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Timezone
        public bool DownloadTimezone(Timezone timezone)
        {
            if (serialPort.IsOpen)
            {
                int dayofweek = 0;
                int start = 0;
                int stop = 0;
                if (timezone.Sun != "00:00-00:00")
                {
                    dayofweek += 1;
                    if (DateUI.GetTimeNumber(timezone.Sun.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Sun.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Sun.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Sun.Substring(6, 5));
                }
                if (timezone.Mon != "00:00-00:00")
                {
                    dayofweek += 2;
                    if (DateUI.GetTimeNumber(timezone.Mon.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Mon.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Mon.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Mon.Substring(6, 5));
                }
                if (timezone.Tue != "00:00-00:00")
                {
                    dayofweek += 4;
                    if (DateUI.GetTimeNumber(timezone.Tue.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Tue.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Tue.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Tue.Substring(6, 5));
                }
                if (timezone.Wed != "00:00-00:00")
                {
                    dayofweek += 8;
                    if (DateUI.GetTimeNumber(timezone.Wed.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Wed.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Wed.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Wed.Substring(6, 5));
                }
                if (timezone.Thu != "00:00-00:00")
                {
                    dayofweek += 16;
                    if (DateUI.GetTimeNumber(timezone.Thu.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Thu.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Thu.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Thu.Substring(6, 5));
                }
                if (timezone.Fri != "00:00-00:00")
                {
                    dayofweek += 32;
                    if (DateUI.GetTimeNumber(timezone.Fri.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Fri.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Fri.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Fri.Substring(6, 5));
                }
                if (timezone.Sat != "00:00-00:00")
                {
                    dayofweek += 64;
                    if (DateUI.GetTimeNumber(timezone.Sat.Substring(0, 5)) < start)
                        start = DateUI.GetTimeNumber(timezone.Sat.Substring(0, 5));
                    if (DateUI.GetTimeNumber(timezone.Sat.Substring(6, 5)) > stop)
                        stop = DateUI.GetTimeNumber(timezone.Sat.Substring(6, 5));
                }

                string DayOfWeek = ByteUntils.DecimalToBase(dayofweek, 16);
                string HH1 = DateUI.GetTimeString(start).Substring(0, 2);
                string MM1 = DateUI.GetTimeString(start).Substring(3, 2);
                string HH2 = DateUI.GetTimeString(stop).Substring(0, 2);
                string MM2 = DateUI.GetTimeString(stop).Substring(3, 2);
                byte[] bytes = new byte[] { 0x12, 0x5A, 0x4D, GetHighByte(address), GetLowByte(address), 0x30, 0x30, 0x30, GetByte(Convert.ToString(timezone.ID + 1)), GetHighByte(HH1), GetLowByte(HH1), GetHighByte(MM1), GetLowByte(MM1), GetHighByte(HH2), GetLowByte(HH2), GetHighByte(MM2), GetLowByte(MM2), GetHighByte(DayOfWeek), GetLowByte(DayOfWeek), 0x31, 0x31, 0x30, 0x30, 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Download Card
        public bool DownloadCard(string cardNumber, string password, int timezoneID)
        {
            if (serialPort.IsOpen)
            {
                string card1 = cardNumber.Substring(0, 1);
                string card2 = cardNumber.Substring(1, 1);
                string card3 = cardNumber.Substring(2, 1);
                string card4 = cardNumber.Substring(3, 1);
                string card5 = cardNumber.Substring(4, 1);
                string card6 = cardNumber.Substring(5, 1);
                //
                string pass1 = password.Substring(0, 1);
                string pass2 = password.Substring(1, 1);
                string pass3 = password.Substring(2, 1);
                string pass4 = password.Substring(3, 1);
                //
                timezoneID += 1;
                timezoneID = (int)Math.Pow(2, timezoneID - 1);
                string temp = ByteUntils.DecimalToBase(timezoneID, 16);
                byte[] bytes = new byte[] { 0x08, 0x50, 0x4D, GetHighByte(address), GetLowByte(address), GetByte(card3), GetByte(card4), GetByte(card5), GetByte(card6), GetHighByte(temp), GetLowByte(temp), 0x31, 0x30, GetByte(pass1), GetByte(pass2), GetByte(pass3), GetByte(pass4), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Delete Card
        public bool DeleteCard(string cardNumber, string password)
        {
            if (serialPort.IsOpen)
            {
                string card1 = cardNumber.Substring(0, 1);
                string card2 = cardNumber.Substring(1, 1);
                string card3 = cardNumber.Substring(2, 1);
                string card4 = cardNumber.Substring(3, 1);
                string card5 = cardNumber.Substring(4, 1);
                string card6 = cardNumber.Substring(5, 1);
                //
                string pass1 = password.Substring(0, 1);
                string pass2 = password.Substring(1, 1);
                string pass3 = password.Substring(2, 1);
                string pass4 = password.Substring(3, 1);
                //
                byte[] bytes = new byte[] { 0x08, 0x50, 0x4D, GetHighByte(address), GetLowByte(address), GetByte(card1), GetByte(card2), GetByte(card3), GetByte(card4), GetByte(card5), GetByte(card6), 0x30, 0x30, 0x30, 0x30, GetByte(pass1), GetByte(pass2), GetByte(pass3), GetByte(pass4), 0x47, 0x0D, 0x0A };
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Delete All Card
        public bool DeleteAllCard()
        {
            if (serialPort.IsOpen)
            {
                byte[] byte1 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x31, 0x37, 0x30, 0x31, 0x47, 0x0D, 0x0A };
                byte[] byte2 = new byte[] { 0x09, 0x46, 0x45, GetHighByte(address), GetLowByte(address), 0x30, 0x30, 0x30, 0x30, 0x47, 0x0D, 0x0A };

                // request to reader
                Request(byte1);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    Thread.Sleep(1000);
                    // request to reader
                    Request(byte2);
                    // wait to reply from reader
                    Thread.Sleep(delaytime * 2);
                    if (serialPort.BytesToRead > 0)
                    {
                        while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Get Byte
        private byte GetByte(string str)
        {
            return Convert.ToByte("3" + str, 16);
        }

        // Get High Byte
        private byte GetHighByte(string str)
        {
            return Convert.ToByte("3" + str.Substring(0, 1), 16);
        }

        // Get Low Byte
        private byte GetLowByte(string str)
        {
            return Convert.ToByte("3" + str.Substring(1, 1), 16);
        }

        private string GetValue(string bytes)
        {
            return bytes.Substring(1, 1);
        }
    }
}
