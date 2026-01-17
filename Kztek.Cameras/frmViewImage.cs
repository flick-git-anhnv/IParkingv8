using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Imaging;
using System.IO;

namespace Kztek.Cameras
{
    public partial class frmViewImage : Form
    {
        public static string preferPath = @"C:\";
        private Image currentImage = null;
        public Image CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;

                pictureBox1.Image = currentImage;
                if (currentImage != null)
                {
                    lblVideoSize.Text = "Size: " + currentImage.Width + "x" + currentImage.Height;
                }
                else
                    lblVideoSize.Text = "";
            }
        }

        public frmViewImage()
        {
            InitializeComponent();
        }

        private void frmViewImage_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = currentImage;
            if (currentImage != null)
            {
                lblVideoSize.Text = "Size: " + currentImage.Width + "x" + currentImage.Height;
            }
            else
                lblVideoSize.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = preferPath;
            saveFileDialog.DefaultExt = ".jpeg";
            saveFileDialog.Filter = "image files (*.jpeg)|*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveFileDialog.FileName;
                preferPath = Path.GetDirectoryName(savePath);
                pictureBox1.Image.Save(savePath, ImageFormat.Jpeg);
            }
        }
    }
}
