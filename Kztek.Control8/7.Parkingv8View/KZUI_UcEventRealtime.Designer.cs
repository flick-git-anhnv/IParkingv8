namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcEventRealtime
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            lblRealtimeEvent = new Label();
            lblLicenseInfo = new Label();
            lblUser = new Label();
            lblServerName = new Label();
            lblLprType = new Label();
            lblVersion = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(226, 238, 255);
            panelMain.BorderRadius = 8;
            panelMain.Controls.Add(lblRealtimeEvent);
            panelMain.Controls.Add(lblLicenseInfo);
            panelMain.Controls.Add(lblUser);
            panelMain.Controls.Add(lblServerName);
            panelMain.Controls.Add(lblLprType);
            panelMain.Controls.Add(lblVersion);
            panelMain.CustomizableEdges = customizableEdges1;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(8, 4, 8, 4);
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelMain.Size = new Size(1211, 42);
            panelMain.TabIndex = 0;
            // 
            // lblRealtimeEvent
            // 
            lblRealtimeEvent.BackColor = Color.White;
            lblRealtimeEvent.Dock = DockStyle.Fill;
            lblRealtimeEvent.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblRealtimeEvent.Location = new Point(187, 4);
            lblRealtimeEvent.Margin = new Padding(0);
            lblRealtimeEvent.Name = "lblRealtimeEvent";
            lblRealtimeEvent.Size = new Size(786, 34);
            lblRealtimeEvent.TabIndex = 1;
            lblRealtimeEvent.Text = "_";
            lblRealtimeEvent.TextAlign = ContentAlignment.MiddleCenter;
            lblRealtimeEvent.Click += lblRealtimeEvent_Click;
            // 
            // lblLicenseInfo
            // 
            lblLicenseInfo.BackColor = Color.White;
            lblLicenseInfo.Dock = DockStyle.Right;
            lblLicenseInfo.Font = new Font("Consolas", 16F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblLicenseInfo.Location = new Point(973, 4);
            lblLicenseInfo.Margin = new Padding(0);
            lblLicenseInfo.Name = "lblLicenseInfo";
            lblLicenseInfo.Size = new Size(115, 34);
            lblLicenseInfo.TabIndex = 3;
            lblLicenseInfo.Text = "HSD: ";
            lblLicenseInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUser
            // 
            lblUser.BackColor = Color.White;
            lblUser.Dock = DockStyle.Left;
            lblUser.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblUser.Location = new Point(155, 4);
            lblUser.Margin = new Padding(0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(32, 34);
            lblUser.TabIndex = 2;
            lblUser.Text = "_";
            lblUser.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblServerName
            // 
            lblServerName.BackColor = Color.White;
            lblServerName.Dock = DockStyle.Left;
            lblServerName.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblServerName.Location = new Point(123, 4);
            lblServerName.Margin = new Padding(0);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(32, 34);
            lblServerName.TabIndex = 5;
            lblServerName.Text = "_";
            lblServerName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblLprType
            // 
            lblLprType.BackColor = Color.White;
            lblLprType.Dock = DockStyle.Right;
            lblLprType.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblLprType.Location = new Point(1088, 4);
            lblLprType.Margin = new Padding(0);
            lblLprType.Name = "lblLprType";
            lblLprType.Size = new Size(115, 34);
            lblLprType.TabIndex = 6;
            lblLprType.Text = "LPR:";
            lblLprType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.White;
            lblVersion.Dock = DockStyle.Left;
            lblVersion.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblVersion.Location = new Point(8, 4);
            lblVersion.Margin = new Padding(0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(115, 34);
            lblVersion.TabIndex = 4;
            lblVersion.Text = "version _";
            lblVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timerCheckLicense_Tick;
            // 
            // KZUI_UcEventRealtime
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            Controls.Add(panelMain);
            Margin = new Padding(0);
            Name = "KZUI_UcEventRealtime";
            Size = new Size(1211, 42);
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Label lblRealtimeEvent;
        private Label lblUser;
        private Label lblLicenseInfo;
        private System.Windows.Forms.Timer timer1;
        private Label lblVersion;
        private Label lblServerName;
        private Label lblLprType;
    }
}
