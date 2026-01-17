using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_Function : UserControl
    {
        public bool KZUI_IsDisplayWriteTicket
        {
            get => panelWriteTicket.Visible;
            set
            {
                panelWriteTicket.Visible = value;
                panelMain.RefreshUI();
            }
        }
        public bool KZUI_IsDisplayGuestRegister
        {
            get => panelRegisterClient.Visible;
            set
            {
                panelRegisterClient.Visible = value;
                panelMain.RefreshUI();
            }
        }
        public bool KZUI_IsDisplayRetakeImage
        {
            get => panelRetakeImage.Visible;
            set
            {
                panelRetakeImage.Visible = value;
                panelMain.RefreshUI();
            }
        }
        public bool KZUI_IsDisplayOpenBarrie
        {
            get => panelOpenBarrie.Visible;
            set
            {
                panelOpenBarrie.Visible = value;
                panelMain.RefreshUI();
            }
        }
        public bool KZUI_IsDisplayCloseBarrie
        {
            get => pnlCloseBarrie.Visible;
            set
            {
                pnlCloseBarrie.Visible = value;
                panelMain.RefreshUI();
            }
        }
        public bool KZUI_IsDisplayPrint
        {
            get => panelPrintTicket.Visible;
            set
            {
                panelPrintTicket.Visible = value;
                panelMain.RefreshUI();
            }
        }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"),
                           Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        btnWriteTicket.Font = btnRetakeImage.Font = btnOpenBarrie.Font =
                            new Font(btnWriteTicket.Font.Name, SizeManagement.SMALL_FONT_SIZE, btnWriteTicket.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.SMALL_HEIGHT);
                        break;
                    case EmControlSizeMode.MEDIUM:
                        btnWriteTicket.Font = btnRetakeImage.Font = btnOpenBarrie.Font =
                            new Font(btnWriteTicket.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, btnWriteTicket.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.MEDIUM_HEIGHT);
                        break;
                    case EmControlSizeMode.LARGE:
                        btnWriteTicket.Font = btnRetakeImage.Font = btnOpenBarrie.Font =
                            new Font(btnWriteTicket.Font.Name, SizeManagement.LARRGE_HEIGHT, btnWriteTicket.Font.Style, GraphicsUnit.Pixel);
                        this.MinimumSize = this.MaximumSize = new Size(0, SizeManagement.LARRGE_HEIGHT);
                        break;
                    default:
                        break;
                }
            }
        }

        private EmLaneType laneType = EmLaneType.LANE_IN;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Lane Type"), Description("LaneType")]
        public EmLaneType KZUI_LaneType
        {
            get => laneType;
            set
            {
                laneType = value;
                switch (value)
                {
                    case EmLaneType.LANE_IN:
                        panelPrintTicket.Visible = false;
                        break;
                    case EmLaneType.KIOSK_IN:
                        panelPrintTicket.Visible = false;
                        break;
                    case EmLaneType.LANE_OUT:
                        panelPrintTicket.Visible = true;
                        break;
                    case EmLaneType.KIOSK_OUT:
                        break;
                    default:
                        break;
                }
                panelMain.RefreshUI(panelMain.Spacing);
            }
        }

        #region Properties
        public Func<object?, Task<bool>>? OnOpenBarrieClick;
        public Func<object?, Task<bool>>? OnCloseBarrieClick;
        public Func<object?, Task<bool>>? OnRetakeImageClick;
        public Func<object?, Task<bool>>? OnWriteTicketClick;
        public Func<object?, Task<bool>>? OnPrintClick;
        public Func<object?, Task<bool>>? OnRegisterClient;
        private Lane lane;
        #endregion End Properties

        #region Constructor
        public KZUI_Function()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KZUI_ControlSizeMode = EmControlSizeMode.MEDIUM;
            btnOpenBarrie.Click += BtnOpenBarrie_Click;
            btnRetakeImage.Click += BtnRetakeImage_Click;
            btnWriteTicket.Click += BtnBinhChungThongTin_Click;
            btnPrint.Click += BtnPrint_Click;
            btnRegisterClient.Click += BtnRegisterClient_Click;
            btnCloseBarrie.Click += BtnCloseBarrie_Click;
        }

        #endregion End Constructor

        #region Controls In Form
        private async void BtnPrint_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnPrintClick != null)
            {
                await OnPrintClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void BtnRetakeImage_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnRetakeImageClick != null)
            {
                await OnRetakeImageClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void BtnOpenBarrie_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnOpenBarrieClick != null)
            {
                await OnOpenBarrieClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void BtnBinhChungThongTin_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnWriteTicketClick != null)
            {
                await OnWriteTicketClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void BtnRegisterClient_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (this.OnRegisterClient != null)
            {
                await OnRegisterClient(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void BtnCloseBarrie_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (this.OnCloseBarrieClick != null)
            {
                await OnCloseBarrieClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        #endregion End Controls In Form

        #region Public Function
        public void InitView(Lane lane, string openBarrieTitle, string writeTicketTitle, string retakeImageTitle, string printTitle, string registerClientTitle)
        {
            this.lane = lane;
            btnOpenBarrie.Text = openBarrieTitle;
            btnWriteTicket.Text = writeTicketTitle;
            btnRetakeImage.Text = retakeImageTitle;
            btnPrint.Text = printTitle;
            btnRegisterClient.Text = registerClientTitle;
            this.KZUI_LaneType = (EmLaneType)lane.Type;
            panelRetakeImage.Visible = lane.Loop;
            panelMain.RefreshUI(panelMain.Spacing);
        }
        public void InitFunction(Func<object?, Task<bool>>? OnOpenBarrieClick, Func<object?, Task<bool>>? OnRetakeImageClick,
                                 Func<object?, Task<bool>>? OnWriteInClick, Func<object?, Task<bool>>? OnPrintClick,
                                 Func<object?, Task<bool>>? OnRegisterClient, Func<object?, Task<bool>>? OnCloseBarrieClick)
        {
            this.OnOpenBarrieClick += OnOpenBarrieClick;
            this.OnRetakeImageClick += OnRetakeImageClick;
            this.OnWriteTicketClick += OnWriteInClick;
            this.OnPrintClick += OnPrintClick;
            this.OnRegisterClient += OnRegisterClient;
            this.OnCloseBarrieClick += OnCloseBarrieClick;
        }
        public void RefreshUI()
        {
            panelMain.RefreshUI(panelMain.Spacing);
        }

        public void Init(EmLaneType laneType, EmControlSizeMode sizeMode)
        {
            this.KZUI_ControlSizeMode = sizeMode;
            this.KZUI_LaneType = laneType;
        }
        #endregion End Public Function
    }
}