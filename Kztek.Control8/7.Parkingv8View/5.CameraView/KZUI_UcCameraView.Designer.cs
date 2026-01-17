namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcCameraView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KZUI_UcCameraView));
            panelCameraView = new Guna.UI2.WinForms.Guna2Panel();
            lblResult = new Label();
            picStartLpr = new PictureBox();
            lblCameraMotion = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            picCameraType = new PictureBox();
            lblCameraName = new Label();
            panelCameraView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picStartLpr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCameraType).BeginInit();
            SuspendLayout();
            // 
            // panelCameraView
            // 
            panelCameraView.BorderColor = Color.FromArgb(226, 238, 255);
            panelCameraView.Controls.Add(lblResult);
            panelCameraView.Controls.Add(picStartLpr);
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
            // lblResult
            // 
            lblResult.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblResult.AutoSize = true;
            lblResult.BackColor = Color.Transparent;
            lblResult.Font = new Font("Segoe UI", 8F);
            lblResult.Location = new Point(403, 240);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(101, 13);
            lblResult.TabIndex = 0;
            lblResult.Text = "30A-12345 : 300ms";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            lblResult.Visible = false;
            // 
            // picStartLpr
            // 
            picStartLpr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picStartLpr.BackColor = Color.White;
            picStartLpr.Image = (Image)resources.GetObject("picStartLpr.Image");
            picStartLpr.Location = new Point(480, 4);
            picStartLpr.Name = "picStartLpr";
            picStartLpr.Size = new Size(24, 24);
            picStartLpr.SizeMode = PictureBoxSizeMode.StretchImage;
            picStartLpr.TabIndex = 0;
            picStartLpr.TabStop = false;
            picStartLpr.Visible = false;
            // 
            // lblCameraMotion
            // 
            lblCameraMotion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCameraMotion.AutoSize = true;
            lblCameraMotion.BackColor = Color.White;
            lblCameraMotion.Font = new Font("Segoe UI Semibold", 9F);
            lblCameraMotion.Location = new Point(4, 238);
            lblCameraMotion.Margin = new Padding(0);
            lblCameraMotion.Name = "lblCameraMotion";
            lblCameraMotion.Size = new Size(12, 15);
            lblCameraMotion.TabIndex = 0;
            lblCameraMotion.Text = "_";
            lblCameraMotion.TextAlign = ContentAlignment.MiddleLeft;
            lblCameraMotion.Visible = false;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 16;
            guna2Elipse1.TargetControl = panelCameraView;
            // 
            // picCameraType
            // 
            picCameraType.BackColor = Color.White;
            picCameraType.Image = Properties.Resources.icons8_exercise_32px_9;
            picCameraType.Location = new Point(4, 4);
            picCameraType.Margin = new Padding(0);
            picCameraType.Name = "picCameraType";
            picCameraType.Size = new Size(24, 24);
            picCameraType.SizeMode = PictureBoxSizeMode.Zoom;
            picCameraType.TabIndex = 1;
            picCameraType.TabStop = false;
            // 
            // lblCameraName
            // 
            lblCameraName.AutoSize = true;
            lblCameraName.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblCameraName.Location = new Point(36, 4);
            lblCameraName.Margin = new Padding(0);
            lblCameraName.Name = "lblCameraName";
            lblCameraName.Size = new Size(109, 19);
            lblCameraName.TabIndex = 0;
            lblCameraName.Text = "CAMERA NAME";
            lblCameraName.Visible = false;
            // 
            // KZUI_UcCameraView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            Controls.Add(lblCameraMotion);
            Controls.Add(picCameraType);
            Controls.Add(lblCameraName);
            Controls.Add(panelCameraView);
            Margin = new Padding(0);
            Name = "KZUI_UcCameraView";
            Size = new Size(506, 258);
            panelCameraView.ResumeLayout(false);
            panelCameraView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picStartLpr).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCameraType).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelCameraView;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Label lblCameraMotion;
        private PictureBox picCameraType;
        private PictureBox picStartLpr;
        private Label lblResult;
        private Label lblCameraName;
    }
}
