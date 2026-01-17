using iParkingv5.Controller.VoucherDevices;
using iParkingv5.Objects.Events;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;
using IParkingv8.Forms.SystemForms;
using IParkingv8.Helpers;
using Kztek.Control8.BaseKiosk;
using Kztek.Control8.Forms;
using Kztek.Control8.KioskBase;
using Kztek.Control8.KioskOut.ConfirmPlatePresenter;
using Kztek.Control8.KioskOut.PaymentPresenter.CASH;
using Kztek.Control8.KioskOut.PaymentPresenter.ConfirmPayment;
using Kztek.Control8.KioskOut.PaymentPresenter.KioskPayment;
using Kztek.Control8.KioskOut.PaymentPresenter.QR;
using Kztek.Control8.KioskOut.PaymentPresenter.VISA;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Control8.UserControls.DialogUcs.KioskOut;
using Kztek.Control8.UserControls.ucDataGridViewInfo;
using Kztek.Tool;

namespace IParkingv8.LaneUIs.KioskOut
{
    public partial class UcKioskOut : UserControl, IKioskOutView
    {
        public bool IsBusy { get; set; }

        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new(1, 1);
        public readonly SemaphoreSlim semaphoreSlimOnKeyPress = new(1, 1);

        public readonly Image? defaultImg = ImageHelper.Base64ToImage(AppData.DefaultImageBase64);
        public KioskOutPresenter presenter;
        private UcLoading loadingView;
        private UcConfirmKioskDaily ucDailyNotify;
        private UcConfirmKioskMonthly ucMonthlyNotify;
        public UcSelectVehicles ucSelectVehicles { get; set; }

        #region Properties
        //Control Define
        public List<Splitter> activeSpliters = [];
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
        public Image? lastVehicleImage = null;
        public AccessKey? lastAccessKey = null;
        public Image? lastOverviewImage = null;
        public List<CardEventArgs> LastCardEventDatas { get; set; } = [];
        public List<InputEventArgs> LastInputEventDatas { get; set; } = [];
        public Lane Lane { get; set; }
        public event OnChangeLaneEvent? OnChangeLaneEvent;

        private ContextMenuStrip contextMenu = new ContextMenuStrip();

        private IVoucherController voucherController;

        #endregion

        #region Forms
        public UcKioskOut(Lane lane)
        {
            InitializeComponent();

            DoubleBuffered = true;
            this.Lane = lane;
            InitUI();
            Translate();
            voucherController = VoucherDeviceFactory.CreateController((EmKioskControllerType)AppData.PaymentKioskConfig.VoucherDeviceType);

            InitPresenter(voucherController);

            Task.Run(new Action(() =>
            {
                voucherController.Connect(AppData.PaymentKioskConfig.VoucherComport, AppData.PaymentKioskConfig.VoucherBaudrate, 0);

                if (AppData.PaymentKioskConfig.IsUseVoucher)
                {
                    voucherController.PollingStart();
                }
                voucherController.CardEvent += VoucherController_CardEvent;
                LedHelper.DisplayDefaultLed(this.Lane.Id);
            }));
            this.SizeChanged += UcKioskOut_SizeChanged;

            CreateContextMenu();

            ucKioskOutDashboard1.MouseUp += panelDashboard_MouseUp;
        }
        private void CreateContextMenu()
        {
            contextMenu.Items.Add("Đút thẻ", null, OnOption1Click);
            contextMenu.Items.Add("Đè loop", null, OnOption2Click);
            contextMenu.Items.Add("Check Voucher", null, OnReloadClick);
        }
        private async void OnOption1Click(object? sender, EventArgs e)
        {
            CardEventArgs abc = new CardEventArgs()
            {
                PreferCard = "33ACF89A",
                ReaderIndex = 1,
            };

            await OnNewEvent(abc);
        }

        private void OnOption2Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Option 2");
        }

        private void OnReloadClick(object? sender, EventArgs e)
        {
            MessageBox.Show($"Trang thai Voucher: {voucherController.IsConnect}");
        }
        private void InitPresenter(IVoucherController voucherController)
        {
            var maskedDialog = new MaskedUserControl(this);

            var confirmPlateDailyView = new ucConfirmPlateDailyKioskOut(defaultImg) { Visible = false };
            var confirmPlateMonthlyView = new ucConfirmPlateMonthlyKioskOut(defaultImg) { Visible = false };
            var confirmPaymentView = new UcConfirmPayment(defaultImg) { Visible = false };

            var confirmPlateDailyPresenter = new ConfirmPlateDailyPresenter(confirmPlateDailyView);
            var confirmPlateMonthlyPresenter = new ConfirmPlateMonthlyPresenter(confirmPlateMonthlyView);
            var confirmPaymentPresenter = new KioskOutConfirmPaymentPresenter(confirmPaymentView);


            var paymentView = new ucKioskPayment(this);
            var _qrView = new UcQRView()
            {
                TargetControl = (UserControl)this,
                BorderRadius = 64,
            };
            var _visaView = new UcVisaView(maskedDialog);
            var _cashView = new UcCashView(this)
            {
                TargetControl = (UserControl)this,
                BorderRadius = 64,
            };
            var _voucherView = new UcVoucherView()
            {
                TargetControl = (UserControl)this,
                BorderRadius = 64,
            };

            var qrPresenter = new QrPresenter(_qrView, AppData.PaymentKioskConfig);
            var visaPresenter = new VisaPresenter(_visaView, AppData.PaymentKioskConfig);
            var cashPresenter = new CashPresenter(_cashView, AppData.PaymentKioskConfig, AppData.ApiServer);
            var voucherPresenter = new VoucherPresenter(_voucherView, voucherController, AppData.ApiServer);

            var paymentPresenter = new KioskOutPaymentPresenter(paymentView,
                                                                qrPresenter, visaPresenter, cashPresenter, voucherPresenter,
                                                                AppData.ApiServer, AppData.PaymentKioskConfig);

            var basePresenter = new KioskOutBasePresenter(this, AppData.ApiServer,
                                                         paymentPresenter, confirmPaymentPresenter,
                                                         confirmPlateDailyPresenter, confirmPlateMonthlyPresenter,
                                                         this.LastCardEventDatas);

            var cardPresenter = new KioskOutCardPresenter(basePresenter);
            var loopPresenter = new KioskOutLoopPresenter(basePresenter);

            presenter = new KioskOutPresenter(this, basePresenter, cardPresenter, loopPresenter);

            panelDialog.Controls.Add(confirmPlateDailyView);
            panelDialog.Controls.Add(confirmPlateMonthlyView);
            panelDialog.Controls.Add(confirmPaymentView);
            panelDialog.Controls.Add(paymentView);

            confirmPlateDailyView.Size = this.ucKioskOutDashboard1.Size;
            confirmPlateMonthlyView.Size = this.ucKioskOutDashboard1.Size;
            confirmPaymentView.Size = this.ucKioskOutDashboard1.Size;
            paymentView.Size = this.ucKioskOutDashboard1.Size;

            confirmPlateDailyView.Location = this.ucKioskOutDashboard1.Location;
            confirmPlateMonthlyView.Location = this.ucKioskOutDashboard1.Location;
            confirmPaymentView.Location = this.ucKioskOutDashboard1.Location;
            paymentView.Location = this.ucKioskOutDashboard1.Location;
        }

        private void UcKioskOut_SizeChanged(object? sender, EventArgs e)
        {
            this.SuspendLayout();
            this.panelDashboard.SuspendLayout();
            this.panelDialog.SuspendLayout();
            this.panelDashboard.Location = new Point((this.ClientSize.Width - ucKioskOutDashboard1.Width) / 2, ucKioskTitle1.Height);
            this.panelDialog.Location = this.panelDashboard.Location;
            this.panelDialog.ResumeLayout();
            this.panelDashboard.ResumeLayout();
            this.ResumeLayout();
        }
        #endregion

        #region Đóng Làn
        public void Stop()
        {
            UcCameraList.Stop();
        }
        public void InitUI()
        {
            loadingView = new UcLoading()
            {
                TargetControl = this,
                BorderRadius = 24
            };
            ucSelectVehicles = new UcSelectVehicles(false)
            {
                TargetControl = this,
                BorderRadius = 24,
            };
            ucDailyNotify = new()
            {
                Visible = false,
                Size = this.ucKioskOutDashboard1.Size,
                Location = this.ucKioskOutDashboard1.Location
            };
            ucMonthlyNotify = new()
            {
                Visible = false,
                Size = this.ucKioskOutDashboard1.Size,
                Location = this.ucKioskOutDashboard1.Location
            };

            panelDialog.Controls.Add(ucDailyNotify);
            panelDialog.Controls.Add(ucMonthlyNotify);

            this.ucKioskTitle1.Init(defaultImg, UcKioskTitle1_OnLanguageChangedEvent, OpenSettingPage);
            UcCameraList.Init(this, AppData.Cameras, AppData.AppConfig.IsUseVirtualLoop, AppData.AppConfig.VirtualLoopMode, AppData.AppConfig.MotionAlarmLevel, AppData.AppConfig.cameraSDk);
            ucKioskOutDashboard1.Init(this.Lane, BtnRetakeImage_Click);
        }
        private void Translate()
        {
            this.ucKioskOutDashboard1.Translate();
            this.ucDailyNotify.Translate();
            this.ucMonthlyNotify.Translate();
            this.presenter?.Translate();
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
        private void OpenSettingPage(object? sender, EventArgs e)
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
        }
        public async Task OnNewEvent(EventArgs e)
        {
            await semaphoreSlimOnNewEvent.WaitAsync();
            try
            {
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
                else if (e is CardNotSupportEventArgs cardNotSupportEventArgs)
                {
                    ucKioskTitle1.NotifyCardEvent();
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Card not support", cardNotSupportEventArgs));
                    await this.presenter.ExecuteCardNotSupportEventArgs(cardNotSupportEventArgs);
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
        }
        private async void VoucherController_CardEvent(object sender, CardEventArgs e)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New QR500 Card Event", e.PreferCard));
            this.Invoke(new Action(() =>
            {
                e.ReaderIndex = KioskOutCardPresenter.READER_MONTHLY;
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
        //Nhận được kết quả thanh toán từ server
        public void ApplyPaymentResult(PaymentResult paymentResult)
        {
            this.presenter.ApplyPaymentResult(paymentResult);
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
                await this.Invoke(async () => await this.ucDailyNotify.ShowDialog(request));
                return;
            }
            await this.ucDailyNotify.ShowDialog(request);
        }
        public async Task ShowMonthlyNotifyDialog(KioskDialogMonthlyRequest request)
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                await this.Invoke(async () => await this.ucMonthlyNotify.ShowDialog(request));
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

        private async void đútThẻLượtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void panelDashboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(ucKioskOutDashboard1, e.Location);
            }
        }
    }
}