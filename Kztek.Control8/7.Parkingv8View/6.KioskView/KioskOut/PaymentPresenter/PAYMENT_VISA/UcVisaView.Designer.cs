namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class UcVisaView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcVisaView));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pictureBox1 = new PictureBox();
            lblTitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            lblTransactionId = new Label();
            lblGuide = new Label();
            uiLine1 = new Sunny.UI.UILine();
            lblTransationTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(250, 32);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblTitle.Location = new Point(8, 156);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(584, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Quét thẻ để thanh toán";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBack.AutoRoundedCorners = true;
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
            btnBack.Image = (Image)resources.GetObject("btnBack.Image");
            btnBack.Location = new Point(212, 385);
            btnBack.Margin = new Padding(4, 2, 4, 2);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(176, 56);
            btnBack.TabIndex = 9;
            btnBack.Text = "Quay lại";
            btnBack.TextFormatNoPrefix = true;
            // 
            // lblTransactionId
            // 
            lblTransactionId.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTransactionId.ForeColor = Color.FromArgb(242, 102, 51);
            lblTransactionId.Location = new Point(8, 321);
            lblTransactionId.Name = "lblTransactionId";
            lblTransactionId.Size = new Size(584, 28);
            lblTransactionId.TabIndex = 10;
            lblTransactionId.Text = "ID12345678";
            lblTransactionId.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblGuide
            // 
            lblGuide.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblGuide.ForeColor = Color.FromArgb(37, 28, 84);
            lblGuide.Location = new Point(8, 197);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(584, 51);
            lblGuide.TabIndex = 10;
            lblGuide.Text = "Vui lòng kiểm tra đúng thông tin giao dịch và quét thẻ ngân hàng vào thiết bị thanh toán";
            lblGuide.TextAlign = ContentAlignment.TopCenter;
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("Microsoft Sans Serif", 12F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.FromArgb(212, 212, 212);
            uiLine1.Location = new Point(32, 265);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(552, 1);
            uiLine1.TabIndex = 11;
            // 
            // lblTransationTitle
            // 
            lblTransationTitle.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTransationTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblTransationTitle.Location = new Point(8, 289);
            lblTransationTitle.Name = "lblTransationTitle";
            lblTransationTitle.Size = new Size(584, 32);
            lblTransationTitle.TabIndex = 1;
            lblTransationTitle.Text = "Mã giao dịch:";
            lblTransationTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UcVisaView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(uiLine1);
            Controls.Add(lblGuide);
            Controls.Add(lblTransactionId);
            Controls.Add(btnBack);
            Controls.Add(lblTransationTitle);
            Controls.Add(lblTitle);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "UcVisaView";
            Size = new Size(600, 473);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Label lblTransactionId;
        private Label lblGuide;
        private Sunny.UI.UILine uiLine1;
        private Label lblTransationTitle;
    }
}