//using Kztek.LPR;
//using NXT.Net6.LPR_AI;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace KztekLprDetectionTest.UserControls
{
    public partial class ucDetectByPic : UserControl
    {

        private Image? selectedImage = null;
        private bool isDrawing = false;

        public ucDetectByPic()
        {
            InitializeComponent();
        }

        private void trackbarRotate_ValueChanged(object? sender, EventArgs e)
        {
            numRotate.ValueChanged -= numRotate_ValueChanged;
            numRotate.Value = trackbarRotate.Value;
            picInput.Image = RotateImage(selectedImage, (float)numRotate.Value);
            picInput.Refresh();
            numRotate.ValueChanged += numRotate_ValueChanged;
            //picInput.Invalidate();
        }

        private void numRotate_ValueChanged(object sender, EventArgs e)
        {
            trackbarRotate.ValueChanged -= trackbarRotate_ValueChanged;
            trackbarRotate.Value = (int)numRotate.Value;
            picInput.Image = RotateImage(selectedImage, (float)numRotate.Value);
            trackbarRotate.ValueChanged += trackbarRotate_ValueChanged;
        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            if (img == null)
            {
                return img;
            }
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(img, new Point(0, 0));
            gfx.Dispose();
            return bmp;
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            selectedImage = null;
            picInput.Image = null;
            picOutput.Image = null;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedImage = Image.FromFile(ofd.FileName);
                    picInput.Image = RotateImage(selectedImage, (float)numRotate.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void btnDetectFromImage_Click(object sender, EventArgs e)
        {
            picOutput.Image = null;
            if (selectedImage == null)
            {
                MessageBox.Show("Hãy chọn hình ảnh cần nhận dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var sw = Stopwatch.StartNew();
            //var lPRObject_Result = new LPR_Result_Object();
            //lPRObject_Result.vehicleImage = (Bitmap)picInput.Image;
            //if (chbIsCar.Checked)
            //{
            //    AppData.carANPRAI.Analyze(ref lPRObject_Result);
            //}
            //else
            //{
            //    AppData.motorANPRAI.Analyze(ref lPRObject_Result);
            //}

            var result = await AppData.LprDetecter.GetPlateNumberAsync(picInput.Image, chbIsCar.Checked, null, 0, false);
            var detectTime = sw.ElapsedMilliseconds;
            picOutput.Image = result.LprImage;
            lblResult.Text = $"\"{result.PlateNumber}\" - {detectTime}ms";
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            isDrawing = true;
        }

        private void picInput_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void picInput_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void picInput_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnSimpleLPR3_Click(object sender, EventArgs e)
        {
            //picOutput.Image = null;
            //if (selectedImage == null)
            //{
            //    MessageBox.Show("Hãy chọn hình ảnh cần nhận dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //var sw = Stopwatch.StartNew();
            //LPRObject lPRObject_Result = new LPRObject((Bitmap)picInput.Image);

            //lPRObject_Result.vehicleImage = (Bitmap)picInput.Image;
            //if (chbIsCar.Checked)
            //{
            //    lPRObject_Result.enableMultiplePlateNumber = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    AppData.carANPR1.Analyze2(ref lPRObject_Result);
            //}
            //else
            //{
            //    lPRObject_Result.enableMultiplePlateNumber = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    AppData.motoANPR1.Analyze2(ref lPRObject_Result);
            //}

            //var detectTime = sw.ElapsedMilliseconds;
            //picOutput.Image = lPRObject_Result.plateImage;
            //lblResult.Text = $"\"{lPRObject_Result.plateNumber}\" - {detectTime}ms";
        }

        private void btnSimpleLPR2_Click(object sender, EventArgs e)
        {
            //picOutput.Image = null;
            //if (selectedImage == null)
            //{
            //    MessageBox.Show("Hãy chọn hình ảnh cần nhận dạng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //var sw = Stopwatch.StartNew();
            //LPRObject lPRObject_Result = new LPRObject((Bitmap)picInput.Image);
            //lPRObject_Result.vehicleImage = (Bitmap)picInput.Image;
            //if (chbIsCar.Checked)
            //{
            //    lPRObject_Result.enableMultiplePlateNumber = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    AppData.carANPR1.Analyze(ref lPRObject_Result);
            //}
            //else
            //{
            //    lPRObject_Result.enableMultiplePlateNumber = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    lPRObject_Result.enableRawFormat = true;
            //    AppData.motoANPR1.Analyze(ref lPRObject_Result);
            //}

            //var detectTime = sw.ElapsedMilliseconds;
            //picOutput.Image = lPRObject_Result.plateImage;
            //lblResult.Text = $"\"{lPRObject_Result.plateNumber}\" - {detectTime}ms";
        }
    }
}
