using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public partial class UcRegisterPlate : UserControl, IDialog<ConfirmPlateRequest, ConfirmPlateResult>, KzITranslate
    {
        private TaskCompletionSource<bool>? tcs;
        private int countDown = 0;
        public LaneOptionalConfig LaneOptionalConfig { get; set; }
        Form dialogHost;
        public UcRegisterPlate(LaneOptionalConfig laneOptionalConfig)
        {
            InitializeComponent();
            this.LaneOptionalConfig = laneOptionalConfig;
            this.DoubleBuffered = true;

            btnConfirm.Cursor = Cursors.Hand;
            btnCancel.Cursor = Cursors.Hand;
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += BtnCancel_Click;
            this.KeyDown += FrmRegisterPlate_KeyDown;
            lblCountDown.Text = "";

            countDown = laneOptionalConfig.AutoRejectDialogTime;

            dialogHost = new Form()
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Size = this.Size,
                BackColor = Color.White,
                ShowInTaskbar = false,
            };

            typeof(Form).InvokeMember("DoubleBuffered",
                                    System.Reflection.BindingFlags.SetProperty |
                                    System.Reflection.BindingFlags.Instance |
                                    System.Reflection.BindingFlags.NonPublic,
                                    null, dialogHost, new object[] { true });


            this.Dock = DockStyle.Fill;
            dialogHost.Controls.Add(this);
        }

        private void FrmRegisterPlate_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { btnCancel.PerformClick(); }
            else if (e.KeyCode == Keys.Return) { btnConfirm.PerformClick(); }
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            timerAutoConfirm.Enabled = false;
            tcs?.SetResult(true);
        }
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            tcs?.SetResult(false);
        }
        private void MaskedForm_SizeChanged(object? sender, EventArgs e)
        {
            var frm = (sender as Form)!;
            dialogHost.Location = new Point(
                  frm.Left + (frm.Width - dialogHost.Width) / 2,
                  frm.Top + (frm.Height - dialogHost.Height) / 2
              );
        }
        public async Task<ConfirmPlateResult> ShowDialog(ConfirmPlateRequest confirmPlateRequest, Form maskedForm)
        {
            Translate();
            txtPlate.Text = confirmPlateRequest.PlateNumber;
            pictureBox1.Image = (Image)(confirmPlateRequest.LprImage?.Clone());

            dialogHost.Location = new Point(
                maskedForm.Left + (maskedForm.Width - dialogHost.Width) / 2,
                maskedForm.Top + (maskedForm.Height - dialogHost.Height) / 2
            );

            // Hiển thị dialog chính
            dialogHost.Show(maskedForm);

            if (!maskedForm.Visible)
            {
                maskedForm.Show();
            }
            maskedForm.BringToFront();

            maskedForm.SizeChanged += MaskedForm_SizeChanged;
            maskedForm.LocationChanged += MaskedForm_SizeChanged;

            countDown = LaneOptionalConfig.AutoRejectDialogTime;
            timerAutoConfirm.Enabled = LaneOptionalConfig.IsUseAlarmDialog && countDown > 0;

            btnConfirm.Focus();
            this.ActiveControl = btnConfirm;

            tcs = new TaskCompletionSource<bool>();
            var isConfirm = await tcs.Task;
            var result = new ConfirmPlateResult()
            {
                IsConfirm = isConfirm,
                UpdatePlate = txtPlate.Text,
            };
            maskedForm.SizeChanged -= MaskedForm_SizeChanged;
            maskedForm.LocationChanged -= MaskedForm_SizeChanged;
            timerAutoConfirm.Enabled = false;
            dialogHost.Hide();
            return result;
        }

        private void TimerAutoConfirm_Tick(object sender, EventArgs e)
        {
            countDown--;
            if (countDown == 0)
            {
                timerAutoConfirm.Stop();
                if (this.LaneOptionalConfig.AutoRejectDialogResult)
                {
                    btnCancel.PerformClick();
                }
                else
                {
                    btnConfirm.PerformClick();
                }
                return;
            }
            else
            {
                lblCountDown.Text = this.LaneOptionalConfig.AutoRejectDialogResult ?
                                            KZUIStyles.CurrentResources.AutoCancelAfter + $" {countDown} s" :
                                            KZUIStyles.CurrentResources.AutoConfirmAfter + $" {countDown} s";
                lblCountDown.Refresh();
            }
        }

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            lblTitle.Text = KZUIStyles.CurrentResources.ConfirmPlateRequired;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            lblShortcutGuide1Line.Text = KZUIStyles.CurrentResources.ShortCutGuide1Line;
        }
        public void Cancel()
        {
            if (btnCancel.InvokeRequired)
            {
                btnCancel.Invoke(new Action(() => btnCancel.PerformClick()));
                return;
            }

            btnCancel.PerformClick();
        }
    }
}
