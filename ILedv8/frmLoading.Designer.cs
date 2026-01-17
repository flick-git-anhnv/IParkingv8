namespace ILedv8.Forms
{
    partial class FrmLoading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoading));
            lblMessage = new Label();
            timerDisplayLoadingMessage = new System.Windows.Forms.Timer(components);
            timerLoading = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.Blue;
            lblMessage.Location = new Point(8, 416);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(784, 32);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "_";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timerDisplayLoadingMessage
            // 
            timerDisplayLoadingMessage.Interval = 300;
            timerDisplayLoadingMessage.Tick += TimerDisplayLoadingMessage_Tick;
            // 
            // timerLoading
            // 
            timerLoading.Tick += TimerLoading_Tick;
            // 
            // FrmLoading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(lblMessage);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmLoading";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tải cấu hình hệ thống";
            ResumeLayout(false);
        }

        #endregion

        private Label lblMessage;
        private System.Windows.Forms.Timer timerDisplayLoadingMessage;
        private System.Windows.Forms.Timer timerLoading;
    }
}