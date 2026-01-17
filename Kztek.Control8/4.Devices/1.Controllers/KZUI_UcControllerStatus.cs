using iParkingv5.Controller.Aopu;
using iParkingv8.Ultility;
using Kztek.Control8.Forms;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcControllerStatus : UserControl
    {
        #region Properties
        private System.Timers.Timer? timerCheckControllerConnection;
        private iParkingv5.Controller.IController controller;
        private bool connected = true;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ Connection Status"), Description("Connection Status")]
        public bool KZUI_ConnectionStatus
        {
            get => connected;
            set
            {
                SetControllerStatus(value);
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ Name"), Description("Name")]
        public string KZUI_ControllerName
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
        public KZUI_UcControllerStatus()
        {
            InitializeComponent();

            this.KZUI_ConnectionStatus = true;
            timerCheckControllerConnection = new System.Timers.Timer();
            timerCheckControllerConnection.Elapsed += TimerCheckControllerConnection_Tick;
            timerCheckControllerConnection.Interval = 5000;

            lblName.DoubleClick += LblName_DoubleClick;
        }
        #endregion

        #region Controls In Form
        private void LblName_DoubleClick(object? sender, EventArgs e)
        {
            new FrmTestController(this.controller).ShowDialog();
        }
        #endregion

        #region Public Functions
        public void Init(iParkingv5.Controller.IController bdk)
        {
            this.KZUI_ControllerName = bdk.ControllerInfo.Name;
            this.KZUI_ConnectionStatus = false;
            this.controller = bdk;

            if (this.controller.ControllerInfo.IsConnect)
            {
                if (iconPic.IconColor != ColorManagement.SuccessColor)
                {
                    iconPic.IconColor = ColorManagement.SuccessColor;
                }
            }
            else
            {
                if (iconPic.IconColor != ColorManagement.ErrorColor)
                {
                    iconPic.IconColor = ColorManagement.ErrorColor;
                }
            }

            StartTimer();
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
                if (this.controller is AopuController)
                {
                    this.KZUI_ConnectionStatus = await this.controller.TestConnectionAsync();
                }
                else
                {
                    this.KZUI_ConnectionStatus = this.controller is not null && this.controller.ControllerInfo.IsConnect;
                }
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
