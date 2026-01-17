using System;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class IPCameraSettingPage : UserControl
    {
        // state changed event
        public event EventHandler StateChanged;

        // constructor
        public IPCameraSettingPage()
        {
            InitializeComponent();
            cbxChanelVideo.SelectedIndex = 0;
            cbxProtocol.SelectedIndex = 0;
            cbxChanelImg.SelectedIndex = 0;
        }

        // Completed property
        private bool completed = false;
        public bool Completed
        {
            get { return completed; }
        }

        // Show the page
        public void Display()
        {
            txtVideoSource.Focus();
            txtVideoSource.SelectionStart = txtVideoSource.TextLength;
        }

        // Apply the page
        public bool Apply()
        {
            return true;
        }

        public string VideoSource
        {
            get
            {
                string ret = txtVideoSource.Text;
                if (this.numHttpPort.Value != 0) ret += ":" + this.numHttpPort.Value;
                if (this.numServerPort.Value != 0) ret += ":" + this.numServerPort.Value;
                return ret;
            }
            set
            {
                string[] s = value.Split(new char[]
                {
                    ':'
                });
                if (s.Length <= 1) this.txtVideoSource.Text = value;
                else if(s.Length == 2 )
                {
                    this.txtVideoSource.Text = s[0];
                    int videoPort = 10000;
                    if (int.TryParse(s[1], out videoPort))
                    {
                        this.numHttpPort.Value = videoPort;
                    }
                }    
                else if (s.Length == 3)
                {
                    this.txtVideoSource.Text = s[0];
                    int videoPort = 10000;
                    if (int.TryParse(s[1], out videoPort))
                    {
                        this.numHttpPort.Value = videoPort;
                    }
                    if (int.TryParse(s[2], out videoPort))
                    {
                        this.numServerPort.Value = videoPort;
                    }
                }
            }
        }

        // Login Property
        public string Login
        {
            get { return txtLogin.Text; }
            set { txtLogin.Text = value; }
        }

        // Password property
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        // stream type property
        public int StreamType
        {
            get { return cbxStreamType.SelectedIndex; }
            set { cbxStreamType.SelectedIndex = value; }
        }

        public int ProtocolType
        {
            get { return cbxProtocol.SelectedIndex; }
            set { cbxProtocol.SelectedIndex = value; }
        }

        // chanel property
        public int Chanel
        {
            get { return cbxChanelVideo.SelectedIndex; }
            set { cbxChanelVideo.SelectedIndex = value; }
        }

        public int ChanelImg
        {
            get { return cbxChanelImg.SelectedIndex; }
            set { cbxChanelImg.SelectedIndex = value; }
        }

        // resolution property
        public string Resolution
        {
            get { return cbxImageResolution.Text; }
            set { cbxImageResolution.Text = value; }
        }

        // quality property
        public string Quality
        {
            get { return cbxImageQuality.Text; }
            set { cbxImageQuality.Text = value; }
        }

        // frame rate property
        public int FrameRate
        {
            get { return cbxFrameRate.SelectedIndex; }
            set { cbxFrameRate.SelectedIndex = value; }
        }

        // using de-interlace filter property
        private bool usingDeinterlaceFilter = false;
        public bool UsingDeinterlaceFilter
        {
            get { return usingDeinterlaceFilter; }
            set { usingDeinterlaceFilter = value; }
        }

        // de-interlace filter name property
        private string deinterlaceFilterName = "Deinterlace Filter";
        public string DeinterlaceFilterName
        {
            get { return deinterlaceFilterName; }
            set { deinterlaceFilterName = value; }
        }

        // using plugins property
        public int UsingPlugins
        {
            get { return cbxVideoDecoder.SelectedIndex; }
            set
            {
                if (value <= 4)
                    cbxVideoDecoder.SelectedIndex = value;
                else
                    cbxVideoDecoder.SelectedIndex = 0;
            }
        }

        public int HttpPort
        {
            get { return (int)numHttpPort.Value; }
            set { numHttpPort.Value = value; }
        }

        public int ServerPort
        {
            get { return (int)numServerPort.Value; }
            set { numServerPort.Value = value; }
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            completed = (txtVideoSource.Text.Trim() != "");

            if (StateChanged != null)
                StateChanged(this, new EventArgs());
        }

        private void cbxStreamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxFrameRate.Enabled = cbxImageQuality.Enabled = (cbxStreamType.SelectedIndex == 0 || cbxStreamType.SelectedIndex == 1);
            //cbxVideoDecoder.Enabled = (cbxStreamType.SelectedIndex == 1);
            if (cbxStreamType.SelectedIndex == 0)
                cbxVideoDecoder.SelectedIndex = 0;
            else if (cbxStreamType.SelectedIndex == 2 || cbxStreamType.SelectedIndex == 3)
                cbxVideoDecoder.SelectedIndex = 1;
        }
    }
}
