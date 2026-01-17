using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class ucKioskOutDashboard : UserControl, KzITranslate
    {
        public ucKioskOutDashboard()
        {
            InitializeComponent();
        }

        public void Init(Lane lane, EventHandler OnRetakeImageClick)
        {
            ucKioskOutIdentityMonthGuide1.Init(lane, OnRetakeImageClick);
            ucKioskOutIdentityDailyGuide1.Init(lane);
            Translate();
        }
        public void Translate()
        {
            ucKioskOutIdentityMonthGuide1.Translate();
            ucKioskOutIdentityDailyGuide1.Translate();

            lblTitle.Text = KZUIStyles.CurrentResources.KioskOutDashboardTitle;
            lblSubTitle.Text = KZUIStyles.CurrentResources.KioskOutDashboardSubTitle;
            lblPaymentSupport.Text = KZUIStyles.CurrentResources.PaymentSupport;

            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            lblPaymentSupport.Left = (this.ClientSize.Width - lblPaymentSupport.Width) / 2;
            this.ucPaymentSupport1.Translate();
        }
    }
}
