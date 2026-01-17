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
    public class RCP4000
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
        public RCP4000()
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
                        if (controller.isFirstPackage)
                        {
                            Thread.Sleep(downloadTime * 1000);
                            string readerModel = "";
                            string readerDate = "";
                            string readerTime = "";
                            controller.eventCount = 0;

                            UploadSettings(controller.Address, ref readerModel, ref readerDate, ref readerTime, ref controller.eventCount);

                            if (serialPort.IsOpen)
                                serialPort.Close();
                            serialPort.Open();
                            if (serialPort.IsOpen)
                            {
                                if (controller.eventCount > 0)
                                {
                                    // get controller
                                    byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xB1, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
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
                                DeleteAllEvent(controller.Address);
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

       // Download clock
        public bool DownloadDateTime(DateTime datetime)
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
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xAA, Convert.ToByte(ByteUntils.DecimalToBase(Address, 16), 16), Year, MonthWeek, Day, Hour, Minute, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
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

        public void UploadSettings(int adr, ref string readerModel, ref string date, ref string time, ref int numEvents)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xAF, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                bytes[15] = ByteUntils.CRC(bytes, 16);
                // request to reader
                Request(bytes);
                // wait to reply from reader
                Thread.Sleep(delaytime);
                string[] message = Answer();
                if (message != null)
                {
                    switch (message[3])
                    {
                        case "02":
                            {
                                readerModel = "RCP4000";
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
                    }
                }
                serialPort.Close();
            }
        }

        // Delete All Event
        public bool DeleteAllEvent(int adr)
        {
            if (serialPort.IsOpen)
                serialPort.Close();
            Thread.Sleep(500);
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                byte[] bytes = new byte[16] { 0xEE, 0xBB, 0xA1, Convert.ToByte(ByteUntils.DecimalToBase(adr, 16), 16), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
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
