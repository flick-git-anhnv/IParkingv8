namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcImageView
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelCameraView = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            pic = new KZUI_PictureBox();
            panelCameraView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // panelCameraView
            // 
            panelCameraView.BorderColor = Color.FromArgb(226, 238, 255);
            panelCameraView.Controls.Add(lblTitle);
            panelCameraView.Controls.Add(pic);
            panelCameraView.CustomizableEdges = customizableEdges1;
            panelCameraView.Dock = DockStyle.Fill;
            panelCameraView.FillColor = Color.White;
            panelCameraView.Location = new Point(0, 0);
            panelCameraView.Margin = new Padding(0);
            panelCameraView.Name = "panelCameraView";
            panelCameraView.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelCameraView.Size = new Size(506, 258);
            panelCameraView.TabIndex = 0;
            // 
            // lblCameraName
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(4, 4);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblCameraName";
            lblTitle.Size = new Size(125, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CAMERA NAME";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 16;
            guna2Elipse1.TargetControl = panelCameraView;
            // 
            // pic
            // 
            pic.BackColor = Color.White;
            pic.Dock = DockStyle.Fill;
            pic.ErrorImage = null;
            pic.Image = Properties.Resources.Union;
            pic.KZUI_DefaultImage = Properties.Resources.Union;
            pic.KZUI_Image = null;
            pic.Location = new Point(0, 0);
            pic.Name = "pic";
            pic.Size = new Size(506, 258);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 12;
            pic.TabStop = false;
            // 
            // KZUI_UcImageView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            Controls.Add(panelCameraView);
            Margin = new Padding(0);
            Name = "KZUI_UcImageView";
            Size = new Size(506, 258);
            panelCameraView.ResumeLayout(false);
            panelCameraView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelCameraView;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Label lblTitle;
        private KZUI_PictureBox pic;
    }
}
