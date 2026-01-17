using iParkingv8.Object.ConfigObjects.OEMConfigs;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcOEM : UserControl
    {
        #region Properties
        private string selectedLogoPath = string.Empty;
        #endregion End Properties

        #region Forms
        public UcOEM(OEMConfig? oem)
        {
            InitializeComponent();
            if (oem != null)
            {
                txtAppName.Text = oem.AppName;
                txtCompanyName.Text = oem.CompanyName;
                int selectedLanguage = oem.Language >= cbLanguage.Items.Count ? -1 : oem.Language;
                cbLanguage.SelectedIndex = selectedLanguage;
              
                picLogo.BackgroundImage = null;
                if (!string.IsNullOrEmpty(oem.LogoPath))
                {
                    selectedLogoPath = oem.LogoPath;
                    if (File.Exists(selectedLogoPath))
                    {
                        picLogo.BackgroundImage = Image.FromFile(selectedLogoPath);
                    }
                }
            }

            btnChooseLogoPath.Click += BtnChooseLogoPath_Click;
        }
        #endregion End Forms

        #region Controls In Form
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
        #endregion

        #region Public Function
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
        #endregion End Public Function

        private void btnChooseLogoPath_Click_1(object sender, EventArgs e)
        {

        }
    }
}
