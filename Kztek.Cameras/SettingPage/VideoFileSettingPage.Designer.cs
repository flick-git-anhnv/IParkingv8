namespace Kztek.Cameras
{
    partial class VideoFileSettingPage
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDesiredFrameSize = new System.Windows.Forms.ComboBox();
            this.cbxVideoDecoder = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVideoFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 39;
            this.label3.Text = "Desired Frame Size:";
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
            this.cbxDesiredFrameSize.Location = new System.Drawing.Point(186, 190);
            this.cbxDesiredFrameSize.Name = "cbxDesiredFrameSize";
            this.cbxDesiredFrameSize.Size = new System.Drawing.Size(128, 25);
            this.cbxDesiredFrameSize.TabIndex = 38;
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
            "FFMPEG"});
            this.cbxVideoDecoder.Location = new System.Drawing.Point(186, 146);
            this.cbxVideoDecoder.Name = "cbxVideoDecoder";
            this.cbxVideoDecoder.Size = new System.Drawing.Size(229, 25);
            this.cbxVideoDecoder.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "Video Decoder:";
            // 
            // txtVideoFile
            // 
            this.txtVideoFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVideoFile.Location = new System.Drawing.Point(50, 49);
            this.txtVideoFile.Multiline = true;
            this.txtVideoFile.Name = "txtVideoFile";
            this.txtVideoFile.Size = new System.Drawing.Size(365, 78);
            this.txtVideoFile.TabIndex = 35;
            this.txtVideoFile.TextChanged += new System.EventHandler(this.txtVideoFile_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(419, 49);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(87, 36);
            this.btnBrowse.TabIndex = 34;
            this.btnBrowse.Text = "Mở ...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 33;
            this.label1.Text = "Đường dẫn File:";
            // 
            // VideoFileSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxDesiredFrameSize);
            this.Controls.Add(this.cbxVideoDecoder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtVideoFile);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "VideoFileSettingPage";
            this.Size = new System.Drawing.Size(510, 292);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDesiredFrameSize;
        private System.Windows.Forms.ComboBox cbxVideoDecoder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVideoFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
    }
}
