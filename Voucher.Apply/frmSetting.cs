using System;
using System.IO;
using System.Windows.Forms;
using Voucher.Apply;

namespace Kztek.Voucher.Apply
{
    public partial class frmSetting : Form
    {
        #region Forms

        #endregion
        public frmSetting()
        {
            InitializeComponent();
            this.Load += FrmSetting_Load;
        }

        private void FrmSetting_Load(object? sender, EventArgs e)
        {
            txtAPILink.Text = AppData.AppConfig?.ApiLink ?? string.Empty;
            txtLoginLink.Text = AppData.AppConfig?.LoginLink ?? string.Empty;
            txtUsername.Text = AppData.AppConfig?.Username ?? string.Empty;
            txtPassword.Text = AppData.AppConfig?.Password ?? string.Empty;
            txtClientID.Text = AppData.AppConfig?.ClientID ?? string.Empty;
            txtVoucherCar.Text = AppData.AppConfig?.VoucherCar ?? string.Empty;
            txtVoucherMotor.Text = AppData.AppConfig?.VoucherMotor ?? string.Empty;
            txtVoucherOther.Text = AppData.AppConfig?.VoucherOther ?? string.Empty;
        }

        #region Controls In Form
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string loginLink = txtLoginLink.Text.Trim();
            string apiLink = txtAPILink.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string voucherCar = txtVoucherCar.Text.Trim();
            string voucherMotor = txtVoucherMotor.Text.Trim();
            string voucherOther = txtVoucherOther.Text.Trim();
            string clientId = txtClientID.Text.Trim();

            if (string.IsNullOrEmpty(loginLink) ||
                string.IsNullOrEmpty(apiLink) ||
                string.IsNullOrEmpty(username) ||
              (string.IsNullOrEmpty(voucherCar) && string.IsNullOrEmpty(voucherMotor) && string.IsNullOrEmpty(voucherOther)) ||
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
            AppData.AppConfig.VoucherCar = voucherCar;
            AppData.AppConfig.VoucherMotor = voucherMotor;
            AppData.AppConfig.VoucherOther = voucherOther;
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
