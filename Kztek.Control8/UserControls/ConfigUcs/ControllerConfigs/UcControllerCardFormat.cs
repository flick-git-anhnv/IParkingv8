using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Object;
using Kztek.Object.Entity.Device;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    public partial class UcControllerCardFormat : UserControl, KzITranslate
    {
        #region Properties
        private readonly List<ControllerInLane> controllerInLanes = [];
        private readonly string laneId;
        private ControllerInLane? settingController = default;
        private readonly List<UcControllerReaderCardFormat> controllerReaderCardFormats = [];
        private readonly List<UcControllerBarrieConfig> controllerBarrieOpenConfigs = [];
        private readonly List<Collection> collections = [];
        private readonly IEnumerable<Bdk> bdks = [];
        #endregion

        #region Forms
        public UcControllerCardFormat(List<ControllerInLane> controllerInLanes, string laneId, List<Collection> collections, IEnumerable<Bdk> bdks)
        {
            InitializeComponent();
            this.controllerInLanes = controllerInLanes;
            this.bdks = bdks;
            this.laneId = laneId;
            this.collections = collections;
            this.Load += UcControllerReaderFormat_Load;
        }
        private void UcControllerReaderFormat_Load(object? sender, EventArgs e)
        {
            Translate();
            foreach (var item in this.controllerInLanes)
            {
                var bdk = bdks.FirstOrDefault(e => e.Id.Equals(item.Id, StringComparison.CurrentCultureIgnoreCase));
                if (bdk != null)
                {
                    ListItem controllerItem = new()
                    {
                        Name = bdk.Id,
                        Value = bdk.Name
                    };
                    cbController.Items.Add(controllerItem);
                }
            }
            cbController.DisplayMember = "Value";
            cbController.ValueMember = "Name";
            cbController.SelectedIndex = cbController.Items.Count > 0 ? 0 : -1;
        }
        #endregion

        #region Controls In Form
        private void CbController_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbController.SelectedItem is null)
            {
                return;
            }
            ListItem selectedItem = (ListItem)cbController.SelectedItem;
            if (selectedItem is null)
            {
                return;
            }
            settingController = this.controllerInLanes.Where(e => e.Id == selectedItem.Name).FirstOrDefault();
            if (settingController is null)
            {
                return;
            }

            this.controllerReaderCardFormats.Clear();
            foreach (UcControllerReaderCardFormat item in panelReaderConfigs.Controls)
            {
                item.Dispose();
            }
            panelReaderConfigs.Controls.Clear();
            for (int i = 0; i < settingController.Readers.Count; i++)
            {
                var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(this.laneId, settingController.Id, settingController.Readers[i]);
                var oldConfig = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                new CardFormatConfig()
                                {
                                    ReaderIndex = settingController.Readers[i],
                                    OutputOption = CardFormat.EmCardFormatOption.Toi_Gian,
                                };
                UcControllerReaderCardFormat uc = new(oldConfig, this.collections);
                panelReaderConfigs.Controls.Add(uc);
                uc.Dock = DockStyle.Top;
                uc.SendToBack();
                this.controllerReaderCardFormats.Add(uc);
            }

            this.controllerBarrieOpenConfigs.Clear();
            foreach (UcControllerBarrieConfig item in panelBarrieConfigs.Controls)
            {
                item.Dispose();
            }
            panelBarrieConfigs.Controls.Clear();
            for (int i = 0; i < settingController.Barriers.Count; i++)
            {
                var configPath = IparkingingPathManagement.laneControllerBarrieConfigPath(this.laneId, settingController.Id, settingController.Barriers[i]);
                var oldConfig = NewtonSoftHelper<BarrieOpenModeConfig>.DeserializeObjectFromPath(configPath) ??
                                new BarrieOpenModeConfig()
                                {
                                    BarrieIndex = settingController.Barriers[i],
                                    OpenMode = EmBarrieOpenMode.ALL,
                                };
                UcControllerBarrieConfig uc = new(oldConfig);
                panelBarrieConfigs.Controls.Add(uc);
                uc.Dock = DockStyle.Top;
                uc.SendToBack();
                this.controllerBarrieOpenConfigs.Add(uc);
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool isSaveSuccess = true;
            foreach (var item in this.controllerReaderCardFormats)
            {
                var newConfig = item.GetNewConfig();
                if (settingController != null)
                {
                    var path = IparkingingPathManagement.laneControllerReaderConfigPath(this.laneId, settingController.Id, newConfig.ReaderIndex);
                    bool result = NewtonSoftHelper<CardFormatConfig>.SaveConfig(newConfig, IparkingingPathManagement.laneControllerReaderConfigPath(this.laneId, settingController.Id, newConfig.ReaderIndex));
                    if (!result)
                    {
                        isSaveSuccess = false;
                    }
                }
            }

            foreach (var item in this.controllerBarrieOpenConfigs)
            {
                var newConfig = item.GetNewConfig();
                if (settingController != null)
                {
                    bool result = NewtonSoftHelper<BarrieOpenModeConfig>.SaveConfig(newConfig, IparkingingPathManagement.laneControllerBarrieConfigPath(this.laneId, settingController.Id, newConfig.BarrieIndex));
                    if (!result)
                    {
                        isSaveSuccess = false;
                    }
                }
            }

            if (!isSaveSuccess)
            {
                MessageBox.Show("Lưu thông tin cấu hình thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Lưu thông tin cấu hình thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            lblDevice.Text = KZUIStyles.CurrentResources.Device;
            btnSave.Text = KZUIStyles.CurrentResources.Save;
        }
    }
}
