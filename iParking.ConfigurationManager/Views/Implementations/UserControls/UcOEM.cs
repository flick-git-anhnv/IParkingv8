using iParking.ConfigurationManager.Views.Interfaces;
using iParkingv8.Object.ConfigObjects.OEMConfigs;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcOEM : UserControl, IOEMView
    {
        private string selectedLogoPath = string.Empty;

        public UcOEM()
        {
            InitializeComponent();
            btnChooseLogoPath.Click += BtnChooseLogoPath_Click;
        }

        private void BtnChooseLogoPath_Click(object? sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff|All Files|*.*",
                Multiselect = false,
                Title = "Chọn logo hiển thị phần mềm"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedLogoPath = ofd.FileName;
                picLogo.BackgroundImage = Image.FromFile(selectedLogoPath);
            }
        }

        public OEMConfig GetConfig()
        {
            return new OEMConfig()
            {
                AppName = txtAppName.Text,
                CompanyName = txtCompanyName.Text,
                Language = cbLanguage.SelectedIndex,
                LogoPath = selectedLogoPath,
            };
        }
        public void SetConfig(OEMConfig? config)
        {
            if (config == null)
            {
                return;
            }
            txtAppName.Text = config.AppName;
            txtCompanyName.Text = config.CompanyName;
            int selectedLanguage = config.Language >= cbLanguage.Items.Count ? -1 : config.Language;
            cbLanguage.SelectedIndex = selectedLanguage;

            picLogo.BackgroundImage = null;
            if (!string.IsNullOrEmpty(config.LogoPath))
            {
                selectedLogoPath = config.LogoPath;
                if (File.Exists(selectedLogoPath))
                {
                    picLogo.BackgroundImage = Image.FromFile(selectedLogoPath);
                }
            }
        }
    }
}
