namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class UcLanguage
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
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            btnEn = new Guna.UI2.WinForms.Guna2Button();
            btnVi = new Guna.UI2.WinForms.Guna2Button();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderRadius = 27;
            guna2Panel1.Controls.Add(btnEn);
            guna2Panel1.Controls.Add(btnVi);
            guna2Panel1.CustomizableEdges = customizableEdges5;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.FillColor = Color.White;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Margin = new Padding(0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.Padding = new Padding(6);
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel1.Size = new Size(291, 54);
            guna2Panel1.TabIndex = 0;
            // 
            // btnEn
            // 
            btnEn.AutoRoundedCorners = true;
            btnEn.BorderRadius = 20;
            btnEn.CustomizableEdges = customizableEdges1;
            btnEn.DisabledState.BorderColor = Color.DarkGray;
            btnEn.DisabledState.CustomBorderColor = Color.DarkGray;
            btnEn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnEn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnEn.Dock = DockStyle.Right;
            btnEn.FillColor = Color.White;
            btnEn.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            btnEn.ForeColor = Color.Black;
            btnEn.Location = new Point(168, 6);
            btnEn.Margin = new Padding(2, 1, 2, 1);
            btnEn.Name = "btnEn";
            btnEn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnEn.Size = new Size(117, 42);
            btnEn.TabIndex = 3;
            btnEn.Text = "English";
            // 
            // btnVi
            // 
            btnVi.AutoRoundedCorners = true;
            btnVi.BorderRadius = 20;
            btnVi.CustomizableEdges = customizableEdges3;
            btnVi.DisabledState.BorderColor = Color.DarkGray;
            btnVi.DisabledState.CustomBorderColor = Color.DarkGray;
            btnVi.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnVi.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnVi.Dock = DockStyle.Left;
            btnVi.FillColor = Color.FromArgb(242, 102, 51);
            btnVi.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Pixel);
            btnVi.ForeColor = Color.White;
            btnVi.Location = new Point(6, 6);
            btnVi.Margin = new Padding(0);
            btnVi.Name = "btnVi";
            btnVi.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnVi.Size = new Size(152, 42);
            btnVi.TabIndex = 2;
            btnVi.Text = "Tiếng Việt";
            // 
            // UcLanguage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            Controls.Add(guna2Panel1);
            Margin = new Padding(0);
            Name = "UcLanguage";
            Size = new Size(291, 54);
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnEn;
        private Guna.UI2.WinForms.Guna2Button btnVi;
    }
}
