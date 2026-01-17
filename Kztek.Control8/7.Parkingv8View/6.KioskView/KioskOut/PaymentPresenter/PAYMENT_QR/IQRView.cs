using iParkingv8.Object.Objects.Payments;

namespace Kztek.Control8.KioskOut.PaymentPresenter.QR
{
    public interface IQRView
    {
        event EventHandler OnBackClicked;

        void DisplayQRImage(Image image);

        void ShowView();
        void HideView(); 

        void SetLblGuide(string text);
        void SetBtnBackText(string text);
    }
}
