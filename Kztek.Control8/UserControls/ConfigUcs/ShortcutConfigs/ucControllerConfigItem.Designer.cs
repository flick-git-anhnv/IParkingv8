namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    partial class ucControllerConfigItem
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
            lblControllerName = new Label();
            SuspendLayout();
            // 
            // lblControllerName
            // 
            lblControllerName.AutoSize = true;
            lblControllerName.Dock = DockStyle.Top;
            lblControllerName.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline);
            lblControllerName.Location = new Point(0, 0);
            lblControllerName.Margin = new Padding(4, 0, 4, 0);
            lblControllerName.Name = "lblControllerName";
            lblControllerName.Size = new Size(17, 21);
            lblControllerName.TabIndex = 1;
            lblControllerName.Text = "_";
            // 
            // ucControllerConfigItem
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblControllerName);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ucControllerConfigItem";
            Size = new Size(167, 155);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblControllerName;
    }
}
