namespace ILedv8.UserControls
{
    partial class ucLed
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            lblTime = new Label();
            panelLines = new Panel();
            lblLedName = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.Gray;
            guna2Panel1.BorderRadius = 16;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(lblTime);
            guna2Panel1.Controls.Add(panelLines);
            guna2Panel1.Controls.Add(lblLedName);
            guna2Panel1.CustomizableEdges = customizableEdges3;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Panel1.Size = new Size(287, 456);
            guna2Panel1.TabIndex = 0;
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTime.Font = new Font("Segoe UI", 12F);
            lblTime.Location = new Point(8, 424);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(272, 23);
            lblTime.TabIndex = 16;
            lblTime.Text = "10:00:00 11/11/2222";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelLines
            // 
            panelLines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLines.AutoScroll = true;
            panelLines.Location = new Point(8, 48);
            panelLines.Name = "panelLines";
            panelLines.Size = new Size(272, 368);
            panelLines.TabIndex = 15;
            // 
            // lblLedName
            // 
            lblLedName.BackColor = Color.White;
            lblLedName.BorderColor = Color.DarkBlue;
            lblLedName.BorderRadius = 8;
            lblLedName.BorderThickness = 1;
            customizableEdges1.BottomLeft = false;
            customizableEdges1.BottomRight = false;
            lblLedName.CustomizableEdges = customizableEdges1;
            lblLedName.DisabledState.BorderColor = Color.DarkBlue;
            lblLedName.DisabledState.CustomBorderColor = Color.DarkBlue;
            lblLedName.DisabledState.FillColor = Color.DarkBlue;
            lblLedName.DisabledState.ForeColor = Color.White;
            lblLedName.Dock = DockStyle.Top;
            lblLedName.Enabled = false;
            lblLedName.FillColor = Color.DarkBlue;
            lblLedName.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblLedName.ForeColor = Color.White;
            lblLedName.Location = new Point(0, 0);
            lblLedName.Margin = new Padding(0);
            lblLedName.Name = "lblLedName";
            lblLedName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            lblLedName.Size = new Size(287, 40);
            lblLedName.TabIndex = 14;
            lblLedName.Text = "LED_NAME";
            // 
            // ucLed
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Panel1);
            Name = "ucLed";
            Size = new Size(287, 456);
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button lblLedName;
        private Panel panelLines;
        private Label lblTime;
    }
}
