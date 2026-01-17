namespace Kztek.Cameras
{
    partial class VideoCaptureDeviceSettingPage
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
            this.cbxChanel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxVideoDecoder = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFindFilter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxDeinterlaceFilterName = new System.Windows.Forms.ComboBox();
            this.chkUsingDeinterlaceFilter = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDesiredFrameSize = new System.Windows.Forms.ComboBox();
            this.cbxDesiredFrameRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbxChanel
            // 
            this.cbxChanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxChanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChanel.Items.AddRange(new object[] {
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
            "16"});
            this.cbxChanel.Location = new System.Drawing.Point(177, 51);
            this.cbxChanel.Name = "cbxChanel";
            this.cbxChanel.Size = new System.Drawing.Size(152, 25);
            this.cbxChanel.TabIndex = 69;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(47, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 17);
            this.label6.TabIndex = 68;
            this.label6.Text = "Kênh";
            // 
            // cbxVideoDecoder
            // 
            this.cbxVideoDecoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxVideoDecoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxVideoDecoder.FormattingEnabled = true;
            this.cbxVideoDecoder.Items.AddRange(new object[] {
            "DirectShow",
            "VLC",
            "VLC Player"});
            this.cbxVideoDecoder.Location = new System.Drawing.Point(177, 248);
            this.cbxVideoDecoder.Name = "cbxVideoDecoder";
            this.cbxVideoDecoder.Size = new System.Drawing.Size(369, 25);
            this.cbxVideoDecoder.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(47, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 66;
            this.label5.Text = "Video Decoder";
            // 
            // btnFindFilter
            // 
            this.btnFindFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFindFilter.Location = new System.Drawing.Point(410, 168);
            this.btnFindFilter.Name = "btnFindFilter";
            this.btnFindFilter.Size = new System.Drawing.Size(136, 28);
            this.btnFindFilter.TabIndex = 65;
            this.btnFindFilter.Text = "Find Filter";
            this.btnFindFilter.UseVisualStyleBackColor = true;
            this.btnFindFilter.Click += new System.EventHandler(this.btnFindFilter_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(111, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 64;
            this.label4.Text = "Name";
            // 
            // cbxDeinterlaceFilterName
            // 
            this.cbxDeinterlaceFilterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDeinterlaceFilterName.FormattingEnabled = true;
            this.cbxDeinterlaceFilterName.Items.AddRange(new object[] {
            "Deinterlace Filter",
            "blend",
            "bob",
            "discard",
            "linear",
            "mean",
            "x",
            "yadif",
            "yadif2x"});
            this.cbxDeinterlaceFilterName.Location = new System.Drawing.Point(177, 202);
            this.cbxDeinterlaceFilterName.Name = "cbxDeinterlaceFilterName";
            this.cbxDeinterlaceFilterName.Size = new System.Drawing.Size(369, 25);
            this.cbxDeinterlaceFilterName.TabIndex = 63;
            // 
            // chkUsingDeinterlaceFilter
            // 
            this.chkUsingDeinterlaceFilter.AutoSize = true;
            this.chkUsingDeinterlaceFilter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkUsingDeinterlaceFilter.Location = new System.Drawing.Point(50, 176);
            this.chkUsingDeinterlaceFilter.Name = "chkUsingDeinterlaceFilter";
            this.chkUsingDeinterlaceFilter.Size = new System.Drawing.Size(166, 21);
            this.chkUsingDeinterlaceFilter.TabIndex = 62;
            this.chkUsingDeinterlaceFilter.Text = "Using De-Interlace Filter";
            this.chkUsingDeinterlaceFilter.UseVisualStyleBackColor = true;
            this.chkUsingDeinterlaceFilter.CheckedChanged += new System.EventHandler(this.chkUsingDeinterlaceFilter_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(47, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Desired Frame Size";
            // 
            // cbxDesiredFrameSize
            // 
            this.cbxDesiredFrameSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDesiredFrameSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDesiredFrameSize.Items.AddRange(new object[] {
            "Uncontrolled",
            "320x180",
            "320x240",
            "352x240",
            "352x288",
            "640x360",
            "640x480",
            "704x480",
            "704x576",
            "720x576",
            "768x576",
            "800x600",
            "960x720",
            "1024x768",
            "1280x720",
            "1280x960",
            "1280x1024",
            "1600x1200",
            "1920x1080",
            "2048x1536"});
            this.cbxDesiredFrameSize.Location = new System.Drawing.Point(177, 125);
            this.cbxDesiredFrameSize.Name = "cbxDesiredFrameSize";
            this.cbxDesiredFrameSize.Size = new System.Drawing.Size(152, 25);
            this.cbxDesiredFrameSize.TabIndex = 60;
            // 
            // cbxDesiredFrameRate
            // 
            this.cbxDesiredFrameRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDesiredFrameRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDesiredFrameRate.Items.AddRange(new object[] {
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
            this.cbxDesiredFrameRate.Location = new System.Drawing.Point(177, 88);
            this.cbxDesiredFrameRate.Name = "cbxDesiredFrameRate";
            this.cbxDesiredFrameRate.Size = new System.Drawing.Size(152, 25);
            this.cbxDesiredFrameRate.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(47, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 58;
            this.label2.Text = "Desired Frame Rate";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(47, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "Video Device";
            // 
            // cbxDevice
            // 
            this.cbxDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDevice.Location = new System.Drawing.Point(177, 14);
            this.cbxDevice.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(369, 25);
            this.cbxDevice.TabIndex = 56;
            // 
            // VideoCaptureDeviceSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxChanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxVideoDecoder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFindFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxDeinterlaceFilterName);
            this.Controls.Add(this.chkUsingDeinterlaceFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDesiredFrameSize);
            this.Controls.Add(this.cbxDesiredFrameRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxDevice);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VideoCaptureDeviceSettingPage";
            this.Size = new System.Drawing.Size(575, 326);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxChanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxVideoDecoder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFindFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxDeinterlaceFilterName;
        private System.Windows.Forms.CheckBox chkUsingDeinterlaceFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDesiredFrameSize;
        private System.Windows.Forms.ComboBox cbxDesiredFrameRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDevice;
    }
}
