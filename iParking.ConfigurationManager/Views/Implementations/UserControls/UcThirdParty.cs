using iParking.ConfigurationManager.Views.Interfaces;
using iParkingv8.Object.ConfigObjects.OEMConfigs;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcThirdParty : UserControl, IThirdPartyView
    {
        public UcThirdParty()
        {
            InitializeComponent();
        }

        public ThirdPartyConfig GetConfig()
        {
            return new ThirdPartyConfig()
            {
                ServerUrl = txtServerUrl.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
            };
        }
        public void SetConfig(ThirdPartyConfig? config)
        {
            if (config != null)
            {
                txtServerUrl.Text = config!.ServerUrl;
                txtUsername.Text = config.Username;
                txtPassword.Text = config.Password;
            }
        }
    }
}
