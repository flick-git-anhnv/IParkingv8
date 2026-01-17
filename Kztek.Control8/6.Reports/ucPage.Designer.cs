namespace Kztek.Control8.UserControls.ReportUcs
{
    partial class ucPage
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnPage = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // btnPage
            // 
            btnPage.BorderRadius = 4;
            btnPage.BorderThickness = 1;
            btnPage.CustomizableEdges = customizableEdges1;
            btnPage.DisabledState.BorderColor = Color.DarkGray;
            btnPage.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPage.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPage.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPage.Dock = DockStyle.Fill;
            btnPage.FillColor = Color.White;
            btnPage.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            btnPage.ForeColor = Color.Black;
            btnPage.Location = new Point(0, 0);
            btnPage.Margin = new Padding(0);
            btnPage.Name = "btnPage";
            btnPage.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPage.Size = new Size(32, 32);
            btnPage.TabIndex = 3;
            btnPage.Text = "1";
            // 
            // ucPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnPage);
            Margin = new Padding(0);
            Name = "ucPage";
            Size = new Size(32, 32);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnPage;
    }
}
