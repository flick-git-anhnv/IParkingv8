using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.DialogUcs.UserControlDialogs
{
    public partial class UcConfirmInRegisterCard : UserControl, IDialog<ConfirmInRegisterCardRequest, ConfirmInRegisterCardResult>, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<bool>? tcs;
        private int countDown = 0;
        private readonly Form dialogHost = new();
        private readonly System.Timers.Timer timerAutoConfirm = new();
        public LaneOptionalConfig LaneOptionalConfig { get; set; }
        #endregion

        #region Properties
        public UcConfirmInRegisterCard(LaneOptionalConfig laneOptionalConfig, Image? defaultImage)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.LaneOptionalConfig = laneOptionalConfig;
            countDown = laneOptionalConfig.AutoRejectDialogTime;

            kzuI_UcImagePanorama.KZUI_DefaultImage = defaultImage;
            kzuI_UcImageVehicle.KZUI_DefaultImage = defaultImage;

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

            Translate();
            this.SizeChanged += UcConfirmIn_SizeChanged;
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
        private void UcConfirmIn_SizeChanged(object? sender, EventArgs e)
        {
            Responsive();
        }

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(Translate));
                return;
            }

            lblDetectPlateSmallTitle.Text = KZUIStyles.CurrentResources.PlateDetected;
            lblTimeInTitle.Text = KZUIStyles.CurrentResources.TimeIn;
            lblAccessKeyNameTitle.Text = KZUIStyles.CurrentResources.AccesskeyName;
            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;

            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
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
            kzuI_UcImagePanorama.Width = kzuI_UcImageVehicle.Width = (this.Width - padding * 3) / 2;
            btnCancel.Width = btnCancel.Width = (this.Width - padding * 3) / 2;
            btnConfirm.Width = btnCancel.Width = (this.Width - padding * 3) / 2;

            kzuI_UcImageVehicle.Location = new Point(kzuI_UcImagePanorama.Right + 8, kzuI_UcImageVehicle.Location.Y);
            btnConfirm.Location = new Point(kzuI_UcImageVehicle.Location.X, btnConfirm.Location.Y);
        }
        #endregion

        #region Controls In Form
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
            dialogHost.Width = MathTool.GetMin(dialogHost.Width, ((Control)sender).Width * 95 / 100, ((Control)sender).Width);
            dialogHost.Height = MathTool.GetMin(dialogHost.Height, ((Control)sender).Height * 95 / 100, ((Control)sender).Height);

            dialogHost.Location = new Point(
                  frm.Left + (frm.Width - dialogHost.Width) / 2,
                  frm.Top + (frm.Height - dialogHost.Height) / 2
              );
        }
        #endregion

        #region Public Function
        public async Task<ConfirmInRegisterCardResult> ShowDialog(ConfirmInRegisterCardRequest request, Form maskedForm)
        {
            //Lấy thông tin cần hiển thị
            Translate();

            dialogHost.Width = MathTool.GetMin(dialogHost.Width, maskedForm.Width * 95 / 100);
            dialogHost.Height = MathTool.GetMin(dialogHost.Height, maskedForm.Height * 95 / 100);
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
            maskedForm.BringToFront();

            maskedForm.SizeChanged += MaskedForm_SizeChanged;
            maskedForm.LocationChanged += MaskedForm_SizeChanged;

            UpdateView(request.Code, request.PlateNumber, request.Collection, request.Images);

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

            var result = new ConfirmInRegisterCardResult()
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
        private void UpdateView(string code, string plateNumber, string collection, Dictionary<EmImageType, Image?> images)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    UpdateView(code, plateNumber, collection, images);
                }));
                return;
            }
            txtDetectPlate.Text = plateNumber;
            lblTimeIn.Text = DateTime.Now.ToVNTime();

            lblAccessKeyName.Text = code;
            lblAccessKeyGroup.Text = collection;

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

    }
}
