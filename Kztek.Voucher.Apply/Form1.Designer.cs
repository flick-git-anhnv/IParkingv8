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
            this.components = new System.ComponentModel.Container();
            this.txtAccessKeyCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnApplyVoucher = new System.Windows.Forms.Button();
            this.picSetting = new System.Windows.Forms.PictureBox();
            this.timerAuth = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAccessKeyCode
            // 
            this.txtAccessKeyCode.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtAccessKeyCode.Location = new System.Drawing.Point(176, 58);
            this.txtAccessKeyCode.Name = "txtAccessKeyCode";
            this.txtAccessKeyCode.Size = new System.Drawing.Size(280, 32);
            this.txtAccessKeyCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thông tin thẻ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Voucher áp dụng:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(483, 32);
            this.label3.TabIndex = 1;
            this.label3.Text = "Quản Lý Áp Dụng Giảm Giá";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "_";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(8, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Enter: Xác Nhận";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(8, 249);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "ESC: Làm mới";
            // 
            // btnApplyVoucher
            // 
            this.btnApplyVoucher.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnApplyVoucher.Location = new System.Drawing.Point(176, 168);
            this.btnApplyVoucher.Name = "btnApplyVoucher";
            this.btnApplyVoucher.Size = new System.Drawing.Size(280, 40);
            this.btnApplyVoucher.TabIndex = 4;
            this.btnApplyVoucher.Text = "Xác Nhận";
            this.btnApplyVoucher.UseVisualStyleBackColor = true;
            // 
            // picSetting
            // 
            this.picSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSetting.Image = global::Kztek.Voucher.Apply.Properties.Resources.icons8_admin_settings_male_32px_1;
            this.picSetting.Location = new System.Drawing.Point(491, 8);
            this.picSetting.Name = "picSetting";
            this.picSetting.Size = new System.Drawing.Size(36, 32);
            this.picSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSetting.TabIndex = 2;
            this.picSetting.TabStop = false;
            // 
            // timerAuth
            // 
            this.timerAuth.Interval = 1000;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(539, 274);
            this.Controls.Add(this.btnApplyVoucher);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picSetting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccessKeyCode);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Áp dụng vouhcer";
            ((System.ComponentModel.ISupportInitialize)(this.picSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAccessKeyCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnApplyVoucher;
        private System.Windows.Forms.Timer timerAuth;
    }
}

