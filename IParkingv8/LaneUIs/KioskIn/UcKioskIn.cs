using iParkingv5.Controller.VoucherDevices;
using iParkingv5.Objects.Events;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.RabbitMQ;
using IParkingv8.Forms.SystemForms;
using IParkingv8.Helpers;
using Kztek.Control8.Forms;
using Kztek.Control8.KioskBase;
using Kztek.Control8.KioskIn.ConfirmOpenBarrie;
using Kztek.Control8.KioskIn.ConfirmPlate;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.ucDataGridViewInfo;
using Kztek.Tool;

namespace IParkingv8.LaneUIs.KioskIn
{
    public partial class UcKioskIn : UserControl, IKioskInView
    {
        public bool IsBusy { get; set; }

        public readonly Image? defaultImage = ImageHelper.Base64ToImage(AppData.DefaultImageBase64);
        public KioskInPresenter presenter;
        private UcLoading loadingView;
        public UcSelectVehicles ucSelectVehicles { get; set; }

        #region Properties
        public KZUI_UcLaneTitle UcLaneTitle { get; set; } = new KZUI_UcLaneTitle();
        public KZUI_UcCameraList UcCameraList { get; set; } = new KZUI_UcCameraList();
        public KZUI_UcResult UcResult { get; set; } = new KZUI_UcResult();
        public IKZUI_UcPlate UcPlateIn { get; set; } = new KZUI_UcPlateVertical();
        public IDataInfo ucEventInfoNew { get; set; }
        public KZUI_Function UcAppFunctions { get; set; } = new KZUI_Function();

        #region Config
        public LaneDisplayConfig laneDisplayConfig;
        public LaneOptionalConfig? laneOptionalConfig = new();
        #endregion

        //OTHER
        public List<CardEventArgs> LastCardEventDatas { get; set; } = [];
        public List<InputEventArgs> LastInputEventDatas { get; set; } = [];

        public Lane Lane { get; set; }

        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new(1, 1);
        public readonly SemaphoreSlim semaphoreSlimOnKeyPress = new(1, 1);

        public event OnChangeLaneEvent? OnChangeLaneEvent;
        #endregion

        #region Forms
        public UcKioskIn(Lane lane)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Lane = lane;

            InitUI();
            Translate();
            InitPresenter();

            Task.Run(new Action(() =>
            {
                var voucherController = VoucherDeviceFactory.CreateController((EmKioskControllerType)AppData.PaymentKioskConfig.VoucherDeviceType);
                voucherController.Connect(AppData.PaymentKioskConfig.VoucherComport, AppData.PaymentKioskConfig.VoucherBaudrate, 0);

                if (AppData.PaymentKioskConfig.IsUseVoucher)
                {
                    voucherController.PollingStart();
                }
                voucherController.CardEvent += VoucherController_CardEvent;
                LedHelper.DisplayDefaultLed(this.Lane.Id);
            }));

            this.SizeChanged += UcKioskIn_SizeChanged;
        }
        private void UcKioskIn_SizeChanged(object? sender, EventArgs e)
        {
            this.SuspendLayout();
            this.panelDashboard.SuspendLayout();
            this.panelDialog.SuspendLayout();
            this.panelDashboard.Location = new Point((this.ClientSize.Width - ucKioskInDashboard1.Width) / 2, ucKioskTitle1.Height);
            this.panelDialog.Location = this.panelDashboard.Location;
            this.panelDialog.ResumeLayout();
            this.panelDashboard.ResumeLayout();
            this.ResumeLayout();
        }
        #endregion

        #region Khởi Tạo Làn
        public void Translate()
        {
            this.ucKioskInDashboard1.Translate();
            this.ucDailyNotify.Translate();
            this.ucMonthlyNotify.Translate();
            this.presenter?.Translate();
        }
        public void InitUI()
        {
            loadingView = new UcLoading()
            {
                TargetControl = this,
                BorderRadius = 24
            };
            ucSelectVehicles = new UcSelectVehicles(true)
            {
                TargetControl = this,
                BorderRadius = 24,
            };

            ucDailyNotify = new() { Visible = false };
            ucMonthlyNotify = new() { Visible = false };

            panelDialog.Controls.Add(ucDailyNotify);
            panelDialog.Controls.Add(ucMonthlyNotify);

            this.ucDailyNotify.Size = this.ucKioskInDashboard1.Size;
            this.ucMonthlyNotify.Size = this.ucKioskInDashboard1.Size;

            this.ucDailyNotify.Location = this.ucKioskInDashboard1.Location;
            this.ucMonthlyNotify.Location = this.ucKioskInDashboard1.Location;

            this.ucKioskTitle1.Init(defaultImage, UcKioskTitle1_OnLanguageChangedEvent, OpenSettingPage);
            UcCameraList.Init(this, AppData.Cameras, AppData.AppConfig.IsUseVirtualLoop,
                              AppData.AppConfig.VirtualLoopMode, AppData.AppConfig.MotionAlarmLevel, AppData.AppConfig.cameraSDk);
            ucKioskInDashboard1.Init(this.Lane, BtnRetakeImage_Click);
        }
        private void InitPresenter()
        {
            var dailyDialogPresenter = new ConfirmOpenBarrieKioskInPresenter(ucDalilyDialog);
            var monthlyDialogPresenter = new ConfirmPlateMonthlyKioskInPresenter(this.ucMonthlyDialog);

            var basePresenter = new KioskInBasePresenter(this, AppData.ApiServer, dailyDialogPresenter, monthlyDialogPresenter);
            var kioskInCardPresenter = new KioskInCardPresenter(basePresenter);
            var kioskInLoopPresenter = new KioskInLoopPresenter(basePresenter);

            this.presenter = new KioskInPresenter(this, basePresenter, kioskInCardPresenter, kioskInLoopPresenter);
        }
        #endregion

        #region Đóng Làn
        public void Stop()
        {
            UcCameraList.Stop();
        }
        #endregion

        #region Controls In Forms
        private void BtnRetakeImage_Click(object? sender, EventArgs e)
        {
            _ = OnNewEvent(new InputEventArgs()
            {
                InputIndex = 1
            });
            //_ = OnNewEvent(new CardEventArgs()
            //{
            //    ReaderIndex = 1,
            //    PreferCard = "4EC0176B"
            //});
        }
        private async void OpenSettingPage(object? sender, EventArgs e)
        {
            this.presenter.OpenSettingPage();
        }
        public void OpenSettingPage()
        {
            new FrmLaneSetting(this, AppData.Leds, AppData.Cameras, this.Lane.ControlUnits, true, this.laneOptionalConfig!, EmControlSizeMode.MEDIUM, this.Size).ShowDialog();
        }
        private void UcKioskTitle1_OnLanguageChangedEvent()
        {
            Translate();
        }
        #endregion

        #region Event
        public async void OnKeyPress(Keys keys)
        {
            await Task.Delay(1);
            return;
        }
        public async Task OnNewEvent(EventArgs e)
        {
            await semaphoreSlimOnNewEvent.WaitAsync();
            try
            {
                if (e is CardOnRFEventArgs onRFEventArgs)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Card On RFEvent", onRFEventArgs));
                    await this.presenter.CardOnRFEventArgs(onRFEventArgs);
                }
                if (e is CardEventArgs cardEvent)
                {
                    ucKioskTitle1.NotifyCardEvent();
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Card Event", cardEvent));
                    await this.presenter.OnNewCardEvent(cardEvent);
                }
                else if (e is InputEventArgs inputEvent)
                {
                    ucKioskTitle1.NotifyLoopEvent();
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Input Event", inputEvent));
                    await this.presenter.ExecuteInputEvent(inputEvent);
                }
                else if (e is CardBeTakenEventArgs cardBeTakenEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"OnNewEventIn Type = Cancel"));
                    await this.presenter.ExcecuteCardbeTaken(cardBeTakenEvent);
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("OnNewEventOut Error", ex));
            }
            finally
            {
                ucKioskTitle1.NotifyNoneEvent();
                semaphoreSlimOnNewEvent.Release();
            }
        }

        /// <summary>
        /// Sử dụng cho máy nhả thẻ có nút yêu cầu hỗ trợ
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task OnNewStatus(EventArgs e)
        {
            await Task.Delay(1);
            return;
        }
        private void VoucherController_CardEvent(object sender, CardEventArgs e)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New QR500 Card Event", e.PreferCard));
            this.Invoke(new Action(() =>
            {
                e.ReaderIndex = 1;
                _ = OnNewEvent(e);
            }));
        }
        #endregion

        #region Server Request
        //Nhận được yêu cầu gửi lại message gần nhất từ server
        public void NotifyLastMessage()
        {
            this.presenter.NotifyLastMessage();
        }
        //Nhận được kết quả xác nhận từ server
        public void ApplyConfirmResult(EventRequest eventRequest)
        {
            this.presenter.ApplyConfirmResult(eventRequest);
        }
        #endregion

        public void OpenHomePage()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    OpenHomePage();
                }));
                return;
            }
            panelDialog.Visible = false;
            panelDashboard.Visible = true;
            ucDailyNotify.CloseDialog();
            ucMonthlyNotify.CloseDialog();
        }

        public void OpenDialogPage()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    OpenDialogPage();
                }));
                return;
            }
            panelDialog.Visible = true;
            panelDashboard.Visible = false;
        }
        public async Task ShowDailyNotifyDialog(KioskDialogDialyRequest request)
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                await this.Invoke(async () => await ShowDailyNotifyDialog(request));
                return;
            }
            await this.ucDailyNotify.ShowDialog(request);
        }
        public async Task ShowMonthlyNotifyDialog(KioskDialogMonthlyRequest request)
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                await this.Invoke(async () => await ShowMonthlyNotifyDialog(request));
                
                return;
            }
            await this.ucMonthlyNotify.ShowDialog(request);
        }

        public void HideLoadingIndicator()
        {
            this.Invoke(new Action(() =>
            {
                this.loadingView.HideLoadingIndicator();
            }));
        }
        public void ShowLoadingIndicator(string title, string subTitle)
        {
            this.Invoke(new Action(() =>
            {
                this.loadingView.ShowLoadingIndicator(title, subTitle);
            }));
        }
        public void UpdateLoadingIndicator(string title, string subTitle)
        {
            this.Invoke(new Action(() =>
            {
                loadingView.UpdateLoadingIndicator(title, subTitle);
            }));
        }

        #region Hàm không sử dụng
        public void ChangeLaneDirectionConfig(LaneDirectionConfig config) { }
        public virtual void LoadViewSetting(LaneDisplayConfig config)
        {
            this.laneDisplayConfig = config;
        }
        public virtual LaneDisplayConfig GetLaneDisplayConfig()
        {
            return new LaneDisplayConfig();
        }
        public void StopTimerCheckAllowOpenBarrie()
        {
        }
        public void StartTimerCheckAllowOpenBarrie()
        {
        }
        #endregion
    }
}
