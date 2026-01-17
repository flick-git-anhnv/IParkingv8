namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    partial class ucLaneOutSmallPreview
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            panelBack = new Guna.UI2.WinForms.Guna2Panel();
            splitContainerDisplayDirection = new Sunny.UI.UISplitContainer();
            splitContainerCameraPic = new Sunny.UI.UISplitContainer();
            splitContainerEventDirection = new Sunny.UI.UISplitContainer();
            panelLpr = new TableLayoutPanel();
            panelPlateIn = new Panel();
            panelPlateOut = new Panel();
            panelUcResult = new Panel();
            panelAppFunction = new Panel();
            panelLaneTitle = new Guna.UI2.WinForms.Guna2Panel();
            kzuI_UcLaneTitle1 = new KZUI_UcLaneTitle();
            UcCameraList = new KZUI_UcCameraList();
            UcEventImageListOut = new KZUI_UcImageListInOutMerge();
            UcResult = new KZUI_UcResult();
            kzuI_Function1 = new KZUI_Function();
            ucEventInfoNew = new Kztek.Control8.UserControls.ucDataGridViewInfo.ucExitInfor();
            panelMain.SuspendLayout();
            panelBack.SuspendLayout();
            (splitContainerDisplayDirection).BeginInit();
            splitContainerDisplayDirection.Panel1.SuspendLayout();
            splitContainerDisplayDirection.Panel2.SuspendLayout();
            splitContainerDisplayDirection.SuspendLayout();
            (splitContainerCameraPic).BeginInit();
            splitContainerCameraPic.Panel1.SuspendLayout();
            splitContainerCameraPic.Panel2.SuspendLayout();
            splitContainerCameraPic.SuspendLayout();
            (splitContainerEventDirection).BeginInit();
            splitContainerEventDirection.Panel1.SuspendLayout();
            splitContainerEventDirection.Panel2.SuspendLayout();
            splitContainerEventDirection.SuspendLayout();
            panelLpr.SuspendLayout();
            panelUcResult.SuspendLayout();
            panelAppFunction.SuspendLayout();
            panelLaneTitle.SuspendLayout();
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
            panelMain.CustomizableEdges = customizableEdges11;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelMain.Size = new Size(442, 615);
            panelMain.TabIndex = 27;
            // 
            // panelBack
            // 
            panelBack.BorderRadius = 8;
            panelBack.BorderThickness = 1;
            panelBack.Controls.Add(splitContainerDisplayDirection);
            panelBack.Controls.Add(panelAppFunction);
            customizableEdges7.TopLeft = false;
            customizableEdges7.TopRight = false;
            panelBack.CustomizableEdges = customizableEdges7;
            panelBack.Dock = DockStyle.Fill;
            panelBack.FillColor = Color.White;
            panelBack.Location = new Point(0, 24);
            panelBack.Margin = new Padding(0);
            panelBack.Name = "panelBack";
            panelBack.Padding = new Padding(4);
            panelBack.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            // 
            // splitContainerCameraPic.Panel1
            // 
            splitContainerCameraPic.Panel1.Controls.Add(UcCameraList);
            // 
            // splitContainerCameraPic.Panel2
            // 
            splitContainerCameraPic.Panel2.Controls.Add(UcEventImageListOut);
            splitContainerCameraPic.Size = new Size(434, 230);
            splitContainerCameraPic.SplitterDistance = 104;
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
            // 
            // splitContainerEventDirection.Panel2
            // 
            splitContainerEventDirection.Panel2.Controls.Add(ucEventInfoNew);
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
            panelUcResult.Controls.Add(UcResult);
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
            panelAppFunction.Controls.Add(kzuI_Function1);
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
            panelLaneTitle.Controls.Add(kzuI_UcLaneTitle1);
            panelLaneTitle.CustomizableEdges = customizableEdges9;
            panelLaneTitle.Dock = DockStyle.Top;
            panelLaneTitle.FillColor = Color.White;
            panelLaneTitle.Location = new Point(0, 0);
            panelLaneTitle.Margin = new Padding(0);
            panelLaneTitle.Name = "panelLaneTitle";
            panelLaneTitle.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelLaneTitle.Size = new Size(442, 24);
            panelLaneTitle.TabIndex = 34;
            // 
            // kzuI_UcLaneTitle1
            // 
            kzuI_UcLaneTitle1.BackColor = Color.FromArgb(226, 238, 255);
            kzuI_UcLaneTitle1.Dock = DockStyle.Fill;
            kzuI_UcLaneTitle1.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            kzuI_UcLaneTitle1.KZUI_Title = "Làn ra";
            kzuI_UcLaneTitle1.Location = new Point(0, 0);
            kzuI_UcLaneTitle1.Margin = new Padding(0);
            kzuI_UcLaneTitle1.Name = "kzuI_UcLaneTitle1";
            kzuI_UcLaneTitle1.Size = new Size(442, 24);
            kzuI_UcLaneTitle1.TabIndex = 1;
            // 
            // UcCameraList
            // 
            UcCameraList.BackColor = Color.White;
            UcCameraList.Dock = DockStyle.Fill;
            UcCameraList.KZUI_ControlDirection = EmControlDirection.VERTICAL;
            UcCameraList.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            UcCameraList.KZUI_IsDisplayTitle = true;
            UcCameraList.KZUI_Spacing = 4;
            UcCameraList.KZUI_Title = "CAMERA LỐI VÀO";
            UcCameraList.Location = new Point(0, 0);
            UcCameraList.Name = "UcCameraList";
            UcCameraList.Size = new Size(434, 104);
            UcCameraList.TabIndex = 1;
            // 
            // UcEventImageListOut
            // 
            UcEventImageListOut.BackColor = Color.White;
            UcEventImageListOut.Dock = DockStyle.Fill;
            UcEventImageListOut.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            UcEventImageListOut.KZUI_IsDisplayFace = true;
            UcEventImageListOut.KZUI_IsDisplayOther = true;
            UcEventImageListOut.KZUI_IsDisplayPanorama = true;
            UcEventImageListOut.KZUI_IsDisplayTitle = true;
            UcEventImageListOut.KZUI_IsDisplayVehicle = true;
            UcEventImageListOut.Location = new Point(0, 0);
            UcEventImageListOut.Margin = new Padding(0);
            UcEventImageListOut.Name = "UcEventImageListOut";
            UcEventImageListOut.Size = new Size(434, 115);
            UcEventImageListOut.TabIndex = 1;
            // 
            // UcResult
            // 
            UcResult.BackColor = Color.White;
            UcResult.Dock = DockStyle.Fill;
            UcResult.ForeColor = Color.White;
            UcResult.KZUI_ResultType = KZUI_UcResult.EmResultType.ERROR;
            UcResult.Location = new Point(0, 0);
            UcResult.Margin = new Padding(0);
            UcResult.Name = "UcResult";
            UcResult.Size = new Size(434, 40);
            UcResult.TabIndex = 1;
            // 
            // kzuI_Function1
            // 
            kzuI_Function1.BackColor = Color.White;
            kzuI_Function1.Dock = DockStyle.Bottom;
            kzuI_Function1.KZUI_ControlSizeMode = EmControlSizeMode.MEDIUM;
            kzuI_Function1.KZUI_IsDisplayCloseBarrie = true;
            kzuI_Function1.KZUI_IsDisplayGuestRegister = true;
            kzuI_Function1.KZUI_IsDisplayOpenBarrie = true;
            kzuI_Function1.KZUI_IsDisplayPrint = false;
            kzuI_Function1.KZUI_IsDisplayRetakeImage = true;
            kzuI_Function1.KZUI_IsDisplayWriteTicket = true;
            kzuI_Function1.KZUI_LaneType = iParkingv8.Object.Enums.Bases.EmLaneType.LANE_IN;
            kzuI_Function1.Location = new Point(0, 8);
            kzuI_Function1.Margin = new Padding(0);
            kzuI_Function1.MaximumSize = new Size(0, 32);
            kzuI_Function1.MinimumSize = new Size(0, 32);
            kzuI_Function1.Name = "kzuI_Function1";
            kzuI_Function1.Size = new Size(434, 32);
            kzuI_Function1.TabIndex = 1;
            // 
            // ucEventInfoNew
            // 
            ucEventInfoNew.BackColor = Color.White;
            ucEventInfoNew.Dock = DockStyle.Fill;
            ucEventInfoNew.KZUI_ControlSizeMode = EmControlSizeMode.SMALL;
            ucEventInfoNew.KZUI_IsDisplayMoney = true;
            ucEventInfoNew.KZUI_IsDisplayTitle = true;
            ucEventInfoNew.KZUI_Title = "THÔNG TIN SỰ KIỆN";
            ucEventInfoNew.Location = new Point(0, 0);
            ucEventInfoNew.Margin = new Padding(0);
            ucEventInfoNew.Name = "ucEventInfoNew";
            ucEventInfoNew.Size = new Size(434, 122);
            ucEventInfoNew.TabIndex = 1;
            // 
            // ucLaneOutSmallPreview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Name = "ucLaneOutSmallPreview";
            Size = new Size(442, 615);
            panelMain.ResumeLayout(false);
            panelBack.ResumeLayout(false);
            splitContainerDisplayDirection.Panel1.ResumeLayout(false);
            splitContainerDisplayDirection.Panel2.ResumeLayout(false);
            (splitContainerDisplayDirection).EndInit();
            splitContainerDisplayDirection.ResumeLayout(false);
            splitContainerCameraPic.Panel1.ResumeLayout(false);
            splitContainerCameraPic.Panel2.ResumeLayout(false);
            (splitContainerCameraPic).EndInit();
            splitContainerCameraPic.ResumeLayout(false);
            splitContainerEventDirection.Panel1.ResumeLayout(false);
            splitContainerEventDirection.Panel2.ResumeLayout(false);
            (splitContainerEventDirection).EndInit();
            splitContainerEventDirection.ResumeLayout(false);
            panelLpr.ResumeLayout(false);
            panelUcResult.ResumeLayout(false);
            panelAppFunction.ResumeLayout(false);
            panelLaneTitle.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private Guna.UI2.WinForms.Guna2Panel panelBack;
        private Sunny.UI.UISplitContainer splitContainerDisplayDirection;
        private Sunny.UI.UISplitContainer splitContainerCameraPic;
        private Sunny.UI.UISplitContainer splitContainerEventDirection;
        private TableLayoutPanel panelLpr;
        private Panel panelPlateIn;
        private Panel panelPlateOut;
        private Panel panelUcResult;
        private Panel panelAppFunction;
        private Guna.UI2.WinForms.Guna2Panel panelLaneTitle;
        private KZUI_UcLaneTitle kzuI_UcLaneTitle1;
        private KZUI_UcCameraList UcCameraList;
        private KZUI_UcImageListInOutMerge UcEventImageListOut;
        private KZUI_UcResult UcResult;
        private KZUI_Function kzuI_Function1;
        private ucDataGridViewInfo.ucExitInfor ucEventInfoNew;
    }
}
