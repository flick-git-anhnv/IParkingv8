namespace IParkingv8.LaneUIs.KioskOut
{
    partial class UcKioskOut
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
            ucKioskTitle1 = new Kztek.Control8.UserControls.DialogUcs.KioskOut.ucKioskTitle();
            ucKioskOutDashboard1 = new Kztek.Control8.UserControls.DialogUcs.KioskOut.ucKioskOutDashboard();
            panelDashboard = new Panel();
            panelDialog = new Panel();
            panelDashboard.SuspendLayout();
            SuspendLayout();
            // 
            // ucKioskTitle1
            // 
            ucKioskTitle1.BackColor = Color.Transparent;
            ucKioskTitle1.Dock = DockStyle.Top;
            ucKioskTitle1.Location = new Point(0, 0);
            ucKioskTitle1.Margin = new Padding(0);
            ucKioskTitle1.Name = "ucKioskTitle1";
            ucKioskTitle1.Size = new Size(1280, 91);
            ucKioskTitle1.TabIndex = 9;
            // 
            // ucKioskOutDashboard1
            // 
            ucKioskOutDashboard1.BackColor = Color.Transparent;
            ucKioskOutDashboard1.Location = new Point(0, 0);
            ucKioskOutDashboard1.Name = "ucKioskOutDashboard1";
            ucKioskOutDashboard1.Size = new Size(1280, 933);
            ucKioskOutDashboard1.TabIndex = 10;
            // 
            // panelDashboard
            // 
            panelDashboard.Controls.Add(ucKioskOutDashboard1);
            panelDashboard.Location = new Point(0, 91);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(1280, 933);
            panelDashboard.TabIndex = 11;
            // 
            // panelDialog
            // 
            panelDialog.Location = new Point(0, 91);
            panelDialog.Name = "panelDialog";
            panelDialog.Size = new Size(1280, 933);
            panelDialog.TabIndex = 12;
            // 
            // UcKioskOut
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(193, 220, 250);
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(ucKioskTitle1);
            Controls.Add(panelDashboard);
            Controls.Add(panelDialog);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UcKioskOut";
            Size = new Size(1280, 1024);
            panelDashboard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Kztek.Control8.UserControls.DialogUcs.KioskOut.ucKioskTitle ucKioskTitle1;
        private Kztek.Control8.UserControls.DialogUcs.KioskOut.ucKioskOutDashboard ucKioskOutDashboard1;
        private Panel panelDashboard;
        private Panel panelDialog;
    }
}