namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    partial class ucLaneOutMediumPreview
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelLaneTitle = new Guna.UI2.WinForms.Guna2Panel();
            kzuI_UcLaneTitle1 = new KZUI_UcLaneTitle();
            panelUcResult = new Panel();
            UcResult = new KZUI_UcResult();
            panelAppFunction = new Panel();
            kzuI_Function1 = new KZUI_Function();
            panelPlateIn = new Panel();
            panelBack = new Guna.UI2.WinForms.Guna2Panel();
            splitContainerDisplayDirection = new Sunny.UI.UISplitContainer();
            splitContainerCameraPic = new Sunny.UI.UISplitContainer();
            UcCameraList = new KZUI_UcCameraList();
            UcEventImageListOut = new KZUI_UcImageListInOutMerge();
            splitContainerEventDirection = new Sunny.UI.UISplitContainer();
            panelLpr = new KZUI_DirectionPanel();
            panelPlateOut = new Panel();
            ucEventInfoNew = new Kztek.Control8.UserControls.ucDataGridViewInfo.ucExitInfor();
            panelMain = new Guna.UI2.WinForms.Guna2Panel();
            panelLaneTitle.SuspendLayout();
            panelUcResult.SuspendLayout();
            panelAppFunction.SuspendLayout();
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
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelLaneTitle
            // 
            panelLaneTitle.BorderRadius = 8;
            panelLaneTitle.Controls.Add(kzuI_UcLaneTitle1);
            panelLaneTitle.CustomizableEdges = customizableEdges1;
            panelLaneTitle.Dock = DockStyle.Top;
            panelLaneTitle.FillColor = Color.White;
            panelLaneTitle.Location = new Point(0, 0);
            panelLaneTitle.Margin = new Padding(0);
            panelLaneTitle.Name = "panelLaneTitle";
            panelLaneTitle.ShadowDecoration.CustomizableEdges = customizableEdges2;
            panelLaneTitle.Size = new Size(442, 40);
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
            kzuI_UcLaneTitle1.Size = new Size(442, 40);
            kzuI_UcLaneTitle1.TabIndex = 0;
            // 
            // panelUcResult
            // 
            panelUcResult.BackColor = Color.White;
            panelUcResult.Controls.Add(UcResult);
            panelUcResult.Dock = DockStyle.Top;
            panelUcResult.Location = new Point(0, 0);
            panelUcResult.Margin = new Padding(0);
            panelUcResult.MaximumSize = new Size(0, 40);
            panelUcResult.MinimumSize = new Size(0, 40);
            panelUcResult.Name = "panelUcResult";
            panelUcResult.Size = new Size(434, 40);
            panelUcResult.TabIndex = 31;
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
            UcResult.TabIndex = 0;
            // 
            // panelAppFunction
            // 
            panelAppFunction.BackColor = Color.White;
            panelAppFunction.Controls.Add(kzuI_Function1);
            panelAppFunction.Dock = DockStyle.Bottom;
            panelAppFunction.Location = new Point(4, 531);
            panelAppFunction.Margin = new Padding(0);
            panelAppFunction.MaximumSize = new Size(0, 40);
            panelAppFunction.MinimumSize = new Size(0, 40);
            panelAppFunction.Name = "panelAppFunction";
            panelAppFunction.Size = new Size(434, 40);
            panelAppFunction.TabIndex = 32;
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
            kzuI_Function1.TabIndex = 0;
            // 
            // panelPlateIn
            // 
            panelPlateIn.Dock = DockStyle.Left;
            panelPlateIn.Location = new Point(0, 0);
            panelPlateIn.Margin = new Padding(0);
            panelPlateIn.Name = "panelPlateIn";
            panelPlateIn.Padding = new Padding(0, 0, 4, 0);
            panelPlateIn.Size = new Size(101, 196);
            panelPlateIn.TabIndex = 33;
            // 
            // panelBack
            // 
            panelBack.BorderColor = Color.FromArgb(183, 29, 24);
            panelBack.BorderRadius = 8;
            panelBack.BorderThickness = 1;
            panelBack.Controls.Add(splitContainerDisplayDirection);
            panelBack.Controls.Add(panelAppFunction);
            customizableEdges5.TopLeft = false;
            customizableEdges5.TopRight = false;
            panelBack.CustomizableEdges = customizableEdges5;
            panelBack.Dock = DockStyle.Fill;
            panelBack.FillColor = Color.White;
            panelBack.Location = new Point(0, 40);
            panelBack.Margin = new Padding(0);
            panelBack.Name = "panelBack";
            panelBack.Padding = new Padding(4);
            panelBack.ShadowDecoration.CustomizableEdges = customizableEdges6;
            panelBack.Size = new Size(442, 575);
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
            splitContainerDisplayDirection.Size = new Size(434, 527);
            splitContainerDisplayDirection.SplitterDistance = 280;
            splitContainerDisplayDirection.SplitterWidth = 11;
            splitContainerDisplayDirection.TabIndex = 44;
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
            // 
            // splitContainerCameraPic.Panel1
            // 
            splitContainerCameraPic.Panel1.Controls.Add(UcCameraList);
            // 
            // splitContainerCameraPic.Panel2
            // 
            splitContainerCameraPic.Panel2.Controls.Add(UcEventImageListOut);
            splitContainerCameraPic.Size = new Size(434, 280);
            splitContainerCameraPic.SplitterDistance = 199;
            splitContainerCameraPic.SplitterWidth = 11;
            splitContainerCameraPic.TabIndex = 43;
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
            UcCameraList.Size = new Size(199, 280);
            UcCameraList.TabIndex = 0;
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
            UcEventImageListOut.Size = new Size(224, 280);
            UcEventImageListOut.TabIndex = 0;
            // 
            // splitContainerEventDirection
            // 
            splitContainerEventDirection.ArrowColor = Color.Red;
            splitContainerEventDirection.BarColor = Color.White;
            splitContainerEventDirection.Dock = DockStyle.Fill;
            splitContainerEventDirection.HandleColor = Color.White;
            splitContainerEventDirection.HandleHoverColor = Color.FromArgb(255, 192, 192);
            splitContainerEventDirection.Location = new Point(0, 40);
            splitContainerEventDirection.Margin = new Padding(0);
            splitContainerEventDirection.MinimumSize = new Size(20, 20);
            splitContainerEventDirection.Name = "splitContainerEventDirection";
            // 
            // splitContainerEventDirection.Panel1
            // 
            splitContainerEventDirection.Panel1.Controls.Add(panelLpr);
            // 
            // splitContainerEventDirection.Panel2
            // 
            splitContainerEventDirection.Panel2.Controls.Add(ucEventInfoNew);
            splitContainerEventDirection.Size = new Size(434, 196);
            splitContainerEventDirection.SplitterDistance = 199;
            splitContainerEventDirection.SplitterWidth = 11;
            splitContainerEventDirection.TabIndex = 41;
            // 
            // panelLpr
            // 
            panelLpr.Controls.Add(panelPlateOut);
            panelLpr.Controls.Add(panelPlateIn);
            panelLpr.CustomizableEdges = customizableEdges3;
            panelLpr.CustomSpacing = "0,0,0,0";
            panelLpr.Dock = DockStyle.Fill;
            panelLpr.KZUI_ControlDirection = EmControlDirection.HORIZONTAL;
            panelLpr.Location = new Point(0, 0);
            panelLpr.Name = "panelLpr";
            panelLpr.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelLpr.Size = new Size(199, 196);
            panelLpr.SpaceBetween = true;
            panelLpr.Spacing = 4;
            panelLpr.TabIndex = 36;
            // 
            // panelPlateOut
            // 
            panelPlateOut.Dock = DockStyle.Left;
            panelPlateOut.Location = new Point(101, 0);
            panelPlateOut.Margin = new Padding(0);
            panelPlateOut.Name = "panelPlateOut";
            panelPlateOut.Size = new Size(97, 196);
            panelPlateOut.TabIndex = 35;
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
            ucEventInfoNew.Size = new Size(224, 196);
            ucEventInfoNew.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(226, 238, 255);
            panelMain.BorderColor = Color.FromArgb(17, 141, 87);
            panelMain.BorderRadius = 8;
            panelMain.BorderThickness = 1;
            panelMain.Controls.Add(panelBack);
            panelMain.Controls.Add(panelLaneTitle);
            panelMain.CustomizableEdges = customizableEdges7;
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelMain.Size = new Size(442, 615);
            panelMain.TabIndex = 28;
            // 
            // ucLaneOutMediumPreview
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Margin = new Padding(0);
            Name = "ucLaneOutMediumPreview";
            Size = new Size(442, 615);
            panelLaneTitle.ResumeLayout(false);
            panelUcResult.ResumeLayout(false);
            panelAppFunction.ResumeLayout(false);
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
            panelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelLaneTitle;
        private Panel panelUcResult;
        private Panel panelAppFunction;
        private Panel panelPlateIn;
        private Guna.UI2.WinForms.Guna2Panel panelBack;
        private Sunny.UI.UISplitContainer splitContainerDisplayDirection;
        private Sunny.UI.UISplitContainer splitContainerCameraPic;
        private Sunny.UI.UISplitContainer splitContainerEventDirection;
        private KZUI_DirectionPanel panelLpr;
        private Panel panelPlateOut;
        private Guna.UI2.WinForms.Guna2Panel panelMain;
        private KZUI_UcResult UcResult;
        private KZUI_UcCameraList UcCameraList;
        private ucDataGridViewInfo.ucExitInfor ucEventInfoNew;
        private KZUI_UcImageListInOutMerge UcEventImageListOut;
        private KZUI_UcLaneTitle kzuI_UcLaneTitle1;
        private KZUI_Function kzuI_Function1;
    }
}
