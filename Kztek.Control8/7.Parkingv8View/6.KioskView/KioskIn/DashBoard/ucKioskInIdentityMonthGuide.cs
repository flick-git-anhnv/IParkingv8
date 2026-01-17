using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.KioskIn.DashBoard
{
    public partial class ucKioskInIdentityMonthGuide : UserControl
    {
        private Lane lane = new Lane();
        public event EventHandler? OnRetakeImageClick;

        public ucKioskInIdentityMonthGuide()
        {
            InitializeComponent();
        }

        public void Init(Lane lane, EventHandler? onRetakeImageClick)
        {
            this.lane = lane;

            btnCapture.Visible = lane?.Loop == true;

            Translate();

            if (onRetakeImageClick != null)
            {
                btnCapture.Click -= onRetakeImageClick;
                btnCapture.Click += onRetakeImageClick;
            }
        }

        public void Translate()
        {
            btnCapture.Text = KZUIStyles.CurrentResources.RetakeImage;
            lblTitle.Text = KZUIStyles.CurrentResources.MonthVehicle;
            lblSubTitle.Text = lane?.Loop == true
                                ? KZUIStyles.CurrentResources.KIOSK_OUT_MONTH_CARD_SUBTITLE
                                : KZUIStyles.CurrentResources.KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE;
        }
    }
}
