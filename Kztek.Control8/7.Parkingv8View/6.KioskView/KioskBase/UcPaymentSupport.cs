using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class ucPaymentSupport : UserControl, KzITranslate
    {

        public ucPaymentSupport()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true);
            Translate();
        }

        public void Translate()
        {
            lblCashTitle.SetText(KZUIStyles.CurrentResources.Cash);
            lblVoucherTitle.SetText(KZUIStyles.CurrentResources.Voucher);
            lblQrCodeTitle.SetText(KZUIStyles.CurrentResources.QR);
            //lblVisaTitle.Text = KzDictionary.GetDisplayText(languageCode, EmLanguageKey.BTN_VISA);
        }
    }
}
