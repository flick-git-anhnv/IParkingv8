using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcConfirmKiosk : UserControl
    {
        const int CLOSE_TIME = 15;

        #region Properties
        private int closeTime = 0;
        public LaneOptionalConfig laneOptionalConfig { get; set; }

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
        #endregion

        #region Forms
        public UcConfirmKiosk()
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

            btnBack.Click += BtnBack_Click;
        }
        #endregion

        #region Controls In Form
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            StopTimer();
            HideView();
        }
        #endregion

        #region Public Function
        public void Show(KioskDialogRequest request)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    ShowLoadingIndicatorInternal(request);
                    StartTimer();
                }));
                return;
            }
            ShowLoadingIndicatorInternal(request);
            StartTimer();
        }

        private void ShowLoadingIndicatorInternal(KioskDialogRequest request)
        {
            this.Visible = true;

            lblTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.TitleLanguageTag));
            lblSubTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.SubTitleLanguageTag));
            btnBack.SetText(UIBuiltInResourcesHelper.GetValue(request.BackTitleTag));
            lblTimeToMain.SetText(string.Empty);

            switch (request.DialogType)
            {
                case EmImageDialogType.Error:
                    picStatus.SetImage(Properties.Resources.error);
                    break;
                case EmImageDialogType.Infor:
                    picStatus.SetImage(Properties.Resources.Infor);
                    break;
                case EmImageDialogType.Success:
                    picStatus.SetImage(Properties.Resources.Success);
                    break;
                default:
                    break;
            }

            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();
        }
        #endregion

        #region Timer
        private void StopTimer()
        {
            closeTime = CLOSE_TIME;
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(timerAutoClose.Stop);
            }
            else
            {
                timerAutoClose.Stop();
            }
        }
        private void StartTimer()
        {
            closeTime = CLOSE_TIME;
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(timerAutoClose.Start);
            }
            else
            {
                timerAutoClose.Start();
            }
        }

        private void TimerAutoClose_Tick(object sender, EventArgs e)
        {
            timerAutoClose.Enabled = false;
            closeTime--;
            if (closeTime > 0)
            {
                lblTimeToMain.Text = $"{KZUIStyles.CurrentResources.AutoCancelAfter} {closeTime}s";
                timerAutoClose.Enabled = true;
            }
            else
            {
                HideView();
            }
        }
        #endregion

        private void HideView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    dialogHost.Hide();
                    this.Visible = false;
                }));
                return;
            }
            this.Visible = false;
            dialogHost.Hide();
            masked.Hide();
        }
    }
}
