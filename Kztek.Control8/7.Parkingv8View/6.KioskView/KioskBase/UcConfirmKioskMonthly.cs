using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Tool;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kztek.Control8.BaseKiosk
{
    public partial class UcConfirmKioskMonthly : UserControl, IKioskNotifyDialog<KioskDialogMonthlyRequest, BaseDialogResult?>
    {
        #region Properties
        private TaskCompletionSource<bool>? tcs;
        private int closeTime = 0;
        public LaneOptionalConfig LaneOptionalConfig { get; set; }
        private KioskDialogMonthlyRequest request;
        const int CLOSE_TIME = 15;
        #endregion

        #region Forms
        public UcConfirmKioskMonthly()
        {
            InitializeComponent();
            TraceInfo("Init view");
            DoubleBuffered = true;
            btnBack.Click += BtnBack_Click;
        }
        #endregion

        #region Controls In Form
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            TraceInfo("Back click");
            CloseDialogInternal();
        }
        #endregion

        #region Public Function
        public async Task<BaseDialogResult?> ShowDialog(KioskDialogMonthlyRequest request, Form? masked = null)
        {
            try
            {
                this.request = request;

                ClearView();
                Translate();
                DisplayEventInfor(request);
                CenterControlLocation();

                this.Invoke(new Action(() =>
                {
                    this.Visible = true;
                    this.BringToFront();
                }));

                tcs = new TaskCompletionSource<bool>();
                BaseDialogResult baseDialogResult = new();

                StartTimerOpenHomePage();
                baseDialogResult.IsConfirm = await tcs.Task;

                return baseDialogResult;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("frmConfirmKiosk Error", ex));
                return null;
            }
            finally
            {
                CloseDialogInternal();
            }
        }

        public void CloseDialog()
        {
            TraceInfo("Close dialog");
            CloseDialogInternal();
        }
        #endregion

        #region Private Function
        private void ClearView()
        {
            this.Invoke(this.SuspendLayout);

            picPanorama.SetImage(null);
            picVehicle.SetImage(null);

            EnableWarningExpireMode(false);
            lblDetectedPlate.SetText(KzDictionary.EMPTY_STRING);
            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblAccessKeyCollection.SetText(KzDictionary.EMPTY_STRING);

            lblRegisterCustomerName.SetText(KzDictionary.EMPTY_STRING);
            lblRegisterVehicleCode.SetText(KzDictionary.EMPTY_STRING);
            lblExpireDate.SetText(KzDictionary.EMPTY_STRING);

            lblTimeToMain.SetText(KzDictionary.EMPTY_STRING);
            lblTitle.SetText(KzDictionary.EMPTY_STRING);
            lblSubTitle.SetText(KzDictionary.EMPTY_STRING);

            this.Invoke(this.ResumeLayout);
        }
        private void DisplayEventInfor(KioskDialogMonthlyRequest request)
        {
            TraceInfo("Display event infor");
            this.Invoke(this.SuspendLayout);

            Translate();
            picPanorama.SetImage(request.PanoramaImage);
            picVehicle.SetImage(request.VehicleImage);

            lblTimeToMain.SetText(KzDictionary.EMPTY_STRING);
            lblTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.TitleTag));
            lblSubTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.SubTitleTag));

            string accessKeyName = request.AccessKey == null ?
                                        (request.RegisterVehicle?.Name ?? KzDictionary.EMPTY_STRING) :
                                         request.AccessKey.Name ?? KzDictionary.EMPTY_STRING;
            lblAccessKeyName.SetText(accessKeyName);

            string vehicleTypeName = request.AccessKey == null ?
                                            (request.RegisterVehicle?.Collection?.GetVehicleTypeName() ?? KzDictionary.EMPTY_STRING) :
                                            (request.AccessKey.Collection?.GetVehicleTypeName() ?? KzDictionary.EMPTY_STRING);

            string accessKeyGroupName = request.AccessKey == null ?
                                            (request.RegisterVehicle?.Collection?.Name ?? KzDictionary.EMPTY_STRING) :
                                            (request.AccessKey.Collection?.Name ?? KzDictionary.EMPTY_STRING);
            lblAccessKeyCollection.SetText(accessKeyGroupName);

            var registerVehicle = request.AccessKey == null ?
                                    request.RegisterVehicle : request.AccessKey.GetVehicleInfo();
            lblRegisterCustomerName.SetText(registerVehicle?.Customer?.Name ?? KzDictionary.EMPTY_STRING);
            lblRegisterVehicleCode.SetText(registerVehicle?.Code ?? KzDictionary.EMPTY_STRING);
            lblExpireDate.SetText(registerVehicle == null || registerVehicle.ExpireTime == null ? KzDictionary.EMPTY_STRING : registerVehicle.ExpireTime.Value.ToString("dd/MM/yyyy"));

            if (registerVehicle != null && registerVehicle.ExpireTime != null && (registerVehicle.ExpireTime.Value - DateTime.Now).TotalDays <= 10)
            {
                EnableWarningExpireMode(true);
            }

            lblDetectedPlate.SetText(string.IsNullOrEmpty(request.DetectedPlate) ? KzDictionary.EMPTY_STRING : request.DetectedPlate);
            picPanorama.SetImage(request.PanoramaImage);
            picVehicle.SetImage(request.VehicleImage);

            this.Invoke(this.ResumeLayout);
        }

        private void CenterControlLocation()
        {
            TraceInfo("Center control location");
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CenterControlLocation();
                }));
                return;
            }

            TraceInfo("Center control location invoke");
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            uiLine1.Left = (this.ClientSize.Width - uiLine1.Width) / 2;
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
        }

        public void Translate()
        {
            TraceInfo("Change language");
            lblTitle.SetText(UIBuiltInResourcesHelper.GetValue(this.request?.TitleTag ?? ""));
            lblSubTitle.SetText(UIBuiltInResourcesHelper.GetValue(this.request?.SubTitleTag ?? ""));

            lblAccessKeyNameTitle.SetText(KZUIStyles.CurrentResources.AccesskeyName);
            lblAccessKeyNameTitle.SetSize(lblAccessKeyNameTitle.PreferredWidth, lblAccessKeyNameTitle.Height);

            lblAccessKeyCollectionTitle.SetText(KZUIStyles.CurrentResources.AccessKeyCollection);
            lblAccessKeyCollectionTitle.SetSize(lblAccessKeyCollectionTitle.PreferredWidth, lblAccessKeyCollectionTitle.Height);

            lblRegisterCustomerNameTitle.SetText(KZUIStyles.CurrentResources.CustomerName);
            lblRegisterCustomerNameTitle.SetSize(lblRegisterCustomerNameTitle.PreferredWidth, lblRegisterCustomerNameTitle.Height);

            lblRegisterVehicleCodeTitle.SetText(KZUIStyles.CurrentResources.VehicleCode);
            lblRegisterVehicleCodeTitle.SetSize(lblRegisterVehicleCodeTitle.PreferredWidth, lblRegisterVehicleCodeTitle.Height);

            lblExpireDateTitle.SetText(KZUIStyles.CurrentResources.VehicleExpiredDate);
            lblExpireDateTitle.SetSize(lblExpireDateTitle.PreferredWidth, lblExpireDateTitle.Height);

            lblDetectedPlateTitle.SetText(KZUIStyles.CurrentResources.PlateDetected);
            lblDetectedPlateTitle.SetSize(lblDetectedPlateTitle.PreferredWidth, lblDetectedPlateTitle.Height);

            btnBack.SetText(KZUIStyles.CurrentResources.BackToMain);

            if (this.request == null)
            {
                return;
            }
            CenterControlLocation();
        }

        private void EnableWarningExpireMode(bool isEnable)
        {
            if (this.IsHandleCreated && picWarningExpire.InvokeRequired)
            {
                picWarningExpire.Invoke(new Action(() =>
                {
                    picWarningExpire.Visible = isEnable;
                    panelExpireDate.BorderColor = isEnable ? Color.FromArgb(242, 102, 51) : Color.WhiteSmoke;
                    panelExpireDate.BorderThickness = isEnable ? 2 : 0;
                    lblExpireDate.ForeColor = isEnable ? Color.FromArgb(242, 102, 51) : Color.FromArgb(37, 28, 84);
                }));
            }
            else
            {
                picWarningExpire.Visible = isEnable;
                panelExpireDate.BorderColor = isEnable ? Color.FromArgb(242, 102, 51) : Color.WhiteSmoke;
                panelExpireDate.BorderThickness = isEnable ? 2 : 0;
                lblExpireDate.ForeColor = isEnable ? Color.FromArgb(242, 102, 51) : Color.FromArgb(37, 28, 84);
            }
        }

        private void CloseDialogInternal()
        {
            TraceInfo("Close Dialog Invoke");
            StopTimerOpenHomePage();
            this.Invoke(new Action(() =>
            {
                this.Visible = false;
                this.SendToBack();
            }));

            tcs?.TrySetResult(false);
        }
        #endregion

        #region Timer
        private void StopTimerOpenHomePage()
        {
            TraceInfo("Stop timer back to home page");
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
        private void StartTimerOpenHomePage()
        {
            TraceInfo("Start timer back to home page");
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
                TraceInfo($"Update Timer close time {closeTime}");
                lblTimeToMain.Text = $"{KZUIStyles.CurrentResources.AutoCancelAfter} {closeTime}s";
                timerAutoClose.Enabled = true;
            }
            else
            {
                TraceInfo("Timer out of delay, back to home page");
                CloseDialogInternal();
            }
        }
        #endregion

        public static void TraceInfo(string description, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
#if DEBUG
            Trace.TraceInformation($"INFOR: {callerName} - {lineNumber} - {filePath} - {description}");
#endif
        }
    }
}
