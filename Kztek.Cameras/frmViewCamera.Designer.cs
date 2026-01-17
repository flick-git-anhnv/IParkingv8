namespace Kztek.Cameras
{
    partial class frmViewCamera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewCamera));
            lblVideoSize = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            txtIP = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            btnOpenWebview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblVideoSize
            // 
            lblVideoSize.Dock = System.Windows.Forms.DockStyle.Right;
            lblVideoSize.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblVideoSize.Location = new System.Drawing.Point(798, 0);
            lblVideoSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblVideoSize.Name = "lblVideoSize";
            lblVideoSize.Size = new System.Drawing.Size(128, 40);
            lblVideoSize.TabIndex = 8;
            lblVideoSize.Text = "640x480";
            lblVideoSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox1.Location = new System.Drawing.Point(0, 40);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(926, 543);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // txtIP
            // 
            txtIP.Dock = System.Windows.Forms.DockStyle.Fill;
            txtIP.Font = new System.Drawing.Font("Segoe UI", 12F);
            txtIP.Location = new System.Drawing.Point(136, 0);
            txtIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            txtIP.Name = "txtIP";
            txtIP.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            txtIP.Size = new System.Drawing.Size(662, 40);
            txtIP.TabIndex = 8;
            txtIP.Text = "127.0.0.1";
            txtIP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtIP);
            panel1.Controls.Add(btnOpenWebview);
            panel1.Controls.Add(lblVideoSize);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(926, 40);
            panel1.TabIndex = 9;
            // 
            // btnOpenWebview
            // 
            btnOpenWebview.AutoSize = true;
            btnOpenWebview.Dock = System.Windows.Forms.DockStyle.Left;
            btnOpenWebview.Font = new System.Drawing.Font("Segoe UI", 12F);
            btnOpenWebview.Location = new System.Drawing.Point(0, 0);
            btnOpenWebview.Name = "btnOpenWebview";
            btnOpenWebview.Size = new System.Drawing.Size(136, 40);
            btnOpenWebview.TabIndex = 9;
            btnOpenWebview.Text = "Mở Webview";
            btnOpenWebview.UseVisualStyleBackColor = true;
            btnOpenWebview.Click += btnOpenWebview_Click;
            // 
            // frmViewCamera
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(926, 583);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "frmViewCamera";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Camera View";
            FormClosing += frmViewCamera_FormClosing;
            Load += frmViewCamera_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblVideoSize;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label txtIP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenWebview;
    }
}