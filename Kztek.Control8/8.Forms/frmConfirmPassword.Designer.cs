using Kztek.Control8.Controls;

namespace Kztek.Control8.Forms
{
    partial class FrmConfirmPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmPassword));
            lblPasswordRequire = new KzLabel();
            txtPassword = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2 = new Panel();
            lblShortcutGuide = new KzLabel();
            btnCancel = new KzButton();
            btnConfirm = new KzButton();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblPasswordRequire
            // 
            lblPasswordRequire.Dock = DockStyle.Top;
            lblPasswordRequire.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPasswordRequire.ForeColor = Color.DarkOrange;
            lblPasswordRequire.Location = new Point(0, 0);
            lblPasswordRequire.Name = "lblPasswordRequire";
            lblPasswordRequire.Size = new Size(624, 77);
            lblPasswordRequire.TabIndex = 0;
            lblPasswordRequire.Text = "Vui lòng nhập mật khẩu để thực hiện chức năng này";
            lblPasswordRequire.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            txtPassword.Dock = DockStyle.Fill;
            txtPassword.Font = new Font("Segoe UI", 32F);
            txtPassword.Location = new Point(85, 41);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(454, 64);
            txtPassword.TabIndex = 0;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 460F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(txtPassword, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 77);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(624, 147);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblShortcutGuide);
            panel2.Controls.Add(btnCancel);
            panel2.Controls.Add(btnConfirm);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 224);
            panel2.Name = "panel2";
            panel2.Size = new Size(624, 85);
            panel2.TabIndex = 3;
            // 
            // lblGuide
            // 
            lblShortcutGuide.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblShortcutGuide.AutoSize = true;
            lblShortcutGuide.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShortcutGuide.ForeColor = Color.FromArgb(255, 128, 0);
            lblShortcutGuide.Location = new Point(8, 30);
            lblShortcutGuide.Name = "lblGuide";
            lblShortcutGuide.Size = new Size(142, 42);
            lblShortcutGuide.TabIndex = 13;
            lblShortcutGuide.Text = "Enter để xác nhận.\r\nEsc để hủy.";
            lblShortcutGuide.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(368, 24);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(117, 48);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Đóng";
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges3;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(496, 24);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(117, 48);
            btnConfirm.TabIndex = 14;
            btnConfirm.Text = "Xác Nhận";
            // 
            // FrmConfirmPassword
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(624, 309);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel2);
            Controls.Add(lblPasswordRequire);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfirmPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Xác thực thông tin";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private KzLabel lblPasswordRequire;
        private TextBox txtPassword;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private KzLabel lblShortcutGuide;
        private KzButton btnCancel;
        private KzButton btnConfirm;
    }

}