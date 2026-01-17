using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskIn.ConfirmOpenBarrie
{
    public class ConfirmOpenBarrieKioskInPresenter
    {
        private readonly IConfirmOpenBarrieKioskInView _view;
        private TaskCompletionSource<BaseKioskResult>? _processCompletionSource;
        private EntryData? _currentEntryData;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer;
        private string messageTag = string.Empty;

        public ConfirmOpenBarrieKioskInPresenter(IConfirmOpenBarrieKioskInView view)
        {
            _view = view;
            _view.OnBackClicked += View_OnBackClicked;
            Translate();
        }

        public async Task<BaseKioskResult> ShowDialogAsync(EntryData entryData, AccessKey accessKey, Lane lane,
                                                           Image? vehicleImage, Image? panoramaImage,
                                                           string messageTag)
        {
            this.messageTag = messageTag;

            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentEntryData = entryData;
            _kioskUserType = EmKioskUserType.Customer;

            _view.ClearView();
            Translate();
            _view.DisplayEventInfor(_currentEntryData, accessKey, lane, vehicleImage, panoramaImage, 
                                    UIBuiltInResourcesHelper.GetValue(messageTag));

            return await _processCompletionSource.Task;
        }

        //Thay đổi ngôn ngữ
        public void Translate()
        {
            _view.SetTitleText(UIBuiltInResourcesHelper.GetValue(this.messageTag));
            _view.SetSubTitleText(KZUIStyles.CurrentResources.WaitForSecurityConfirm);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.AccesskeyName);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);

            _view.SetAccesskeyCollectionTitle(KZUIStyles.CurrentResources.AccessKeyCollection);
            _view.SetLaneTitle(KZUIStyles.CurrentResources.Lane);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetBtnBackToMainText(KZUIStyles.CurrentResources.BackToMain);

            _view.CenterControlLocation();
        }

        //Nhận kết quả xác nhận từ server
        public void ApplyServerResult(bool isConfirm, string requestId)
        {
            if (!((Control)_view).Visible)
            {
                return;
            }
            if (_currentEntryData?.Id != requestId)
            {
                return;
            }
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.System;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = isConfirm, kioskUserType = _kioskUserType });
        }

        //Người dùng bấm nút back trên giao diện phần mềm
        private void View_OnBackClicked(object? sender, EventArgs e)
        {
            _view.CloseDialog();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }
    }
}
