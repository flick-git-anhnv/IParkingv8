namespace Futech.App
{
    partial class frmMain
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
            this.btnVideo = new System.Windows.Forms.Button();
            this.btnLpr = new System.Windows.Forms.Button();
            this.btnObjects = new System.Windows.Forms.Button();
            this.btnSocket = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtComPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVideo
            // 
            this.btnVideo.Location = new System.Drawing.Point(12, 12);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(124, 39);
            this.btnVideo.TabIndex = 0;
            this.btnVideo.Text = "VIDEO";
            this.btnVideo.UseVisualStyleBackColor = true;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnLpr
            // 
            this.btnLpr.Location = new System.Drawing.Point(12, 57);
            this.btnLpr.Name = "btnLpr";
            this.btnLpr.Size = new System.Drawing.Size(124, 39);
            this.btnLpr.TabIndex = 1;
            this.btnLpr.Text = "LPR";
            this.btnLpr.UseVisualStyleBackColor = true;
            this.btnLpr.Click += new System.EventHandler(this.btnLpr_Click);
            // 
            // btnObjects
            // 
            this.btnObjects.Location = new System.Drawing.Point(12, 102);
            this.btnObjects.Name = "btnObjects";
            this.btnObjects.Size = new System.Drawing.Size(124, 39);
            this.btnObjects.TabIndex = 2;
            this.btnObjects.Text = "OBJECTS";
            this.btnObjects.UseVisualStyleBackColor = true;
            this.btnObjects.Click += new System.EventHandler(this.btnObjects_Click);
            // 
            // btnSocket
            // 
            this.btnSocket.Location = new System.Drawing.Point(12, 147);
            this.btnSocket.Name = "btnSocket";
            this.btnSocket.Size = new System.Drawing.Size(124, 39);
            this.btnSocket.TabIndex = 3;
            this.btnSocket.Text = "Socket";
            this.btnSocket.UseVisualStyleBackColor = true;
            this.btnSocket.Click += new System.EventHandler(this.btnSocket_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(158, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(377, 147);
            this.listBox1.TabIndex = 4;
            // 
            // txtComPort
            // 
            this.txtComPort.Location = new System.Drawing.Point(158, 12);
            this.txtComPort.Name = "txtComPort";
            this.txtComPort.Size = new System.Drawing.Size(100, 20);
            this.txtComPort.TabIndex = 5;
            this.txtComPort.Text = "COM16";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(264, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(345, 10);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(426, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 220);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtComPort);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnSocket);
            this.Controls.Add(this.btnObjects);
            this.Controls.Add(this.btnLpr);
            this.Controls.Add(this.btnVideo);
            this.Name = "frmMain";
            this.Text = "FUTECH DEMO APPLICATION ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVideo;
        private System.Windows.Forms.Button btnLpr;
        private System.Windows.Forms.Button btnObjects;
        private System.Windows.Forms.Button btnSocket;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtComPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnClear;
    }
}