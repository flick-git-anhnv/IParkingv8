using iParking.ConfigurationManager.Presenters;
using iParking.ConfigurationManager.Services;
using iParking.ConfigurationManager.UserControls;
using iParking.ConfigurationManager.Views.Interfaces;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Forms
{
    public partial class FrmConnectionConfig : Form, KzITranslate, IConnectionConfigView
    {
        ConnectionConfigPresenter presenter;

        public event EventHandler? ConfirmClicked;
        public event EventHandler? CancelClicked;

        #region Properties
        private IServerConfigView serverConfigView;
        private ILprOptionView lprConfigView;
        private IAppOptionView appOptionView;
        private IOEMView oemView;
        private IThirdPartyView thirdPartyView;
        private IPaymentConfigView paymentConfigView;

        public ServerConfig? ServerConfig { get => serverConfigView?.GetServerConfig(); set => serverConfigView?.SetServerConfig(value); }
        public RabbitMQConfig? RabbitMQConfig { get => serverConfigView?.GetRabbitMQConfig(); set => serverConfigView?.SetRabbitMQConfig(value); }
        public MQTTConfig? MqttConfig { get => serverConfigView?.GetMQTTConfig(); set => serverConfigView?.SetMQTTConfig(value); }

        public LprConfig? LprConfig { get => lprConfigView.GetConfig(); set => lprConfigView.SetConfig(value); }
        public AppOption? AppOption { get => appOptionView.GetConfig(); set => appOptionView.SetConfig(value); }
        public OEMConfig? OemConfig { get => oemView.GetConfig(); set => oemView.SetConfig(value); }
        public ThirdPartyConfig? ThirdPartyConfig { get => thirdPartyView.GetConfig(); set => thirdPartyView.SetConfig(value); }
        public PaymentKioskConfig? PaymentConfig { get => paymentConfigView.GetConfig(); set => paymentConfigView.SetConfig(value); }
        #endregion

        #region Forms
        public FrmConnectionConfig()
        {
            InitializeComponent();
            this.Load += FrmConnectionConfig_Load;
        }
        private void FrmConnectionConfig_Load(object? sender, EventArgs e)
        {
            InitUI();

            this.presenter = new ConnectionConfigPresenter(this, new ConfigService());
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
                this.appOptionView?.DisplayDevelopMode(true);
                return true;
            }
            else if (isDisEnableDevelopeMode)
            {
                this.appOptionView?.DisplayDevelopMode(false);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Private Function
        private void InitUI()
        {
            Translate();

            AddTabServerConfig();
            AddTabLpr();
            AddTabOption();
            AddTabOEM();
            AddTabThirdPartyConfig();
            AddTabPaymentConfig();

            btnConfirm.Click += (s, e) => ConfirmClicked?.Invoke(s, e);
            btnCancel.Click += (s, e) => CancelClicked?.Invoke(s, e);
        }

        private void AddTabServerConfig()
        {
            TabPage tab = new()
            {
                Text = "Server"
            };
            tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            serverConfigView = new ucServerConfig();
            if (serverConfigView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        private void AddTabLpr()
        {
            TabPage tab = new()
            {
                Text = "Nhận dạng biển số"
            };
            tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            lprConfigView = new UcLprOption();
            if (lprConfigView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        private void AddTabOption()
        {
            TabPage tab = new()
            {
                Text = "Tùy chọn"
            };
            this.tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            appOptionView = new UcAppOption();
            if (appOptionView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        private void AddTabOEM()
        {
            TabPage tab = new()
            {
                Text = "OEM"
            };
            this.tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            oemView = new UcOEM();
            if (oemView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        private void AddTabThirdPartyConfig()
        {
            TabPage tab = new()
            {
                Text = "Tích hợp bên thứ 3"
            };
            this.tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            thirdPartyView = new UcThirdParty();
            if (thirdPartyView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        private void AddTabPaymentConfig()
        {
            TabPage tab = new()
            {
                Text = "Cấu hình thanh toán"
            };
            this.tabOption.TabPages.Add(tab);
            tab.BackColor = SystemColors.ButtonHighlight;

            paymentConfigView = new UcPaymentConfig();
            if (paymentConfigView is Control view)
            {
                tab.Controls.Add(view);
                view.Dock = DockStyle.Fill;
            }
            tab.AutoScroll = true;
        }
        #endregion End Private Function

        #region Public Function
        public void Translate()
        {
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
        }
        public void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public new void Close() => base.Close();
        #endregion
    }
}
