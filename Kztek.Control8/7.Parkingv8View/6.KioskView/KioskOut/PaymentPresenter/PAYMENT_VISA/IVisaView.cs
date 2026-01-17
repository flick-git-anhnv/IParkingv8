namespace Kztek.Control8.KioskOut.PaymentPresenter.VISA
{
    public interface IVisaView
    {
        event EventHandler OnBackClicked;

        void ShowView();
        void GotResult();

        void SetLblTransactionTitle(string text);
        void SetLblTransactionId(string text);
        void SetLblTitle(string text);
        void SetLblGuide(string text);

        void SetBtnBackText(string text);
    }
}
