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
    public class CS2000
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
        public CS2000()
        {
 
        }

        // Line ID
        private int lineID = 0;
        public int LineID
        {
            get { return lineID; }
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
        private string address = "00";
        public int Address
        {
            get { return Convert.ToInt32(address); }
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
                //serialPort.Open();
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
                int index = 0;
                while (serialPort.BytesToRead > 0)
                {
                    if (temp == null)
                    {
                        temp = ByteUntils.DecimalToBase(serialPort.ReadByte(), 16);
                        if (temp != "EE")
                            temp = null;
                        else
                            index++;
                    }
                    else
                    {
                        temp = temp + "|" + ByteUntils.DecimalToBase(serialPort.ReadByte(), 16);
                        index++;
                    }

                    if (index >= 16)
                        break;
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
                    foreach (Futech.Objects.Controller controller in controllers)
                    {
                        byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
                        byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
                        if (controller.isFirstPackage)
                        {
                            Thread.Sleep(downloadTime * 1000);
                            string readerModel = "";
                            string readerDate = "";
                            string readerTime = "";
                            controller.eventCount = 0;
                            int readerRelay = 0;
                            int doorRelay = 0;

                            UploadSettings(controller.Address, ref readerModel, ref readerDate, ref readerTime, ref controller.eventCount, ref readerRelay, ref doorRelay, pin_HI, pin_LO);

                            if (serialPort.IsOpen)
                                serialPort.Close();
                            serialPort.Open();
                            if (serialPort.IsOpen)
                            {
                                if (controller.eventCount > 0)
                                {
                                    // get controller
                                    byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xB1, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                                    bytes[15] = ByteUntils.CRC(bytes, 16);
                                    // request to reader
                                    Request(bytes);
                                    // wait to reply from reader
                                    Thread.Sleep(delaytime);
                                    Thread.Sleep(1000);
                                }
                            }

                            controller.nextPackage = 0;
                            controller.isFirstPackage = false;
                        }

                        if (serialPort.IsOpen)
                        {
                            while (controller.eventCount > controller.nextPackage)
                            {
                                string[] message = Answer();
                                if (message != null)
                                {
                                    CardEventArgs e = new CardEventArgs();
                                    e.LineID = controller.LineID;
                                    e.LineCode = controller.LineCode;
                                    e.ControllerAddress = controller.Address;
                                    e.ReaderIndex = Convert.ToInt32(message[10]) + 1;
                                    e.CardNumber = message[1].ToString() + message[2].ToString() + message[3].ToString() + message[4].ToString();
                                    e.Date = "20" + message[5] + "/" + message[6] + "/" + message[7];
                                    e.Time = message[8] + ":" + message[9] + ":00";
                                    e.EventStatus = "Access Granted";
                                    CardEvent(this, e);
                                    controller.nextPackage++;
                                }
                            }

                            if (controller.eventCount > 0)
                            {
                                DeleteAllEvent(controller.Address, pin_HI, pin_LO);
                                controller.eventCount = 0;
                                controller.nextPackage = 0;
                            }
                            controller.isFirstPackage = true;
                            serialPort.Close();
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
        public bool OpenDoor(byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xB0, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        // Download clock
        public bool DownloadDateTime(DateTime datetime, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                //get year, moth + week, day, hour, minute
                byte Year, MonthWeek, Day, Hour, Minute;
                int Month, Week = 0;
                //
                Year = Convert.ToByte(Convert.ToString(datetime.Year - 2000), 16);

                Month = datetime.Month;
                Week = DateUI.GetDayOfWeekInNumberDS3000(datetime);
                MonthWeek = Convert.ToByte(ByteUntils.DecimalToBase(Month + Week, 16), 16);

                Day = Convert.ToByte(datetime.Day.ToString(), 16);
                Hour = Convert.ToByte(datetime.Hour.ToString(), 16);
                Minute = Convert.ToByte(datetime.Minute.ToString(), 16);
                //
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xAA, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), Year, MonthWeek, Day, Hour, Minute, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        public bool DownloadRelayTime(int readerRelay, int doorRelay, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xA6, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), Convert.ToByte(ByteUntils.DecimalToBase(readerRelay, 16), 16), Convert.ToByte(ByteUntils.DecimalToBase(doorRelay, 16), 16), 0, 0, 0, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        public void UploadSettings(int adr, ref string readerModel, ref string date, ref string time, ref int numEvents, ref int readerRelay, ref int doorRelay, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xAF, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                string[] message = Answer();
                if (message != null)
                {
                    switch (message[0])
                    {
                        case "EE":
                            {
                                readerModel = "CS2000";
                                break;
                            }
                        default:
                            {
                                readerModel = "";
                                break;
                            }
                    }

                    if (readerModel != "")
                    {
                        //get date
                        date = "20" + message[5] + "/" + message[6] + "/" + message[7];
                        //get time
                        time = message[8] + ":" + message[9] + ":00";
                        //get number of events
                        numEvents = (ByteUntils.BaseToDecimal(message[2], 16) * 256 + ByteUntils.BaseToDecimal(message[1], 16)) / 8;
                        //get relay time 00h-1sec....0Fh-16sec
                        readerRelay = ByteUntils.BaseToDecimal(message[10], 16);
                        //get maximum time for door opening only DS-2000
                        doorRelay = ByteUntils.BaseToDecimal(message[11], 16);
                    }
                }
                serialPort.Close();
            }
        }

        // Download Card
        public bool DownloadCard(string cardNumber, Timezone timezone, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen && cardNumber.Length == 8)
            {
                //card number
                byte card1, card2, card3, card4;
                cardNumber = cardNumber.ToUpper();
                card1 = Convert.ToByte(cardNumber.Substring(0, 2), 16);
                card2 = Convert.ToByte(cardNumber.Substring(2, 2), 16);
                card3 = Convert.ToByte(cardNumber.Substring(4, 2), 16);
                card4 = Convert.ToByte(cardNumber.Substring(6, 2), 16);
                //
                int weekday = 0;
                int start = 0;
                int stop = 0;
                GetWeekDay(timezone, ref weekday, ref start, ref stop);

                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xB6, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), card1, card2, card3, card4, 0x00, pin_HI, pin_LO, Convert.ToByte(ByteUntils.DecimalToBase(weekday, 16), 16), Convert.ToByte(start.ToString(), 16), Convert.ToByte(stop.ToString(), 16), 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        //weekday
        private void GetWeekDay(Timezone timezone, ref int weekday, ref int start, ref int stop)
        {
            byte[] buffer = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            if (timezone.Sun != "00:00-00:00")
                buffer[7] = 1;
            if (timezone.Mon != "00:00-00:00")
                buffer[6] = 1;
            if (timezone.Tue != "00:00-00:00")
                buffer[5] = 1;
            if (timezone.Wed != "00:00-00:00")
                buffer[4] = 1;
            if (timezone.Thu != "00:00-00:00")
                buffer[3] = 1;
            if (timezone.Fri != "00:00-00:00")
                buffer[2] = 1;
            if (timezone.Sat != "00:00-00:00")
                buffer[1] = 1;
            //
            string temp = "";
            for (int i = 0; i < 8; i++)
            {
                temp = temp + buffer[i].ToString();
            }
            weekday = Convert.ToInt32(temp, 2);

            string[] mon = timezone.Mon.Split('-');
            start = Convert.ToInt32(mon[0].Substring(0, 2));
            stop = Convert.ToInt32(mon[1].Substring(0, 2));
            if (Convert.ToInt32(mon[1].Substring(3, 2)) > 30)
                stop += 1;
        }

        // Delete Card
        public bool DeleteCard(string cardNumber, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                //card number
                byte card1, card2, card3, card4;
                cardNumber = cardNumber.ToUpper();
                card1 = Convert.ToByte(cardNumber.Substring(0, 2), 16);
                card2 = Convert.ToByte(cardNumber.Substring(2, 2), 16);
                card3 = Convert.ToByte(cardNumber.Substring(4, 2), 16);
                card4 = Convert.ToByte(cardNumber.Substring(6, 2), 16);
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xBA, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), card1, card2, card3, card4, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        // Delete All Card
        public bool DeleteAllCard(byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xBC, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }

        // Delete All Event
        public bool DeleteAllEvent(int adr, byte pin_HI, byte pin_LO)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xA1, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, pin_HI, pin_LO, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                if (serialPort.BytesToRead > 0)
                {
                    while (serialPort.BytesToRead > 0) { serialPort.ReadByte(); };
                    serialPort.Close();
                    return true;
                }
            }
            serialPort.Close();
            return false;
        }
    }
}
