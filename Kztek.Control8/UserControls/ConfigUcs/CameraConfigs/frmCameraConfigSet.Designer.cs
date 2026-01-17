using Kztek.Control8.Controls;

namespace Kztek.Control8.UserControls.ConfigUcs.CameraConfigs
{
    partial class FrmCameraConfigSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCameraConfigSet));
            panelCameraView = new Panel();
            pic = new PictureBox();
            panelActions = new Panel();
            btnCancel = new KzButton();
            btnConfirm = new KzButton();
            panelCameraView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelCameraView
            // 
            panelCameraView.Controls.Add(pic);
            panelCameraView.Dock = DockStyle.Fill;
            panelCameraView.Location = new Point(0, 0);
            panelCameraView.Margin = new Padding(0);
            panelCameraView.Name = "panelCameraView";
            panelCameraView.Size = new Size(890, 508);
            panelCameraView.TabIndex = 0;
            // 
            // pic
            // 
            pic.BackColor = SystemColors.GradientInactiveCaption;
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Location = new Point(0, 0);
            pic.Margin = new Padding(0);
            pic.Name = "pic";
            pic.Size = new Size(888, 504);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.TabIndex = 0;
            pic.TabStop = false;
            pic.MouseDown += Pic_MouseDown;
            pic.MouseMove += Pic_MouseMove;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.White;
            panelActions.Controls.Add(btnCancel);
            panelActions.Controls.Add(btnConfirm);
            panelActions.Dock = DockStyle.Bottom;
            panelActions.Location = new Point(0, 508);
            panelActions.Margin = new Padding(0);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(890, 65);
            panelActions.TabIndex = 1;
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
            btnCancel.Location = new Point(631, 8);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(117, 48);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Đóng";
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
            btnConfirm.Location = new Point(759, 8);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(117, 48);
            btnConfirm.TabIndex = 16;
            btnConfirm.Text = "Xác Nhận";
            // 
            // FrmCameraConfigSet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(890, 573);
            Controls.Add(panelCameraView);
            Controls.Add(panelActions);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "FrmCameraConfigSet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cấu hình vùng nhận diện cho Camera";
            panelCameraView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            panelActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCameraView;
        private PictureBox pic;
        private Panel panelActions;
        private KzButton btnCancel;
        private KzButton btnConfirm;
    }
}