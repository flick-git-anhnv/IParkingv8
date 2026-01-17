namespace Futech.Objects
{
    partial class FINGERTECSettingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FINGERTECSettingPage));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numCommunicationKey = new System.Windows.Forms.NumericUpDown();
            this.btnSetCommunicationKey = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearAdministrator = new System.Windows.Forms.Button();
            this.btnClearAllData = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnDateTimeUpLoad = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDateTimeDownload = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbSDKVersion = new System.Windows.Forms.Label();
            this.lbFirmwareVersion = new System.Windows.Forms.Label();
            this.btnGetFirmware = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnGetSDKVersion = new System.Windows.Forms.Button();
            this.txtWorkCode = new System.Windows.Forms.TextBox();
            this.lbWorkCode = new System.Windows.Forms.Label();
            this.btnGetWorkCode = new System.Windows.Forms.Button();
            this.btnClearWorkCode = new System.Windows.Forms.Button();
            this.btnSetWorkCode = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.axBioBridgeSDK = new AxBioBridgeSDKLib.AxBioBridgeSDK();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCommunicationKey)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numCommunicationKey);
            this.groupBox4.Controls.Add(this.btnSetCommunicationKey);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // numCommunicationKey
            // 
            resources.ApplyResources(this.numCommunicationKey, "numCommunicationKey");
            this.numCommunicationKey.Name = "numCommunicationKey";
            // 
            // btnSetCommunicationKey
            // 
            resources.ApplyResources(this.btnSetCommunicationKey, "btnSetCommunicationKey");
            this.btnSetCommunicationKey.Name = "btnSetCommunicationKey";
            this.btnSetCommunicationKey.UseVisualStyleBackColor = true;
            this.btnSetCommunicationKey.Click += new System.EventHandler(this.btnSetCommunicationKey_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearAdministrator);
            this.groupBox3.Controls.Add(this.btnClearAllData);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnClearAdministrator
            // 
            resources.ApplyResources(this.btnClearAdministrator, "btnClearAdministrator");
            this.btnClearAdministrator.Name = "btnClearAdministrator";
            this.btnClearAdministrator.UseVisualStyleBackColor = true;
            this.btnClearAdministrator.Click += new System.EventHandler(this.btnClearAdministrator_Click);
            // 
            // btnClearAllData
            // 
            resources.ApplyResources(this.btnClearAllData, "btnClearAllData");
            this.btnClearAllData.Name = "btnClearAllData";
            this.btnClearAllData.UseVisualStyleBackColor = true;
            this.btnClearAllData.Click += new System.EventHandler(this.btnClearAllData_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.txtDate);
            this.groupBox2.Controls.Add(this.btnDateTimeUpLoad);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtTime, "txtTime");
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtDate, "txtDate");
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            // 
            // btnDateTimeUpLoad
            // 
            resources.ApplyResources(this.btnDateTimeUpLoad, "btnDateTimeUpLoad");
            this.btnDateTimeUpLoad.Name = "btnDateTimeUpLoad";
            this.btnDateTimeUpLoad.UseVisualStyleBackColor = true;
            this.btnDateTimeUpLoad.Click += new System.EventHandler(this.btnDateTimeUpLoad_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnDateTimeDownload);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // dtpTime
            // 
            resources.ApplyResources(this.dtpTime, "dtpTime");
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            // 
            // dtpDate
            // 
            resources.ApplyResources(this.dtpDate, "dtpDate");
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Name = "dtpDate";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnDateTimeDownload
            // 
            resources.ApplyResources(this.btnDateTimeDownload, "btnDateTimeDownload");
            this.btnDateTimeDownload.Name = "btnDateTimeDownload";
            this.btnDateTimeDownload.UseVisualStyleBackColor = true;
            this.btnDateTimeDownload.Click += new System.EventHandler(this.btnDateTimeDownload_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.btnTimezoneDownload);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.cbxTimezone);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.graphControl);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // btnTimezoneDownload
            // 
            resources.ApplyResources(this.btnTimezoneDownload, "btnTimezoneDownload");
            this.btnTimezoneDownload.Name = "btnTimezoneDownload";
            this.btnTimezoneDownload.UseVisualStyleBackColor = true;
            this.btnTimezoneDownload.Click += new System.EventHandler(this.btnTimezoneDownload_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // cbxTimezone
            // 
            this.cbxTimezone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTimezone.FormattingEnabled = true;
            resources.ApplyResources(this.cbxTimezone, "cbxTimezone");
            this.cbxTimezone.Name = "cbxTimezone";
            this.cbxTimezone.SelectedIndexChanged += new System.EventHandler(this.cbxTimezone_SelectedIndexChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // graphControl
            // 
            this.graphControl.AxesColor = System.Drawing.Color.Red;
            this.graphControl.GraphColor = System.Drawing.Color.Blue;
            this.graphControl.GraphStyle = Futech.Objects.GraphType.Line;
            resources.ApplyResources(this.graphControl, "graphControl");
            this.graphControl.Name = "graphControl";
            this.graphControl.ShowPointsOnGraph = false;
            this.graphControl.TextColor = System.Drawing.Color.Black;
            this.graphControl.TitleXAxis = "None";
            this.graphControl.TitleYAxis = "None";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.lbSDKVersion);
            this.tabPage2.Controls.Add(this.lbFirmwareVersion);
            this.tabPage2.Controls.Add(this.btnGetFirmware);
            this.tabPage2.Controls.Add(this.lbStatus);
            this.tabPage2.Controls.Add(this.btnGetSDKVersion);
            this.tabPage2.Controls.Add(this.txtWorkCode);
            this.tabPage2.Controls.Add(this.lbWorkCode);
            this.tabPage2.Controls.Add(this.btnGetWorkCode);
            this.tabPage2.Controls.Add(this.btnClearWorkCode);
            this.tabPage2.Controls.Add(this.btnSetWorkCode);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // lbSDKVersion
            // 
            resources.ApplyResources(this.lbSDKVersion, "lbSDKVersion");
            this.lbSDKVersion.Name = "lbSDKVersion";
            // 
            // lbFirmwareVersion
            // 
            resources.ApplyResources(this.lbFirmwareVersion, "lbFirmwareVersion");
            this.lbFirmwareVersion.Name = "lbFirmwareVersion";
            // 
            // btnGetFirmware
            // 
            resources.ApplyResources(this.btnGetFirmware, "btnGetFirmware");
            this.btnGetFirmware.Name = "btnGetFirmware";
            this.btnGetFirmware.UseVisualStyleBackColor = true;
            this.btnGetFirmware.Click += new System.EventHandler(this.btnGetFirmware_Click);
            // 
            // lbStatus
            // 
            resources.ApplyResources(this.lbStatus, "lbStatus");
            this.lbStatus.Name = "lbStatus";
            // 
            // btnGetSDKVersion
            // 
            resources.ApplyResources(this.btnGetSDKVersion, "btnGetSDKVersion");
            this.btnGetSDKVersion.Name = "btnGetSDKVersion";
            this.btnGetSDKVersion.UseVisualStyleBackColor = true;
            this.btnGetSDKVersion.Click += new System.EventHandler(this.btnGetSDKVersion_Click);
            // 
            // txtWorkCode
            // 
            resources.ApplyResources(this.txtWorkCode, "txtWorkCode");
            this.txtWorkCode.Name = "txtWorkCode";
            // 
            // lbWorkCode
            // 
            resources.ApplyResources(this.lbWorkCode, "lbWorkCode");
            this.lbWorkCode.Name = "lbWorkCode";
            // 
            // btnGetWorkCode
            // 
            resources.ApplyResources(this.btnGetWorkCode, "btnGetWorkCode");
            this.btnGetWorkCode.Name = "btnGetWorkCode";
            this.btnGetWorkCode.UseVisualStyleBackColor = true;
            this.btnGetWorkCode.Click += new System.EventHandler(this.btnGetWorkCode_Click);
            // 
            // btnClearWorkCode
            // 
            resources.ApplyResources(this.btnClearWorkCode, "btnClearWorkCode");
            this.btnClearWorkCode.Name = "btnClearWorkCode";
            this.btnClearWorkCode.UseVisualStyleBackColor = true;
            this.btnClearWorkCode.Click += new System.EventHandler(this.btnClearWorkCode_Click);
            // 
            // btnSetWorkCode
            // 
            resources.ApplyResources(this.btnSetWorkCode, "btnSetWorkCode");
            this.btnSetWorkCode.Name = "btnSetWorkCode";
            this.btnSetWorkCode.UseVisualStyleBackColor = true;
            this.btnSetWorkCode.Click += new System.EventHandler(this.btnSetWorkCode_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // axBioBridgeSDK
            // 
            resources.ApplyResources(this.axBioBridgeSDK, "axBioBridgeSDK");
            this.axBioBridgeSDK.Name = "axBioBridgeSDK";
            this.axBioBridgeSDK.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBioBridgeSDK.OcxState")));
            // 
            // FINGERTECSettingPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.axBioBridgeSDK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FINGERTECSettingPage";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FINGERTECSettingPage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FINGERTECSettingPage_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCommunicationKey)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axBioBridgeSDK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnClearAdministrator;
        private System.Windows.Forms.Button btnClearAllData;
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
        private System.Windows.Forms.TabPage tabPage3;
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
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGetSDKVersion;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSetCommunicationKey;
        private System.Windows.Forms.NumericUpDown numCommunicationKey;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnClearWorkCode;
        private System.Windows.Forms.Button btnSetWorkCode;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox txtWorkCode;
        private System.Windows.Forms.Label lbWorkCode;
        private System.Windows.Forms.Button btnGetWorkCode;
        private System.Windows.Forms.Button btnGetFirmware;
        private System.Windows.Forms.Label lbSDKVersion;
        private System.Windows.Forms.Label lbFirmwareVersion;
        private AxBioBridgeSDKLib.AxBioBridgeSDK axBioBridgeSDK;
    }
}