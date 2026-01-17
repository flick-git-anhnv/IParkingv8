using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.ConfigUcs.CameraConfigs
{
    public partial class FrmCameraConfigSet : Form, KzITranslate
    {
        #region Properties
        private Image img;
        private Rectangle? config = null;
        private Point? StartPoint = null;
        private bool isDrawing = false;
        #endregion

        #region Forms
        public FrmCameraConfigSet(Image img, Rectangle? config)
        {
            InitializeComponent();
            InitProperties(img, config);
            Translate();

            this.Load += FrmCameraConfigSet_Load;
        }

        private void InitProperties(Image img, Rectangle? config)
        {
            this.img = img;
            this.config = config;
        }

        private void FrmCameraConfigSet_Load(object? sender, EventArgs e)
        {
            btnConfirm.Click += BtnOk1_Click;
            btnCancel.Click += BtnCancel1_Click;

            panelCameraView.AutoScroll = true;
            pic.Paint += Pic_Paint;
            pic.MouseUp += Pic_MouseUp;

            if (img != null)
            {
                pic.Image = img;
                pic.Location = new Point(0, 0);
                pic.Height = this.DisplayRectangle.Height - panelActions.Height;
                pic.Width = (int)(((float)img.Size.Width / (img.Size.Height)) * pic.Height);
            }
            this.config = GetSaveDisplayConfig(config);
            pic.Invalidate();
        }
        #endregion

        private void BtnCancel1_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void BtnOk1_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Pic_Paint(object? sender, PaintEventArgs e)
        {
            if (config != null)
            {
                Pen blackPen = new(Color.RebeccaPurple, 3);
                e.Graphics.DrawRectangle(blackPen, (Rectangle)config);
            }
        }
        private void Pic_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            this.StartPoint = e.Location;
            pic.Invalidate();
        }
        private void Pic_MouseUp(object? sender, MouseEventArgs e)
        {
            isDrawing = false;
        }
        private void Pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.StartPoint != null && isDrawing)
            {
                config = new Rectangle(Math.Min(StartPoint.Value.X, e.X),
                                        Math.Min(StartPoint.Value.Y, e.Y),
                                        Math.Abs(StartPoint.Value.X - e.X),
                                        Math.Abs(StartPoint.Value.Y - e.Y));
                pic.Invalidate();
            }
        }
        public Rectangle? GetSaveConfig()
        {
            if (pic.Image == null || config == null)
            {
                return null;
            }
            float ratioWidth = (float)pic.Width / pic.Image.Width;
            float ratioHeight = (float)pic.Height / pic.Image.Height;

            return new Rectangle((int)(config.Value.X / ratioWidth), (int)(config.Value.Y / ratioHeight),
                                (int)(config.Value.Width / ratioWidth), (int)(config.Value.Height / ratioHeight));
        }

        public Rectangle? GetSaveDisplayConfig(Rectangle? config)
        {
            if (pic.Image == null || config == null)
            {
                return null;
            }
            float ratioWidth = (float)pic.Width / pic.Image.Width;
            float ratioHeight = (float)pic.Height / pic.Image.Height;

            return new Rectangle((int)(config.Value.X * ratioWidth), (int)(config.Value.Y * ratioHeight),
                                (int)(config.Value.Width * ratioWidth), (int)(config.Value.Height * ratioHeight));
        }

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            this.Text = KZUIStyles.CurrentResources.Config;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
        }
    }
}
