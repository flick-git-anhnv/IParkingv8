namespace Kztek.Control8.UserControls
{
    partial class KZUI_UcLaneTitle
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KZUI_UcLaneTitle));
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            picLoopEvent = new PictureBox();
            picCardEvent = new PictureBox();
            lblTitle = new Label();
            panelPics = new TableLayoutPanel();
            picSetting = new PictureBox();
            picOpenBarrie = new PictureBox();
            picWriteTicket = new PictureBox();
            picRetakeImage = new PictureBox();
            picMotion = new PictureBox();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLoopEvent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picCardEvent).BeginInit();
            panelPics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSetting).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picOpenBarrie).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picWriteTicket).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picRetakeImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMotion).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.White;
            panelMain.BorderColor = Color.FromArgb(17, 141, 87);
            panelMain.BorderRadius = 8;
            panelMain.BorderThickness = 1;
            panelMain.Controls.Add(picMotion);
            panelMain.Controls.Add(picLoopEvent);
            panelMain.Controls.Add(picCardEvent);
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(panelPics);
            customizableEdges1.BottomLeft = false;
            customizableEdges1.BottomRight = false;
            panelMain.CustomizableEdges = customizableEdges1;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.FromArgb(17, 141, 87);
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(8, 4, 8, 4);
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelMain.Size = new Size(768, 32);
            panelMain.TabIndex = 1;
            // 
            // picLoopEvent
            // 
            picLoopEvent.BackColor = Color.Transparent;
            picLoopEvent.Dock = DockStyle.Left;
            picLoopEvent.Image = (Image)resources.GetObject("picLoopEvent.Image");
            picLoopEvent.Location = new Point(112, 4);
            picLoopEvent.Margin = new Padding(0);
            picLoopEvent.Name = "picLoopEvent";
            picLoopEvent.Size = new Size(36, 24);
            picLoopEvent.SizeMode = PictureBoxSizeMode.Zoom;
            picLoopEvent.TabIndex = 14;
            picLoopEvent.TabStop = false;
            picLoopEvent.Visible = false;
            // 
            // picCardEvent
            // 
            picCardEvent.BackColor = Color.Transparent;
            picCardEvent.Dock = DockStyle.Left;
            picCardEvent.Image = (Image)resources.GetObject("picCardEvent.Image");
            picCardEvent.Location = new Point(76, 4);
            picCardEvent.Margin = new Padding(0);
            picCardEvent.Name = "picCardEvent";
            picCardEvent.Size = new Size(36, 24);
            picCardEvent.SizeMode = PictureBoxSizeMode.Zoom;
            picCardEvent.TabIndex = 13;
            picCardEvent.TabStop = false;
            picCardEvent.Visible = false;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(17, 141, 87);
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(8, 4);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(68, 24);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "xxx";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelPics
            // 
            panelPics.AutoSize = true;
            panelPics.BackColor = Color.FromArgb(17, 141, 87);
            panelPics.ColumnCount = 7;
            panelPics.ColumnStyles.Add(new ColumnStyle());
            panelPics.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panelPics.ColumnStyles.Add(new ColumnStyle());
            panelPics.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panelPics.ColumnStyles.Add(new ColumnStyle());
            panelPics.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            panelPics.ColumnStyles.Add(new ColumnStyle());
            panelPics.Controls.Add(picSetting, 6, 0);
            panelPics.Controls.Add(picOpenBarrie, 0, 0);
            panelPics.Controls.Add(picWriteTicket, 4, 0);
            panelPics.Controls.Add(picRetakeImage, 2, 0);
            panelPics.Dock = DockStyle.Right;
            panelPics.Location = new Point(592, 4);
            panelPics.Margin = new Padding(0);
            panelPics.Name = "panelPics";
            panelPics.RowCount = 1;
            panelPics.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelPics.Size = new Size(168, 24);
            panelPics.TabIndex = 12;
            // 
            // picSetting
            // 
            picSetting.BackColor = Color.Transparent;
            picSetting.Dock = DockStyle.Fill;
            picSetting.Image = (Image)resources.GetObject("picSetting.Image");
            picSetting.Location = new Point(132, 0);
            picSetting.Margin = new Padding(0);
            picSetting.Name = "picSetting";
            picSetting.Size = new Size(36, 24);
            picSetting.SizeMode = PictureBoxSizeMode.Zoom;
            picSetting.TabIndex = 2;
            picSetting.TabStop = false;
            // 
            // picOpenBarrie
            // 
            picOpenBarrie.BackColor = Color.Transparent;
            picOpenBarrie.Dock = DockStyle.Fill;
            picOpenBarrie.Image = (Image)resources.GetObject("picOpenBarrie.Image");
            picOpenBarrie.Location = new Point(0, 0);
            picOpenBarrie.Margin = new Padding(0);
            picOpenBarrie.Name = "picOpenBarrie";
            picOpenBarrie.Size = new Size(36, 24);
            picOpenBarrie.SizeMode = PictureBoxSizeMode.Zoom;
            picOpenBarrie.TabIndex = 5;
            picOpenBarrie.TabStop = false;
            // 
            // picWriteTicket
            // 
            picWriteTicket.BackColor = Color.Transparent;
            picWriteTicket.Dock = DockStyle.Fill;
            picWriteTicket.Image = (Image)resources.GetObject("picWriteTicket.Image");
            picWriteTicket.Location = new Point(88, 0);
            picWriteTicket.Margin = new Padding(0);
            picWriteTicket.Name = "picWriteTicket";
            picWriteTicket.Size = new Size(36, 24);
            picWriteTicket.SizeMode = PictureBoxSizeMode.Zoom;
            picWriteTicket.TabIndex = 3;
            picWriteTicket.TabStop = false;
            // 
            // picRetakeImage
            // 
            picRetakeImage.BackColor = Color.Transparent;
            picRetakeImage.Dock = DockStyle.Fill;
            picRetakeImage.Image = (Image)resources.GetObject("picRetakeImage.Image");
            picRetakeImage.Location = new Point(44, 0);
            picRetakeImage.Margin = new Padding(0);
            picRetakeImage.Name = "picRetakeImage";
            picRetakeImage.Size = new Size(36, 24);
            picRetakeImage.SizeMode = PictureBoxSizeMode.Zoom;
            picRetakeImage.TabIndex = 4;
            picRetakeImage.TabStop = false;
            // 
            // picMotion
            // 
            picMotion.BackColor = Color.Transparent;
            picMotion.Dock = DockStyle.Left;
            picMotion.Image = (Image)resources.GetObject("picMotion.Image");
            picMotion.Location = new Point(148, 4);
            picMotion.Margin = new Padding(0);
            picMotion.Name = "picMotion";
            picMotion.Size = new Size(36, 24);
            picMotion.SizeMode = PictureBoxSizeMode.Zoom;
            picMotion.TabIndex = 15;
            picMotion.TabStop = false;
            picMotion.Visible = false;
            // 
            // KZUI_UcLaneTitle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(panelMain);
            Margin = new Padding(0);
            Name = "KZUI_UcLaneTitle";
            Size = new Size(768, 32);
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLoopEvent).EndInit();
            ((System.ComponentModel.ISupportInitialize)picCardEvent).EndInit();
            panelPics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picSetting).EndInit();
            ((System.ComponentModel.ISupportInitialize)picOpenBarrie).EndInit();
            ((System.ComponentModel.ISupportInitialize)picWriteTicket).EndInit();
            ((System.ComponentModel.ISupportInitialize)picRetakeImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMotion).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Label lblTitle;
        private PictureBox picSetting;
        private PictureBox picWriteTicket;
        private PictureBox picRetakeImage;
        private PictureBox picOpenBarrie;
        private TableLayoutPanel panelPics;
        private PictureBox picLoopEvent;
        private PictureBox picCardEvent;
        private PictureBox picMotion;
    }
}
