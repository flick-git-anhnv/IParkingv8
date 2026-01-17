namespace Kztek.Voucher.Apply
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtAccessKeyCode = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblAppliedVoucherCar = new Label();
            label5 = new Label();
            label6 = new Label();
            btnApplyVoucher = new Button();
            picSetting = new PictureBox();
            timerAuth = new System.Windows.Forms.Timer(components);
            lblAppliedVoucherMotor = new Label();
            label7 = new Label();
            lblAppliedVoucherOther = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)picSetting).BeginInit();
            SuspendLayout();
            // 
            // txtAccessKeyCode
            // 
            txtAccessKeyCode.Font = new Font("Segoe UI", 14F);
            txtAccessKeyCode.Location = new Point(205, 67);
            txtAccessKeyCode.Margin = new Padding(4, 3, 4, 3);
            txtAccessKeyCode.Name = "txtAccessKeyCode";
            txtAccessKeyCode.Size = new Size(326, 32);
            txtAccessKeyCode.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(37, 74);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(102, 21);
            label1.TabIndex = 1;
            label1.Text = "Thông tin thẻ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(37, 115);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(101, 21);
            label2.TabIndex = 1;
            label2.Text = "Voucher ô tô:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            label3.ForeColor = Color.DarkBlue;
            label3.Location = new Point(9, 9);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(564, 37);
            label3.TabIndex = 1;
            label3.Text = "Quản Lý Áp Dụng Giảm Giá";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAppliedVoucherCar
            // 
            lblAppliedVoucherCar.AutoSize = true;
            lblAppliedVoucherCar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAppliedVoucherCar.Location = new Point(205, 115);
            lblAppliedVoucherCar.Margin = new Padding(4, 0, 4, 0);
            lblAppliedVoucherCar.Name = "lblAppliedVoucherCar";
            lblAppliedVoucherCar.Size = new Size(17, 21);
            lblAppliedVoucherCar.TabIndex = 1;
            lblAppliedVoucherCar.Text = "_";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.DarkBlue;
            label5.Location = new Point(9, 295);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(121, 21);
            label5.TabIndex = 3;
            label5.Text = "Enter: Xác Nhận";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.DarkBlue;
            label6.Location = new Point(9, 322);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(104, 21);
            label6.TabIndex = 3;
            label6.Text = "ESC: Làm mới";
            // 
            // btnApplyVoucher
            // 
            btnApplyVoucher.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold);
            btnApplyVoucher.Location = new Point(208, 224);
            btnApplyVoucher.Margin = new Padding(4, 3, 4, 3);
            btnApplyVoucher.Name = "btnApplyVoucher";
            btnApplyVoucher.Size = new Size(327, 46);
            btnApplyVoucher.TabIndex = 4;
            btnApplyVoucher.Text = "Xác Nhận";
            btnApplyVoucher.UseVisualStyleBackColor = true;
            // 
            // picSetting
            // 
            picSetting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picSetting.Image = (Image)resources.GetObject("picSetting.Image");
            picSetting.Location = new Point(573, 9);
            picSetting.Margin = new Padding(4, 3, 4, 3);
            picSetting.Name = "picSetting";
            picSetting.Size = new Size(42, 37);
            picSetting.SizeMode = PictureBoxSizeMode.CenterImage;
            picSetting.TabIndex = 2;
            picSetting.TabStop = false;
            // 
            // timerAuth
            // 
            timerAuth.Interval = 1000;
            // 
            // lblAppliedVoucherMotor
            // 
            lblAppliedVoucherMotor.AutoSize = true;
            lblAppliedVoucherMotor.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAppliedVoucherMotor.Location = new Point(208, 144);
            lblAppliedVoucherMotor.Margin = new Padding(4, 0, 4, 0);
            lblAppliedVoucherMotor.Name = "lblAppliedVoucherMotor";
            lblAppliedVoucherMotor.Size = new Size(17, 21);
            lblAppliedVoucherMotor.TabIndex = 1;
            lblAppliedVoucherMotor.Text = "_";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(40, 144);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(123, 21);
            label7.TabIndex = 1;
            label7.Text = "Voucher xe máy:";
            // 
            // lblAppliedVoucherOther
            // 
            lblAppliedVoucherOther.AutoSize = true;
            lblAppliedVoucherOther.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAppliedVoucherOther.Location = new Point(208, 176);
            lblAppliedVoucherOther.Margin = new Padding(4, 0, 4, 0);
            lblAppliedVoucherOther.Name = "lblAppliedVoucherOther";
            lblAppliedVoucherOther.Size = new Size(17, 21);
            lblAppliedVoucherOther.TabIndex = 1;
            lblAppliedVoucherOther.Text = "_";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(40, 176);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(154, 21);
            label9.TabIndex = 1;
            label9.Text = "Voucher loại xe khác:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(629, 351);
            Controls.Add(btnApplyVoucher);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(picSetting);
            Controls.Add(label9);
            Controls.Add(label7);
            Controls.Add(lblAppliedVoucherOther);
            Controls.Add(label2);
            Controls.Add(lblAppliedVoucherMotor);
            Controls.Add(label3);
            Controls.Add(lblAppliedVoucherCar);
            Controls.Add(label1);
            Controls.Add(txtAccessKeyCode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Áp dụng voucher";
            ((System.ComponentModel.ISupportInitialize)picSetting).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAccessKeyCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAppliedVoucherCar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnApplyVoucher;
        private System.Windows.Forms.Timer timerAuth;
        private Label lblAppliedVoucherMotor;
        private Label label7;
        private Label lblAppliedVoucherOther;
        private Label label9;
    }
}

