namespace Kztek.Control8.Forms
{
    partial class UcSelectVouchers
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnCancel = new Kztek.Control8.Controls.KzButton();
            uiLine1 = new Sunny.UI.UILine();
            lblTitle = new Kztek.Control8.Controls.KzLabel();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Location = new Point(0, 40);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(616, 328);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.BackColor = Color.White;
            btnCancel.BorderColor = Color.FromArgb(41, 97, 27);
            btnCancel.BorderRadius = 8;
            btnCancel.BorderThickness = 1;
            btnCancel.CustomizableEdges = customizableEdges1;
            btnCancel.DisabledState.BorderColor = Color.DarkGray;
            btnCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btnCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnCancel.FillColor = Color.White;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(460, 400);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(150, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "BTN_CANCEL";
            // 
            // uiLine1
            // 
            uiLine1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Font = new Font("Microsoft Sans Serif", 12F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.Location = new Point(8, 368);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(600, 16);
            uiLine1.TabIndex = 6;
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
            // UcSelectVouchers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(uiLine1);
            Controls.Add(btnCancel);
            Controls.Add(flowLayoutPanel1);
            Name = "UcSelectVouchers";
            Padding = new Padding(8);
            Size = new Size(616, 450);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Controls.KzButton btnCancel;
        private Sunny.UI.UILine uiLine1;
        private Controls.KzLabel lblTitle;
    }
}