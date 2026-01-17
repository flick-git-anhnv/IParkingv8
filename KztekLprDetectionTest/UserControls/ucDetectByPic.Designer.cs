namespace KztekLprDetectionTest.UserControls
{
    partial class ucDetectByPic
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
            picOutput = new PictureBox();
            lblResult = new Label();
            kryptonTrackBar1 = new TrackBar();
            trackbarRotate = new TrackBar();
            picInput = new PictureBox();
            kryptonNumericUpDown1 = new NumericUpDown();
            numRotate = new NumericUpDown();
            label4 = new Label();
            label6 = new Label();
            label5 = new Label();
            label3 = new Label();
            chbIsCar = new CheckBox();
            kryptonButton1 = new Button();
            btnDraw = new Button();
            btnChooseImage = new Button();
            kryptonHeaderGroup1 = new Panel();
            kryptonHeaderGroup4 = new Panel();
            btnSimpleLPR2 = new Button();
            btnSimpleLPR3 = new Button();
            ((System.ComponentModel.ISupportInitialize)picOutput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonTrackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackbarRotate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonNumericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRotate).BeginInit();
            SuspendLayout();
            // 
            // picOutput
            // 
            picOutput.Location = new Point(552, 195);
            picOutput.Margin = new Padding(4, 3, 4, 3);
            picOutput.Name = "picOutput";
            picOutput.Size = new Size(456, 288);
            picOutput.SizeMode = PictureBoxSizeMode.Zoom;
            picOutput.TabIndex = 23;
            picOutput.TabStop = false;
            // 
            // lblResult
            // 
            lblResult.BackColor = Color.Navy;
            lblResult.Font = new Font("Segoe UI", 14F);
            lblResult.ForeColor = Color.White;
            lblResult.Location = new Point(8, 515);
            lblResult.Margin = new Padding(0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(1016, 37);
            lblResult.TabIndex = 19;
            lblResult.Text = "-";
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // kryptonTrackBar1
            // 
            kryptonTrackBar1.Location = new Point(192, 58);
            kryptonTrackBar1.Maximum = 360;
            kryptonTrackBar1.Name = "kryptonTrackBar1";
            kryptonTrackBar1.Size = new Size(336, 45);
            kryptonTrackBar1.TabIndex = 34;
            // 
            // trackbarRotate
            // 
            trackbarRotate.Location = new Point(192, 8);
            trackbarRotate.Maximum = 360;
            trackbarRotate.Name = "trackbarRotate";
            trackbarRotate.Size = new Size(336, 45);
            trackbarRotate.TabIndex = 33;
            trackbarRotate.ValueChanged += trackbarRotate_ValueChanged;
            // 
            // picInput
            // 
            picInput.Location = new Point(24, 195);
            picInput.Margin = new Padding(4, 3, 4, 3);
            picInput.Name = "picInput";
            picInput.Size = new Size(456, 288);
            picInput.SizeMode = PictureBoxSizeMode.Zoom;
            picInput.TabIndex = 24;
            picInput.TabStop = false;
            // 
            // kryptonNumericUpDown1
            // 
            kryptonNumericUpDown1.Location = new Point(536, 51);
            kryptonNumericUpDown1.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            kryptonNumericUpDown1.Name = "kryptonNumericUpDown1";
            kryptonNumericUpDown1.Size = new Size(88, 29);
            kryptonNumericUpDown1.TabIndex = 26;
            // 
            // numRotate
            // 
            numRotate.Location = new Point(536, 1);
            numRotate.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numRotate.Name = "numRotate";
            numRotate.Size = new Size(88, 29);
            numRotate.TabIndex = 25;
            numRotate.ValueChanged += numRotate_ValueChanged;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(8, 53);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(196, 37);
            label4.TabIndex = 22;
            label4.Text = "Phóng to / thu nhỏ";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(552, 163);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(456, 29);
            label6.TabIndex = 18;
            label6.Text = "Ảnh biển số";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(24, 163);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(456, 29);
            label5.TabIndex = 21;
            label5.Text = "Ảnh nhận diện";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(8, 3);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(72, 37);
            label3.TabIndex = 20;
            label3.Text = "Xoay";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chbIsCar
            // 
            chbIsCar.Location = new Point(632, 48);
            chbIsCar.Name = "chbIsCar";
            chbIsCar.Size = new Size(80, 37);
            chbIsCar.TabIndex = 29;
            chbIsCar.Text = "Ô tô";
            // 
            // kryptonButton1
            // 
            kryptonButton1.Cursor = Cursors.Hand;
            kryptonButton1.Location = new Point(536, 104);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.Size = new Size(144, 40);
            kryptonButton1.TabIndex = 30;
            kryptonButton1.Text = "LprAI";
            kryptonButton1.Click += btnDetectFromImage_Click;
            // 
            // btnDraw
            // 
            btnDraw.Cursor = Cursors.Hand;
            btnDraw.Location = new Point(272, 104);
            btnDraw.Name = "btnDraw";
            btnDraw.Size = new Size(256, 40);
            btnDraw.TabIndex = 31;
            btnDraw.Text = "Vẽ vùng nhận dạng";
            btnDraw.Click += btnDraw_Click;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Cursor = Cursors.Hand;
            btnChooseImage.Location = new Point(16, 104);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(248, 40);
            btnChooseImage.TabIndex = 32;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // kryptonHeaderGroup1
            // 
            kryptonHeaderGroup1.Location = new Point(8, 155);
            kryptonHeaderGroup1.Margin = new Padding(0);
            kryptonHeaderGroup1.Name = "kryptonHeaderGroup1";
            kryptonHeaderGroup1.Size = new Size(488, 344);
            kryptonHeaderGroup1.TabIndex = 27;
            // 
            // kryptonHeaderGroup4
            // 
            kryptonHeaderGroup4.Location = new Point(536, 155);
            kryptonHeaderGroup4.Margin = new Padding(0);
            kryptonHeaderGroup4.Name = "kryptonHeaderGroup4";
            kryptonHeaderGroup4.Size = new Size(488, 344);
            kryptonHeaderGroup4.TabIndex = 28;
            // 
            // btnSimpleLPR2
            // 
            btnSimpleLPR2.Cursor = Cursors.Hand;
            btnSimpleLPR2.Location = new Point(688, 104);
            btnSimpleLPR2.Name = "btnSimpleLPR2";
            btnSimpleLPR2.Size = new Size(144, 40);
            btnSimpleLPR2.TabIndex = 30;
            btnSimpleLPR2.Text = "LPR2";
            btnSimpleLPR2.Click += btnSimpleLPR2_Click;
            // 
            // btnSimpleLPR3
            // 
            btnSimpleLPR3.Cursor = Cursors.Hand;
            btnSimpleLPR3.Location = new Point(840, 104);
            btnSimpleLPR3.Name = "btnSimpleLPR3";
            btnSimpleLPR3.Size = new Size(144, 40);
            btnSimpleLPR3.TabIndex = 30;
            btnSimpleLPR3.Text = "LPR3";
            btnSimpleLPR3.Click += btnSimpleLPR3_Click;
            // 
            // ucDetectByPic
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            Controls.Add(picOutput);
            Controls.Add(lblResult);
            Controls.Add(kryptonTrackBar1);
            Controls.Add(trackbarRotate);
            Controls.Add(picInput);
            Controls.Add(kryptonNumericUpDown1);
            Controls.Add(numRotate);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(chbIsCar);
            Controls.Add(btnSimpleLPR3);
            Controls.Add(btnSimpleLPR2);
            Controls.Add(kryptonButton1);
            Controls.Add(btnDraw);
            Controls.Add(btnChooseImage);
            Controls.Add(kryptonHeaderGroup1);
            Controls.Add(kryptonHeaderGroup4);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "ucDetectByPic";
            Size = new Size(1033, 561);
            ((System.ComponentModel.ISupportInitialize)picOutput).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonTrackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackbarRotate).EndInit();
            ((System.ComponentModel.ISupportInitialize)picInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonNumericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRotate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picOutput;
        private Label lblResult;
        private TrackBar kryptonTrackBar1;
        private TrackBar trackbarRotate;
        private PictureBox picInput;
        private NumericUpDown kryptonNumericUpDown1;
        private NumericUpDown numRotate;
        private Label label4;
        private Label label6;
        private Label label5;
        private Label label3;
        private CheckBox chbIsCar;
        private Button kryptonButton1;
        private Button btnDraw;
        private Button btnChooseImage;
        private Panel kryptonHeaderGroup1;
        private Panel kryptonHeaderGroup4;
        private Button btnSimpleLPR2;
        private Button btnSimpleLPR3;
    }
}
