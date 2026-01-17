using iParking.ConfigurationManager.Forms;

namespace ILedv8
{
    partial class FrmSetting
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabOption = new TabControl();
            SuspendLayout();
            // 
            // tabOption
            // 
            tabOption.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabOption.Font = new Font("Segoe UI", 9F);
            tabOption.Location = new Point(8, 8);
            tabOption.Name = "tabOption";
            tabOption.SelectedIndex = 0;
            tabOption.Size = new Size(872, 664);
            tabOption.TabIndex = 2;
            // 
            // FrmSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(886, 681);
            Controls.Add(tabOption);
            DoubleBuffered = true;
            Name = "FrmSetting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cấu hình hệ thống";
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabOption;
    }
}