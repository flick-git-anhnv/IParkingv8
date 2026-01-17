namespace Kztek.Cameras
{
    partial class IPCameraSettingPage
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
            this.cbxProtocol = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxVideoDecoder = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxChanelVideo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFrameRate = new System.Windows.Forms.ComboBox();
            this.cbxImageQuality = new System.Windows.Forms.ComboBox();
            this.labelImageQuality = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxImageResolution = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtVideoSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxStreamType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numHttpPort = new System.Windows.Forms.NumericUpDown();
            this.cbxChanelImg = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numServerPort = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numHttpPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxProtocol
            // 
            this.cbxProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProtocol.FormattingEnabled = true;
            this.cbxProtocol.Items.AddRange(new object[] {
            "HTTP",
            "RTSP",
            "RTP",
            "TCP",
            "UDP"});
            this.cbxProtocol.Location = new System.Drawing.Point(487, 110);
            this.cbxProtocol.Name = "cbxProtocol";
            this.cbxProtocol.Size = new System.Drawing.Size(163, 25);
            this.cbxProtocol.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 62;
            this.label4.Text = "Protocol";
            // 
            // cbxVideoDecoder
            // 
            this.cbxVideoDecoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxVideoDecoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideoDecoder.FormattingEnabled = true;
            this.cbxVideoDecoder.Items.AddRange(new object[] {
            "Default ( Manufacture)",
            "KztekSDK1",
            "KztekSDK2",
            "KztekSDK3"});
            this.cbxVideoDecoder.Location = new System.Drawing.Point(160, 238);
            this.cbxVideoDecoder.Name = "cbxVideoDecoder";
            this.cbxVideoDecoder.Size = new System.Drawing.Size(490, 25);
            this.cbxVideoDecoder.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 60;
            this.label5.Text = "Video Decoder";
            // 
            // cbxChanelVideo
            // 
            this.cbxChanelVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxChanelVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChanelVideo.FormattingEnabled = true;
            this.cbxChanelVideo.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.cbxChanelVideo.Location = new System.Drawing.Point(487, 142);
            this.cbxChanelVideo.Name = "cbxChanelVideo";
            this.cbxChanelVideo.Size = new System.Drawing.Size(163, 25);
            this.cbxChanelVideo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "Kênh Video";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(47, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "Frame Rate";
            // 
            // cbxFrameRate
            // 
            this.cbxFrameRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxFrameRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFrameRate.Items.AddRange(new object[] {
            "Uncontrolled",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cbxFrameRate.Location = new System.Drawing.Point(160, 174);
            this.cbxFrameRate.Name = "cbxFrameRate";
            this.cbxFrameRate.Size = new System.Drawing.Size(236, 25);
            this.cbxFrameRate.TabIndex = 9;
            // 
            // cbxImageQuality
            // 
            this.cbxImageQuality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxImageQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxImageQuality.FormattingEnabled = true;
            this.cbxImageQuality.Items.AddRange(new object[] {
            "Clarity",
            "Standard",
            "Motion"});
            this.cbxImageQuality.Location = new System.Drawing.Point(160, 206);
            this.cbxImageQuality.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxImageQuality.Name = "cbxImageQuality";
            this.cbxImageQuality.Size = new System.Drawing.Size(490, 25);
            this.cbxImageQuality.TabIndex = 10;
            // 
            // labelImageQuality
            // 
            this.labelImageQuality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelImageQuality.AutoSize = true;
            this.labelImageQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelImageQuality.Location = new System.Drawing.Point(47, 209);
            this.labelImageQuality.Name = "labelImageQuality";
            this.labelImageQuality.Size = new System.Drawing.Size(97, 17);
            this.labelImageQuality.TabIndex = 54;
            this.labelImageQuality.Text = "Chất lượng ảnh";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(160, 78);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(236, 25);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(47, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 46;
            this.label6.Text = "Tên truy cập";
            // 
            // cbxImageResolution
            // 
            this.cbxImageResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxImageResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxImageResolution.FormattingEnabled = true;
            this.cbxImageResolution.Items.AddRange(new object[] {
            "192x144",
            "320x180",
            "320x240",
            "640x360",
            "640x480",
            "800x600",
            "960x720",
            "1024x768",
            "1280x720",
            "1280x960",
            "1280x1024",
            "1600x1200",
            "1920x1080",
            "1920x1200",
            "2048x1536",
            "2560x1600",
            "2592x1944",
            "2944x1920",
            "3648x2752"});
            this.cbxImageResolution.Location = new System.Drawing.Point(160, 142);
            this.cbxImageResolution.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxImageResolution.Name = "cbxImageResolution";
            this.cbxImageResolution.Size = new System.Drawing.Size(236, 25);
            this.cbxImageResolution.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(47, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 48;
            this.label7.Text = "Mật khẩu";
            // 
            // txtLogin
            // 
            this.txtLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogin.Location = new System.Drawing.Point(160, 46);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(236, 25);
            this.txtLogin.TabIndex = 2;
            // 
            // txtVideoSource
            // 
            this.txtVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVideoSource.Location = new System.Drawing.Point(160, 14);
            this.txtVideoSource.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVideoSource.Name = "txtVideoSource";
            this.txtVideoSource.Size = new System.Drawing.Size(490, 25);
            this.txtVideoSource.TabIndex = 1;
            this.txtVideoSource.TextChanged += new System.EventHandler(this.txtURL_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(47, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 44;
            this.label3.Text = "Địa chỉ";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(47, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 17);
            this.label9.TabIndex = 52;
            this.label9.Text = "Độ phân giải";
            // 
            // cbxStreamType
            // 
            this.cbxStreamType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxStreamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStreamType.FormattingEnabled = true;
            this.cbxStreamType.Items.AddRange(new object[] {
            "jpeg",
            "mjpeg",
            "mpeg4",
            "h264",
            "h265"});
            this.cbxStreamType.Location = new System.Drawing.Point(160, 110);
            this.cbxStreamType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxStreamType.Name = "cbxStreamType";
            this.cbxStreamType.Size = new System.Drawing.Size(236, 25);
            this.cbxStreamType.TabIndex = 5;
            this.cbxStreamType.SelectedIndexChanged += new System.EventHandler(this.cbxStreamType_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(47, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 50;
            this.label8.Text = "Kiểu hình ảnh";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(413, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 64;
            this.label10.Text = "Http Port";
            // 
            // numHttpPort
            // 
            this.numHttpPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numHttpPort.Location = new System.Drawing.Point(487, 47);
            this.numHttpPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numHttpPort.Name = "numHttpPort";
            this.numHttpPort.Size = new System.Drawing.Size(163, 25);
            this.numHttpPort.TabIndex = 78;
            this.numHttpPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // cbxChanelImg
            // 
            this.cbxChanelImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxChanelImg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChanelImg.FormattingEnabled = true;
            this.cbxChanelImg.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32"});
            this.cbxChanelImg.Location = new System.Drawing.Point(487, 174);
            this.cbxChanelImg.Name = "cbxChanelImg";
            this.cbxChanelImg.Size = new System.Drawing.Size(163, 25);
            this.cbxChanelImg.TabIndex = 79;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(413, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 17);
            this.label11.TabIndex = 80;
            this.label11.Text = "Kênh Img";
            // 
            // numServerPort
            // 
            this.numServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numServerPort.Location = new System.Drawing.Point(487, 78);
            this.numServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numServerPort.Name = "numServerPort";
            this.numServerPort.Size = new System.Drawing.Size(163, 25);
            this.numServerPort.TabIndex = 82;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(413, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 17);
            this.label12.TabIndex = 81;
            this.label12.Text = "Server Port";
            // 
            // IPCameraSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numServerPort);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbxChanelImg);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numHttpPort);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxProtocol);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxVideoDecoder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbxChanelVideo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFrameRate);
            this.Controls.Add(this.cbxImageQuality);
            this.Controls.Add(this.labelImageQuality);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxImageResolution);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtVideoSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbxStreamType);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "IPCameraSettingPage";
            this.Size = new System.Drawing.Size(678, 280);
            ((System.ComponentModel.ISupportInitialize)(this.numHttpPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxProtocol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxVideoDecoder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxChanelVideo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFrameRate;
        private System.Windows.Forms.ComboBox cbxImageQuality;
        private System.Windows.Forms.Label labelImageQuality;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxImageResolution;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtVideoSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbxStreamType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numHttpPort;
        private System.Windows.Forms.ComboBox cbxChanelImg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numServerPort;
        private System.Windows.Forms.Label label12;
    }
}
