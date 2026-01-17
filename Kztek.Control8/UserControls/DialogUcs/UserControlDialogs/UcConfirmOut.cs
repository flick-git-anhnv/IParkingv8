using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Users;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.QRScreenController;
using Kztek.Cameras;
using Kztek.Control8.Forms;
using Kztek.Tool;
using System.Data;

namespace Kztek.Control8.UserControls.DialogUcs.UserControlDialogs
{
    public partial class UcConfirmOut : UserControl, IDialog<ConfirmOutRequest, ConfirmOutResult>, KzITranslate
    {
        #region PROPERTIES
        //[BASE]
        private TaskCompletionSource<bool>? tcs;
        private int countDown = 0;
        private readonly Form dialogHost = new();
        private readonly System.Timers.Timer timerAutoConfirm = new();
        private readonly bool isAllowUseVoucher = false;
        private readonly bool isAllowChangeCollection = false;
        private readonly IQRDevice qRViewDevice;
        private readonly IAPIServer ApiServer;
        private readonly List<Collection> collections;

        public LaneOptionalConfig LaneOptionalConfig { get; set; }

        //[INPUT]
        private List<string> appliedVouchers = [];
        private string? currentCollectionId = null;

        //[OUTPUT]
        private bool isChangeAccessKeyGroup = false;
        private bool IsPayByQR = false;

        public ExitData? eventOut = default;
        public string updatePlate = string.Empty;

        //[SUB - CONTROLS]
        private Form maskedForm;
        private readonly UcSelectVouchers ucVouchers;
        private readonly UcSelectAccessKeyCollection ucAccessKeyCollections;
        #endregion End PROPERTIES

        #region CONSTRUCTOR
        public UcConfirmOut(IAPIServer apiServer, List<Collection> dailyCollections,
                            LaneOptionalConfig laneOptionalConfig, IQRDevice qRViewDevice, User? user, Image? defaultImage)
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.ApiServer = apiServer;
            this.collections = dailyCollections;
            this.LaneOptionalConfig = laneOptionalConfig;
            this.qRViewDevice = qRViewDevice;
            this.isAllowUseVoucher = (user?.Rights?.Contains("entities/discount/objects/*/privileges/write_discount") ?? false) ||
                                     (user?.screenFeatures?.Contains("screens/entry_exit_system_operation/features/apply_voucher") ?? false);
            if (user is null || user.screenFeatures is null || user.screenFeatures.Count == 0)
            {
                this.isAllowChangeCollection = true;
            }
            else
                this.isAllowChangeCollection = user.screenFeatures.Contains("screens/entry_exit_system_operation/features/change_access_key_collection");

            picVehicleIn.KZUI_DefaultImage = defaultImage;
            picPanoramaIn.KZUI_DefaultImage = defaultImage;
            picPanoramaOut.KZUI_DefaultImage = defaultImage;
            picVehicleOut.KZUI_DefaultImage = defaultImage;

            btnChangeAccessKeyCollection.Visible = this.isAllowChangeCollection;
            btnPayQR.Visible = !string.IsNullOrEmpty(this.LaneOptionalConfig.StaticQRComport);

            countDown = laneOptionalConfig.AutoRejectDialogTime;

            btnCancel.Click += BtnCancel_Click;
            btnConfirm.Click += BtnConfirm_Click;
            btnVoucher.Click += BtnVoucher_Click;
            btnPayQR.Click += BtnPayQR_Click;
            btnChangeAccessKeyCollection.Click += BtnChangeCollectionType_Click;

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

            ucVouchers = new UcSelectVouchers
            {
                TargetControl = this,
                BorderRadius = 24,
            };

            ucAccessKeyCollections = new UcSelectAccessKeyCollection
            {
                TargetControl = this,
                BorderRadius = 24,
            };
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

        #region CONTROLS IN FORM
        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            var frm = new frmViewImage
            {
                CurrentImage = (Image)((PictureBox)sender).Image.Clone(),
            };
            frm.Show(this);
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            StopAndRefreshTimer();
            txtDetectPlate.Text = lblAlarmPlate.Text;
            this.ActiveControl = txtDetectPlate;
            txtDetectPlate.SelectionStart = txtDetectPlate.Text.Length;
            txtDetectPlate.SelectionLength = 0;
        }
        private void BtnPayQR_Click(object? sender, EventArgs e)
        {
            StopAndRefreshTimer();

            dialogHost.Visible = false;

            var frm = new FrmConfirmQRView(this.eventOut, this.qRViewDevice, this.LaneOptionalConfig);
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(
                  maskedForm.Left + (maskedForm.Width - frm.Width) / 2,
                  maskedForm.Top + (maskedForm.Height - frm.Height) / 2
              );
            frm.TopMost = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IsPayByQR = true;
                btnConfirm.PerformClick();
            }
            else
            {
                dialogHost.Visible = true;
            }
        }
        private async void BtnVoucher_Click(object? sender, EventArgs e)
        {
            try
            {
                btnConfirm.Enabled = false;
                btnVoucher.Enabled = false;

                StopAndRefreshTimer();

                var voucherList = await ApiServer.PaymentService.GetVoucherDataAsync(this.eventOut.CollecionID);
                if (voucherList is null || voucherList.Count == 0 ||
                    this.eventOut?.AccessKey is null || this.eventOut?.Entry is null)
                {
                    lblResult1.Text = KZUIStyles.CurrentResources.VoucherlistEmpty;
                    return;
                }

                var selectedVoucher = await ucVouchers.SelectVoucherAsync(voucherList, this.LaneOptionalConfig);
                if (selectedVoucher is null)
                {
                    return;
                }
                string voucherCode = selectedVoucher.GetCode();
                var applyVoucherResult = (await ApiServer.PaymentService.ApplyVoucherExitAsync(voucherCode, this.eventOut.AccessKey.Code));

                //Kiểm tra mất kết nối tới máy chủ
                if (applyVoucherResult == null || (applyVoucherResult.Item1 == null && applyVoucherResult.Item2 == null))
                {
                    lblResult1.Text = KZUIStyles.CurrentResources.VoucherApplyError;
                    btnConfirm.Enabled = btnVoucher.Enabled = true;
                    return;
                }

                //Có kết nối đến máy chủ, kiểm tra kết quả áp dụng có thành công hay không
                var vcDetail = applyVoucherResult.Item1;
                var errorData = applyVoucherResult.Item2;
                if (errorData != null)
                {
                    //Áp dụng không thành công, nhưng ko lấy được thông tin lỗi
                    if (errorData.Fields == null || errorData.Fields.Count == 0)
                    {
                        lblResult1.Text = KZUIStyles.CurrentResources.VoucherApplyError;
                        btnConfirm.Enabled = btnVoucher.Enabled = true;
                        return;
                    }

                    string errorMessage = errorData.ToString();
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        lblResult1.Text = KZUIStyles.CurrentResources.VoucherApplyError;
                        btnConfirm.Enabled = btnVoucher.Enabled = true;
                        return;
                    }

                    lblResult1.Invoke(new Action(() =>
                    {
                        lblResult1.Text = errorMessage;
                        lblResult1.Refresh();
                        btnConfirm.Enabled = btnVoucher.Enabled = true;
                    }));
                    return;
                }

                this.appliedVouchers.Add(vcDetail!.VoucherTypeName);
                this.eventOut.DiscountAmount += vcDetail.Amount;
                var fee = this.eventOut.Amount - this.eventOut.DiscountAmount - this.eventOut.Entry.Amount;
                fee = Math.Max(fee, 0);
                lblParkingFee.Text = TextFormatingTool.GetMoneyFormat(fee.ToString());

                btnPayQR.Enabled = btnVoucher.Enabled = fee > 0;
                lblVoucherTitle.Visible = lblVoucher.Visible = true;
                lblVoucher.Text = string.Join("\r\n", appliedVouchers);
                lblVoucher.Location = new Point(lblVoucherTitle.Location.X + lblVoucherTitle.Width + 8,
                                                lblVoucherTitle.Location.Y + (lblVoucherTitle.Height - lblVoucher.Height) / 2);
            }
            catch (Exception)
            {
                lblResult1.Text = KZUIStyles.CurrentResources.VoucherApplyError;
                btnConfirm.Enabled = btnVoucher.Enabled = true;
            }
            finally
            {
                btnConfirm.Enabled = true;
            }
        }
        private async void BtnChangeCollectionType_Click(object? sender, EventArgs e)
        {
            try
            {
                StopAndRefreshTimer();
                btnConfirm.Enabled = false;
                var selectedCollection = await ucAccessKeyCollections.SelectCollectionAsync(collections, this.LaneOptionalConfig);
                if (selectedCollection is null)
                {
                    return;
                }

                string selectedCollectionId = selectedCollection.Id;

                string notePlate = string.IsNullOrEmpty(this.eventOut!.PlateNumber) ? this.eventOut.Entry.PlateNumber :
                                                                                      this.eventOut.PlateNumber;
                isChangeAccessKeyGroup = true;
                var newInfo = await ApiServer.OperatorService.Exit.ChangeCollectionAsync(this.eventOut.Id, selectedCollectionId);
                if (newInfo != null)
                {
                    this.appliedVouchers = [];
                    lblVoucher.Text = "_";
                    this.eventOut = newInfo;
                    lblAccessKeyGroupName.Text = selectedCollection.Name;

                    long fee = eventOut.Amount - eventOut.DiscountAmount - eventOut.Entry.Amount;
                    fee = Math.Max(fee, 0);
                    lblParkingFee.Text = TextFormatingTool.GetMoneyFormat(fee.ToString());
                    btnVoucher.Visible = fee > 0;

                    btnVoucher.Visible = btnVoucher.Enabled = fee > 0 && isAllowUseVoucher;
                    btnPayQR.Visible = fee > 0 && !string.IsNullOrEmpty(this.LaneOptionalConfig.StaticQRComport);
                }
                else
                {
                    lblResult1.Text = KZUIStyles.CurrentResources.ChangeCollectionError;
                    return;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                btnConfirm.Enabled = true;
            }

        }
        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            StopAndRefreshTimer();
            updatePlate = txtDetectPlate.Text;
            tcs?.TrySetResult(true);
        }
        private async void BtnCancel_Click(object? sender, EventArgs e)
        {
            StopAndRefreshTimer();
            if (isChangeAccessKeyGroup)
            {
                await ApiServer.OperatorService.Exit.ChangeCollectionAsync(this.eventOut!.Id, this.currentCollectionId);
            }
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
        #endregion End CONTROLS IN FORM

        #region Public function
        public async Task<ConfirmOutResult> ShowDialog(ConfirmOutRequest request, Form maskedForm)
        {
            Translate();
            this.maskedForm = maskedForm;

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

            ClearView();

            this.eventOut = request.EventOut!;
            this.currentCollectionId = eventOut.CollecionID;
            this.updatePlate = request.DetectedPlate;

            lblPlateAlarmTitle.Text = request.AbnormalCode == EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM ?
                                                    KZUIStyles.CurrentResources.VehicleCode :
                                                    KZUIStyles.CurrentResources.PlateIn;

            var entry = this.eventOut.Entry!;
            long fee = this.eventOut.Amount - this.eventOut.DiscountAmount - entry.Amount;
            fee = Math.Max(fee, 0);

            btnPayQR.Visible = !string.IsNullOrEmpty(this.LaneOptionalConfig.StaticQRComport);
            btnVoucher.Enabled = true;
            btnVoucher.Visible = fee > 0 && isAllowUseVoucher;

            lblMessage.Text = request.ErrorMessage.Trim();

            var panoramaImageIn = request.EntryImages.ContainsKey(EmImageType.PANORAMA) ? request.EntryImages[EmImageType.PANORAMA] : null;
            var vehicleImageIn = request.EntryImages.ContainsKey(EmImageType.VEHICLE) ? request.EntryImages[EmImageType.VEHICLE] : null;
            var faceImageIn = request.EntryImages.ContainsKey(EmImageType.FACE) ? request.EntryImages[EmImageType.FACE] : null;
            var plateImageIn = request.EntryImages.ContainsKey(EmImageType.PLATE_NUMBER) ? request.EntryImages[EmImageType.PLATE_NUMBER] : null;

            if (panoramaImageIn is null)
                picPanoramaIn.Visible = false;
            else
                picPanoramaIn.KZUI_Image = panoramaImageIn;

            if (vehicleImageIn is null)
                picVehicleIn.Visible = false;
            else
                picVehicleIn.KZUI_Image = vehicleImageIn;

            if (faceImageIn is null)
                picFaceIn.Visible = false;
            else
                picFaceIn.KZUI_Image = faceImageIn;

            if (plateImageIn is null)
                picPlateIn.Visible = false;
            else
                picPlateIn.KZUI_Image = plateImageIn;

            //--OUT
            var panoramaImageOut = request.ExitImages.ContainsKey(EmImageType.PANORAMA) ? request.ExitImages[EmImageType.PANORAMA] : null;
            var vehicleImageOut = request.ExitImages.ContainsKey(EmImageType.VEHICLE) ? request.ExitImages[EmImageType.VEHICLE] : null;
            var faceImageOut = request.ExitImages.ContainsKey(EmImageType.FACE) ? request.ExitImages[EmImageType.FACE] : null;
            var plateImageOut = request.ExitImages.ContainsKey(EmImageType.PLATE_NUMBER) ? request.ExitImages[EmImageType.PLATE_NUMBER] : null;

            if (panoramaImageOut is null)
                picPanoramaOut.Visible = false;
            else
                picPanoramaOut.KZUI_Image = panoramaImageOut;

            if (vehicleImageOut is null)
                picVehicleOut.Visible = false;
            else
                picVehicleOut.KZUI_Image = vehicleImageOut;

            if (faceImageOut is null)
                picFaceOut.Visible = false;
            else
                picFaceOut.KZUI_Image = faceImageOut;

            if (plateImageOut is null)
                picPlateOut.Visible = false;
            else
                picPlateOut.KZUI_Image = plateImageOut;

            try
            {
                var ParkingTime = eventOut.DatetimeOut - entry.DateTimeIn;
                string formattedTime = "";
                if (ParkingTime.TotalDays > 1)
                {
                    formattedTime = string.Format("{0} ngày {1} giờ {2} phút {3} giây", ParkingTime.Days, ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
                }
                else
                {
                    formattedTime = string.Format("{0} giờ {1} phút {2} giây", ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
                }

                string accessKeyName = eventOut.AccessKey?.Name ?? string.Empty;
                string accessKeyCode = eventOut.AccessKey?.Code ?? string.Empty;

                txtDetectPlate.Text = request.DetectedPlate;
                lblTimeOut.Text = eventOut.DatetimeOut.ToVNTime();
                lblParkingTime.Text = formattedTime;
                lblAccessKeyName.Text = $"{accessKeyName} | {accessKeyCode}";

                lblAlarmPlate.Text = request.AlarmPlate;
                lblTimeIn.Text = entry.DateTimeIn.ToVNTime();
                lblParkingFee.Text = TextFormatingTool.GetMoneyFormat(fee.ToString());
                lblAccessKeyGroupName.Text = eventOut.CollecionName;


                this.appliedVouchers = [.. request.VoucherApplies.Select(e => e.voucherTypeName ?? "")];
                if (this.appliedVouchers.Count > 0)
                {
                    lblVoucherTitle.Visible = lblVoucher.Visible = true;
                    lblVoucher.Text = string.Join("\r\n", appliedVouchers);
                }
                else
                {
                    lblVoucherTitle.Visible = lblVoucher.Visible = false;
                    lblVoucher.Text = "_";
                }
                lblVoucher.Location = new Point(lblVoucherTitle.Location.X + lblVoucherTitle.Width + 8,
                                                lblVoucherTitle.Location.Y + (lblVoucherTitle.Height - lblVoucher.Height) / 2);
                this.Visible = true;
                this.Focus();
                dialogHost.Show(maskedForm);
                countDown = LaneOptionalConfig.AutoRejectDialogTime;
                if (LaneOptionalConfig.IsUseAlarmDialog && countDown > 0)
                {
                    timerAutoConfirm.Start();
                    lblCountDown.Visible = true;
                }
                else
                {
                    lblCountDown.Visible = false;
                }

                this.SuspendLayout();
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

                var result = new ConfirmOutResult()
                {
                    IsConfirm = isConfirm,
                    UpdatePlate = this.updatePlate.ToUpper(),
                    ExitData = this.eventOut,
                    IsPayByQR = this.IsPayByQR
                };

                timerAutoConfirm.Stop();
                dialogHost.Hide();
                this.Visible = false;
                this.ResumeLayout(false);
                dialogHost.ResumeLayout(false);
                maskedForm.ResumeLayout(false);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
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

        #region Private function
        private void ClearView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    ClearView();
                }));
                return;
            }
            picPanoramaIn.KZUI_Image = null;
            picVehicleIn.KZUI_Image = null;

            picPanoramaOut.KZUI_Image = null;
            picVehicleOut.KZUI_Image = null;

            txtDetectPlate.Text = string.Empty;
            lblAlarmPlate.Text = string.Empty;

            lblTimeOut.Text = string.Empty;
            lblTimeIn.Text = string.Empty;
            lblParkingTime.Text = string.Empty;
            lblParkingFee.Text = "0";

            lblAccessKeyName.Text = string.Empty;
            lblAccessKeyGroupName.Text = string.Empty;
            lblVoucher.Text = string.Empty;
            lblResult1.Text = string.Empty;

            eventOut = null;
            updatePlate = string.Empty;

            appliedVouchers = [];
            currentCollectionId = string.Empty;
            isChangeAccessKeyGroup = false;
        }

        private void StopAndRefreshTimer()
        {
            timerAutoConfirm.Stop();
            countDown = this.LaneOptionalConfig.AutoRejectDialogTime;
            lblCountDown.Visible = false;
        }
        public void SetImageSafe(PictureBox pb, Image? newImage)
        {
            if (pb == null) return;
            if (pb.InvokeRequired)
            {
                pb.Invoke(new Action(() =>
                {
                    var old = pb.Image;
                    // Gắn ảnh mới trước, sau đó gỡ & dispose ảnh cũ để tránh đang dùng.
                    pb.Image = newImage.ResizeImage(pb.Width, pb.Height);
                    if (old != null && !ReferenceEquals(old, newImage))
                    {
                        // Tránh dispose ảnh đang share ở nơi khác: chỉ dispose nếu bạn đã clone riêng cho PictureBox này.
                        try { old.Dispose(); } catch { /* ignore */ }
                    }
                }
                ));
                return;
            }

            var old = pb.Image;
            // Gắn ảnh mới trước, sau đó gỡ & dispose ảnh cũ để tránh đang dùng.
            pb.Image = newImage.ResizeImage(pb.Width, pb.Height);
            if (old != null && !ReferenceEquals(old, newImage))
            {
                // Tránh dispose ảnh đang share ở nơi khác: chỉ dispose nếu bạn đã clone riêng cho PictureBox này.
                try { old.Dispose(); } catch { /* ignore */ }
            }
        }
        #endregion

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            picPanoramaIn.KZUI_Title = KZUIStyles.CurrentResources.PicPanoramaIn;
            picVehicleIn.KZUI_Title = KZUIStyles.CurrentResources.PicVehicleIn;
            picPanoramaOut.KZUI_Title = KZUIStyles.CurrentResources.PicPanoramaOut;
            picVehicleOut.KZUI_Title = KZUIStyles.CurrentResources.PicVehicleOut;

            lblDetectPlateTitle.Text = KZUIStyles.CurrentResources.PlateDetected;
            lblPlateAlarmTitle.Text = KZUIStyles.CurrentResources.PlateIn;
            lblTimeOutTitle.Text = KZUIStyles.CurrentResources.TimeOut;
            lblTimeInTitle.Text = KZUIStyles.CurrentResources.TimeIn;
            lblParkingTimeTitle.Text = KZUIStyles.CurrentResources.Duration;
            lblParkingFeeTitle.Text = KZUIStyles.CurrentResources.Fee;

            lblAccessKeyNameTitle.Text = KZUIStyles.CurrentResources.AccesskeyName;
            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;

            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnChangeAccessKeyCollection.Text = KZUIStyles.CurrentResources.ChangCollection;
            if (btnChangeAccessKeyCollection.Width < btnChangeAccessKeyCollection.PreferredSize.Width)
            {
                btnChangeAccessKeyCollection.Width = btnChangeAccessKeyCollection.PreferredSize.Width;
            }

            lblVoucherTitle.Text = KZUIStyles.CurrentResources.Voucher;
            lblShortcutGuide1Line.Text = KZUIStyles.CurrentResources.ShortCutGuide1Line;

        }
        private void Responsive()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Responsive);
                return;
            }

            //int padding = 8;
          
            //txtDetectPlate.Width = (panelEventInfo.Width - padding) / 2;
            //lblAlarmPlate.Width = panelEventInfo.Width - btnCopy.Width - padding - txtDetectPlate.Width - padding;

            //lblTimeOut.Width = lblTimeIn.Width =
            //lblParkingTime.Width = lblParkingFee.Width =
            //lblAccessKeyName.Width = lblAccessKeyGroupName.Width =
            //txtDetectPlate.Width;

            //picVehicleIn.Location = new Point(picPanoramaIn.Right + padding, picPanoramaIn.Location.Y);
            //picPanoramaOut.Location = new Point(picPanoramaIn.Location.X, picPanoramaIn.Bottom + padding);
            //picVehicleOut.Location = new Point(picVehicleIn.Location.X, picPanoramaOut.Location.Y);

            //lblPlateAlarmTitle.Location = new Point(picVehicleIn.Location.X, lblPlateAlarmTitle.Location.Y);
            //lblAlarmPlate.Location = new Point(picVehicleIn.Location.X, lblAlarmPlate.Location.Y);
            //btnCopy.Location = new Point(lblAlarmPlate.Right + padding, btnCopy.Location.Y);

            //lblTimeInTitle.Location = new Point(picVehicleIn.Location.X, lblTimeInTitle.Location.Y);
            //lblTimeIn.Location = new Point(picVehicleIn.Location.X, lblTimeIn.Location.Y);

            //lblParkingFeeTitle.Location = new Point(picVehicleIn.Location.X, lblParkingFeeTitle.Location.Y);
            //lblParkingFee.Location = new Point(picVehicleIn.Location.X, lblParkingFee.Location.Y);
            //lblAccessKeyGroupName.Location = new Point(picVehicleIn.Location.X, lblAccessKeyGroupName.Location.Y);
        }
    }
}