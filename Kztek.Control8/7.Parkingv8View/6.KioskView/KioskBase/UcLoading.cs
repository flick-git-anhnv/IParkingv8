using Guna.UI2.WinForms;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls.DialogUcs;

namespace Kztek.Control8.KioskBase
{
    public partial class UcLoading : UserControl
    {
        Guna2Elipse guna2Elipse = new Guna2Elipse();

        private Form dialogHost = new Form();
        private MaskedUserControl masked;

        private UserControl _TargetControl;

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

        public UcLoading()
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

        public void HideLoadingIndicator()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    dialogHost.Hide();
                    this.Visible = false;
                    masked.Hide();
                }));
                return;
            }
            this.Visible = false;
            dialogHost.Hide();
            masked.Hide();
        }

        public void ShowLoadingIndicator(string subTitle, string title)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    ShowLoadingIndicatorInternal(title, subTitle);
                }));
                return;
            }
            ShowLoadingIndicatorInternal(title, subTitle);
        }

        private void ShowLoadingIndicatorInternal(string title, string subTitle)
        {
            this.Visible = true;
            lblTitle.Text = title;
            lblSubTitle.Text = KZUIStyles.CurrentResources.WaitAMoment;
            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();
        }

        public void UpdateLoadingIndicator(string subTitle, string title)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    lblTitle.Text = title;
                    //if (!string.IsNullOrEmpty(subTitle))
                    //{
                    //    lblSubTitle.Text = subTitle;
                    //}
                }));
                return;
            }
            lblTitle.Text = title;
            //if (!string.IsNullOrEmpty(subTitle))
            //{
            //    lblSubTitle.Text = subTitle;
            //}
        }
    }
}
