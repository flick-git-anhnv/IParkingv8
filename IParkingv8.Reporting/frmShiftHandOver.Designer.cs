namespace IParkingv8.Reporting
{
    partial class frmShiftHandOver
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
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShiftHandOver));
            lblStartTime = new Label();
            dtpStartTime = new DateTimePicker();
            btnSearch = new Button();
            dgvData = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            cbUser = new Guna.UI2.WinForms.Guna2ComboBox();
            label4 = new Label();
            btnExcel = new Button();
            btnPrint = new Button();
            dtpEndTime = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            numRealFee = new Guna.UI2.WinForms.Guna2NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRealFee).BeginInit();
            SuspendLayout();
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.BackColor = Color.Transparent;
            lblStartTime.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStartTime.Location = new Point(8, 20);
            lblStartTime.Margin = new Padding(4, 0, 4, 0);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(65, 21);
            lblStartTime.TabIndex = 44;
            lblStartTime.Text = "Bắt đầu";
            lblStartTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpStartTime.Font = new Font("Segoe UI", 12F);
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(136, 16);
            dtpStartTime.Margin = new Padding(4, 3, 4, 3);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(200, 29);
            dtpStartTime.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.AutoSize = true;
            btnSearch.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnSearch.ForeColor = Color.Black;
            btnSearch.Location = new Point(640, 96);
            btnSearch.Margin = new Padding(4, 3, 4, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(108, 42);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm kiếm";
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(192, 255, 255);
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = Color.White;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = SystemColors.Control;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.Padding = new Padding(3);
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column7, Column5, Column8 });
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = SystemColors.Window;
            dataGridViewCellStyle16.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle16.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle16.Padding = new Padding(3);
            dataGridViewCellStyle16.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle16;
            dgvData.Location = new Point(8, 152);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(984, 448);
            dgvData.TabIndex = 77;
            // 
            // Column1
            // 
            Column1.HeaderText = "STT";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 66;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column2.HeaderText = "Loại vé";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column3.DefaultCellStyle = dataGridViewCellStyle11;
            Column3.HeaderText = "Vào";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 67;
            // 
            // Column4
            // 
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column4.DefaultCellStyle = dataGridViewCellStyle12;
            Column4.HeaderText = "Ra";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 59;
            // 
            // Column7
            // 
            dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column7.DefaultCellStyle = dataGridViewCellStyle13;
            Column7.HeaderText = "Phí Tổng";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 102;
            // 
            // Column5
            // 
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column5.DefaultCellStyle = dataGridViewCellStyle14;
            Column5.HeaderText = "Thực Thu Phần Mềm";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 183;
            // 
            // Column8
            // 
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleRight;
            Column8.DefaultCellStyle = dataGridViewCellStyle15;
            Column8.HeaderText = "Giảm Giá";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 105;
            // 
            // cbUser
            // 
            cbUser.BackColor = Color.Transparent;
            cbUser.BorderRadius = 8;
            cbUser.CustomizableEdges = customizableEdges5;
            cbUser.DrawMode = DrawMode.OwnerDrawFixed;
            cbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUser.FocusedColor = Color.FromArgb(41, 97, 27);
            cbUser.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbUser.Font = new Font("Segoe UI", 12F);
            cbUser.ForeColor = Color.Black;
            cbUser.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbUser.ItemHeight = 30;
            cbUser.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbUser.ItemsAppearance.SelectedForeColor = Color.White;
            cbUser.Location = new Point(136, 56);
            cbUser.Margin = new Padding(0);
            cbUser.Name = "cbUser";
            cbUser.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbUser.Size = new Size(496, 36);
            cbUser.TabIndex = 91;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label4.Location = new Point(8, 64);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(98, 21);
            label4.TabIndex = 87;
            label4.Text = "Người dùng";
            // 
            // btnExcel
            // 
            btnExcel.AutoSize = true;
            btnExcel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnExcel.ForeColor = Color.Black;
            btnExcel.Location = new Point(760, 96);
            btnExcel.Margin = new Padding(4, 3, 4, 3);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(108, 42);
            btnExcel.TabIndex = 0;
            btnExcel.Text = "Xuất Excel";
            btnExcel.Click += btnExcel_Click;
            // 
            // btnPrint
            // 
            btnPrint.AutoSize = true;
            btnPrint.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnPrint.ForeColor = Color.Black;
            btnPrint.Location = new Point(880, 96);
            btnPrint.Margin = new Padding(4, 3, 4, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(104, 42);
            btnPrint.TabIndex = 0;
            btnPrint.Text = "In";
            btnPrint.Click += btnPrint_Click;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpEndTime.Font = new Font("Segoe UI", 12F);
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(424, 16);
            dtpEndTime.Margin = new Padding(4, 3, 4, 3);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(208, 29);
            dtpEndTime.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(344, 20);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(71, 21);
            label1.TabIndex = 44;
            label1.Text = "Kết thúc";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(8, 112);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(118, 21);
            label2.TabIndex = 87;
            label2.Text = "Thực tu bảo vệ";
            // 
            // numRealFee
            // 
            numRealFee.BackColor = Color.Transparent;
            numRealFee.BorderRadius = 8;
            numRealFee.CustomizableEdges = customizableEdges7;
            numRealFee.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numRealFee.Font = new Font("Segoe UI", 12F);
            numRealFee.Location = new Point(136, 104);
            numRealFee.Margin = new Padding(0);
            numRealFee.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            numRealFee.Name = "numRealFee";
            numRealFee.ShadowDecoration.CustomizableEdges = customizableEdges8;
            numRealFee.Size = new Size(496, 36);
            numRealFee.TabIndex = 92;
            numRealFee.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numRealFee.UpDownButtonForeColor = Color.White;
            // 
            // frmShiftHandOver
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1002, 608);
            Controls.Add(numRealFee);
            Controls.Add(cbUser);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(dgvData);
            Controls.Add(label1);
            Controls.Add(lblStartTime);
            Controls.Add(btnPrint);
            Controls.Add(btnExcel);
            Controls.Add(btnSearch);
            Controls.Add(dtpEndTime);
            Controls.Add(dtpStartTime);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "frmShiftHandOver";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo chốt ca";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRealFee).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStartTime;
        private DateTimePicker dtpStartTime;
        private Button btnSearch;
        private DataGridView dgvData;
        private Guna.UI2.WinForms.Guna2ComboBox cbUser;
        private Label label4;
        private Button btnExcel;
        private Button btnPrint;
        private DateTimePicker dtpEndTime;
        private Label label1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column8;
        private Label label2;
        private Guna.UI2.WinForms.Guna2NumericUpDown numRealFee;
    }
}