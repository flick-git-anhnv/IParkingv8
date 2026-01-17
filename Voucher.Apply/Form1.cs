using IParkingv8.API.Implementation.v8;
using Kztek.Tool;
using Voucher.Apply;

namespace Kztek.Voucher.Apply
{
    public partial class Form1 : Form
    {
        string configFilePath = "config.json";
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btnApplyVoucher.Enabled = false;
            btnApplyVoucher.Click += BtnApplyVoucher_Click;
            picSetting.Click += PicSetting_Click;
            txtAccessKeyCode.TextChanged += TxtAccessKeyCode_TextChanged;
        }

        #region Forms
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnApplyVoucher.Enabled = false;
                txtAccessKeyCode.Text = string.Empty;
                return true;
            }

            if (keyData == Keys.Return)
            {
                btnApplyVoucher.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private async void Form1_Load(object sender, System.EventArgs e)
        {

            if (!File.Exists(configFilePath))
            {
                AppData.AppConfig = null;
                return;
            }
            string config = File.ReadAllText("config.json");
            try
            {
                AppData.AppConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfig>(config);
            }
            catch (System.Exception)
            {
                AppData.AppConfig = null;
            }

            if (AppData.AppConfig != null)
            {
                lblAppliedVoucherCar.Text = AppData.AppConfig.VoucherCar;
                lblAppliedVoucherMotor.Text = AppData.AppConfig.VoucherMotor;
                lblAppliedVoucherOther.Text = AppData.AppConfig.VoucherOther;
                AppData.ApiServer = new ApiServerv8(new iParkingv8.Object.Objects.Systems.ServerConfig()
                {
                    ApiUrl = AppData.AppConfig.ApiLink,
                    LoginUrl = AppData.AppConfig.LoginLink,
                    Username = "",
                    Password = "",
                    ClientId = AppData.AppConfig.ClientID,
                    Scope = "openid profile client-data user-data role-data resource-data config-data event-data offline_access"
                });

                AppData.ApiServer.Auth.Username = AppData.AppConfig.Username;
                AppData.ApiServer.Auth.Password = AppData.AppConfig.Password;

                bool isLoginSuccess = await AppData.ApiServer.Auth.LoginAsync();
                if (!isLoginSuccess)
                {
                    MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin cấu hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _ = AppData.ApiServer.Auth.PollingAuthorizeAsync();
            }
        }
        #endregion

        #region Controls In Form
        private void TxtAccessKeyCode_TextChanged(object sender, System.EventArgs e)
        {
            btnApplyVoucher.Enabled = AppData.AppConfig != null && !string.IsNullOrEmpty(txtAccessKeyCode.Text);
        }

        private async void BtnApplyVoucher_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!btnApplyVoucher.Enabled)
                {
                    return;
                }

                if (AppData.AppConfig == null)
                {
                    MessageBox.Show("Vui lòng cấu hình ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtAccessKeyCode.Text))
                {
                    MessageBox.Show("Vui lòng quẹt thẻ vào đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var accessKeyCode = txtAccessKeyCode.Text.Trim().ToUpper();
                var accessKey = await AppData.ApiServer.DataService.AccessKey.GetByCodeAsync(accessKeyCode);

                if (accessKey?.Item1 == null)
                {
                    MessageBox.Show("Không tìm thấy mã thẻ này trong hệ thống, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //var entry = await AppData.ApiServer.ReportingService.Entry.GetEntryByAccessKeyIdAsync(accessKey.Item1.Id);
                //if (entry == null)
                //{
                //    MessageBox.Show("Không tìm thông tin xe trong bãi, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                string voucherCode = string.Empty;
                switch (accessKey.Item1.Collection.GetVehicleType())
                {
                    case iParkingv8.Object.Enums.Parkings.EmVehicleType.CAR:
                        voucherCode = AppData.AppConfig.VoucherCar;
                        break;
                    case iParkingv8.Object.Enums.Parkings.EmVehicleType.MOTORBIKE:
                        voucherCode = AppData.AppConfig.VoucherMotor;
                        break;
                    case iParkingv8.Object.Enums.Parkings.EmVehicleType.BIKE:
                        voucherCode = AppData.AppConfig.VoucherOther;
                        break;
                    default:
                        break;
                }

                var applyVoucherResult = await AppData.ApiServer.PaymentService.ApplyVoucherEntryAsync(voucherCode, accessKeyCode);
                if (applyVoucherResult?.Item1 == null)
                {
                    MessageBox.Show($"Áp dụng voucher không thành công:\r\n{applyVoucherResult?.Item2?.ToString() ?? ""}\r\nVui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("Áp dụng voucher thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Áp dụng voucher: {voucherCode} - {accessKeyCode}"));
                txtAccessKeyCode.Text = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Gặp lỗi trong quá trình xử lý, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.ActiveControl = txtAccessKeyCode;
                txtAccessKeyCode.Focus();
            }
        }
        private void PicSetting_Click(object sender, System.EventArgs e)
        {
            if (new frmConfirmPassword().ShowDialog() != DialogResult.OK)
            {
                return;
            }
            bool isUpdate = new frmSetting().ShowDialog() == DialogResult.OK;
            if (isUpdate)
            {
                MessageBox.Show("Mở lại ứng dụng để cập nhật thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnApplyVoucher.Enabled = AppData.AppConfig != null && !string.IsNullOrEmpty(txtAccessKeyCode.Text);
        }
        #endregion
    }
}
