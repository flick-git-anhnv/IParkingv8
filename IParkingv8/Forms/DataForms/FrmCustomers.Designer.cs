using Kztek.Control8.Controls;

namespace IParkingv8.Forms.DataForms
{
    partial class FrmCustomers
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomers));
            btnCancel = new KzButton();
            btnSearch = new KzButton();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            lblKeywordTitle = new Label();
            dgvData = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            col_customer_name = new DataGridViewTextBoxColumn();
            col_customer_code = new DataGridViewTextBoxColumn();
            col_customer_phone = new DataGridViewTextBoxColumn();
            col_customer_collection = new DataGridViewTextBoxColumn();
            col_customer_address = new DataGridViewTextBoxColumn();
            ucNavigator1 = new Kztek.Control8.UserControls.ReportUcs.ucNavigator();
            cbCustomerGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCollectionTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(168, 80);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(150, 40);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Đóng";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.BorderColor = Color.FromArgb(41, 97, 27);
            btnSearch.BorderRadius = 8;
            btnSearch.BorderThickness = 1;
            btnSearch.CustomizableEdges = customizableEdges3;
            btnSearch.DisabledState.BorderColor = Color.DarkGray;
            btnSearch.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSearch.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSearch.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSearch.FillColor = Color.FromArgb(41, 97, 27);
            btnSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(8, 80);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSearch.Size = new Size(150, 40);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm Kiếm";
            // 
            // txtKeyword
            // 
            txtKeyword.BorderRadius = 8;
            txtKeyword.CustomizableEdges = customizableEdges5;
            txtKeyword.DefaultText = "";
            txtKeyword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtKeyword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtKeyword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtKeyword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtKeyword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtKeyword.Font = new Font("Segoe UI", 12F);
            txtKeyword.ForeColor = Color.Black;
            txtKeyword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtKeyword.Location = new Point(8, 32);
            txtKeyword.Margin = new Padding(4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderForeColor = Color.Gray;
            txtKeyword.PlaceholderText = "Tên | Mã";
            txtKeyword.SelectedText = "";
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtKeyword.Size = new Size(448, 36);
            txtKeyword.TabIndex = 2;
            // 
            // lblKeywordTitle
            // 
            lblKeywordTitle.AutoSize = true;
            lblKeywordTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblKeywordTitle.Location = new Point(8, 8);
            lblKeywordTitle.Margin = new Padding(0);
            lblKeywordTitle.Name = "lblKeywordTitle";
            lblKeywordTitle.Size = new Size(68, 21);
            lblKeywordTitle.TabIndex = 12;
            lblKeywordTitle.Text = "Từ khóa";
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AllowUserToResizeColumns = false;
            dgvData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 251, 255);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.BackgroundColor = SystemColors.ButtonHighlight;
            dgvData.BorderStyle = BorderStyle.None;
            dgvData.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvData.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.75F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(24, 115, 204);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData.ColumnHeadersHeight = 40;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column5, col_customer_name, col_customer_code, col_customer_phone, col_customer_collection, col_customer_address });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.Padding = new Padding(3, 0, 3, 0);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            dgvData.EnableHeadersVisualStyles = false;
            dgvData.GridColor = Color.LightGray;
            dgvData.Location = new Point(8, 136);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 40;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(917, 344);
            dgvData.TabIndex = 13;
            // 
            // Column5
            // 
            Column5.HeaderText = "#";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 51;
            // 
            // col_customer_name
            // 
            col_customer_name.HeaderText = "Tên";
            col_customer_name.Name = "col_customer_name";
            col_customer_name.ReadOnly = true;
            col_customer_name.Width = 67;
            // 
            // col_customer_code
            // 
            col_customer_code.HeaderText = "Mã";
            col_customer_code.Name = "col_customer_code";
            col_customer_code.ReadOnly = true;
            col_customer_code.Width = 65;
            // 
            // col_customer_phone
            // 
            col_customer_phone.HeaderText = "SĐT";
            col_customer_phone.Name = "col_customer_phone";
            col_customer_phone.ReadOnly = true;
            col_customer_phone.Width = 71;
            // 
            // col_customer_collection
            // 
            col_customer_collection.HeaderText = "Nhóm";
            col_customer_collection.Name = "col_customer_collection";
            col_customer_collection.ReadOnly = true;
            col_customer_collection.Width = 87;
            // 
            // col_customer_address
            // 
            col_customer_address.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_customer_address.HeaderText = "Địa chỉ";
            col_customer_address.Name = "col_customer_address";
            col_customer_address.ReadOnly = true;
            // 
            // ucNavigator1
            // 
            ucNavigator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ucNavigator1.BackColor = Color.White;
            ucNavigator1.KZUI_CurrentPage = 0;
            ucNavigator1.KZUI_MaxPage = 0;
            ucNavigator1.KZUI_TotalRecord = 0;
            ucNavigator1.Location = new Point(8, 496);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(918, 41);
            ucNavigator1.TabIndex = 14;
            // 
            // cbCustomerGroup
            // 
            cbCustomerGroup.BackColor = Color.Transparent;
            cbCustomerGroup.BorderRadius = 8;
            cbCustomerGroup.CustomizableEdges = customizableEdges7;
            cbCustomerGroup.DrawMode = DrawMode.OwnerDrawFixed;
            cbCustomerGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCustomerGroup.FocusedColor = Color.FromArgb(41, 97, 27);
            cbCustomerGroup.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCustomerGroup.Font = new Font("Segoe UI", 12F);
            cbCustomerGroup.ForeColor = Color.Black;
            cbCustomerGroup.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCustomerGroup.ItemHeight = 30;
            cbCustomerGroup.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbCustomerGroup.ItemsAppearance.SelectedForeColor = Color.White;
            cbCustomerGroup.Location = new Point(469, 32);
            cbCustomerGroup.Margin = new Padding(0);
            cbCustomerGroup.Name = "cbCustomerGroup";
            cbCustomerGroup.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbCustomerGroup.Size = new Size(456, 36);
            cbCustomerGroup.TabIndex = 3;
            // 
            // lblCollectionTitle
            // 
            lblCollectionTitle.AutoSize = true;
            lblCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCollectionTitle.Location = new Point(472, 8);
            lblCollectionTitle.Margin = new Padding(0);
            lblCollectionTitle.Name = "lblCollectionTitle";
            lblCollectionTitle.Size = new Size(141, 21);
            lblCollectionTitle.TabIndex = 75;
            lblCollectionTitle.Text = "Nhóm khách hàng";
            // 
            // FrmCustomers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(935, 543);
            Controls.Add(cbCustomerGroup);
            Controls.Add(lblCollectionTitle);
            Controls.Add(dgvData);
            Controls.Add(lblKeywordTitle);
            Controls.Add(txtKeyword);
            Controls.Add(btnSearch);
            Controls.Add(btnCancel);
            Controls.Add(ucNavigator1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmCustomers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Danh Sách Khách Hàng";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private KzButton btnCancel;
        private KzButton btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtKeyword;
        private Label lblKeywordTitle;
        private DataGridView dgvData;
        private Kztek.Control8.UserControls.ReportUcs.ucNavigator ucNavigator1;
        private Guna.UI2.WinForms.Guna2ComboBox cbCustomerGroup;
        private Label lblCollectionTitle;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn col_customer_name;
        private DataGridViewTextBoxColumn col_customer_code;
        private DataGridViewTextBoxColumn col_customer_phone;
        private DataGridViewTextBoxColumn col_customer_collection;
        private DataGridViewTextBoxColumn col_customer_address;
    }
}