namespace Kztek.Control8.UserControls
{
    partial class UcAccessKeyCollection
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
            btnName = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // btnName
            // 
            btnName.BackColor = Color.White;
            btnName.BorderColor = Color.FromArgb(41, 97, 27);
            btnName.BorderRadius = 16;
            btnName.BorderThickness = 2;
            btnName.CustomizableEdges = customizableEdges1;
            btnName.DisabledState.BorderColor = Color.DarkGray;
            btnName.DisabledState.CustomBorderColor = Color.DarkGray;
            btnName.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnName.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnName.Dock = DockStyle.Fill;
            btnName.FillColor = Color.White;
            btnName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnName.ForeColor = Color.FromArgb(41, 97, 27);
            btnName.Location = new Point(0, 0);
            btnName.Margin = new Padding(0);
            btnName.Name = "btnName";
            btnName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnName.Size = new Size(204, 64);
            btnName.TabIndex = 5;
            btnName.Text = "_";
            // 
            // UcAccessKeyCollection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnName);
            DoubleBuffered = true;
            Margin = new Padding(0);
            Name = "UcAccessKeyCollection";
            Size = new Size(204, 64);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnName;
    }
}
