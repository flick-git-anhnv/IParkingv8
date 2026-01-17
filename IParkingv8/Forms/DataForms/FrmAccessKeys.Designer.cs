using Kztek.Control8.Controls;

namespace IParkingv8.Forms.DataForms
{
    partial class FrmAccessKeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAccessKeys));
            btnCancel = new KzButton();
            btnSearch = new KzButton();
            txtKeyword = new Guna.UI2.WinForms.Guna2TextBox();
            lblKeywordTitle = new Label();
            dgvData = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            col_access_key_name = new DataGridViewTextBoxColumn();
            col_access_key_code = new DataGridViewTextBoxColumn();
            col_access_key_type = new DataGridViewTextBoxColumn();
            col_access_key_status = new DataGridViewTextBoxColumn();
            col_access_key_note = new DataGridViewTextBoxColumn();
            col_access_key_collection = new DataGridViewTextBoxColumn();
            col_access_key_id = new DataGridViewTextBoxColumn();
            ucNavigator1 = new Kztek.Control8.UserControls.ReportUcs.ucNavigator();
            cbCollection = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCollectionTitle = new Label();
            lblStatusTitle = new Label();
            cbStatus = new Guna.UI2.WinForms.Guna2ComboBox();
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
            txtKeyword.Location = new Point(8, 30);
            txtKeyword.Margin = new Padding(4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderForeColor = Color.Gray;
            txtKeyword.PlaceholderText = "Mã | Tên | Ghi chú";
            txtKeyword.SelectedText = "";
            txtKeyword.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtKeyword.Size = new Size(320, 36);
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
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column5, col_access_key_name, col_access_key_code, col_access_key_type, col_access_key_status, col_access_key_note, col_access_key_collection, col_access_key_id });
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
            dgvData.Location = new Point(8, 136);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 40;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(1044, 424);
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
            col_access_key_status.HeaderText = "Trạng Thái";
            col_access_key_status.Name = "col_access_key_status";
            col_access_key_status.ReadOnly = true;
            col_access_key_status.Width = 117;
            // 
            // col_access_key_note
            // 
            col_access_key_note.HeaderText = "Ghi Chú";
            col_access_key_note.Name = "col_access_key_note";
            col_access_key_note.ReadOnly = true;
            col_access_key_note.Width = 98;
            // 
            // col_access_key_collection
            // 
            col_access_key_collection.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_access_key_collection.HeaderText = "Nhóm";
            col_access_key_collection.Name = "col_access_key_collection";
            col_access_key_collection.ReadOnly = true;
            // 
            // col_access_key_id
            // 
            col_access_key_id.HeaderText = "Id";
            col_access_key_id.Name = "col_access_key_id";
            col_access_key_id.ReadOnly = true;
            col_access_key_id.Visible = false;
            col_access_key_id.Width = 57;
            // 
            // ucNavigator1
            // 
            ucNavigator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ucNavigator1.BackColor = Color.White;
            ucNavigator1.KZUI_CurrentPage = 0;
            ucNavigator1.KZUI_MaxPage = 0;
            ucNavigator1.KZUI_TotalRecord = 0;
            ucNavigator1.Location = new Point(8, 576);
            ucNavigator1.Margin = new Padding(0);
            ucNavigator1.Name = "ucNavigator1";
            ucNavigator1.Size = new Size(1045, 43);
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
            cbCollection.Location = new Point(344, 30);
            cbCollection.Margin = new Padding(0);
            cbCollection.Name = "cbCollection";
            cbCollection.ShadowDecoration.CustomizableEdges = customizableEdges8;
            cbCollection.Size = new Size(344, 36);
            cbCollection.TabIndex = 3;
            // 
            // lblCollectionTitle
            // 
            lblCollectionTitle.AutoSize = true;
            lblCollectionTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCollectionTitle.Location = new Point(344, 6);
            lblCollectionTitle.Margin = new Padding(0);
            lblCollectionTitle.Name = "lblCollectionTitle";
            lblCollectionTitle.Size = new Size(131, 21);
            lblCollectionTitle.TabIndex = 73;
            lblCollectionTitle.Text = "Nhóm định danh";
            // 
            // lblStatusTitle
            // 
            lblStatusTitle.AutoSize = true;
            lblStatusTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStatusTitle.Location = new Point(704, 6);
            lblStatusTitle.Margin = new Padding(0);
            lblStatusTitle.Name = "lblStatusTitle";
            lblStatusTitle.Size = new Size(82, 21);
            lblStatusTitle.TabIndex = 73;
            lblStatusTitle.Text = "Trạng thái";
            // 
            // cbStatus
            // 
            cbStatus.BackColor = Color.Transparent;
            cbStatus.BorderRadius = 8;
            cbStatus.CustomizableEdges = customizableEdges9;
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
            cbStatus.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbStatus.Size = new Size(344, 36);
            cbStatus.TabIndex = 4;
            // 
            // FrmAccessKeys
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1062, 625);
            Controls.Add(cbStatus);
            Controls.Add(cbCollection);
            Controls.Add(lblStatusTitle);
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
            Name = "FrmAccessKeys";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Danh Sách Định Danh";
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
        private Guna.UI2.WinForms.Guna2ComboBox cbCollection;
        private Label lblCollectionTitle;
        private Label lblStatusTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbStatus;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn col_access_key_name;
        private DataGridViewTextBoxColumn col_access_key_code;
        private DataGridViewTextBoxColumn col_access_key_type;
        private DataGridViewTextBoxColumn col_access_key_status;
        private DataGridViewTextBoxColumn col_access_key_note;
        private DataGridViewTextBoxColumn col_access_key_collection;
        private DataGridViewTextBoxColumn col_access_key_id;
    }
}