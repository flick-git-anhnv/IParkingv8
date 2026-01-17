namespace Futech.Objects
{
    partial class PEGASUSPB7SettingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PEGASUSPB7SettingPage));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearAdministrator = new System.Windows.Forms.Button();
            this.btnClearAllData = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDateTimeDownload = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnDateTimeUpLoad = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTimezoneDownload = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbxTimezone = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.graphControl = new Futech.Objects.GraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbxDeviceStatus = new System.Windows.Forms.ComboBox();
            this.btnGetDeviceStatus = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.axFKAttend = new AxFKAttendLib.AxFKAttend();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axFKAttend)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(370, 363);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(362, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cấu hình chung";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearAdministrator);
            this.groupBox3.Controls.Add(this.btnClearAllData);
            this.groupBox3.Location = new System.Drawing.Point(6, 178);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 153);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Khởi tạo Bộ điều khiển";
            // 
            // btnClearAdministrator
            // 
            this.btnClearAdministrator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearAdministrator.Location = new System.Drawing.Point(99, 56);
            this.btnClearAdministrator.Name = "btnClearAdministrator";
            this.btnClearAdministrator.Size = new System.Drawing.Size(152, 23);
            this.btnClearAdministrator.TabIndex = 4;
            this.btnClearAdministrator.Text = "Xóa tất cả các sự kiện";
            this.btnClearAdministrator.UseVisualStyleBackColor = true;
            this.btnClearAdministrator.Click += new System.EventHandler(this.btnClearAdministrator_Click);
            // 
            // btnClearAllData
            // 
            this.btnClearAllData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearAllData.Location = new System.Drawing.Point(99, 27);
            this.btnClearAllData.Name = "btnClearAllData";
            this.btnClearAllData.Size = new System.Drawing.Size(152, 23);
            this.btnClearAllData.TabIndex = 3;
            this.btnClearAllData.Text = "Khởi tạo Bộ điều khiển";
            this.btnClearAllData.UseVisualStyleBackColor = true;
            this.btnClearAllData.Click += new System.EventHandler(this.btnClearAllData_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnDateTimeDownload);
            this.groupBox1.Location = new System.Drawing.Point(6, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Đặt lại ngày giờ trên Bộ điều khiển";
            // 
            // dtpTime
            // 
            this.dtpTime.CustomFormat = "HH:mm:ss";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(99, 46);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(88, 20);
            this.dtpTime.TabIndex = 20;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(99, 22);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(88, 20);
            this.dtpDate.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(50, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Giờ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(50, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Ngày";
            // 
            // btnDateTimeDownload
            // 
            this.btnDateTimeDownload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDateTimeDownload.Location = new System.Drawing.Point(262, 45);
            this.btnDateTimeDownload.Name = "btnDateTimeDownload";
            this.btnDateTimeDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDateTimeDownload.TabIndex = 16;
            this.btnDateTimeDownload.Text = "Cập nhật";
            this.btnDateTimeDownload.UseVisualStyleBackColor = true;
            this.btnDateTimeDownload.Click += new System.EventHandler(this.btnDateTimeDownload_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.txtDate);
            this.groupBox2.Controls.Add(this.btnDateTimeUpLoad);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ngày giờ hiện tại trên Bộ điều khiển";
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.White;
            this.txtTime.Location = new System.Drawing.Point(99, 48);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(88, 20);
            this.txtTime.TabIndex = 22;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.Location = new System.Drawing.Point(99, 22);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(88, 20);
            this.txtDate.TabIndex = 21;
            // 
            // btnDateTimeUpLoad
            // 
            this.btnDateTimeUpLoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDateTimeUpLoad.Location = new System.Drawing.Point(265, 46);
            this.btnDateTimeUpLoad.Name = "btnDateTimeUpLoad";
            this.btnDateTimeUpLoad.Size = new System.Drawing.Size(75, 23);
            this.btnDateTimeUpLoad.TabIndex = 20;
            this.btnDateTimeUpLoad.Text = "Nhận";
            this.btnDateTimeUpLoad.UseVisualStyleBackColor = true;
            this.btnDateTimeUpLoad.Click += new System.EventHandler(this.btnDateTimeUpLoad_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(50, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Giờ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(50, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Ngày";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btnTimezoneDownload);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.cbxTimezone);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.graphControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(362, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Thiết lập Timezone";
            // 
            // btnTimezoneDownload
            // 
            this.btnTimezoneDownload.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTimezoneDownload.Location = new System.Drawing.Point(263, 298);
            this.btnTimezoneDownload.Name = "btnTimezoneDownload";
            this.btnTimezoneDownload.Size = new System.Drawing.Size(76, 23);
            this.btnTimezoneDownload.TabIndex = 39;
            this.btnTimezoneDownload.Text = "Cập nhật";
            this.btnTimezoneDownload.UseVisualStyleBackColor = true;
            this.btnTimezoneDownload.Click += new System.EventHandler(this.btnTimezoneDownload_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(0, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "CN";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(0, 95);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 36;
            this.label16.Text = "T7";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(-3, 121);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(20, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "T6";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(-3, 148);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 34;
            this.label14.Text = "T5";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(-3, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "T4";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(-3, 205);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "T3";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(-1, 233);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "T2";
            // 
            // cbxTimezone
            // 
            this.cbxTimezone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTimezone.FormattingEnabled = true;
            this.cbxTimezone.Location = new System.Drawing.Point(140, 16);
            this.cbxTimezone.Name = "cbxTimezone";
            this.cbxTimezone.Size = new System.Drawing.Size(199, 21);
            this.cbxTimezone.TabIndex = 30;
            this.cbxTimezone.SelectedIndexChanged += new System.EventHandler(this.cbxTimezone_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(14, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Chọn Timezone";
            // 
            // graphControl
            // 
            this.graphControl.AxesColor = System.Drawing.Color.Red;
            this.graphControl.GraphColor = System.Drawing.Color.Blue;
            this.graphControl.GraphStyle = Futech.Objects.GraphType.Line;
            this.graphControl.Location = new System.Drawing.Point(2, 43);
            this.graphControl.Name = "graphControl";
            this.graphControl.ShowPointsOnGraph = false;
            this.graphControl.Size = new System.Drawing.Size(363, 244);
            this.graphControl.TabIndex = 38;
            this.graphControl.TextColor = System.Drawing.Color.Black;
            this.graphControl.TitleXAxis = "None";
            this.graphControl.TitleYAxis = "None";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(362, 337);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Thông số khác";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtValue);
            this.groupBox4.Controls.Add(this.cbxDeviceStatus);
            this.groupBox4.Controls.Add(this.btnGetDeviceStatus);
            this.groupBox4.Location = new System.Drawing.Point(6, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(350, 115);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Get Device Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Status";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(143, 87);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(201, 20);
            this.txtValue.TabIndex = 1;
            // 
            // cbxDeviceStatus
            // 
            this.cbxDeviceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDeviceStatus.FormattingEnabled = true;
            this.cbxDeviceStatus.Items.AddRange(new object[] {
            "GET NUMBER MANAGERS",
            "GET NUMBER USERS ",
            "GET NUMBER FPS ",
            "GET NUMBER PASSWORD",
            "GET NUMBER SLOGS ",
            "GET NUMBER GENERAL LOGS ",
            "GET NUMBER ASLOGS ",
            "GET NUMBER AGLOGS ",
            "GET NUMBER CARDS "});
            this.cbxDeviceStatus.Location = new System.Drawing.Point(6, 41);
            this.cbxDeviceStatus.Name = "cbxDeviceStatus";
            this.cbxDeviceStatus.Size = new System.Drawing.Size(338, 21);
            this.cbxDeviceStatus.TabIndex = 2;
            // 
            // btnGetDeviceStatus
            // 
            this.btnGetDeviceStatus.Location = new System.Drawing.Point(6, 85);
            this.btnGetDeviceStatus.Name = "btnGetDeviceStatus";
            this.btnGetDeviceStatus.Size = new System.Drawing.Size(75, 23);
            this.btnGetDeviceStatus.TabIndex = 0;
            this.btnGetDeviceStatus.Text = "Get";
            this.btnGetDeviceStatus.UseVisualStyleBackColor = true;
            this.btnGetDeviceStatus.Click += new System.EventHandler(this.btnGetDeviceStatus_Click);
            // 
            // btnClose
            // 
            this.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClose.Location = new System.Drawing.Point(307, 383);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // axFKAttend
            // 
            this.axFKAttend.Enabled = true;
            this.axFKAttend.Location = new System.Drawing.Point(215, 381);
            this.axFKAttend.Name = "axFKAttend";
            this.axFKAttend.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axFKAttend.OcxState")));
            this.axFKAttend.Size = new System.Drawing.Size(32, 32);
            this.axFKAttend.TabIndex = 12;
            this.axFKAttend.Visible = false;
            // 
            // PEGASUSPB7SettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(394, 418);
            this.Controls.Add(this.axFKAttend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PEGASUSPB7SettingPage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thiết lập các thông số của Bộ điều khiển PEGASUSFINGER";
            this.Load += new System.EventHandler(this.PEGASUSFINGERSettingPage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PEGASUSFINGERSettingPage_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axFKAttend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Button btnDateTimeUpLoad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDateTimeDownload;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearAdministrator;
        private System.Windows.Forms.Button btnClearAllData;
        private AxFKAttendLib.AxFKAttend axFKAttend;
        private System.Windows.Forms.Button btnTimezoneDownload;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbxTimezone;
        private System.Windows.Forms.Label label8;
        private GraphControl graphControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cbxDeviceStatus;
        private System.Windows.Forms.Button btnGetDeviceStatus;
    }
}