using iParkingv8.Object;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using Kztek.Object;
using static iParkingv8.Ultility.dictionary.KzDictionary;

namespace Kztek.Control8.Forms
{
    public partial class FrmChangePassword : Form, KzITranslate
    {
        private readonly IAPIServer apiServer;

        public FrmChangePassword(IAPIServer apiServer)
        {
            InitializeComponent();
            this.apiServer = apiServer;

            InitUI();
            Translate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btnConfirm.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            if (txtPassword.Text != this.apiServer.Auth.Password && !KzDictionary.IsMasterPassword(txtPassword.Text))
            {
                MessageBox.Show("Mật khẩu hiện tại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu lặp lại không chính xác, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool result = await this.apiServer.UserService.ChangePassword(StaticPool.SelectedUser.Id, txtUsername.Text, txtNewPassword.Text);
            if (!result)
            {
                MessageBox.Show("Đổi mật khẩu không thành công, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            this.apiServer.Auth.Password = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            return true;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.DialogResult = DialogResult.Cancel;
            return true;
        }

        private void InitUI()
        {
            txtUsername.Text = StaticPool.SelectedUser?.Upn ?? "";
            btnConfirm.OnClickAsync += BtnConfirm_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }

        public void Translate()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(Translate);
                return;
            }

            lblUsername.Text = KZUIStyles.CurrentResources.Username;
            lblCurrentPassword.Text = KZUIStyles.CurrentResources.CurrentPassword;
            lblNewPassword.Text = KZUIStyles.CurrentResources.NewPassword;
            lblConfirmPassword.Text = KZUIStyles.CurrentResources.ConfirmPassword;

            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
        }
    }
}
