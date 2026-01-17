using Kztek.Control8.Controls;

namespace IParkingv8.Forms.DataForms
{
    partial class FrmSelectCard
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectCard));
            lblGuide = new Label();
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            btnSearch = new KzButton();
            dgvData = new DataGridView();
            colSTT = new DataGridViewTextBoxColumn();
            col_access_key_name = new DataGridViewTextBoxColumn();
            col_access_key_code = new DataGridViewTextBoxColumn();
            col_access_key_type = new DataGridViewTextBoxColumn();
            col_access_key_status = new DataGridViewTextBoxColumn();
            col_access_key_note = new DataGridViewTextBoxColumn();
            col_access_key_collection = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            ucNavigator1 = new Kztek.Control8.UserControls.ReportUcs.ucNavigator();
            cbStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            cbCollection = new Guna.UI2.WinForms.Guna2ComboBox();
            lblStatusTitle = new Label();
            lblCollectionTitle = new Label();
            lblKeywordTitle = new Label();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // lblGuide
            // 
            lblGuide.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblGuide.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGuide.ForeColor = Color.FromArgb(255, 128, 0);
            lblGuide.Location = new Point(8, 571);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(448, 45);
            lblGuide.TabIndex = 8;
            lblGuide.Text = "Enter để tìm kiếm.\r\nKích đúp chuột hoặc bấm Xác Nhận để chọn định danh.";
            lblGuide.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges1;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(900, 571);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 9;
            btnConfirm.Text = "Xác Nhận";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges3;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(730, 571);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(150, 48);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Đóng";
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.White;
            btnSearch.BorderColor = Color.FromArgb(41, 97, 27);
            btnSearch.BorderRadius = 8;
            btnSearch.BorderThickness = 1;
            btnSearch.CustomizableEdges = customizableEdges5;
            btnSearch.DisabledState.BorderColor = Color.DarkGray;
            btnSearch.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSearch.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSearch.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSearch.FillColor = Color.FromArgb(41, 97, 27);
            btnSearch.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(8, 72);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSearch.Size = new Size(150, 40);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Tìm Kiếm";
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
            dgvData.Columns.AddRange(new DataGridViewColumn[] { colSTT, col_access_key_name, col_access_key_code, col_access_key_type, col_access_key_status, col_access_key_note, col_access_key_collection, colId });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            dgvData.EnableHeadersVisualStyles = false;
            dgvData.GridColor = Color.FromArgb(221, 238, 255);
            dgvData.Location = new Point(8, 120);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 40;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(1044, 394);
            dgvData.TabIndex = 13;
            // 
            // colSTT
            // 
            colSTT.HeaderText = "STT";
            colSTT.Name = "colSTT";
            colSTT.ReadOnly = true;
            colSTT.Width = 69;
            // 
            // col_access_key_name
            // 
            col_access_key_name.HeaderText = "Tên";
            col_access_key_name.Name = "col_access_key_name";
            col_access_key_name.ReadOnly = true;
            col_access_key_name.Width = 67;
            // 
            // col_access_key_code
            // 
            col_access_key_code.HeaderText = "Mã";
            col_access_key_code.Name = "col_access_key_code";
            col_access_key_code.ReadOnly = true;
            col_access_key_code.Width = 65;
            // 
            // col_access_key_type
            // 
            col_access_key_type.HeaderText = "Loại";
            col_access_key_type.Name = "col_access_key_type";
            col_access_key_type.ReadOnly = true;
            col_access_key_type.Width = 72;
            // 
            // col_access_key_status
            // 
            col_access_key_status.HeaderText = "Trạng thái";
            col_access_key_status.Name = "col_access_key_status";
            col_access_key_status.ReadOnly = true;
            col_access_key_status.Width = 114;
            // 
            // col_access_key_note
            // 
            col_access_key_note.HeaderText = "Ghi chú";
            col_access_key_note.Name = "col_access_key_note";
            col_access_key_note.ReadOnly = true;
            col_access_key_note.Width = 96;
            // 
            // col_access_key_collection
            // 
            col_access_key_collection.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_access_key_collection.HeaderText = "Nhóm";
            col_access_key_collection.Name = "col_access_key_collection";
            col_access_key_collection.ReadOnly = true;
            // 
            // colId
            // 
            colId.HeaderText = "Id";
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            colId.Width = 57;
            // 
            // ucNavigator1
            // 
            ucNavigator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ucNavigator1.BackColor = Color.White;
            ucNavigator1.KZUI_CurrentPage = 0;
            ucNavigator1.KZUI_MaxPage = 0;
            ucNavigator1.KZUI_TotalRecord = 0;
            ucNavigator1.Location = new Point(8, 514);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1045, 49);
            ucNavigator1.TabIndex = 14;
            // 
            // cbStatus
            // 
            cbStatus.BackColor = Color.Transparent;
            cbStatus.BorderRadius = 8;
            cbStatus.CustomizableEdges = customizableEdges7;
            cbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStatus.FocusedColor = Color.FromArgb(41, 97, 27);
            cbStatus.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            cbStatus.Font = new Font("Segoe UI", 12F);
            cbStatus.ForeColor = Color.Black;
            cbStatus.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            cbStatus.ItemHeight = 30;
            cbStatus.ItemsAppearance.SelectedBackColor = Color.FromArgb(41, 97, 27);
            cbStatus.ItemsAppearance.SelectedForeColor = Color.White;
            cbStatus.Location = new Point(704, 30);
            cbStatus.Margin = new Padding(0);
            cbStatus.Name = "cbStatus";
            cbStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbStatus.Size = new Size(344, 36);
            cbStatus.TabIndex = 79;
            // 
            // cbCollection
            // 
            cbCollection.BackColor = Color.Transparent;
            cbCollection.BorderRadius = 8;
            cbCollection.CustomizableEdges = customizableEdges9;
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
            cbCollection.Location = new Point(344, 30);
            cbCollection.Margin = new Padding(0);
            cbCollection.Name = "cbCollection";
            cbCollection.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbCollection.Size = new Size(344, 36);
            cbCollection.TabIndex = 80;
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStatusTitle.Location = new Point(704, 6);
            lblStatusTitle.Margin = new Padding(0);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(82, 21);
            lblStatusTitle.TabIndex = 77;
            lblStatusTitle.Text = "Trạng thái";
            // 
            // lblCollectionTitle
            // 
            lblCollectionTitle.AutoSize = true;
            lblCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCollectionTitle.Location = new Point(344, 6);
            lblCollectionTitle.Margin = new Padding(0);
            lblCollectionTitle.Name = "lblCollectionTitle";
            lblCollectionTitle.Size = new Size(131, 21);
            lblCollectionTitle.TabIndex = 78;
            lblCollectionTitle.Text = "Nhóm định danh";
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
            // txtKeyword
            // 
            txtKeyword.BorderRadius = 8;
            txtKeyword.CustomizableEdges = customizableEdges11;
            txtKeyword.DefaultText = "";
            txtKeyword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtKeyword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtKeyword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtKeyword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtKeyword.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            txtKeyword.Font = new Font("Segoe UI", 12F);
            txtKeyword.ForeColor = Color.Black;
            txtKeyword.HoverState.BorderColor = Color.FromArgb(41, 97, 27);
            txtKeyword.Location = new Point(8, 30);
            txtKeyword.Margin = new Padding(4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderForeColor = Color.Gray;
            txtKeyword.PlaceholderText = "Mã | Tên | Ghi chú";
            txtKeyword.SelectedText = "";
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtKeyword.Size = new Size(320, 36);
            txtKeyword.TabIndex = 75;
            // 
            // FrmSelectCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1062, 625);
            Controls.Add(cbStatus);
            Controls.Add(cbCollection);
            Controls.Add(lblStatusTitle);
            Controls.Add(lblCollectionTitle);
            Controls.Add(lblKeywordTitle);
            Controls.Add(txtKeyword);
            Controls.Add(ucNavigator1);
            Controls.Add(dgvData);
            Controls.Add(btnSearch);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            Controls.Add(lblGuide);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "FrmSelectCard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lựa chọn định danh";
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblGuide;
        private KzButton btnConfirm;
        private KzButton btnCancel;
        private KzButton btnSearch;
        private DataGridView dgvData;
        private Kztek.Control8.UserControls.ReportUcs.ucNavigator ucNavigator1;
        private Guna.UI2.WinForms.Guna2ComboBox cbStatus;
        private Guna.UI2.WinForms.Guna2ComboBox cbCollection;
        private Label lblStatusTitle;
        private Label lblCollectionTitle;
        private Label lblKeywordTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtKeyword;
        private DataGridViewTextBoxColumn colSTT;
        private DataGridViewTextBoxColumn col_access_key_name;
        private DataGridViewTextBoxColumn col_access_key_code;
        private DataGridViewTextBoxColumn col_access_key_type;
        private DataGridViewTextBoxColumn col_access_key_status;
        private DataGridViewTextBoxColumn col_access_key_note;
        private DataGridViewTextBoxColumn col_access_key_collection;
        private DataGridViewTextBoxColumn colId;
    }
}