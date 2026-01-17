using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskOut.ConfirmPlatePresenter
{
    public class ConfirmPlateDailyPresenter
    {
        IConfirmPlateDailyView _view;
        private TaskCompletionSource<BaseKioskResult> _processCompletionSource;
        private ExitData? _currentExitData;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer;
        private string messageTag = string.Empty;
        public ConfirmPlateDailyPresenter(IConfirmPlateDailyView view)
        {
            _view = view;

            _view.OnBackClicked += _view_OnBackClicked;

            Translate();
        }

        public async Task<BaseKioskResult> ShowConfirmPlateAsync(ExitData exitData, Image? displayImage, string messageTag, Control parent)
        {
            this.messageTag = messageTag;
            Translate();

            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentExitData = exitData; // Lưu dữ liệu vào Presenter
            _kioskUserType = EmKioskUserType.Customer; // Khởi tạo trạng thái người dùng

            // 1. Xóa nội dung hiển thị cũ
            _view.ClearView();

            // 2. Yêu cầu View hiển thị thông tin
            _view.DisplayEventInfo(_currentExitData, displayImage, UIBuiltInResourcesHelper.GetValue(messageTag), parent);

            // 3. Chờ đợi một kết quả từ việc xử lý các sự kiện của View
            return await _processCompletionSource.Task;
        }

        public void ApplyResult(bool isConfirm, string requestId)
        {
            if (!((Control)_view).Visible)
            {
                return;
            }
            if (_currentExitData?.Id != requestId)
            {
                return;
            }
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.System;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = isConfirm, kioskUserType = _kioskUserType });
        }

        private void _view_OnBackClicked(object? sender, EventArgs e)
        {
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }

        public void Translate()
        {
            _view.SetTitleText(UIBuiltInResourcesHelper.GetValue(messageTag));
            _view.SetSubTitleText(KZUIStyles.CurrentResources.WaitForSecurityConfirm);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.AccesskeyName);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetTimeOutTitleText(KZUIStyles.CurrentResources.TimeOut);
            _view.SetPlateOutTitleText(KZUIStyles.CurrentResources.PlateOut);
            _view.SetBtnBackToMainText(KZUIStyles.CurrentResources.BackToMain);
            _view.CenterControlLocation();
        }
    }
}
