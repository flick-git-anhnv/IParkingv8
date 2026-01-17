using Kztek.Control8.Controls;

namespace IParkingv8.Forms.DataForms
{
    partial class FrmVehicles
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVehicles));
            btnCancel = new KzButton();
            btnSearch = new KzButton();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            lblKeywordTitle = new Label();
            dgvData = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            col_vehicle_name = new DataGridViewTextBoxColumn();
            col_vehicle_code = new DataGridViewTextBoxColumn();
            col_vehicle_access_key = new DataGridViewTextBoxColumn();
            col_vehicle_status = new DataGridViewTextBoxColumn();
            col_vehicle_type = new DataGridViewTextBoxColumn();
            col_vehicle_customer_name = new DataGridViewTextBoxColumn();
            col_vehicle_customer_collection = new DataGridViewTextBoxColumn();
            col_vehicle_customer_phone = new DataGridViewTextBoxColumn();
            col_vehicle_customer_address = new DataGridViewTextBoxColumn();
            col_vehicle_expired_date = new DataGridViewTextBoxColumn();
            col_vehicle_collection = new DataGridViewTextBoxColumn();
            ucNavigator1 = new Kztek.Control8.UserControls.ReportUcs.ucNavigator();
            cbCollection = new Guna.UI2.WinForms.Guna2ComboBox();
            lblAccesskeyCollectionTitle = new Label();
            lblVehicleType = new Label();
            cbVehicleType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCustomerCollectionTitle = new Label();
            cbCustomerCollection = new Guna.UI2.WinForms.Guna2ComboBox();
            btnRegister = new KzButton();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            flowLayoutPanel1.SuspendLayout();
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
            btnCancel.Location = new Point(316, 0);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(150, 36);
            btnCancel.TabIndex = 5;
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
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(0, 0, 8, 0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSearch.Size = new Size(150, 36);
            btnSearch.TabIndex = 4;
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
            txtKeyword.Margin = new Padding(0);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "";
            txtKeyword.SelectedText = "";
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtKeyword.Size = new Size(320, 36);
            txtKeyword.TabIndex = 0;
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
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column5, col_vehicle_name, col_vehicle_code, col_vehicle_access_key, col_vehicle_status, col_vehicle_type, col_vehicle_customer_name, col_vehicle_customer_collection, col_vehicle_customer_phone, col_vehicle_customer_address, col_vehicle_expired_date, col_vehicle_collection });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.Black;
            dataGridViewCellStyle4.Padding = new Padding(3, 0, 3, 0);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            dgvData.EnableHeadersVisualStyles = false;
            dgvData.GridColor = Color.LightGray;
            dgvData.Location = new Point(8, 152);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 40;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(1044, 447);
            dgvData.TabIndex = 13;
            // 
            // Column5
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Column5.DefaultCellStyle = dataGridViewCellStyle3;
            Column5.HeaderText = "#";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 51;
            // 
            // col_vehicle_name
            // 
            col_vehicle_name.HeaderText = "Tên";
            col_vehicle_name.Name = "col_vehicle_name";
            col_vehicle_name.ReadOnly = true;
            col_vehicle_name.Width = 67;
            // 
            // col_vehicle_code
            // 
            col_vehicle_code.HeaderText = "Biển số";
            col_vehicle_code.Name = "col_vehicle_code";
            col_vehicle_code.ReadOnly = true;
            col_vehicle_code.Width = 95;
            // 
            // col_vehicle_access_key
            // 
            col_vehicle_access_key.HeaderText = "Định Danh";
            col_vehicle_access_key.Name = "col_vehicle_access_key";
            col_vehicle_access_key.ReadOnly = true;
            col_vehicle_access_key.Width = 116;
            // 
            // col_vehicle_status
            // 
            col_vehicle_status.HeaderText = "Trạng Thái";
            col_vehicle_status.Name = "col_vehicle_status";
            col_vehicle_status.ReadOnly = true;
            col_vehicle_status.Width = 117;
            // 
            // col_vehicle_type
            // 
            col_vehicle_type.HeaderText = "Loại";
            col_vehicle_type.Name = "col_vehicle_type";
            col_vehicle_type.ReadOnly = true;
            col_vehicle_type.Width = 72;
            // 
            // col_vehicle_customer_name
            // 
            col_vehicle_customer_name.HeaderText = "Khách Hàng";
            col_vehicle_customer_name.Name = "col_vehicle_customer_name";
            col_vehicle_customer_name.ReadOnly = true;
            col_vehicle_customer_name.Width = 129;
            // 
            // col_vehicle_customer_collection
            // 
            col_vehicle_customer_collection.HeaderText = "Nhóm Khách Hàng";
            col_vehicle_customer_collection.Name = "col_vehicle_customer_collection";
            col_vehicle_customer_collection.ReadOnly = true;
            col_vehicle_customer_collection.Width = 178;
            // 
            // col_vehicle_customer_phone
            // 
            col_vehicle_customer_phone.HeaderText = "SĐT";
            col_vehicle_customer_phone.Name = "col_vehicle_customer_phone";
            col_vehicle_customer_phone.ReadOnly = true;
            col_vehicle_customer_phone.Width = 71;
            // 
            // col_vehicle_customer_address
            // 
            col_vehicle_customer_address.HeaderText = "Địa Chỉ";
            col_vehicle_customer_address.Name = "col_vehicle_customer_address";
            col_vehicle_customer_address.ReadOnly = true;
            col_vehicle_customer_address.Width = 92;
            // 
            // col_vehicle_expired_date
            // 
            col_vehicle_expired_date.HeaderText = "Ngày Hết Hạn";
            col_vehicle_expired_date.Name = "col_vehicle_expired_date";
            col_vehicle_expired_date.ReadOnly = true;
            col_vehicle_expired_date.Width = 144;
            // 
            // col_vehicle_collection
            // 
            col_vehicle_collection.HeaderText = "Nhóm";
            col_vehicle_collection.Name = "col_vehicle_collection";
            col_vehicle_collection.ReadOnly = true;
            col_vehicle_collection.Width = 87;
            // 
            // ucNavigator1
            // 
            ucNavigator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ucNavigator1.BackColor = Color.White;
            ucNavigator1.KZUI_CurrentPage = 0;
            ucNavigator1.KZUI_MaxPage = 0;
            ucNavigator1.KZUI_TotalRecord = 0;
            ucNavigator1.Location = new Point(8, 607);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1045, 41);
            ucNavigator1.TabIndex = 14;
            // 
            // cbCollection
            // 
            cbCollection.BackColor = Color.Transparent;
            cbCollection.BorderRadius = 8;
            cbCollection.CustomizableEdges = customizableEdges7;
            cbCollection.DrawMode = DrawMode.OwnerDrawFixed;
            cbCollection.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCollection.FocusedColor = Color.FromArgb(41, 97, 27);
            cbCollection.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCollection.Font = new Font("Segoe UI", 12F);
            cbCollection.ForeColor = Color.Black;
            cbCollection.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCollection.ItemHeight = 30;
            cbCollection.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbCollection.ItemsAppearance.SelectedForeColor = Color.White;
            cbCollection.Location = new Point(344, 32);
            cbCollection.Margin = new Padding(0);
            cbCollection.Name = "cbCollection";
            cbCollection.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbCollection.Size = new Size(344, 36);
            cbCollection.TabIndex = 1;
            // 
            // lblAccesskeyCollectionTitle
            // 
            lblAccesskeyCollectionTitle.AutoSize = true;
            lblAccesskeyCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblAccesskeyCollectionTitle.Location = new Point(344, 8);
            lblAccesskeyCollectionTitle.Margin = new Padding(0);
            lblAccesskeyCollectionTitle.Name = "lblAccesskeyCollectionTitle";
            lblAccesskeyCollectionTitle.Size = new Size(131, 21);
            lblAccesskeyCollectionTitle.TabIndex = 75;
            lblAccesskeyCollectionTitle.Text = "Nhóm định danh";
            // 
            // lblVehicleType
            // 
            lblVehicleType.AutoSize = true;
            lblVehicleType.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblVehicleType.Location = new Point(704, 8);
            lblVehicleType.Margin = new Padding(0);
            lblVehicleType.Name = "lblVehicleType";
            lblVehicleType.Size = new Size(134, 21);
            lblVehicleType.TabIndex = 75;
            lblVehicleType.Text = "Loại phương tiện";
            // 
            // cbVehicleType
            // 
            cbVehicleType.BackColor = Color.Transparent;
            cbVehicleType.BorderRadius = 8;
            cbVehicleType.CustomizableEdges = customizableEdges9;
            cbVehicleType.DrawMode = DrawMode.OwnerDrawFixed;
            cbVehicleType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbVehicleType.FocusedColor = Color.FromArgb(41, 97, 27);
            cbVehicleType.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbVehicleType.Font = new Font("Segoe UI", 12F);
            cbVehicleType.ForeColor = Color.Black;
            cbVehicleType.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbVehicleType.ItemHeight = 30;
            cbVehicleType.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbVehicleType.ItemsAppearance.SelectedForeColor = Color.White;
            cbVehicleType.Location = new Point(704, 32);
            cbVehicleType.Margin = new Padding(0);
            cbVehicleType.Name = "cbVehicleType";
            cbVehicleType.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbVehicleType.Size = new Size(344, 36);
            cbVehicleType.TabIndex = 2;
            // 
            // lblCustomerCollectionTitle
            // 
            lblCustomerCollectionTitle.AutoSize = true;
            lblCustomerCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCustomerCollectionTitle.Location = new Point(8, 80);
            lblCustomerCollectionTitle.Margin = new Padding(0);
            lblCustomerCollectionTitle.Name = "lblCustomerCollectionTitle";
            lblCustomerCollectionTitle.Size = new Size(141, 21);
            lblCustomerCollectionTitle.TabIndex = 75;
            lblCustomerCollectionTitle.Text = "Nhóm khách hàng";
            // 
            // cbCustomerCollection
            // 
            cbCustomerCollection.BackColor = Color.Transparent;
            cbCustomerCollection.BorderRadius = 8;
            cbCustomerCollection.CustomizableEdges = customizableEdges11;
            cbCustomerCollection.DrawMode = DrawMode.OwnerDrawFixed;
            cbCustomerCollection.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCustomerCollection.FocusedColor = Color.FromArgb(41, 97, 27);
            cbCustomerCollection.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCustomerCollection.Font = new Font("Segoe UI", 12F);
            cbCustomerCollection.ForeColor = Color.Black;
            cbCustomerCollection.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbCustomerCollection.ItemHeight = 30;
            cbCustomerCollection.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbCustomerCollection.ItemsAppearance.SelectedForeColor = Color.White;
            cbCustomerCollection.Location = new Point(8, 104);
            cbCustomerCollection.Margin = new Padding(0);
            cbCustomerCollection.Name = "cbCustomerCollection";
            cbCustomerCollection.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cbCustomerCollection.Size = new Size(320, 36);
            cbCustomerCollection.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.White;
            btnRegister.BorderColor = Color.FromArgb(41, 97, 27);
            btnRegister.BorderRadius = 8;
            btnRegister.BorderThickness = 1;
            btnRegister.CustomizableEdges = customizableEdges13;
            btnRegister.DisabledState.BorderColor = Color.DarkGray;
            btnRegister.DisabledState.CustomBorderColor = Color.DarkGray;
            btnRegister.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnRegister.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnRegister.FillColor = Color.White;
            btnRegister.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnRegister.ForeColor = Color.FromArgb(41, 97, 27);
            btnRegister.Location = new Point(158, 0);
            btnRegister.Margin = new Padding(0, 0, 8, 0);
            btnRegister.Name = "btnRegister";
            btnRegister.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnRegister.Size = new Size(150, 36);
            btnRegister.TabIndex = 5;
            btnRegister.Text = "Đăng ký";
            btnRegister.Click += BtnRegister_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnSearch);
            flowLayoutPanel1.Controls.Add(btnRegister);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Location = new Point(344, 104);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(472, 36);
            flowLayoutPanel1.TabIndex = 76;
            // 
            // FrmVehicles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1062, 654);
            Controls.Add(cbCustomerCollection);
            Controls.Add(cbVehicleType);
            Controls.Add(cbCollection);
            Controls.Add(lblCustomerCollectionTitle);
            Controls.Add(lblVehicleType);
            Controls.Add(lblAccesskeyCollectionTitle);
            Controls.Add(dgvData);
            Controls.Add(lblKeywordTitle);
            Controls.Add(txtKeyword);
            Controls.Add(ucNavigator1);
            Controls.Add(flowLayoutPanel1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmVehicles";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Danh Sách Phương Tiện";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2ComboBox cbCollection;
        private Label lblAccesskeyCollectionTitle;
        private Label lblVehicleType;
        private Guna.UI2.WinForms.Guna2ComboBox cbVehicleType;
        private Label lblCustomerCollectionTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbCustomerCollection;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn col_vehicle_name;
        private DataGridViewTextBoxColumn col_vehicle_code;
        private DataGridViewTextBoxColumn col_vehicle_access_key;
        private DataGridViewTextBoxColumn col_vehicle_status;
        private DataGridViewTextBoxColumn col_vehicle_type;
        private DataGridViewTextBoxColumn col_vehicle_customer_name;
        private DataGridViewTextBoxColumn col_vehicle_customer_collection;
        private DataGridViewTextBoxColumn col_vehicle_customer_phone;
        private DataGridViewTextBoxColumn col_vehicle_customer_address;
        private DataGridViewTextBoxColumn col_vehicle_expired_date;
        private DataGridViewTextBoxColumn col_vehicle_collection;
        private KzButton btnRegister;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}