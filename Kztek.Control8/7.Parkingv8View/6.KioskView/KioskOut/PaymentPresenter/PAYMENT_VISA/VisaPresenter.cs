using iParkingv8.Object.Enums.Payments;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskOut.PaymentPresenter.VISA
{
    public class VisaPresenter
    {
        private readonly IVisaView _view;
        private string _currentTransactionId = string.Empty;
        private TaskCompletionSource<BaseKioskResult>? _processCompletionSource;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer; // Trạng thái này thuộc về Presenter
        private PaymentKioskConfig config;
        public VisaPresenter(IVisaView view, PaymentKioskConfig config)
        {
            _view = view;
            _view.OnBackClicked += View_OnBackClicked;
            this.config = config;
            Translate();
        }
        private void View_OnBackClicked(object? sender, EventArgs e)
        {
            _view.GotResult();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }
        public async Task<BaseKioskResult> ShowVisaTransactionId(string transactionCode, string transactionId)
        {
            _currentTransactionId = transactionId;
            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();

            //string translatedIdLabel = KzDictionary.GetDisplayText(_currentLanguageCode, EmLanguageKey.LBL_TRANSACTION_ID);
            _view.SetLblTransactionTitle(KZUIStyles.CurrentResources.TransactionId); // Đặt text cho label ID
            _view.SetLblTransactionId(transactionCode); // Đặt text cho label ID

            _view.ShowView();
            return await _processCompletionSource.Task;
        }
        public void ApplyResult(PaymentResult result)
        {
            if (string.IsNullOrEmpty(_currentTransactionId))
            {
                return;
            }
            if (result.Id != _currentTransactionId)
            {
                return;
            }
            _kioskUserType = EmKioskUserType.System;
            bool isConfirm = result.Status == EmOrderStatus.SUCCESS;
            _view.GotResult();
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = isConfirm, kioskUserType = _kioskUserType });
        }
        public void Translate()
        {
            _view.SetLblTransactionTitle(KZUIStyles.CurrentResources.TransactionId);
            _view.SetLblTitle(KZUIStyles.CurrentResources.PaymentVisaTitle);
            _view.SetLblGuide(KZUIStyles.CurrentResources.PaymentVisaSubTitle);
            _view.SetBtnBackText(KZUIStyles.CurrentResources.Back);
        }
    }
}
