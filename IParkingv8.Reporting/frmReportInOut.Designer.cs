using iPakrkingv5.Controls.Usercontrols;
using Kztek.Control8.Controls;
using Kztek.Control8.UserControls.ReportUcs;

namespace iParkingv8.Reporting
{
    partial class FrmReportInOut
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportInOut));
            tableLayoutPanel1 = new TableLayoutPanel();
            picOverviewImageIn = new MovablePictureBox();
            picVehicleImageIn = new MovablePictureBox();
            dgvData = new DataGridView();
            colEventOutId = new DataGridViewTextBoxColumn();
            colEventInId = new DataGridViewTextBoxColumn();
            colInvoicePendingId = new DataGridViewTextBoxColumn();
            colInvoiceId = new DataGridViewTextBoxColumn();
            colIndex = new DataGridViewTextBoxColumn();
            colPlateIn = new DataGridViewTextBoxColumn();
            colPlateOut = new DataGridViewTextBoxColumn();
            colTimeIn = new DataGridViewTextBoxColumn();
            colTimeOut = new DataGridViewTextBoxColumn();
            colParkingTime = new DataGridViewTextBoxColumn();
            colAccessKeyCollection = new DataGridViewTextBoxColumn();
            colFee = new DataGridViewTextBoxColumn();
            colDiscount = new DataGridViewTextBoxColumn();
            colPaid = new DataGridViewTextBoxColumn();
            colRealFee = new DataGridViewTextBoxColumn();
            colAccessKeyCode = new DataGridViewTextBoxColumn();
            colAccessKeyName = new DataGridViewTextBoxColumn();
            colUserIn = new DataGridViewTextBoxColumn();
            colUserOut = new DataGridViewTextBoxColumn();
            colInvoiceTemplate = new DataGridViewTextBoxColumn();
            colInvoiceNo = new DataGridViewTextBoxColumn();
            colLaneIn = new DataGridViewTextBoxColumn();
            colLaneOut = new DataGridViewTextBoxColumn();
            colNote = new DataGridViewTextBoxColumn();
            colFileKeyIn = new DataGridViewTextBoxColumn();
            colFileKeyOut = new DataGridViewTextBoxColumn();
            tableLayoutPanel2 = new TableLayoutPanel();
            picOverviewImageOut = new MovablePictureBox();
            picVehicleImageOut = new MovablePictureBox();
            ucNavigator1 = new ucNavigator();
            lblTotalEvents = new KzLabel();
            tablePic = new TableLayoutPanel();
            toolTip1 = new ToolTip(components);
            cbIdentityGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            lblKeywordTitle = new KzLabel();
            cbLane = new Guna.UI2.WinForms.Guna2ComboBox();
            cbUser = new Guna.UI2.WinForms.Guna2ComboBox();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            cbVehicleType = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStartTimeTitle = new KzLabel();
            lblEndTimeTitle = new KzLabel();
            lblVehicleTypeTitle = new KzLabel();
            lblLaneTitle = new KzLabel();
            lblUserTitle = new KzLabel();
            lblAccessKeyCollectionTitle = new KzLabel();
            btnPrintPhieuThu = new KzButton();
            btnCancel = new KzButton();
            btnExportExcel = new KzButton();
            btnSearch = new KzButton();
            splitContainer1 = new SplitContainer();
            dtpStartTime = new DateTimePicker();
            dtpEndTime = new DateTimePicker();
            lblCustomerCollectionTitle = new KzLabel();
            cbCustomerGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picOverviewImageIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageIn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picOverviewImageOut).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageOut).BeginInit();
            tablePic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(picOverviewImageIn, 0, 0);
            tableLayoutPanel1.Controls.Add(picVehicleImageIn, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(250, 484);
            tableLayoutPanel1.TabIndex = 53;
            // 
            // picOverviewImageIn
            // 
            picOverviewImageIn.BackColor = Color.White;
            picOverviewImageIn.BorderStyle = BorderStyle.FixedSingle;
            picOverviewImageIn.DisplayImage = null;
            picOverviewImageIn.Dock = DockStyle.Fill;
            picOverviewImageIn.Location = new Point(0, 0);
            picOverviewImageIn.Margin = new Padding(0);
            picOverviewImageIn.Name = "picOverviewImageIn";
            picOverviewImageIn.Size = new Size(250, 242);
            picOverviewImageIn.SizeMode = PictureBoxSizeMode.StretchImage;
            picOverviewImageIn.TabIndex = 30;
            picOverviewImageIn.TabStop = false;
            toolTip1.SetToolTip(picOverviewImageIn, "Kích đúp chuột để xem hình ảnh phóng to");
            picOverviewImageIn.LoadCompleted += Pic_LoadCompleted;
            // 
            // picVehicleImageIn
            // 
            picVehicleImageIn.BackColor = Color.White;
            picVehicleImageIn.BorderStyle = BorderStyle.FixedSingle;
            picVehicleImageIn.DisplayImage = null;
            picVehicleImageIn.Dock = DockStyle.Fill;
            picVehicleImageIn.Location = new Point(0, 242);
            picVehicleImageIn.Margin = new Padding(0);
            picVehicleImageIn.Name = "picVehicleImageIn";
            picVehicleImageIn.Size = new Size(250, 242);
            picVehicleImageIn.SizeMode = PictureBoxSizeMode.StretchImage;
            picVehicleImageIn.TabIndex = 29;
            picVehicleImageIn.TabStop = false;
            toolTip1.SetToolTip(picVehicleImageIn, "Kích đúp chuột để xem hình ảnh phóng to");
            picVehicleImageIn.LoadCompleted += Pic_LoadCompleted;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(230, 230, 230);
            dgvData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.Padding = new Padding(3);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { colEventOutId, colEventInId, colInvoicePendingId, colInvoiceId, colIndex, colPlateIn, colPlateOut, colTimeIn, colTimeOut, colParkingTime, colAccessKeyCollection, colFee, colDiscount, colPaid, colRealFee, colAccessKeyCode, colAccessKeyName, colUserIn, colUserOut, colInvoiceTemplate, colInvoiceNo, colLaneIn, colLaneOut, colNote, colFileKeyIn, colFileKeyOut });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.Padding = new Padding(3);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(233, 238, 246);
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle8;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 0);
            dgvData.Margin = new Padding(0);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 29;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(1032, 440);
            dgvData.TabIndex = 50;
            dgvData.CellMouseClick += DgvData_CellMouseClick;
            // 
            // colEventOutId
            // 
            colEventOutId.HeaderText = "colEventOutId";
            colEventOutId.Name = "colEventOutId";
            colEventOutId.ReadOnly = true;
            colEventOutId.Visible = false;
            colEventOutId.Width = 126;
            // 
            // colEventInId
            // 
            colEventInId.HeaderText = "colEventInId";
            colEventInId.Name = "colEventInId";
            colEventInId.ReadOnly = true;
            colEventInId.Visible = false;
            colEventInId.Width = 113;
            // 
            // colInvoicePendingId
            // 
            colInvoicePendingId.HeaderText = "colInvoicePendingId";
            colInvoicePendingId.Name = "colInvoicePendingId";
            colInvoicePendingId.ReadOnly = true;
            colInvoicePendingId.Visible = false;
            colInvoicePendingId.Width = 171;
            // 
            // colInvoiceId
            // 
            colInvoiceId.HeaderText = "colInvoiceId";
            colInvoiceId.Name = "colInvoiceId";
            colInvoiceId.ReadOnly = true;
            colInvoiceId.Visible = false;
            colInvoiceId.Width = 112;
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
            // colPlateOut
            // 
            colPlateOut.HeaderText = "colPlateOut";
            colPlateOut.Name = "colPlateOut";
            colPlateOut.ReadOnly = true;
            colPlateOut.Width = 126;
            // 
            // colTimeIn
            // 
            colTimeIn.HeaderText = "colTimeIn";
            colTimeIn.Name = "colTimeIn";
            colTimeIn.ReadOnly = true;
            colTimeIn.Width = 113;
            // 
            // colTimeOut
            // 
            colTimeOut.HeaderText = "colTimeOut";
            colTimeOut.Name = "colTimeOut";
            colTimeOut.ReadOnly = true;
            colTimeOut.Width = 126;
            // 
            // colParkingTime
            // 
            colParkingTime.HeaderText = "colParkingTime";
            colParkingTime.Name = "colParkingTime";
            colParkingTime.ReadOnly = true;
            colParkingTime.Width = 153;
            // 
            // colAccessKeyCollection
            // 
            colAccessKeyCollection.HeaderText = "colAccessKeyCollection";
            colAccessKeyCollection.Name = "colAccessKeyCollection";
            colAccessKeyCollection.ReadOnly = true;
            colAccessKeyCollection.Width = 214;
            // 
            // colFee
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            colFee.DefaultCellStyle = dataGridViewCellStyle4;
            colFee.HeaderText = "colFee";
            colFee.Name = "colFee";
            colFee.ReadOnly = true;
            colFee.Width = 89;
            // 
            // colDiscount
            // 
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            colDiscount.DefaultCellStyle = dataGridViewCellStyle5;
            colDiscount.HeaderText = "colDiscount";
            colDiscount.Name = "colDiscount";
            colDiscount.ReadOnly = true;
            colDiscount.Width = 127;
            // 
            // colPaid
            // 
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPaid.DefaultCellStyle = dataGridViewCellStyle6;
            colPaid.HeaderText = "colPaid";
            colPaid.Name = "colPaid";
            colPaid.ReadOnly = true;
            colPaid.Width = 94;
            // 
            // colRealFee
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            colRealFee.DefaultCellStyle = dataGridViewCellStyle7;
            colRealFee.HeaderText = "colRealFee";
            colRealFee.Name = "colRealFee";
            colRealFee.ReadOnly = true;
            colRealFee.Width = 120;
            // 
            // colAccessKeyCode
            // 
            colAccessKeyCode.HeaderText = "colAccessKeyCode";
            colAccessKeyCode.Name = "colAccessKeyCode";
            colAccessKeyCode.ReadOnly = true;
            colAccessKeyCode.Width = 179;
            // 
            // colAccessKeyName
            // 
            colAccessKeyName.HeaderText = "colAccessKeyName";
            colAccessKeyName.Name = "colAccessKeyName";
            colAccessKeyName.ReadOnly = true;
            colAccessKeyName.Width = 183;
            // 
            // colUserIn
            // 
            colUserIn.HeaderText = "colUserIn";
            colUserIn.Name = "colUserIn";
            colUserIn.ReadOnly = true;
            colUserIn.Width = 110;
            // 
            // colUserOut
            // 
            colUserOut.HeaderText = "colUserOut";
            colUserOut.Name = "colUserOut";
            colUserOut.ReadOnly = true;
            colUserOut.Width = 123;
            // 
            // colInvoiceTemplate
            // 
            colInvoiceTemplate.HeaderText = "colInvoiceTemplate";
            colInvoiceTemplate.Name = "colInvoiceTemplate";
            colInvoiceTemplate.ReadOnly = true;
            colInvoiceTemplate.Width = 183;
            // 
            // colInvoiceNo
            // 
            colInvoiceNo.HeaderText = "colInvoiceNo";
            colInvoiceNo.Name = "colInvoiceNo";
            colInvoiceNo.ReadOnly = true;
            colInvoiceNo.Width = 138;
            // 
            // colLaneIn
            // 
            colLaneIn.HeaderText = "colLaneIn";
            colLaneIn.Name = "colLaneIn";
            colLaneIn.ReadOnly = true;
            colLaneIn.Width = 111;
            // 
            // colLaneOut
            // 
            colLaneOut.HeaderText = "colLaneOut";
            colLaneOut.Name = "colLaneOut";
            colLaneOut.ReadOnly = true;
            colLaneOut.Width = 124;
            // 
            // colNote
            // 
            colNote.HeaderText = "colNote";
            colNote.Name = "colNote";
            colNote.ReadOnly = true;
            // 
            // colFileKeyIn
            // 
            colFileKeyIn.HeaderText = "colFileKeyIn";
            colFileKeyIn.Name = "colFileKeyIn";
            colFileKeyIn.ReadOnly = true;
            colFileKeyIn.Visible = false;
            colFileKeyIn.Width = 129;
            // 
            // colFileKeyOut
            // 
            colFileKeyOut.HeaderText = "colFileKeyOut";
            colFileKeyOut.Name = "colFileKeyOut";
            colFileKeyOut.ReadOnly = true;
            colFileKeyOut.Visible = false;
            colFileKeyOut.Width = 142;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(picOverviewImageOut, 0, 0);
            tableLayoutPanel2.Controls.Add(picVehicleImageOut, 0, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(250, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(250, 484);
            tableLayoutPanel2.TabIndex = 53;
            // 
            // picOverviewImageOut
            // 
            picOverviewImageOut.BackColor = Color.White;
            picOverviewImageOut.BorderStyle = BorderStyle.FixedSingle;
            picOverviewImageOut.DisplayImage = null;
            picOverviewImageOut.Dock = DockStyle.Fill;
            picOverviewImageOut.Location = new Point(0, 0);
            picOverviewImageOut.Margin = new Padding(0);
            picOverviewImageOut.Name = "picOverviewImageOut";
            picOverviewImageOut.Size = new Size(250, 242);
            picOverviewImageOut.SizeMode = PictureBoxSizeMode.StretchImage;
            picOverviewImageOut.TabIndex = 30;
            picOverviewImageOut.TabStop = false;
            toolTip1.SetToolTip(picOverviewImageOut, "Kích đúp chuột để xem hình ảnh phóng to");
            picOverviewImageOut.LoadCompleted += Pic_LoadCompleted;
            // 
            // picVehicleImageOut
            // 
            picVehicleImageOut.BackColor = Color.White;
            picVehicleImageOut.BorderStyle = BorderStyle.FixedSingle;
            picVehicleImageOut.DisplayImage = null;
            picVehicleImageOut.Dock = DockStyle.Fill;
            picVehicleImageOut.Location = new Point(0, 242);
            picVehicleImageOut.Margin = new Padding(0);
            picVehicleImageOut.Name = "picVehicleImageOut";
            picVehicleImageOut.Size = new Size(250, 242);
            picVehicleImageOut.SizeMode = PictureBoxSizeMode.StretchImage;
            picVehicleImageOut.TabIndex = 29;
            picVehicleImageOut.TabStop = false;
            toolTip1.SetToolTip(picVehicleImageOut, "Kích đúp chuột để xem hình ảnh phóng to");
            picVehicleImageOut.LoadCompleted += Pic_LoadCompleted;
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
            ucNavigator1.Location = new Point(0, 440);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.MinimumSize = new Size(0, 44);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1032, 44);
            ucNavigator1.TabIndex = 54;
            ucNavigator1.Visible = false;
            // 
            // lblTotalEvents
            // 
            lblTotalEvents.AutoSize = true;
            lblTotalEvents.BackColor = Color.Transparent;
            lblTotalEvents.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalEvents.ForeColor = Color.FromArgb(253, 149, 40);
            lblTotalEvents.Location = new Point(8, 200);
            lblTotalEvents.Margin = new Padding(0);
            lblTotalEvents.Name = "lblTotalEvents";
            lblTotalEvents.Size = new Size(627, 25);
            lblTotalEvents.TabIndex = 56;
            lblTotalEvents.Text = "Tổng số sự kiện : Tổng phí gửi xe : Giảm trừ             : Thực thu             :";
            lblTotalEvents.Visible = false;
            // 
            // tablePic
            // 
            tablePic.ColumnCount = 2;
            tablePic.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tablePic.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tablePic.Controls.Add(tableLayoutPanel1, 0, 0);
            tablePic.Controls.Add(tableLayoutPanel2, 1, 0);
            tablePic.Dock = DockStyle.Fill;
            tablePic.Location = new Point(0, 0);
            tablePic.Margin = new Padding(0);
            tablePic.Name = "tablePic";
            tablePic.RowCount = 1;
            tablePic.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tablePic.Size = new Size(500, 484);
            tablePic.TabIndex = 60;
            // 
            // toolTip1
            // 
            toolTip1.IsBalloon = true;
            // 
            // cbIdentityGroup
            // 
            cbIdentityGroup.BackColor = Color.Transparent;
            cbIdentityGroup.BorderRadius = 8;
            cbIdentityGroup.CustomizableEdges = customizableEdges1;
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
            cbIdentityGroup.Location = new Point(480, 104);
            cbIdentityGroup.Margin = new Padding(0);
            cbIdentityGroup.Name = "cbIdentityGroup";
            cbIdentityGroup.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbIdentityGroup.Size = new Size(512, 36);
            cbIdentityGroup.TabIndex = 11;
            // 
            // lblKeywordTitle
            // 
            lblKeywordTitle.AutoSize = true;
            lblKeywordTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblKeywordTitle.Location = new Point(8, 8);
            lblKeywordTitle.Margin = new Padding(0);
            lblKeywordTitle.Name = "lblKeywordTitle";
            lblKeywordTitle.Size = new Size(68, 21);
            lblKeywordTitle.TabIndex = 76;
            lblKeywordTitle.Text = "Từ khóa";
            // 
            // cbLane
            // 
            cbLane.BackColor = Color.Transparent;
            cbLane.BorderRadius = 8;
            cbLane.CustomizableEdges = customizableEdges3;
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
            cbLane.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbLane.Size = new Size(248, 36);
            cbLane.TabIndex = 5;
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
            cbUser.Location = new Point(744, 32);
            cbUser.Margin = new Padding(0);
            cbUser.Name = "cbUser";
            cbUser.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbUser.Size = new Size(248, 36);
            cbUser.TabIndex = 6;
            // 
            // txtKeyword
            // 
            txtKeyword.BorderRadius = 8;
            txtKeyword.CustomizableEdges = customizableEdges7;
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
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtKeyword.Size = new Size(456, 36);
            txtKeyword.TabIndex = 4;
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
            cbVehicleType.Location = new Point(1008, 104);
            cbVehicleType.Margin = new Padding(0);
            cbVehicleType.Name = "cbVehicleType";
            cbVehicleType.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbVehicleType.Size = new Size(280, 36);
            cbVehicleType.TabIndex = 12;
            // 
            // lblStartTimeTitle
            // 
            lblStartTimeTitle.AutoSize = true;
            lblStartTimeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStartTimeTitle.Location = new Point(8, 80);
            lblStartTimeTitle.Margin = new Padding(0);
            lblStartTimeTitle.Name = "lblStartTimeTitle";
            lblStartTimeTitle.Size = new Size(65, 21);
            lblStartTimeTitle.TabIndex = 77;
            lblStartTimeTitle.Text = "Bắt đầu";
            // 
            // lblEndTimeTitle
            // 
            lblEndTimeTitle.AutoSize = true;
            lblEndTimeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblEndTimeTitle.Location = new Point(248, 80);
            lblEndTimeTitle.Margin = new Padding(0);
            lblEndTimeTitle.Name = "lblEndTimeTitle";
            lblEndTimeTitle.Size = new Size(71, 21);
            lblEndTimeTitle.TabIndex = 78;
            lblEndTimeTitle.Text = "Kết thúc";
            // 
            // lblVehicleTypeTitle
            // 
            lblVehicleTypeTitle.AutoSize = true;
            lblVehicleTypeTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblVehicleTypeTitle.Location = new Point(1008, 80);
            lblVehicleTypeTitle.Margin = new Padding(0);
            lblVehicleTypeTitle.Name = "lblVehicleTypeTitle";
            lblVehicleTypeTitle.Size = new Size(61, 21);
            lblVehicleTypeTitle.TabIndex = 79;
            lblVehicleTypeTitle.Text = "Loại xe";
            // 
            // lblLaneTitle
            // 
            lblLaneTitle.AutoSize = true;
            lblLaneTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblLaneTitle.Location = new Point(480, 8);
            lblLaneTitle.Margin = new Padding(0);
            lblLaneTitle.Name = "lblLaneTitle";
            lblLaneTitle.Size = new Size(56, 21);
            lblLaneTitle.TabIndex = 80;
            lblLaneTitle.Text = "Làn xe";
            // 
            // lblUserTitle
            // 
            lblUserTitle.AutoSize = true;
            lblUserTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblUserTitle.Location = new Point(744, 8);
            lblUserTitle.Margin = new Padding(0);
            lblUserTitle.Name = "lblUserTitle";
            lblUserTitle.Size = new Size(98, 21);
            lblUserTitle.TabIndex = 81;
            lblUserTitle.Text = "Người dùng";
            // 
            // lblAccessKeyCollectionTitle
            // 
            lblAccessKeyCollectionTitle.AutoSize = true;
            lblAccessKeyCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblAccessKeyCollectionTitle.Location = new Point(480, 80);
            lblAccessKeyCollectionTitle.Margin = new Padding(0);
            lblAccessKeyCollectionTitle.Name = "lblAccessKeyCollectionTitle";
            lblAccessKeyCollectionTitle.Size = new Size(131, 21);
            lblAccessKeyCollectionTitle.TabIndex = 82;
            lblAccessKeyCollectionTitle.Text = "Nhóm định danh";
            // 
            // btnPrintPhieuThu
            // 
            btnPrintPhieuThu.BackColor = Color.White;
            btnPrintPhieuThu.BorderColor = Color.FromArgb(41, 97, 27);
            btnPrintPhieuThu.BorderRadius = 8;
            btnPrintPhieuThu.BorderThickness = 1;
            btnPrintPhieuThu.CustomizableEdges = customizableEdges11;
            btnPrintPhieuThu.DisabledState.BorderColor = Color.DarkGray;
            btnPrintPhieuThu.DisabledState.CustomBorderColor = Color.DarkGray;
            btnPrintPhieuThu.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnPrintPhieuThu.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnPrintPhieuThu.FillColor = Color.White;
            btnPrintPhieuThu.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnPrintPhieuThu.ForeColor = Color.FromArgb(41, 97, 27);
            btnPrintPhieuThu.Location = new Point(160, 152);
            btnPrintPhieuThu.Margin = new Padding(0);
            btnPrintPhieuThu.Name = "btnPrintPhieuThu";
            btnPrintPhieuThu.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnPrintPhieuThu.Size = new Size(142, 40);
            btnPrintPhieuThu.TabIndex = 1;
            btnPrintPhieuThu.Text = "In Phiếu Thu";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges13;
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
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnCancel.Size = new Size(142, 40);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Đóng";
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
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.BorderColor = Color.FromArgb(41, 97, 27);
            btnSearch.BorderRadius = 8;
            btnSearch.BorderThickness = 1;
            btnSearch.CustomizableEdges = customizableEdges17;
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
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges18;
            btnSearch.Size = new Size(142, 40);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "Tìm Kiếm";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(8, 240);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dgvData);
            splitContainer1.Panel1.Controls.Add(ucNavigator1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tablePic);
            splitContainer1.Size = new Size(1536, 484);
            splitContainer1.SplitterDistance = 1032;
            splitContainer1.TabIndex = 93;
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
            lblCustomerCollectionTitle.TabIndex = 79;
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
            // FrmReportInOut
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1550, 729);
            Controls.Add(splitContainer1);
            Controls.Add(btnPrintPhieuThu);
            Controls.Add(btnCancel);
            Controls.Add(btnExportExcel);
            Controls.Add(btnSearch);
            Controls.Add(lblKeywordTitle);
            Controls.Add(cbLane);
            Controls.Add(cbUser);
            Controls.Add(txtKeyword);
            Controls.Add(cbCustomerGroup);
            Controls.Add(cbVehicleType);
            Controls.Add(lblStartTimeTitle);
            Controls.Add(lblEndTimeTitle);
            Controls.Add(lblCustomerCollectionTitle);
            Controls.Add(lblVehicleTypeTitle);
            Controls.Add(lblLaneTitle);
            Controls.Add(lblUserTitle);
            Controls.Add(lblAccessKeyCollectionTitle);
            Controls.Add(lblTotalEvents);
            Controls.Add(cbIdentityGroup);
            Controls.Add(dtpEndTime);
            Controls.Add(dtpStartTime);
            Font = new Font("Segoe UI", 12F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmReportInOut";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Danh Sách Xe Đã Ra";
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picOverviewImageIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageIn).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picOverviewImageOut).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVehicleImageOut).EndInit();
            tablePic.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private MovablePictureBox picOverviewImageIn;
        private MovablePictureBox picVehicleImageIn;
        private DataGridView dgvData;
        private TableLayoutPanel tableLayoutPanel2;
        private MovablePictureBox picOverviewImageOut;
        private MovablePictureBox picVehicleImageOut;
        private ucNavigator ucNavigator1;
        private KzLabel lblTotalEvents;
        private TableLayoutPanel tablePic;
        private ToolTip toolTip1;
        private Guna.UI2.WinForms.Guna2ComboBox cbIdentityGroup;
        private KzLabel lblKeywordTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbLane;
        private Guna.UI2.WinForms.Guna2ComboBox cbUser;
        private Guna.UI2.WinForms.Guna2TextBox txtKeyword;
        private Guna.UI2.WinForms.Guna2ComboBox cbVehicleType;
        private KzLabel lblStartTimeTitle;
        private KzLabel lblEndTimeTitle;
        private KzLabel lblVehicleTypeTitle;
        private KzLabel lblLaneTitle;
        private KzLabel lblUserTitle;
        private KzLabel lblAccessKeyCollectionTitle;
        private KzButton btnPrintPhieuThu;
        private KzButton btnCancel;
        private KzButton btnExportExcel;
        private KzButton btnSearch;
        private SplitContainer splitContainer1;
        private DateTimePicker dtpStartTime;
        private DateTimePicker dtpEndTime;
        private KzLabel lblCustomerCollectionTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbCustomerGroup;
        private DataGridViewTextBoxColumn colEventOutId;
        private DataGridViewTextBoxColumn colEventInId;
        private DataGridViewTextBoxColumn colInvoicePendingId;
        private DataGridViewTextBoxColumn colInvoiceId;
        private DataGridViewTextBoxColumn colIndex;
        private DataGridViewTextBoxColumn colPlateIn;
        private DataGridViewTextBoxColumn colPlateOut;
        private DataGridViewTextBoxColumn colTimeIn;
        private DataGridViewTextBoxColumn colTimeOut;
        private DataGridViewTextBoxColumn colParkingTime;
        private DataGridViewTextBoxColumn colAccessKeyCollection;
        private DataGridViewTextBoxColumn colFee;
        private DataGridViewTextBoxColumn colDiscount;
        private DataGridViewTextBoxColumn colPaid;
        private DataGridViewTextBoxColumn colRealFee;
        private DataGridViewTextBoxColumn colAccessKeyCode;
        private DataGridViewTextBoxColumn colAccessKeyName;
        private DataGridViewTextBoxColumn colUserIn;
        private DataGridViewTextBoxColumn colUserOut;
        private DataGridViewTextBoxColumn colInvoiceTemplate;
        private DataGridViewTextBoxColumn colInvoiceNo;
        private DataGridViewTextBoxColumn colLaneIn;
        private DataGridViewTextBoxColumn colLaneOut;
        private DataGridViewTextBoxColumn colNote;
        private DataGridViewTextBoxColumn colFileKeyIn;
        private DataGridViewTextBoxColumn colFileKeyOut;
    }
}