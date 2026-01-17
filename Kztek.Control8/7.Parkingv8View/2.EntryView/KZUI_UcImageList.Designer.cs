namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcImageList
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
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            panelImageList = new KZUI_DirectionPanel();
            lblTitle = new Guna.UI2.WinForms.Guna2Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BorderRadius = 8;
            panelMain.Controls.Add(panelImageList);
            panelMain.Controls.Add(lblTitle);
            panelMain.CustomizableEdges = customizableEdges5;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.FromArgb(226, 238, 255);
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelMain.Size = new Size(295, 521);
            panelMain.TabIndex = 0;
            // 
            // panelImageList
            // 
            panelImageList.BackColor = Color.White;
            panelImageList.BorderRadius = 16;
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            panelImageList.CustomizableEdges = customizableEdges1;
            panelImageList.CustomSpacing = "";
            panelImageList.Dock = DockStyle.Fill;
            panelImageList.FillColor = Color.FromArgb(226, 238, 255);
            panelImageList.KZUI_ControlDirection = EmControlDirection.VERTICAL;
            panelImageList.Location = new Point(0, 32);
            panelImageList.Name = "panelImageList";
            panelImageList.Padding = new Padding(8, 8, 8, 0);
            panelImageList.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelImageList.Size = new Size(295, 489);
            panelImageList.SpaceBetween = false;
            panelImageList.Spacing = 8;
            panelImageList.TabIndex = 15;
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
            lblTitle.Size = new Size(295, 32);
            lblTitle.TabIndex = 13;
            lblTitle.Text = "ẢNH VÀO";
            // 
            // KZUI_UcImageList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelMain);
            Name = "KZUI_UcImageList";
            Size = new Size(295, 521);
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Button lblTitle;
        private KZUI_DirectionPanel panelImageList;
    }
}
