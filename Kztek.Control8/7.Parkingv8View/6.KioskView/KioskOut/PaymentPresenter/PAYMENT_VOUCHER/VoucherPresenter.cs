using iParkingv5.Controller.VoucherDevices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using Kztek.Tool;

namespace Kztek.Control8.KioskOut.PaymentPresenter.CASH
{
    public class VoucherPresenter
    {
        private readonly IVoucherView _view;
        private TaskCompletionSource<BaseKioskResult> _processCompletionSource;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer;
        private ExitData? _currentExitData;
        private PaymentKioskConfig paymentKioskConfig;
        private IVoucherController voucherController;
        private IAPIServer apiServer;
        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new SemaphoreSlim(1, 1);

        public VoucherPresenter(IVoucherView vouhcerView, IVoucherController voucherController, IAPIServer apiServer)
        {
            this.apiServer = apiServer;
            this.voucherController = voucherController;

            _view = vouhcerView;
            _view.OnBackClicked += _view_OnBackClicked;
        }

        private async void VoucherController_VoucherEvent(object sender, iParkingv5.Objects.Events.CardEventArgs e)
        {
            try
            {
                if (this._currentExitData is null)
                {
                    return;
                }
                await semaphoreSlimOnNewEvent.WaitAsync();
                await ApplyVoucher(e.PreferCard);
            }
            catch (Exception)
            {
            }
            finally
            {
                semaphoreSlimOnNewEvent.Release();
            }
        }

        public bool ConnectToDevice()
        {
            bool isConnectSuccess = this.voucherController?.IsConnect ?? false;
            if (isConnectSuccess)
            {
                this.voucherController!.VoucherEvent += VoucherController_VoucherEvent;
            }
            return isConnectSuccess;
        }
        public bool DisconnectWithDevice()
        {
            this.voucherController.VoucherEvent -= VoucherController_VoucherEvent;
            return true;
        }

        private void _view_OnBackClicked(object? sender, EventArgs e)
        {
            //VoucherController_CardEvent(null, new iParkingv5.Objects.Events.CardEventArgs()
            //{
            //    PreferCard = "8DDAA5CCCE7B1D85323847"
            //});
            //return;
            DisconnectWithDevice();
            _view.HideView();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }
        public async Task<BaseKioskResult> ShowView(ExitData exitData)
        {
            this._currentExitData = exitData;
            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentExitData = exitData; // Lưu dữ liệu vào Presenter
            _kioskUserType = EmKioskUserType.Customer; // Khởi tạo trạng thái người dùng

            _view.ClearView();
            _view.DisplayEventInfo(exitData);
            _view.ShowView();

            Translate();
            return await _processCompletionSource.Task;
        }

        public void Translate()
        {
            _view.SetInfoText(KZUIStyles.CurrentResources.PaymentRequired);
            _view.SetTitleText(KZUIStyles.CurrentResources.PaymentVoucherTitle);
            _view.SetSubTitleText(KZUIStyles.CurrentResources.PaymentVoucherSubTitle);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.AccesskeyName);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetTimeOutTitleText(KZUIStyles.CurrentResources.TimeOut);
            _view.SetPlateOutTitleText(KZUIStyles.CurrentResources.PlateOut);
            _view.SetFeeTitleText(KZUIStyles.CurrentResources.Fee);
            _view.SetPaidTitleText(KZUIStyles.CurrentResources.Paid);
            _view.SetRemainTitleText(KZUIStyles.CurrentResources.Remain);
            _view.SetBtnBackText(KZUIStyles.CurrentResources.Back);
            this._view.ShowErrorMessage("_");
        }
        private async Task ApplyVoucher(string voucherData)
        {
            try
            {
                if (this._currentExitData is null)
                {
                    return;
                }
                this._view.ShowErrorMessage("_");

                //--Gửi api parking nhận thông tin voicher
                var voucherDetail = await this.apiServer.PaymentService.ApplyVoucherExitAsync(voucherData, this._currentExitData.AccessKey.Code);
                if (voucherDetail == null || (voucherDetail.Item1 == null && voucherDetail.Item2 == null))
                {
                    string errorMessage = voucherData + ": Can't get your voucher info";
                    this._view.ShowErrorMessage(errorMessage);
                    return;
                }

                if (voucherDetail.Item2 != null)
                {
                    this._view.ShowErrorMessage(voucherDetail.Item2.ToString());
                    return;
                }

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{voucherData} - Insert Voucher Payment - Success"));

                //Lấy lại thông tin sau khi áp dụng voucher
                this._currentExitData.DiscountAmount += voucherDetail.Item1!.Amount;
                _view.DisplayEventInfo(_currentExitData);

                var remain = this._currentExitData.Amount - this._currentExitData.DiscountAmount - this._currentExitData.Entry.Amount;
                if (remain <= 0)
                {
                    DisconnectWithDevice();
                    _view.HideView();
                    _kioskUserType = EmKioskUserType.System;
                    _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = _kioskUserType });
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
