using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_Camera
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            cameraWindow1.CreateCameraControl("KztekSDK2");
            //cameraWindow1.CameraType = "HIK";
            //cameraWindow1.Start("Cam1", "192.168.20.164", 80, 554, "admin", "Admin123", "H264", 1, "", true);

            cameraWindow1.CameraType = "Tiandy";
            cameraWindow1.Resolution = "1920x1080";
            cameraWindow1.Start("Cam1", "192.168.20.182", 80, 554, "admin", "Kztek123456", "H264", 1, "", true);


        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cameraWindow1.Stop();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = cameraWindow1.GetCurrentImage();
        }
    }
}
