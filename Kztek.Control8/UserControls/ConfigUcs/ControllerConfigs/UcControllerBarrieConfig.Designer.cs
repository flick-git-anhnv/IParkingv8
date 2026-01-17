namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    partial class UcControllerBarrieConfig
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
            lblBarrieName = new Label();
            cbBarrieOpenMode = new ComboBox();
            SuspendLayout();
            // 
            // lblReaderName
            // 
            lblBarrieName.AutoSize = true;
            lblBarrieName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblBarrieName.Location = new Point(8, 12);
            lblBarrieName.Name = "lblReaderName";
            lblBarrieName.Size = new Size(68, 21);
            lblBarrieName.TabIndex = 5;
            lblBarrieName.Text = "Barrie 1";
            // 
            // cbOpenBarrieMode
            // 
            cbBarrieOpenMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbBarrieOpenMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBarrieOpenMode.FormattingEnabled = true;
            cbBarrieOpenMode.Items.AddRange(new object[] { "Tất cả", "Ô tô", "Không phải ô tô" });
            cbBarrieOpenMode.Location = new Point(96, 8);
            cbBarrieOpenMode.Name = "cbOpenBarrieMode";
            cbBarrieOpenMode.Size = new Size(624, 29);
            cbBarrieOpenMode.TabIndex = 3;
            // 
            // UcControllerBarrieConfig
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblBarrieName);
            Controls.Add(cbBarrieOpenMode);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4, 4, 4, 4);
            Name = "UcControllerBarrieConfig";
            Size = new Size(729, 344);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBarrieName;
        private ComboBox cbBarrieOpenMode;
    }
}
