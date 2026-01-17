using Kztek.Cameras;
using Kztek.Tool;
using System.Runtime.InteropServices;

namespace iPakrkingv5.Controls.Usercontrols
{
    public partial class MovablePictureBox : PictureBox
    {
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        public delegate void OnMouseDownEvent(object sender, MouseEventArgs e);
        public delegate void OnMouseMoveEvent(object sender, MouseEventArgs e);
        public delegate void OnMouseUpEvent(object sender, MouseEventArgs e);
        public delegate void OnChangeSizeEvent(object sender, ChangeSizeEventArgs e);
        public class ChangeSizeEventArgs : EventArgs
        {
            public Size Oldsize { get; set; }
            public Size Newsize { get; set; }
        }

        public event OnMouseDownEvent? onMouseDownEvent;
        public event OnMouseDownEvent? onMouseMoveEvent;
        public event OnMouseDownEvent? onMouseUpEvent;
        public event OnChangeSizeEvent? onChangeSizeEvent;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int LPAR);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public MovablePictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.Zoom;

            this.MouseDown += MoveablePictureBox_MouseDown;
            this.MouseMove += MoveablePictureBox_MouseMove;
            this.MouseWheel += MovablePictureBox_MouseWheel;
            this.MouseDoubleClick += MovablePictureBox_MouseDoubleClick;
            this.MouseEnter += MovablePictureBox_MouseEnter;
            this.MouseLeave += MovablePictureBox_MouseLeave;
        }

        private void MovablePictureBox_MouseLeave(object? sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        private void MovablePictureBox_MouseEnter(object? sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void MovablePictureBox_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (this.DisplayImage != null)
            {
                var frm = new frmViewImage
                {
                    CurrentImage = (Image?)this.DisplayImage?.Clone()
                };
                frm.Show();
            }
        }

        bool isWheeling = false;
        private float zoomFactor = 1.0f;
        private const float ZoomIncrement = 0.2f;

        private void MovablePictureBox_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && !isWheeling)
            {
                isWheeling = true;
                this.Cursor = Cursors.WaitCursor;
                float zoomDirection = Math.Sign(e.Delta) * ZoomIncrement;
                int changeWidth = (int)(zoomDirection * this.Width);
                int changeHeight = (int)(zoomDirection * this.Height);

                Size oldMapSize = new(this.Width, this.Height);
                Size newMapSize = new(this.Width + changeWidth, this.Height + changeHeight);

                float ratioWidth = (float)newMapSize.Width / oldMapSize.Width;
                float ratioHeight = (float)newMapSize.Height / oldMapSize.Height;

                int imagePreferWidth = this.Image.Width;
                int imagePreferHeight = this.Image.Height;

                //Old real map size
                int oldHeight = oldMapSize.Height;
                int oldWidth = imagePreferWidth * oldHeight / imagePreferHeight;
                Size oldRealPicSize = new(oldWidth, oldHeight);
                Point oldRealPicLocation = new((oldMapSize.Width - oldRealPicSize.Width) / 2, 0);

                //New real map size
                int newHeight = newMapSize.Height;
                int newWidth = imagePreferWidth * newHeight / imagePreferHeight;
                Size newRealPicSize = new(newWidth, newHeight);
                Point newRealPicLocation = new((newMapSize.Width - newRealPicSize.Width) / 2, 0);

                this.SuspendLayout();
                this.Size = new Size(this.Width + changeWidth, this.Height + changeHeight);
                Point UpdateMousePoint = new((int)(Math.Round(e.X * ratioWidth)), (int)(e.Y * ratioHeight));
                this.Location = new Point((int)(this.Location.X - (UpdateMousePoint.X - e.X)),
                                                this.Location.Y - UpdateMousePoint.Y + e.Y);
                this.ResumeLayout();

                onChangeSizeEvent?.Invoke(this, new ChangeSizeEventArgs() { Oldsize = oldMapSize, Newsize = newMapSize });
                this.Cursor = Cursors.Default;
                isWheeling = false;
            }

        }
        private void MoveablePictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            onMouseMoveEvent?.Invoke(this, e);
        }
        private void MoveablePictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control)
            {
                this.Cursor = Cursors.Hand;
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                this.Cursor = Cursors.Default;
            }
            else
            {
                onMouseDownEvent?.Invoke(sender, e);
            }
        }


        public Image? DisplayImage { get; set; }

        public void ShowImage(Image? img)
        {
            this.DisplayImage = img;
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Image?>(ShowImageInternal), img);
                ShowImageInternal(img);
                return;
            }
            ShowImageInternal(img);
        }
        private void ShowImageInternal(Image? img)
        {
            try
            {
                var resizedImage = ImageHelper.GetResizeImage(img, this.Width, this.Height);
                this.Image = resizedImage;
            }
            catch (Exception)
            {
            }
        }
    }
}