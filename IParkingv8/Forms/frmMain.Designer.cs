namespace IParkingv8.Forms
{
    partial class FrmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            timerLoading = new System.Windows.Forms.Timer(components);
            kzuI_UcEventRealtime1 = new Kztek.Control8.UserControls.KZUI_UcEventRealtime();
            ucViewGrid1 = new Kztek.Control8.UserControls.UcViewGrid();
            guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(components);
            timerRestartSocket = new System.Windows.Forms.Timer(components);
            kzuI_UcAppMenu1 = new Kztek.Control8.UserControls.KZUI_UcAppMenu();
            SuspendLayout();
            // 
            // timerLoading
            // 
            timerLoading.Interval = 1000;
            timerLoading.Tick += TimerLoading_Tick;
            // 
            // kzuI_UcEventRealtime1
            // 
            kzuI_UcEventRealtime1.BackColor = Color.FromArgb(226, 238, 255);
            kzuI_UcEventRealtime1.Dock = DockStyle.Bottom;
            kzuI_UcEventRealtime1.Location = new Point(0, 1059);
            kzuI_UcEventRealtime1.Margin = new Padding(0);
            kzuI_UcEventRealtime1.Name = "kzuI_UcEventRealtime1";
            kzuI_UcEventRealtime1.Size = new Size(1463, 43);
            kzuI_UcEventRealtime1.TabIndex = 3;
            // 
            // ucViewGrid1
            // 
            ucViewGrid1.ColumnsCount = 2;
            ucViewGrid1.Dock = DockStyle.Fill;
            ucViewGrid1.Location = new Point(0, 53);
            ucViewGrid1.Margin = new Padding(0);
            ucViewGrid1.Name = "ucViewGrid1";
            ucViewGrid1.RowsCount = 2;
            ucViewGrid1.Size = new Size(1463, 1006);
            ucViewGrid1.TabIndex = 4;
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = this;
            guna2DragControl1.UseTransparentDrag = true;
            // 
            // timerRestartSocket
            // 
            timerRestartSocket.Interval = 3600000;
            // 
            // kzuI_UcAppMenu1
            // 
            kzuI_UcAppMenu1.BackColor = Color.FromArgb(226, 238, 255);
            kzuI_UcAppMenu1.Dock = DockStyle.Top;
            kzuI_UcAppMenu1.Location = new Point(0, 0);
            kzuI_UcAppMenu1.Margin = new Padding(0);
            kzuI_UcAppMenu1.Name = "kzuI_UcAppMenu1";
            kzuI_UcAppMenu1.Size = new Size(1463, 53);
            kzuI_UcAppMenu1.TabIndex = 5;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            ClientSize = new Size(1463, 1102);
            Controls.Add(ucViewGrid1);
            Controls.Add(kzuI_UcAppMenu1);
            Controls.Add(kzuI_UcEventRealtime1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmMain";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Timer timerLoading;
        private Kztek.Control8.UserControls.KZUI_UcEventRealtime kzuI_UcEventRealtime1;
        private Kztek.Control8.UserControls.UcViewGrid ucViewGrid1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private System.Windows.Forms.Timer timerRestartSocket;
        private Kztek.Control8.UserControls.KZUI_UcAppMenu kzuI_UcAppMenu1;
    }
}