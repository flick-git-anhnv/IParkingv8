using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;

namespace Kztek.Control8.KioskIn.ConfirmOpenBarrie
{
    public interface IConfirmOpenBarrieKioskInView
    {
        event EventHandler OnBackClicked;

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfor(EntryData entryData, AccessKey accessKey, Lane lane, Image? vehicleImage, Image? panoramaImage, string errorMessage);
        void CloseDialog();
        void ClearView();

        void CenterControlLocation();

        public void SetTitleText(string text);
        public void SetSubTitleText(string text);

        public void SetAccessKeyNameTitleText(string text);
        public void SetTimeInTitleText(string text);
        public void SetPlateInTitleText(string text);
        public void SetVehicleTypeTitleText(string text);
        public void SetAccesskeyCollectionTitle(string text);
        public void SetLaneTitle(string text);

        public void SetBtnBackToMainText(string text);
    }
}
