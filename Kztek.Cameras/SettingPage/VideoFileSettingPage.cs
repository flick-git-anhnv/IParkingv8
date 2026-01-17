using System;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class VideoFileSettingPage : UserControl
    {
        // state changed event
        public event EventHandler StateChanged;
        public VideoFileSettingPage()
        {
            InitializeComponent();
            cbxDesiredFrameSize.SelectedIndex = 0;
            completed = txtVideoFile.Text != "";
        }

        private bool completed = false;
        // Completed property
        public bool Completed
        {
            get { return completed; }
        }

        // Show the page
        public void Display()
        {
            txtVideoFile.Focus();
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
                return txtVideoFile.Text;
            }
            set
            {
                txtVideoFile.Text = value;
                completed = txtVideoFile.Text != "";
            }
        }

        // Login property
        private string login = "";
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        // Password property
        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private int streamType = 1;
        public int StreamType
        {
            get { return streamType; }
            set { streamType = value; }
        }

        private int protocolType = 0;
        public int ProtocolType
        {
            get { return protocolType; }
            set { protocolType = value; }
        }

        private int chanel = 1;
        public int Chanel
        {
            get { return chanel; }
            set { chanel = value; }
        }

        // Resolution property
        //private string resolution = "640x480";
        //public string Resolution
        //{
        //    get { return resolution; }
        //    set { resolution = value; }
        //}

        public string Resolution
        {
            get { return cbxDesiredFrameSize.Text; }
            set { cbxDesiredFrameSize.Text = value; }
        }

        // Quality property
        private string quality = "Standard";
        public string Quality
        {
            get { return quality; }
            set { quality = value; }
        }

        private int frameRate = 30;
        public int FrameRate
        {
            get { return frameRate; }
            set { frameRate = value; }
        }

        private bool usingDeinterlaceFilter = false;
        public bool UsingDeinterlaceFilter
        {
            get { return usingDeinterlaceFilter; }
            set { usingDeinterlaceFilter = value; }
        }

        private string deinterlaceFilterName = "Deinterlace Filter";
        public string DeinterlaceFilterName
        {
            get { return deinterlaceFilterName; }
            set { deinterlaceFilterName = value; }
        }

        public int UsingPlugins
        {
            get { return cbxVideoDecoder.SelectedIndex; }
            set
            {
                if (value <= 2)
                    cbxVideoDecoder.SelectedIndex = value;
                else
                    cbxVideoDecoder.SelectedIndex = 0;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = "AVI files (*.avi)|*.avi|All files (*.*)|*.*";
            if (opd.ShowDialog() == DialogResult.OK)
            {
                txtVideoFile.Text = opd.FileName;
                completed = txtVideoFile.Text != "";
            }
        }

        private void txtVideoFile_TextChanged(object sender, EventArgs e)
        {
            completed = txtVideoFile.Text != "";
            if (StateChanged != null)
                StateChanged(this, new EventArgs());
        }
    }
}
