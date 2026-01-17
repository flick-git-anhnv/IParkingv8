using Kztek.Control8._1.GeneralControls._4.CheckBox;
using Kztek.Control8.Controls;

namespace IParkingv8.Forms
{
    partial class FrmSelectLaneMode
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectLaneMode));
            lblTitle = new KzLabel();
            panelActiveLanes = new Panel();
            chbSelectAll = new KzCheckBox();
            timerAutoSelectLane = new System.Windows.Forms.Timer(components);
            lblStatus = new KzLabel();
            btnConfirm = new KzButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(8, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(178, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "LBL_CHOOSE_LANE";
            // 
            // panelActiveLanes
            // 
            panelActiveLanes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelActiveLanes.BackColor = Color.White;
            panelActiveLanes.Font = new Font("Segoe UI", 12F);
            panelActiveLanes.Location = new Point(8, 72);
            panelActiveLanes.Margin = new Padding(3, 2, 3, 2);
            panelActiveLanes.Name = "panelActiveLanes";
            panelActiveLanes.Size = new Size(608, 335);
            panelActiveLanes.TabIndex = 1;
            // 
            // chbSelectAll
            // 
            chbSelectAll.AutoSize = true;
            chbSelectAll.BackColor = Color.White;
            chbSelectAll.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            chbSelectAll.CheckedState.BorderColor = Color.FromArgb(41, 97, 27);
            chbSelectAll.CheckedState.BorderRadius = 2;
            chbSelectAll.CheckedState.BorderThickness = 1;
            chbSelectAll.CheckedState.FillColor = Color.FromArgb(41, 97, 27);
            chbSelectAll.Font = new Font("Segoe UI", 12F);
            chbSelectAll.Location = new Point(8, 40);
            chbSelectAll.Margin = new Padding(3, 2, 3, 2);
            chbSelectAll.Name = "chbSelectAll";
            chbSelectAll.Size = new Size(150, 25);
            chbSelectAll.TabIndex = 4;
            chbSelectAll.Text = "CHB_SELECT_ALL";
            chbSelectAll.UncheckedState.BorderColor = Color.FromArgb(41, 97, 27);
            chbSelectAll.UncheckedState.BorderRadius = 2;
            chbSelectAll.UncheckedState.BorderThickness = 1;
            chbSelectAll.UseVisualStyleBackColor = false;
            // 
            // timerAutoSelectLane
            // 
            timerAutoSelectLane.Enabled = true;
            timerAutoSelectLane.Interval = 1000;
            timerAutoSelectLane.Tick += TimerAutoSelectLane_Tick;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.BackColor = Color.White;
            lblStatus.Font = new Font("Segoe UI", 11.25F, FontStyle.Italic);
            lblStatus.ForeColor = Color.Green;
            lblStatus.Location = new Point(8, 443);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(224, 20);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "LBL_AUTO_OPEN_HOME_SCREEN";
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges3;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(466, 415);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(150, 48);
            btnConfirm.TabIndex = 10;
            btnConfirm.Text = "BTN_CONFIRM";
            // 
            // FrmSelectLaneMode
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(626, 472);
            Controls.Add(lblStatus);
            Controls.Add(panelActiveLanes);
            Controls.Add(btnConfirm);
            Controls.Add(chbSelectAll);
            Controls.Add(lblTitle);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSelectLaneMode";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lựa chọn làn hoạt động";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KzLabel lblTitle;
        private Panel panelActiveLanes;
        private KzCheckBox chbSelectAll;
        private System.Windows.Forms.Timer timerAutoSelectLane;
        private KzLabel lblStatus;
        private KzButton btnConfirm;
    }
}