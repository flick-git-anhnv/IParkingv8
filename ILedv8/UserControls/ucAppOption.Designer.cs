namespace ILedv8.UserControls
{
    partial class ucAppOption
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
            numCheckCountDuration = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            numSendToLedRowDuration = new Guna.UI2.WinForms.Guna2NumericUpDown();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnConfirm = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)numCheckCountDuration).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSendToLedRowDuration).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // numCheckCountDuration
            // 
            numCheckCountDuration.BackColor = Color.Transparent;
            numCheckCountDuration.BorderRadius = 8;
            numCheckCountDuration.CustomizableEdges = customizableEdges1;
            numCheckCountDuration.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numCheckCountDuration.Font = new Font("Segoe UI", 12F);
            numCheckCountDuration.Location = new Point(328, 8);
            numCheckCountDuration.Margin = new Padding(0);
            numCheckCountDuration.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numCheckCountDuration.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            numCheckCountDuration.Name = "numCheckCountDuration";
            numCheckCountDuration.ShadowDecoration.CustomizableEdges = customizableEdges2;
            numCheckCountDuration.Size = new Size(288, 36);
            numCheckCountDuration.TabIndex = 19;
            numCheckCountDuration.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numCheckCountDuration.UpDownButtonForeColor = Color.White;
            numCheckCountDuration.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(8, 16);
            label2.Margin = new Padding(2);
            label2.Name = "label2";
            label2.Size = new Size(280, 21);
            label2.TabIndex = 18;
            label2.Text = "Thời gian chờ giữa 2 lần kiểm tra (ms)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(8, 64);
            label1.Margin = new Padding(2);
            label1.Name = "label1";
            label1.Size = new Size(313, 21);
            label1.TabIndex = 18;
            label1.Text = "Thời gian chờ gửi lệnh giữa các dòng (ms)\r\n";
            // 
            // numSendToLedRowDuration
            // 
            numSendToLedRowDuration.BackColor = Color.Transparent;
            numSendToLedRowDuration.BorderRadius = 8;
            numSendToLedRowDuration.CustomizableEdges = customizableEdges3;
            numSendToLedRowDuration.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numSendToLedRowDuration.Font = new Font("Segoe UI", 12F);
            numSendToLedRowDuration.Location = new Point(328, 56);
            numSendToLedRowDuration.Margin = new Padding(0);
            numSendToLedRowDuration.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numSendToLedRowDuration.Minimum = new decimal(new int[] { 300, 0, 0, 0 });
            numSendToLedRowDuration.Name = "numSendToLedRowDuration";
            numSendToLedRowDuration.ShadowDecoration.CustomizableEdges = customizableEdges4;
            numSendToLedRowDuration.Size = new Size(288, 36);
            numSendToLedRowDuration.TabIndex = 19;
            numSendToLedRowDuration.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numSendToLedRowDuration.UpDownButtonForeColor = Color.White;
            numSendToLedRowDuration.Value = new decimal(new int[] { 300, 0, 0, 0 });
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
            tableLayoutPanel1.Location = new Point(0, 275);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(690, 48);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderRadius = 8;
            btnConfirm.CustomizableEdges = customizableEdges5;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.Dock = DockStyle.Fill;
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(270, 0);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "Lưu cấu hình";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // ucAppOption
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(tableLayoutPanel1);
            Controls.Add(numSendToLedRowDuration);
            Controls.Add(label1);
            Controls.Add(numCheckCountDuration);
            Controls.Add(label2);
            Name = "ucAppOption";
            Size = new Size(690, 323);
            ((System.ComponentModel.ISupportInitialize)numCheckCountDuration).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSendToLedRowDuration).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2NumericUpDown numCheckCountDuration;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2NumericUpDown numSendToLedRowDuration;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button btnConfirm;
    }
}
