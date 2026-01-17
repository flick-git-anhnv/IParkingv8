namespace KztekLprDetectionTest.UserControls
{
    partial class ucDetectByPath
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            txtInputPath = new TextBox();
            dgvData = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            kryptonHeaderGroup2 = new Panel();
            txtOutputPath = new TextBox();
            label1 = new Label();
            btnStopTrain = new Button();
            label2 = new Label();
            btnStartTraining = new Button();
            btnSaveError = new Button();
            picOutput = new PictureBox();
            picInput = new PictureBox();
            label6 = new Label();
            label5 = new Label();
            kryptonHeaderGroup1 = new Panel();
            kryptonHeaderGroup4 = new Panel();
            chbIsCar = new CheckBox();
            numRotate = new NumericUpDown();
            label3 = new Label();
            label4 = new Label();
            txtUrl = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRotate).BeginInit();
            SuspendLayout();
            // 
            // txtInputPath
            // 
            txtInputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtInputPath.Location = new Point(96, 39);
            txtInputPath.Margin = new Padding(0);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.Size = new Size(504, 23);
            txtInputPath.TabIndex = 19;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvData.BackgroundColor = SystemColors.ButtonHighlight;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { Column1, Column4, Column5, Column3, Column7, Column8, Column6, Column2 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new Padding(3);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle3;
            dgvData.Location = new Point(24, 216);
            dgvData.Margin = new Padding(4, 3, 4, 3);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersVisible = false;
            dgvData.RowTemplate.Height = 29;
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.Size = new Size(568, 336);
            dgvData.TabIndex = 18;
            // 
            // Column1
            // 
            Column1.HeaderText = "STT";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 58;
            // 
            // Column4
            // 
            Column4.HeaderText = "rootPath";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Visible = false;
            Column4.Width = 84;
            // 
            // Column5
            // 
            Column5.HeaderText = "resultPath";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Visible = false;
            Column5.Width = 91;
            // 
            // Column3
            // 
            Column3.HeaderText = "Biển nhận diện";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 108;
            // 
            // Column7
            // 
            Column7.HeaderText = "Biển số hiệu chỉnh";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 97;
            // 
            // Column8
            // 
            Column8.HeaderText = "So sánh";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 74;
            // 
            // Column6
            // 
            Column6.HeaderText = "Thời gian xử lý";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 98;
            // 
            // Column2
            // 
            Column2.HeaderText = "Ảnh gốc";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 77;
            // 
            // kryptonHeaderGroup2
            // 
            kryptonHeaderGroup2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            kryptonHeaderGroup2.Location = new Point(8, 200);
            kryptonHeaderGroup2.Margin = new Padding(0);
            kryptonHeaderGroup2.Name = "kryptonHeaderGroup2";
            kryptonHeaderGroup2.Size = new Size(592, 368);
            kryptonHeaderGroup2.TabIndex = 21;
            // 
            // txtOutputPath
            // 
            txtOutputPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOutputPath.Location = new Point(96, 79);
            txtOutputPath.Margin = new Padding(0);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(504, 23);
            txtOutputPath.TabIndex = 20;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(8, 32);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(72, 37);
            label1.TabIndex = 16;
            label1.Text = "Input";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnStopTrain
            // 
            btnStopTrain.Cursor = Cursors.Hand;
            btnStopTrain.Location = new Point(176, 152);
            btnStopTrain.Name = "btnStopTrain";
            btnStopTrain.Size = new Size(160, 40);
            btnStopTrain.TabIndex = 25;
            btnStopTrain.Text = "Dừng training";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(8, 72);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(72, 37);
            label2.TabIndex = 17;
            label2.Text = "Output";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnStartTraining
            // 
            btnStartTraining.Cursor = Cursors.Hand;
            btnStartTraining.Location = new Point(8, 152);
            btnStartTraining.Name = "btnStartTraining";
            btnStartTraining.Size = new Size(160, 40);
            btnStartTraining.TabIndex = 23;
            btnStartTraining.Text = "Bắt đầu training";
            btnStartTraining.Click += btnStartTraining_Click_1;
            // 
            // btnSaveError
            // 
            btnSaveError.Cursor = Cursors.Hand;
            btnSaveError.Location = new Point(344, 152);
            btnSaveError.Name = "btnSaveError";
            btnSaveError.Size = new Size(168, 40);
            btnSaveError.TabIndex = 24;
            btnSaveError.Text = "Lưu ảnh sai";
            btnSaveError.Click += btnSaveErrorPic_Click;
            // 
            // picOutput
            // 
            picOutput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picOutput.Location = new Point(624, 328);
            picOutput.Margin = new Padding(4, 3, 4, 3);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(305, 225);
            picOutput.SizeMode = PictureBoxSizeMode.Zoom;
            picOutput.TabIndex = 31;
            picOutput.TabStop = false;
            // 
            // picInput
            // 
            picInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picInput.Location = new Point(624, 56);
            picInput.Margin = new Padding(4, 3, 4, 3);
            picInput.Name = "picInput";
            picInput.Size = new Size(305, 209);
            picInput.SizeMode = PictureBoxSizeMode.Zoom;
            picInput.TabIndex = 32;
            picInput.TabStop = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(624, 296);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(305, 25);
            label6.TabIndex = 29;
            label6.Text = "Ảnh biển số";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(624, 24);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(305, 24);
            label5.TabIndex = 30;
            label5.Text = "Ảnh nhận diện";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // kryptonHeaderGroup1
            // 
            kryptonHeaderGroup1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            kryptonHeaderGroup1.Location = new Point(608, 16);
            kryptonHeaderGroup1.Margin = new Padding(0);
            kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            kryptonHeaderGroup1.Size = new Size(337, 265);
            kryptonHeaderGroup1.TabIndex = 33;
            // 
            // kryptonHeaderGroup4
            // 
            kryptonHeaderGroup4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            kryptonHeaderGroup4.Location = new Point(608, 288);
            kryptonHeaderGroup4.Margin = new Padding(0);
            kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            kryptonHeaderGroup4.Size = new Size(337, 281);
            kryptonHeaderGroup4.TabIndex = 34;
            // 
            // chbIsCar
            // 
            chbIsCar.Location = new Point(224, 112);
            chbIsCar.Name = "chbIsCar";
            chbIsCar.Size = new Size(80, 24);
            chbIsCar.TabIndex = 35;
            // 
            // numRotate
            // 
            numRotate.Location = new Point(96, 113);
            numRotate.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numRotate.Name = "numRotate";
            numRotate.Size = new Size(88, 23);
            numRotate.TabIndex = 36;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(12, 106);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(72, 37);
            label3.TabIndex = 17;
            label3.Text = "Góc";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(8, 0);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(72, 37);
            label4.TabIndex = 16;
            label4.Text = "Url";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUrl
            // 
            txtUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUrl.Location = new Point(96, 7);
            txtUrl.Margin = new Padding(0);
            txtUrl.Name = "txtUrl";
            txtUrl.Size = new Size(504, 23);
            txtUrl.TabIndex = 19;
            // 
            // ucDetectByPath
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(numRotate);
            Controls.Add(chbIsCar);
            Controls.Add(picOutput);
            Controls.Add(picInput);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(kryptonHeaderGroup1);
            Controls.Add(kryptonHeaderGroup4);
            Controls.Add(txtUrl);
            Controls.Add(txtInputPath);
            Controls.Add(dgvData);
            Controls.Add(kryptonHeaderGroup2);
            Controls.Add(txtOutputPath);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(btnStopTrain);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnStartTraining);
            Controls.Add(btnSaveError);
            Name = "ucDetectByPath";
            Size = new Size(951, 573);
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOutput).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRotate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtInputPath;
        private DataGridView dgvData;
        private Panel kryptonHeaderGroup2;
        private TextBox txtOutputPath;
        private Label label1;
        private Button btnStopTrain;
        private Label label2;
        private Button btnStartTraining;
        private Button btnSaveError;
        private PictureBox picOutput;
        private PictureBox picInput;
        private Label label6;
        private Label label5;
        private Panel kryptonHeaderGroup1;
        private Panel kryptonHeaderGroup4;
        private CheckBox chbIsCar;
        private NumericUpDown numRotate;
        private Label label3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column2;
        private Label label4;
        private TextBox txtUrl;
    }
}
