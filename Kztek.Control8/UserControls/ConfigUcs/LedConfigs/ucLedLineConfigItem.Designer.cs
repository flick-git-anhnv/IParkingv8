namespace Kztek.Control8.UserControls.ConfigUcs
{
    partial class ucLedLineConfigItem
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblLineName = new Label();
            cbDisplayMode = new Guna.UI2.WinForms.Guna2ComboBox();
            cbColor = new Guna.UI2.WinForms.Guna2ComboBox();
            cbFontSize = new Guna.UI2.WinForms.Guna2ComboBox();
            txtDisplaytext = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // lblLineName
            // 
            lblLineName.AutoSize = true;
            lblLineName.Location = new Point(0, 16);
            lblLineName.Margin = new Padding(0);
            lblLineName.Name = "lblLineName";
            lblLineName.Size = new Size(59, 21);
            lblLineName.TabIndex = 1;
            lblLineName.Text = "Dòng _";
            // 
            // cbDisplayMode
            // 
            cbDisplayMode.BackColor = Color.Transparent;
            cbDisplayMode.BorderRadius = 8;
            cbDisplayMode.CustomizableEdges = customizableEdges1;
            cbDisplayMode.DrawMode = DrawMode.OwnerDrawFixed;
            cbDisplayMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDisplayMode.FocusedColor = Color.FromArgb(41, 97, 27);
            cbDisplayMode.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbDisplayMode.Font = new Font("Segoe UI", 12F);
            cbDisplayMode.ForeColor = Color.Black;
            cbDisplayMode.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbDisplayMode.IntegralHeight = false;
            cbDisplayMode.ItemHeight = 30;
            cbDisplayMode.Items.AddRange(new object[] { "Để Trống", "Mã Thẻ", "Số Thẻ", "Loại Thẻ", "Loại Sự Kiện", "Biển Số Xe", "Thời Gian Vào", "Thời Gian Ra", "Phí Gửi Xe", "Tùy Chọn" });
            cbDisplayMode.Location = new Point(80, 8);
            cbDisplayMode.Margin = new Padding(0);
            cbDisplayMode.Name = "cbDisplayMode";
            cbDisplayMode.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbDisplayMode.Size = new Size(128, 36);
            cbDisplayMode.TabIndex = 9;
            // 
            // cbColor
            // 
            cbColor.BackColor = Color.Transparent;
            cbColor.BorderRadius = 8;
            cbColor.CustomizableEdges = customizableEdges3;
            cbColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbColor.FocusedColor = Color.FromArgb(41, 97, 27);
            cbColor.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbColor.Font = new Font("Segoe UI", 12F);
            cbColor.ForeColor = Color.Black;
            cbColor.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbColor.IntegralHeight = false;
            cbColor.ItemHeight = 30;
            cbColor.Items.AddRange(new object[] { "Đỏ", "Xanh Lá", "Vàng", "Xanh Da Trời", "Tím", "Lục Lam", "Trắng" });
            cbColor.Location = new Point(216, 8);
            cbColor.Margin = new Padding(0);
            cbColor.Name = "cbColor";
            cbColor.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbColor.Size = new Size(80, 36);
            cbColor.TabIndex = 9;
            // 
            // cbFontSize
            // 
            cbFontSize.BackColor = Color.Transparent;
            cbFontSize.BorderRadius = 8;
            cbFontSize.CustomizableEdges = customizableEdges5;
            cbFontSize.DrawMode = DrawMode.OwnerDrawFixed;
            cbFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFontSize.FocusedColor = Color.FromArgb(41, 97, 27);
            cbFontSize.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbFontSize.Font = new Font("Segoe UI", 12F);
            cbFontSize.ForeColor = Color.Black;
            cbFontSize.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbFontSize.IntegralHeight = false;
            cbFontSize.ItemHeight = 30;
            cbFontSize.Items.AddRange(new object[] { "7", "8", "10", "12", "13", "14", "16", "23", "24", "25", "26" });
            cbFontSize.Location = new Point(304, 8);
            cbFontSize.Margin = new Padding(0);
            cbFontSize.Name = "cbFontSize";
            cbFontSize.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbFontSize.Size = new Size(72, 36);
            cbFontSize.TabIndex = 9;
            // 
            // txtDisplaytext
            // 
            txtDisplaytext.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDisplaytext.BorderRadius = 8;
            txtDisplaytext.CustomizableEdges = customizableEdges7;
            txtDisplaytext.DefaultText = "";
            txtDisplaytext.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtDisplaytext.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtDisplaytext.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtDisplaytext.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtDisplaytext.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtDisplaytext.Font = new Font("Segoe UI", 12F);
            txtDisplaytext.ForeColor = Color.Black;
            txtDisplaytext.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtDisplaytext.Location = new Point(384, 8);
            txtDisplaytext.Margin = new Padding(0);
            txtDisplaytext.Name = "txtDisplaytext";
            txtDisplaytext.PlaceholderText = "";
            txtDisplaytext.SelectedText = "";
            txtDisplaytext.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtDisplaytext.Size = new Size(440, 36);
            txtDisplaytext.TabIndex = 10;
            // 
            // ucLedLineConfigItem
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(txtDisplaytext);
            Controls.Add(cbFontSize);
            Controls.Add(cbColor);
            Controls.Add(cbDisplayMode);
            Controls.Add(lblLineName);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ucLedLineConfigItem";
            Size = new Size(831, 53);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblLineName;
        private Guna.UI2.WinForms.Guna2ComboBox cbDisplayMode;
        private Guna.UI2.WinForms.Guna2ComboBox cbColor;
        private Guna.UI2.WinForms.Guna2ComboBox cbFontSize;
        private Guna.UI2.WinForms.Guna2TextBox txtDisplaytext;
    }
}
