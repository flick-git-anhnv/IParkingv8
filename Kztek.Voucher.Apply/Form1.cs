using System.IO;
using System.Windows.Forms;

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
        }
        #endregion

        #region Controls In Form
        private void TxtAccessKeyCode_TextChanged(object sender, System.EventArgs e)
        {
            btnApplyVoucher.Enabled = AppData.AppConfig != null && !string.IsNullOrEmpty(txtAccessKeyCode.Text);
        }

        private void BtnApplyVoucher_Click(object sender, System.EventArgs e)
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
            new frmSetting().ShowDialog();
            btnApplyVoucher.Enabled = AppData.AppConfig != null && !string.IsNullOrEmpty(txtAccessKeyCode.Text);
        }
        #endregion
    }
}
