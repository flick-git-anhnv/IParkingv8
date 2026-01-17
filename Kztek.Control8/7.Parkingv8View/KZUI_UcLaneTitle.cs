using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcLaneTitle : UserControl
    {
        public Func<object?, Task<bool>>? OnOpenBarrieClick;
        public Func<object?, Task<bool>>? OnWriteTicketClick;
        public Func<object?, Task<bool>>? OnRetakeImageClick;
        public Func<object?, bool>? OnSettingClick;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Title"), Description("Display Lane Title")]
        public string KZUI_Title
        {
            get => lblTitle.Text;
            set
            {
                try
                {
                    lblTitle.Text = value;
                    lblTitle.Width = lblTitle.PreferredWidth;
                    lblTitle.Refresh();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        panelMain.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        panelMain.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        break;
                    case EmControlSizeMode.LARGE:
                        panelMain.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        break;
                    default:
                        break;
                }
                lblTitle.Width = lblTitle.PreferredWidth;
                picOpenBarrie.Size = picRetakeImage.Size = picSetting.Size = picWriteTicket.Size =
                    new Size(picOpenBarrie.Height, picOpenBarrie.Height);
            }
        }

        private Lane lane;

        public KZUI_UcLaneTitle()
        {
            InitializeComponent();
            this.BackColor = ColorManagement.AppBackgroudColor;
            panelMain.BackColor = ColorManagement.AppBackgroudColor;
            panelMain.FillColor = ColorManagement.LaneInColor;
            this.KZUI_ControlSizeMode = EmControlSizeMode.MEDIUM;
            picOpenBarrie.Click += PicOpenBarrie_Click;
            picRetakeImage.Click += PicRetakeImage_Click;
            picSetting.Click += PicSetting_Click;
            picWriteTicket.Click += PicWriteTicket_Click;
        }
        public void Init(Lane lane,
                        Func<object?, Task<bool>>? OnOpenBarrieClick = null,
                        Func<object?, Task<bool>>? OnWriteTicketClick = null,
                        Func<object?, Task<bool>>? OnRetakeImageClick = null,
                        Func<object?, bool>? OnSettingClick = null)
        {
            this.lane = lane;
            this.KZUI_Title = lane.Name;
            this.OnOpenBarrieClick += OnOpenBarrieClick;
            this.OnWriteTicketClick += OnWriteTicketClick;
            this.OnRetakeImageClick += OnRetakeImageClick;
            this.OnSettingClick += OnSettingClick;

            if (!lane.Loop)
            {
                picRetakeImage.Visible = false;
                panelPics.ColumnStyles[3].Width = 0;
            }
            switch ((EmLaneType)lane.Type)
            {
                case EmLaneType.LANE_IN:
                case EmLaneType.KIOSK_IN:
                    SetBackColor(ColorManagement.LaneInColor);
                    panelMain.FillColor = lblTitle.BackColor = panelPics.BackColor = ColorManagement.LaneInColor;
                    break;
                case EmLaneType.LANE_OUT:
                case EmLaneType.KIOSK_OUT:
                    SetBackColor(ColorManagement.LaneOutColor);
                    break;
                default:
                    break;
            }
        }

        private async void PicWriteTicket_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnWriteTicketClick != null)
            {
                await OnWriteTicketClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void PicSetting_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            OnSettingClick?.Invoke(sender);
            if (control != null) control.Enabled = true;
        }
        private async void PicRetakeImage_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnRetakeImageClick != null)
            {
                await OnRetakeImageClick(sender);
            }
            if (control != null) control.Enabled = true;
        }
        private async void PicOpenBarrie_Click(object? sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null) control.Enabled = false;
            if (OnOpenBarrieClick != null)
            {
                await OnOpenBarrieClick(sender);
            }
            if (control != null) control.Enabled = true;
        }

        private void SetBackColor(Color color)
        {
            panelMain.FillColor = panelMain.BorderColor =
            lblTitle.BackColor = panelPics.BackColor = color;
        }

        public void UpdateSetting(LaneDirectionConfig? laneDirectionConfig)
        {
            if (laneDirectionConfig == null)
            {
                return;
            }
            picOpenBarrie.Visible = laneDirectionConfig.IsDisplayOpenBarrieButton;
            picRetakeImage.Visible = this.lane.Loop && laneDirectionConfig.IsDisplayRetakeImageButton;
            picWriteTicket.Visible = laneDirectionConfig.IsDisplayWriteEventButton;
        }

        public void NotifyCardEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = true;
                picLoopEvent.Visible = false;
                picMotion.Visible = false;
            }));
        }
        public void NotifyLoopEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = false;
                picLoopEvent.Visible = true;
                picMotion.Visible = false;
            }));
        }
        public void NotifyMotionEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = false;
                picLoopEvent.Visible = false;
                picMotion.Visible = true;
            }));
        }
        public void NotifyNoneEvent()
        {
            picCardEvent.BeginInvoke(new Action(() =>
            {
                picCardEvent.Visible = false;
                picLoopEvent.Visible = false;
                picMotion.Visible = false;
            }));
        }
    }
}
