namespace Kztek.Control8.UserControls.ConfigUcs.ControllerConfigs
{
    partial class UcControllerReaderCardFormat
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
            cbOutputFormat = new ComboBox();
            lblOutputFormatTitle = new Label();
            cbConfigOption = new ComboBox();
            lblAdditionalTitle = new Label();
            lblReaderName = new Label();
            cbIdentityGroup = new ComboBox();
            lblAccessKeyCollectionTitle = new Label();
            cbDailyCardRegister = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            txtCloseBarrieIndex = new TextBox();
            SuspendLayout();
            // 
            // cbOutputFormat
            // 
            cbOutputFormat.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbOutputFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOutputFormat.FormattingEnabled = true;
            cbOutputFormat.Location = new Point(144, 40);
            cbOutputFormat.Name = "cbOutputFormat";
            cbOutputFormat.Size = new Size(335, 29);
            cbOutputFormat.TabIndex = 0;
            // 
            // lblOutputFormatTitle
            // 
            lblOutputFormatTitle.AutoSize = true;
            lblOutputFormatTitle.Location = new Point(11, 44);
            lblOutputFormatTitle.Name = "lblOutputFormatTitle";
            lblOutputFormatTitle.Size = new Size(100, 21);
            lblOutputFormatTitle.TabIndex = 1;
            lblOutputFormatTitle.Text = "Định dạng ra";
            // 
            // cbConfigOption
            // 
            cbConfigOption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbConfigOption.DropDownStyle = ComboBoxStyle.DropDownList;
            cbConfigOption.FormattingEnabled = true;
            cbConfigOption.Location = new Point(144, 75);
            cbConfigOption.Name = "cbConfigOption";
            cbConfigOption.Size = new Size(335, 29);
            cbConfigOption.TabIndex = 0;
            // 
            // lblAdditionalTitle
            // 
            lblAdditionalTitle.AutoSize = true;
            lblAdditionalTitle.Location = new Point(11, 79);
            lblAdditionalTitle.Name = "lblAdditionalTitle";
            lblAdditionalTitle.Size = new Size(66, 21);
            lblAdditionalTitle.TabIndex = 1;
            lblAdditionalTitle.Text = "Bổ sung";
            // 
            // lblReaderName
            // 
            lblReaderName.AutoSize = true;
            lblReaderName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblReaderName.Location = new Point(11, 10);
            lblReaderName.Name = "lblReaderName";
            lblReaderName.Size = new Size(57, 21);
            lblReaderName.TabIndex = 2;
            lblReaderName.Text = "label4";
            // 
            // cbIdentityGroup
            // 
            cbIdentityGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbIdentityGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIdentityGroup.FormattingEnabled = true;
            cbIdentityGroup.Location = new Point(144, 114);
            cbIdentityGroup.Name = "cbIdentityGroup";
            cbIdentityGroup.Size = new Size(337, 29);
            cbIdentityGroup.TabIndex = 0;
            // 
            // lblAccessKeyCollectionTitle
            // 
            lblAccessKeyCollectionTitle.AutoSize = true;
            lblAccessKeyCollectionTitle.Location = new Point(13, 118);
            lblAccessKeyCollectionTitle.Name = "lblAccessKeyCollectionTitle";
            lblAccessKeyCollectionTitle.Size = new Size(80, 21);
            lblAccessKeyCollectionTitle.TabIndex = 1;
            lblAccessKeyCollectionTitle.Text = "Nhóm thẻ";
            // 
            // cbDailyCardRegister
            // 
            cbDailyCardRegister.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbDailyCardRegister.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDailyCardRegister.FormattingEnabled = true;
            cbDailyCardRegister.Location = new Point(144, 152);
            cbDailyCardRegister.Name = "cbDailyCardRegister";
            cbDailyCardRegister.Size = new Size(337, 29);
            cbDailyCardRegister.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 156);
            label1.Name = "label1";
            label1.Size = new Size(125, 21);
            label1.TabIndex = 1;
            label1.Text = "Đăng ký thẻ lượt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 192);
            label2.Name = "label2";
            label2.Size = new Size(93, 21);
            label2.TabIndex = 1;
            label2.Text = "Đóng barrie";
            // 
            // txtCloseBarrieIndex
            // 
            txtCloseBarrieIndex.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCloseBarrieIndex.Location = new Point(144, 192);
            txtCloseBarrieIndex.Name = "txtCloseBarrieIndex";
            txtCloseBarrieIndex.PlaceholderText = "Điền chân đóng barrie, để trống để không sử dụng";
            txtCloseBarrieIndex.Size = new Size(336, 29);
            txtCloseBarrieIndex.TabIndex = 3;
            // 
            // UcControllerReaderCardFormat
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(txtCloseBarrieIndex);
            Controls.Add(lblReaderName);
            Controls.Add(lblOutputFormatTitle);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblAccessKeyCollectionTitle);
            Controls.Add(lblAdditionalTitle);
            Controls.Add(cbOutputFormat);
            Controls.Add(cbDailyCardRegister);
            Controls.Add(cbIdentityGroup);
            Controls.Add(cbConfigOption);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "UcControllerReaderCardFormat";
            Size = new Size(500, 236);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbOutputFormat;
        private Label lblOutputFormatTitle;
        private ComboBox cbConfigOption;
        private Label lblAdditionalTitle;
        private Label lblReaderName;
        private ComboBox cbIdentityGroup;
        private Label lblAccessKeyCollectionTitle;
        private ComboBox cbDailyCardRegister;
        private Label label1;
        private Label label2;
        private TextBox txtCloseBarrieIndex;
    }
}
