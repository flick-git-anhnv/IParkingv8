using iParkingv8.Object.ConfigObjects.OEMConfigs;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcThirdParty : UserControl
    {
        #region Properties
        public ThirdPartyConfig? serverConfig = new();
        #endregion End Properties

        #region Forms
        public UcThirdParty(ThirdPartyConfig? serverConfig)
        {
            InitializeComponent();
            this.serverConfig = serverConfig;
            if (serverConfig != null)
            {
                txtServerUrl.Text = this.serverConfig!.ServerUrl;
                txtUsername.Text = this.serverConfig.Username;
                txtPassword.Text = this.serverConfig.Password;
            }
        }
        #endregion End Forms

        #region Public Function
        public ThirdPartyConfig GetConfig()
        {
            return new ThirdPartyConfig()
            {
                ServerUrl = txtServerUrl.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
            };
        }
        #endregion End Public Function
    }
}
