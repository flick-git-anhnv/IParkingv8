using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskIn.DashBoard
{
    public partial class ucKioskInIdentityDailyGuide : UserControl
    {
        public ucKioskInIdentityDailyGuide()
        {
            InitializeComponent();
        }
        public void Init()
        {
            Translate();
        }

        public void Translate()
        {
            lblTitle.Text = KZUIStyles.CurrentResources.KioskDailyCard;
            lblSubTitle.Text = KZUIStyles.CurrentResources.KIOSK_IN_DAILY_CARD_SUBTITLE;
        }
    }
}
