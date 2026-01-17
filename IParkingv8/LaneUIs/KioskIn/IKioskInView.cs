using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.RabbitMQ;
using IParkingv8.UserControls;

namespace IParkingv8.LaneUIs.KioskIn
{
    public interface IKioskInView : ILane
    {
        void StopTimerCheckAllowOpenBarrie();
        void StartTimerCheckAllowOpenBarrie();

        void ApplyConfirmResult(EventRequest eventRequest);
        void NotifyLastMessage();
        void OpenSettingPage();

        void OpenHomePage();
        void OpenDialogPage();
        Task ShowDailyNotifyDialog(KioskDialogDialyRequest request);
        Task ShowMonthlyNotifyDialog(KioskDialogMonthlyRequest request);

        void HideLoadingIndicator();
        void ShowLoadingIndicator(string title, string subTitle);
        void UpdateLoadingIndicator(string title, string subTitle);
    }
}
