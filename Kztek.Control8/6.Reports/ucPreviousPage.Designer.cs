namespace Kztek.Control8.UserControls.ReportUcs
{
    partial class ucPreviousPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPreviousPage));
            guna2Button1 = new Button();
            SuspendLayout();
            // 
            // guna2Button1
            // 
            guna2Button1.BackColor = Color.White;
            guna2Button1.Dock = DockStyle.Fill;
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Image = (Image)resources.GetObject("guna2Button1.Image");
            guna2Button1.Location = new Point(0, 0);
            guna2Button1.Margin = new Padding(0);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.Size = new Size(32, 32);
            guna2Button1.TabIndex = 1;
            // 
            // ucPreviousPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Button1);
            Name = "ucPreviousPage";
            Size = new Size(32, 32);
            ResumeLayout(false);
        }

        #endregion

        private Button guna2Button1;
    }
}
