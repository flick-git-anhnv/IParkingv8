using iParkingv8.Object.ConfigObjects.ControllerConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    public partial class ucControllerConfigItem : UserControl
    {
        #region Properties
        private ControllerInLane controllerInLane;
        private string laneId;
        public List<Bdk> allBdks;
        #endregion End Properties

        #region Forms
        public ucControllerConfigItem(ControllerInLane controllerInLane, string laneId, List<Bdk> allBdks)
        {
            InitializeComponent();
            this.controllerInLane = controllerInLane;
            this.allBdks = allBdks;
            this.laneId = laneId;
            this.Load += UcControllerConfigItem_Load;
        }

        private void UcControllerConfigItem_Load(object? sender, EventArgs e)
        {
            string controllerName = string.Empty;
            foreach (var item in this.allBdks)
            {
                if (item.Id.ToLower() == this.controllerInLane.Id.ToLower())
                {
                    controllerName = item.Name;
                    break;
                }
            }
            lblControllerName.Text = controllerName;

            List<ControllerShortcutConfig>? controllerShortcutConfigs = NewtonSoftHelper<List<ControllerShortcutConfig>>.
                DeserializeObjectFromPath(IparkingingPathManagement.laneControllerShortcutConfigPath(
                                                    this.laneId));
            if (controllerShortcutConfigs == null)
            {
                foreach (var item in this.controllerInLane.Barriers)
                {
                    ucControllerConfigBarrieItem ucBarrieItem = new ucControllerConfigBarrieItem(item, null);
                    this.Controls.Add(ucBarrieItem);
                    ucBarrieItem.Dock = DockStyle.Top;
                    ucBarrieItem.BringToFront();
                }
            }
            else
            {
                bool isFound = false;
                foreach (var item in controllerShortcutConfigs)
                {
                    if (item.ControllerId == this.controllerInLane.Id)
                    {
                        isFound = true;
                        foreach (var barrieItem in this.controllerInLane.Barriers)
                        {
                            ucControllerConfigBarrieItem ucBarrieItem = new(barrieItem,
                                                                            item.KeySetByRelays.ContainsKey(barrieItem) ?
                                                                            (Keys)item.KeySetByRelays[barrieItem] :
                                                                            null);
                            this.Controls.Add(ucBarrieItem);
                            ucBarrieItem.Dock = DockStyle.Top;
                            ucBarrieItem.BringToFront();
                        }
                        break;
                    }
                }
                if (!isFound)
                {
                    foreach (var item in this.controllerInLane.Barriers)
                    {
                        ucControllerConfigBarrieItem ucBarrieItem = new ucControllerConfigBarrieItem(item, null);
                        this.Controls.Add(ucBarrieItem);
                        ucBarrieItem.Dock = DockStyle.Top;
                        ucBarrieItem.BringToFront();
                    }
                }
            }

        }
        #endregion

        #region Public Function
        public ControllerShortcutConfig GetConfig()
        {
            var newConfig = new ControllerShortcutConfig();
            newConfig.ControllerId = this.controllerInLane.Id;
            newConfig.KeySetByRelays = new Dictionary<int, int>();

            foreach (var item in this.Controls)
            {
                if (item is ucControllerConfigBarrieItem)
                {
                    ucControllerConfigBarrieItem? _temp = item as ucControllerConfigBarrieItem;
                    if (_temp != null)
                    {
                        if (_temp.keySet != null)
                        {
                            newConfig.KeySetByRelays.Add(_temp.barrieIndex, (int)_temp.keySet);
                        }
                    }
                }
            }
            return newConfig;
        }
        #endregion

    }
}
