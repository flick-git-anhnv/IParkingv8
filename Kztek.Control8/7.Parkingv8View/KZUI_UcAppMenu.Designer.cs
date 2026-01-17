namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcAppMenu
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KZUI_UcAppMenu));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            menuStrip1 = new MenuStrip();
            miSystem = new ToolStripMenuItem();
            tsmiChangePassword = new ToolStripMenuItem();
            tsmiLogOut = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            miReport = new ToolStripMenuItem();
            tsmiReportIn = new ToolStripMenuItem();
            tsmiReportOut = new ToolStripMenuItem();
            tsmiRevenue = new ToolStripMenuItem();
            tsmiHandOver = new ToolStripMenuItem();
            tsmiAlarm = new ToolStripMenuItem();
            miData = new ToolStripMenuItem();
            tsmiAccessKey = new ToolStripMenuItem();
            tsmiCustomer = new ToolStripMenuItem();
            tsmiVehicle = new ToolStripMenuItem();
            flowLayoutPanel1 = new FlowLayoutPanel();
            lblInPark = new Guna.UI2.WinForms.Guna2Button();
            lblGotOut = new Guna.UI2.WinForms.Guna2Button();
            lblGotIn = new Guna.UI2.WinForms.Guna2Button();
            lblTime = new Label();
            minimizeBox = new Guna.UI2.WinForms.Guna2ControlBox();
            panel1 = new Panel();
            maximizeBox = new Guna.UI2.WinForms.Guna2ControlBox();
            panel2 = new Panel();
            closeBox = new Guna.UI2.WinForms.Guna2ControlBox();
            picLogo = new PictureBox();
            timerUpdateSystemCount = new System.Windows.Forms.Timer(components);
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            panelMain.SuspendLayout();
            menuStrip1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(226, 238, 255);
            panelMain.BorderRadius = 8;
            panelMain.Controls.Add(menuStrip1);
            panelMain.Controls.Add(flowLayoutPanel1);
            panelMain.Controls.Add(lblTime);
            panelMain.Controls.Add(minimizeBox);
            panelMain.Controls.Add(panel1);
            panelMain.Controls.Add(maximizeBox);
            panelMain.Controls.Add(panel2);
            panelMain.Controls.Add(closeBox);
            panelMain.Controls.Add(picLogo);
            customizableEdges13.BottomLeft = false;
            customizableEdges13.BottomRight = false;
            panelMain.CustomizableEdges = customizableEdges13;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.FromArgb(226, 238, 255);
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(8, 4, 8, 4);
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelMain.Size = new Size(1356, 40);
            panelMain.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.BackColor = Color.FromArgb(226, 238, 255);
            menuStrip1.Dock = DockStyle.Fill;
            menuStrip1.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Pixel);
            menuStrip1.Items.AddRange(new ToolStripItem[] { miSystem, miReport, miData });
            menuStrip1.Location = new Point(89, 4);
            menuStrip1.MaximumSize = new Size(257, 32);
            menuStrip1.MinimumSize = new Size(257, 32);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(0);
            menuStrip1.Size = new Size(257, 32);
            menuStrip1.TabIndex = 16;
            menuStrip1.Text = "menuStrip1";
            // 
            // miSystem
            // 
            miSystem.DropDownItems.AddRange(new ToolStripItem[] { tsmiChangePassword, tsmiLogOut, tsmiExit });
            miSystem.Name = "miSystem";
            miSystem.Size = new Size(91, 32);
            miSystem.Text = "Hệ thống";
            // 
            // tsmiChangePassword
            // 
            tsmiChangePassword.BackColor = Color.FromArgb(226, 238, 255);
            tsmiChangePassword.Image = (Image)resources.GetObject("tsmiChangePassword.Image");
            tsmiChangePassword.ImageScaling = ToolStripItemImageScaling.None;
            tsmiChangePassword.Name = "tsmiChangePassword";
            tsmiChangePassword.Size = new Size(191, 38);
            tsmiChangePassword.Text = "Đổi mật khẩu";
            // 
            // tsmiLogOut
            // 
            tsmiLogOut.BackColor = Color.FromArgb(226, 238, 255);
            tsmiLogOut.Image = (Image)resources.GetObject("tsmiLogOut.Image");
            tsmiLogOut.ImageScaling = ToolStripItemImageScaling.None;
            tsmiLogOut.Name = "tsmiLogOut";
            tsmiLogOut.Size = new Size(191, 38);
            tsmiLogOut.Text = "Đăng xuất";
            tsmiLogOut.Click += tsmiLogOut_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.BackColor = Color.FromArgb(226, 238, 255);
            tsmiExit.Image = (Image)resources.GetObject("tsmiExit.Image");
            tsmiExit.ImageScaling = ToolStripItemImageScaling.None;
            tsmiExit.Name = "tsmiExit";
            tsmiExit.Size = new Size(191, 38);
            tsmiExit.Text = "Thoát";
            tsmiExit.Click += tsmiExit_Click;
            // 
            // miReport
            // 
            miReport.DropDownItems.AddRange(new ToolStripItem[] { tsmiReportIn, tsmiReportOut, tsmiRevenue, tsmiHandOver, tsmiAlarm });
            miReport.Name = "miReport";
            miReport.Size = new Size(80, 32);
            miReport.Text = "Báo cáo";
            // 
            // tsmiReportIn
            // 
            tsmiReportIn.BackColor = Color.FromArgb(226, 238, 255);
            tsmiReportIn.Image = (Image)resources.GetObject("tsmiReportIn.Image");
            tsmiReportIn.ImageScaling = ToolStripItemImageScaling.None;
            tsmiReportIn.Name = "tsmiReportIn";
            tsmiReportIn.Size = new Size(232, 38);
            tsmiReportIn.Text = "Xe đang gửi";
            // 
            // tsmiReportOut
            // 
            tsmiReportOut.BackColor = Color.FromArgb(226, 238, 255);
            tsmiReportOut.Image = (Image)resources.GetObject("tsmiReportOut.Image");
            tsmiReportOut.ImageScaling = ToolStripItemImageScaling.None;
            tsmiReportOut.Name = "tsmiReportOut";
            tsmiReportOut.Size = new Size(232, 38);
            tsmiReportOut.Text = "Xe đã ra";
            // 
            // tsmiRevenue
            // 
            tsmiRevenue.BackColor = Color.FromArgb(226, 238, 255);
            tsmiRevenue.Image = (Image)resources.GetObject("tsmiRevenue.Image");
            tsmiRevenue.ImageScaling = ToolStripItemImageScaling.None;
            tsmiRevenue.Name = "tsmiRevenue";
            tsmiRevenue.Size = new Size(232, 38);
            tsmiRevenue.Text = "Báo cáo doanh thu";
            // 
            // tsmiHandOver
            // 
            tsmiHandOver.BackColor = Color.FromArgb(226, 238, 255);
            tsmiHandOver.Image = (Image)resources.GetObject("tsmiHandOver.Image");
            tsmiHandOver.ImageScaling = ToolStripItemImageScaling.None;
            tsmiHandOver.Name = "tsmiHandOver";
            tsmiHandOver.Size = new Size(232, 38);
            tsmiHandOver.Text = "Báo cáo chốt ca";
            // 
            // tsmiAlarm
            // 
            tsmiAlarm.BackColor = Color.FromArgb(226, 238, 255);
            tsmiAlarm.Image = (Image)resources.GetObject("tsmiAlarm.Image");
            tsmiAlarm.Name = "tsmiAlarm";
            tsmiAlarm.Size = new Size(232, 38);
            tsmiAlarm.Text = "Sự kiện cảnh báo";
            // 
            // miData
            // 
            miData.DropDownItems.AddRange(new ToolStripItem[] { tsmiAccessKey, tsmiCustomer, tsmiVehicle });
            miData.Name = "miData";
            miData.Size = new Size(73, 32);
            miData.Text = "Dữ liệu";
            // 
            // tsmiAccessKey
            // 
            tsmiAccessKey.BackColor = Color.FromArgb(226, 238, 255);
            tsmiAccessKey.Image = (Image)resources.GetObject("tsmiAccessKey.Image");
            tsmiAccessKey.ImageScaling = ToolStripItemImageScaling.None;
            tsmiAccessKey.Name = "tsmiAccessKey";
            tsmiAccessKey.Size = new Size(263, 38);
            tsmiAccessKey.Text = "Danh sách định danh";
            // 
            // tsmiCustomer
            // 
            tsmiCustomer.BackColor = Color.FromArgb(226, 238, 255);
            tsmiCustomer.Image = (Image)resources.GetObject("tsmiCustomer.Image");
            tsmiCustomer.ImageScaling = ToolStripItemImageScaling.None;
            tsmiCustomer.Name = "tsmiCustomer";
            tsmiCustomer.Size = new Size(263, 38);
            tsmiCustomer.Text = "Danh sách khách hàng";
            // 
            // tsmiVehicle
            // 
            tsmiVehicle.BackColor = Color.FromArgb(226, 238, 255);
            tsmiVehicle.Image = (Image)resources.GetObject("tsmiVehicle.Image");
            tsmiVehicle.ImageScaling = ToolStripItemImageScaling.None;
            tsmiVehicle.Name = "tsmiVehicle";
            tsmiVehicle.Size = new Size(263, 38);
            tsmiVehicle.Text = "Danh sách phương tiện";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.FromArgb(226, 238, 255);
            flowLayoutPanel1.Controls.Add(lblInPark);
            flowLayoutPanel1.Controls.Add(lblGotOut);
            flowLayoutPanel1.Controls.Add(lblGotIn);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(89, 4);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel1.Size = new Size(927, 32);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // lblInPark
            // 
            lblInPark.AutoRoundedCorners = true;
            lblInPark.BackColor = Color.FromArgb(226, 238, 255);
            lblInPark.BorderColor = Color.FromArgb(60, 0, 184, 217);
            lblInPark.BorderRadius = 15;
            lblInPark.BorderThickness = 1;
            lblInPark.CustomizableEdges = customizableEdges1;
            lblInPark.DisabledState.BorderColor = Color.FromArgb(60, 0, 184, 217);
            lblInPark.DisabledState.CustomBorderColor = Color.FromArgb(60, 0, 184, 217);
            lblInPark.DisabledState.FillColor = Color.FromArgb(60, 0, 184, 217);
            lblInPark.DisabledState.ForeColor = Color.FromArgb(0, 108, 156);
            lblInPark.Enabled = false;
            lblInPark.FillColor = Color.FromArgb(60, 0, 184, 217);
            lblInPark.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblInPark.ForeColor = Color.FromArgb(0, 108, 156);
            lblInPark.Location = new Point(767, 0);
            lblInPark.Margin = new Padding(0, 0, 16, 0);
            lblInPark.Name = "lblInPark";
            lblInPark.ShadowDecoration.CustomizableEdges = customizableEdges2;
            lblInPark.Size = new Size(160, 32);
            lblInPark.TabIndex = 7;
            lblInPark.Text = "TRONG BÃI : 999";
            lblInPark.Visible = false;
            // 
            // lblGotOut
            // 
            lblGotOut.AutoRoundedCorners = true;
            lblGotOut.BackColor = Color.FromArgb(226, 238, 255);
            lblGotOut.BorderColor = Color.FromArgb(60, 255, 86, 48);
            lblGotOut.BorderRadius = 15;
            lblGotOut.BorderThickness = 1;
            lblGotOut.CustomizableEdges = customizableEdges3;
            lblGotOut.DisabledState.BorderColor = Color.FromArgb(60, 255, 86, 48);
            lblGotOut.DisabledState.FillColor = Color.FromArgb(60, 255, 86, 48);
            lblGotOut.DisabledState.ForeColor = Color.FromArgb(183, 29, 24);
            lblGotOut.Enabled = false;
            lblGotOut.FillColor = Color.FromArgb(60, 255, 86, 48);
            lblGotOut.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblGotOut.ForeColor = Color.FromArgb(183, 29, 24);
            lblGotOut.Location = new Point(630, 0);
            lblGotOut.Margin = new Padding(0, 0, 16, 0);
            lblGotOut.Name = "lblGotOut";
            lblGotOut.ShadowDecoration.CustomizableEdges = customizableEdges4;
            lblGotOut.Size = new Size(121, 32);
            lblGotOut.TabIndex = 6;
            lblGotOut.Text = "RA : 999";
            lblGotOut.Visible = false;
            // 
            // lblGotIn
            // 
            lblGotIn.AutoRoundedCorners = true;
            lblGotIn.BackColor = Color.FromArgb(226, 238, 255);
            lblGotIn.BorderColor = Color.FromArgb(60, 34, 197, 94);
            lblGotIn.BorderRadius = 15;
            lblGotIn.BorderThickness = 1;
            lblGotIn.CustomizableEdges = customizableEdges5;
            lblGotIn.DisabledState.BorderColor = Color.FromArgb(60, 34, 197, 94);
            lblGotIn.DisabledState.FillColor = Color.FromArgb(60, 34, 197, 94);
            lblGotIn.DisabledState.ForeColor = Color.FromArgb(17, 141, 87);
            lblGotIn.Enabled = false;
            lblGotIn.FillColor = Color.FromArgb(60, 34, 197, 94);
            lblGotIn.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblGotIn.ForeColor = Color.FromArgb(17, 141, 87);
            lblGotIn.Location = new Point(474, 0);
            lblGotIn.Margin = new Padding(0, 0, 16, 0);
            lblGotIn.Name = "lblGotIn";
            lblGotIn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            lblGotIn.Size = new Size(140, 32);
            lblGotIn.TabIndex = 8;
            lblGotIn.Text = "VÀO : 999";
            lblGotIn.Visible = false;
            // 
            // lblTime
            // 
            lblTime.BackColor = Color.FromArgb(226, 238, 255);
            lblTime.Dock = DockStyle.Right;
            lblTime.Font = new Font("MS Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTime.Location = new Point(1016, 4);
            lblTime.Margin = new Padding(0);
            lblTime.Name = "lblTime";
            lblTime.Padding = new Padding(8, 0, 8, 0);
            lblTime.Size = new Size(193, 32);
            lblTime.TabIndex = 2;
            lblTime.Text = "12:00:00 30/03/2025";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // minimizeBox
            // 
            minimizeBox.BackColor = Color.FromArgb(226, 238, 255);
            minimizeBox.BorderRadius = 4;
            minimizeBox.BorderThickness = 1;
            minimizeBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeBox.CustomizableEdges = customizableEdges7;
            minimizeBox.Dock = DockStyle.Right;
            minimizeBox.FillColor = Color.FromArgb(226, 238, 255);
            minimizeBox.IconColor = Color.Black;
            minimizeBox.Location = new Point(1209, 4);
            minimizeBox.Margin = new Padding(0);
            minimizeBox.Name = "minimizeBox";
            minimizeBox.PressedColor = Color.White;
            minimizeBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
            minimizeBox.Size = new Size(41, 32);
            minimizeBox.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(226, 238, 255);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(1250, 4);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(8, 32);
            panel1.TabIndex = 13;
            // 
            // maximizeBox
            // 
            maximizeBox.BackColor = Color.FromArgb(226, 238, 255);
            maximizeBox.BorderRadius = 4;
            maximizeBox.BorderThickness = 1;
            maximizeBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            maximizeBox.CustomizableEdges = customizableEdges9;
            maximizeBox.Dock = DockStyle.Right;
            maximizeBox.FillColor = Color.FromArgb(226, 238, 255);
            maximizeBox.IconColor = Color.Black;
            maximizeBox.Location = new Point(1258, 4);
            maximizeBox.Margin = new Padding(0);
            maximizeBox.Name = "maximizeBox";
            maximizeBox.ShadowDecoration.CustomizableEdges = customizableEdges10;
            maximizeBox.Size = new Size(41, 32);
            maximizeBox.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(226, 238, 255);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1299, 4);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(8, 32);
            panel2.TabIndex = 14;
            // 
            // closeBox
            // 
            closeBox.BackColor = Color.FromArgb(226, 238, 255);
            closeBox.BorderRadius = 4;
            closeBox.BorderThickness = 1;
            closeBox.CustomClick = true;
            closeBox.CustomizableEdges = customizableEdges11;
            closeBox.Dock = DockStyle.Right;
            closeBox.FillColor = Color.FromArgb(226, 238, 255);
            closeBox.IconColor = Color.Black;
            closeBox.Location = new Point(1307, 4);
            closeBox.Margin = new Padding(0);
            closeBox.Name = "closeBox";
            closeBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
            closeBox.Size = new Size(41, 32);
            closeBox.TabIndex = 12;
            closeBox.Click += closeBox_Click;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Dock = DockStyle.Left;
            picLogo.Location = new Point(8, 4);
            picLogo.Margin = new Padding(0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(81, 32);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // timerUpdateSystemCount
            // 
            timerUpdateSystemCount.Interval = 5000;
            timerUpdateSystemCount.Tick += TimerUpdateSystemCount_Tick;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = flowLayoutPanel1;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // KZUI_UcAppMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            Controls.Add(panelMain);
            Margin = new Padding(0);
            Name = "KZUI_UcAppMenu";
            Size = new Size(1356, 40);
            panelMain.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2ControlBox minimizeBox;
        private Label lblTime;
        private Guna.UI2.WinForms.Guna2Button lblGotOut;
        private Guna.UI2.WinForms.Guna2Button lblInPark;
        private Guna.UI2.WinForms.Guna2Button lblGotIn;
        private System.Windows.Forms.Timer timerUpdateSystemCount;
        private FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2ControlBox closeBox;
        private Panel panel1;
        private Guna.UI2.WinForms.Guna2ControlBox maximizeBox;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem miReport;
        private ToolStripMenuItem tsmiReportIn;
        private ToolStripMenuItem tsmiReportOut;
        private ToolStripMenuItem tsmiRevenue;
        private ToolStripMenuItem miData;
        private ToolStripMenuItem tsmiAccessKey;
        private ToolStripMenuItem tsmiCustomer;
        private ToolStripMenuItem tsmiVehicle;
        private ToolStripMenuItem tsmiHandOver;
        private ToolStripMenuItem miSystem;
        private ToolStripMenuItem tsmiChangePassword;
        private ToolStripMenuItem tsmiLogOut;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem tsmiAlarm;
    }
}
