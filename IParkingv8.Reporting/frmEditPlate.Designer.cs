using Kztek.Control8.Controls;

namespace iParkingv8.Reporting
{
    partial class FrmEditPlate
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
            lblCurrentPlate = new KzLabel();
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            txtNewPlate = new Guna.UI2.WinForms.Guna2TextBox();
            lblNewPlateTitle = new KzLabel();
            lblCurrentPlateTitle = new KzLabel();
            SuspendLayout();
            // 
            // lblCurrentPlate
            // 
            lblCurrentPlate.AutoSize = true;
            lblCurrentPlate.Location = new Point(176, 8);
            lblCurrentPlate.Name = "lblCurrentPlate";
            lblCurrentPlate.Size = new Size(17, 21);
            lblCurrentPlate.TabIndex = 8;
            lblCurrentPlate.Text = "_";
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderRadius = 8;
            btnConfirm.CustomizableEdges = customizableEdges1;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(344, 112);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 11;
            btnConfirm.Text = "Xác Nhận";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
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
            btnCancel.Location = new Point(176, 112);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(150, 48);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Đóng";
            // 
            // txtNewPlate
            // 
            txtNewPlate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewPlate.BorderRadius = 8;
            txtNewPlate.CustomizableEdges = customizableEdges5;
            txtNewPlate.DefaultText = "";
            txtNewPlate.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNewPlate.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNewPlate.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNewPlate.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNewPlate.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtNewPlate.Font = new Font("Segoe UI", 12F);
            txtNewPlate.ForeColor = Color.Black;
            txtNewPlate.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtNewPlate.Location = new Point(176, 48);
            txtNewPlate.Margin = new Padding(4);
            txtNewPlate.Name = "txtNewPlate";
            txtNewPlate.PlaceholderText = "";
            txtNewPlate.SelectedText = "";
            txtNewPlate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtNewPlate.Size = new Size(320, 40);
            txtNewPlate.TabIndex = 13;
            // 
            // lblNewPlateTitle
            // 
            lblNewPlateTitle.AutoSize = true;
            lblNewPlateTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblNewPlateTitle.Location = new Point(8, 58);
            lblNewPlateTitle.Margin = new Padding(0);
            lblNewPlateTitle.Name = "lblNewPlateTitle";
            lblNewPlateTitle.Size = new Size(95, 21);
            lblNewPlateTitle.TabIndex = 14;
            lblNewPlateTitle.Text = "Biển số mới";
            // 
            // lblCurrentPlateTitle
            // 
            lblCurrentPlateTitle.AutoSize = true;
            lblCurrentPlateTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCurrentPlateTitle.Location = new Point(8, 8);
            lblCurrentPlateTitle.Margin = new Padding(0);
            lblCurrentPlateTitle.Name = "lblCurrentPlateTitle";
            lblCurrentPlateTitle.Size = new Size(120, 21);
            lblCurrentPlateTitle.TabIndex = 14;
            lblCurrentPlateTitle.Text = "Biển số hiện tại";
            // 
            // FrmEditPlate
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(513, 173);
            Controls.Add(lblCurrentPlateTitle);
            Controls.Add(lblNewPlateTitle);
            Controls.Add(txtNewPlate);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            Controls.Add(lblCurrentPlate);
            Font = new Font("Segoe UI", 12F);
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmEditPlate";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CẬP NHẬT THÔNG TIN";
            KeyDown += FrmEditPlate_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KzLabel lblCurrentPlate;
        private KzButton btnConfirm;
        private KzButton btnCancel;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPlate;
        private KzLabel lblNewPlateTitle;
        private KzLabel lblCurrentPlateTitle;
    }
}