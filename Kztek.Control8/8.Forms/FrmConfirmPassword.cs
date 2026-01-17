using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Object;

namespace Kztek.Control8.Forms
{
    public partial class FrmConfirmPassword : Form, KzITranslate
    {
        #region Forms
        public FrmConfirmPassword()
        {
            InitializeComponent();
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
        #endregion

        #region Controls In Form
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            if (KzDictionary.IsMasterPassword(txtPassword.Text))
            {
                this.DialogResult = DialogResult.OK;
                return true;
            }
            MessageBox.Show(KZUIStyles.CurrentResources.InvalidPassword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.DialogResult = DialogResult.Cancel;
            return true;
        }
        #endregion

        private void InitUI()
        {
            btnConfirm.OnClickAsync += BtnConfirm_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }
        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            this.Text = KZUIStyles.CurrentResources.FrmVerifyPassword;
            lblPasswordRequire.Text = KZUIStyles.CurrentResources.InputPasswordRequired;
            lblShortcutGuide.Text = KZUIStyles.CurrentResources.ShortCutGuide2Line;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
        }
    }
}
