using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace Kztek.Cameras
{
    public partial class frmViewCamera : Form
    {
        private Camera currentCamera;
        private int updateInterval = 300;
        private System.Timers.Timer timer = new System.Timers.Timer();
        System.Drawing.Bitmap currentVideoFrame = null;

        public Camera CurrentCamera
        {
            get
            {
                return this.currentCamera;
            }
            set
            {
                this.currentCamera = value;
            }
        }
        public int UpdateInterval
        {
            get
            {
                return this.updateInterval;
            }
            set
            {
                this.updateInterval = value;
            }
        }

        public frmViewCamera()
        {
            InitializeComponent();
        }

        private void frmViewCamera_Load(object sender, EventArgs e)
        {
            this.timer.Interval = (double)((this.updateInterval > 300) ? this.updateInterval : 300);
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_Tick);
            this.timer.Start();
            txtIP.Text = currentCamera.videoSourcePlayer.VideoSource.Source;
        }
        private void frmViewCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer.Stop();
            this.timer.Dispose();
            GC.Collect();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    timer.Enabled = false;
                }));
                if (this.currentCamera != null)
                {
                    currentVideoFrame = null;
                    this.Invoke(new Action(() =>
                    {
                        if (this.currentCamera.IsRunning)
                        {
                            currentVideoFrame = this.currentCamera.GetCurrentVideoFrame();
                            this.pictureBox1.Image = currentVideoFrame;
                        }
                        if (currentVideoFrame != null)
                        {
                            this.lblVideoSize.Text = $"Video Size: {currentVideoFrame.Width}x{currentVideoFrame.Height}";
                            lblVideoSize.Width = lblVideoSize.PreferredWidth;
                        }
                        else
                        {
                            this.lblVideoSize.Text = "";
                        }
                    }));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Invoke(new Action(() =>
                {
                    timer.Enabled = true;
                }));
            }
        }

        private void btnOpenWebview_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName ="http://" +this.currentCamera.VideoSource,
                UseShellExecute = true
            });
        }
    }
}
