using Kztek.Control8.KioskIn.DashBoard;
using Kztek.Control8.UIHelpers;

namespace IParkingv8.LaneUIs.KioskIn
{
    partial class UcKioskIn
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
            ucKioskInDashboard1 = new ucKioskInDashBoard();
            ucDalilyDialog = new Kztek.Control8.KioskIn.ucConfirmOpenBarrieKioskInView();
            ucMonthlyDialog = new Kztek.Control8.KioskIn.ConfirmPlate.UcConfirmPlateMonthlyKioskIn();
            panelDashboard = new RoundedTransparentPanel();
            panelDialog = new RoundedTransparentPanel();
            panelDashboard.SuspendLayout();
            panelDialog.SuspendLayout();
            SuspendLayout();
            // 
            // ucKioskTitle1
            // 
            ucKioskTitle1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ucKioskTitle1.BackColor = Color.Transparent;
            ucKioskTitle1.Location = new Point(0, 0);
            ucKioskTitle1.Margin = new Padding(0);
            ucKioskTitle1.Name = "ucKioskTitle1";
            ucKioskTitle1.Size = new Size(1280, 91);
            ucKioskTitle1.TabIndex = 9;
            // 
            // ucKioskOutDashboard1
            // 
            ucKioskInDashboard1.BackColor = Color.Transparent;
            ucKioskInDashboard1.Location = new Point(0, 0);
            ucKioskInDashboard1.Name = "ucKioskOutDashboard1";
            ucKioskInDashboard1.Size = new Size(1280, 933);
            ucKioskInDashboard1.TabIndex = 10;
            // 
            // ucDalilyDialog
            // 
            ucDalilyDialog.BackColor = Color.Transparent;
            ucDalilyDialog.BackgroundImageLayout = ImageLayout.Stretch;
            ucDalilyDialog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ucDalilyDialog.Location = new Point(0, 0);
            ucDalilyDialog.Margin = new Padding(0);
            ucDalilyDialog.Name = "ucDalilyDialog";
            ucDalilyDialog.Size = new Size(1280, 933);
            ucDalilyDialog.TabIndex = 11;
            ucDalilyDialog.Visible = false;
            // 
            // ucMonthlyDialog
            // 
            ucMonthlyDialog.BackColor = Color.Transparent;
            ucMonthlyDialog.BackgroundImageLayout = ImageLayout.Stretch;
            ucMonthlyDialog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ucMonthlyDialog.Location = new Point(0, 0);
            ucMonthlyDialog.Margin = new Padding(0);
            ucMonthlyDialog.Name = "ucMonthlyDialog";
            ucMonthlyDialog.Size = new Size(1280, 933);
            ucMonthlyDialog.TabIndex = 12;
            ucMonthlyDialog.Visible = false;
            // 
            // panelDashboard
            // 
            panelDashboard.BackColor = Color.Transparent;
            panelDashboard.BorderColor = Color.White;
            panelDashboard.BorderThickness = 0;
            panelDashboard.Controls.Add(ucKioskInDashboard1);
            panelDashboard.CornerRadius = 0;
            panelDashboard.Location = new Point(0, 91);
            panelDashboard.Name = "panelDashboard";
            panelDashboard.Size = new Size(1280, 933);
            panelDashboard.TabIndex = 15;
            // 
            // panelDialog
            // 
            panelDialog.BackColor = Color.Transparent;
            panelDialog.BorderColor = Color.White;
            panelDialog.BorderThickness = 0;
            panelDialog.Controls.Add(ucMonthlyDialog);
            panelDialog.Controls.Add(ucDalilyDialog);
            panelDialog.CornerRadius = 0;
            panelDialog.Location = new Point(0, 91);
            panelDialog.Name = "panelDialog";
            panelDialog.Size = new Size(1280, 933);
            panelDialog.TabIndex = 16;
            panelDialog.Visible = false;
            // 
            // UcKioskIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(193, 220, 250);
            BackgroundImageLayout = ImageLayout.Stretch;
            Controls.Add(panelDashboard);
            Controls.Add(ucKioskTitle1);
            Controls.Add(panelDialog);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UcKioskIn";
            Size = new Size(1280, 1024);
            panelDashboard.ResumeLayout(false);
            panelDialog.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Kztek.Control8.UserControls.DialogUcs.KioskOut.ucKioskTitle ucKioskTitle1;
        private ucKioskInDashBoard ucKioskInDashboard1;
        private Kztek.Control8.KioskIn.ucConfirmOpenBarrieKioskInView ucDalilyDialog;
        private Kztek.Control8.KioskIn.ConfirmPlate.UcConfirmPlateMonthlyKioskIn ucMonthlyDialog;
        private Kztek.Control8.BaseKiosk.UcConfirmKioskDaily ucDailyNotify;
        private Kztek.Control8.BaseKiosk.UcConfirmKioskMonthly ucMonthlyNotify;
        private Kztek.Control8.UIHelpers.RoundedTransparentPanel panelDashboard;
        private RoundedTransparentPanel panelDialog;
    }
}