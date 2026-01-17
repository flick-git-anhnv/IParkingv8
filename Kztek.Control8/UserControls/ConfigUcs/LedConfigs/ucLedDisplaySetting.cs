using iParkingv5.LedDisplay.Enums;
using iParkingv5.LedDisplay.LEDs;
using iParkingv8.Ultility;
using Kztek.Control8.UserControls.ConfigUcs.LedConfigs;
using Kztek.Object;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs
{
    public partial class ucLedDisplaySetting : UserControl
    {
        #region Properties
        private readonly IEnumerable<Led> leds = [];
        private Led? currentLed;
        int stepOrder = 0;
        int stepOrderDefault = 0;
        private readonly List<ucLedLineConfig> UCConfigs = [];
        private readonly List<ucLedLineConfig> UCConfigsDefault = [];
        IDisplayLED? displayLED = null;
        private readonly string laneId;
        #endregion End Properties

        #region Forms
        public ucLedDisplaySetting(string laneId, IEnumerable<Led> leds)
        {
            InitializeComponent();
            btnAddStep.Click += BtnAddStep_Click;
            btnAddDefault.Click += BtnAddDefault_Click;
            btnSave.Click += BtnSave_Click;
            btnTest.Click += BtnTest_Click;

            this.leds = leds;
            this.laneId = laneId;
            foreach (Led item in this.leds)
            {
                cbLeds.Items.Add(item.name ?? string.Empty);
            }
            foreach (EmLedDirectionDisplayMode item in Enum.GetValues(typeof(EmLedDirectionDisplayMode)))
            {
                cbDirectionLedMode.Items.Add(LedDirectionDisplayMode.GetDisplayStr(item));
            }
            cbLeds.SelectedIndexChanged += CbLeds_SelectedIndexChanged;
            if (cbLeds.Items.Count > 0)
            {
                cbLeds.SelectedIndex = 0;
            }
        }

        #endregion End Forms

        #region Controls In Form
        private void CbLeds_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnSave.PerformClick();
            stepOrder = 0;
            EmLedType ledType = EmLedType.P10FULL;
            foreach (var item in this.leds)
            {
                if (item.name == cbLeds.Text)
                {
                    currentLed = item;
                    displayLED = LedFactory.CreateLed(this.currentLed);
                    ledType = (EmLedType)item.type;
                    break;
                }
            }

            switch (ledType)
            {
                case EmLedType.DIRECTION_LED_16_32:
                case EmLedType.DIRECTION_LED_32_64:
                    {
                        panelDirectionLedConfig.BringToFront();
                        panelDisplayDataLedConfigs.Visible = false;
                        LedDisplayConfig? configData = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                      IparkingingPathManagement.laneLedConfigPath(laneId, this.currentLed?.id ?? ""));
                        if (configData != null)
                        {
                            cbDirectionLedMode.SelectedIndex = cbDirectionLedMode.Items.Count > (int)configData.DirectionDisplayMode ?
                                                                (int)configData.DirectionDisplayMode : 0;
                        }
                        else
                        {
                            cbDirectionLedMode.SelectedIndex = 0;
                        }
                    }
                    break;
                default:
                    {
                        panelDisplayDataLedConfigs.BringToFront();
                        panelDirectionLedConfig.Visible = false;

                        panelLedConfigs.Controls.Clear();
                        panelDefault.Controls.Clear();

                        LedDisplayConfig? configData = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                          IparkingingPathManagement.laneLedConfigPath(laneId, this.currentLed?.id ?? ""));
                        if (configData != null)
                        {
                            foreach (var item in configData.LedDisplaySteps)
                            {
                                stepOrder = item.Key;

                                panelLedConfigs.SuspendLayout();
                                ucLedLineConfig uc = new(currentLed?.row ?? 1, stepOrder);
                                uc.LoadOldConfig(item.Value);

                                panelLedConfigs.Controls.Add(uc);

                                uc.Dock = DockStyle.Top;
                                //uc.BorderStyle = BorderStyle.Fixed3D;
                                uc.OnDeleteItemEvent += Uc_OnDeleteItemEvent;
                                uc.BringToFront();
                                UCConfigs.Add(uc);
                                panelLedConfigs.ResumeLayout();
                            }
                        }

                        LedDisplayConfig? configDefaultData = NewtonSoftHelper<LedDisplayConfig>.DeserializeObjectFromPath(
                                                          IparkingingPathManagement.laneLedDefaultConfigPath(laneId, this.currentLed?.id ?? ""));
                        if (configDefaultData != null)
                        {
                            foreach (var item in configDefaultData.LedDisplaySteps)
                            {
                                stepOrderDefault = item.Key;

                                panelDefault.SuspendLayout();
                                ucLedLineConfig uc = new(currentLed?.row ?? 1, stepOrderDefault);
                                uc.LoadOldConfig(item.Value);

                                panelDefault.Controls.Add(uc);

                                uc.Dock = DockStyle.Top;
                                //uc.BorderStyle = BorderStyle.Fixed3D;
                                uc.OnDeleteItemEvent += Uc_OnDeleteItemEvent1;
                                uc.BringToFront();
                                UCConfigsDefault.Add(uc);
                                panelDefault.ResumeLayout();
                            }
                        }
                        break;
                    }
            }
        }

        private void BtnAddStep_Click(object? sender, EventArgs e)
        {
            stepOrder++;
            ucLedLineConfig uc = new(currentLed?.row ?? 1, stepOrder);
            panelLedConfigs.Controls.Add(uc);
            uc.Dock = DockStyle.Top;
            //uc.BorderStyle = BorderStyle.Fixed3D;
            uc.OnDeleteItemEvent += Uc_OnDeleteItemEvent;
            var a = uc.Location;
            uc.BringToFront();
            UCConfigs.Add(uc);
        }
        private void BtnAddDefault_Click(object? sender, EventArgs e)
        {
            panelDefault.SuspendLayout();
            stepOrderDefault++;
            ucLedLineConfig uc = new(currentLed?.row ?? 1, stepOrderDefault);
            panelDefault.Controls.Add(uc);
            uc.Dock = DockStyle.Top;
            //uc.BorderStyle = BorderStyle.Fixed3D;
            uc.OnDeleteItemEvent += Uc_OnDeleteItemEvent1;
            uc.BringToFront();
            UCConfigsDefault.Add(uc);
            panelDefault.ResumeLayout();
        }

        private void Uc_OnDeleteItemEvent(object sender)
        {
            this.stepOrder--;
            if (sender is not ucLedLineConfig deleteUC)
            {
                return;
            }
            UCConfigs.Remove(deleteUC);

            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < UCConfigs.Count; i++)
                {
                    if (UCConfigs[i].Order > deleteUC.Order)
                    {
                        UCConfigs[i].Order = UCConfigs[i].Order - 1;
                    }
                }
                panelLedConfigs.Controls.Remove(deleteUC);
            }));

            deleteUC.Dispose();
        }
        private void Uc_OnDeleteItemEvent1(object sender)
        {
            this.stepOrderDefault--;
            if (sender is not ucLedLineConfig deleteUC)
            {
                return;
            }
            UCConfigsDefault.Remove(deleteUC);

            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < UCConfigsDefault.Count; i++)
                {
                    if (UCConfigsDefault[i].Order > deleteUC.Order)
                    {
                        UCConfigsDefault[i].Order = UCConfigsDefault[i].Order - 1;
                    }
                }
                panelDefault.Controls.Remove(deleteUC);
            }));

            deleteUC.Dispose();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (this.currentLed == null)
            {
                return;
            }
            Dictionary<int, DisplayStepDetail> steps = [];
            for (int i = 0; i < UCConfigs.Count; i++)
            {
                var stepDetail = UCConfigs[i].GetConfig();
                steps.Add(UCConfigs[i].Order, stepDetail);
            }
            LedDisplayConfig ledDisplayConfig = new()
            {
                LedId = this.currentLed.id,
                DirectionDisplayMode = (EmLedDirectionDisplayMode)cbDirectionLedMode.SelectedIndex,
                LedDisplaySteps = steps,
            };
            SaveConfig(ledDisplayConfig);

            Dictionary<int, DisplayStepDetail> stepDefaults = [];
            for (int i = 0; i < UCConfigsDefault.Count; i++)
            {
                var stepDetail = UCConfigsDefault[i].GetConfig();
                stepDefaults.Add(UCConfigsDefault[i].Order, stepDetail);
            }
            LedDisplayConfig ledDisplayDefaultConfig = new()
            {
                LedId = this.currentLed.id,
                LedDisplaySteps = stepDefaults,
            };
            SaveDefaultConfig(ledDisplayDefaultConfig);
        }
        private void BtnTest_Click(object? sender, EventArgs e)
        {
            if (displayLED == null)
            {
                MessageBox.Show("Loại LED không được hỗ trợ chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmTestLedDisplay frm = new();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                Dictionary<int, DisplayStepDetail> steps = [];
                for (int i = 0; i < UCConfigs.Count; i++)
                {
                    var stepDetail = UCConfigs[i].GetConfig();
                    steps.Add(UCConfigs[i].Order, stepDetail);
                }
                LedDisplayConfig ledDisplayConfig = new()
                {
                    LedId = this.currentLed!.id,
                    LedDisplaySteps = steps,
                };
                displayLED.Connect(this.currentLed);
                displayLED.SendToLED(frm.TestData, ledDisplayConfig);
            }
        }
        #endregion End Controls In Form

        #region Private Function
        private void SaveConfig(LedDisplayConfig ledDisplayConfig)
        {
            bool isSaveSuccess = NewtonSoftHelper<LedDisplayConfig>.SaveConfig(ledDisplayConfig, IparkingingPathManagement.laneLedConfigPath(laneId, this.currentLed?.id ?? ""));
            if (isSaveSuccess)
            {
                MessageBox.Show("Lưu cấu hình hiển thị LED thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lưu cấu hình hiển thị LED thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SaveDefaultConfig(LedDisplayConfig ledDisplayConfig)
        {
            bool isSaveSuccess = NewtonSoftHelper<LedDisplayConfig>.SaveConfig(ledDisplayConfig, IparkingingPathManagement.laneLedDefaultConfigPath(laneId, this.currentLed?.id ?? ""));
            if (isSaveSuccess)
            {
                MessageBox.Show("Lưu cấu hình hiển thị LED mặc định thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lưu cấu hình hiển thị LED mặc định thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion End Private Function

        #region Public Function
        #endregion End Public Function


    }
}
