using Kztek.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IParkingv8
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
            var img = ImageHelper.Base64ToImage(AppData.DefaultImageBase64);
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public frmTest(Image img)
        {
            InitializeComponent();
            pictureBox1.Image = img;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
