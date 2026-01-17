using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class ucServerConfig : UserControl
    {
        #region Properties
        public ServerConfig? serverConfig = new();
        #endregion

        #region Forms
        public ucServerConfig(ServerConfig? serverConfig, RabbitMQConfig? rabbitMQConfig, MQTTConfig? mqttConfig)
        {
            InitializeComponent();
            toolTip1.SetToolTip(iconServerHint, "Cấu hình thông tin máy chủ parking");
            toolTip1.SetToolTip(iconRabbitMQHint, "Cần thiết lập khi sử dụng một trong các tính năng\r\n- Thanh toán online\r\n- Giám sát tại trung tâm - CMS");
            toolTip1.SetToolTip(iconMqttHint, "Cấu hình MQTT");
            toolTip1.SetToolTip(btnOpenAdvanceMode, "Cấu hình nâng cao");

            this.serverConfig = serverConfig;
            if (serverConfig != null)
            {
                txtApiUrl.Text = serverConfig.ApiUrl;
                txtLoginUrl.Text = serverConfig.LoginUrl;
                txtScope.Text = serverConfig.Scope;
                txtUsername.Text = serverConfig.Username;
                txtPassword.Text = serverConfig.Password;
                txtClientId.Text = serverConfig.ClientId;
                txtServerName.Text = serverConfig.ServerName;
            }
            if (rabbitMQConfig != null)
            {
                txtRabbitMQUrl.Text = rabbitMQConfig.RabbitMqUrl;
                txtRabbitMQPort.Text = rabbitMQConfig.Port.ToString();
                txtRabbitMQUsername.Text = rabbitMQConfig.RabbitMqUsername;
                txtRabbitMQPassword.Text = rabbitMQConfig.RabbitMqPassword;
            }
            if (mqttConfig != null)
            {
                txtMQTTUrl.Text = mqttConfig.Url;
                txtMQTTUsername.Text = mqttConfig.Username;
                txtMQTTPassword.Text = mqttConfig.Password;
                txtMQTTTopic.Text = mqttConfig.Topic;
            }
            TxtRabbitMQUrl_TextChanged(null, EventArgs.Empty);
            TxtMQTTUrl_TextChanged(null, EventArgs.Empty);
        }
        #endregion

        #region Public Function
        public ServerConfig GetServerConfig()
        {
            return new ServerConfig()
            {
                ApiUrl = txtApiUrl.Text,
                LoginUrl = txtLoginUrl.Text,
                Scope = txtScope.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                ClientId = txtClientId.Text,
                ServerName = txtServerName.Text,
            };
        }
        public RabbitMQConfig GetRabbitMQConfig()
        {
            _ = int.TryParse(txtRabbitMQPort.Text, out int port);
            return new RabbitMQConfig()
            {
                RabbitMqUrl = txtRabbitMQUrl.Text,
                Port = port,
                RabbitMqUsername = txtRabbitMQUsername.Text,
                RabbitMqPassword = txtRabbitMQPassword.Text,
            };
        }
        public MQTTConfig GetMQTTConfig()
        {
            return new MQTTConfig()
            {
                Url = txtMQTTUrl.Text,
                Username = txtMQTTUsername.Text,
                Password = txtMQTTPassword.Text,
                Topic = txtMQTTTopic.Text,
            };
        }
        #endregion

        private void TxtRabbitMQUrl_TextChanged(object? sender, EventArgs e)
        {
            txtRabbitMQPort.Enabled = !string.IsNullOrEmpty(txtRabbitMQPort.Text.Trim());
            txtRabbitMQUsername.Enabled = !string.IsNullOrEmpty(txtRabbitMQPort.Text.Trim());
            txtRabbitMQPassword.Enabled = !string.IsNullOrEmpty(txtRabbitMQPort.Text.Trim());
        }
        private void TxtMQTTUrl_TextChanged(object? sender, EventArgs e)
        {
            txtMQTTTopic.Enabled = !string.IsNullOrEmpty(txtMQTTUrl.Text.Trim());
            txtMQTTUsername.Enabled = !string.IsNullOrEmpty(txtMQTTUrl.Text.Trim());
            txtMQTTPassword.Enabled = !string.IsNullOrEmpty(txtMQTTUrl.Text.Trim());
        }
        private void BtnOpenAdvanceMode_Click(object sender, EventArgs e)
        {
            if (btnOpenAdvanceMode.IconChar == FontAwesome.Sharp.IconChar.CaretDown)
            {
                btnOpenAdvanceMode.IconChar = FontAwesome.Sharp.IconChar.CaretUp;
                panelAdvanceConfig.Visible = true;
            }
            else
            {
                btnOpenAdvanceMode.IconChar = FontAwesome.Sharp.IconChar.CaretDown;
                panelAdvanceConfig.Visible = false;
            }
        }
    }
}
