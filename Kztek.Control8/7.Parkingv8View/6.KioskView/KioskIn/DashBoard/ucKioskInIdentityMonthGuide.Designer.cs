using Kztek.Control8.UserControls.KioskOut;

namespace Kztek.Control8.KioskIn.DashBoard
{
    partial class ucKioskInIdentityMonthGuide
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucKioskInIdentityMonthGuide));
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnCapture = new Guna.UI2.WinForms.Guna2Button();
            panel15 = new Panel();
            uiPanel1 = new Sunny.UI.UIPanel();
            pictureBox1 = new PictureBox();
            lblTitle = new Label();
            lblSubTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2Panel1.SuspendLayout();
            panel15.SuspendLayout();
            uiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BorderColor = Color.White;
            guna2Panel1.BorderRadius = 40;
            guna2Panel1.BorderThickness = 3;
            guna2Panel1.Controls.Add(btnCapture);
            guna2Panel1.Controls.Add(panel15);
            guna2Panel1.Controls.Add(lblSubTitle);
            guna2Panel1.CustomizableEdges = customizableEdges7;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.FillColor = Color.Transparent;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Margin = new Padding(0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.Padding = new Padding(0, 32, 0, 32);
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel1.Size = new Size(558, 423);
            guna2Panel1.TabIndex = 0;
            // 
            // btnCapture
            // 
            btnCapture.AutoRoundedCorners = true;
            btnCapture.BorderColor = Color.White;
            btnCapture.BorderRadius = 29;
            btnCapture.BorderThickness = 1;
            btnCapture.CustomizableEdges = customizableEdges5;
            btnCapture.DisabledState.BorderColor = Color.DarkGray;
            btnCapture.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCapture.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCapture.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCapture.FillColor = Color.FromArgb(242, 102, 51);
            btnCapture.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            btnCapture.ForeColor = Color.White;
            btnCapture.Location = new Point(172, 328);
            btnCapture.Margin = new Padding(0);
            btnCapture.Name = "btnCapture";
            btnCapture.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCapture.Size = new Size(215, 61);
            btnCapture.TabIndex = 9;
            btnCapture.Text = "Chụp biển số";
            // 
            // panel15
            // 
            panel15.Controls.Add(uiPanel1);
            panel15.Controls.Add(lblTitle);
            panel15.Location = new Point(171, 32);
            panel15.Margin = new Padding(0);
            panel15.Name = "panel15";
            panel15.Size = new Size(215, 178);
            panel15.TabIndex = 11;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(pictureBox1);
            uiPanel1.Dock = DockStyle.Fill;
            uiPanel1.Font = new Font("Microsoft Sans Serif", 12F);
            uiPanel1.Location = new Point(0, 48);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Radius = 32;
            uiPanel1.RectColor = Color.White;
            uiPanel1.Size = new Size(215, 130);
            uiPanel1.TabIndex = 13;
            uiPanel1.Text = null;
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(215, 130);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.FromArgb(37, 28, 84);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(215, 48);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Vé xe tháng";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            lblSubTitle.AutoSize = false;
            lblSubTitle.BackColor = Color.Transparent;
            lblSubTitle.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSubTitle.Location = new Point(0, 232);
            lblSubTitle.Margin = new Padding(0);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(560, 76);
            lblSubTitle.TabIndex = 10;
            lblSubTitle.Text = null;
            // 
            // ucKioskInIdentityMonthGuide
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(193, 220, 250);
            Controls.Add(guna2Panel1);
            DoubleBuffered = true;
            Name = "ucKioskInIdentityMonthGuide";
            Size = new Size(558, 423);
            guna2Panel1.ResumeLayout(false);
            panel15.ResumeLayout(false);
            uiPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnCapture;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubTitle;
        private Panel panel15;
        private Label lblTitle;
        private Sunny.UI.UIPanel uiPanel1;
        private PictureBox pictureBox1;
    }
}
