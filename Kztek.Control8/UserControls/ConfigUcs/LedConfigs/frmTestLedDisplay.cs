using iParkingv5.LedDisplay.LEDs;

namespace Kztek.Control8.UserControls.ConfigUcs.LedConfigs
{
    public partial class frmTestLedDisplay : Form
    {
        public ParkingData TestData
        {
            get
            {
                string cardNumber = "";
                string cardNo = "";
                string cardType = "";
                string eventStatus = "";
                string plate = "";
                DateTime datetimeIn = DateTime.Now;
                DateTime datetimeOut = DateTime.Now;
                string money = "";
                return new ParkingData(cardNumber, cardNo, cardType, eventStatus, plate, datetimeIn, datetimeOut, money);
            }
        }
        public frmTestLedDisplay()
        {
            InitializeComponent();
        }
    }
}
