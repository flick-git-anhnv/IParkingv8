using iParkingv8.Object;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;

namespace iParkingv8.Auth
{
    public partial class FrmLogin : Form, KzITranslate
    {
        #region Properties
        private IAPIServer parkingApi;
        private Action completeAuthAction;
        private bool IsKioskDevice;
        private string kioskUsername;
        private string kioskPassword;
        private int loginCountDown = 30;
        #endregion

        public static void updateLoginTime()
        {
            Properties.Settings.Default.LastLoginTime = StaticPool.LoginTime;
            Properties.Settings.Default.Save();
        }
        #region Forms
        public FrmLogin(IAPIServer iParkingApi, Action completeAuthAction, bool isKioskDevice, string username, string password)
        {
            InitializeComponent();
            InitProperties(iParkingApi, completeAuthAction, isKioskDevice, username, password);
            Translate();

            this.Load += FrmLogin_Load;
            this.FormClosed += FrmLogin_FormClosed;
        }
        private async void FrmLogin_Load(object? sender, EventArgs e)
        {
            InitUI();
        }
        private void FrmLogin_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btnConfirm.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Controls In Form
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            timerAutoLogin.Enabled = false;
            lblCountDown.Visible = false;
            if (chbIsRemember.Checked)
            {
                Properties.Settings.Default.username = txtUsername.Text;
                Properties.Settings.Default.password = txtPassword.Text;
                Properties.Settings.Default.is_remember = chbIsRemember.Checked;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.is_remember = false;
                Properties.Settings.Default.Save();
            }

            this.parkingApi.Auth.Username = txtUsername.Text;
            this.parkingApi.Auth.Password = txtPassword.Text;
            bool isSuccess = await this.parkingApi.Auth.LoginAsync();
            if (!isSuccess)
            {
                lblCountDown.Visible = true;
                lblCountDown.Text = KZUIStyles.CurrentResources.InvalidLogin;
                lblCountDown.ForeColor = Color.Red;
                return false;
            }

            await this.parkingApi.UserService.GetUserDetailAsync();
            //if (StaticPool.SelectedUser?.IsValidWindowsAppPermission() != true)
            //{
            //    MessageBox.Show(KZUIStyles.CurrentResources.AccountInvalidPermission, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}

            this.FormClosed -= FrmLogin_FormClosed;
            this.Hide();
            this.parkingApi.Auth.PollingAuthorizeAsync();

            var data = await this.parkingApi.DataService.SystemConfig.GetSystemConfigAsync();
            StaticPool.configs = data?.Data ?? [];

            if (StaticPool.SelectedUser.Id != Properties.Settings.Default.LastUserId ||
                Properties.Settings.Default.LastLoginTime.Date != DateTime.Now.Date)
            {
                StaticPool.LoginTime = DateTime.Now;
                Properties.Settings.Default.LastUserId = StaticPool.SelectedUser.Id;
                Properties.Settings.Default.LastLoginTime = StaticPool.LoginTime;
                Properties.Settings.Default.Save();
            }

            completeAuthAction();
            return true;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            Application.Exit();
            Environment.Exit(0);
            return true;
        }
        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            lblCountDown.Visible = timerAutoLogin.Enabled = false;
        }
        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            lblCountDown.Visible = timerAutoLogin.Enabled = false;
        }
        private void ChbIsRemember_CheckedChanged(object sender, EventArgs e)
        {
            lblCountDown.Visible = timerAutoLogin.Enabled = false;
        }
        #endregion

        #region Timer
        private async void TimerAutoLogin_Tick(object sender, EventArgs e)
        {
            timerAutoLogin.Enabled = false;
            loginCountDown--;
            if (loginCountDown <= 0)
            {
                this.parkingApi.Auth.Username = txtUsername.Text;
                this.parkingApi.Auth.Password = txtPassword.Text;
                bool isSuccess = await this.parkingApi.Auth.LoginAsync();
                if (!isSuccess)
                {
                    loginCountDown = 30;
                    lblCountDown.Visible = true;
                    lblCountDown.Text = KZUIStyles.CurrentResources.InvalidLogin
                                    + " " +
                                     KZUIStyles.CurrentResources.AutoLoginAfter + $" {loginCountDown} s";
                    lblCountDown.ForeColor = Color.Red;
                    timerAutoLogin.Enabled = true;
                }
                else
                {
                    this.FormClosed -= FrmLogin_FormClosed;
                    this.Hide();
                    this.parkingApi.Auth.PollingAuthorizeAsync();
                    await this.parkingApi.UserService.GetUserDetailAsync();

                    if (StaticPool.SelectedUser.Id != Properties.Settings.Default.LastUserId ||
                        Properties.Settings.Default.LastLoginTime.Date != DateTime.Now.Date)
                    {
                        StaticPool.LoginTime = DateTime.Now;
                        Properties.Settings.Default.LastUserId = StaticPool.SelectedUser.Id;
                        Properties.Settings.Default.LastLoginTime = StaticPool.LoginTime;
                        Properties.Settings.Default.Save();
                    }

                    completeAuthAction();
                }
            }
            else
            {
                lblCountDown.Text = KZUIStyles.CurrentResources.AutoLoginAfter + $" {loginCountDown} s";
                timerAutoLogin.Enabled = true;
            }
        }
        #endregion

        private void InitProperties(IAPIServer iParkingApi, Action completeAuthAction, bool isKioskDevice, string username, string password)
        {
            this.IsKioskDevice = isKioskDevice;
            this.kioskUsername = username;
            this.kioskPassword = password;

            if (Properties.Settings.Default.LastLoginTime == null)
            {
                Properties.Settings.Default.LastLoginTime = DateTime.Now;
            }
            StaticPool.LoginTime = Properties.Settings.Default.LastLoginTime;

            this.parkingApi = iParkingApi;
            this.completeAuthAction = completeAuthAction;
        }
        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            this.Text = KZUIStyles.CurrentResources.FrmLogin;
            lblUsername.Text = KZUIStyles.CurrentResources.Username;
            lblPassword.Text = KZUIStyles.CurrentResources.Password;

            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;

            chbIsRemember.Text = KZUIStyles.CurrentResources.RememberPassword;
            lblCountDown.Text = KZUIStyles.CurrentResources.AutoLoginAfter;
        }
        private void InitUI()
        {
            txtUsername.Text = Properties.Settings.Default.username;
            txtPassword.Text = Properties.Settings.Default.password;
            chbIsRemember.Checked = Properties.Settings.Default.is_remember;
            if (Properties.Settings.Default.is_remember)
            {
                timerAutoLogin.Enabled = true;
                lblCountDown.Visible = true;
            }

            btnConfirm.OnClickAsync += BtnConfirm_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }
    }
}