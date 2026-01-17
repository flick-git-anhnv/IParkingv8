using Guna.UI2.WinForms;

namespace IParkingv8.LaneUIs
{
    partial class UcOutSmallSize
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna2Panel();
            panelBack = new Guna2Panel();
            splitContainerDisplayDirection = new Sunny.UI.UISplitContainer();
            splitContainerCameraPic = new Sunny.UI.UISplitContainer();
            splitContainerEventDirection = new Sunny.UI.UISplitContainer();
            panelLpr = new TableLayoutPanel();
            panelPlateIn = new Panel();
            panelPlateOut = new Panel();
            panelUcResult = new Panel();
            panelAppFunction = new Panel();
            panelLaneTitle = new Guna2Panel();
            panelMain.SuspendLayout();
            panelBack.SuspendLayout();
            (splitContainerDisplayDirection).BeginInit();
            splitContainerDisplayDirection.Panel1.SuspendLayout();
            splitContainerDisplayDirection.Panel2.SuspendLayout();
            splitContainerDisplayDirection.SuspendLayout();
            (splitContainerCameraPic).BeginInit();
            splitContainerCameraPic.SuspendLayout();
            (splitContainerEventDirection).BeginInit();
            splitContainerEventDirection.Panel1.SuspendLayout();
            splitContainerEventDirection.SuspendLayout();
            panelLpr.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(226, 238, 255);
            panelMain.BorderColor = Color.FromArgb(183, 29, 24);
            panelMain.BorderRadius = 8;
            panelMain.BorderThickness = 1;
            panelMain.Controls.Add(panelBack);
            panelMain.Controls.Add(panelLaneTitle);
            panelMain.CustomizableEdges = customizableEdges5;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelMain.Size = new Size(442, 615);
            panelMain.TabIndex = 26;
            // 
            // panelBack
            // 
            panelBack.BorderRadius = 8;
            panelBack.BorderThickness = 1;
            panelBack.Controls.Add(splitContainerDisplayDirection);
            panelBack.Controls.Add(panelAppFunction);
            customizableEdges1.TopLeft = false;
            customizableEdges1.TopRight = false;
            panelBack.CustomizableEdges = customizableEdges1;
            panelBack.Dock = DockStyle.Fill;
            panelBack.FillColor = Color.White;
            panelBack.Location = new Point(0, 24);
            panelBack.Margin = new Padding(0);
            panelBack.Name = "panelBack";
            panelBack.Padding = new Padding(4);
            panelBack.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelBack.Size = new Size(442, 591);
            panelBack.TabIndex = 35;
            // 
            // splitContainerDisplayDirection
            // 
            splitContainerDisplayDirection.ArrowColor = Color.Red;
            splitContainerDisplayDirection.BackColor = Color.White;
            splitContainerDisplayDirection.BarColor = Color.White;
            splitContainerDisplayDirection.Dock = DockStyle.Fill;
            splitContainerDisplayDirection.HandleColor = Color.White;
            splitContainerDisplayDirection.HandleHoverColor = Color.FromArgb(255, 192, 192);
            splitContainerDisplayDirection.Location = new Point(4, 4);
            splitContainerDisplayDirection.Margin = new Padding(0);
            splitContainerDisplayDirection.MinimumSize = new Size(20, 20);
            splitContainerDisplayDirection.Name = "splitContainerDisplayDirection";
            splitContainerDisplayDirection.Orientation = Orientation.Horizontal;
            // 
            // splitContainerDisplayDirection.Panel1
            // 
            splitContainerDisplayDirection.Panel1.Controls.Add(splitContainerCameraPic);
            // 
            // splitContainerDisplayDirection.Panel2
            // 
            splitContainerDisplayDirection.Panel2.Controls.Add(splitContainerEventDirection);
            splitContainerDisplayDirection.Panel2.Controls.Add(panelUcResult);
            splitContainerDisplayDirection.Size = new Size(434, 543);
            splitContainerDisplayDirection.SplitterDistance = 230;
            splitContainerDisplayDirection.SplitterWidth = 11;
            splitContainerDisplayDirection.TabIndex = 46;
            // 
            // splitContainerCameraPic
            // 
            splitContainerCameraPic.ArrowColor = Color.Red;
            splitContainerCameraPic.BackColor = Color.White;
            splitContainerCameraPic.BarColor = Color.White;
            splitContainerCameraPic.Dock = DockStyle.Fill;
            splitContainerCameraPic.HandleColor = Color.White;
            splitContainerCameraPic.HandleHoverColor = Color.FromArgb(255, 192, 192);
            splitContainerCameraPic.Location = new Point(0, 0);
            splitContainerCameraPic.Margin = new Padding(0);
            splitContainerCameraPic.MinimumSize = new Size(20, 20);
            splitContainerCameraPic.Name = "splitContainerCameraPic";
            splitContainerCameraPic.Orientation = Orientation.Horizontal;
            splitContainerCameraPic.Size = new Size(434, 230);
            splitContainerCameraPic.SplitterDistance = 105;
            splitContainerCameraPic.SplitterWidth = 11;
            splitContainerCameraPic.TabIndex = 44;
            // 
            // splitContainerEventDirection
            // 
            splitContainerEventDirection.ArrowColor = Color.Red;
            splitContainerEventDirection.BackColor = Color.White;
            splitContainerEventDirection.BarColor = Color.White;
            splitContainerEventDirection.Dock = DockStyle.Fill;
            splitContainerEventDirection.HandleColor = Color.White;
            splitContainerEventDirection.HandleHoverColor = Color.FromArgb(255, 192, 192);
            splitContainerEventDirection.Location = new Point(0, 40);
            splitContainerEventDirection.Margin = new Padding(0);
            splitContainerEventDirection.MinimumSize = new Size(20, 20);
            splitContainerEventDirection.Name = "splitContainerEventDirection";
            splitContainerEventDirection.Orientation = Orientation.Horizontal;
            // 
            // splitContainerEventDirection.Panel1
            // 
            splitContainerEventDirection.Panel1.Controls.Add(panelLpr);
            splitContainerEventDirection.Size = new Size(434, 262);
            splitContainerEventDirection.SplitterDistance = 129;
            splitContainerEventDirection.SplitterWidth = 11;
            splitContainerEventDirection.TabIndex = 45;
            // 
            // panelLpr
            // 
            panelLpr.ColumnCount = 3;
            panelLpr.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelLpr.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 4F));
            panelLpr.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panelLpr.Controls.Add(panelPlateIn, 0, 0);
            panelLpr.Controls.Add(panelPlateOut, 2, 0);
            panelLpr.Dock = DockStyle.Fill;
            panelLpr.Location = new Point(0, 0);
            panelLpr.Margin = new Padding(0);
            panelLpr.Name = "panelLpr";
            panelLpr.RowCount = 1;
            panelLpr.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            panelLpr.Size = new Size(434, 129);
            panelLpr.TabIndex = 1;
            // 
            // panelPlateIn
            // 
            panelPlateIn.Dock = DockStyle.Fill;
            panelPlateIn.Location = new Point(0, 0);
            panelPlateIn.Margin = new Padding(0);
            panelPlateIn.Name = "panelPlateIn";
            panelPlateIn.Size = new Size(215, 129);
            panelPlateIn.TabIndex = 0;
            // 
            // panelPlateOut
            // 
            panelPlateOut.Dock = DockStyle.Fill;
            panelPlateOut.Location = new Point(219, 0);
            panelPlateOut.Margin = new Padding(0);
            panelPlateOut.Name = "panelPlateOut";
            panelPlateOut.Size = new Size(215, 129);
            panelPlateOut.TabIndex = 1;
            // 
            // panelUcResult
            // 
            panelUcResult.BackColor = Color.White;
            panelUcResult.Dock = DockStyle.Top;
            panelUcResult.Location = new Point(0, 0);
            panelUcResult.Margin = new Padding(0);
            panelUcResult.Name = "panelUcResult";
            panelUcResult.Size = new Size(434, 40);
            panelUcResult.TabIndex = 31;
            // 
            // panelAppFunction
            // 
            panelAppFunction.BackColor = Color.White;
            panelAppFunction.Dock = DockStyle.Bottom;
            panelAppFunction.Location = new Point(4, 547);
            panelAppFunction.Margin = new Padding(0);
            panelAppFunction.MaximumSize = new Size(0, 40);
            panelAppFunction.Name = "panelAppFunction";
            panelAppFunction.Size = new Size(434, 40);
            panelAppFunction.TabIndex = 38;
            // 
            // panelLaneTitle
            // 
            panelLaneTitle.BorderRadius = 8;
            panelLaneTitle.CustomizableEdges = customizableEdges3;
            panelLaneTitle.Dock = DockStyle.Top;
            panelLaneTitle.FillColor = Color.White;
            panelLaneTitle.Location = new Point(0, 0);
            panelLaneTitle.Margin = new Padding(0);
            panelLaneTitle.Name = "panelLaneTitle";
            panelLaneTitle.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelLaneTitle.Size = new Size(442, 24);
            panelLaneTitle.TabIndex = 34;
            // 
            // UcOutSmallSize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 238, 255);
            Controls.Add(panelMain);
            Margin = new Padding(0);
            Name = "UcOutSmallSize";
            Size = new Size(442, 615);
            panelMain.ResumeLayout(false);
            panelBack.ResumeLayout(false);
            splitContainerDisplayDirection.Panel1.ResumeLayout(false);
            splitContainerDisplayDirection.Panel2.ResumeLayout(false);
            (splitContainerDisplayDirection).EndInit();
            splitContainerDisplayDirection.ResumeLayout(false);
            (splitContainerCameraPic).EndInit();
            splitContainerCameraPic.ResumeLayout(false);
            splitContainerEventDirection.Panel1.ResumeLayout(false);
            (splitContainerEventDirection).EndInit();
            splitContainerEventDirection.ResumeLayout(false);
            panelLpr.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Panel panelUcResult;
        private Guna.UI2.WinForms.Guna2Panel panelLaneTitle;
        private Guna.UI2.WinForms.Guna2Panel panelBack;
        private TableLayoutPanel panelLpr;
        private Panel panelPlateIn;
        private Panel panelPlateOut;
        private Panel panelAppFunction;
        private Sunny.UI.UISplitContainer splitContainerDisplayDirection;
        private Sunny.UI.UISplitContainer splitContainerEventDirection;
        private Sunny.UI.UISplitContainer splitContainerCameraPic;
    }
}