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
    public class DS3000
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
        public DS3000()
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
                        if (temp != "EB")
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
                        if (controller.isFirstPackage)
                        {
                            Thread.Sleep(downloadTime * 1000);
                            string readerModel = "";
                            string readerDate = "";
                            string readerTime = "";
                            controller.eventCount = 0;
                            int readerRelay = 0;
                            int doorRelay = 0;

                            UploadSettings(controller.Address, ref readerModel, ref readerDate, ref readerTime, ref controller.eventCount, ref readerRelay, ref doorRelay);
                            controller.nextPackage = 0;
                            controller.isFirstPackage = false;
                        }

                        byte adr_LO = (byte)(controller.nextPackage & 0xff);
                        byte adr_HI = (byte)(controller.nextPackage >> 8);

                        if (serialPort.IsOpen)
                            serialPort.Close();
                        serialPort.Open();
                        if (serialPort.IsOpen)
                        {
                            if (controller.eventCount * 8 > controller.nextPackage)
                            {
                                // get controller
                                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xB1, Convert.ToByte(ByteUntils.DecimalToBase(controller.Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, adr_HI, adr_LO, 0x00 };
                                bytes[15] = ByteUntils.CRC(bytes, 16);
                                // request to reader
                                Request(bytes);
                                // wait to reply from reader
                                Thread.Sleep(delaytime);
                                Thread.Sleep(1000);
                                int package = 0;
                                while (true)
                                {
                                    package++;
                                    if (package > 16)
                                        break;
                                    string[] message = Answer();
                                    //Thread.Sleep(500);
                                    if (message != null && message.Length >= 16)
                                    {
                                        CardEventArgs e = new CardEventArgs();
                                        e.LineID = controller.LineID;
                                        e.LineCode = controller.LineCode;
                                        e.ControllerAddress = controller.Address;
                                        e.ReaderIndex = Convert.ToInt32(message[12]) + 1;
                                        e.CardNumber = message[3].ToString() + message[4].ToString() + message[5].ToString() + message[6].ToString();
                                        e.Date = "20" + message[7] + "/" + message[8] + "/" + message[9];
                                        e.Time = message[10] + ":" + message[11] + ":00";
                                        e.EventStatus = "Access Granted";
                                        CardEvent(this, e);
                                        controller.nextPackage += 8;
                                        if (IsLastRecord(message))
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (controller.eventCount > 0)
                                {
                                    DeleteAllEvent(controller.Address);
                                    controller.eventCount = 0;
                                    controller.nextPackage = 0;
                                }

                                controller.isFirstPackage = true;
                            }
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

        // is last record
        public bool IsLastRecord(string[] buffer)
        {
            //CRC
            int temp = 0;
            for (int i = 0; i < 15; i++)
            {
                temp = temp + ByteUntils.BaseToDecimal(buffer[i], 16);
            }
            temp = temp % 256;
            //if CRC + 1 = B15
            if (temp + 1 == ByteUntils.BaseToDecimal(buffer[15], 16))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Open Door
        public bool OpenDoor()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xB0, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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
        public bool DownloadSettings(DateTime datetime, int readerRelay, int doorRelay)
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
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xAA, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), Year, MonthWeek, Day, Hour, Minute, Convert.ToByte(ByteUntils.DecimalToBase(readerRelay, 16), 16), Convert.ToByte(ByteUntils.DecimalToBase(doorRelay, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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

        public void UploadSettings(int adr, ref string readerModel, ref string date, ref string time, ref int numEvents, ref int readerRelay, ref int doorRelay)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(1000);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xAF, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && message.Length >= 16)
                {
                    switch (message[1])
                    {
                        case "B0":
                            {
                                readerModel = "CP-4000";
                                break;
                            }
                        case "B1":
                            {
                                readerModel = "DS-2000";
                                break;
                            }
                        case "B3":
                            {
                                readerModel = "DS-1700";
                                break;
                            }
                        case "B5":
                            {
                                readerModel = "DS-1000";
                                break;
                            }
                        case "B6":
                            {
                                readerModel = "DS-1600";
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
                        numEvents = (ByteUntils.BaseToDecimal(message[4], 16) * 256 + ByteUntils.BaseToDecimal(message[3], 16)) / 8;
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
        public bool DownloadCard(string cardNumber, int memoryID, Timezone timezone)
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

                //address
                byte adr_HI = 0x40, adr_LO = 0x00;
                adr_HI = Convert.ToByte(ByteUntils.DecimalToBase((64 + memoryID / 256), 16), 16);
                adr_LO = Convert.ToByte(ByteUntils.DecimalToBase(memoryID % 256, 16), 16);

                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xBF, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), card1, card2, card3, card4, Convert.ToByte(ByteUntils.DecimalToBase(weekday, 16), 16), Convert.ToByte(start.ToString(), 16), Convert.ToByte(stop.ToString(), 16), 0xFF, 0x00, adr_HI, adr_LO, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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
        public bool DeleteCard(int memoryID)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte adr_HI = 0x40, adr_LO = 0x00;
                adr_HI = Convert.ToByte(ByteUntils.DecimalToBase((64 + memoryID / 256), 16), 16);
                adr_LO = Convert.ToByte(ByteUntils.DecimalToBase(memoryID % 256, 16), 16);
                //
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xBF, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x24, 0xFF, 0x00, adr_HI, adr_LO, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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
        public bool DeleteAllCard()
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(1000);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xBC, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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
        public bool DeleteAllEvent(int adr)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(1000);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEB, 0xBB, 0xA1, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
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

        // Test CE
        public bool TestCE(ref string CEModel)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                CEModel = "";
                byte[] bytes = new byte[16] { 0xEB, 0xCC, 0xAF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x66 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime * 2);
                Thread.Sleep(1000);
                string[] message = Answer();
                if (message != null && message.Length >= 16)
                {
                    switch (message[15])
                    {
                        case "89":
                            {
                                CEModel = "CE-1";
                                break;
                            }
                        case "8D":
                            {
                                CEModel = "CE-8";
                                break;
                            }
                        case "91":
                            {
                                CEModel = "CE-16";
                                break;
                            }
                        default:
                            {
                                CEModel = "";
                                break;
                            }
                    }
                }
                serialPort.Close();
            }
            if (CEModel != "")
                return true;
            return false;
        }
    }
}
