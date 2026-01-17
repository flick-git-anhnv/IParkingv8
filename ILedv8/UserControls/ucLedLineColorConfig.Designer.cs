namespace ILedv8.UserControls
{
    partial class ucLedLineColorConfig
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblLineName = new Label();
            cbColor = new Guna.UI2.WinForms.Guna2ComboBox();
            label4 = new Label();
            cbFontSize = new Guna.UI2.WinForms.Guna2ComboBox();
            label3 = new Label();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            cbZeroColor = new Guna.UI2.WinForms.Guna2ComboBox();
            label1 = new Label();
            numMaxCharacter = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label2 = new Label();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxCharacter).BeginInit();
            SuspendLayout();
            // 
            // lblLineName
            // 
            lblLineName.AutoSize = true;
            lblLineName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLineName.Location = new Point(8, 8);
            lblLineName.Margin = new Padding(2);
            lblLineName.Name = "lblLineName";
            lblLineName.Size = new Size(60, 21);
            lblLineName.TabIndex = 13;
            lblLineName.Text = "Dòng 1";
            // 
            // cbColor
            // 
            cbColor.BackColor = Color.Transparent;
            cbColor.BorderRadius = 8;
            cbColor.CustomizableEdges = customizableEdges11;
            cbColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbColor.FocusedColor = Color.FromArgb(41, 97, 27);
            cbColor.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbColor.Font = new Font("Segoe UI", 12F);
            cbColor.ForeColor = Color.Black;
            cbColor.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbColor.ItemHeight = 30;
            cbColor.Items.AddRange(new object[] { "Đỏ", "Xanh Lá", "Vàng", "Xanh Da Trời", "Tím", "Lục Lam", "Trắng" });
            cbColor.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbColor.ItemsAppearance.SelectedForeColor = Color.White;
            cbColor.Location = new Point(216, 72);
            cbColor.Margin = new Padding(0);
            cbColor.Name = "cbColor";
            cbColor.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cbColor.Size = new Size(192, 36);
            cbColor.TabIndex = 12;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.Location = new Point(216, 48);
            label4.Margin = new Padding(2);
            label4.Name = "label4";
            label4.Size = new Size(42, 21);
            label4.TabIndex = 13;
            label4.Text = "Màu";
            // 
            // cbFontSize
            // 
            cbFontSize.BackColor = Color.Transparent;
            cbFontSize.BorderRadius = 8;
            cbFontSize.CustomizableEdges = customizableEdges13;
            cbFontSize.DrawMode = DrawMode.OwnerDrawFixed;
            cbFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFontSize.FocusedColor = Color.FromArgb(41, 97, 27);
            cbFontSize.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbFontSize.Font = new Font("Segoe UI", 12F);
            cbFontSize.ForeColor = Color.Black;
            cbFontSize.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbFontSize.ItemHeight = 30;
            cbFontSize.Items.AddRange(new object[] { "Cỡ Chữ: 7", "Cỡ Chữ: 8", "Cỡ Chữ: 10", "Cỡ Chữ: 12", "Cỡ Chữ: 13", "Cỡ Chữ: 14", "Cỡ Chữ: 16", "Cỡ Chữ: 23", "Cỡ Chữ: 24", "Cỡ Chữ: 25", "Cỡ Chữ: 26" });
            cbFontSize.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbFontSize.ItemsAppearance.SelectedForeColor = Color.White;
            cbFontSize.Location = new Point(424, 72);
            cbFontSize.Margin = new Padding(0);
            cbFontSize.Name = "cbFontSize";
            cbFontSize.ShadowDecoration.CustomizableEdges = customizableEdges14;
            cbFontSize.Size = new Size(192, 36);
            cbFontSize.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(424, 48);
            label3.Margin = new Padding(2);
            label3.Name = "label3";
            label3.Size = new Size(61, 21);
            label3.TabIndex = 13;
            label3.Text = "Cỡ chữ";
            // 
            // guna2Panel1
            // 
            guna2Panel1.BorderColor = Color.Gray;
            guna2Panel1.BorderRadius = 8;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(numMaxCharacter);
            guna2Panel1.Controls.Add(label2);
            guna2Panel1.Controls.Add(cbZeroColor);
            guna2Panel1.Controls.Add(cbColor);
            guna2Panel1.Controls.Add(cbFontSize);
            guna2Panel1.Controls.Add(label3);
            guna2Panel1.Controls.Add(label1);
            guna2Panel1.Controls.Add(lblLineName);
            guna2Panel1.Controls.Add(label4);
            guna2Panel1.CustomizableEdges = customizableEdges19;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.Location = new Point(0, 0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges20;
            guna2Panel1.Size = new Size(788, 128);
            guna2Panel1.TabIndex = 15;
            // 
            // cbZeroColor
            // 
            cbZeroColor.BackColor = Color.Transparent;
            cbZeroColor.BorderRadius = 8;
            cbZeroColor.CustomizableEdges = customizableEdges17;
            cbZeroColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbZeroColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbZeroColor.FocusedColor = Color.FromArgb(41, 97, 27);
            cbZeroColor.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbZeroColor.Font = new Font("Segoe UI", 12F);
            cbZeroColor.ForeColor = Color.Black;
            cbZeroColor.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbZeroColor.ItemHeight = 30;
            cbZeroColor.Items.AddRange(new object[] { "Đỏ", "Xanh Lá", "Vàng", "Xanh Da Trời", "Tím", "Lục Lam", "Trắng" });
            cbZeroColor.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbZeroColor.ItemsAppearance.SelectedForeColor = Color.White;
            cbZeroColor.Location = new Point(8, 72);
            cbZeroColor.Margin = new Padding(0);
            cbZeroColor.Name = "cbZeroColor";
            cbZeroColor.ShadowDecoration.CustomizableEdges = customizableEdges18;
            cbZeroColor.Size = new Size(192, 36);
            cbZeroColor.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(8, 48);
            label1.Margin = new Padding(2);
            label1.Name = "label1";
            label1.Size = new Size(101, 21);
            label1.TabIndex = 13;
            label1.Text = "Màu hết chỗ";
            // 
            // numMaxCharacter
            // 
            numMaxCharacter.BackColor = Color.Transparent;
            numMaxCharacter.BorderRadius = 8;
            numMaxCharacter.CustomizableEdges = customizableEdges15;
            numMaxCharacter.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numMaxCharacter.Font = new Font("Segoe UI", 12F);
            numMaxCharacter.Location = new Point(632, 72);
            numMaxCharacter.Margin = new Padding(0);
            numMaxCharacter.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMaxCharacter.Name = "numMaxCharacter";
            numMaxCharacter.ShadowDecoration.CustomizableEdges = customizableEdges16;
            numMaxCharacter.Size = new Size(144, 36);
            numMaxCharacter.TabIndex = 17;
            numMaxCharacter.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numMaxCharacter.UpDownButtonForeColor = Color.White;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(632, 48);
            label2.Margin = new Padding(2);
            label2.Name = "label2";
            label2.Size = new Size(139, 21);
            label2.TabIndex = 16;
            label2.Text = "Số chữ số hiển thị";
            // 
            // ucLedLineColorConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Panel1);
            Name = "ucLedLineColorConfig";
            Padding = new Padding(0, 0, 0, 16);
            Size = new Size(788, 144);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMaxCharacter).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblLineName;
        private Guna.UI2.WinForms.Guna2ComboBox cbColor;
        private Label label4;
        private Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cbFontSize;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2ComboBox cbZeroColor;
        private Label label1;
        private Guna.UI2.WinForms.Guna2NumericUpDown numMaxCharacter;
        private Label label2;
    }
}
