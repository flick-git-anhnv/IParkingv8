namespace KztekLprDetectionTest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            btnDetect = new TabPage();
            ucDetectByPic1 = new KztekLprDetectionTest.UserControls.ucDetectByPic();
            tabPage2 = new TabPage();
            ucDetectByPath1 = new KztekLprDetectionTest.UserControls.ucDetectByPath();
            tabControl1.SuspendLayout();
            btnDetect.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(btnDetect);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1042, 603);
            tabControl1.TabIndex = 18;
            // 
            // btnDetect
            // 
            btnDetect.Controls.Add(ucDetectByPic1);
            btnDetect.Location = new Point(4, 30);
            btnDetect.Name = "btnDetect";
            btnDetect.Padding = new Padding(3);
            btnDetect.Size = new Size(1034, 569);
            btnDetect.TabIndex = 0;
            btnDetect.Text = "Nhận dạng từ hình ảnh";
            btnDetect.UseVisualStyleBackColor = true;
            // 
            // ucDetectByPic1
            // 
            ucDetectByPic1.BackColor = SystemColors.ButtonHighlight;
            ucDetectByPic1.Dock = DockStyle.Fill;
            ucDetectByPic1.Font = new Font("Segoe UI", 12F);
            ucDetectByPic1.Location = new Point(3, 3);
            ucDetectByPic1.Margin = new Padding(4);
            ucDetectByPic1.Name = "ucDetectByPic1";
            ucDetectByPic1.Size = new Size(1028, 563);
            ucDetectByPic1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ucDetectByPath1);
            tabPage2.Font = new Font("Segoe UI", 9F);
            tabPage2.Location = new Point(4, 30);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1034, 569);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Nhận dạng từ folder";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucDetectByPath1
            // 
            ucDetectByPath1.BackColor = Color.White;
            ucDetectByPath1.Dock = DockStyle.Fill;
            ucDetectByPath1.Location = new Point(3, 3);
            ucDetectByPath1.Name = "ucDetectByPath1";
            ucDetectByPath1.Size = new Size(1028, 563);
            ucDetectByPath1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1042, 603);
            Controls.Add(tabControl1);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            btnDetect.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private TabControl tabControl1;
        private TabPage btnDetect;
        private TabPage tabPage2;
        private UserControls.ucDetectByPic ucDetectByPic1;
        private UserControls.ucDetectByPath ucDetectByPath1;
    }
}
