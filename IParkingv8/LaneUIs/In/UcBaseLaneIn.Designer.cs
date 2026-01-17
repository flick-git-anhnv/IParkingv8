namespace IParkingv8.LaneUIs
{
    partial class UcBaseLaneIn
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
            timerCheckAllowOpenBarrie = new System.Windows.Forms.Timer(components);
            timerRefreshUI = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // timerCheckAllowOpenBarrie
            // 
            timerCheckAllowOpenBarrie.Interval = 1000;
            timerCheckAllowOpenBarrie.Tick += TimerCheckAllowOpenBarrie_Tick;
            // 
            // timerRefreshUI
            // 
            timerRefreshUI.Interval = 1000;
            timerRefreshUI.Tick += TimerRefreshUI_Tick;
            // 
            // UcBaseLaneIn
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "UcBaseLaneIn";
            Size = new Size(800, 450);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerCheckAllowOpenBarrie;
        private System.Windows.Forms.Timer timerRefreshUI;
    }
}