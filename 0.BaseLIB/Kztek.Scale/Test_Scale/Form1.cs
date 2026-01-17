using Kztek.Scale;

namespace Test_Scale
{
    public partial class Form1 : Form
    {
        private string comPort = "COM1";
        public string ComPort
        {
            get { return comPort; }
            set { comPort = value; }
        }

        private int baudRate = 9600;
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private int dataBits = 8;
        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        private int parity = 2;
        public int Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        private int timeReceived = 100;
        public int TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }

        private int stopBits = 1;
        public int StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        private IScale scale = new KingbirdScale();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxScaleType.SelectedIndex = 0;
            cbxComPort.Text = comPort;
            cbxBaudRate.Text = baudRate.ToString();
            cbxDataBits.Text = dataBits.ToString();
            cbxParity.SelectedIndex = parity;
            cbxStopBits.Text = stopBits.ToString();
            numTimeReceived.Value = timeReceived;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxScaleType.Text == "KingBird")
                {
                    scale = new KingbirdScale();
                    lblScaleType.Text = "KingBird";
                }
                else if (cbxScaleType.Text == "KingbirdStandard")
                {
                    scale = new KingbirdStandardScale();
                    lblScaleType.Text = "KingBirdStandard";
                }
                else if (cbxScaleType.Text == "D2008")
                {
                    scale = new D2008Scale();
                    lblScaleType.Text = "D2008Scale";
                }
                else if (cbxScaleType.Text == "RinstrumR320")
                {
                    scale = new RinstrumR320Scale();
                    lblScaleType.Text = "RinstrumR320Scale";
                }
                else
                {
                    scale = new KingbirdScale();
                    lblScaleType.Text = "Other";
                }

                scale.ReceivedTimeOut = (int)numTimeReceived.Value;
                scale.DataBits = int.Parse(cbxDataBits.Text);
                scale.Parity = cbxParity.SelectedIndex;
                scale.StopBits = int.Parse(cbxStopBits.Text);

                scale.DataReceivedEvent += new DataReceivedEventHandler(scale_DataReceivedEvent);
                scale.ScaleEvent += new ScaleEventHandler(scale_ScaleEvent);
                scale.ErrorEvent += new Kztek.Scale.ErrorEventHandler(scale_ScaleErrorEvent);

                if (scale.Connect(cbxComPort.Text, int.Parse(cbxBaudRate.Text)))
                {
                    btnConnect.Enabled = false;
                    btnDisConnect.Enabled = true;
                    SetText("Connected");

                    scale.PollingStart();
                }
                else
                {
                    btnConnect.Enabled = true;
                    btnDisConnect.Enabled = false;
                    SetText("Connection Fail!");
                }
            }
            catch (Exception ex)
            {
                //MainForm.configs.Logger_Error("Button Connect: " + ex.ToString());
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            scale.DataReceivedEvent -= new DataReceivedEventHandler(scale_DataReceivedEvent);
            scale.ScaleEvent -= new ScaleEventHandler(scale_ScaleEvent);
            scale.PollingStop();
            scale.Disconnect();

            btnConnect.Enabled = true;
            btnDisConnect.Enabled = false;
            SetText("Disconnect");
        }

        private void scale_DataReceivedEvent(object sender, string dataString)
        {
            SetText(dataString);
        }

        private void scale_ScaleEvent(object sender, ScaleEventArgs e)
        {
            try
            {
                string w1 = e.Gross.ToString();

                SetControlPropertyValue(labelGross, "Text", DateTime.Now.ToString("HH:mm:ss") + w1 + " kg");
            }
            catch (Exception ex)
            {
                //MainForm.configs.Logger_Error(ex.ToString());
            }
        }

        private void scale_ScaleErrorEvent(object sender, string errorString)
        {
            //MainForm.configs.Logger_Error(errorString);
        }

        // set text
        private void SetText(string text)
        {
            if (lstReceivedMessage.InvokeRequired)
            {
                lstReceivedMessage.Invoke(new MethodInvoker(delegate { SetText(text); }));

                return;
            }
            lstReceivedMessage.Items.Add(text);
            lstReceivedMessage.SelectedIndex = lstReceivedMessage.Items.Count - 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnDisConnect.Enabled)
                btnDisConnect_Click(null, null);
        }


        delegate void SetControlValueCallback(Control control, string propertyName, object propertyValue);
        public void SetControlPropertyValue(Control control, string propertyName, object propertyValue)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                    control.Invoke(d, new object[] { control, propertyName, propertyValue });
                }
                else
                {
                    Type t = control.GetType();
                    System.Reflection.PropertyInfo[] props = t.GetProperties();
                    foreach (System.Reflection.PropertyInfo p in props)
                    {
                        if (p.Name.ToUpper() == propertyName.ToUpper())
                        {
                            p.SetValue(control, propertyValue, null);
                        }
                    }
                }
            }
            catch { }
            ;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnDisConnect.Enabled == false)
            {
                btnDisConnect_Click(null, null);
            }
            comPort = cbxComPort.Text;
            baudRate = Convert.ToInt32(cbxBaudRate.Text);
            dataBits = Convert.ToInt16(cbxDataBits.Text);
            parity = cbxParity.SelectedIndex;
            stopBits = Convert.ToInt16(cbxStopBits.Text);
            timeReceived = (int)numTimeReceived.Value;

            //MainForm.configs.scaleType = cbxScaleType.Text;            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnDisConnect.Enabled == false)
            {
                btnDisConnect_Click(null, null);
            }
        }
    }
}