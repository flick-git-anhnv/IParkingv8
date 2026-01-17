namespace Kztek.Control8.UserControls.ConfigUcs
{
    partial class ucLedLineConfig
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblStepName = new Label();
            panel2 = new Panel();
            numDelayTime = new Guna.UI2.WinForms.Guna2NumericUpDown();
            label2 = new Label();
            label1 = new Label();
            btnCancel1 = new Button();
            uiLine1 = new Sunny.UI.UILine();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDelayTime).BeginInit();
            SuspendLayout();
            // 
            // lblStepName
            // 
            lblStepName.Dock = DockStyle.Left;
            lblStepName.Location = new Point(0, 0);
            lblStepName.Name = "lblStepName";
            lblStepName.Size = new Size(60, 77);
            lblStepName.TabIndex = 1;
            lblStepName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(numDelayTime);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(60, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(0, 8, 0, 0);
            panel2.Size = new Size(284, 40);
            panel2.TabIndex = 2;
            // 
            // numDelayTime
            // 
            numDelayTime.BackColor = Color.Transparent;
            numDelayTime.BorderRadius = 8;
            numDelayTime.CustomizableEdges = customizableEdges3;
            numDelayTime.Dock = DockStyle.Fill;
            numDelayTime.FocusedState.BorderColor = Color.FromArgb(41, 97, 27);
            numDelayTime.Font = new Font("Segoe UI", 12F);
            numDelayTime.Location = new Point(80, 8);
            numDelayTime.Margin = new Padding(0);
            numDelayTime.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numDelayTime.Name = "numDelayTime";
            numDelayTime.ShadowDecoration.CustomizableEdges = customizableEdges4;
            numDelayTime.Size = new Size(166, 32);
            numDelayTime.TabIndex = 11;
            numDelayTime.UpDownButtonFillColor = Color.FromArgb(41, 97, 27);
            numDelayTime.UpDownButtonForeColor = Color.White;
            numDelayTime.Value = new decimal(new int[] { 300, 0, 0, 0 });
            // 
            // label2
            // 
            label2.BackColor = Color.White;
            label2.Dock = DockStyle.Right;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(246, 8);
            label2.Name = "label2";
            label2.Size = new Size(38, 32);
            label2.TabIndex = 2;
            label2.Text = "ms";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.BackColor = Color.White;
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(0, 8);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(80, 32);
            label1.TabIndex = 0;
            label1.Text = "Delay";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnCancel1
            // 
            btnCancel1.AutoSize = true;
            btnCancel1.Dock = DockStyle.Right;
            btnCancel1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold);
            btnCancel1.ForeColor = Color.Black;
            btnCancel1.Location = new Point(344, 0);
            btnCancel1.Margin = new Padding(3, 2, 3, 2);
            btnCancel1.Name = "btnCancel1";
            btnCancel1.Size = new Size(46, 77);
            btnCancel1.TabIndex = 3;
            btnCancel1.Text = "Xóa";
            // 
            // uiLine1
            // 
            uiLine1.BackColor = Color.Transparent;
            uiLine1.Dock = DockStyle.Bottom;
            uiLine1.Font = new Font("Microsoft Sans Serif", 12F);
            uiLine1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLine1.LineColor = Color.Gray;
            uiLine1.Location = new Point(60, 74);
            uiLine1.MinimumSize = new Size(1, 1);
            uiLine1.Name = "uiLine1";
            uiLine1.Size = new Size(284, 3);
            uiLine1.TabIndex = 4;
            // 
            // ucLedLineConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            Controls.Add(uiLine1);
            Controls.Add(panel2);
            Controls.Add(lblStepName);
            Controls.Add(btnCancel1);
            Margin = new Padding(0);
            Name = "ucLedLineConfig";
            Size = new Size(390, 77);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numDelayTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStepName;
        private Panel panel2;
        private Label label2;
        private Label label1;
        private Button btnCancel1;
        private Guna.UI2.WinForms.Guna2NumericUpDown numDelayTime;
        private Sunny.UI.UILine uiLine1;
    }
}
