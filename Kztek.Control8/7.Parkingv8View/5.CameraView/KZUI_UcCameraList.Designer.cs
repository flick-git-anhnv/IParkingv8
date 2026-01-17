namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcCameraList
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelCameraList = new Guna.UI2.WinForms.Guna2Panel();
            panelCameras = new KZUI_DirectionPanel();
            lblTitle = new Guna.UI2.WinForms.Guna2Button();
            panelCameraList.SuspendLayout();
            SuspendLayout();
            // 
            // panelCameraList
            // 
            panelCameraList.BorderRadius = 16;
            panelCameraList.Controls.Add(panelCameras);
            panelCameraList.Controls.Add(lblTitle);
            panelCameraList.CustomizableEdges = customizableEdges5;
            panelCameraList.Dock = DockStyle.Fill;
            panelCameraList.FillColor = Color.FromArgb(226, 238, 255);
            panelCameraList.Location = new Point(0, 0);
            panelCameraList.Name = "panelCameraList";
            panelCameraList.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelCameraList.Size = new Size(294, 521);
            panelCameraList.TabIndex = 0;
            // 
            // panelCameras
            // 
            panelCameras.BackColor = Color.White;
            panelCameras.BorderRadius = 16;
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            panelCameras.CustomizableEdges = customizableEdges1;
            panelCameras.CustomSpacing = "";
            panelCameras.Dock = DockStyle.Fill;
            panelCameras.FillColor = Color.FromArgb(226, 238, 255);
            panelCameras.KZUI_ControlDirection = EmControlDirection.VERTICAL;
            panelCameras.Location = new Point(0, 32);
            panelCameras.Name = "panelCameras";
            panelCameras.Padding = new Padding(8, 8, 8, 0);
            panelCameras.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelCameras.Size = new Size(294, 489);
            panelCameras.SpaceBetween = true;
            panelCameras.Spacing = 8;
            panelCameras.TabIndex = 15;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.White;
            lblTitle.BorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.BorderRadius = 8;
            lblTitle.BorderThickness = 1;
            customizableEdges3.BottomLeft = false;
            customizableEdges3.BottomRight = false;
            lblTitle.CustomizableEdges = customizableEdges3;
            lblTitle.DisabledState.BorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.CustomBorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.FillColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.ForeColor = Color.White;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Enabled = false;
            lblTitle.FillColor = Color.FromArgb(6, 59, 167);
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.ShadowDecoration.CustomizableEdges = customizableEdges4;
            lblTitle.Size = new Size(294, 32);
            lblTitle.TabIndex = 13;
            lblTitle.Text = "CAMERA LỐI VÀO";
            // 
            // KZUI_UcCameraList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelCameraList);
            Name = "KZUI_UcCameraList";
            Size = new Size(294, 521);
            panelCameraList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelCameraList;
        private Guna.UI2.WinForms.Guna2Button lblTitle;
        private KZUI_DirectionPanel panelCameras;
    }
}
