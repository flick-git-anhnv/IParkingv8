namespace ILedv8.UserControls
{
    partial class ucLedLaneConfig
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
            label3 = new Label();
            cbLeds = new Guna.UI2.WinForms.Guna2ComboBox();
            panelConfig = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnConfirm = new Guna.UI2.WinForms.Guna2Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(8, 16);
            label3.Margin = new Padding(2);
            label3.Name = "label3";
            label3.Size = new Size(78, 21);
            label3.TabIndex = 11;
            label3.Text = "Bảng LED";
            // 
            // cbLeds
            // 
            cbLeds.BackColor = Color.Transparent;
            cbLeds.BorderRadius = 8;
            cbLeds.CustomizableEdges = customizableEdges1;
            cbLeds.DrawMode = DrawMode.OwnerDrawFixed;
            cbLeds.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLeds.FocusedColor = Color.FromArgb(41, 97, 27);
            cbLeds.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbLeds.Font = new Font("Segoe UI", 12F);
            cbLeds.ForeColor = Color.Black;
            cbLeds.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbLeds.ItemHeight = 30;
            cbLeds.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbLeds.ItemsAppearance.SelectedForeColor = Color.White;
            cbLeds.Location = new Point(96, 8);
            cbLeds.Margin = new Padding(0);
            cbLeds.Name = "cbLeds";
            cbLeds.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbLeds.Size = new Size(296, 36);
            cbLeds.TabIndex = 10;
            // 
            // panelConfig
            // 
            panelConfig.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelConfig.AutoScroll = true;
            panelConfig.Location = new Point(8, 56);
            panelConfig.Name = "panelConfig";
            panelConfig.Size = new Size(799, 367);
            panelConfig.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(btnConfirm, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 431);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(815, 48);
            tableLayoutPanel1.TabIndex = 13;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderRadius = 8;
            btnConfirm.CustomizableEdges = customizableEdges3;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.Dock = DockStyle.Fill;
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(332, 0);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "Lưu cấu hình";
            // 
            // ucLedLaneConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panelConfig);
            Controls.Add(label3);
            Controls.Add(cbLeds);
            Name = "ucLedLaneConfig";
            Size = new Size(815, 479);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox cbLeds;
        private Panel panelConfig;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button btnConfirm;
    }
}
