using System;
using System.IO.Ports;
using System.Threading;
namespace Kztek.Scale
{
    public class EX2001Scale : IScale
    {
        public SerialPort serialPort;
        private bool isConnected;
        private bool isStable = true;
        private DateTime lastTimeReceivedEvent = DateTime.Now;
        private System.Timers.Timer reconnectTimer = new System.Timers.Timer();
        private string comPort = "COM1";
        private int baudRate = 9600;
        private int dataBits = 8;
        private int parity = 2;
        private int stopBits = 1;
        public int receivedTimeOut = 500;
        public event ScaleEventHandler ScaleEvent;
        public event ErrorEventHandler ErrorEvent;
        public event DataReceivedEventHandler DataReceivedEvent;
        public string ComPort
        {
            get
            {
                return this.comPort;
            }
            set
            {
                this.comPort = value;
            }
        }
        public int BaudRate
        {
            get
            {
                return this.baudRate;
            }
            set
            {
                this.baudRate = value;
            }
        }
        public int DataBits
        {
            get
            {
                return this.dataBits;
            }
            set
            {
                this.dataBits = value;
            }
        }
        public int Parity
        {
            get
            {
                return this.parity;
            }
            set
            {
                this.parity = value;
            }
        }
        public int StopBits
        {
            get
            {
                return this.stopBits;
            }
            set
            {
                this.stopBits = value;
            }
        }
        public int ReceivedTimeOut
        {
            get
            {
                return this.receivedTimeOut;
            }
            set
            {
                this.receivedTimeOut = value;
                if (this.receivedTimeOut < 400) this.receivedTimeOut = 400;
            }
        }
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }
        public bool IsStable
        {
            get { return isStable; }
            set { isStable = value; }
        }

        public EX2001Scale()
        {
        }
        public bool Connect()
        {
            return this.Connect(this.comPort, this.baudRate);
        }
        public bool Connect(string portName, int baudRate)
        {
            this.comPort = portName;
            this.baudRate = baudRate;
            bool result;
            try
            {
                this.serialPort = new SerialPort();
                this.serialPort.PortName = portName;
                this.serialPort.BaudRate = baudRate;
                this.serialPort.ReadBufferSize = 4096;
                this.serialPort.WriteBufferSize = 4096;
                this.serialPort.DataBits = this.dataBits;
                if (this.parity == 0)
                {
                    this.serialPort.Parity = System.IO.Ports.Parity.Even;
                }
                else
                {
                    if (this.parity == 1)
                    {
                        this.serialPort.Parity = System.IO.Ports.Parity.Odd;
                    }
                    else
                    {
                        if (this.parity == 2)
                        {
                            this.serialPort.Parity = System.IO.Ports.Parity.None;
                        }
                    }
                }
                if (this.stopBits == 1)
                {
                    this.serialPort.StopBits = System.IO.Ports.StopBits.One;
                }
                else
                {
                    if (this.stopBits == 2)
                    {
                        this.serialPort.StopBits = System.IO.Ports.StopBits.Two;
                    }
                }
                this.serialPort.ReadTimeout = -1;
                this.serialPort.WriteTimeout = -1;
                this.serialPort.DtrEnable = true;
                this.serialPort.RtsEnable = true;
                this.serialPort.Open();
                this.isConnected = true;
                result = true;
            }
            catch (Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, ex.ToString());
                }
                result = false;
            }
            return result;
        }
        public bool Disconnect()
        {
            bool result;
            try
            {
                if (this.serialPort.IsOpen)
                {
                    this.serialPort.Close();
                }
                this.isConnected = false;
                result = true;
            }
            catch (Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, ex.ToString());
                }
                result = false;
            }
            return result;
        }

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks - this.lastTimeReceivedEvent.Ticks);
            if (timeSpan.TotalSeconds >= 15.0)
            {
                this.PollingStop();
                this.Disconnect();
                System.Threading.Thread.Sleep(100);
                this.Connect();
                this.PollingStart();
                this.lastTimeReceivedEvent = DateTime.Now;
            }
        }
        public void PollingStart()
        {
            try
            {
                this.isConnected = false;
                if (this.serialPort.IsOpen)
                {
                    this.isConnected = true;
                }
                this.serialPort.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            }
            catch (Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, ex.ToString());
                }
            }
        }
        public void SignalToStop()
        {
        }
        public void PollingStop()
        {
            try
            {
                this.serialPort.DataReceived -= new SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            }
            catch (Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, ex.ToString());
                }
            }
        }
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.isConnected = true;
            string dataString = "";
            this.serialPort.DiscardInBuffer();
            string dataReceived = this.ReadData(ref dataString);
            if (this.DataReceivedEvent != null)
            {
                this.DataReceivedEvent(this, dataString);
            }
            this.lastTimeReceivedEvent = DateTime.Now;
            this.ParseData(dataReceived);
        }
        private void ParseData(string dataReceived)
        {
            if (this.ScaleEvent != null)
            {
                try
                {
                    if (dataReceived.Length >= 6)
                    {
                        bool isMinusValue = false;
                        int decimalValue = 0;
                        if (dataReceived.Length == 8)
                        {
                            if (dataReceived.Contains("-"))
                            {
                                isMinusValue = true;
                            }
                            dataReceived = dataReceived.Substring(2, 6);
                        }
                        int gross = 0;
                        ScaleEventArgs scaleEventArgs = new ScaleEventArgs();
                        if (int.TryParse(dataReceived, out gross))
                        {
                            scaleEventArgs.Gross = gross;
                            scaleEventArgs.IsMinusValue = isMinusValue;
                            scaleEventArgs.DecimalValue = decimalValue;
                        }
                        else
                        {
                            scaleEventArgs.Gross = 0;
                        }
                        this.ScaleEvent(this, scaleEventArgs);
                    }
                }
                catch (Exception ex)
                {
                    if (this.ErrorEvent != null)
                    {
                        this.ErrorEvent(this, ex.ToString());
                    }
                }
            }
        }
        private string ReadData(ref string dataReceived)
        {
            string result;
            try
            {
                Thread.Sleep(this.receivedTimeOut);
                string text = "";
                while (this.serialPort.BytesToRead > 0)
                {
                    int num = this.serialPort.ReadByte();
                    char c = Convert.ToChar(num);   
                    dataReceived += c;
                    text += c;
                }
                int num2 = text.LastIndexOf("kg");
                if (num2 <= 0) num2 = text.LastIndexOf("KG"); Console.WriteLine(num2);
                if (num2 >= 6)
                {
                    if (num2 < 8)
                    {
                        text = text.Substring(num2 - 6, 6);
                    }
                    else
                    {
                        text = text.Substring(num2 - 8, 8);
                    }
                    result = text;
                }
                else
                {
                    result = "";
                }
            }
            catch (Exception ex)
            {
                if (this.ErrorEvent != null)
                {
                    this.ErrorEvent(this, ex.ToString());
                }
                result = "";
            }
            return result;
        }
        public bool TestConnection()
        {
            return this.serialPort != null && this.serialPort.IsOpen;
        }
    }
}
