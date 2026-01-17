using System;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class CameraSettingPage : UserControl, IWizardPage
    {
        //private IVideoSourcePage sourcePage;
        private Camera camera;
        private int cameraType = -1;
        private bool completed;

        public event EventHandler StateChanged;
        public event EventHandler Reset;
        public Camera Camera
        {
            get
            {
                return this.camera;
            }
            set
            {
                if (value != null)
                {
                    this.camera = value;
                    //if (this.sourcePage != null)
                    //{
                    //    this.sourcePage.VideoSource = this.camera.VideoSource;
                    //    this.sourcePage.Login = this.camera.Login;
                    //    this.sourcePage.Password = this.camera.Password;
                    //    this.sourcePage.StreamType = (int)this.camera.StreamType;
                    //    this.sourcePage.ProtocolType = (int)this.camera.ProtocolType;
                    //    this.sourcePage.Chanel = this.camera.Chanel;
                    //    this.sourcePage.Resolution = this.camera.Resolution;
                    //    this.sourcePage.Quality = this.camera.Quality;
                    //    this.sourcePage.FrameRate = this.camera.FrameRate;
                    //    this.sourcePage.UsingPlugins = this.camera.UsingPlugins;
                    //    this.completed = this.sourcePage.Completed;
                    //}
                }
            }
        }
        public int CameraType
        {
            get
            {
                return this.cameraType;
            }
            set
            {
                if (this.cameraType == -1 || this.cameraType != value)
                {
                    //if (this.sourcePage != null)
                    //{
                    //    base.Controls.Remove((Control)this.sourcePage);
                    //}
                    this.completed = false;
                    this.cameraType = value;
                    if (this.cameraType > -1)
                    {
                        //switch (CameraTypes.GetType(this.cameraType))
                        //{
                        //    case Kztek.Cameras.CameraType.VideoFile:
                        //        this.sourcePage = new VideoFileSettingPage();
                        //        break;
                        //    case Kztek.Cameras.CameraType.VideoCaptureDevice:
                        //        this.sourcePage = new VideoCaptureDeviceSettingPage();
                        //        break;
                        //    default:
                        //        this.sourcePage = new IPCameraSettingPage();
                        //        break;
                        //}

                        //if (this.sourcePage != null)
                        //{
                        //    Control control = (Control)this.sourcePage;
                        //    control.Dock = DockStyle.Fill;
                        //    base.Controls.Add(control);
                        //    this.sourcePage.StateChanged += new EventHandler(this.page_StateChanged);
                        //    this.sourcePage.VideoSource = this.camera.VideoSource;
                        //    this.sourcePage.Login = this.camera.Login;
                        //    this.sourcePage.Password = this.camera.Password;
                        //    this.sourcePage.StreamType = (int)this.camera.StreamType;
                        //    this.sourcePage.ProtocolType = (int)this.camera.ProtocolType;
                        //    this.sourcePage.Chanel = this.camera.Chanel;
                        //    this.sourcePage.Resolution = this.camera.Resolution;
                        //    this.sourcePage.Quality = this.camera.Quality;
                        //    this.sourcePage.FrameRate = this.camera.FrameRate;
                        //    this.sourcePage.UsingDeinterlaceFilter = this.camera.UsingDeinterlaceFilter;
                        //    this.sourcePage.DeinterlaceFilterName = this.camera.DeinterlaceFilterName;
                        //    this.sourcePage.UsingPlugins = this.camera.UsingPlugins;
                        //    this.completed = this.sourcePage.Completed;
                        //}
                    }
                }
            }
        }
        public string PageName
        {
            get
            {
                return "Thông số Camera";
            }
        }
        public string PageDescription
        {
            get
            {
                string str = "2. Thiết lập thông số Camera\n";
                if (this.cameraType > -1)
                {
                    str = str + "             " + CameraTypes.GetType(this.cameraType).ToString();
                }
                return str;
            }
        }
        public bool Completed
        {
            get
            {
                return this.completed;
            }
        }
        public CameraSettingPage()
        {
            this.InitializeComponent();
        }
        public void Display()
        {
            //    if (this.sourcePage != null)
            //    {
            //        ((Control)this.sourcePage).Show();
            //        this.sourcePage.Display();
            //    }
        }
        public bool Apply()
        {
            bool ret = false;
            //if (this.sourcePage != null && (ret = this.sourcePage.Apply()))
            //{
            //    this.camera.VideoSource = this.sourcePage.VideoSource;
            //    this.camera.Login = this.sourcePage.Login;
            //    this.camera.Password = this.sourcePage.Password;
            //    this.camera.StreamType = (StreamType)this.sourcePage.StreamType;
            //    this.camera.ProtocolType = (ProtocolType)this.sourcePage.ProtocolType;
            //    this.camera.Chanel = this.sourcePage.Chanel;
            //    this.camera.Resolution = this.sourcePage.Resolution;
            //    this.camera.Quality = this.sourcePage.Quality;
            //    this.camera.FrameRate = this.sourcePage.FrameRate;
            //    this.camera.UsingDeinterlaceFilter = this.sourcePage.UsingDeinterlaceFilter;
            //    this.camera.DeinterlaceFilterName = this.sourcePage.DeinterlaceFilterName;
            //    this.camera.UsingPlugins = this.sourcePage.UsingPlugins;
            //}
            return ret;
        }
        private void page_StateChanged(object sender, EventArgs e)
        {
            //this.completed = this.sourcePage.Completed;
            if (this.StateChanged != null)
            {
                this.StateChanged(this, new EventArgs());
            }
        }
    }
}
