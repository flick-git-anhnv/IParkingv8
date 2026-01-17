namespace Kztek.Control8.UserControls.ucDataGridViewInfo
{
    partial class ucExitInfor
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblTitle = new Guna.UI2.WinForms.Guna2Button();
            pnlMoney = new Guna.UI2.WinForms.Guna2Panel();
            lblMoney = new Label();
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            dgvData = new Guna.UI2.WinForms.Guna2DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            toolTip1 = new ToolTip(components);
            lblDuration = new Label();
            panel1 = new Panel();
            pnlMoney.SuspendLayout();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.White;
            lblTitle.BorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.BorderRadius = 8;
            lblTitle.BorderThickness = 1;
            customizableEdges1.BottomLeft = false;
            customizableEdges1.BottomRight = false;
            lblTitle.CustomizableEdges = customizableEdges1;
            lblTitle.DisabledState.BorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.CustomBorderColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.FillColor = Color.FromArgb(6, 59, 167);
            lblTitle.DisabledState.ForeColor = Color.White;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Enabled = false;
            lblTitle.FillColor = Color.FromArgb(6, 59, 167);
            lblTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.ShadowDecoration.CustomizableEdges = customizableEdges2;
            lblTitle.Size = new Size(520, 28);
            lblTitle.TabIndex = 16;
            lblTitle.Text = "THÔNG TIN SỰ KIỆN";
            // 
            // pnlMoney
            // 
            pnlMoney.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pnlMoney.AutoSize = true;
            pnlMoney.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlMoney.BackColor = Color.FromArgb(226, 238, 255);
            pnlMoney.BorderRadius = 10;
            pnlMoney.Controls.Add(lblMoney);
            pnlMoney.CustomizableEdges = customizableEdges3;
            pnlMoney.FillColor = Color.FromArgb(255, 234, 219);
            pnlMoney.Location = new Point(408, 208);
            pnlMoney.Margin = new Padding(0);
            pnlMoney.Name = "pnlMoney";
            pnlMoney.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlMoney.Size = new Size(105, 37);
            pnlMoney.TabIndex = 19;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.BackColor = Color.Transparent;
            lblMoney.Dock = DockStyle.Fill;
            lblMoney.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMoney.ForeColor = Color.Red;
            lblMoney.Location = new Point(0, 0);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(105, 37);
            lblMoney.TabIndex = 20;
            lblMoney.Text = "money";
            lblMoney.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.White;
            guna2Panel1.BorderRadius = 16;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(pnlMoney);
            guna2Panel1.Controls.Add(dgvData);
            customizableEdges5.TopLeft = false;
            customizableEdges5.TopRight = false;
            guna2Panel1.CustomizableEdges = customizableEdges5;
            guna2Panel1.Dock = DockStyle.Fill;
            guna2Panel1.FillColor = Color.FromArgb(226, 238, 255);
            guna2Panel1.Location = new Point(0, 28);
            guna2Panel1.Margin = new Padding(0);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.Padding = new Padding(6, 0, 6, 6);
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel1.Size = new Size(520, 252);
            guna2Panel1.TabIndex = 21;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(226, 238, 255);
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = Color.FromArgb(226, 238, 255);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData.ColumnHeadersHeight = 4;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvData.ColumnHeadersVisible = false;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(226, 238, 255);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.Padding = new Padding(3);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(226, 238, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            dgvData.Dock = DockStyle.Fill;
            dgvData.GridColor = Color.FromArgb(231, 229, 255);
            dgvData.Location = new Point(6, 0);
            dgvData.Margin = new Padding(0);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.Size = new Size(508, 246);
            dgvData.TabIndex = 19;
            dgvData.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvData.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvData.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvData.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvData.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvData.ThemeStyle.BackColor = Color.FromArgb(226, 238, 255);
            dgvData.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvData.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvData.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvData.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvData.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvData.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvData.ThemeStyle.HeaderStyle.Height = 4;
            dgvData.ThemeStyle.ReadOnly = true;
            dgvData.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvData.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvData.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvData.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvData.ThemeStyle.RowsStyle.Height = 25;
            dgvData.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvData.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Column1.DefaultCellStyle = dataGridViewCellStyle3;
            Column1.HeaderText = "Title";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 5;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Value";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // lblDuration
            // 
            lblDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDuration.BackColor = Color.FromArgb(6, 59, 167);
            lblDuration.Font = new Font("Segoe UI", 10F);
            lblDuration.ForeColor = Color.White;
            lblDuration.Location = new Point(456, 4);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(60, 21);
            lblDuration.TabIndex = 20;
            lblDuration.TextAlign = ContentAlignment.MiddleRight;
            lblDuration.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblDuration);
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 28);
            panel1.TabIndex = 22;
            // 
            // ucExitInfor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(guna2Panel1);
            Controls.Add(panel1);
            Margin = new Padding(0);
            Name = "ucExitInfor";
            Size = new Size(520, 280);
            pnlMoney.ResumeLayout(false);
            pnlMoney.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2TextBox txbMoney;
        private Guna.UI2.WinForms.Guna2Button lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlMoney;
        private Label lblMoney;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvData;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private ToolTip toolTip1;
        private Label lblDuration;
        private Panel panel1;
    }
}
