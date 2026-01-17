using iParkingv8.Object.Objects.Events;

namespace Kztek.Control8.KioskOut.PaymentPresenter.ConfirmPayment
{
    public interface IKioskOutConfirmPaymentView
    {
        event EventHandler OnBackClicked;

        event EventHandler OnPaymentClicked;

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfo(ExitData exitData, Image? displayImage);
        void CloseDialog();
        void ClearView();
        void CenterControlLocation();

        void SetTitleText(string text);

        void SetAccessKeyNameTitleText(string text);
        void SetTimeInTitleText(string text);
        void SetPlateInTitleText(string text);
        void SetVehicleTypeTitleText(string text);
        void SetTimeOutTitleText(string text);
        void SetPlateOutTitleText(string text);

        void SetFeeTitleText(string text);

        void SetBtnBackToMainText(string text);
        void SetBtnPaymentText(string text);
    }
}
