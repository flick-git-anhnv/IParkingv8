namespace ILedv8.UserControls
{
    partial class ucLedLine
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
            components = new System.ComponentModel.Container();
            lblLineName = new Label();
            lblCount = new Label();
            pictureBox1 = new PictureBox();
            toolTip1 = new ToolTip(components);
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblLineName
            // 
            lblLineName.Dock = DockStyle.Left;
            lblLineName.Font = new Font("Segoe UI", 16F);
            lblLineName.Location = new Point(0, 0);
            lblLineName.Name = "lblLineName";
            lblLineName.Size = new Size(96, 67);
            lblLineName.TabIndex = 0;
            lblLineName.Text = "Dòng 1";
            lblLineName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            lblCount.Dock = DockStyle.Fill;
            lblCount.Font = new Font("Segoe UI", 24F);
            lblCount.Location = new Point(96, 0);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(184, 67);
            lblCount.TabIndex = 0;
            lblCount.Text = "0000";
            lblCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Right;
            pictureBox1.Image = Properties.Resources.ball_green;
            pictureBox1.Location = new Point(280, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(44, 67);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 67);
            panel1.Name = "panel1";
            panel1.Size = new Size(324, 2);
            panel1.TabIndex = 2;
            // 
            // ucLedLine
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblCount);
            Controls.Add(pictureBox1);
            Controls.Add(lblLineName);
            Controls.Add(panel1);
            Name = "ucLedLine";
            Size = new Size(324, 69);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblLineName;
        private Label lblCount;
        private PictureBox pictureBox1;
        private ToolTip toolTip1;
        private Panel panel1;
    }
}
