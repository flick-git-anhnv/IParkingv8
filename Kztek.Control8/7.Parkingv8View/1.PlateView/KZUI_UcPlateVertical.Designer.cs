namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcPlateVertical
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            pic = new Guna.UI2.WinForms.Guna2PictureBox();
            txtPlate = new Guna.UI2.WinForms.Guna2TextBox();
            lblTitle = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Gainsboro;
            guna2Panel1.BorderColor = Color.FromArgb(80, 30, 26, 80);
            guna2Panel1.BorderRadius = 8;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(pic);
            guna2Panel1.Controls.Add(txtPlate);
            guna2Panel1.Controls.Add(lblTitle);
            guna2Panel1.CustomizableEdges = customizableEdges7;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2Panel1.Size = new Size(188, 199);
            guna2Panel1.TabIndex = 13;
            // 
            // pic
            // 
            pic.BackColor = Color.White;
            pic.CustomizableEdges = customizableEdges1;
            pic.Dock = DockStyle.Fill;
            pic.Image = Properties.Resources.Union;
            pic.ImageRotate = 0F;
            pic.Location = new Point(0, 32);
            pic.Margin = new Padding(0);
            pic.Name = "pic";
            pic.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pic.Size = new Size(188, 131);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 16;
            pic.TabStop = false;
            // 
            // txtPlate
            // 
            txtPlate.BackColor = Color.White;
            txtPlate.BorderColor = Color.FromArgb(6, 59, 167);
            txtPlate.BorderRadius = 8;
            txtPlate.CharacterCasing = CharacterCasing.Upper;
            customizableEdges3.TopLeft = false;
            customizableEdges3.TopRight = false;
            txtPlate.CustomizableEdges = customizableEdges3;
            txtPlate.DefaultText = "";
            txtPlate.DisabledState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPlate.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPlate.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPlate.Dock = DockStyle.Bottom;
            txtPlate.FocusedState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.FocusedState.ForeColor = Color.FromArgb(30, 26, 80);
            txtPlate.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPlate.ForeColor = Color.FromArgb(6, 59, 167);
            txtPlate.HoverState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.HoverState.ForeColor = Color.FromArgb(30, 26, 80);
            txtPlate.Location = new Point(0, 163);
            txtPlate.Margin = new Padding(0);
            txtPlate.Name = "txtPlate";
            txtPlate.PlaceholderText = "_ _ _ _ _ _ _ _";
            txtPlate.SelectedText = "";
            txtPlate.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtPlate.Size = new Size(188, 36);
            txtPlate.TabIndex = 13;
            txtPlate.TextAlign = HorizontalAlignment.Center;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.White;
            lblTitle.BorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.BorderRadius = 8;
            lblTitle.BorderThickness = 1;
            customizableEdges5.BottomLeft = false;
            customizableEdges5.BottomRight = false;
            lblTitle.CustomizableEdges = customizableEdges5;
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
            lblTitle.ShadowDecoration.CustomizableEdges = customizableEdges6;
            lblTitle.Size = new Size(188, 32);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "BIỂN SỐ VÀO";
            // 
            // KZUI_UcPlateVertical
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Panel1);
            Margin = new Padding(0);
            Name = "KZUI_UcPlateVertical";
            Size = new Size(188, 199);
            guna2Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtPlate;
        private Guna.UI2.WinForms.Guna2PictureBox pic;
    }
}
