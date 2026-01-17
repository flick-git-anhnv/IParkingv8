using System;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class VideoCaptureDeviceSettingPage : UserControl
    {
        //FilterInfoCollection videoDevices;
        // state changed event
        public event EventHandler StateChanged;
        public VideoCaptureDeviceSettingPage()
        {
            InitializeComponent();

            // show device list
            try
            {
                // enumerate video devices
                //videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                //if (videoDevices.Count == 0)
                //    throw new ApplicationException();

                //// add all devices to combo
                //foreach (FilterInfo device in videoDevices)
                //{
                //    cbxDevice.Items.Add(device.Name);
                //}
                completed = true;
            }
            catch (ApplicationException)
            {
                cbxDevice.Items.Add("No local capture devices");
                cbxDevice.Enabled = false;
            }

            cbxDevice.SelectedIndex = 0;
            cbxDesiredFrameRate.SelectedIndex = 0;
            cbxDesiredFrameSize.SelectedIndex = 0;
            cbxChanel.SelectedIndex = 0;
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
            cbxDevice.Focus();
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
                return "";
                //return videoDevices[cbxDevice.SelectedIndex].MonikerString;
            }
            set
            {
                //for (int i = 0; i < videoDevices.Count; i++)
                //{
                //    if (videoDevices[i].MonikerString == value)
                //    {
                //        cbxDevice.SelectedIndex = i;
                //        break;
                //    }
                //}
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

        // chanel property
        public int Chanel
        {
            get { return cbxChanel.SelectedIndex; }
            set { cbxChanel.SelectedIndex = value; }
        }

        // Resolution property
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

        public int FrameRate
        {
            get { return cbxDesiredFrameRate.SelectedIndex; }
            set { cbxDesiredFrameRate.SelectedIndex = value; }
        }

        public bool UsingDeinterlaceFilter
        {
            get { return chkUsingDeinterlaceFilter.Checked; }
            set { chkUsingDeinterlaceFilter.Checked = value; }
        }

        public string DeinterlaceFilterName
        {
            get { return cbxDeinterlaceFilterName.Text; }
            set { cbxDeinterlaceFilterName.Text = value; }
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

        private void chkUsingDeinterlaceFilter_CheckedChanged(object sender, EventArgs e)
        {
            cbxDeinterlaceFilterName.Enabled = btnFindFilter.Enabled = chkUsingDeinterlaceFilter.Checked;
        }

        private void btnFindFilter_Click(object sender, EventArgs e)
        {
            bool notFound = true;
            string deinterlaceFilterName = cbxDeinterlaceFilterName.Text;
            if (deinterlaceFilterName != "")
            {
                //FilterInfoCollection filters = new FilterInfoCollection(FilterCategory.LegacyAmFilterCategory);
                //foreach (FilterInfo filterInfo in filters)
                //{
                //    if (filterInfo.Name.StartsWith(deinterlaceFilterName))
                //    {
                //        notFound = false;
                //        break;
                //    }
                //}
            }

            if (!notFound)
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Deinterlace Filter '" + deinterlaceFilterName + "' not found.");
            }
        }
    }
}
