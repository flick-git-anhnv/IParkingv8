using System;
using System.IO;
using System.Windows.Forms;

namespace Kztek.Voucher.Apply
{
    public partial class frmSetting : Form
    {
        #region Forms

        #endregion
        public frmSetting()
        {
            InitializeComponent();
        }

        #region Controls In Form
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string loginLink = txtLoginLink.Text.Trim();
            string apiLink = txtAPILink.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string voucherName = txtVoucherName.Text.Trim();
            string clientId = txtClientID.Text.Trim();

            if (string.IsNullOrEmpty(loginLink) ||
                string.IsNullOrEmpty(apiLink) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(voucherName) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(clientId)
                )
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AppData.AppConfig = new AppConfig();
            AppData.AppConfig.LoginLink = loginLink;
            AppData.AppConfig.ApiLink = apiLink;
            AppData.AppConfig.Username = username;
            AppData.AppConfig.Password = password;
            AppData.AppConfig.VoucherName = voucherName;
            AppData.AppConfig.ClientID = clientId;

            string config = Newtonsoft.Json.JsonConvert.SerializeObject(AppData.AppConfig);
            File.WriteAllText("config.json", config);
            this.DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
