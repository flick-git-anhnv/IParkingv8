namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    partial class ucKioskTitle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucKioskTitle));
            ucLanguage1 = new UcLanguage();
            picLogo = new PictureBox();
            lblTime = new Label();
            timerUpdateTime = new System.Windows.Forms.Timer(components);
            picLoopEvent = new PictureBox();
            picCardEvent = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picLoopEvent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCardEvent).BeginInit();
            SuspendLayout();
            // 
            // ucLanguage1
            // 
            ucLanguage1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ucLanguage1.BackColor = Color.Transparent;
            ucLanguage1.Location = new Point(929, 19);
            ucLanguage1.Margin = new Padding(0);
            ucLanguage1.Name = "ucLanguage1";
            ucLanguage1.Size = new Size(291, 54);
            ucLanguage1.TabIndex = 11;
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.Location = new Point(540, 12);
            picLogo.Margin = new Padding(0);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(199, 67);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 9;
            picLogo.TabStop = false;
            picLogo.DoubleClick += picLogo_DoubleClick;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTime.Location = new Point(60, 31);
            lblTime.Margin = new Padding(0);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(87, 28);
            lblTime.TabIndex = 10;
            lblTime.Text = "14:24:05";
            lblTime.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timerUpdateTime
            // 
            timerUpdateTime.Interval = 1000;
            // 
            // picLoopEvent
            // 
            picLoopEvent.BackColor = Color.Transparent;
            picLoopEvent.Image = (Image)resources.GetObject("picLoopEvent.Image");
            picLoopEvent.Location = new Point(180, 31);
            picLoopEvent.Margin = new Padding(0);
            picLoopEvent.Name = "picLoopEvent";
            picLoopEvent.Size = new Size(36, 28);
            picLoopEvent.SizeMode = PictureBoxSizeMode.Zoom;
            picLoopEvent.TabIndex = 17;
            picLoopEvent.TabStop = false;
            picLoopEvent.Visible = false;
            // 
            // picCardEvent
            // 
            picCardEvent.BackColor = Color.Transparent;
            picCardEvent.Image = (Image)resources.GetObject("picCardEvent.Image");
            picCardEvent.Location = new Point(144, 31);
            picCardEvent.Margin = new Padding(0);
            picCardEvent.Name = "picCardEvent";
            picCardEvent.Size = new Size(36, 28);
            picCardEvent.SizeMode = PictureBoxSizeMode.Zoom;
            picCardEvent.TabIndex = 16;
            picCardEvent.TabStop = false;
            picCardEvent.Visible = false;
            // 
            // ucKioskTitle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            Controls.Add(picLoopEvent);
            Controls.Add(picCardEvent);
            Controls.Add(ucLanguage1);
            Controls.Add(picLogo);
            Controls.Add(lblTime);
            Margin = new Padding(0);
            Name = "ucKioskTitle";
            Size = new Size(1280, 91);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)picLoopEvent).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCardEvent).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UcLanguage ucLanguage1;
        private PictureBox picLogo;
        private Label lblTime;
        private System.Windows.Forms.Timer timerUpdateTime;
        private PictureBox picLoopEvent;
        private PictureBox picCardEvent;
    }
}
