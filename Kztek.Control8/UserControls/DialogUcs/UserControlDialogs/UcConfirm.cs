using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls.DialogUcs;

namespace Kztek.Control8.Forms
{
    public partial class UcConfirm : UserControl, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<bool>? tcs;

        private readonly Form dialogHost = new();
        private MaskedUserControl masked;
        private readonly Guna2Elipse guna2Elipse = new();

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

        private int closeTime = 0;
        #endregion

        #region Constructor
        public UcConfirm()
        {
            InitializeComponent();

            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;

            Translate();
            btnConfirm.OnClickAsync += BtnOk_Click;
            btnCancel.OnClickAsync += BtnCancel1_Click;
        }
        public UcConfirm(MaskedUserControl masked, UserControl targetControl) : this()
        {
            this.masked = masked;
            this._TargetControl = targetControl;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btnConfirm.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Public Function
        public async Task<bool> IsConfirmAsync(string message, LaneOptionalConfig laneOptionalConfig)
        {
            Translate();
            lblMessage.Text = message;

            //dialogHost.Width = MathTool.GetMin(dialogHost.Width, masked.Width * 95 / 100, masked.Width);
            //dialogHost.Height = MathTool.GetMin(dialogHost.Height, masked.Height * 95 / 100, masked.Height);

            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();

            closeTime = laneOptionalConfig.AutoRejectDialogTime;
            bool isEnableTimer = laneOptionalConfig.IsUseAlarmDialog && closeTime > 0;
            if (isEnableTimer)
            {
                StartTimer();
            }

            tcs = new TaskCompletionSource<bool>();
            bool isConfirm = await tcs.Task;

            StopTimer();
            dialogHost.Hide();
            masked.Hide();
            return isConfirm;
        }
        #endregion

        #region CONTROLS IN FORM
        private async Task<bool> BtnOk_Click(object? sender)
        {
            tcs.TrySetResult(true);
            return true;
        }
        private async Task<bool> BtnCancel1_Click(object? sender)
        {
            tcs.TrySetResult(false);
            return true;
        }
        #endregion End CONTROLS IN FORM

        #region TIMER
        private void TimerAutoClose_Tick(object sender, EventArgs e)
        {
            timerAutoConfirm.Enabled = false;
            closeTime--;
            if (closeTime > 0)
            {
                lblTimer.Text = $"{KZUIStyles.CurrentResources.AutoCancelAfter} {closeTime}s";
                timerAutoConfirm.Enabled = true;
            }
            else
            {
                btnCancel.PerformClick();
            }
        }

        private void StopTimer()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(timerAutoConfirm.Stop);
            }
            else
            {
                timerAutoConfirm.Stop();
            }
        }
        private void StartTimer()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(timerAutoConfirm.Start);
            }
            else
            {
                timerAutoConfirm.Start();
            }
        }
        #endregion End TIMER

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            lblGuide.Text = KZUIStyles.CurrentResources.ShortCutGuide2Line;
        }
    }
}