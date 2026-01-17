using iParkingv8.Object.Objects.Events;

namespace Kztek.Control8.KioskOut.PaymentPresenter.KioskPayment
{
    public interface IKioskOutPaymentView
    {
        event EventHandler OnBackClicked;

        event EventHandler OnCreateQRClicked;
        event EventHandler OnStartCashClick;
        event EventHandler OnStartVoucherClick;

        void AllowQRPayment(bool isAllow);
        void AllowVoucherPayment(bool isAllow);
        void AllowCashPayment(bool isAllow);

        void ActiveCashMode();
        void ActiveVoucherMode();
        void ActiveQrMode();
        void DisablePaymentMode();

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfo(ExitData exitData);
        void ShowErrorMessage(string title, string subTitle, string backtitle);
        void CloseDialog();
        void ClearView();
        void ShowCompletedPayment();

        void ShowLoading(string title, string subTitle);
        void UpdateLoadingMessage(string title, string subTitle);
        void HideLoadingIndicator();

        // THÊM: Các phương thức để Presenter truyền văn bản dịch cho View
        void SetTitleText(string text);

        void SetAccessKeyNameTitleText(string text);
        void SetTimeInTitleText(string text);
        void SetPlateInTitleText(string text);
        void SetVehicleTypeTitleText(string text);
        void SetTimeOutTitleText(string text);
        void SetPlateOutTitleText(string text);

        void SetFeeTitleText(string text);
        void SetPaidTitleText(string text);
        void SetRemainTitleText(string text);

        void SetChoosePaymentMethodTitleText(string text);

        void SetCashButtonText(string text);
        void SetVoucherButtonText(string text);
        void SetQRButtonText(string text);

        void SetBtnBackToMainText(string text);

    }
}
