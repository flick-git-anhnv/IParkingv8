namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class UcConfirmKiosk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcConfirmKiosk));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            picStatus = new PictureBox();
            lblTitle = new Label();
            lblSubTitle = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            timerAutoClose = new System.Windows.Forms.Timer(components);
            lblTimeToMain = new Label();
            ((System.ComponentModel.ISupportInitialize)picStatus).BeginInit();
            SuspendLayout();
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 64;
            guna2Elipse1.TargetControl = this;
            // 
            // picStatus
            // 
            picStatus.BackColor = Color.Transparent;
            picStatus.Image = Properties.Resources.error;
            picStatus.Location = new Point(270, 32);
            picStatus.Name = "picStatus";
            picStatus.Size = new Size(60, 60);
            picStatus.SizeMode = PictureBoxSizeMode.Zoom;
            picStatus.TabIndex = 0;
            picStatus.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.FromArgb(255, 65, 65);
            lblTitle.Location = new Point(8, 116);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(584, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Không tìm thấy thông tin";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSubTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblSubTitle.Location = new Point(8, 157);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(584, 28);
            lblSubTitle.TabIndex = 2;
            lblSubTitle.Text = "Vui lòng thử lại";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
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
            btnBack.Location = new Point(136, 221);
            btnBack.Margin = new Padding(4, 2, 4, 2);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(328, 56);
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
            lblTimeToMain.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTimeToMain.ForeColor = Color.FromArgb(242, 102, 51);
            lblTimeToMain.Location = new Point(16, 296);
            lblTimeToMain.Name = "lblTimeToMain";
            lblTimeToMain.Size = new Size(568, 28);
            lblTimeToMain.TabIndex = 2;
            lblTimeToMain.Text = "Tự động quay lại";
            lblTimeToMain.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // UcConfirmKiosk
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnBack);
            Controls.Add(lblTimeToMain);
            Controls.Add(lblSubTitle);
            Controls.Add(lblTitle);
            Controls.Add(picStatus);
            DoubleBuffered = true;
            Name = "UcConfirmKiosk";
            Size = new Size(600, 351);
            ((System.ComponentModel.ISupportInitialize)picStatus).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private PictureBox picStatus;
        private Label lblSubTitle;
        private Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private System.Windows.Forms.Timer timerAutoClose;
        private Label lblTimeToMain;
    }
}