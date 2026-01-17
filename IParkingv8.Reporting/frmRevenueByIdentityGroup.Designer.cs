using Kztek.Control8.Controls;

namespace iParkingv8.Reporting
{
    partial class FrmRevenueByIdentityGroup
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRevenueByIdentityGroup));
            lblStartTime = new KzLabel();
            dtpStartTime = new DateTimePicker();
            btnSearch = new KzButton();
            dtpEndTime = new DateTimePicker();
            lblEndTime = new KzLabel();
            lblGroupFilter = new KzLabel();
            dgvData = new DataGridView();
            cbUser = new Guna.UI2.WinForms.Guna2ComboBox();
            lblUserTitle = new KzLabel();
            lblCollection = new KzLabel();
            cbIdentityGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            cbMode = new Guna.UI2.WinForms.Guna2ComboBox();
            btnExcel = new KzButton();
            btnPrint = new KzButton();
            colIndex = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colFee = new DataGridViewTextBoxColumn();
            colDiscount = new DataGridViewTextBoxColumn();
            colPaid = new DataGridViewTextBoxColumn();
            colRealFee = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.BackColor = Color.Transparent;
            lblStartTime.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStartTime.Location = new Point(8, 80);
            lblStartTime.Margin = new Padding(4, 0, 4, 0);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(33, 21);
            lblStartTime.TabIndex = 44;
            lblStartTime.Text = "Từ ";
            lblStartTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpStartTime.Font = new Font("Segoe UI", 12F);
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(8, 112);
            dtpStartTime.Margin = new Padding(4, 3, 4, 3);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(216, 29);
            dtpStartTime.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.BorderColor = Color.FromArgb(41, 97, 27);
            btnSearch.BorderRadius = 8;
            btnSearch.BorderThickness = 1;
            btnSearch.CustomizableEdges = customizableEdges1;
            btnSearch.FillColor = Color.FromArgb(41, 97, 27);
            btnSearch.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(8, 152);
            btnSearch.Margin = new Padding(4, 3, 4, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSearch.Size = new Size(142, 40);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm kiếm";
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpEndTime.Font = new Font("Segoe UI", 12F);
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(240, 112);
            dtpEndTime.Margin = new Padding(4, 3, 4, 3);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(216, 29);
            dtpEndTime.TabIndex = 6;
            // 
            // lblEndTime
            // 
            lblEndTime.AutoSize = true;
            lblEndTime.BackColor = Color.Transparent;
            lblEndTime.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblEndTime.Location = new Point(240, 80);
            lblEndTime.Margin = new Padding(4, 0, 4, 0);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(39, 21);
            lblEndTime.TabIndex = 44;
            lblEndTime.Text = "Đến";
            lblEndTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGroupFilter
            // 
            lblGroupFilter.AutoSize = true;
            lblGroupFilter.BackColor = Color.Transparent;
            lblGroupFilter.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblGroupFilter.Location = new Point(472, 80);
            lblGroupFilter.Margin = new Padding(4, 0, 4, 0);
            lblGroupFilter.Name = "lblGroupFilter";
            lblGroupFilter.Size = new Size(40, 21);
            lblGroupFilter.TabIndex = 44;
            lblGroupFilter.Text = "Loại";
            lblGroupFilter.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 255);
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { colIndex, colName, colQuantity, colFee, colDiscount, colPaid, colRealFee });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new Padding(3);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle6;
            dgvData.Location = new Point(8, 200);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(1000, 400);
            dgvData.TabIndex = 77;
            // 
            // cbUser
            // 
            cbUser.BackColor = Color.Transparent;
            cbUser.BorderRadius = 8;
            cbUser.CustomizableEdges = customizableEdges3;
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
            cbUser.Location = new Point(8, 32);
            cbUser.Margin = new Padding(0);
            cbUser.Name = "cbUser";
            cbUser.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbUser.Size = new Size(216, 36);
            cbUser.TabIndex = 3;
            // 
            // lblUserTitle
            // 
            lblUserTitle.AutoSize = true;
            lblUserTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUserTitle.Location = new Point(8, 8);
            lblUserTitle.Margin = new Padding(0);
            lblUserTitle.Name = "lblUserTitle";
            lblUserTitle.Size = new Size(98, 21);
            lblUserTitle.TabIndex = 87;
            lblUserTitle.Text = "Người dùng";
            // 
            // lblCollection
            // 
            lblCollection.AutoSize = true;
            lblCollection.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCollection.Location = new Point(240, 8);
            lblCollection.Margin = new Padding(0);
            lblCollection.Name = "lblCollection";
            lblCollection.Size = new Size(55, 21);
            lblCollection.TabIndex = 88;
            lblCollection.Text = "Nhóm";
            // 
            // cbIdentityGroup
            // 
            cbIdentityGroup.BackColor = Color.Transparent;
            cbIdentityGroup.BorderRadius = 8;
            cbIdentityGroup.CustomizableEdges = customizableEdges5;
            cbIdentityGroup.DrawMode = DrawMode.OwnerDrawFixed;
            cbIdentityGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIdentityGroup.FocusedColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroup.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroup.Font = new Font("Segoe UI", 12F);
            cbIdentityGroup.ForeColor = Color.Black;
            cbIdentityGroup.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroup.ItemHeight = 30;
            cbIdentityGroup.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroup.ItemsAppearance.SelectedForeColor = Color.White;
            cbIdentityGroup.Location = new Point(240, 32);
            cbIdentityGroup.Margin = new Padding(0);
            cbIdentityGroup.Name = "cbIdentityGroup";
            cbIdentityGroup.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbIdentityGroup.Size = new Size(528, 36);
            cbIdentityGroup.TabIndex = 4;
            // 
            // cbMode
            // 
            cbMode.BackColor = Color.Transparent;
            cbMode.BorderRadius = 8;
            cbMode.CustomizableEdges = customizableEdges7;
            cbMode.DrawMode = DrawMode.OwnerDrawFixed;
            cbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbMode.FocusedColor = Color.FromArgb(41, 97, 27);
            cbMode.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbMode.Font = new Font("Segoe UI", 12F);
            cbMode.ForeColor = Color.Black;
            cbMode.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbMode.ItemHeight = 30;
            cbMode.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbMode.ItemsAppearance.SelectedForeColor = Color.White;
            cbMode.Location = new Point(472, 105);
            cbMode.Margin = new Padding(0);
            cbMode.Name = "cbMode";
            cbMode.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbMode.Size = new Size(296, 36);
            cbMode.TabIndex = 7;
            // 
            // btnExcel
            // 
            btnExcel.BackColor = Color.White;
            btnExcel.BorderColor = Color.FromArgb(41, 97, 27);
            btnExcel.BorderRadius = 8;
            btnExcel.BorderThickness = 1;
            btnExcel.CustomizableEdges = customizableEdges9;
            btnExcel.FillColor = Color.White;
            btnExcel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnExcel.ForeColor = Color.FromArgb(41, 97, 27);
            btnExcel.Location = new Point(168, 152);
            btnExcel.Margin = new Padding(4, 3, 4, 3);
            btnExcel.Name = "btnExcel";
            btnExcel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnExcel.Size = new Size(142, 40);
            btnExcel.TabIndex = 1;
            btnExcel.Text = "Xuất Excel";
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.White;
            btnPrint.BorderColor = Color.FromArgb(41, 97, 27);
            btnPrint.BorderRadius = 8;
            btnPrint.BorderThickness = 1;
            btnPrint.CustomizableEdges = customizableEdges11;
            btnPrint.FillColor = Color.White;
            btnPrint.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnPrint.ForeColor = Color.FromArgb(41, 97, 27);
            btnPrint.Location = new Point(328, 152);
            btnPrint.Margin = new Padding(4, 3, 4, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnPrint.Size = new Size(142, 40);
            btnPrint.TabIndex = 2;
            btnPrint.Text = "In";
            // 
            // colIndex
            // 
            colIndex.HeaderText = "#";
            colIndex.Name = "colIndex";
            colIndex.ReadOnly = true;
            colIndex.Width = 50;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.HeaderText = "colName";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colQuantity
            // 
            colQuantity.HeaderText = "colQuantity";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;
            colQuantity.Width = 121;
            // 
            // colFee
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            colFee.DefaultCellStyle = dataGridViewCellStyle3;
            colFee.HeaderText = "colFee";
            colFee.Name = "colFee";
            colFee.ReadOnly = true;
            colFee.Width = 85;
            // 
            // colDiscount
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDiscount.DefaultCellStyle = dataGridViewCellStyle4;
            colDiscount.HeaderText = "colDiscount";
            colDiscount.Name = "colDiscount";
            colDiscount.ReadOnly = true;
            colDiscount.Width = 122;
            // 
            // colPaid
            // 
            colPaid.HeaderText = "colPaid";
            colPaid.Name = "colPaid";
            colPaid.ReadOnly = true;
            colPaid.Width = 90;
            // 
            // colRealFee
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            colRealFee.DefaultCellStyle = dataGridViewCellStyle5;
            colRealFee.HeaderText = "colRealFee";
            colRealFee.Name = "colRealFee";
            colRealFee.ReadOnly = true;
            colRealFee.Width = 115;
            // 
            // FrmRevenueByIdentityGroup
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1017, 608);
            Controls.Add(btnPrint);
            Controls.Add(btnExcel);
            Controls.Add(btnSearch);
            Controls.Add(cbMode);
            Controls.Add(cbUser);
            Controls.Add(lblUserTitle);
            Controls.Add(lblCollection);
            Controls.Add(cbIdentityGroup);
            Controls.Add(dgvData);
            Controls.Add(lblEndTime);
            Controls.Add(lblGroupFilter);
            Controls.Add(lblStartTime);
            Controls.Add(dtpEndTime);
            Controls.Add(dtpStartTime);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "FrmRevenueByIdentityGroup";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Báo cáo doanh thu theo nhóm thẻ";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private KzLabel lblStartTime;
        private DateTimePicker dtpStartTime;
        private KzButton btnSearch;
        private DateTimePicker dtpEndTime;
        private KzLabel lblEndTime;
        private KzLabel lblGroupFilter;
        private DataGridView dgvData;
        private Guna.UI2.WinForms.Guna2ComboBox cbUser;
        private KzLabel lblUserTitle;
        private KzLabel lblCollection;
        private Guna.UI2.WinForms.Guna2ComboBox cbIdentityGroup;
        private Guna.UI2.WinForms.Guna2ComboBox cbMode;
        private KzButton btnExcel;
        private KzButton btnPrint;
        private DataGridViewTextBoxColumn colIndex;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colFee;
        private DataGridViewTextBoxColumn colDiscount;
        private DataGridViewTextBoxColumn colPaid;
        private DataGridViewTextBoxColumn colRealFee;
    }
}