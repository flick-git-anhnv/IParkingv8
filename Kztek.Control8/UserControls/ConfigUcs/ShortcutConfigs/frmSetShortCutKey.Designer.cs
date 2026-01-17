namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    partial class frmSetShortCutKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetShortCutKey));
            lblTitle = new Label();
            lblCurrentKeySetValue = new Label();
            panelActions = new Panel();
            btnConfirm = new Button();
            btnCancel = new Button();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(291, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nhập phím tắt muốn gán vào chức năng";
            // 
            // lblCurrentKeySetValue
            // 
            lblCurrentKeySetValue.Dock = DockStyle.Fill;
            lblCurrentKeySetValue.Font = new Font("Segoe UI", 25F);
            lblCurrentKeySetValue.Location = new Point(0, 20);
            lblCurrentKeySetValue.Name = "lblCurrentKeySetValue";
            lblCurrentKeySetValue.Size = new Size(544, 157);
            lblCurrentKeySetValue.TabIndex = 1;
            lblCurrentKeySetValue.Text = "label1";
            lblCurrentKeySetValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelActions
            // 
            panelActions.Controls.Add(btnConfirm);
            panelActions.Controls.Add(btnCancel);
            panelActions.Dock = DockStyle.Bottom;
            panelActions.Location = new Point(0, 177);
            panelActions.Margin = new Padding(0);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(544, 56);
            panelActions.TabIndex = 3;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI", 12F);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(408, 8);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(128, 40);
            btnConfirm.TabIndex = 7;
            btnConfirm.Text = "Xác nhận";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.ForeColor = Color.FromArgb(41, 97, 27);
            btnCancel.Location = new Point(256, 8);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(144, 40);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Xóa thiết lập";
            // 
            // frmSetShortCutKey
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(544, 233);
            Controls.Add(lblCurrentKeySetValue);
            Controls.Add(panelActions);
            Controls.Add(lblTitle);
            Font = new Font("Segoe UI", 11.25F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetShortCutKey";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chọn phím tắt";
            panelActions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCurrentKeySetValue;
        private Panel panelActions;
        private Button btnConfirm;
        private Button btnCancel;
    }
}