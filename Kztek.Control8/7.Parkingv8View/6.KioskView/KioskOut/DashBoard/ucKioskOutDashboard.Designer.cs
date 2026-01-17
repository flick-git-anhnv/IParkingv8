namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class ucKioskOutDashboard
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
            ucKioskOutIdentityMonthGuide1 = new Kztek.Control8.UserControls.KioskOut.ucKioskOutIdentityMonthGuide();
            lblSubTitle = new Label();
            ucPaymentSupport1 = new ucPaymentSupport();
            lblPaymentSupport = new Label();
            lblTitle = new Label();
            ucKioskOutIdentityDailyGuide1 = new Kztek.Control8.UserControls.KioskOut.ucKioskOutIdentityDailyGuide();
            SuspendLayout();
            // 
            // ucKioskOutIdentityMonthGuide1
            // 
            ucKioskOutIdentityMonthGuide1.BackColor = Color.Transparent;
            ucKioskOutIdentityMonthGuide1.Location = new Point(40, 200);
            ucKioskOutIdentityMonthGuide1.Name = "ucKioskOutIdentityMonthGuide1";
            ucKioskOutIdentityMonthGuide1.Size = new Size(558, 423);
            ucKioskOutIdentityMonthGuide1.TabIndex = 2;
            // 
            // lblSubTitle
            // 
            lblSubTitle.BackColor = Color.Transparent;
            lblSubTitle.Font = new Font("Segoe UI", 32F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSubTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblSubTitle.Location = new Point(40, 104);
            lblSubTitle.Margin = new Padding(0);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(1160, 48);
            lblSubTitle.TabIndex = 4;
            lblSubTitle.Text = "Vui lòng chọn loại thẻ quý khách đang sử dụng";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucPaymentSupport1
            // 
            ucPaymentSupport1.BackColor = Color.Transparent;
            ucPaymentSupport1.Location = new Point(40, 736);
            ucPaymentSupport1.Name = "ucPaymentSupport1";
            ucPaymentSupport1.Size = new Size(1160, 148);
            ucPaymentSupport1.TabIndex = 9;
            // 
            // lblPaymentSupport
            // 
            lblPaymentSupport.BackColor = Color.Transparent;
            lblPaymentSupport.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblPaymentSupport.ForeColor = Color.FromArgb(37, 28, 84);
            lblPaymentSupport.Location = new Point(40, 680);
            lblPaymentSupport.Margin = new Padding(0);
            lblPaymentSupport.Name = "lblPaymentSupport";
            lblPaymentSupport.Size = new Size(1160, 32);
            lblPaymentSupport.TabIndex = 6;
            lblPaymentSupport.Text = "Hỗ trợ các hình thức thanh toán";
            lblPaymentSupport.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 40F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblTitle.Location = new Point(40, 55);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1160, 48);
            lblTitle.TabIndex = 3;
            lblTitle.Text = "HỆ THỐNG THANH TOÁN VÉ XE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucKioskOutIdentityDailyGuide1
            // 
            ucKioskOutIdentityDailyGuide1.BackColor = Color.Transparent;
            ucKioskOutIdentityDailyGuide1.Location = new Point(642, 200);
            ucKioskOutIdentityDailyGuide1.Name = "ucKioskOutIdentityDailyGuide1";
            ucKioskOutIdentityDailyGuide1.Size = new Size(558, 423);
            ucKioskOutIdentityDailyGuide1.TabIndex = 6;
            // 
            // ucKioskOutDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(193, 220, 250);
            Controls.Add(ucPaymentSupport1);
            Controls.Add(ucKioskOutIdentityDailyGuide1);
            Controls.Add(lblPaymentSupport);
            Controls.Add(lblTitle);
            Controls.Add(ucKioskOutIdentityMonthGuide1);
            Controls.Add(lblSubTitle);
            DoubleBuffered = true;
            Name = "ucKioskOutDashboard";
            Size = new Size(1280, 940);
            ResumeLayout(false);
        }

        #endregion
        private UserControls.KioskOut.ucKioskOutIdentityMonthGuide ucKioskOutIdentityMonthGuide1;
        private Label lblSubTitle;
        private ucPaymentSupport ucPaymentSupport1;
        private Label lblPaymentSupport;
        private Label lblTitle;
        private UserControls.KioskOut.ucKioskOutIdentityDailyGuide ucKioskOutIdentityDailyGuide1;
    }
}
