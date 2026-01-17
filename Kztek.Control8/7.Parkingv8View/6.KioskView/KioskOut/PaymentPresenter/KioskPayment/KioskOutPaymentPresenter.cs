using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using Kztek.Control8.KioskOut.PaymentPresenter.CASH;
using Kztek.Control8.KioskOut.PaymentPresenter.QR;
using Kztek.Control8.KioskOut.PaymentPresenter.VISA;
using Kztek.Object;
using Kztek.Tool;

namespace Kztek.Control8.KioskOut.PaymentPresenter.KioskPayment
{
    public class KioskOutPaymentPresenter
    {
        private IKioskOutPaymentView _view { get; set; }
        private IAPIServer _apiServer { get; set; }

        // Đây là TaskCompletionSource mà bạn sẽ chờ đợi từ bên ngoài Presenter
        private TaskCompletionSource<BaseKioskResult> _processCompletionSource;
        private ExitData? _currentExitData; // Presenter giữ dữ liệu hiện tại
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer; // Trạng thái này thuộc về Presenter
        private PaymentKioskConfig _paymentConfig;

        public QrPresenter _qrPresenter;
        public CashPresenter _cashPresenter;
        public VisaPresenter _visaPresenter;
        public VoucherPresenter _voucherPresenter;

        public KioskOutPaymentPresenter(IKioskOutPaymentView view,
                                        QrPresenter qrPresenter, VisaPresenter visaPresenter,
                                        CashPresenter cashPresenter, VoucherPresenter voucherPresenter,
                                        IAPIServer apiServer, PaymentKioskConfig config)
        {
            _view = view;
            _apiServer = apiServer;
            _paymentConfig = config;

            //Đăng ký lắng nghe sự kiện từ view
            _view.OnBackClicked += KioskPaymentView_BackClicked;

            //if (config.IsUseQR)
            _view.OnCreateQRClicked += KioskPaymentView_CreateQRClicked;

            //if (config.IsUseCash)
            _view.OnStartCashClick += KioskPaymentView_StartCashClick;

            //if (config.IsUseVoucher)
            _view.OnStartVoucherClick += KioskPaymentView_StartVoucherClick;

            _qrPresenter = qrPresenter;
            _visaPresenter = visaPresenter;
            _cashPresenter = cashPresenter;
            _voucherPresenter = voucherPresenter;

            // Khi Presenter được tạo, cập nhật ngôn ngữ ban đầu cho View
            TranslateUI();
        }

        public async Task<BaseKioskResult> RunPaymentProcessAsync(ExitData exitData, Control parent)
        {
            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentExitData = exitData; // Lưu dữ liệu vào Presenter
            _kioskUserType = EmKioskUserType.Customer; // Khởi tạo trạng thái người dùng

            _view.ClearView(); // Yêu cầu View làm sạch
            _view.DisplayEventInfo(_currentExitData); // Yêu cầu View hiển thị thông tin

            Translate(); // Đảm bảo ngôn ngữ đúng khi hiển thị

            // 3. Chờ đợi một kết quả từ việc xử lý các sự kiện của View
            return await _processCompletionSource.Task;
        }
        private void KioskPaymentView_BackClicked(object? sender, EventArgs e)
        {
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }

        public void Translate()
        {
            _qrPresenter.Translate();
            _visaPresenter.Translate();
            _cashPresenter.Translate();
            _voucherPresenter.Translate();
            TranslateUI();
        }
        private void TranslateUI()
        {
            _view.SetTitleText(KZUIStyles.CurrentResources.PaymentInfo);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.colAccessKeyName);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetTimeOutTitleText(KZUIStyles.CurrentResources.TimeOut);
            _view.SetPlateOutTitleText(KZUIStyles.CurrentResources.PlateOut);

            _view.SetFeeTitleText(KZUIStyles.CurrentResources.Fee);
            _view.SetPaidTitleText(KZUIStyles.CurrentResources.Paid);
            _view.SetRemainTitleText(KZUIStyles.CurrentResources.Remain);

            _view.SetChoosePaymentMethodTitleText(KZUIStyles.CurrentResources.ChoosePaymentMethodRequired);
            _view.SetCashButtonText(KZUIStyles.CurrentResources.Cash);
            _view.SetVoucherButtonText(KZUIStyles.CurrentResources.Voucher);
            _view.SetQRButtonText(KZUIStyles.CurrentResources.QR);

            _view.SetBtnBackToMainText(KZUIStyles.CurrentResources.BackToMain);
        }

        #region CashPayment
        private async void KioskPaymentView_StartCashClick(object? sender, EventArgs e)
        {
            if (!this._paymentConfig.IsUseCash)
            {
                ShowNotActivePaymentMethod();
                return;
            }
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Nguoi dung nhan tien mat"));

            this._view.ShowLoading(KZUIStyles.CurrentResources.ConnectingWithDevice, KZUIStyles.CurrentResources.WaitAMoment);
            try
            {
                bool isConnectToDeviceSuccess = await Task.Run(_cashPresenter.ConnectToDevice);
                //isConnectToDeviceSuccess = true;
                if (!isConnectToDeviceSuccess)
                {
                    this._view.HideLoadingIndicator();
                    ShowDisconnectedWithDeviceNotify();
                    return;
                }

                this._view.HideLoadingIndicator();

                // Đổi màu option Cash
                this._view.ActiveCashMode();

                // Cập nhật thông tin tiền
                var result = await _cashPresenter.ShowView(_currentExitData);
                if (result.IsConfirm)
                {
                    _view.CloseDialog();
                    _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = EmKioskUserType.System });
                }
                else
                {
                    this._view.DisplayEventInfo(_currentExitData);
                }
            }
            catch (Exception ex)
            {
                this._view.HideLoadingIndicator();
                ShowDisconnectedWithDeviceNotify();
            }
            finally
            {
                this._view.DisablePaymentMode();
            }
        }
        #endregion

        private async Task<Transaction?> CreateCashTransaction(ExitData exitData, PaymentKioskConfig config)
        {
            var entry = exitData.Entry!;
            string description = (exitData.Entry?.AccessKey?.Name ?? "") + " - " + (exitData.Entry?.PlateNumber ?? "");
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(description));

            long remain = exitData.Amount - exitData.DiscountAmount - entry.Amount;
            OrderMethod method = OrderMethod.CASH;

            // Fixme: posid
            PaymentRequest paymentRequest = new PaymentRequest(exitData.Id, method, remain, description, targetType: TargetType.EXIT, posId: config.QRPosId);
            return await _apiServer.PaymentService.CreateTransactionAsync(paymentRequest);
        }
        #region Voucher Payment
        private async void KioskPaymentView_StartVoucherClick(object? sender, EventArgs e)
        {
            if (!this._paymentConfig.IsUseCash)
            {
                ShowNotActivePaymentMethod();
                return;
            }
            this._view.ShowLoading(KZUIStyles.CurrentResources.ConnectingWithDevice, KZUIStyles.CurrentResources.WaitAMoment);
            try
            {
                bool isConnectToDeviceSuccess = await Task.Run(_voucherPresenter.ConnectToDevice);
                if (!isConnectToDeviceSuccess)
                {
                    this._view.HideLoadingIndicator();
                    ShowDisconnectedWithDeviceNotify();
                    return;
                }

                this._view.HideLoadingIndicator();
                this._view.ActiveVoucherMode();
                var result = await _voucherPresenter.ShowView(_currentExitData);
                _voucherPresenter.DisconnectWithDevice();

                //Hoàn tất thành toán thì hiện thông báo
                //Chưa hoàn tất, cập nhật lại thông tin thanh toán hiện tại
                if (result.IsConfirm && result.kioskUserType == EmKioskUserType.System)
                {
                    _view.CloseDialog();
                    _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = EmKioskUserType.System });
                }
                else
                {
                    this._view.DisplayEventInfo(_currentExitData);
                }
            }
            catch (Exception ex)
            {
                this._view.HideLoadingIndicator();
                ShowDisconnectedWithDeviceNotify();
            }
            finally
            {
                this._view.DisablePaymentMode();
            }
        }
        #endregion

        #region Qr Payment
        private async void KioskPaymentView_CreateQRClicked(object? sender, EventArgs e)
        {
            if (!this._paymentConfig.IsUseCash)
            {
                ShowNotActivePaymentMethod();
                return;
            }
            this._view.ShowLoading(KZUIStyles.CurrentResources.ConnectingWithDevice, KZUIStyles.CurrentResources.WaitAMoment);
            try
            {
                var result = await CreateQRTransaction(_currentExitData!, _paymentConfig);
                this._view.HideLoadingIndicator();

                if (result == null || string.IsNullOrEmpty(result.Id))
                {
                    string title = nameof(KZUIStyles.CurrentResources.SystemError);
                    string subTitle = nameof(KZUIStyles.CurrentResources.TryAgain);
                    string backTitle = nameof(KZUIStyles.CurrentResources.Back);
                    _view.ShowErrorMessage(title, subTitle, backTitle);
                    return;
                }
                this._view.ActiveQrMode();
                var qrPaymentResult = await _qrPresenter.ShowQR(result);
                if (qrPaymentResult.IsConfirm)
                {
                    _view.CloseDialog();
                    _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = EmKioskUserType.System });
                }
            }
            catch (Exception ex)
            {
                this._view.HideLoadingIndicator();
                ShowDisconnectedWithDeviceNotify();
            }
            finally
            {
                this._view.DisablePaymentMode();
            }
        }
        private async Task<Transaction?> CreateQRTransaction(ExitData exitData, PaymentKioskConfig config)
        {
            var entry = exitData.Entry!;
            string description = (exitData.Entry?.AccessKey?.Name ?? "") + " - " + (exitData.Entry?.PlateNumber ?? "");
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(description));

            long remain = exitData.Amount - exitData.DiscountAmount - entry.Amount;
            OrderMethod method = config.QRProvider == EmQRProvider.TINGEE ? OrderMethod.TINGEE_QR_CODE : OrderMethod.VIMO_QR_CODE;
            PaymentRequest paymentRequest = new PaymentRequest(exitData.Id, method, remain, description, targetType: TargetType.EXIT, posId: config.QRPosId);
            return await _apiServer.PaymentService.CreateTransactionAsync(paymentRequest);
        }
        #endregion

        //#region Visa Payment
        //private async void KioskPaymentView_CreateVisaClicked(object? sender, EventArgs e)
        //{
        //    ShowLoading();
        //    try
        //    {
        //        var result = await CreateVisaTransaction(_currentExitData!, _paymentConfig);
        //        _loadingView.HideLoadingIndicator();
        //        if (result == null || string.IsNullOrEmpty(result.Id))
        //        {
        //            string title = KzDictionary.GetDisplayText(_currentLanguageCode, EmLanguageKey.DIALOG_SYSTEM_ERROR_TITLE);
        //            string subTitle = KzDictionary.GetDisplayText(_currentLanguageCode, EmLanguageKey.NOTIFY_TRY_AGAIN);
        //            string backTitle = KzDictionary.GetDisplayText(_currentLanguageCode, EmLanguageKey.BTN_BACK);
        //            _view.ShowErrorMessage(title, subTitle, backTitle, this._currentLanguageCode);
        //            return;
        //        }
        //        var visaPayment = await _visaPresenter.ShowVisaTransactionId(result.Code, result.Id);
        //        this._view.DisablePaymentMode();
        //        if (visaPayment.IsConfirm)
        //        {
        //            _view.CloseDialog();
        //            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = EmKioskUserType.System });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _loadingView.HideLoadingIndicator();
        //        ShowTransactionErrorDialog();
        //    }
        //    finally
        //    {
        //        this._view.DisablePaymentMode();
        //    }
        //}
        //private async Task<CreateOnlineOrderResponse?> CreateVisaTransaction(ExitData exitData, PaymentKioskConfig config)
        //{
        //    var entry = exitData.Entry!;
        //    string description = (exitData.Entry?.AccessKey?.Name ?? "") + " - " + (exitData.Entry?.PlateNumber ?? "");
        //    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(description));

        //    long remain = exitData.Amount - exitData.DiscountAmount - entry.Amount;
        //    OrderMethod method = OrderMethod.VIMO_CARD;
        //    PaymentRequest paymentRequest = new PaymentRequest(exitData.Id, method, remain, description, posId: config.VisaId, targetType: TargetType.Exit);
        //    return await _apiServer.PaymentService.CreateTransactionAsync(paymentRequest);
        //}
        //#endregion

        public void ApplyPaymentResult(PaymentResult result)
        {
            _qrPresenter.ApplyResult(result);
            _visaPresenter.ApplyResult(result);
        }

        public void ShowDisconnectedWithDeviceNotify()
        {
            string title = nameof(KZUIStyles.CurrentResources.DeviceDisconnected);
            string subTitle = nameof(KZUIStyles.CurrentResources.TryAgain);
            string backTitle = nameof(KZUIStyles.CurrentResources.Back);

            _view.ShowErrorMessage(title, subTitle, backTitle);
        }
        public void ShowNotActivePaymentMethod()
        {
            string title = nameof(KZUIStyles.CurrentResources.PaymentMethodNotActive);
            string subTitle = nameof(KZUIStyles.CurrentResources.ChooseOtherPaymentMethodRequired);
            string backTitle = nameof(KZUIStyles.CurrentResources.Back);
            _view.ShowErrorMessage(title, subTitle, backTitle);
        }

        public void ShowCreateTransactionErrorNotify()
        {
            string title = nameof(KZUIStyles.CurrentResources.DeviceDisconnected);
            string subTitle = nameof(KZUIStyles.CurrentResources.TryAgain);
            string backTitle = nameof(KZUIStyles.CurrentResources.Back);

            _view.ShowErrorMessage(title, subTitle, backTitle);
        }
    }
}
