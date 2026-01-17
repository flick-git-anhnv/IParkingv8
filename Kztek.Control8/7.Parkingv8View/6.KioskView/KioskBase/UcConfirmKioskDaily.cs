using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Tool;

namespace Kztek.Control8.BaseKiosk
{
    public partial class UcConfirmKioskDaily : UserControl, IKioskNotifyDialog<KioskDialogDialyRequest, BaseDialogResult?>, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<bool>? tcs;
        private int closeTime = 0;
        public LaneOptionalConfig LaneOptionalConfig { get; set; }
        KioskDialogDialyRequest request;
        const int CLOSE_TIME = 15;
        #endregion

        #region Forms
        public UcConfirmKioskDaily()
        {
            InitializeComponent();
            DoubleBuffered = true;
            btnBack.Click += BtnBack_Click;
        }
        #endregion

        #region Controls In Form
        private void BtnBack_Click(object? sender, EventArgs e)
        {
            CloseDialogInternal();
        }
        #endregion

        #region Public Function
        public async Task<BaseDialogResult?> ShowDialog(KioskDialogDialyRequest request, Form? masked = null)
        {
            try
            {
                this.request = request;
                ClearView();
                Translate();
                DisplayEventInfo(request);
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
            CloseDialogInternal();
        }
        #endregion

        #region Private Function
        private void ClearView()
        {
            this.Invoke(this.SuspendLayout);

            picPanorama.SetImage(null);
            picVehicle.SetImage(null);

            lblDetectedPlate.SetText(KzDictionary.EMPTY_STRING);
            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblAccessKeyCollection.SetText(KzDictionary.EMPTY_STRING);
            lblDetectedPlate.SetText(KzDictionary.EMPTY_STRING);

            lblTimeToMain.SetText(KzDictionary.EMPTY_STRING);
            lblTitle.SetText(KzDictionary.EMPTY_STRING);
            lblSubTitle.SetText(KzDictionary.EMPTY_STRING);
            btnBack.SetText(KzDictionary.EMPTY_STRING);

            this.Invoke(this.ResumeLayout);
        }
        private void DisplayEventInfo(KioskDialogDialyRequest request)
        {
            lblTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.TitleTag));
            lblSubTitle.SetText(UIBuiltInResourcesHelper.GetValue(request.SubTitleTag));

            lblTimeToMain.SetText(string.Empty);
            btnBack.SetText(KZUIStyles.CurrentResources.BackToMain);

            lblAccessKeyName.SetText(request.AccessKey?.Name ?? KzDictionary.EMPTY_STRING);
            lblAccessKeyCollection.SetText(request.AccessKey?.Collection?.Name ?? KzDictionary.EMPTY_STRING);
            lblDetectedPlate.SetText(string.IsNullOrEmpty(request.DetectedPlate) ? KzDictionary.EMPTY_STRING : request.DetectedPlate);

            picPanorama.SetImage(request.PanoramaImage);
            picVehicle.SetImage(request.VehicleImage);
        }
        private void CenterControlLocation()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CenterControlLocation();
                }));
                return;
            }
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            uiLine1.Left = (this.ClientSize.Width - uiLine1.Width) / 2;
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
        }
        public void Translate()
        {
            lblTitle.SetText(UIBuiltInResourcesHelper.GetValue(this.request?.TitleTag ?? ""));
            lblSubTitle.SetText(UIBuiltInResourcesHelper.GetValue(this.request?.SubTitleTag ?? ""));

            lblAccessKeyNameTitle.SetText(KZUIStyles.CurrentResources.AccesskeyName);
            lblAccessKeyCollectionTitle.SetText(KZUIStyles.CurrentResources.AccessKeyCollection);
            lblDetectedPlateTitle.SetText(KZUIStyles.CurrentResources.PlateDetected);

            lblAccessKeyNameTitle.SetSize(lblAccessKeyNameTitle.PreferredWidth, lblAccessKeyName.Height);
            lblAccessKeyCollectionTitle.SetSize(lblAccessKeyCollectionTitle.PreferredWidth, lblAccessKeyCollectionTitle.Height);
            lblDetectedPlateTitle.SetSize(lblDetectedPlateTitle.PreferredWidth, lblDetectedPlateTitle.Height);

            btnBack.SetText(KZUIStyles.CurrentResources.BackToMain);

            CenterControlLocation();
        }

        private void CloseDialogInternal()
        {
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
                lblTimeToMain.Refresh();
                timerAutoClose.Enabled = true;
            }
            else
            {
                CloseDialogInternal();
            }
        }
        #endregion
    }
}
