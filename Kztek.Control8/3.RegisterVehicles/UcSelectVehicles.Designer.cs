using Kztek.Control8.Controls;

namespace Kztek.Control8.Forms
{
    partial class UcSelectVehicles
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flowVehicle = new FlowLayoutPanel();
            lblTitle = new KzLabel();
            btnConfirm = new KzButton();
            btnCancel = new KzButton();
            SuspendLayout();
            // 
            // flowVehicle
            // 
            flowVehicle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowVehicle.Location = new Point(0, 40);
            flowVehicle.Name = "flowVehicle";
            flowVehicle.Size = new Size(660, 307);
            flowVehicle.TabIndex = 15;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(8, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(142, 21);
            lblTitle.TabIndex = 7;
            lblTitle.Text = "LBL_VEHICLE_LIST";
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
            btnConfirm.FillColor = Color.White;
            btnConfirm.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Location = new Point(500, 360);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(150, 40);
            btnConfirm.TabIndex = 5;
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
            btnCancel.Location = new Point(340, 360);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnCancel.Size = new Size(150, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "BTN_CANCEL";
            // 
            // UcSelectVehicles
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(flowVehicle);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(0);
            Name = "UcSelectVehicles";
            Size = new Size(660, 411);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FlowLayoutPanel flowVehicle;
        private KzLabel lblTitle;
        private KzButton btnConfirm;
        private KzButton btnCancel;
    }
}