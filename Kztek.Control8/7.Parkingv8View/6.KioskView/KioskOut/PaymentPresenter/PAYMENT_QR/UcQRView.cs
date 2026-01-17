using Guna.UI2.WinForms;
using Kztek.Control8.KioskOut.PaymentPresenter.QR;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcQRView : UserControl, IQRView
    {
        Guna2Elipse guna2Elipse = new Guna2Elipse();

        private readonly Form dialogHost = new();
        private MaskedUserControl? masked;

        private UserControl? _TargetControl;

        public UserControl TargetControl
        {
            get => _TargetControl;
            set
            {
                this._TargetControl = value;
                masked?.Dispose();
                masked = new MaskedUserControl(value);
            }
        }

        public int BorderRadius
        {
            get => guna2Elipse.BorderRadius;
            set
            {
                guna2Elipse.BorderRadius = value;
            }
        }
        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }

        public UcQRView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.Visible = false;
            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;
        }

        public void DisplayQRImage(Image image)
        {
            picQR.SetImage(image);
        }

        public void ShowView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Visible = true;
                    dialogHost.Location = new Point(
                            this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                            this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                        );
                    dialogHost.Show(this.masked);
                    masked.Show();
                }));
                return;
            }
            this.Visible = true;
            dialogHost.Location = new Point(
                            this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                            this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                        );
            dialogHost.Show(this.masked);
            masked.Show();
        }
        public void HideView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Visible = false;
                    dialogHost.Hide();
                    masked.Hide();
                }));
                return;
            }
            this.Visible = false;
            dialogHost.Hide();
            masked.Hide();
        }

        public void SetLblGuide(string text) => lblGuide.SetText(text);
        public void SetBtnBackText(string text) => btnBack.SetText(text);
    }
}
