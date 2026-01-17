namespace Kztek.Control8.UserControls
{
    partial class UcVehicle
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblStatus = new Label();
            lblPlateNumber = new Label();
            lblStatusTitle = new Label();
            lblPlateNumberTitle = new Label();
            radioStatus = new Guna.UI2.WinForms.Guna2CustomRadioButton();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.FromArgb(246, 237, 233);
            lblStatus.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Location = new Point(128, 46);
            lblStatus.Margin = new Padding(0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(181, 32);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Đang Sử Dụng";
            // 
            // lblPlateNumber
            // 
            lblPlateNumber.AutoSize = true;
            lblPlateNumber.BackColor = Color.FromArgb(246, 237, 233);
            lblPlateNumber.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblPlateNumber.Location = new Point(128, 14);
            lblPlateNumber.Margin = new Padding(0);
            lblPlateNumber.Name = "lblPlateNumber";
            lblPlateNumber.Size = new Size(129, 32);
            lblPlateNumber.TabIndex = 1;
            lblPlateNumber.Text = "30A12345";
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.BackColor = Color.FromArgb(246, 237, 233);
            lblStatusTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblStatusTitle.Location = new Point(8, 48);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(106, 28);
            lblStatusTitle.TabIndex = 1;
            lblStatusTitle.Text = "Trạng Thái";
            // 
            // lblPlateNumberTitle
            // 
            lblPlateNumberTitle.AutoSize = true;
            lblPlateNumberTitle.BackColor = Color.FromArgb(246, 237, 233);
            lblPlateNumberTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblPlateNumberTitle.Location = new Point(8, 16);
            lblPlateNumberTitle.Margin = new Padding(0);
            lblPlateNumberTitle.Name = "lblPlateNumberTitle";
            lblPlateNumberTitle.Size = new Size(87, 28);
            lblPlateNumberTitle.TabIndex = 1;
            lblPlateNumberTitle.Text = "Đăng ký";
            // 
            // radioStatus
            // 
            radioStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            radioStatus.BackColor = Color.FromArgb(246, 237, 233);
            radioStatus.CheckedState.BorderColor = Color.Green;
            radioStatus.CheckedState.BorderThickness = 1;
            radioStatus.CheckedState.FillColor = Color.White;
            radioStatus.CheckedState.InnerColor = Color.Green;
            radioStatus.Location = new Point(280, 8);
            radioStatus.Margin = new Padding(0);
            radioStatus.Name = "radioStatus";
            radioStatus.ShadowDecoration.CustomizableEdges = customizableEdges1;
            radioStatus.Size = new Size(24, 24);
            radioStatus.TabIndex = 0;
            radioStatus.Text = "guna2CustomRadioButton1";
            radioStatus.UncheckedState.BorderColor = Color.Black;
            radioStatus.UncheckedState.BorderThickness = 1;
            radioStatus.UncheckedState.FillColor = Color.White;
            radioStatus.UncheckedState.InnerColor = Color.Transparent;
            // 
            // guna2Panel1
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.BorderRadius = 24;
            pnlMain.BorderThickness = 1;
            pnlMain.Controls.Add(radioStatus);
            pnlMain.Controls.Add(lblStatus);
            pnlMain.Controls.Add(lblPlateNumberTitle);
            pnlMain.Controls.Add(lblStatusTitle);
            pnlMain.Controls.Add(lblPlateNumber);
            pnlMain.CustomizableEdges = customizableEdges2;
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.FromArgb(246, 237, 233);
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "guna2Panel1";
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges3;
            pnlMain.Size = new Size(314, 88);
            pnlMain.TabIndex = 2;
            // 
            // UcVehicle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pnlMain);
            Margin = new Padding(0);
            Name = "UcVehicle";
            Size = new Size(314, 88);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2CustomRadioButton radioStatus;
        private Label lblPlateNumberTitle;
        private Label lblPlateNumber;
        private Label lblStatusTitle;
        private Label lblStatus;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
    }
}
