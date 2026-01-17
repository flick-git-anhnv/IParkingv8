using iParkingv5.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;
using iParkingv8.Ultility.Style;
using IParkingv8.Helpers;
using Kztek.Control8.Forms;

namespace IParkingv8.LaneUIs.KioskOut
{
    public class KioskOutPresenter
    {
        private readonly IKioskOutView mainView;
        private readonly KioskOutBasePresenter basePresenter;
        private readonly KioskOutCardPresenter kioskOutCardPresenter;
        private readonly KioskOutLoopPresenter kioskOutLoopPresenter;

        public KioskOutPresenter(
                    IKioskOutView mainView,
                    KioskOutBasePresenter basePresenter,
                    KioskOutCardPresenter kioskOutCardPresenter,
                    KioskOutLoopPresenter kioskOutLoopPresenter)
        {
            this.mainView = mainView;
            this.basePresenter = basePresenter;
            this.kioskOutCardPresenter = kioskOutCardPresenter;
            this.kioskOutLoopPresenter = kioskOutLoopPresenter;
        }

        public async Task OnNewCardEvent(CardEventArgs e)
        {
            try
            {
                await this.kioskOutCardPresenter.ExcecuteCardEvent(e);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle), ex.Message, EmImageDialogType.Error,
                                                    "", null, null, null);
            }
        }
        public async Task ExecuteInputEvent(InputEventArgs ie)
        {
            try
            {
                await this.kioskOutLoopPresenter.ExecuteInputEvent(ie);
            }
            catch (Exception ex)
            {
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle), ex.Message, EmImageDialogType.Error,
                                                    "", null, null, null);
            }
        }
        public async Task ExecuteCardNotSupportEventArgs(CardNotSupportEventArgs ie)
        {
            basePresenter.OpenHomePage();

            //thông báo thẻ không được hỗ trợ
            var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, "", basePresenter._mainView.UcCameraList, false, null);
            _ = this.basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle),
                                                         nameof(KZUIStyles.CurrentResources.AccessKeyNotSupport), EmImageDialogType.Error,
                                                         _result.PlateNumber, null, _result.VehicleImage, _result.PanoramaImage);
            //Ra lệnh nhả thẻ
            _ = ControllerHelper.RejectCard(basePresenter._mainView, ie.DeviceId);
        }
        public void Translate()
        {
            this.basePresenter?.Translate();
        }

        public void NotifyLastMessage()
        {
            this.basePresenter.NotifyLastMessage();
        }
        public void ApplyConfirmResult(EventRequest eventRequest)
        {
            this.basePresenter.ApplyConfirmResult(eventRequest);
        }
        public void ApplyPaymentResult(PaymentResult paymentResult)
        {
            this.basePresenter.ApplyPaymentResult(paymentResult);
        }

        public void OpenSettingPage()
        {
            if (!IsValidAuthentication()) return;
            this.mainView.OpenSettingPage();
        }
        public bool IsValidAuthentication()
        {
            if (!AppData.IsNeedToConfirmPassword)
            {
                return true;
            }
            var frm = new FrmConfirmPassword();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            AppData.IsNeedToConfirmPassword = false;
            return true;
        }
    }
}
