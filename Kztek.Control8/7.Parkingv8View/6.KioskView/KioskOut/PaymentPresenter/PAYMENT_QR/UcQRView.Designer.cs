namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class UcQRView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcQRView));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            picQR = new PictureBox();
            lblGuide = new Label();
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
            SuspendLayout();
            // 
            // picQR
            // 
            picQR.BackColor = Color.Transparent;
            picQR.Image = (Image)resources.GetObject("picQR.Image");
            picQR.Location = new Point(200, 32);
            picQR.Margin = new Padding(0);
            picQR.Name = "picQR";
            picQR.Size = new Size(200, 200);
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
            picQR.TabIndex = 0;
            picQR.TabStop = false;
            // 
            // lblGuide
            // 
            lblGuide.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblGuide.ForeColor = Color.FromArgb(37, 28, 84);
            lblGuide.Location = new Point(8, 240);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(584, 40);
            lblGuide.TabIndex = 2;
            lblGuide.Text = "Quét mã để thanh toán";
            lblGuide.TextAlign = ContentAlignment.MiddleCenter;
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
            btnBack.Location = new Point(212, 333);
            btnBack.Margin = new Padding(0);
            btnBack.Name = "btnBack";
            btnBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnBack.Size = new Size(176, 56);
            btnBack.TabIndex = 9;
            btnBack.Text = "Quay lại";
            btnBack.TextFormatNoPrefix = true;
            // 
            // UcQRView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnBack);
            Controls.Add(lblGuide);
            Controls.Add(picQR);
            Name = "UcQRView";
            Size = new Size(600, 421);
            ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox picQR;
        private Label lblGuide;
        private Guna.UI2.WinForms.Guna2Button btnBack;
    }
}