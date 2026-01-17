using Kztek.Control8.Controls;

namespace Kztek.Control8.Forms
{
    partial class FrmTestController
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTestController));
            lblDevice = new KzLabel();
            lblControllerName = new Label();
            dgvEvent = new DataGridView();
            colTime = new DataGridViewTextBoxColumn();
            colEventType = new DataGridViewTextBoxColumn();
            colReaderOrLoop = new DataGridViewTextBoxColumn();
            colCardNumber = new DataGridViewTextBoxColumn();
            numRelay = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label2 = new KzLabel();
            lblEventList = new KzLabel();
            btnOpenBarrie = new KzButton();
            lblResult = new KzLabel();
            btnCollectCard = new KzButton();
            ((System.ComponentModel.ISupportInitialize)dgvEvent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRelay).BeginInit();
            SuspendLayout();
            // 
            // lblDevice
            // 
            lblDevice.AutoSize = true;
            lblDevice.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDevice.Location = new Point(8, 8);
            lblDevice.Name = "lblDevice";
            lblDevice.Size = new Size(73, 21);
            lblDevice.TabIndex = 0;
            lblDevice.Text = "Thiết bị: ";
            // 
            // lblControllerName
            // 
            lblControllerName.AutoSize = true;
            lblControllerName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblControllerName.Location = new Point(88, 8);
            lblControllerName.Name = "lblControllerName";
            lblControllerName.Size = new Size(17, 21);
            lblControllerName.TabIndex = 0;
            lblControllerName.Text = "_";
            // 
            // dgvEvent
            // 
            dgvEvent.AllowUserToAddRows = false;
            dgvEvent.AllowUserToDeleteRows = false;
            dgvEvent.AllowUserToResizeColumns = false;
            dgvEvent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 251, 255);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dgvEvent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvEvent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEvent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEvent.BackgroundColor = SystemColors.ButtonHighlight;
            dgvEvent.BorderStyle = BorderStyle.None;
            dgvEvent.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEvent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 11.75F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(24, 115, 204);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvEvent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvEvent.ColumnHeadersHeight = 40;
            dgvEvent.Columns.AddRange(new DataGridViewColumn[] { colTime, colEventType, colReaderOrLoop, colCardNumber });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvEvent.DefaultCellStyle = dataGridViewCellStyle3;
            dgvEvent.EnableHeadersVisualStyles = false;
            dgvEvent.GridColor = Color.FromArgb(221, 238, 255);
            dgvEvent.Location = new Point(8, 144);
            dgvEvent.Name = "dgvEvent";
            dgvEvent.ReadOnly = true;
            dgvEvent.RowHeadersVisible = false;
            dgvEvent.RowTemplate.Height = 40;
            dgvEvent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvent.Size = new Size(784, 296);
            dgvEvent.TabIndex = 14;
            // 
            // colTime
            // 
            colTime.HeaderText = "Thời Gian";
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            colTime.Width = 110;
            // 
            // colEventType
            // 
            colEventType.HeaderText = "Sự kiện";
            colEventType.Name = "colEventType";
            colEventType.ReadOnly = true;
            colEventType.Width = 95;
            // 
            // colReaderOrLoop
            // 
            colReaderOrLoop.HeaderText = "Đầu đọc/ Loop";
            colReaderOrLoop.Name = "colReaderOrLoop";
            colReaderOrLoop.ReadOnly = true;
            colReaderOrLoop.Width = 151;
            // 
            // colCardNumber
            // 
            colCardNumber.HeaderText = "Mã Thẻ";
            colCardNumber.Name = "colCardNumber";
            colCardNumber.ReadOnly = true;
            colCardNumber.Width = 96;
            // 
            // numRelay
            // 
            numRelay.BackColor = Color.Transparent;
            numRelay.BorderRadius = 8;
            numRelay.CustomizableEdges = customizableEdges1;
            numRelay.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numRelay.Font = new Font("Segoe UI", 12F);
            numRelay.Location = new Point(96, 40);
            numRelay.Margin = new Padding(0);
            numRelay.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numRelay.Name = "numRelay";
            numRelay.ShadowDecoration.CustomizableEdges = customizableEdges2;
            numRelay.Size = new Size(104, 36);
            numRelay.TabIndex = 15;
            numRelay.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numRelay.UpDownButtonForeColor = Color.White;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 48);
            label2.Name = "label2";
            label2.Size = new Size(53, 21);
            label2.TabIndex = 0;
            label2.Text = "Barrie";
            // 
            // lblEventList
            // 
            lblEventList.AutoSize = true;
            lblEventList.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEventList.Location = new Point(8, 112);
            lblEventList.Name = "lblEventList";
            lblEventList.Size = new Size(138, 21);
            lblEventList.TabIndex = 0;
            lblEventList.Text = "Danh sách sự kiện";
            // 
            // btnOpenBarrie
            // 
            btnOpenBarrie.BackColor = Color.White;
            btnOpenBarrie.BorderColor = Color.FromArgb(41, 97, 27);
            btnOpenBarrie.BorderRadius = 8;
            btnOpenBarrie.BorderThickness = 1;
            btnOpenBarrie.CustomizableEdges = customizableEdges3;
            btnOpenBarrie.DisabledState.BorderColor = Color.DarkGray;
            btnOpenBarrie.DisabledState.CustomBorderColor = Color.DarkGray;
            btnOpenBarrie.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnOpenBarrie.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnOpenBarrie.FillColor = Color.FromArgb(41, 97, 27);
            btnOpenBarrie.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnOpenBarrie.ForeColor = Color.White;
            btnOpenBarrie.Location = new Point(216, 38);
            btnOpenBarrie.Margin = new Padding(0);
            btnOpenBarrie.Name = "btnOpenBarrie";
            btnOpenBarrie.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnOpenBarrie.Size = new Size(128, 40);
            btnOpenBarrie.TabIndex = 16;
            btnOpenBarrie.Text = "Mở";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 12F);
            lblResult.Location = new Point(8, 80);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(17, 21);
            lblResult.TabIndex = 17;
            lblResult.Text = "_";
            lblResult.Visible = false;
            // 
            // btnCollectCard
            // 
            btnCollectCard.BackColor = Color.White;
            btnCollectCard.BorderColor = Color.FromArgb(41, 97, 27);
            btnCollectCard.BorderRadius = 8;
            btnCollectCard.BorderThickness = 1;
            btnCollectCard.CustomizableEdges = customizableEdges5;
            btnCollectCard.DisabledState.BorderColor = Color.DarkGray;
            btnCollectCard.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCollectCard.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCollectCard.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCollectCard.FillColor = Color.FromArgb(41, 97, 27);
            btnCollectCard.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCollectCard.ForeColor = Color.White;
            btnCollectCard.Location = new Point(352, 38);
            btnCollectCard.Margin = new Padding(0);
            btnCollectCard.Name = "btnCollectCard";
            btnCollectCard.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnCollectCard.Size = new Size(128, 40);
            btnCollectCard.TabIndex = 16;
            btnCollectCard.Text = "Thu thẻ";
            // 
            // FrmTestController
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(lblResult);
            Controls.Add(btnCollectCard);
            Controls.Add(btnOpenBarrie);
            Controls.Add(numRelay);
            Controls.Add(dgvEvent);
            Controls.Add(lblControllerName);
            Controls.Add(lblEventList);
            Controls.Add(label2);
            Controls.Add(lblDevice);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmTestController";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Test bộ điều khiển";
            ((System.ComponentModel.ISupportInitialize)dgvEvent).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRelay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KzLabel lblDevice;
        private Label lblControllerName;
        private DataGridView dgvEvent;
        private Guna.UI2.WinForms.Guna2NumericUpDown numRelay;
        private KzLabel label2;
        private KzLabel lblEventList;
        private KzButton btnOpenBarrie;
        private KzLabel lblResult;
        private KzButton btnCollectCard;
        private DataGridViewTextBoxColumn colTime;
        private DataGridViewTextBoxColumn colEventType;
        private DataGridViewTextBoxColumn colReaderOrLoop;
        private DataGridViewTextBoxColumn colCardNumber;
    }
}