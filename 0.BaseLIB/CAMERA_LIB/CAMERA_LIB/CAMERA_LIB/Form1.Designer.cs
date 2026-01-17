
namespace CAMERA_LIB
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
            this.anvPlayer1 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer2 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer3 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer4 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer5 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer6 = new Kztek.Cameras.AnvPlayer();
            this.btnSnap1 = new System.Windows.Forms.Button();
            this.btnSnap2 = new System.Windows.Forms.Button();
            this.btnSnap3 = new System.Windows.Forms.Button();
            this.btnSnap6 = new System.Windows.Forms.Button();
            this.btnSnap4 = new System.Windows.Forms.Button();
            this.btnSnap5 = new System.Windows.Forms.Button();
            this.picSnap = new System.Windows.Forms.PictureBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.anvPlayer7 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer8 = new Kztek.Cameras.AnvPlayer();
            this.anvPlayer9 = new Kztek.Cameras.AnvPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.picSnap)).BeginInit();
            this.SuspendLayout();
            // 
            // anvPlayer1
            // 
            this.anvPlayer1.Location = new System.Drawing.Point(12, 12);
            this.anvPlayer1.Name = "anvPlayer1";
            this.anvPlayer1.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer1.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer1.TabIndex = 0;
            // 
            // anvPlayer2
            // 
            this.anvPlayer2.Location = new System.Drawing.Point(251, 12);
            this.anvPlayer2.Name = "anvPlayer2";
            this.anvPlayer2.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer2.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer2.TabIndex = 0;
            // 
            // anvPlayer3
            // 
            this.anvPlayer3.Location = new System.Drawing.Point(490, 12);
            this.anvPlayer3.Name = "anvPlayer3";
            this.anvPlayer3.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer3.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer3.TabIndex = 0;
            // 
            // anvPlayer4
            // 
            this.anvPlayer4.Location = new System.Drawing.Point(12, 269);
            this.anvPlayer4.Name = "anvPlayer4";
            this.anvPlayer4.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer4.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer4.TabIndex = 0;
            // 
            // anvPlayer5
            // 
            this.anvPlayer5.Location = new System.Drawing.Point(251, 269);
            this.anvPlayer5.Name = "anvPlayer5";
            this.anvPlayer5.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer5.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer5.TabIndex = 0;
            // 
            // anvPlayer6
            // 
            this.anvPlayer6.Location = new System.Drawing.Point(490, 269);
            this.anvPlayer6.Name = "anvPlayer6";
            this.anvPlayer6.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer6.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer6.TabIndex = 0;
            // 
            // btnSnap1
            // 
            this.btnSnap1.Location = new System.Drawing.Point(729, 12);
            this.btnSnap1.Name = "btnSnap1";
            this.btnSnap1.Size = new System.Drawing.Size(103, 34);
            this.btnSnap1.TabIndex = 1;
            this.btnSnap1.Text = "Snap1";
            this.btnSnap1.UseVisualStyleBackColor = true;
            this.btnSnap1.Click += new System.EventHandler(this.btnSnap1_Click);
            // 
            // btnSnap2
            // 
            this.btnSnap2.Location = new System.Drawing.Point(838, 12);
            this.btnSnap2.Name = "btnSnap2";
            this.btnSnap2.Size = new System.Drawing.Size(103, 34);
            this.btnSnap2.TabIndex = 1;
            this.btnSnap2.Text = "Snap2";
            this.btnSnap2.UseVisualStyleBackColor = true;
            this.btnSnap2.Click += new System.EventHandler(this.btnSnap2_Click);
            // 
            // btnSnap3
            // 
            this.btnSnap3.Location = new System.Drawing.Point(947, 12);
            this.btnSnap3.Name = "btnSnap3";
            this.btnSnap3.Size = new System.Drawing.Size(103, 34);
            this.btnSnap3.TabIndex = 1;
            this.btnSnap3.Text = "Snap3";
            this.btnSnap3.UseVisualStyleBackColor = true;
            this.btnSnap3.Click += new System.EventHandler(this.btnSnap3_Click);
            // 
            // btnSnap6
            // 
            this.btnSnap6.Location = new System.Drawing.Point(947, 52);
            this.btnSnap6.Name = "btnSnap6";
            this.btnSnap6.Size = new System.Drawing.Size(103, 34);
            this.btnSnap6.TabIndex = 1;
            this.btnSnap6.Text = "Snap6";
            this.btnSnap6.UseVisualStyleBackColor = true;
            this.btnSnap6.Click += new System.EventHandler(this.btnSnap6_Click);
            // 
            // btnSnap4
            // 
            this.btnSnap4.Location = new System.Drawing.Point(729, 52);
            this.btnSnap4.Name = "btnSnap4";
            this.btnSnap4.Size = new System.Drawing.Size(103, 34);
            this.btnSnap4.TabIndex = 1;
            this.btnSnap4.Text = "Snap4";
            this.btnSnap4.UseVisualStyleBackColor = true;
            this.btnSnap4.Click += new System.EventHandler(this.btnSnap4_Click);
            // 
            // btnSnap5
            // 
            this.btnSnap5.Location = new System.Drawing.Point(838, 52);
            this.btnSnap5.Name = "btnSnap5";
            this.btnSnap5.Size = new System.Drawing.Size(103, 34);
            this.btnSnap5.TabIndex = 1;
            this.btnSnap5.Text = "Snap5";
            this.btnSnap5.UseVisualStyleBackColor = true;
            this.btnSnap5.Click += new System.EventHandler(this.btnSnap5_Click);
            // 
            // picSnap
            // 
            this.picSnap.Location = new System.Drawing.Point(729, 89);
            this.picSnap.Name = "picSnap";
            this.picSnap.Size = new System.Drawing.Size(426, 309);
            this.picSnap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSnap.TabIndex = 2;
            this.picSnap.TabStop = false;
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(1056, 71);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(12, 15);
            this.lblResolution.TabIndex = 3;
            this.lblResolution.Text = "_";
            // 
            // anvPlayer7
            // 
            this.anvPlayer7.Location = new System.Drawing.Point(12, 513);
            this.anvPlayer7.Name = "anvPlayer7";
            this.anvPlayer7.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer7.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer7.TabIndex = 0;
            // 
            // anvPlayer8
            // 
            this.anvPlayer8.Location = new System.Drawing.Point(251, 513);
            this.anvPlayer8.Name = "anvPlayer8";
            this.anvPlayer8.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer8.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer8.TabIndex = 0;
            // 
            // anvPlayer9
            // 
            this.anvPlayer9.Location = new System.Drawing.Point(490, 513);
            this.anvPlayer9.Name = "anvPlayer9";
            this.anvPlayer9.RtspURL = "rtsp://admin:Kztek123456@192.168.21.195:554/1/1";
            this.anvPlayer9.Size = new System.Drawing.Size(233, 238);
            this.anvPlayer9.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 720);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.picSnap);
            this.Controls.Add(this.btnSnap5);
            this.Controls.Add(this.btnSnap4);
            this.Controls.Add(this.btnSnap6);
            this.Controls.Add(this.btnSnap3);
            this.Controls.Add(this.btnSnap2);
            this.Controls.Add(this.btnSnap1);
            this.Controls.Add(this.anvPlayer9);
            this.Controls.Add(this.anvPlayer6);
            this.Controls.Add(this.anvPlayer8);
            this.Controls.Add(this.anvPlayer5);
            this.Controls.Add(this.anvPlayer7);
            this.Controls.Add(this.anvPlayer4);
            this.Controls.Add(this.anvPlayer3);
            this.Controls.Add(this.anvPlayer2);
            this.Controls.Add(this.anvPlayer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picSnap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Kztek.Cameras.AnvPlayer anvPlayer1;
        private Kztek.Cameras.AnvPlayer anvPlayer2;
        private Kztek.Cameras.AnvPlayer anvPlayer3;
        private Kztek.Cameras.AnvPlayer anvPlayer4;
        private Kztek.Cameras.AnvPlayer anvPlayer5;
        private Kztek.Cameras.AnvPlayer anvPlayer6;
        private System.Windows.Forms.Button btnSnap1;
        private System.Windows.Forms.Button btnSnap2;
        private System.Windows.Forms.Button btnSnap3;
        private System.Windows.Forms.Button btnSnap6;
        private System.Windows.Forms.Button btnSnap4;
        private System.Windows.Forms.Button btnSnap5;
        private System.Windows.Forms.PictureBox picSnap;
        private System.Windows.Forms.Label lblResolution;
        private Kztek.Cameras.AnvPlayer anvPlayer7;
        private Kztek.Cameras.AnvPlayer anvPlayer8;
        private Kztek.Cameras.AnvPlayer anvPlayer9;
    }
}

