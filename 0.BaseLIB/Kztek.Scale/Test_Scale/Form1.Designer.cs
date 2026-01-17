namespace Test_Scale
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numTimeReceived = new System.Windows.Forms.NumericUpDown();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.btnDisConnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lstReceivedMessage = new System.Windows.Forms.ListBox();
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.labelNet = new System.Windows.Forms.Label();
            this.labelTare = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelGross = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxScaleType = new System.Windows.Forms.ComboBox();
            this.lblScaleType = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeReceived)).BeginInit();
            this.groupBoxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numTimeReceived);
            this.groupBox1.Controls.Add(this.cbxDataBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxStopBits);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxComPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 238);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Truyền thông";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 208);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 16);
            this.label9.TabIndex = 15;
            this.label9.Text = "received time out";
            // 
            // numTimeReceived
            // 
            this.numTimeReceived.Location = new System.Drawing.Point(125, 206);
            this.numTimeReceived.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTimeReceived.Name = "numTimeReceived";
            this.numTimeReceived.Size = new System.Drawing.Size(121, 22);
            this.numTimeReceived.TabIndex = 14;
            this.numTimeReceived.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cbxDataBits.Location = new System.Drawing.Point(125, 92);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(121, 24);
            this.cbxDataBits.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data Bits";
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbxStopBits.Location = new System.Drawing.Point(125, 152);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(121, 24);
            this.cbxStopBits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Stop Bits";
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Items.AddRange(new object[] {
            "Even",
            "Odd",
            "None"});
            this.cbxParity.Location = new System.Drawing.Point(125, 122);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(121, 24);
            this.cbxParity.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Parity";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "1800",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbxBaudRate.Location = new System.Drawing.Point(125, 62);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(121, 24);
            this.cbxBaudRate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baud Rate";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cổng COM";
            // 
            // cbxComPort
            // 
            this.cbxComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxComPort.FormattingEnabled = true;
            this.cbxComPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.cbxComPort.Location = new System.Drawing.Point(125, 32);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(121, 24);
            this.cbxComPort.TabIndex = 0;
            // 
            // btnDisConnect
            // 
            this.btnDisConnect.Enabled = false;
            this.btnDisConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDisConnect.Location = new System.Drawing.Point(429, 243);
            this.btnDisConnect.Name = "btnDisConnect";
            this.btnDisConnect.Size = new System.Drawing.Size(133, 38);
            this.btnDisConnect.TabIndex = 11;
            this.btnDisConnect.Text = "DISCONNECT";
            this.btnDisConnect.UseVisualStyleBackColor = true;
            this.btnDisConnect.Click += new System.EventHandler(this.btnDisConnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConnect.Location = new System.Drawing.Point(290, 243);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(133, 38);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lstReceivedMessage
            // 
            this.lstReceivedMessage.FormattingEnabled = true;
            this.lstReceivedMessage.ItemHeight = 16;
            this.lstReceivedMessage.Location = new System.Drawing.Point(12, 288);
            this.lstReceivedMessage.Name = "lstReceivedMessage";
            this.lstReceivedMessage.ScrollAlwaysVisible = true;
            this.lstReceivedMessage.Size = new System.Drawing.Size(634, 196);
            this.lstReceivedMessage.TabIndex = 1;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.labelNet);
            this.groupBoxResult.Controls.Add(this.labelTare);
            this.groupBoxResult.Controls.Add(this.label8);
            this.groupBoxResult.Controls.Add(this.label7);
            this.groupBoxResult.Controls.Add(this.label6);
            this.groupBoxResult.Controls.Add(this.labelGross);
            this.groupBoxResult.Location = new System.Drawing.Point(290, 43);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(356, 194);
            this.groupBoxResult.TabIndex = 12;
            this.groupBoxResult.TabStop = false;
            // 
            // labelNet
            // 
            this.labelNet.AutoSize = true;
            this.labelNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNet.Location = new System.Drawing.Point(127, 140);
            this.labelNet.Name = "labelNet";
            this.labelNet.Size = new System.Drawing.Size(69, 31);
            this.labelNet.TabIndex = 5;
            this.labelNet.Text = "0 kg";
            // 
            // labelTare
            // 
            this.labelTare.AutoSize = true;
            this.labelTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTare.Location = new System.Drawing.Point(127, 85);
            this.labelTare.Name = "labelTare";
            this.labelTare.Size = new System.Drawing.Size(69, 31);
            this.labelTare.TabIndex = 4;
            this.labelTare.Text = "0 kg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(25, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 31);
            this.label8.TabIndex = 3;
            this.label8.Text = "Net:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(20, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 31);
            this.label7.TabIndex = 2;
            this.label7.Text = "Tare:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(20, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 31);
            this.label6.TabIndex = 1;
            this.label6.Text = "Gross:";
            // 
            // labelGross
            // 
            this.labelGross.AutoSize = true;
            this.labelGross.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelGross.Location = new System.Drawing.Point(127, 32);
            this.labelGross.Name = "labelGross";
            this.labelGross.Size = new System.Drawing.Size(69, 31);
            this.labelGross.TabIndex = 0;
            this.labelGross.Text = "0 kg";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(18, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 20;
            this.label10.Text = "Loại đầu CÂN";
            // 
            // cbxScaleType
            // 
            this.cbxScaleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScaleType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cbxScaleType.FormattingEnabled = true;
            this.cbxScaleType.Items.AddRange(new object[] {
            "KingBird",
            "KingbirdStandard",
            "EX2001",
            "RinstrumR320",
            "D2008"});
            this.cbxScaleType.Location = new System.Drawing.Point(127, 12);
            this.cbxScaleType.Name = "cbxScaleType";
            this.cbxScaleType.Size = new System.Drawing.Size(157, 24);
            this.cbxScaleType.TabIndex = 19;
            // 
            // lblScaleType
            // 
            this.lblScaleType.AutoSize = true;
            this.lblScaleType.Location = new System.Drawing.Point(9, 509);
            this.lblScaleType.Name = "lblScaleType";
            this.lblScaleType.Size = new System.Drawing.Size(56, 16);
            this.lblScaleType.TabIndex = 16;
            this.lblScaleType.Text = "Kingbird";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 529);
            this.Controls.Add(this.lblScaleType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxScaleType);
            this.Controls.Add(this.groupBoxResult);
            this.Controls.Add(this.btnDisConnect);
            this.Controls.Add(this.lstReceivedMessage);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đầu Cân";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeReceived)).EndInit();
            this.groupBoxResult.ResumeLayout(false);
            this.groupBoxResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDisConnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbxDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxComPort;
        private System.Windows.Forms.ListBox lstReceivedMessage;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.Label labelGross;
        private System.Windows.Forms.Label labelNet;
        private System.Windows.Forms.Label labelTare;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numTimeReceived;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxScaleType;
        private System.Windows.Forms.Label lblScaleType;
    }
}