using iParkingv8.Object.Objects.Events;

namespace Kztek.Control8.KioskOut.PaymentPresenter.CASH
{
    public interface ICashView
    {
        event EventHandler OnBackClicked;

        // Các phương thức hiển thị dữ liệu và trạng thái
        void DisplayEventInfo(ExitData exitData);

        void ShowView();
        void HideView();
        void ClearView();

        public void ShowLoading(string title, string subTitle);

        public void UpdateLoadingMessage(string title, string subTitle);

        public void HideLoadingIndicator();

        // THÊM: Các phương thức để Presenter truyền văn bản dịch cho View
        void SetTitleText(string text);
        void SetSubTitleText(string text);

        void SetInfoText(string text);

        void SetAccessKeyNameTitleText(string text);
        void SetTimeInTitleText(string text);
        void SetPlateInTitleText(string text);
        void SetVehicleTypeTitleText(string text);
        void SetTimeOutTitleText(string text);
        void SetPlateOutTitleText(string text);

        void SetFeeTitleText(string text);
        void SetPaidTitleText(string text);
        void SetRemainTitleText(string text);

        void SetBtnBackText(string text);

    }
}
