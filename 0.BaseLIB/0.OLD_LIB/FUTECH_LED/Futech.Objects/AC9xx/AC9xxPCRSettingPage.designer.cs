namespace Futech.Objects
{
    partial class AC9xxPCRSettingPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AC9xxPCRSettingPage));
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbControllerInfo = new System.Windows.Forms.Label();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRecoverRecord = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtKeyB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnWriteKeyA = new System.Windows.Forms.Button();
            this.txtKeyA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDateTimeUpload = new System.Windows.Forms.Button();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDateTimeDownload = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDO1PeriodTime = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtDelayTime = new System.Windows.Forms.TextBox();
            this.cbLEDStatusOfSuscess = new System.Windows.Forms.ComboBox();
            this.cbPayMode = new System.Windows.Forms.ComboBox();
            this.cbAdvance = new System.Windows.Forms.ComboBox();
            this.cbFID = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbMID = new System.Windows.Forms.ComboBox();
            this.txtSystemCode = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnClearBalckList = new System.Windows.Forms.Button();
            this.btnReadBlackList = new System.Windows.Forms.Button();
            this.btnWriteBlackList = new System.Windows.Forms.Button();
            this.btnRemoveFromBlackList = new System.Windows.Forms.Button();
            this.btnAddToBlackList = new System.Windows.Forms.Button();
            this.lsbBlackList = new System.Windows.Forms.ListBox();
            this.txtBlackList = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lbControllerInfo);
            this.groupBox4.Controls.Add(this.btnGetVersion);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // lbControllerInfo
            // 
            resources.ApplyResources(this.lbControllerInfo, "lbControllerInfo");
            this.lbControllerInfo.Name = "lbControllerInfo";
            // 
            // btnGetVersion
            // 
            resources.ApplyResources(this.btnGetVersion, "btnGetVersion");
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.UseVisualStyleBackColor = true;
            this.btnGetVersion.Click += new System.EventHandler(this.btnGetVersion_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRecoverRecord);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // btnRecoverRecord
            // 
            resources.ApplyResources(this.btnRecoverRecord, "btnRecoverRecord");
            this.btnRecoverRecord.Name = "btnRecoverRecord";
            this.btnRecoverRecord.UseVisualStyleBackColor = true;
            this.btnRecoverRecord.Click += new System.EventHandler(this.btnRecoverRecord_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtKeyB);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.btnWriteKeyA);
            this.groupBox2.Controls.Add(this.txtKeyA);
            this.groupBox2.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // txtKeyB
            // 
            resources.ApplyResources(this.txtKeyB, "txtKeyB");
            this.txtKeyB.Name = "txtKeyB";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // btnWriteKeyA
            // 
            resources.ApplyResources(this.btnWriteKeyA, "btnWriteKeyA");
            this.btnWriteKeyA.Name = "btnWriteKeyA";
            this.btnWriteKeyA.UseVisualStyleBackColor = true;
            this.btnWriteKeyA.Click += new System.EventHandler(this.btnWriteKeyA_Click);
            // 
            // txtKeyA
            // 
            resources.ApplyResources(this.txtKeyA, "txtKeyA");
            this.txtKeyA.Name = "txtKeyA";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDateTimeUpload);
            this.groupBox1.Controls.Add(this.dtpTime);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnDateTimeDownload);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnDateTimeUpload
            // 
            resources.ApplyResources(this.btnDateTimeUpload, "btnDateTimeUpload");
            this.btnDateTimeUpload.Name = "btnDateTimeUpload";
            this.btnDateTimeUpload.UseVisualStyleBackColor = true;
            this.btnDateTimeUpload.Click += new System.EventHandler(this.btnDateTimeUpload_Click);
            // 
            // dtpTime
            // 
            resources.ApplyResources(this.dtpTime, "dtpTime");
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.txtDO1PeriodTime, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtValue, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtDelayTime, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.cbLEDStatusOfSuscess, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.cbPayMode, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.cbAdvance, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.cbFID, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label17, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label12, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.cbMID, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtSystemCode, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.btnLoad, 1, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // txtDO1PeriodTime
            // 
            resources.ApplyResources(this.txtDO1PeriodTime, "txtDO1PeriodTime");
            this.txtDO1PeriodTime.Name = "txtDO1PeriodTime";
            // 
            // txtValue
            // 
            resources.ApplyResources(this.txtValue, "txtValue");
            this.txtValue.Name = "txtValue";
            // 
            // txtDelayTime
            // 
            resources.ApplyResources(this.txtDelayTime, "txtDelayTime");
            this.txtDelayTime.Name = "txtDelayTime";
            // 
            // cbLEDStatusOfSuscess
            // 
            resources.ApplyResources(this.cbLEDStatusOfSuscess, "cbLEDStatusOfSuscess");
            this.cbLEDStatusOfSuscess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLEDStatusOfSuscess.FormattingEnabled = true;
            this.cbLEDStatusOfSuscess.Items.AddRange(new object[] {
            resources.GetString("cbLEDStatusOfSuscess.Items"),
            resources.GetString("cbLEDStatusOfSuscess.Items1")});
            this.cbLEDStatusOfSuscess.Name = "cbLEDStatusOfSuscess";
            // 
            // cbPayMode
            // 
            resources.ApplyResources(this.cbPayMode, "cbPayMode");
            this.cbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayMode.FormattingEnabled = true;
            this.cbPayMode.Items.AddRange(new object[] {
            resources.GetString("cbPayMode.Items"),
            resources.GetString("cbPayMode.Items1")});
            this.cbPayMode.Name = "cbPayMode";
            // 
            // cbAdvance
            // 
            resources.ApplyResources(this.cbAdvance, "cbAdvance");
            this.cbAdvance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAdvance.FormattingEnabled = true;
            this.cbAdvance.Items.AddRange(new object[] {
            resources.GetString("cbAdvance.Items"),
            resources.GetString("cbAdvance.Items1")});
            this.cbAdvance.Name = "cbAdvance";
            // 
            // cbFID
            // 
            resources.ApplyResources(this.cbFID, "cbFID");
            this.cbFID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFID.FormattingEnabled = true;
            this.cbFID.Name = "cbFID";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // cbMID
            // 
            resources.ApplyResources(this.cbMID, "cbMID");
            this.cbMID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMID.FormattingEnabled = true;
            this.cbMID.Name = "cbMID";
            // 
            // txtSystemCode
            // 
            resources.ApplyResources(this.txtSystemCode, "txtSystemCode");
            this.txtSystemCode.Name = "txtSystemCode";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            resources.ApplyResources(this.btnLoad, "btnLoad");
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnClearBalckList);
            this.tabPage3.Controls.Add(this.btnReadBlackList);
            this.tabPage3.Controls.Add(this.btnWriteBlackList);
            this.tabPage3.Controls.Add(this.btnRemoveFromBlackList);
            this.tabPage3.Controls.Add(this.btnAddToBlackList);
            this.tabPage3.Controls.Add(this.lsbBlackList);
            this.tabPage3.Controls.Add(this.txtBlackList);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnClearBalckList
            // 
            resources.ApplyResources(this.btnClearBalckList, "btnClearBalckList");
            this.btnClearBalckList.Name = "btnClearBalckList";
            this.btnClearBalckList.UseVisualStyleBackColor = true;
            this.btnClearBalckList.Click += new System.EventHandler(this.btnClearBalckList_Click);
            // 
            // btnReadBlackList
            // 
            resources.ApplyResources(this.btnReadBlackList, "btnReadBlackList");
            this.btnReadBlackList.Name = "btnReadBlackList";
            this.btnReadBlackList.UseVisualStyleBackColor = true;
            this.btnReadBlackList.Click += new System.EventHandler(this.btnReadBlackList_Click);
            // 
            // btnWriteBlackList
            // 
            resources.ApplyResources(this.btnWriteBlackList, "btnWriteBlackList");
            this.btnWriteBlackList.Name = "btnWriteBlackList";
            this.btnWriteBlackList.UseVisualStyleBackColor = true;
            this.btnWriteBlackList.Click += new System.EventHandler(this.btnWriteBlackList_Click);
            // 
            // btnRemoveFromBlackList
            // 
            resources.ApplyResources(this.btnRemoveFromBlackList, "btnRemoveFromBlackList");
            this.btnRemoveFromBlackList.Name = "btnRemoveFromBlackList";
            this.btnRemoveFromBlackList.UseVisualStyleBackColor = true;
            this.btnRemoveFromBlackList.Click += new System.EventHandler(this.btnRemoveFromBlackList_Click);
            // 
            // btnAddToBlackList
            // 
            resources.ApplyResources(this.btnAddToBlackList, "btnAddToBlackList");
            this.btnAddToBlackList.Name = "btnAddToBlackList";
            this.btnAddToBlackList.UseVisualStyleBackColor = true;
            this.btnAddToBlackList.Click += new System.EventHandler(this.btnAddToBlackList_Click);
            // 
            // lsbBlackList
            // 
            this.lsbBlackList.FormattingEnabled = true;
            resources.ApplyResources(this.lsbBlackList, "lsbBlackList");
            this.lsbBlackList.Name = "lsbBlackList";
            this.lsbBlackList.SelectedIndexChanged += new System.EventHandler(this.lsbBlackList_SelectedIndexChanged);
            // 
            // txtBlackList
            // 
            resources.ApplyResources(this.txtBlackList, "txtBlackList");
            this.txtBlackList.Name = "txtBlackList";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AC9xxPCRSettingPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AC9xxPCRSettingPage";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AC9xxSettingPage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AC9xxSettingPage_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDateTimeDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDateTimeUpload;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnWriteKeyA;
        private System.Windows.Forms.TextBox txtKeyA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRecoverRecord;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbLEDStatusOfSuscess;
        private System.Windows.Forms.ComboBox cbPayMode;
        private System.Windows.Forms.ComboBox cbAdvance;
        private System.Windows.Forms.ComboBox cbFID;
        private System.Windows.Forms.ComboBox cbMID;
        private System.Windows.Forms.TextBox txtSystemCode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbControllerInfo;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.Button btnRemoveFromBlackList;
        private System.Windows.Forms.Button btnAddToBlackList;
        private System.Windows.Forms.ListBox lsbBlackList;
        private System.Windows.Forms.TextBox txtBlackList;
        private System.Windows.Forms.Button btnReadBlackList;
        private System.Windows.Forms.Button btnWriteBlackList;
        private System.Windows.Forms.Button btnClearBalckList;
        private System.Windows.Forms.TextBox txtKeyB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDO1PeriodTime;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtDelayTime;


    }
}