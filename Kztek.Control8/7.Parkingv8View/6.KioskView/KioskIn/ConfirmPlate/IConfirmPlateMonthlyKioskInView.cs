using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;
using static iParkingv8.Ultility.dictionary.KzDictionary;

namespace Kztek.Control8.KioskIn.ConfirmPlate
{
    public interface IConfirmPlateMonthlyKioskInView
    {
        event EventHandler OnBackClicked;

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfo(EntryData exitData, AccessKey accessKey, Lane lane, Image? vehicleImage, Image panoramaImage, string message);
        void CloseDialog();
        void ClearView();
        void CenterControlLocation();
        public void SetTitleText(string text);
        public void SetSubTitleText(string text);

        public void SetAccessKeyNameTitleText(string text);
        public void SetTimeInTitleText(string text);
        public void SetPlateInTitleText(string text);
        public void SetRegisterPlateTitleText(string text);
        public void SetVehicleTypeTitleText(string text);
        public void SetAccessKeyCollectionTitle(string text);
        public void SetLaneTitle(string text);
        public void SetCustomerTitleText(string text);

        public void SetBtnBackToMainText(string text);
    }
}