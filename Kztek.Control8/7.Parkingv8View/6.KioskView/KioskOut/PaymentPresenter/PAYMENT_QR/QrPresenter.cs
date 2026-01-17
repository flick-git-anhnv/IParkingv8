using iParkingv8.Object.Enums.Payments;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.Style;
using QRCoder;

namespace Kztek.Control8.KioskOut.PaymentPresenter.QR
{
    public class QrPresenter
    {
        private readonly IQRView _view;
        private Transaction? _currentQR = null;
        private TaskCompletionSource<BaseKioskResult> _processCompletionSource;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer;
        private PaymentKioskConfig config;
        public QrPresenter(IQRView qrView, PaymentKioskConfig config)
        {
            _view = qrView;
            _view.OnBackClicked += QrView_OnBackClicked;
            this.config = config;
            Translate();
        }

        private void QrView_OnBackClicked(object? sender, EventArgs e)
        {
            _view.HideView();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }

        public async Task<BaseKioskResult> ShowQR(Transaction qr)
        {
            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            _currentQR = qr;
            var qrData = GenerateQRCodeImage(qr.QrCode);
            _view.DisplayQRImage(qrData);
            _view.ShowView();
            return await _processCompletionSource.Task;
        }

        public void ApplyResult(PaymentResult result)
        {
            if (_currentQR == null)
            {
                return;
            }
            if (result.Id != _currentQR.Id)
            {
                return;
            }
            _kioskUserType = EmKioskUserType.System;
            bool isConfirm = result.Status == EmOrderStatus.SUCCESS;
            _view.HideView();
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = isConfirm, kioskUserType = _kioskUserType });
        }

        public void Translate()
        {
            _view.SetLblGuide(KZUIStyles.CurrentResources.PaymentQrTitle);
            _view.SetBtnBackText(KZUIStyles.CurrentResources.Back);
        }
        public Bitmap GenerateQRCodeImage(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    return qrCodeImage;
                }
            }
        }
    }
}
