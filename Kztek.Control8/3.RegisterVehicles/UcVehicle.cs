using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.Style;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class UcVehicle : UserControl
    {
        public delegate void OnSelectedVehicleEventHandler(UcVehicle sender);
        public event OnSelectedVehicleEventHandler? OnSelectedEvent;

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Checked"), Description("Trạng thái")]
        public bool KZUI_Checked
        {
            get => radioStatus.Checked;
            set
            {
                radioStatus.CheckedChanged -= OnClickEventHandler;
                radioStatus.Checked = value;
                radioStatus.CheckedChanged += OnClickEventHandler;
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Plate Number"), Description("Tiêu để")]
        public string KZUI_PlateNumber
        {
            get => lblPlateNumberTitle.Text;
            set => lblPlateNumberTitle.Text = value;
        }

        private EmAccessKeyStatus kZUI_Status = EmAccessKeyStatus.IN_USE;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Status"), Description("Số lượng")]
        public EmAccessKeyStatus KZUI_Status
        {
            get => kZUI_Status;
            set
            {
                kZUI_Status = value;
                SetStatus(value);
            }
        }

        public AccessKey vehicle;
        public UcVehicle(AccessKey vehicle)
        {
            InitializeComponent();

            InitProperties(vehicle);
            Translate();
            InitUI(vehicle);
        }

        private void OnClickEventHandler(object? sender, EventArgs e)
        {
            this.KZUI_Checked = true;
            OnSelectedEvent?.Invoke(this);
        }
        private void SetStatus(EmAccessKeyStatus status)
        {
            lblStatus.Text = AccessKeyStatus.ToDisplayString(status);
            switch (status)
            {
                case EmAccessKeyStatus.LOCKED:
                    lblStatus.ForeColor = Color.DarkOrange;
                    break;
                case EmAccessKeyStatus.IN_USE:
                    lblStatus.ForeColor = Color.DarkGreen;
                    break;
                case EmAccessKeyStatus.UN_USED:
                    lblStatus.ForeColor = Color.DarkRed;
                    break;
                default:
                    break;
            }
        }

        private void InitProperties(AccessKey vehicle)
        {
            this.vehicle = vehicle;
        }
        private void Translate()
        {
            lblPlateNumberTitle.Text = KZUIStyles.CurrentResources.VehicleCodeAcronym;
            lblStatusTitle.Text = KZUIStyles.CurrentResources.Status;
        }
        private void InitUI(AccessKey vehicle)
        {
            lblPlateNumber.Text = vehicle.Code;
            SetStatus(vehicle.Status);

            pnlMain.Click += OnClickEventHandler;
            lblPlateNumberTitle.Click += OnClickEventHandler;
            lblPlateNumber.Click += OnClickEventHandler;
            lblStatusTitle.Click += OnClickEventHandler;
            lblStatus.Click += OnClickEventHandler;
            radioStatus.CheckedChanged += OnClickEventHandler;
        }
    }
}
