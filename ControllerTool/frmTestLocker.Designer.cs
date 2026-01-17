namespace ControllerTool
{
    partial class frmTestLocker
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
            TestLocker = new Label();
            btnStart = new Button();
            txtLockerDelay = new TextBox();
            txtLockerIndex = new TextBox();
            txtlockerIp = new TextBox();
            dgvResponse = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            btnStop = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvResponse).BeginInit();
            SuspendLayout();
            // 
            // TestLocker
            // 
            TestLocker.AutoSize = true;
            TestLocker.Location = new Point(8, 8);
            TestLocker.Name = "TestLocker";
            TestLocker.Size = new Size(17, 15);
            TestLocker.TabIndex = 8;
            TestLocker.Text = "IP";
            TestLocker.Click += TestLocker_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(600, 32);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(104, 23);
            btnStart.TabIndex = 3;
            btnStart.Text = "Gửi";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += button1_Click;
            // 
            // txtLockerDelay
            // 
            txtLockerDelay.Location = new Point(456, 32);
            txtLockerDelay.Name = "txtLockerDelay";
            txtLockerDelay.Size = new Size(136, 23);
            txtLockerDelay.TabIndex = 2;
            txtLockerDelay.Text = "1000";
            // 
            // txtLockerIndex
            // 
            txtLockerIndex.Location = new Point(232, 32);
            txtLockerIndex.Name = "txtLockerIndex";
            txtLockerIndex.Size = new Size(216, 23);
            txtLockerIndex.TabIndex = 1;
            txtLockerIndex.Text = "1,2,3,4,5,6,7";
            // 
            // txtlockerIp
            // 
            txtlockerIp.Location = new Point(8, 32);
            txtlockerIp.Name = "txtlockerIp";
            txtlockerIp.Size = new Size(216, 23);
            txtlockerIp.TabIndex = 0;
            txtlockerIp.Text = "192.168.21.147";
            // 
            // dgvResponse
            // 
            dgvResponse.AllowUserToAddRows = false;
            dgvResponse.AllowUserToDeleteRows = false;
            dgvResponse.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResponse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResponse.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResponse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResponse.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dgvResponse.Location = new Point(8, 64);
            dgvResponse.Name = "dgvResponse";
            dgvResponse.ReadOnly = true;
            dgvResponse.RowHeadersVisible = false;
            dgvResponse.Size = new Size(784, 376);
            dgvResponse.TabIndex = 9;
            // 
            // Column1
            // 
            Column1.HeaderText = "Thời gian";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 82;
            // 
            // Column2
            // 
            Column2.HeaderText = "Cửa";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 53;
            // 
            // Column3
            // 
            Column3.HeaderText = "Kết quả";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 72;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(232, 8);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 8;
            label1.Text = "Cửa";
            label1.Click += TestLocker_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(456, 8);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 8;
            label2.Text = "Thời gian chờ";
            label2.Click += TestLocker_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(712, 32);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(80, 23);
            btnStop.TabIndex = 4;
            btnStop.Text = "Dừng";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // frmTestLocker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvResponse);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TestLocker);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtLockerDelay);
            Controls.Add(txtLockerIndex);
            Controls.Add(txtlockerIp);
            Name = "frmTestLocker";
            Text = "frmTestLocker";
            ((System.ComponentModel.ISupportInitialize)dgvResponse).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TestLocker;
        private Button btnStart;
        private TextBox txtLockerDelay;
        private TextBox txtLockerIndex;
        private TextBox txtlockerIp;
        private DataGridView dgvResponse;
        private Label label1;
        private Label label2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button btnStop;
    }
}