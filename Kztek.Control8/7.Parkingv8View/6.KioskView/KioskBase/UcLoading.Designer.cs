namespace Kztek.Control8.KioskBase
{
    partial class UcLoading
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
            lblSubTitle = new Label();
            lblTitle = new Label();
            guna2CircleProgressBar1 = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            SuspendLayout();
            // 
            // lblSubTitle
            // 
            lblSubTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSubTitle.Font = new Font("Segoe UI", 32F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblSubTitle.Location = new Point(16, 72);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(568, 45);
            lblSubTitle.TabIndex = 0;
            lblSubTitle.Text = "Vui lòng chờ trong giây lát";
            lblSubTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Font = new Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.Location = new Point(16, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(568, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Vui lòng chờ trong giây lát";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // guna2CircleProgressBar1
            // 
            guna2CircleProgressBar1.Animated = true;
            guna2CircleProgressBar1.FillColor = Color.FromArgb(200, 213, 218, 223);
            guna2CircleProgressBar1.Font = new Font("Segoe UI", 12F);
            guna2CircleProgressBar1.ForeColor = Color.White;
            guna2CircleProgressBar1.InnerColor = Color.CornflowerBlue;
            guna2CircleProgressBar1.Location = new Point(232, 184);
            guna2CircleProgressBar1.Minimum = 0;
            guna2CircleProgressBar1.Name = "guna2CircleProgressBar1";
            guna2CircleProgressBar1.ShadowDecoration.CustomizableEdges = customizableEdges1;
            guna2CircleProgressBar1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CircleProgressBar1.Size = new Size(130, 130);
            guna2CircleProgressBar1.TabIndex = 1;
            guna2CircleProgressBar1.Text = "guna2CircleProgressBar1";
            guna2CircleProgressBar1.Value = 75;
            // 
            // UcLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(guna2CircleProgressBar1);
            Controls.Add(lblTitle);
            Controls.Add(lblSubTitle);
            Name = "UcLoading";
            Size = new Size(600, 352);
            ResumeLayout(false);
        }

        #endregion

        private Label lblSubTitle;
        private Label lblTitle;
        private Guna.UI2.WinForms.Guna2CircleProgressBar guna2CircleProgressBar1;
    }
}
