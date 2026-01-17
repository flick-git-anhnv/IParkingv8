using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAMERA_LIB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //anvPlayer1.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            ////anvPlayer2.SetRtspUrl("rtsp://parking:Parking123@192.168.100.205/ONVIF/MediaInput?profile=def_profile2");
            //anvPlayer2.SetRtspUrl("rtsp://parking:Parking123@192.168.100.205/ONVIF/MediaInput?profile=def_profile2");

            //anvPlayer3.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.182:554/1/1");
            //anvPlayer4.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.182:554/1/1");

            //anvPlayer5.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.242:554/Ch1");
            //anvPlayer6.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.242:554/Ch1");

            //anvPlayer7.SetRtspUrl("rtsp://root:root@192.168.21.2:554/stream1");
            //anvPlayer8.SetRtspUrl("rtsp://root:root@192.168.21.2:554/stream1");
            //anvPlayer9.SetRtspUrl("rtsp://192.168.21.205:8554/stream");
            //anvPlayer6.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");

            anvPlayer1.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            anvPlayer2.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            anvPlayer3.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            anvPlayer4.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            anvPlayer5.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            anvPlayer6.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");
            //anvPlayer2.SetRtspUrl("rtsp://192.168.21.201:8554/live/mystream");
            //anvPlayer3.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.195:554/1/1");

            //anvPlayer4.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.182:554/1/1");
            //anvPlayer5.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.182:554/1/1");
            //anvPlayer6.SetRtspUrl("rtsp://admin:Kztek123456@192.168.21.182:554/1/1");

            anvPlayer1.Connect();
            anvPlayer2.Connect();
            anvPlayer3.Connect();
            anvPlayer4.Connect();
            anvPlayer5.Connect();
            anvPlayer6.Connect();
            //anvPlayer7.Connect();
            //anvPlayer8.Connect();
            //anvPlayer9.Connect();
        }

        private async void btnSnap1_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                var bmp = anvPlayer1.GetCurrentImage();
                if (bmp != null)
                {
                    lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
                    picSnap.Image = bmp;
                    picSnap.Refresh();
                }
                await System.Threading.Tasks.Task.Delay(1);
            }

        }
        private async void btnSnap2_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                var bmp = anvPlayer2.GetCurrentImage();
                if (bmp != null)
                {
                    lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
                    picSnap.Image = bmp;
                    picSnap.Refresh();
                }
                await System.Threading.Tasks.Task.Delay(1);
            }
        }
        private async void btnSnap3_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                var bmp = anvPlayer3.GetCurrentImage();
                if (bmp != null)
                {
                    lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
                    picSnap.Image = bmp;
                    picSnap.Refresh();
                }
                await System.Threading.Tasks.Task.Delay(1);
            }
        }
        private async void btnSnap4_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                var bmp = anvPlayer4.GetCurrentImage();
                if (bmp != null)
                {
                    lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
                    picSnap.Image = bmp;
                    picSnap.Refresh();
                }
                await System.Threading.Tasks.Task.Delay(1);
            }
        }
        private void btnSnap5_Click(object sender, System.EventArgs e)
        {
            var bmp = anvPlayer5.GetCurrentImage();
            lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
            picSnap.Image = bmp;
            picSnap.Refresh();
        }
        private void btnSnap6_Click(object sender, System.EventArgs e)
        {
            var bmp = anvPlayer6.GetCurrentImage();
            lblResolution.Text = $"{bmp.Width}x{bmp.Height}";
            picSnap.Image = bmp;
            picSnap.Refresh();
        }
    }
}
