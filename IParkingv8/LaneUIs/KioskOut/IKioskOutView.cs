using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;
using IParkingv8.UserControls;
using Kztek.Object;

namespace IParkingv8.LaneUIs.KioskOut
{
    public interface IKioskOutView : ILane
    {
        void StopTimerCheckAllowOpenBarrie();
        void StartTimerCheckAllowOpenBarrie();

        void ApplyConfirmResult(EventRequest eventRequest);
        void ApplyPaymentResult(PaymentResult paymentResult);

        void NotifyLastMessage();
        void OpenSettingPage();

        //DIALOG
        void OpenHomePage();
        void OpenDialogPage();
        Task ShowDailyNotifyDialog(KioskDialogDialyRequest request);
        Task ShowMonthlyNotifyDialog(KioskDialogMonthlyRequest request);

        //LOADING
        void HideLoadingIndicator();
        void ShowLoadingIndicator(string title, string subTitle);
        void UpdateLoadingIndicator(string title, string subTitle);
    }
}
