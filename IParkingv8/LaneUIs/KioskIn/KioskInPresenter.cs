using iParkingv5.Objects.Events;
using iParkingv8.Object.Objects.RabbitMQ;
using iParkingv8.Ultility.Style;
using Kztek.Control8.Forms;

namespace IParkingv8.LaneUIs.KioskIn
{
    public class KioskInPresenter
    {
        private IKioskInView mainView;

        private readonly KioskInBasePresenter basePresenter;
        private readonly KioskInCardPresenter KioskInCardPresenter;
        private readonly KioskInLoopPresenter KioskInLoopPresenter;


        public KioskInPresenter(
                    IKioskInView mainView,
                    KioskInBasePresenter basePresenter,
                    KioskInCardPresenter kioskInCardPresenter,
                    KioskInLoopPresenter kioskInLoopPresenter
            )
        {
            this.mainView = mainView;
            this.basePresenter = basePresenter;
            KioskInCardPresenter = kioskInCardPresenter;
            KioskInLoopPresenter = kioskInLoopPresenter;
        }

        public async Task CardOnRFEventArgs(CardOnRFEventArgs e)
        {
            try
            {
                await KioskInCardPresenter.CardOnRFEventArgs(e);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SystemError), ex.Message, iParkingv8.Object.Objects.Kiosk.EmImageDialogType.Error,
                                                        "", null, null, null);
            }
        }

        public async Task OnNewCardEvent(CardEventArgs e)
        {
            try
            {
                await KioskInCardPresenter.OnNewCardEvent(e);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SystemError), ex.Message, iParkingv8.Object.Objects.Kiosk.EmImageDialogType.Error,
                                                        "", null, null, null);
            }
        }
        public async Task ExcecuteCardbeTaken(CardBeTakenEventArgs e)
        {
            try
            {
                await KioskInCardPresenter.ExcecuteCardbeTaken(e);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SystemError), ex.Message, iParkingv8.Object.Objects.Kiosk.EmImageDialogType.Error,
                                                        "", null, null, null);
            }
        }
        public async Task ExecuteInputEvent(InputEventArgs ie)
        {
            try
            {
                await KioskInLoopPresenter.ExecuteInputEvent(ie);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SystemError), ex.Message, iParkingv8.Object.Objects.Kiosk.EmImageDialogType.Error,
                                                        "", null, null, null);
            }
        }

        public void NotifyLastMessage()
        {
            basePresenter.NotifyLastMessage();
        }
        public void ApplyConfirmResult(EventRequest eventRequest)
        {
            basePresenter.confirmOpenBarriePresenter.ApplyServerResult(eventRequest.Action == (int)EmRequestAction.CONFIRM, eventRequest.RequestId);
            basePresenter.confirmPlateMonthlyKioskInPresenter.ApplyServerResult(eventRequest.Action == (int)EmRequestAction.CONFIRM, eventRequest.RequestId);
        }

        public void OpenSettingPage()
        {
            if (!IsValidAuthentication()) return;
            mainView.OpenSettingPage();
        }
        public bool IsValidAuthentication()
        {
            if (!AppData.IsNeedToConfirmPassword)
            {
                return true;
            }
            var frmConfirmPassword = new FrmConfirmPassword();
            if (frmConfirmPassword.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            AppData.IsNeedToConfirmPassword = false;
            return true;
        }

        public void Translate()
        {
            basePresenter?.Translate();
        }
    }
}
