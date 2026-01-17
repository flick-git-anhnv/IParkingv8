using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskOut.PaymentPresenter.ConfirmPayment
{
    public class KioskOutConfirmPaymentPresenter
    {
        IKioskOutConfirmPaymentView _view;

        // Đây là TaskCompletionSource mà bạn sẽ chờ đợi từ bên ngoài Presenter
        private TaskCompletionSource<BaseKioskResult> _processCompletionSource;
        private ExitData? _currentExitData; // Presenter giữ dữ liệu hiện tại
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer; // Trạng thái này thuộc về Presenter
        public KioskOutConfirmPaymentPresenter(IKioskOutConfirmPaymentView view)
        {
            _view = view;

            _view.OnBackClicked += _view_OnBackClicked;
            _view.OnPaymentClicked += _view_OnPaymentClicked;

            Translate();
        }

        public async Task<BaseKioskResult> RunConfirmPaymentProcessAsync(ExitData exitData, Image? displayImage)
        {
            Translate();

            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentExitData = exitData; // Lưu dữ liệu vào Presenter
            _kioskUserType = EmKioskUserType.Customer; // Khởi tạo trạng thái người dùng

            // 1. Xóa nội dung hiển thị cũ
            _view.ClearView();

            // 2. Yêu cầu View hiển thị thông tin
            _view.DisplayEventInfo(_currentExitData, displayImage);

            // 3. Chờ đợi một kết quả từ việc xử lý các sự kiện của View
            return await _processCompletionSource.Task;
        }

        private void _view_OnPaymentClicked(object? sender, EventArgs e)
        {
            _view.CloseDialog();
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = _kioskUserType });
        }
        private void _view_OnBackClicked(object? sender, EventArgs e)
        {
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }

        public void Translate()
        {
            _view.SetTitleText(KZUIStyles.CurrentResources.PaymentRequired);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.AccesskeyName);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetTimeOutTitleText(KZUIStyles.CurrentResources.TimeOut);
            _view.SetPlateOutTitleText(KZUIStyles.CurrentResources.PlateOut);
            _view.SetFeeTitleText(KZUIStyles.CurrentResources.Fee);
            _view.SetBtnBackToMainText(KZUIStyles.CurrentResources.BackToMain);
            _view.SetBtnPaymentText(KZUIStyles.CurrentResources.PayParkingFee);

            _view.CenterControlLocation();
        }
    }
}
