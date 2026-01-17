using Kztek.Control8._1.GeneralControls._4.CheckBox;
using Kztek.Control8.Controls;

namespace iParkingv8.Auth
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            lblUsername = new KzLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            lblPassword = new KzLabel();
            chbIsRemember = new KzCheckBox();
            timerAutoLogin = new System.Windows.Forms.Timer(components);
            lblCountDown = new KzLabel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 12F);
            lblUsername.Location = new Point(8, 24);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(124, 21);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "LBL_USERNAME";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnConfirm, 3, 0);
            tableLayoutPanel1.Controls.Add(btnCancel, 1, 0);
            tableLayoutPanel1.Location = new Point(8, 190);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(551, 48);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges1;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.Dock = DockStyle.Fill;
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(285, 0);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "BTN_CONFIRM";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges3;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(115, 0);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(150, 48);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "BTN_CANCEL";
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 8;
            txtUsername.CustomizableEdges = customizableEdges5;
            txtUsername.DefaultText = "";
            txtUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtUsername.Location = new Point(136, 16);
            txtUsername.Margin = new Padding(4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtUsername.Size = new Size(424, 36);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += TxtUsername_TextChanged;
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 8;
            txtPassword.CustomizableEdges = customizableEdges7;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtPassword.Location = new Point(136, 64);
            txtPassword.Margin = new Padding(4);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPassword.Size = new Size(424, 36);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += TxtPassword_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 12F);
            lblPassword.Location = new Point(8, 72);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(126, 21);
            lblPassword.TabIndex = 0;
            lblPassword.Text = "LBL_PASSWORD";
            // 
            // chbIsRemember
            // 
            chbIsRemember.AutoSize = true;
            chbIsRemember.Checked = true;
            chbIsRemember.CheckedState.BorderColor = Color.FromArgb(41, 97, 27);
            chbIsRemember.CheckedState.BorderRadius = 2;
            chbIsRemember.CheckedState.BorderThickness = 1;
            chbIsRemember.CheckedState.FillColor = Color.FromArgb(41, 97, 27);
            chbIsRemember.CheckState = CheckState.Checked;
            chbIsRemember.Font = new Font("Segoe UI", 12F);
            chbIsRemember.ForeColor = Color.Black;
            chbIsRemember.Location = new Point(136, 112);
            chbIsRemember.Margin = new Padding(0);
            chbIsRemember.Name = "chbIsRemember";
            chbIsRemember.Size = new Size(238, 25);
            chbIsRemember.TabIndex = 2;
            chbIsRemember.Text = "CHB_REMEMBER_PASSWORD";
            chbIsRemember.UncheckedState.BorderColor = Color.FromArgb(41, 97, 27);
            chbIsRemember.UncheckedState.BorderRadius = 2;
            chbIsRemember.UncheckedState.BorderThickness = 1;
            chbIsRemember.UncheckedState.FillColor = Color.White;
            chbIsRemember.CheckedChanged += ChbIsRemember_CheckedChanged;
            // 
            // timerAutoLogin
            // 
            timerAutoLogin.Interval = 1000;
            timerAutoLogin.Tick += TimerAutoLogin_Tick;
            // 
            // lblTimer
            // 
            lblCountDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCountDown.AutoSize = true;
            lblCountDown.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblCountDown.ForeColor = Color.Black;
            lblCountDown.Location = new Point(8, 161);
            lblCountDown.Name = "lblTimer";
            lblCountDown.Size = new Size(184, 21);
            lblCountDown.TabIndex = 9;
            lblCountDown.Text = "LBL_AUTO_LOGIN_AFTER";
            lblCountDown.Visible = false;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(567, 244);
            Controls.Add(lblCountDown);
            Controls.Add(chbIsRemember);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private KzLabel lblUsername;
        private TableLayoutPanel tableLayoutPanel1;
        private KzButton btnConfirm;
        private KzButton btnCancel;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private KzLabel lblPassword;
        private KzCheckBox chbIsRemember;
        private System.Windows.Forms.Timer timerAutoLogin;
        private KzLabel lblCountDown;
    }
}