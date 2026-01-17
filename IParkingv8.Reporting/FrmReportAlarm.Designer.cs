using iPakrkingv5.Controls.Usercontrols;
using iParkingv8.Reporting;
using Kztek.Control8.Controls;
using Kztek.Control8.UserControls.ReportUcs;

namespace IParkingv8.Reporting
{
    partial class FrmReportAlarm
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges33 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges34 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges35 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges36 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportAlarm));
            picOverviewImageIn = new MovablePictureBox();
            picVehicleImageIn = new MovablePictureBox();
            dgvData1 = new DataGridView();
            lblTotalEvents = new KzLabel();
            lblEndTimeTitle = new KzLabel();
            lblStartTimeTitle = new KzLabel();
            lblKeywordTitle = new Label();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            btnSearch = new KzButton();
            ucNavigator1 = new ucNavigator();
            cbUser = new Guna.UI2.WinForms.Guna2ComboBox();
            cbLane = new Guna.UI2.WinForms.Guna2ComboBox();
            cbIdentityGroupType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblUserTitle = new KzLabel();
            lblAccessKeyCollectionTitle = new KzLabel();
            lblLaneTitle = new KzLabel();
            btnExportExcel = new KzButton();
            btnCancel = new KzButton();
            Column5 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            dtpStartTime = new DateTimePicker();
            dtpEndTime = new DateTimePicker();
            lblCustomerCollectionTitle = new KzLabel();
            cbCustomerGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            kzLabel1 = new KzLabel();
            cbAlarmMode = new Guna.UI2.WinForms.Guna2ComboBox();
            colEventId = new DataGridViewTextBoxColumn();
            colFileKeys = new DataGridViewTextBoxColumn();
            colIndex = new DataGridViewTextBoxColumn();
            colTime = new DataGridViewTextBoxColumn();
            colAccessKeyName = new DataGridViewTextBoxColumn();
            colAccessKeyCode = new DataGridViewTextBoxColumn();
            colAccessKeyCollection = new DataGridViewTextBoxColumn();
            colCustomer = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colLaneInName = new DataGridViewTextBoxColumn();
            colPlateIn = new DataGridViewTextBoxColumn();
            colVehicleCode = new DataGridViewTextBoxColumn();
            colNote = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)picOverviewImageIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvData1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // picOverviewImageIn
            // 
            picOverviewImageIn.BackColor = Color.White;
            picOverviewImageIn.BorderStyle = BorderStyle.FixedSingle;
            picOverviewImageIn.DisplayImage = null;
            picOverviewImageIn.Dock = DockStyle.Fill;
            picOverviewImageIn.Location = new Point(0, 220);
            picOverviewImageIn.Margin = new Padding(0);
            picOverviewImageIn.Name = "picOverviewImageIn";
            picOverviewImageIn.Size = new Size(329, 220);
            picOverviewImageIn.SizeMode = PictureBoxSizeMode.StretchImage;
            picOverviewImageIn.TabIndex = 30;
            picOverviewImageIn.TabStop = false;
            // 
            // picVehicleImageIn
            // 
            picVehicleImageIn.BackColor = Color.White;
            picVehicleImageIn.BorderStyle = BorderStyle.FixedSingle;
            picVehicleImageIn.DisplayImage = null;
            picVehicleImageIn.Dock = DockStyle.Fill;
            picVehicleImageIn.Location = new Point(0, 0);
            picVehicleImageIn.Margin = new Padding(0);
            picVehicleImageIn.Name = "picVehicleImageIn";
            picVehicleImageIn.Size = new Size(329, 220);
            picVehicleImageIn.SizeMode = PictureBoxSizeMode.StretchImage;
            picVehicleImageIn.TabIndex = 29;
            picVehicleImageIn.TabStop = false;
            // 
            // dgvData1
            // 
            dgvData1.AllowUserToAddRows = false;
            dgvData1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(230, 230, 230);
            dgvData1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData1.BackgroundColor = Color.White;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.Padding = new Padding(3);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvData1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvData1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData1.Columns.AddRange(new DataGridViewColumn[] { colEventId, colFileKeys, colIndex, colTime, colAccessKeyName, colAccessKeyCode, colAccessKeyCollection, colCustomer, colType, colLaneInName, colPlateIn, colVehicleCode, colNote, colUser });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.Padding = new Padding(3);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(233, 238, 246);
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvData1.DefaultCellStyle = dataGridViewCellStyle8;
            dgvData1.Dock = DockStyle.Fill;
            dgvData1.Location = new Point(0, 0);
            dgvData1.Margin = new Padding(4, 3, 4, 3);
            dgvData1.Name = "dgvData1";
            dgvData1.ReadOnly = true;
            dgvData1.RowHeadersVisible = false;
            dgvData1.RowTemplate.Height = 29;
            dgvData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData1.Size = new Size(1001, 392);
            dgvData1.TabIndex = 12;
            // 
            // lblTotalEvents
            // 
            lblTotalEvents.AutoSize = true;
            lblTotalEvents.BackColor = Color.Transparent;
            lblTotalEvents.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalEvents.ForeColor = Color.FromArgb(253, 149, 40);
            lblTotalEvents.Location = new Point(464, 239);
            lblTotalEvents.Margin = new Padding(4, 0, 4, 0);
            lblTotalEvents.Name = "lblTotalEvents";
            lblTotalEvents.Size = new Size(153, 25);
            lblTotalEvents.TabIndex = 58;
            lblTotalEvents.Text = "Tổng số sự kiện";
            lblTotalEvents.Visible = false;
            // 
            // lblEndTimeTitle
            // 
            lblEndTimeTitle.AutoSize = true;
            lblEndTimeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblEndTimeTitle.Location = new Point(248, 80);
            lblEndTimeTitle.Margin = new Padding(0);
            lblEndTimeTitle.Name = "lblEndTimeTitle";
            lblEndTimeTitle.Size = new Size(71, 21);
            lblEndTimeTitle.TabIndex = 71;
            lblEndTimeTitle.Text = "Kết thúc";
            // 
            // lblStartTimeTitle
            // 
            lblStartTimeTitle.AutoSize = true;
            lblStartTimeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStartTimeTitle.Location = new Point(8, 80);
            lblStartTimeTitle.Margin = new Padding(0);
            lblStartTimeTitle.Name = "lblStartTimeTitle";
            lblStartTimeTitle.Size = new Size(65, 21);
            lblStartTimeTitle.TabIndex = 71;
            lblStartTimeTitle.Text = "Bắt đầu";
            // 
            // lblKeywordTitle
            // 
            lblKeywordTitle.AutoSize = true;
            lblKeywordTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblKeywordTitle.Location = new Point(8, 8);
            lblKeywordTitle.Margin = new Padding(0);
            lblKeywordTitle.Name = "lblKeywordTitle";
            lblKeywordTitle.Size = new Size(68, 21);
            lblKeywordTitle.TabIndex = 71;
            lblKeywordTitle.Text = "Từ khóa";
            // 
            // txtKeyword
            // 
            txtKeyword.BorderRadius = 8;
            txtKeyword.CustomizableEdges = customizableEdges19;
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
            txtKeyword.PlaceholderText = "";
            txtKeyword.SelectedText = "";
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtKeyword.Size = new Size(456, 36);
            txtKeyword.TabIndex = 4;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.BorderColor = Color.FromArgb(41, 97, 27);
            btnSearch.BorderRadius = 8;
            btnSearch.BorderThickness = 1;
            btnSearch.CustomizableEdges = customizableEdges21;
            btnSearch.DisabledState.BorderColor = Color.DarkGray;
            btnSearch.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSearch.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSearch.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSearch.FillColor = Color.FromArgb(41, 97, 27);
            btnSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(8, 224);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges22;
            btnSearch.Size = new Size(142, 40);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm Kiếm";
            // 
            // ucNavigator1
            // 
            ucNavigator1.BackColor = Color.White;
            ucNavigator1.BorderStyle = BorderStyle.Fixed3D;
            ucNavigator1.Dock = DockStyle.Bottom;
            ucNavigator1.Font = new Font("Segoe UI", 12F);
            ucNavigator1.KZUI_CurrentPage = 1;
            ucNavigator1.KZUI_MaxPage = 1;
            ucNavigator1.KZUI_TotalRecord = 0;
            ucNavigator1.Location = new Point(0, 392);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.MinimumSize = new Size(0, 44);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1001, 48);
            ucNavigator1.TabIndex = 43;
            ucNavigator1.Visible = false;
            // 
            // cbUser
            // 
            cbUser.BackColor = Color.Transparent;
            cbUser.BorderRadius = 8;
            cbUser.CustomizableEdges = customizableEdges23;
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
            cbUser.Location = new Point(896, 32);
            cbUser.Margin = new Padding(0);
            cbUser.Name = "cbUser";
            cbUser.ShadowDecoration.CustomizableEdges = customizableEdges24;
            cbUser.Size = new Size(392, 36);
            cbUser.TabIndex = 6;
            // 
            // cbLane
            // 
            cbLane.BackColor = Color.Transparent;
            cbLane.BorderRadius = 8;
            cbLane.CustomizableEdges = customizableEdges25;
            cbLane.DrawMode = DrawMode.OwnerDrawFixed;
            cbLane.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLane.FocusedColor = Color.FromArgb(41, 97, 27);
            cbLane.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbLane.Font = new Font("Segoe UI", 12F);
            cbLane.ForeColor = Color.Black;
            cbLane.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbLane.ItemHeight = 30;
            cbLane.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbLane.ItemsAppearance.SelectedForeColor = Color.White;
            cbLane.Location = new Point(480, 32);
            cbLane.Margin = new Padding(0);
            cbLane.Name = "cbLane";
            cbLane.ShadowDecoration.CustomizableEdges = customizableEdges26;
            cbLane.Size = new Size(400, 36);
            cbLane.TabIndex = 5;
            // 
            // cbIdentityGroupType
            // 
            cbIdentityGroupType.BackColor = Color.Transparent;
            cbIdentityGroupType.BorderRadius = 8;
            cbIdentityGroupType.CustomizableEdges = customizableEdges27;
            cbIdentityGroupType.DrawMode = DrawMode.OwnerDrawFixed;
            cbIdentityGroupType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIdentityGroupType.FocusedColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroupType.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroupType.Font = new Font("Segoe UI", 12F);
            cbIdentityGroupType.ForeColor = Color.Black;
            cbIdentityGroupType.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroupType.ItemHeight = 30;
            cbIdentityGroupType.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbIdentityGroupType.ItemsAppearance.SelectedForeColor = Color.White;
            cbIdentityGroupType.Location = new Point(480, 104);
            cbIdentityGroupType.Margin = new Padding(0);
            cbIdentityGroupType.Name = "cbIdentityGroupType";
            cbIdentityGroupType.ShadowDecoration.CustomizableEdges = customizableEdges28;
            cbIdentityGroupType.Size = new Size(808, 36);
            cbIdentityGroupType.TabIndex = 10;
            // 
            // lblUserTitle
            // 
            lblUserTitle.AutoSize = true;
            lblUserTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUserTitle.Location = new Point(896, 8);
            lblUserTitle.Margin = new Padding(0);
            lblUserTitle.Name = "lblUserTitle";
            lblUserTitle.Size = new Size(84, 21);
            lblUserTitle.TabIndex = 71;
            lblUserTitle.Text = "Người tạo";
            // 
            // lblAccessKeyCollectionTitle
            // 
            lblAccessKeyCollectionTitle.AutoSize = true;
            lblAccessKeyCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblAccessKeyCollectionTitle.Location = new Point(480, 80);
            lblAccessKeyCollectionTitle.Margin = new Padding(0);
            lblAccessKeyCollectionTitle.Name = "lblAccessKeyCollectionTitle";
            lblAccessKeyCollectionTitle.Size = new Size(131, 21);
            lblAccessKeyCollectionTitle.TabIndex = 71;
            lblAccessKeyCollectionTitle.Text = "Nhóm định danh";
            // 
            // lblLaneTitle
            // 
            lblLaneTitle.AutoSize = true;
            lblLaneTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLaneTitle.Location = new Point(480, 8);
            lblLaneTitle.Margin = new Padding(0);
            lblLaneTitle.Name = "lblLaneTitle";
            lblLaneTitle.Size = new Size(35, 21);
            lblLaneTitle.TabIndex = 71;
            lblLaneTitle.Text = "Làn";
            // 
            // btnExportExcel
            // 
            btnExportExcel.BackColor = Color.White;
            btnExportExcel.BorderColor = Color.FromArgb(41, 97, 27);
            btnExportExcel.BorderRadius = 8;
            btnExportExcel.BorderThickness = 1;
            btnExportExcel.CustomizableEdges = customizableEdges29;
            btnExportExcel.DisabledState.BorderColor = Color.DarkGray;
            btnExportExcel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExportExcel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExportExcel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExportExcel.FillColor = Color.White;
            btnExportExcel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnExportExcel.ForeColor = Color.FromArgb(41, 97, 27);
            btnExportExcel.Location = new Point(160, 224);
            btnExportExcel.Margin = new Padding(0);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.ShadowDecoration.CustomizableEdges = customizableEdges30;
            btnExportExcel.Size = new Size(142, 40);
            btnExportExcel.TabIndex = 2;
            btnExportExcel.Text = "Xuất Excel";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges31;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(312, 224);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            btnCancel.Size = new Size(142, 40);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Đóng";
            // 
            // Column5
            // 
            Column5.HeaderText = "STT";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 69;
            // 
            // Column7
            // 
            Column7.HeaderText = "Tên";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 67;
            // 
            // Column8
            // 
            Column8.HeaderText = "Mã";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 65;
            // 
            // Column9
            // 
            Column9.HeaderText = "Loại";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.Width = 72;
            // 
            // Column10
            // 
            Column10.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column10.HeaderText = "Nhóm";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            // 
            // Column11
            // 
            Column11.HeaderText = "Id";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.Visible = false;
            Column11.Width = 56;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "STT";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 69;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Tên";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.Width = 67;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Mã";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 65;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Loại";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 72;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn5.HeaderText = "Nhóm";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Id";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Visible = false;
            dataGridViewTextBoxColumn6.Width = 56;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "STT";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 69;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Tên";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.ReadOnly = true;
            dataGridViewTextBoxColumn8.Width = 67;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "Mã";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            dataGridViewTextBoxColumn9.Width = 65;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.HeaderText = "Loại";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            dataGridViewTextBoxColumn10.Width = 72;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn11.HeaderText = "Nhóm";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.HeaderText = "Id";
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            dataGridViewTextBoxColumn12.ReadOnly = true;
            dataGridViewTextBoxColumn12.Visible = false;
            dataGridViewTextBoxColumn12.Width = 56;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.HeaderText = "STT";
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            dataGridViewTextBoxColumn13.ReadOnly = true;
            dataGridViewTextBoxColumn13.Width = 69;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewTextBoxColumn14.HeaderText = "Tên";
            dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            dataGridViewTextBoxColumn14.ReadOnly = true;
            dataGridViewTextBoxColumn14.Width = 67;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewTextBoxColumn15.HeaderText = "Mã";
            dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            dataGridViewTextBoxColumn15.ReadOnly = true;
            dataGridViewTextBoxColumn15.Width = 65;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewTextBoxColumn16.HeaderText = "Loại";
            dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            dataGridViewTextBoxColumn16.ReadOnly = true;
            dataGridViewTextBoxColumn16.Width = 72;
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewTextBoxColumn17.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTextBoxColumn17.HeaderText = "Nhóm";
            dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewTextBoxColumn18.HeaderText = "Id";
            dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            dataGridViewTextBoxColumn18.ReadOnly = true;
            dataGridViewTextBoxColumn18.Visible = false;
            dataGridViewTextBoxColumn18.Width = 56;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(8, 280);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer1.Size = new Size(1334, 440);
            splitContainer1.SplitterDistance = 1001;
            splitContainer1.TabIndex = 74;
            // 
            // panel1
            // 
            panel1.Controls.Add(dgvData1);
            panel1.Controls.Add(ucNavigator1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1001, 440);
            panel1.TabIndex = 76;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(picOverviewImageIn, 0, 1);
            tableLayoutPanel1.Controls.Add(picVehicleImageIn, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(329, 440);
            tableLayoutPanel1.TabIndex = 75;
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(8, 108);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.Size = new Size(216, 29);
            dtpStartTime.TabIndex = 8;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "HH:mm:ss dd/MM/yyyy";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(248, 108);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.Size = new Size(216, 29);
            dtpEndTime.TabIndex = 9;
            // 
            // lblCustomerCollectionTitle
            // 
            lblCustomerCollectionTitle.AutoSize = true;
            lblCustomerCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCustomerCollectionTitle.Location = new Point(8, 144);
            lblCustomerCollectionTitle.Margin = new Padding(0);
            lblCustomerCollectionTitle.Name = "lblCustomerCollectionTitle";
            lblCustomerCollectionTitle.Size = new Size(141, 21);
            lblCustomerCollectionTitle.TabIndex = 71;
            lblCustomerCollectionTitle.Text = "Nhóm khách hàng";
            // 
            // cbCustomerGroup
            // 
            cbCustomerGroup.BackColor = Color.Transparent;
            cbCustomerGroup.BorderRadius = 8;
            cbCustomerGroup.CustomizableEdges = customizableEdges33;
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
            cbCustomerGroup.Location = new Point(8, 168);
            cbCustomerGroup.Margin = new Padding(0);
            cbCustomerGroup.Name = "cbCustomerGroup";
            cbCustomerGroup.ShadowDecoration.CustomizableEdges = customizableEdges34;
            cbCustomerGroup.Size = new Size(456, 36);
            cbCustomerGroup.TabIndex = 7;
            // 
            // kzLabel1
            // 
            kzLabel1.AutoSize = true;
            kzLabel1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            kzLabel1.Location = new Point(480, 144);
            kzLabel1.Margin = new Padding(0);
            kzLabel1.Name = "kzLabel1";
            kzLabel1.Size = new Size(78, 21);
            kzLabel1.TabIndex = 71;
            kzLabel1.Text = "Cảnh báo";
            // 
            // cbAlarmMode
            // 
            cbAlarmMode.BackColor = Color.Transparent;
            cbAlarmMode.BorderRadius = 8;
            cbAlarmMode.CustomizableEdges = customizableEdges35;
            cbAlarmMode.DrawMode = DrawMode.OwnerDrawFixed;
            cbAlarmMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAlarmMode.FocusedColor = Color.FromArgb(41, 97, 27);
            cbAlarmMode.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbAlarmMode.Font = new Font("Segoe UI", 12F);
            cbAlarmMode.ForeColor = Color.Black;
            cbAlarmMode.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbAlarmMode.ItemHeight = 30;
            cbAlarmMode.Items.AddRange(new object[] { "Tất cả", "Mã thẻ không có trong hệ thống", "Thẻ/Xe không hợp lệ", "Định danh bị khóa", "Phương tiện hết hạn sử dụng", "Nhóm định danh không được phép sử dụng với làn", "Biển số khác với biển đăng ký", "Biển số vào ra không khớp", "Biển số đen", "Mở barrie bằng bàn phím", "Mở barrie bằng nút nhấn", "Ghi vé", "Xe đã vào bãi", "Xe chưa vào bãi", "Xe đè vòng loop", "Xóa sự kiện xe trong bãi", "Nợ thẻ", "Đổi nhóm thẻ" });
            cbAlarmMode.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbAlarmMode.ItemsAppearance.SelectedForeColor = Color.White;
            cbAlarmMode.Location = new Point(480, 168);
            cbAlarmMode.Margin = new Padding(0);
            cbAlarmMode.Name = "cbAlarmMode";
            cbAlarmMode.ShadowDecoration.CustomizableEdges = customizableEdges36;
            cbAlarmMode.Size = new Size(808, 36);
            cbAlarmMode.TabIndex = 7;
            // 
            // colEventId
            // 
            colEventId.HeaderText = "colEventId";
            colEventId.Name = "colEventId";
            colEventId.ReadOnly = true;
            colEventId.Visible = false;
            colEventId.Width = 99;
            // 
            // colFileKeys
            // 
            colFileKeys.HeaderText = "colFileKeys";
            colFileKeys.Name = "colFileKeys";
            colFileKeys.ReadOnly = true;
            colFileKeys.Visible = false;
            colFileKeys.Width = 103;
            // 
            // colIndex
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIndex.DefaultCellStyle = dataGridViewCellStyle7;
            colIndex.HeaderText = "#";
            colIndex.Name = "colIndex";
            colIndex.ReadOnly = true;
            colIndex.Width = 50;
            // 
            // colTime
            // 
            colTime.HeaderText = "colTimeIn";
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            colTime.Width = 113;
            // 
            // colAccessKeyName
            // 
            colAccessKeyName.HeaderText = "colAccessKeyName";
            colAccessKeyName.Name = "colAccessKeyName";
            colAccessKeyName.ReadOnly = true;
            colAccessKeyName.Width = 183;
            // 
            // colAccessKeyCode
            // 
            colAccessKeyCode.HeaderText = "colAccessKeyCode";
            colAccessKeyCode.Name = "colAccessKeyCode";
            colAccessKeyCode.ReadOnly = true;
            colAccessKeyCode.Width = 179;
            // 
            // colAccessKeyCollection
            // 
            colAccessKeyCollection.HeaderText = "colAccessKeyCollection";
            colAccessKeyCollection.Name = "colAccessKeyCollection";
            colAccessKeyCollection.ReadOnly = true;
            colAccessKeyCollection.Width = 214;
            // 
            // colCustomer
            // 
            colCustomer.HeaderText = "colCustomer";
            colCustomer.Name = "colCustomer";
            colCustomer.ReadOnly = true;
            colCustomer.Width = 134;
            // 
            // colType
            // 
            colType.HeaderText = "colType";
            colType.Name = "colType";
            colType.ReadOnly = true;
            colType.Width = 98;
            // 
            // colLaneInName
            // 
            colLaneInName.HeaderText = "colLaneInName";
            colLaneInName.Name = "colLaneInName";
            colLaneInName.ReadOnly = true;
            colLaneInName.Width = 154;
            // 
            // colPlateIn
            // 
            colPlateIn.HeaderText = "colPlateIn";
            colPlateIn.Name = "colPlateIn";
            colPlateIn.ReadOnly = true;
            colPlateIn.Width = 113;
            // 
            // colVehicleCode
            // 
            colVehicleCode.HeaderText = "colVehicleCode";
            colVehicleCode.Name = "colVehicleCode";
            colVehicleCode.ReadOnly = true;
            colVehicleCode.Width = 154;
            // 
            // colNote
            // 
            colNote.HeaderText = "colNote";
            colNote.Name = "colNote";
            colNote.ReadOnly = true;
            // 
            // colUser
            // 
            colUser.HeaderText = "colUser";
            colUser.Name = "colUser";
            colUser.ReadOnly = true;
            colUser.Width = 96;
            // 
            // FrmReportAlarm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1350, 729);
            Controls.Add(lblTotalEvents);
            Controls.Add(splitContainer1);
            Controls.Add(cbIdentityGroupType);
            Controls.Add(lblKeywordTitle);
            Controls.Add(cbLane);
            Controls.Add(btnCancel);
            Controls.Add(btnExportExcel);
            Controls.Add(btnSearch);
            Controls.Add(cbAlarmMode);
            Controls.Add(cbCustomerGroup);
            Controls.Add(cbUser);
            Controls.Add(txtKeyword);
            Controls.Add(lblStartTimeTitle);
            Controls.Add(kzLabel1);
            Controls.Add(lblEndTimeTitle);
            Controls.Add(lblCustomerCollectionTitle);
            Controls.Add(lblLaneTitle);
            Controls.Add(lblUserTitle);
            Controls.Add(lblAccessKeyCollectionTitle);
            Controls.Add(dtpStartTime);
            Controls.Add(dtpEndTime);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmReportAlarm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sự kiện cảnh báo";
            ((System.ComponentModel.ISupportInitialize)picOverviewImageIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvData1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MovablePictureBox picOverviewImageIn;
        private MovablePictureBox picVehicleImageIn;
        private DataGridView dgvData1;
        private KzLabel lblTotalEvents;
        private ucNavigator ucNavigator1;
        private DataGridViewTextBoxColumn Column9;
        private Label lblKeywordTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtKeyword;
        private KzButton btnSearch;
        private KzLabel lblStartTimeTitle;
        private KzLabel lblEndTimeTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbUser;
        private Guna.UI2.WinForms.Guna2ComboBox cbLane;
        private Guna.UI2.WinForms.Guna2ComboBox cbIdentityGroupType;
        private KzLabel lblUserTitle;
        private KzLabel lblAccessKeyCollectionTitle;
        private KzLabel lblLaneTitle;
        private KzButton btnExportExcel;
        private KzButton btnCancel;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
        private KzLabel lblCustomerCollectionTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbCustomerGroup;
        private Panel panel1;
        private KzLabel kzLabel1;
        private Guna.UI2.WinForms.Guna2ComboBox cbAlarmMode;
        private DataGridViewTextBoxColumn colEventId;
        private DataGridViewTextBoxColumn colFileKeys;
        private DataGridViewTextBoxColumn colIndex;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colAccessKeyName;
        private DataGridViewTextBoxColumn colAccessKeyCode;
        private DataGridViewTextBoxColumn colAccessKeyCollection;
        private DataGridViewTextBoxColumn colCustomer;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colLaneInName;
        private DataGridViewTextBoxColumn colPlateIn;
        private DataGridViewTextBoxColumn colVehicleCode;
        private DataGridViewTextBoxColumn colNote;
        private DataGridViewTextBoxColumn colUser;
    }
}