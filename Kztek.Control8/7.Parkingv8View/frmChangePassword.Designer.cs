using Kztek.Control8.Controls;

namespace Kztek.Control8.Forms
{
    partial class FrmChangePassword
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangePassword));
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            lblCurrentPassword = new Label();
            lblUsername = new Label();
            lblNewPassword = new Label();
            txtNewPassword = new Guna.UI2.WinForms.Guna2TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new Guna.UI2.WinForms.Guna2TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 8;
            txtPassword.CustomizableEdges = customizableEdges1;
            txtPassword.DefaultText = "";
            txtPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPassword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.ForeColor = Color.Black;
            txtPassword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtPassword.Location = new Point(328, 32);
            txtPassword.Margin = new Padding(0);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtPassword.Size = new Size(304, 36);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 8;
            txtUsername.CustomizableEdges = customizableEdges3;
            txtUsername.DefaultText = "";
            txtUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtUsername.Enabled = false;
            txtUsername.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.ForeColor = Color.Black;
            txtUsername.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtUsername.Location = new Point(8, 32);
            txtUsername.Margin = new Padding(0);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtUsername.Size = new Size(304, 36);
            txtUsername.TabIndex = 0;
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCurrentPassword.Location = new Point(328, 8);
            lblCurrentPassword.Margin = new Padding(2);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(134, 21);
            lblCurrentPassword.TabIndex = 4;
            lblCurrentPassword.Text = "Mật khẩu hiện tại";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUsername.Location = new Point(8, 8);
            lblUsername.Margin = new Padding(2);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(77, 21);
            lblUsername.TabIndex = 5;
            lblUsername.Text = "Tài khoản";
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblNewPassword.Location = new Point(8, 80);
            lblNewPassword.Margin = new Padding(2);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(109, 21);
            lblNewPassword.TabIndex = 4;
            lblNewPassword.Text = "Mật khẩu mới";
            // 
            // txtNewPassword
            // 
            txtNewPassword.BorderRadius = 8;
            txtNewPassword.CustomizableEdges = customizableEdges5;
            txtNewPassword.DefaultText = "";
            txtNewPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNewPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNewPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNewPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNewPassword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtNewPassword.Font = new Font("Segoe UI", 12F);
            txtNewPassword.ForeColor = Color.Black;
            txtNewPassword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtNewPassword.Location = new Point(8, 104);
            txtNewPassword.Margin = new Padding(0);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.PlaceholderText = "";
            txtNewPassword.SelectedText = "";
            txtNewPassword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtNewPassword.Size = new Size(304, 36);
            txtNewPassword.TabIndex = 2;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblConfirmPassword.Location = new Point(328, 80);
            lblConfirmPassword.Margin = new Padding(2);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(177, 21);
            lblConfirmPassword.TabIndex = 4;
            lblConfirmPassword.Text = "Xác nhận mật khẩu mới";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.BorderRadius = 8;
            txtConfirmPassword.CustomizableEdges = customizableEdges7;
            txtConfirmPassword.DefaultText = "";
            txtConfirmPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtConfirmPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtConfirmPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtConfirmPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtConfirmPassword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtConfirmPassword.Font = new Font("Segoe UI", 12F);
            txtConfirmPassword.ForeColor = Color.Black;
            txtConfirmPassword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtConfirmPassword.Location = new Point(328, 104);
            txtConfirmPassword.Margin = new Padding(0);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.PlaceholderText = "";
            txtConfirmPassword.SelectedText = "";
            txtConfirmPassword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtConfirmPassword.Size = new Size(304, 36);
            txtConfirmPassword.TabIndex = 3;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnConfirm, 3, 0);
            tableLayoutPanel1.Controls.Add(btnCancel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(8, 195);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(627, 48);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges9;
            btnConfirm.Dock = DockStyle.Fill;
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(323, 0);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 5;
            btnConfirm.Text = "Xác Nhận";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges11;
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(153, 0);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnCancel.Size = new Size(150, 48);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Đóng";
            // 
            // FrmChangePassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(643, 251);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblConfirmPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(lblUsername);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmChangePassword";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đổi mật khẩu";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Label lblCurrentPassword;
        private Label lblUsername;
        private Label lblNewPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassword;
        private TableLayoutPanel tableLayoutPanel1;
        private KzButton btnConfirm;
        private KzButton btnCancel;
    }
}