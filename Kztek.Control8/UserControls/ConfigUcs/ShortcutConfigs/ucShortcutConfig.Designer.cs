namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    partial class ucShortcutConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelActions = new Panel();
            btnConfirm = new Button();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelActions
            // 
            panelActions.Controls.Add(btnConfirm);
            panelActions.Dock = DockStyle.Bottom;
            panelActions.Location = new Point(0, 318);
            panelActions.Margin = new Padding(0);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(728, 56);
            panelActions.TabIndex = 0;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConfirm.BackColor = Color.FromArgb(41, 97, 27);
            btnConfirm.Font = new Font("Segoe UI", 12F);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.Location = new Point(592, 8);
            btnConfirm.Margin = new Padding(0);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(128, 40);
            btnConfirm.TabIndex = 8;
            btnConfirm.Text = "Xác nhận";
            btnConfirm.UseVisualStyleBackColor = false;
            // 
            // ucShortcutConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelActions);
            Margin = new Padding(0);
            Name = "ucShortcutConfig";
            Size = new Size(728, 374);
            panelActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelActions;
        private Button btnConfirm;
    }
}
