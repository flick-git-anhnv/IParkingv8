using iPakrkingv5.Controls.Usercontrols;
using Kztek.Control8.Controls;
using Kztek.Control8.UserControls.ReportUcs;

namespace iParkingv8.Reporting
{
    partial class FrmReportIn
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportIn));
            picOverviewImageIn = new MovablePictureBox();
            picVehicleImageIn = new MovablePictureBox();
            dgvData1 = new DataGridView();
            colEventId = new DataGridViewTextBoxColumn();
            colAccessKeyId = new DataGridViewTextBoxColumn();
            colLaneInId = new DataGridViewTextBoxColumn();
            colFileKeys = new DataGridViewTextBoxColumn();
            colCustomerId = new DataGridViewTextBoxColumn();
            colVehicleId = new DataGridViewTextBoxColumn();
            colIndex = new DataGridViewTextBoxColumn();
            colPlateIn = new DataGridViewTextBoxColumn();
            colTimeIn = new DataGridViewTextBoxColumn();
            colAccessKeyCollection = new DataGridViewTextBoxColumn();
            colUser = new DataGridViewTextBoxColumn();
            colPaid = new DataGridViewTextBoxColumn();
            colLaneInName = new DataGridViewTextBoxColumn();
            colAccessKeyName = new DataGridViewTextBoxColumn();
            colAccessKeyCode = new DataGridViewTextBoxColumn();
            colVehicleCode = new DataGridViewTextBoxColumn();
            colCustomer = new DataGridViewTextBoxColumn();
            colNote = new DataGridViewTextBoxColumn();
            lblTotalEvents = new KzLabel();
            lblEndTimeTitle = new KzLabel();
            lblStartTimeTitle = new KzLabel();
            lblKeywordTitle = new Label();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            btnSearch = new KzButton();
            ucNavigator1 = new ucNavigator();
            cbVehicleType = new Guna.UI2.WinForms.Guna2ComboBox();
            cbUser = new Guna.UI2.WinForms.Guna2ComboBox();
            cbLane = new Guna.UI2.WinForms.Guna2ComboBox();
            cbIdentityGroupType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblVehicleTypeTitle = new KzLabel();
            lblUserTitle = new KzLabel();
            lblAccessKeyCollectionTitle = new KzLabel();
            lblLaneTitle = new KzLabel();
            btnFee = new KzButton();
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
            picOverviewImageIn.Location = new Point(0, 256);
            picOverviewImageIn.Margin = new Padding(0);
            picOverviewImageIn.Name = "picOverviewImageIn";
            picOverviewImageIn.Size = new Size(329, 256);
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
            picVehicleImageIn.Size = new Size(329, 256);
            picVehicleImageIn.SizeMode = PictureBoxSizeMode.StretchImage;
            picVehicleImageIn.TabIndex = 29;
            picVehicleImageIn.TabStop = false;
            // 
            // dgvData1
            // 
            dgvData1.AllowUserToAddRows = false;
            dgvData1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(230, 230, 230);
            dgvData1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData1.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData1.Columns.AddRange(new DataGridViewColumn[] { colEventId, colAccessKeyId, colLaneInId, colFileKeys, colCustomerId, colVehicleId, colIndex, colPlateIn, colTimeIn, colAccessKeyCollection, colUser, colPaid, colLaneInName, colAccessKeyName, colAccessKeyCode, colVehicleCode, colCustomer, colNote });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.Padding = new Padding(3);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(233, 238, 246);
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvData1.DefaultCellStyle = dataGridViewCellStyle5;
            dgvData1.Dock = DockStyle.Fill;
            dgvData1.Location = new Point(0, 0);
            dgvData1.Margin = new Padding(4, 3, 4, 3);
            dgvData1.Name = "dgvData1";
            dgvData1.ReadOnly = true;
            dgvData1.RowHeadersVisible = false;
            dgvData1.RowTemplate.Height = 29;
            dgvData1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData1.Size = new Size(1001, 464);
            dgvData1.TabIndex = 12;
            dgvData1.CellMouseClick += DgvData_CellMouseClick;
            // 
            // colEventId
            // 
            colEventId.HeaderText = "colEventId";
            colEventId.Name = "colEventId";
            colEventId.ReadOnly = true;
            colEventId.Visible = false;
            colEventId.Width = 99;
            // 
            // colAccessKeyId
            // 
            colAccessKeyId.HeaderText = "colAccessKeyId";
            colAccessKeyId.Name = "colAccessKeyId";
            colAccessKeyId.ReadOnly = true;
            colAccessKeyId.Visible = false;
            colAccessKeyId.Width = 136;
            // 
            // colLaneInId
            // 
            colLaneInId.HeaderText = "colLaneInId";
            colLaneInId.Name = "colLaneInId";
            colLaneInId.ReadOnly = true;
            colLaneInId.Visible = false;
            colLaneInId.Width = 107;
            // 
            // colFileKeys
            // 
            colFileKeys.HeaderText = "colFileKeys";
            colFileKeys.Name = "colFileKeys";
            colFileKeys.ReadOnly = true;
            colFileKeys.Visible = false;
            colFileKeys.Width = 103;
            // 
            // colCustomerId
            // 
            colCustomerId.HeaderText = "colCustomerId";
            colCustomerId.Name = "colCustomerId";
            colCustomerId.ReadOnly = true;
            colCustomerId.Visible = false;
            colCustomerId.Width = 130;
            // 
            // colVehicleId
            // 
            colVehicleId.HeaderText = "colVehicleId";
            colVehicleId.Name = "colVehicleId";
            colVehicleId.ReadOnly = true;
            colVehicleId.Visible = false;
            colVehicleId.Width = 111;
            // 
            // colIndex
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colIndex.DefaultCellStyle = dataGridViewCellStyle3;
            colIndex.HeaderText = "#";
            colIndex.Name = "colIndex";
            colIndex.ReadOnly = true;
            colIndex.Width = 50;
            // 
            // colPlateIn
            // 
            colPlateIn.HeaderText = "colPlateIn";
            colPlateIn.Name = "colPlateIn";
            colPlateIn.ReadOnly = true;
            colPlateIn.Width = 113;
            // 
            // colTimeIn
            // 
            colTimeIn.HeaderText = "colTimeIn";
            colTimeIn.Name = "colTimeIn";
            colTimeIn.ReadOnly = true;
            colTimeIn.Width = 113;
            // 
            // colAccessKeyCollection
            // 
            colAccessKeyCollection.HeaderText = "colAccessKeyCollection";
            colAccessKeyCollection.Name = "colAccessKeyCollection";
            colAccessKeyCollection.ReadOnly = true;
            colAccessKeyCollection.Width = 214;
            // 
            // colUser
            // 
            colUser.HeaderText = "colUser";
            colUser.Name = "colUser";
            colUser.ReadOnly = true;
            colUser.Width = 96;
            // 
            // colPaid
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPaid.DefaultCellStyle = dataGridViewCellStyle4;
            colPaid.HeaderText = "colPaid";
            colPaid.Name = "colPaid";
            colPaid.ReadOnly = true;
            colPaid.Width = 94;
            // 
            // colLaneInName
            // 
            colLaneInName.HeaderText = "colLaneInName";
            colLaneInName.Name = "colLaneInName";
            colLaneInName.ReadOnly = true;
            colLaneInName.Width = 154;
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
            // colVehicleCode
            // 
            colVehicleCode.HeaderText = "colVehicleCode";
            colVehicleCode.Name = "colVehicleCode";
            colVehicleCode.ReadOnly = true;
            colVehicleCode.Width = 154;
            // 
            // colCustomer
            // 
            colCustomer.HeaderText = "colCustomer";
            colCustomer.Name = "colCustomer";
            colCustomer.ReadOnly = true;
            colCustomer.Width = 134;
            // 
            // colNote
            // 
            colNote.HeaderText = "colNote";
            colNote.Name = "colNote";
            colNote.ReadOnly = true;
            // 
            // lblTotalEvents
            // 
            lblTotalEvents.AutoSize = true;
            lblTotalEvents.BackColor = Color.Transparent;
            lblTotalEvents.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalEvents.ForeColor = Color.FromArgb(253, 149, 40);
            lblTotalEvents.Location = new Point(616, 167);
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
            txtKeyword.CustomizableEdges = customizableEdges1;
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
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtKeyword.Size = new Size(456, 36);
            txtKeyword.TabIndex = 4;
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
            btnSearch.Location = new Point(8, 152);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
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
            ucNavigator1.Location = new Point(0, 464);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.MinimumSize = new Size(0, 44);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1001, 48);
            ucNavigator1.TabIndex = 43;
            ucNavigator1.Visible = false;
            // 
            // cbVehicleType
            // 
            cbVehicleType.BackColor = Color.Transparent;
            cbVehicleType.BorderRadius = 8;
            cbVehicleType.CustomizableEdges = customizableEdges5;
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
            cbVehicleType.Location = new Point(1008, 104);
            cbVehicleType.Margin = new Padding(0);
            cbVehicleType.Name = "cbVehicleType";
            cbVehicleType.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbVehicleType.Size = new Size(280, 36);
            cbVehicleType.TabIndex = 11;
            // 
            // cbUser
            // 
            cbUser.BackColor = Color.Transparent;
            cbUser.BorderRadius = 8;
            cbUser.CustomizableEdges = customizableEdges7;
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
            cbUser.Location = new Point(744, 32);
            cbUser.Margin = new Padding(0);
            cbUser.Name = "cbUser";
            cbUser.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbUser.Size = new Size(248, 36);
            cbUser.TabIndex = 6;
            // 
            // cbLane
            // 
            cbLane.BackColor = Color.Transparent;
            cbLane.BorderRadius = 8;
            cbLane.CustomizableEdges = customizableEdges9;
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
            cbLane.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbLane.Size = new Size(248, 36);
            cbLane.TabIndex = 5;
            // 
            // cbIdentityGroupType
            // 
            cbIdentityGroupType.BackColor = Color.Transparent;
            cbIdentityGroupType.BorderRadius = 8;
            cbIdentityGroupType.CustomizableEdges = customizableEdges11;
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
            cbIdentityGroupType.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cbIdentityGroupType.Size = new Size(512, 36);
            cbIdentityGroupType.TabIndex = 10;
            // 
            // lblVehicleTypeTitle
            // 
            lblVehicleTypeTitle.AutoSize = true;
            lblVehicleTypeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblVehicleTypeTitle.Location = new Point(1008, 80);
            lblVehicleTypeTitle.Margin = new Padding(0);
            lblVehicleTypeTitle.Name = "lblVehicleTypeTitle";
            lblVehicleTypeTitle.Size = new Size(61, 21);
            lblVehicleTypeTitle.TabIndex = 71;
            lblVehicleTypeTitle.Text = "Loại xe";
            // 
            // lblUserTitle
            // 
            lblUserTitle.AutoSize = true;
            lblUserTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUserTitle.Location = new Point(744, 8);
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
            // btnFee
            // 
            btnFee.BackColor = Color.White;
            btnFee.BorderColor = Color.FromArgb(41, 97, 27);
            btnFee.BorderRadius = 8;
            btnFee.BorderThickness = 1;
            btnFee.CustomizableEdges = customizableEdges13;
            btnFee.DisabledState.BorderColor = Color.DarkGray;
            btnFee.DisabledState.CustomBorderColor = Color.DarkGray;
            btnFee.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnFee.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnFee.FillColor = Color.White;
            btnFee.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnFee.ForeColor = Color.FromArgb(41, 97, 27);
            btnFee.Location = new Point(160, 152);
            btnFee.Margin = new Padding(0);
            btnFee.Name = "btnFee";
            btnFee.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnFee.Size = new Size(142, 40);
            btnFee.TabIndex = 1;
            btnFee.Text = "Kiểm Tra Phí";
            // 
            // btnExportExcel
            // 
            btnExportExcel.BackColor = Color.White;
            btnExportExcel.BorderColor = Color.FromArgb(41, 97, 27);
            btnExportExcel.BorderRadius = 8;
            btnExportExcel.BorderThickness = 1;
            btnExportExcel.CustomizableEdges = customizableEdges15;
            btnExportExcel.DisabledState.BorderColor = Color.DarkGray;
            btnExportExcel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnExportExcel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnExportExcel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnExportExcel.FillColor = Color.White;
            btnExportExcel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnExportExcel.ForeColor = Color.FromArgb(41, 97, 27);
            btnExportExcel.Location = new Point(312, 152);
            btnExportExcel.Margin = new Padding(0);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.ShadowDecoration.CustomizableEdges = customizableEdges16;
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
            btnCancel.CustomizableEdges = customizableEdges17;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(464, 152);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges18;
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
            splitContainer1.Location = new Point(8, 208);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
            splitContainer1.Size = new Size(1334, 512);
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
            panel1.Size = new Size(1001, 512);
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
            tableLayoutPanel1.Size = new Size(329, 512);
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
            lblCustomerCollectionTitle.Location = new Point(1008, 8);
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
            cbCustomerGroup.CustomizableEdges = customizableEdges19;
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
            cbCustomerGroup.Location = new Point(1008, 32);
            cbCustomerGroup.Margin = new Padding(0);
            cbCustomerGroup.Name = "cbCustomerGroup";
            cbCustomerGroup.ShadowDecoration.CustomizableEdges = customizableEdges20;
            cbCustomerGroup.Size = new Size(280, 36);
            cbCustomerGroup.TabIndex = 7;
            // 
            // FrmReportIn
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1350, 729);
            Controls.Add(lblTotalEvents);
            Controls.Add(splitContainer1);
            Controls.Add(cbIdentityGroupType);
            Controls.Add(btnFee);
            Controls.Add(lblKeywordTitle);
            Controls.Add(cbLane);
            Controls.Add(btnCancel);
            Controls.Add(btnExportExcel);
            Controls.Add(btnSearch);
            Controls.Add(cbCustomerGroup);
            Controls.Add(cbUser);
            Controls.Add(txtKeyword);
            Controls.Add(cbVehicleType);
            Controls.Add(lblStartTimeTitle);
            Controls.Add(lblEndTimeTitle);
            Controls.Add(lblVehicleTypeTitle);
            Controls.Add(lblCustomerCollectionTitle);
            Controls.Add(lblLaneTitle);
            Controls.Add(lblUserTitle);
            Controls.Add(lblAccessKeyCollectionTitle);
            Controls.Add(dtpStartTime);
            Controls.Add(dtpEndTime);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmReportIn";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Danh sách xe đang gửi";
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
        private Guna.UI2.WinForms.Guna2ComboBox cbVehicleType;
        private Guna.UI2.WinForms.Guna2ComboBox cbUser;
        private Guna.UI2.WinForms.Guna2ComboBox cbLane;
        private Guna.UI2.WinForms.Guna2ComboBox cbIdentityGroupType;
        private KzLabel lblVehicleTypeTitle;
        private KzLabel lblUserTitle;
        private KzLabel lblAccessKeyCollectionTitle;
        private KzLabel lblLaneTitle;
        private KzButton btnFee;
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
        private DataGridViewTextBoxColumn colEventId;
        private DataGridViewTextBoxColumn colAccessKeyId;
        private DataGridViewTextBoxColumn colLaneInId;
        private DataGridViewTextBoxColumn colFileKeys;
        private DataGridViewTextBoxColumn colCustomerId;
        private DataGridViewTextBoxColumn colVehicleId;
        private DataGridViewTextBoxColumn colIndex;
        private DataGridViewTextBoxColumn colPlateIn;
        private DataGridViewTextBoxColumn colTimeIn;
        private DataGridViewTextBoxColumn colAccessKeyCollection;
        private DataGridViewTextBoxColumn colUser;
        private DataGridViewTextBoxColumn colPaid;
        private DataGridViewTextBoxColumn colLaneInName;
        private DataGridViewTextBoxColumn colAccessKeyName;
        private DataGridViewTextBoxColumn colAccessKeyCode;
        private DataGridViewTextBoxColumn colVehicleCode;
        private DataGridViewTextBoxColumn colCustomer;
        private DataGridViewTextBoxColumn colNote;
    }
}