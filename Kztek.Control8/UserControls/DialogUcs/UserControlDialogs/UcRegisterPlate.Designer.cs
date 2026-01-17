namespace Kztek.Control8.UserControls.DialogUcs
{
    partial class UcRegisterPlate
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnCancel = new Guna.UI2.WinForms.Guna2Button();
            btnConfirm = new Guna.UI2.WinForms.Guna2Button();
            lblShortcutGuide1Line = new Label();
            timerAutoConfirm = new System.Windows.Forms.Timer(components);
            lblTitle = new Label();
            txtPlate = new Guna.UI2.WinForms.Guna2TextBox();
            lblCountDown = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            pictureBox1 = new PictureBox();
            guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
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
            btnCancel.Location = new Point(216, 337);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnCancel.Size = new Size(96, 43);
            btnCancel.TabIndex = 1;
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
            btnConfirm.Location = new Point(328, 337);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnConfirm.Size = new Size(112, 43);
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "Xác Nhận";
            // 
            // lblShortcutGuide1Line
            // 
            lblShortcutGuide1Line.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblShortcutGuide1Line.AutoSize = true;
            lblShortcutGuide1Line.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblShortcutGuide1Line.ForeColor = Color.FromArgb(255, 128, 0);
            lblShortcutGuide1Line.Location = new Point(8, 392);
            lblShortcutGuide1Line.Name = "lblShortcutGuide1Line";
            lblShortcutGuide1Line.Size = new Size(249, 21);
            lblShortcutGuide1Line.TabIndex = 9;
            lblShortcutGuide1Line.Text = "Enter để Xác Nhận | ESC để Đóng";
            lblShortcutGuide1Line.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timerAutoConfirm
            // 
            timerAutoConfirm.Interval = 1000;
            timerAutoConfirm.Tick += TimerAutoConfirm_Tick;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(192, 0, 0);
            lblTitle.Location = new Point(8, 8);
            lblTitle.Margin = new Padding(2, 0, 2, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(431, 40);
            lblTitle.TabIndex = 25;
            lblTitle.Text = "Xác Nhận Biển Số";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPlate
            // 
            txtPlate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPlate.BackColor = Color.White;
            txtPlate.BorderColor = Color.FromArgb(6, 59, 167);
            txtPlate.BorderRadius = 8;
            txtPlate.CharacterCasing = CharacterCasing.Upper;
            txtPlate.CustomizableEdges = customizableEdges5;
            txtPlate.DefaultText = "_";
            txtPlate.DisabledState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtPlate.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtPlate.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtPlate.FocusedState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.FocusedState.ForeColor = Color.FromArgb(30, 26, 80);
            txtPlate.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPlate.ForeColor = Color.FromArgb(6, 59, 167);
            txtPlate.HoverState.BorderColor = Color.FromArgb(80, 6, 59, 167);
            txtPlate.HoverState.ForeColor = Color.FromArgb(30, 26, 80);
            txtPlate.Location = new Point(8, 248);
            txtPlate.Margin = new Padding(0);
            txtPlate.Name = "txtPlate";
            txtPlate.PlaceholderText = "_ _ _ _ _ _ _ _";
            txtPlate.SelectedText = "";
            txtPlate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPlate.Size = new Size(432, 40);
            txtPlate.TabIndex = 2;
            txtPlate.TextAlign = HorizontalAlignment.Center;
            // 
            // lblCountDown
            // 
            lblCountDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCountDown.AutoSize = true;
            lblCountDown.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblCountDown.ForeColor = SystemColors.ControlText;
            lblCountDown.Location = new Point(8, 305);
            lblCountDown.Name = "lblCountDown";
            lblCountDown.Size = new Size(135, 21);
            lblCountDown.TabIndex = 28;
            lblCountDown.Text = "Tự động đóng sau";
            lblCountDown.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 16;
            guna2Elipse1.TargetControl = this;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(8, 56);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(256, 176);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 29;
            pictureBox1.TabStop = false;
            // 
            // guna2Elipse2
            // 
            guna2Elipse2.BorderRadius = 16;
            guna2Elipse2.TargetControl = pictureBox1;
            // 
            // UcRegisterPlate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(pictureBox1);
            Controls.Add(btnCancel);
            Controls.Add(lblShortcutGuide1Line);
            Controls.Add(lblCountDown);
            Controls.Add(txtPlate);
            Controls.Add(btnConfirm);
            Controls.Add(lblTitle);
            Name = "UcRegisterPlate";
            Padding = new Padding(8);
            Size = new Size(447, 421);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timerAutoConfirm;
        private Label lblTitle;
        private Label lblShortcutGuide1Line;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnConfirm;
        private Guna.UI2.WinForms.Guna2TextBox txtPlate;
        private Label lblCountDown;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
    }
}