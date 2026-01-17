using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskIn.DashBoard
{
    public partial class ucKioskInDashBoard : UserControl, KzITranslate
    {
        public ucKioskInDashBoard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
        }

        public void Init(Lane lane, EventHandler OnRetakeImageClick)
        {
            ucKioskOutIdentityMonthGuide1.Init(lane, OnRetakeImageClick);
            ucKioskOutIdentityDailyGuide1.Init();

            lblTitle.Text = KZUIStyles.CurrentResources.KioskInDashboardTitle;
            lblSubTitle.Text = KZUIStyles.CurrentResources.KioskInDashboardSubTitle;
            this.ucPaymentSupport1.Translate();
        }
        public void Translate()
        {
            ucKioskOutIdentityMonthGuide1.Translate();
            ucKioskOutIdentityDailyGuide1.Translate();

            lblTitle.Text = KZUIStyles.CurrentResources.KioskInDashboardTitle;
            lblSubTitle.Text = KZUIStyles.CurrentResources.KioskInDashboardSubTitle;
            lblPaymentSupport.Text = KZUIStyles.CurrentResources.PaymentSupport;

            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            lblPaymentSupport.Left = (this.ClientSize.Width - lblPaymentSupport.Width) / 2;
            this.ucPaymentSupport1.Translate();
        }
    }
}
