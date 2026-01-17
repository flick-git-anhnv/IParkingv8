using iParkingv8.Object.Enums.Devices;
using iParkingv8.Ultility;
using IParkingv8.UserControls;
using Kztek.Cameras;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcCameraView : UserControl
    {
        public Camera? CurrentCamera { get; set; }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Border Radius"), Description("Border Radius")]
        public int BorderRadius
        {
            get => guna2Elipse1.BorderRadius / 2;
            set
            {
                guna2Elipse1.BorderRadius = value * 2;
            }
        }
        private string laneId = string.Empty;
        private ILane lane;

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Title"), Description("Display Title")]
        public string KZUI_Title
        {
            get => lblCameraName.Text;
            set { lblCameraName.Text = value; }
        }
        private double alarmLevel = 0;
        public KZUI_UcCameraView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            lblResult.Text = "";
        }

        public EmCameraPurpose CameraPurpose;
        private int cameraSDK = 0;
        public void Init(ILane lane, iParkingv8.Object.Objects.Devices.Camera camera, EmCameraPurpose cameraPurpose, int cameraSDK)
        {
            this.cameraSDK = cameraSDK;
            this.laneId = lane.Lane?.Id ?? string.Empty;
            this.lane = lane;
            string _camType = camera.GetCameraType() == "HIK" ? "HIKVISION2" : camera.GetCameraType();
            this.KZUI_Title = camera.Name;
            this.CameraPurpose = cameraPurpose;
            picCameraType.Image = cameraPurpose switch
            {
                EmCameraPurpose.CarLpr => Properties.Resources.icons8_fiat_500_32px_3,
                EmCameraPurpose.Panorama => Properties.Resources.icons8_camera_32px_1,
                EmCameraPurpose.MotorLpr => Properties.Resources.icons8_motorcycle_32px_2,
                _ => Properties.Resources.icons8_camera_32px_1,
            };

            this.CurrentCamera = new Camera()
            {
                ID = camera.Id,
                Name = camera.Name,
                VideoSource = camera.IpAddress,
                HttpPort = int.Parse(camera.HttpPort),
                Login = camera.Username,
                Password = camera.Password,
                Chanel = camera.Channel,
                CameraType = CameraTypes.GetType(_camType),
                StreamType = StreamTypes.GetType("H264"),
                Resolution = string.IsNullOrEmpty(camera.Resolution) ? "1280x720" : camera.Resolution,
                RtspPort = string.IsNullOrEmpty(camera.RtspPort) ? 554 : int.Parse(camera.RtspPort),
            };
            this.CurrentCamera.MotionAnalyzing += CurrentCamera_MotionAnalyzing;
        }
        public void Init(iParkingv8.Object.Objects.Devices.Camera camera, EmControlSizeMode controlSizeMode, EmCameraPurpose cameraPurpose, int cameraSDK)
        {
            lblResult.Visible = controlSizeMode != EmControlSizeMode.SMALL && (cameraPurpose == EmCameraPurpose.MotorLpr || cameraPurpose == EmCameraPurpose.CarLpr);
            this.cameraSDK = cameraSDK;
            string _camType = camera.GetCameraType() == "HIK" ? "HIKVISION2" : camera.GetCameraType();
            this.KZUI_Title = camera.Name;
            this.CameraPurpose = cameraPurpose;
            picCameraType.Image = cameraPurpose switch
            {
                EmCameraPurpose.CarLpr => Properties.Resources.icons8_fiat_500_32px_3,
                EmCameraPurpose.Panorama => Properties.Resources.icons8_camera_32px_1,
                EmCameraPurpose.MotorLpr => Properties.Resources.icons8_motorcycle_32px_2,
                _ => Properties.Resources.icons8_camera_32px_1,
            };

            switch (controlSizeMode)
            {
                case EmControlSizeMode.SMALL:
                    lblCameraName.Font = new Font(lblCameraName.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblCameraName.Font.Style, GraphicsUnit.Pixel);
                    lblCameraName.Height = SizeManagement.MEDIUM_HEIGHT; break;
                case EmControlSizeMode.MEDIUM:
                    lblCameraName.Font = new Font(lblCameraName.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblCameraName.Font.Style, GraphicsUnit.Pixel);
                    lblCameraName.Height = SizeManagement.MEDIUM_HEIGHT; break;
                case EmControlSizeMode.LARGE:
                    lblCameraName.Font = new Font(lblCameraName.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblCameraName.Font.Style, GraphicsUnit.Pixel);
                    lblCameraName.Height = SizeManagement.MEDIUM_HEIGHT; break;
                default:
                    break;
            }

            this.CurrentCamera = new Cameras.Camera()
            {
                ID = camera.Id,
                Name = camera.Name,
                VideoSource = camera.IpAddress,
                HttpPort = int.Parse(camera.HttpPort),
                RtspPort = string.IsNullOrEmpty(camera.RtspPort) ? 554 : int.Parse(camera.RtspPort),
                Login = camera.Username,
                Password = camera.Password,
                Chanel = camera.Channel,
                CameraType = Cameras.CameraTypes.GetType(_camType),
                StreamType = Cameras.StreamTypes.GetType("H264"),
                Resolution = string.IsNullOrEmpty(camera.Resolution) ? "1280x720" : camera.Resolution,
            };
        }

        public void StartViewer(bool isMotionDetection, double alarmLevel)
        {
            this.alarmLevel = alarmLevel;
            if (this.CurrentCamera == null)
            {
                return;
            }
            if (this.lane != null)
            {
                isMotionDetection = this.lane.Lane.Loop && isMotionDetection;
            }
            lblCameraName.Text = this.CurrentCamera.Name;
            this.CurrentCamera.CameraError += CurrentCamera_CameraError;
            this.CurrentCamera.Start(isMotionDetection, alarmLevel, this.cameraSDK, 1000);

            if (CurrentCamera != null && CurrentCamera.videoSourcePlayer != null)
            {
                var control = (Control)CurrentCamera.videoSourcePlayer;
                panelCameraView.Controls.Add(control);

                control.Name = CurrentCamera.ID;
                control.Dock = DockStyle.Fill;
                control.DoubleClick += Control_DoubleClick;
                control.BringToFront();
            }
            lblCameraName.DoubleClick += Control_DoubleClick;
        }

        private void CurrentCamera_MotionAnalyzing(object sender, float motionLevel)
        {
            float _alarmLevel = (float)this.alarmLevel / 1000f;
            if (this.IsHandleCreated)
            {
                lblCameraMotion.BeginInvoke(new Action(() =>
                {
                    if (!lblCameraMotion.Visible)
                    {
                        lblCameraMotion.Visible = true;
                    }
                    lblCameraMotion.Text = Math.Round(motionLevel, 1).ToString();// + " || " + Math.Round(_alarmLevel, 1);
                    lblCameraMotion.Width = lblCameraMotion.PreferredWidth;
                    lblCameraMotion.Refresh();
                }));
                //picMotion.BeginInvoke(new Action(() =>
                //{
                //    bool isReachMotionLevel = motionLevel >= _alarmLevel;
                //    if (picMotion.Visible != isReachMotionLevel)
                //    {
                //        picMotion.Visible = isReachMotionLevel;
                //        picMotion.Refresh();
                //    }
                //}));
            }
            if (motionLevel < _alarmLevel)
            {
                return;
            }
            ((Control)this.lane).BeginInvoke(new Action(() =>
                {
                    var topParentForm = ((Control)this.lane).TopLevelControl;
                    if (topParentForm == null || topParentForm is not Form frm)
                    {
                        return;
                    }
                    if (frm.WindowState != FormWindowState.Minimized)
                    {
                        return;
                    }
                    try
                    {
                        frm.WindowState = FormWindowState.Maximized;
                        frm.Show();
                        frm.Activate();
                    }
                    catch (Exception)
                    {
                    }
                }));
            this.lane.OnNewEvent(new MotionDetectEventArgs());
        }

        public void Stop()
        {
            this.CurrentCamera?.Stop();
        }
        private void CurrentCamera_CameraError(object sender, string errorString)
        {
        }
        private void Control_DoubleClick(object? sender, EventArgs e)
        {
            frmViewCamera frm = new()
            {
                CurrentCamera = CurrentCamera
            };
            frm.ShowDialog();
        }

        private readonly object _lock = new();
        public Image? GetFullCurrentImage()
        {
            //lock (_lock)
            {
                try
                {
                    var bmp = CurrentCamera!.GetCurrentVideoFrame();
                    if (bmp == null)
                    {
                        bmp = CurrentCamera.GetCurrentVideoFrame();
                        if (bmp == null)
                        {
                            return null;
                        }
                    }
                    return bmp;
                }
                catch (AccessViolationException ex)
                {
                    // Log exception details
                    Console.WriteLine("Access violation occurred: " + ex.Message);
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void ActiveDetectPlateMode()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(ActiveDetectPlateMode);
                return;
            }
            picStartLpr.BringToFront();
            picStartLpr.Visible = true;
        }
        public void DisplayPlateResult(string plateNumber, long duration)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    DisplayPlateResult(plateNumber, duration);
                }));
                return;
            }
            picStartLpr.Visible = false;
            if (!string.IsNullOrEmpty(plateNumber))
            {
                lastDetectTime = DateTime.Now;
                if (!lblResult.Visible)
                {
                    lblResult.Visible = true;
                    lblResult.BringToFront();
                }
                lastPlateNumber = plateNumber;
                lblResult.Text = $"{plateNumber} : {duration}ms";
                lblResult.Left = (this.Width - lblResult.Width - 4);
                lblResult.Refresh();
            }
            else if (!string.IsNullOrEmpty(lastPlateNumber) && (DateTime.Now - lastDetectTime).TotalSeconds <= 10)
            {
                if (!lblResult.Visible)
                {
                    lblResult.Visible = true;
                    lblResult.BringToFront();
                }
                lblResult.Text = $"▼{lastPlateNumber}";
                lblResult.Left = (this.Width - lblResult.Width - 4);
                lblResult.Refresh();
            }
            else
            {
                lastPlateNumber = "";
                lblResult.Visible = false;
            }
        }
        string lastPlateNumber = string.Empty;
        DateTime lastDetectTime = DateTime.Now;
    }
}