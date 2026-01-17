using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.KioskOut
{
    public partial class ucKioskOutIdentityDailyGuide : UserControl, KzITranslate
    {
        public ucKioskOutIdentityDailyGuide()
        {
            InitializeComponent();
        }

        public void Translate()
        {
            lblDailyCardTitle.Text = KZUIStyles.CurrentResources.KioskDailyCard;
            lblDailyGuide.Text = KZUIStyles.CurrentResources.KIOSK_OUT_DAILY_CARD_SUBTITLE;
        }
        public void Init(Lane lane)
        {
            Translate();
        }
    }
}
