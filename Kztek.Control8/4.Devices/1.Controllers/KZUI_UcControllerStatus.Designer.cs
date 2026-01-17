namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcControllerStatus
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
            lblName = new Label();
            iconPic = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)iconPic).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Consolas", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblName.Location = new Point(24, 0);
            lblName.Margin = new Padding(0);
            lblName.Name = "lblName";
            lblName.Size = new Size(115, 32);
            lblName.TabIndex = 1;
            lblName.Text = "BDK";
            lblName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // iconPic
            // 
            iconPic.BackColor = Color.White;
            iconPic.Dock = DockStyle.Left;
            iconPic.ForeColor = Color.Green;
            iconPic.IconChar = FontAwesome.Sharp.IconChar.Circle;
            iconPic.IconColor = Color.Green;
            iconPic.IconFont = FontAwesome.Sharp.IconFont.Solid;
            iconPic.IconSize = 24;
            iconPic.Location = new Point(0, 0);
            iconPic.Margin = new Padding(0);
            iconPic.Name = "iconPic";
            iconPic.Size = new Size(24, 32);
            iconPic.SizeMode = PictureBoxSizeMode.CenterImage;
            iconPic.TabIndex = 2;
            iconPic.TabStop = false;
            iconPic.UseGdi = true;
            // 
            // KZUI_UcControllerStatus
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblName);
            Controls.Add(iconPic);
            Margin = new Padding(0);
            Name = "KZUI_UcControllerStatus";
            Size = new Size(139, 32);
            ((System.ComponentModel.ISupportInitialize)iconPic).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblName;
        private FontAwesome.Sharp.IconPictureBox iconPic;
    }
}
