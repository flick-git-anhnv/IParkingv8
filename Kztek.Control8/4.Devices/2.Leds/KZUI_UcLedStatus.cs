using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcLedStatus : UserControl
    {
        #region Properties
        private readonly System.Timers.Timer? timerCheckControllerConnection;
        private Led? Led;

        private bool connected = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★KZUI Status"), Description("Device Status")]
        public bool KZUI_ConnectionStatus
        {
            get => connected;
            set
            {
                SetControllerStatus(value);
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★KZUI Name"), Description("Device Name")]
        public string KZUI_Name
        {
            get => lblName.Text;
            set
            {
                lblName.AutoSize = true;
                lblName.Text = value;
                int width = lblName.Width;
                this.Width = iconPic.Width + width;
                lblName.AutoSize = false;
            }
        }
        #endregion

        #region Constructor
        public KZUI_UcLedStatus()
        {
            InitializeComponent();

            timerCheckControllerConnection = new System.Timers.Timer();
            timerCheckControllerConnection.Elapsed += TimerCheckControllerConnection_Tick;
            timerCheckControllerConnection.Interval = 5000;

            lblName.DoubleClick += LblName_DoubleClick;
        }
        #endregion

        #region Controls In Form
        private void LblName_DoubleClick(object? sender, EventArgs e)
        {
        }
        #endregion

        #region Public Functions
        public void Init(Led led)
        {
            this.KZUI_Name = led.name;
            this.KZUI_ConnectionStatus = false;
            this.Led = led;
        }
        public void StartTimer()
        {
            timerCheckControllerConnection?.Start();
        }
        public void StopTimer()
        {
            timerCheckControllerConnection?.Stop();
        }
        #endregion

        #region Private Functions
        private void SetControllerStatus(bool isConnect)
        {
            this.connected = isConnect;
            var color = isConnect ? ColorManagement.SuccessColor : ColorManagement.ErrorColor;
            ChangeIconColor(color);
        }
        private void ChangeIconColor(Color color)
        {
            if (this.IsHandleCreated && iconPic.InvokeRequired)
            {
                iconPic.BeginInvoke(new Action(() =>
                {
                    ChangeIconColorInternal(color);
                }));
            }
            else
            {
                ChangeIconColorInternal(color);
            }
        }
        private void ChangeIconColorInternal(Color color)
        {
            if (iconPic.IconColor != color)
            {
                iconPic.IconColor = color;
                iconPic.Refresh();
            }
            if (lblName.ForeColor != color)
            {
                lblName.ForeColor = color;
                lblName.Refresh();
            }
        }
        #endregion

        #region Timer
        private async void TimerCheckControllerConnection_Tick(object? sender, EventArgs e)
        {
            timerCheckControllerConnection!.Enabled = false;
            try
            {
                this.KZUI_ConnectionStatus = Led is not null && await NetWorkTools.IsPingSuccessAsync(Led.comport, 200);
            }
            catch (Exception)
            {
            }
            finally
            {
                timerCheckControllerConnection.Enabled = true;
            }
        }
        #endregion
    }
}
