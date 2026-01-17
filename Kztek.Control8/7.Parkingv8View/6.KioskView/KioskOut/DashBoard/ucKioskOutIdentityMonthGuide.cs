using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;

namespace Kztek.Control8.UserControls.KioskOut
{
    public partial class ucKioskOutIdentityMonthGuide : UserControl, KzITranslate
    {
        private Lane lane;
        public event EventHandler OnRetakeImageClick;
        public ucKioskOutIdentityMonthGuide()
        {
            InitializeComponent();
        }

        public void Translate()
        {
            btnCapture.Text = KZUIStyles.CurrentResources.RetakeImage;
            lblTitle.Text = KZUIStyles.CurrentResources.KioskMonthlyCard;
            lblGuide.Text = lane.Loop ? KZUIStyles.CurrentResources.KIOSK_OUT_MONTH_CARD_SUBTITLE : 
                                        KZUIStyles.CurrentResources.KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE;
        }
        public void Init(Lane lane, EventHandler OnRetakeImageClick)
        {
            this.lane = lane;

            btnCapture.Visible = lane?.Loop == true;
            Translate();
            if (OnRetakeImageClick != null)
            {
                btnCapture.Click -= OnRetakeImageClick;
                btnCapture.Click += OnRetakeImageClick;
            }
        }
    }
}
