using iParking.ConfigurationManager.UserControls;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using Kztek.Tool;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Forms
{
    public partial class FrmConnectionConfig : Form, KzITranslate
    {
        #region Properties
        private ucServerConfig? ucServer;
        private UcLprOption? ucLprOption;
        private UcAppOption? ucAppOption;
        private UcOEM? ucOEM;
        private UcThirdParty? ucThirdParty;
        private UcPaymentConfig? ucPaymentConfig;
        #endregion

        #region Forms
        public FrmConnectionConfig()
        {
            InitializeComponent();
            this.Load += FrmConnectionConfig_Load;
        }
        private void FrmConnectionConfig_Load(object? sender, EventArgs e)
        {
            AddTabServerConfig();
            AddTabLpr();
            AddTabOption();
            AddTabOEM();
            AddTabThirdPartyConfig();
            AddTabPaymentConfig();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool isEnableDevelopeMode = (keyData & Keys.Control) == Keys.Control &&
                                 (keyData & Keys.Shift) == Keys.Shift &&
                                 (keyData & Keys.KeyCode) == Keys.E;
            bool isDisEnableDevelopeMode = (keyData & Keys.Control) == Keys.Control &&
                                (keyData & Keys.Shift) == Keys.Shift &&
                                (keyData & Keys.KeyCode) == Keys.D;
            if (isEnableDevelopeMode)
            {
                this.ucAppOption?.DisplayDevelopMode(true);
                return true;
            }
            else if (isDisEnableDevelopeMode)
            {
                this.ucAppOption?.DisplayDevelopMode(false);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Controls In Form
        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            SaveServerConfig();
            SaveLprConfig();
            SaveAppOptionConfig();
            SaveOEMConfig();
            SaveThirdPartyConfig();
            SaveKioskPaymentConfig();
            MessageBox.Show("Cấu hình đã được lưu thành công!", KZUIStyles.CurrentResources.InfoTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion

        #region Private Function
        private void AddTabServerConfig()
        {
            TabPage tabServerConfig = new()
            {
                Text = "Server"
            };
            tabOption.TabPages.Add(tabServerConfig);
            tabServerConfig.BackColor = SystemColors.ButtonHighlight;

            ServerConfig? serverConfig = NewtonSoftHelper<ServerConfig>.DeserializeObjectFromPath(IparkingingPathManagement.serverConfigPath);
            RabbitMQConfig? rabbitMQConfig = NewtonSoftHelper<RabbitMQConfig>.DeserializeObjectFromPath(IparkingingPathManagement.rabbitmqConfigPath);
            MQTTConfig? mqttConfig = NewtonSoftHelper<MQTTConfig>.DeserializeObjectFromPath(IparkingingPathManagement.mqttConfigPath);
            ucServer = new ucServerConfig(serverConfig, rabbitMQConfig, mqttConfig);
            tabServerConfig.Controls.Add(ucServer);
            ucServer.Dock = DockStyle.Fill;
            tabServerConfig.AutoScroll = true;
        }
        private void AddTabLpr()
        {
            TabPage tabLpr = new()
            {
                Text = "Nhận dạng biển số"
            };
            tabOption.TabPages.Add(tabLpr);
            tabLpr.BackColor = SystemColors.ButtonHighlight;

            LprConfig? lprConfig = NewtonSoftHelper<LprConfig>.DeserializeObjectFromPath(IparkingingPathManagement.lprConfigPath);
            ucLprOption = new UcLprOption(lprConfig);
            tabLpr.Controls.Add(ucLprOption);
            ucLprOption.Dock = DockStyle.Fill;
            tabLpr.AutoScroll = true;
        }
        private void AddTabOption()
        {
            TabPage tabOption = new()
            {
                Text = "Tùy chọn"
            };
            this.tabOption.TabPages.Add(tabOption);
            tabOption.BackColor = SystemColors.ButtonHighlight;

            AppOption? appOption = NewtonSoftHelper<AppOption>.DeserializeObjectFromPath(IparkingingPathManagement.appOptionConfigPath);
            ucAppOption = new UcAppOption(appOption);
            tabOption.Controls.Add(ucAppOption);
            ucAppOption.Dock = DockStyle.Fill;
            tabOption.AutoScroll = true;
        }
        private void AddTabOEM()
        {
            TabPage tabOEM = new()
            {
                Text = "OEM"
            };
            this.tabOption.TabPages.Add(tabOEM);
            tabOEM.BackColor = SystemColors.ButtonHighlight;

            OEMConfig? oemConfig = NewtonSoftHelper<OEMConfig>.DeserializeObjectFromPath(IparkingingPathManagement.oemConfigPath);
            ucOEM = new UcOEM(oemConfig);
            tabOEM.Controls.Add(ucOEM);
            ucOEM.Dock = DockStyle.Fill;
            tabOEM.AutoScroll = true;
        }
        private void AddTabThirdPartyConfig()
        {
            TabPage tabThirdPartyConfig = new()
            {
                Text = "Tích hợp bên thứ 3"
            };
            this.tabOption.TabPages.Add(tabThirdPartyConfig);
            tabThirdPartyConfig.BackColor = SystemColors.ButtonHighlight;

            ThirdPartyConfig? thirdPartyConfig = NewtonSoftHelper<ThirdPartyConfig>.DeserializeObjectFromPath(IparkingingPathManagement.thirtPartyConfigPath);
            ucThirdParty = new UcThirdParty(thirdPartyConfig);
            tabThirdPartyConfig.Controls.Add(ucThirdParty);
            ucThirdParty.Dock = DockStyle.Fill;
            tabThirdPartyConfig.AutoScroll = true;
        }
        private void AddTabPaymentConfig()
        {
            TabPage tabPaymentKioskConfig = new()
            {
                Text = "Cấu hình thanh toán"
            };
            this.tabOption.TabPages.Add(tabPaymentKioskConfig);
            tabPaymentKioskConfig.BackColor = SystemColors.ButtonHighlight;

            PaymentKioskConfig? paymentKioskConfig = NewtonSoftHelper<PaymentKioskConfig>.DeserializeObjectFromPath(IparkingingPathManagement.paymentKioskConfigPath);
            ucPaymentConfig = new UcPaymentConfig(paymentKioskConfig);
            tabPaymentKioskConfig.Controls.Add(ucPaymentConfig);
            ucPaymentConfig.Dock = DockStyle.Fill;
            tabPaymentKioskConfig.AutoScroll = true;
        }

        private void SaveServerConfig()
        {
            NewtonSoftHelper<ServerConfig>.SaveConfig(ucServer!.GetServerConfig(), IparkingingPathManagement.serverConfigPath);
            NewtonSoftHelper<RabbitMQConfig>.SaveConfig(ucServer.GetRabbitMQConfig(), IparkingingPathManagement.rabbitmqConfigPath);
            NewtonSoftHelper<MQTTConfig>.SaveConfig(ucServer.GetMQTTConfig(), IparkingingPathManagement.mqttConfigPath);
        }
        private void SaveLprConfig()
        {
            NewtonSoftHelper<LprConfig>.SaveConfig(ucLprOption!.GetConfig(), IparkingingPathManagement.lprConfigPath);
        }
        private void SaveAppOptionConfig()
        {
            NewtonSoftHelper<AppOption>.SaveConfig(ucAppOption!.GetAppOption(), IparkingingPathManagement.appOptionConfigPath);
        }
        private void SaveOEMConfig()
        {
            NewtonSoftHelper<OEMConfig>.SaveConfig(ucOEM!.GetConfig(), IparkingingPathManagement.oemConfigPath);
        }
        private void SaveThirdPartyConfig()
        {
            NewtonSoftHelper<ThirdPartyConfig>.SaveConfig(ucThirdParty!.GetConfig(), IparkingingPathManagement.thirtPartyConfigPath);
        }
        private void SaveKioskPaymentConfig()
        {
            NewtonSoftHelper<PaymentKioskConfig>.SaveConfig(ucPaymentConfig!.GetConfig(), IparkingingPathManagement.paymentKioskConfigPath);
        }
        #endregion End Private Function

        #region Public Function
        public void Translate()
        {
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
        }
        #endregion
    }
}
