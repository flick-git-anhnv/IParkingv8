using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.DialogUcs.UserControlDialogs
{
    public partial class UcConfirmInSmall : UserControl, IDialog<ConfirmInRequest, ConfirmPlateResult>, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<bool>? tcs;
        private int countDown = 0;
        private readonly Form dialogHost = new();
        private readonly System.Timers.Timer timerAutoConfirm = new();
        public LaneOptionalConfig LaneOptionalConfig { get; set; }
        #endregion

        #region Properties
        public UcConfirmInSmall(LaneOptionalConfig laneOptionalConfig)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.LaneOptionalConfig = laneOptionalConfig;
            countDown = laneOptionalConfig.AutoRejectDialogTime;

            btnCancel.Click += BtnCancel_Click;
            btnConfirm.Click += BtnConfirm_Click;

            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.MinimumSize = this.MinimumSize;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;

            dialogHost.Controls.Add(this);
            this.Dock = DockStyle.Fill;

            Guna2Elipse guna2Elipse1 = new()
            {
                TargetControl = dialogHost,
                BorderRadius = 24
            };

            timerAutoConfirm.Interval = 1000;
            timerAutoConfirm.Elapsed += TimerAutoConfirm_Tick;
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

        #region Controls In Form
        private void BtnCopy_Click(object sender, EventArgs e)
        {
            txtDetectPlate.Text = lblAlarmPlate.Text;
        }
        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            timerAutoConfirm.Stop();
            tcs?.TrySetResult(true);
        }
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            timerAutoConfirm.Stop();
            tcs?.TrySetResult(false);
        }
        private void MaskedForm_SizeChanged(object? sender, EventArgs e)
        {
            var frm = (sender as Form)!;
            dialogHost.Location = new Point(
                  frm.Left + (frm.Width - dialogHost.Width) / 2,
                  frm.Top + (frm.Height - dialogHost.Height) / 2
              );
        }
        #endregion

        #region Public Function
        public async Task<ConfirmPlateResult> ShowDialog(ConfirmInRequest request, Form maskedForm)
        {
            //Lấy thông tin cần hiển thị
            Translate();

            var abnormalCode = request.AbnormalCode;
            string message = request.Message;
            AccessKey? accessKey = request.AccessKey;
            string alarmPlate = request.RegisterPlate;
            string plateNumber = request.PlateNumber;

            dialogHost.Width = this.Width = MathTool.GetMin(dialogHost.Width, maskedForm.Width * 95 / 100);
            dialogHost.Height = this.Height = MathTool.GetMin(dialogHost.Height, maskedForm.Height * 95 / 100);
            Responsive();
            dialogHost.Location = new Point(
                maskedForm.Left + (maskedForm.Width - dialogHost.Width) / 2,
                maskedForm.Top + (maskedForm.Height - dialogHost.Height) / 2
            );

            maskedForm.SuspendLayout();
            dialogHost.SuspendLayout();
            this.SuspendLayout();

            if (!maskedForm.Visible)
            {
                maskedForm.Show();
            }

            maskedForm.SizeChanged += MaskedForm_SizeChanged;
            maskedForm.LocationChanged += MaskedForm_SizeChanged;

            UpdateView(abnormalCode, message, accessKey, alarmPlate, plateNumber, request.Images);

            this.Visible = true;
            this.Focus();
            dialogHost.Show(maskedForm);

            countDown = LaneOptionalConfig.AutoRejectDialogTime;
            if (LaneOptionalConfig.IsUseAlarmDialog && countDown > 0)
            {
                timerAutoConfirm.Start();
            }
            this.ResumeLayout(false);
            dialogHost.ResumeLayout(false);
            maskedForm.ResumeLayout(false);

            tcs = new TaskCompletionSource<bool>();
            bool isConfirm = false;
            try
            {
                isConfirm = await tcs.Task;
            }
            catch (TaskCanceledException)
            {
                isConfirm = false;
            }

            maskedForm.SuspendLayout();
            dialogHost.SuspendLayout();
            this.SuspendLayout();
            maskedForm.SizeChanged -= MaskedForm_SizeChanged;
            maskedForm.LocationChanged -= MaskedForm_SizeChanged;

            var result = new ConfirmPlateResult()
            {
                IsConfirm = isConfirm,
                UpdatePlate = txtDetectPlate.Text,
            };

            timerAutoConfirm.Stop();
            dialogHost.Hide();
            this.Visible = false;
            this.ResumeLayout(false);
            dialogHost.ResumeLayout(false);
            maskedForm.ResumeLayout(false);

            return result;
        }
        public void Cancel()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Cancel);
                return;
            }
            btnCancel.PerformClick();
        }
        #endregion

        #region Private Function
        private void UpdateView(EmAlarmCode abnormalCode, string message, AccessKey? accessKey, string alarmPlate, string plateNumber, Dictionary<EmImageType, Image?> images)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateView(abnormalCode, message, accessKey, alarmPlate, plateNumber, images);
                }));
                return;
            }
            lblMessage.Text = message;
            txtDetectPlate.Text = plateNumber;
            lblAlarmPlate.Text = alarmPlate;
            lblTimeIn.Text = DateTime.Now.ToVNTime();

            string accessKeyName = accessKey?.Name ?? string.Empty;
            lblAccessKeyName.Text = accessKeyName;
            lblAccessKeyGroup.Text = accessKey?.Collection?.Name ?? "";

            txtDetectPlate.SelectionStart = plateNumber.Length;

            var panoramaImage = images.ContainsKey(EmImageType.PANORAMA) ? images[EmImageType.PANORAMA] : null;
            var vehicleImage = images.ContainsKey(EmImageType.VEHICLE) ? images[EmImageType.VEHICLE] : null;
            var faceImage = images.ContainsKey(EmImageType.FACE) ? images[EmImageType.FACE] : null;
            var plateImage = images.ContainsKey(EmImageType.PLATE_NUMBER) ? images[EmImageType.PLATE_NUMBER] : null;

            if (panoramaImage is null)
                kzuI_UcImagePanorama.Visible = false;
            else
                kzuI_UcImagePanorama.KZUI_Image = panoramaImage;

            if (vehicleImage is null)
                kzuI_UcImageVehicle.Visible = false;
            else
                kzuI_UcImageVehicle.KZUI_Image = panoramaImage;

            if (faceImage is null)
                kzuI_UcImageFace.Visible = false;
            else
                kzuI_UcImageFace.KZUI_Image = faceImage;

            if (plateImage is null)
                kzuI_UcImageLpr.Visible = false;
            else
                kzuI_UcImageLpr.KZUI_Image = plateImage;
        }
        #endregion

        #region Timer
        private void TimerAutoConfirm_Tick(object? sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                timerAutoConfirm.Stop();
                countDown--;
                if (countDown == 0)
                {
                    lblCountDown.Visible = false;
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

                lblCountDown.Text = this.LaneOptionalConfig.AutoRejectDialogResult ?
                                            KZUIStyles.CurrentResources.AutoCancelAfter + $" {countDown} s" :
                                            KZUIStyles.CurrentResources.AutoConfirmAfter + $" {countDown} s";
                lblCountDown.Visible = true;
                lblCountDown.Refresh();
                timerAutoConfirm.Start();
            }));
        }
        #endregion

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(Translate));
                return;
            }
            lblDetectPlateSmallTitle.Text = KZUIStyles.CurrentResources.PlateDetected;
            lblPlateAlarmTitle.Text = KZUIStyles.CurrentResources.VehicleCode;
            lblTimeInTitle.Text = KZUIStyles.CurrentResources.TimeIn;
            lblAccessKeyNameTitle.Text = KZUIStyles.CurrentResources.AccesskeyName;
            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;

            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            lblShortcutGuide1Line.Text = KZUIStyles.CurrentResources.ShortCutGuide1Line;

            kzuI_UcImagePanorama.KZUI_Title = KZUIStyles.CurrentResources.PicPanoramaIn;
            kzuI_UcImageVehicle.KZUI_Title = KZUIStyles.CurrentResources.PicVehicleIn;
        }
        private void Responsive()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Responsive);
                return;
            }

            int padding = 8;
            txtDetectPlate.Width = (this.Width - padding * 3) / 2;
            lblAlarmPlate.Width = this.Width - btnCopy.Width - padding - txtDetectPlate.Width - padding * 3;

            lblAlarmPlate.Location = new Point(txtDetectPlate.Right + 8, lblAlarmPlate.Location.Y);
            lblPlateAlarmTitle.Location = new Point(lblAlarmPlate.Location.X, lblPlateAlarmTitle.Location.Y);
            btnCopy.Location = new Point(lblAlarmPlate.Right + 8, btnCopy.Location.Y);
            //btnCancel.Location = new Point(this.Width - btnCancel.Width - 8);
        }
    }
}
