namespace Kztek.Control8.BaseKiosk
{
    partial class UcConfirmKioskDaily
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblSubTitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            timerAutoClose = new System.Windows.Forms.Timer(components);
            lblTimeToMain = new Label();
            lblDetectedPlateTitle = new Label();
            lblDetectedPlate = new Label();
            picVehicle = new Guna.UI2.WinForms.Guna2PictureBox();
            uiLine1 = new Sunny.UI.UILine();
            panelDetectedPlate = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            lblAccessKeyName = new Label();
            lblAccessKeyNameTitle = new Label();
            guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            lblAccessKeyCollection = new Label();
            lblAccessKeyCollectionTitle = new Label();
            picPanorama = new Guna.UI2.WinForms.Guna2PictureBox();
            guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            ((System.ComponentModel.ISupportInitialize)picVehicle).BeginInit();
            panelDetectedPlate.SuspendLayout();
            guna2Panel2.SuspendLayout();
            guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPanorama).BeginInit();
            guna2Panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.FromArgb(193, 220, 250);
            lblTitle.Font = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.FromArgb(255, 65, 65);
            lblTitle.Location = new Point(389, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(502, 54);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Không tìm thấy thông tin";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubTitle.AutoSize = true;
            lblSubTitle.BackColor = Color.FromArgb(193, 220, 250);
            lblSubTitle.Font = new Font("Segoe UI", 32F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSubTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblSubTitle.Location = new Point(522, 80);
            lblSubTitle.Margin = new Padding(0);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(236, 45);
            lblSubTitle.TabIndex = 2;
            lblSubTitle.Text = "Vui lòng thử lại";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnBack.AutoRoundedCorners = true;
            btnBack.BackColor = Color.Transparent;
            btnBack.BorderColor = Color.White;
            btnBack.BorderRadius = 27;
            btnBack.BorderThickness = 1;
            btnBack.CustomizableEdges = customizableEdges1;
            btnBack.DisabledState.BorderColor = Color.DarkGray;
            btnBack.DisabledState.CustomBorderColor = Color.DarkGray;
            btnBack.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnBack.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnBack.FillColor = Color.FromArgb(242, 102, 51);
            btnBack.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            btnBack.ForeColor = Color.White;
            btnBack.Image = Properties.Resources.arrow_narrow_left;
            btnBack.Location = new Point(464, 816);
            btnBack.Margin = new Padding(0);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(352, 56);
            btnBack.TabIndex = 9;
            btnBack.Text = "Quay lại màn hình chính";
            btnBack.TextFormatNoPrefix = true;
            // 
            // timerAutoClose
            // 
            timerAutoClose.Interval = 1000;
            timerAutoClose.Tick += TimerAutoClose_Tick;
            // 
            // lblTimeToMain
            // 
            lblTimeToMain.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTimeToMain.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTimeToMain.ForeColor = Color.FromArgb(242, 102, 51);
            lblTimeToMain.Location = new Point(60, 896);
            lblTimeToMain.Margin = new Padding(0);
            lblTimeToMain.Name = "lblTimeToMain";
            lblTimeToMain.Size = new Size(1160, 28);
            lblTimeToMain.TabIndex = 2;
            lblTimeToMain.Text = "Tự động quay lại";
            lblTimeToMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDetectedPlateTitle
            // 
            lblDetectedPlateTitle.BackColor = Color.WhiteSmoke;
            lblDetectedPlateTitle.Dock = DockStyle.Top;
            lblDetectedPlateTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblDetectedPlateTitle.Location = new Point(16, 8);
            lblDetectedPlateTitle.Margin = new Padding(0);
            lblDetectedPlateTitle.Name = "lblDetectedPlateTitle";
            lblDetectedPlateTitle.Size = new Size(341, 38);
            lblDetectedPlateTitle.TabIndex = 11;
            lblDetectedPlateTitle.Text = "Biển số nhận dạng:";
            lblDetectedPlateTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDetectedPlate
            // 
            lblDetectedPlate.AutoEllipsis = true;
            lblDetectedPlate.BackColor = Color.WhiteSmoke;
            lblDetectedPlate.Dock = DockStyle.Fill;
            lblDetectedPlate.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblDetectedPlate.ForeColor = Color.FromArgb(37, 28, 84);
            lblDetectedPlate.Location = new Point(16, 46);
            lblDetectedPlate.Margin = new Padding(0);
            lblDetectedPlate.Name = "lblDetectedPlate";
            lblDetectedPlate.Size = new Size(341, 34);
            lblDetectedPlate.TabIndex = 12;
            lblDetectedPlate.Text = "22A-765.24";
            lblDetectedPlate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picVehicle
            // 
            picVehicle.BackColor = Color.FromArgb(193, 220, 250);
            picVehicle.BorderRadius = 20;
            picVehicle.CustomizableEdges = customizableEdges3;
            picVehicle.FillColor = Color.WhiteSmoke;
            picVehicle.ImageRotate = 0F;
            picVehicle.Location = new Point(664, 168);
            picVehicle.Margin = new Padding(0);
            picVehicle.Name = "picVehicle";
            picVehicle.ShadowDecoration.CustomizableEdges = customizableEdges4;
            picVehicle.Size = new Size(558, 392);
            picVehicle.SizeMode = PictureBoxSizeMode.StretchImage;
            picVehicle.TabIndex = 10;
            picVehicle.TabStop = false;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Red;
            uiLine1.FillColor = Color.Red;
            uiLine1.Font = new Font("Microsoft Sans Serif", 12F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(212, 212, 212);
            uiLine1.LineColor2 = Color.FromArgb(212, 212, 212);
            uiLine1.Location = new Point(56, 136);
            uiLine1.Margin = new Padding(0);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(1160, 1);
            uiLine1.TabIndex = 13;
            // 
            // panelDetectedPlate
            // 
            panelDetectedPlate.BackColor = Color.FromArgb(193, 220, 250);
            panelDetectedPlate.BorderRadius = 16;
            panelDetectedPlate.BorderThickness = 1;
            panelDetectedPlate.Controls.Add(lblDetectedPlate);
            panelDetectedPlate.Controls.Add(lblDetectedPlateTitle);
            panelDetectedPlate.CustomizableEdges = customizableEdges5;
            panelDetectedPlate.FillColor = Color.WhiteSmoke;
            panelDetectedPlate.Location = new Point(760, 16);
            panelDetectedPlate.Margin = new Padding(0);
            panelDetectedPlate.Name = "panelDetectedPlate";
            panelDetectedPlate.Padding = new Padding(16, 8, 0, 8);
            panelDetectedPlate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelDetectedPlate.Size = new Size(357, 88);
            panelDetectedPlate.TabIndex = 14;
            // 
            // guna2Panel2
            // 
            guna2Panel2.BackColor = Color.FromArgb(193, 220, 250);
            guna2Panel2.BorderRadius = 16;
            guna2Panel2.BorderThickness = 1;
            guna2Panel2.Controls.Add(lblAccessKeyName);
            guna2Panel2.Controls.Add(lblAccessKeyNameTitle);
            guna2Panel2.CustomizableEdges = customizableEdges7;
            guna2Panel2.FillColor = Color.WhiteSmoke;
            guna2Panel2.Location = new Point(16, 16);
            guna2Panel2.Margin = new Padding(0);
            guna2Panel2.Name = "guna2Panel2";
            guna2Panel2.Padding = new Padding(16, 8, 0, 8);
            guna2Panel2.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel2.Size = new Size(357, 88);
            guna2Panel2.TabIndex = 14;
            // 
            // lblAccessKeyName
            // 
            lblAccessKeyName.AutoEllipsis = true;
            lblAccessKeyName.BackColor = Color.WhiteSmoke;
            lblAccessKeyName.Dock = DockStyle.Fill;
            lblAccessKeyName.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblAccessKeyName.ForeColor = Color.FromArgb(37, 28, 84);
            lblAccessKeyName.Location = new Point(16, 46);
            lblAccessKeyName.Margin = new Padding(0);
            lblAccessKeyName.Name = "lblAccessKeyName";
            lblAccessKeyName.Size = new Size(341, 34);
            lblAccessKeyName.TabIndex = 12;
            lblAccessKeyName.Text = "VE001";
            lblAccessKeyName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAccessKeyNameTitle
            // 
            lblAccessKeyNameTitle.BackColor = Color.WhiteSmoke;
            lblAccessKeyNameTitle.Dock = DockStyle.Top;
            lblAccessKeyNameTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblAccessKeyNameTitle.Location = new Point(16, 8);
            lblAccessKeyNameTitle.Margin = new Padding(0);
            lblAccessKeyNameTitle.Name = "lblAccessKeyNameTitle";
            lblAccessKeyNameTitle.Size = new Size(341, 38);
            lblAccessKeyNameTitle.TabIndex = 11;
            lblAccessKeyNameTitle.Text = "Vé xe:";
            lblAccessKeyNameTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // guna2Panel4
            // 
            guna2Panel4.BackColor = Color.FromArgb(193, 220, 250);
            guna2Panel4.BorderRadius = 16;
            guna2Panel4.BorderThickness = 1;
            guna2Panel4.Controls.Add(lblAccessKeyCollection);
            guna2Panel4.Controls.Add(lblAccessKeyCollectionTitle);
            guna2Panel4.CustomizableEdges = customizableEdges9;
            guna2Panel4.FillColor = Color.WhiteSmoke;
            guna2Panel4.Location = new Point(389, 16);
            guna2Panel4.Margin = new Padding(0);
            guna2Panel4.Name = "guna2Panel4";
            guna2Panel4.Padding = new Padding(16, 8, 0, 8);
            guna2Panel4.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel4.Size = new Size(357, 88);
            guna2Panel4.TabIndex = 14;
            // 
            // lblAccessKeyCollection
            // 
            lblAccessKeyCollection.AutoEllipsis = true;
            lblAccessKeyCollection.BackColor = Color.WhiteSmoke;
            lblAccessKeyCollection.Dock = DockStyle.Fill;
            lblAccessKeyCollection.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblAccessKeyCollection.ForeColor = Color.FromArgb(37, 28, 84);
            lblAccessKeyCollection.Location = new Point(16, 46);
            lblAccessKeyCollection.Margin = new Padding(0);
            lblAccessKeyCollection.Name = "lblAccessKeyCollection";
            lblAccessKeyCollection.Size = new Size(341, 34);
            lblAccessKeyCollection.TabIndex = 12;
            lblAccessKeyCollection.Text = "Xe máy lượt";
            lblAccessKeyCollection.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAccessKeyCollectionTitle
            // 
            lblAccessKeyCollectionTitle.BackColor = Color.WhiteSmoke;
            lblAccessKeyCollectionTitle.Dock = DockStyle.Top;
            lblAccessKeyCollectionTitle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblAccessKeyCollectionTitle.Location = new Point(16, 8);
            lblAccessKeyCollectionTitle.Margin = new Padding(0);
            lblAccessKeyCollectionTitle.Name = "lblAccessKeyCollectionTitle";
            lblAccessKeyCollectionTitle.Size = new Size(341, 38);
            lblAccessKeyCollectionTitle.TabIndex = 11;
            lblAccessKeyCollectionTitle.Text = "Loại vé";
            lblAccessKeyCollectionTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // picPanorama
            // 
            picPanorama.BackColor = Color.FromArgb(193, 220, 250);
            picPanorama.BorderRadius = 20;
            picPanorama.CustomizableEdges = customizableEdges11;
            picPanorama.FillColor = Color.WhiteSmoke;
            picPanorama.ImageRotate = 0F;
            picPanorama.Location = new Point(56, 168);
            picPanorama.Margin = new Padding(0);
            picPanorama.Name = "picPanorama";
            picPanorama.ShadowDecoration.CustomizableEdges = customizableEdges12;
            picPanorama.Size = new Size(558, 392);
            picPanorama.SizeMode = PictureBoxSizeMode.StretchImage;
            picPanorama.TabIndex = 10;
            picPanorama.TabStop = false;
            // 
            // guna2Panel3
            // 
            guna2Panel3.BackColor = Color.Transparent;
            guna2Panel3.BorderColor = Color.White;
            guna2Panel3.BorderRadius = 20;
            guna2Panel3.BorderThickness = 2;
            guna2Panel3.Controls.Add(guna2Panel2);
            guna2Panel3.Controls.Add(guna2Panel4);
            guna2Panel3.Controls.Add(panelDetectedPlate);
            guna2Panel3.CustomizableEdges = customizableEdges13;
            guna2Panel3.Location = new Point(72, 584);
            guna2Panel3.Margin = new Padding(0);
            guna2Panel3.Name = "guna2Panel3";
            guna2Panel3.Padding = new Padding(16);
            guna2Panel3.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2Panel3.Size = new Size(1136, 120);
            guna2Panel3.TabIndex = 16;
            // 
            // UcConfirmKioskDaily
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(193, 220, 250);
            Controls.Add(lblTitle);
            Controls.Add(lblTimeToMain);
            Controls.Add(picPanorama);
            Controls.Add(picVehicle);
            Controls.Add(btnBack);
            Controls.Add(lblSubTitle);
            Controls.Add(uiLine1);
            Controls.Add(guna2Panel3);
            DoubleBuffered = true;
            ForeColor = Color.FromArgb(42, 47, 48);
            Name = "UcConfirmKioskDaily";
            Size = new Size(1280, 940);
            ((System.ComponentModel.ISupportInitialize)picVehicle).EndInit();
            panelDetectedPlate.ResumeLayout(false);
            guna2Panel2.ResumeLayout(false);
            guna2Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picPanorama).EndInit();
            guna2Panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblSubTitle;
        private Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private System.Windows.Forms.Timer timerAutoClose;
        private Label lblTimeToMain;
        private Label lblDetectedPlateTitle;
        private Label lblDetectedPlate;
        private Guna.UI2.WinForms.Guna2PictureBox picVehicle;
        private Sunny.UI.UILine uiLine1;
        private Guna.UI2.WinForms.Guna2Panel panelDetectedPlate;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private Label lblAccessKeyCollection;
        private Label lblAccessKeyCollectionTitle;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Label lblAccessKeyName;
        private Label lblAccessKeyNameTitle;
        private Guna.UI2.WinForms.Guna2PictureBox picPanorama;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
    }
}