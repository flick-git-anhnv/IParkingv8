using Kztek.Control8.Controls;

namespace Kztek.Control8.Forms
{
    partial class UcConfirm
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblGuide = new KzLabel();
            lblMessage = new Label();
            lblTimer = new Label();
            timerAutoConfirm = new System.Windows.Forms.Timer(components);
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            SuspendLayout();
            // 
            // lblGuide
            // 
            lblGuide.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblGuide.AutoSize = true;
            lblGuide.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblGuide.ForeColor = Color.FromArgb(255, 128, 0);
            lblGuide.Location = new Point(8, 150);
            lblGuide.Name = "lblGuide";
            lblGuide.Size = new Size(142, 42);
            lblGuide.TabIndex = 7;
            lblGuide.Text = "Enter để xác nhận.\r\nEsc để hủy.";
            lblGuide.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMessage.Location = new Point(8, 8);
            lblMessage.Name = "lblMessage";
            lblMessage.Padding = new Padding(0, 0, 0, 50);
            lblMessage.Size = new Size(486, 96);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "_";
            lblMessage.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblTimer
            // 
            lblTimer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblTimer.ForeColor = Color.Black;
            lblTimer.Location = new Point(8, 119);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(204, 21);
            lblTimer.TabIndex = 8;
            lblTimer.Text = "Tự động đóng/xác nhận sau";
            lblTimer.Visible = false;
            // 
            // timerAutoConfirm
            // 
            timerAutoConfirm.Interval = 1000;
            timerAutoConfirm.Tick += TimerAutoClose_Tick;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.White;
            btnConfirm.BorderColor = Color.FromArgb(41, 97, 27);
            btnConfirm.BorderRadius = 8;
            btnConfirm.BorderThickness = 1;
            btnConfirm.CustomizableEdges = customizableEdges1;
            btnConfirm.DisabledState.BorderColor = Color.DarkGray;
            btnConfirm.DisabledState.CustomBorderColor = Color.DarkGray;
            btnConfirm.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnConfirm.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnConfirm.FillColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(376, 144);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(117, 48);
            btnConfirm.TabIndex = 11;
            btnConfirm.Text = "BTN_CONFIRM";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges3;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(248, 144);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(117, 48);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "BTN_CANCEL";
            // 
            // UcConfirm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblTimer);
            Controls.Add(lblMessage);
            Controls.Add(lblGuide);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "UcConfirm";
            Size = new Size(502, 201);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblMessage;
        private KzLabel lblGuide;
        private System.Windows.Forms.Timer timerAutoConfirm;
        private Label lblTimer;
        private KzButton btnCancel;
        private KzButton btnConfirm;
    }
}