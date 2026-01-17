using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.UserControls;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.ConfigUcs;
using Kztek.Control8.UserControls.ConfigUcs.CameraConfigs;
using Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs;
using Kztek.Control8.UserControls.ConfigUcs.LaneConfigs;
using Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs;
using Kztek.Object;

namespace IParkingv8.Forms.SystemForms
{
    public partial class FrmLaneSetting : Form, KzITranslate
    {
        #region Properties
        private IEnumerable<Led> leds = [];
        private IEnumerable<Camera> cameras = [];
        private List<ControllerInLane> controllerInLanes = [];
        private ILane lane;
        private bool isLaneIn = false;
        private LaneOptionalConfig laneOptionalConfig;
        private EmControlSizeMode sizeMode;
        private Size viewSize;
        #endregion End Properties

        #region Forms
        public FrmLaneSetting(ILane lane, IEnumerable<Led> leds, IEnumerable<Camera> cameras, List<ControllerInLane> controllerInLanes, bool isLaneIn, LaneOptionalConfig laneOptionalConfig, EmControlSizeMode sizeMode, Size viewSize)
        {
            InitializeComponent();
            this.sizeMode = sizeMode;
            this.lane = lane;
            this.viewSize = viewSize;
            InitPropeties(leds, cameras, controllerInLanes, isLaneIn, laneOptionalConfig);
            Translate();

            this.Load += FrmLaneSetting_Load;
        }
        private async void FrmLaneSetting_Load(object? sender, EventArgs e)
        {
            await InitUI();
        }
        #endregion End Forms

        private void InitPropeties(IEnumerable<Led> leds, IEnumerable<Camera> cameras, List<ControllerInLane> controllerInLanes, bool isLaneIn, LaneOptionalConfig laneOptionalConfig)
        {
            this.leds = leds;
            this.cameras = cameras;
            this.controllerInLanes = controllerInLanes;
            this.laneOptionalConfig = laneOptionalConfig;
            this.isLaneIn = isLaneIn;

            this.Text += " " + this.lane.Lane.Name;
        }
        public void Translate()
        {
            tabLed.Text = KZUIStyles.CurrentResources.Led;
            tabCamera.Text = KZUIStyles.CurrentResources.Camera;
            tabShortcut.Text = KZUIStyles.CurrentResources.Shortcut;
            tabController.Text = KZUIStyles.CurrentResources.ControllerSetting;
            tabOption.Text = KZUIStyles.CurrentResources.OptionSetting;
            tabDisplaySetting.Text = KZUIStyles.CurrentResources.DisplaySetting;
        }
        private async Task InitUI()
        {
            ucLedDisplaySetting uc = new(this.lane.Lane.Id, leds);
            tabLed.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

            UcCameraConfig ucc = new(this.cameras, this.lane.Lane.Id, AppData.LprDetecter[0]);
            tabCamera.Controls.Add(ucc);
            ucc.Dock = DockStyle.Fill;

            ucShortcutConfig ucLaneShortcutConfig = new(this.lane.Lane.Id, this.controllerInLanes, this.isLaneIn);
            tabShortcut.Controls.Add(ucLaneShortcutConfig);
            ucLaneShortcutConfig.Dock = DockStyle.Fill;

            UcControllerCardFormat ucControllerCardFormat = new(this.controllerInLanes, this.lane.Lane.Id, AppData.AccessKeyCollections, AppData.Controllers);
            tabController.Controls.Add(ucControllerCardFormat);
            ucControllerCardFormat.Dock = DockStyle.Fill;

            UcLaneOptionalConfig ucLaneOptionalConfig = new(this.lane.Lane.Id, laneOptionalConfig, AppData.AccessKeyCollections);
            tabOption.Controls.Add(ucLaneOptionalConfig);
            ucLaneOptionalConfig.Dock = DockStyle.Fill;

            UcLaneDirectionViewConfig ucLaneDirectionViewConfig = new UcLaneDirectionViewConfig(this.lane, this.sizeMode, this.viewSize);
            tabDisplaySetting.Controls.Add(ucLaneDirectionViewConfig);
            ucLaneDirectionViewConfig.Dock = DockStyle.Fill;
        }
    }
}
