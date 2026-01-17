using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Control8.UserControls.DialogUcs.UserControlDialogs;
using Kztek.Utilities;
using Sunny.UI;

namespace IParkingv8.LaneUIs._1920x1080
{
    public partial class UcInMediumSize : UcBaseLaneIn
    {
        public List<UISplitContainer> activeSpliters = [];

        public UcInMediumSize(Lane lane, KZUI_UcEventRealtime ucEventRealtime) : base(lane, ucEventRealtime)
        {
            InitializeComponent();
            this.SizeMode = EmControlSizeMode.MEDIUM;
            this.Lane = lane;
            CreateUI();

            activeSpliters =
            [
                splitContainerDisplayDirection,
                splitContainerCameraPic,
                splitContainerEventDirection,
            ];

            foreach (var spliter in activeSpliters)
            {
                spliter.ToggleDoubleBuffered(true);
                spliter.SplitterMoved += Spliter_SplitterMoved;
                spliter.DoubleClick += Spliter_DoubleClick;
            }

            AllowDesignRealtime(false);
        }
        private void Spliter_SplitterMoved(object? sender, SplitterEventArgs e)
        {
            var spliter = sender as SplitContainer;
            if (spliter == null)
            {
                return;
            }

            var splitterMappings = new Dictionary<string, Action<int>>
                {
                    { splitContainerDisplayDirection.Name, distance => AppData.splitterMain = distance},
                    { splitContainerCameraPic.Name, distance => AppData.splitterCamera_Pic = distance  } ,
                    { splitContainerEventDirection.Name, distance => AppData.splitterLpr_DGV = distance }  ,
                };

            if (splitterMappings.TryGetValue(spliter.Name, out var updateAction))
            {
                updateAction(spliter.SplitterDistance);
            }

            spliter.Refresh();
        }
        private void Spliter_DoubleClick(object? sender, EventArgs e)
        {
            var spliter = sender as UISplitContainer;
            if (spliter is null)
            {
                return;
            }
            spliter.SplitterMoved -= Spliter_SplitterMoved;

            splitContainerDisplayDirection.SplitterDistance = AppData.splitterMain;
            splitContainerCameraPic.SplitterDistance = AppData.splitterCamera_Pic;
            splitContainerEventDirection.SplitterDistance = AppData.splitterLpr_DGV;

            spliter.SplitterMoved += Spliter_SplitterMoved;
        }

        public void CreateUI()
        {
            panelBack.BorderColor = ColorManagement.LaneInColor;

            this.dialogConfirmInRegister = new UcConfirmInRegisterCard(this.laneOptionalConfig, defaultImg);
            if (this.SizeMode == EmControlSizeMode.SMALL)
            {
                this.dialogConfirmIn = new UcConfirmInSmall(this.laneOptionalConfig);
            }
            else
            {
                this.dialogConfirmIn = new UcConfirmIn(this.laneOptionalConfig, defaultImg);
            }
            this.MaskedDialog = new MaskedUserControl(this);

            this.panelLaneTitle.Controls.Add(UcLaneTitle);
            UcLaneTitle.Dock = DockStyle.Fill;
            UcLaneTitle.KZUI_ControlSizeMode = SizeMode;

            UcCameraList.Dock = DockStyle.Fill;
            UcCameraList.KZUI_ControlSizeMode = SizeMode;
            UcCameraList.KZUI_ControlDirection = EmControlDirection.VERTICAL;

            ((Control)ucEventImageListIn).Dock = DockStyle.Fill;
            ucEventImageListIn.Init(SizeMode, EmControlDirection.VERTICAL, false);

            this.panelUcResult.Controls.Add(UcResult);
            UcResult.Dock = DockStyle.Fill;
            UcResult.Init(EmControlSizeMode.MEDIUM, AppData.OEMConfig.AppName);
            this.panelUcResult.Size = this.panelUcResult.MinimumSize = this.panelUcResult.MaximumSize = UcResult.MaximumSize;

            ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
            ucEventInfoNew.KZUI_ControlSizeMode = EmControlSizeMode.MEDIUM;

            this.panelAppFunction.Controls.Add(UcAppFunctions);
            UcAppFunctions.Dock = DockStyle.Fill;
            UcAppFunctions.Init(iParkingv8.Object.Enums.Bases.EmLaneType.LANE_IN, EmControlSizeMode.MEDIUM);
            this.panelAppFunction.Size = this.panelAppFunction.MinimumSize = this.panelAppFunction.MaximumSize = UcAppFunctions.MaximumSize;
        }

        public override LaneDisplayConfig GetLaneDisplayConfig()
        {
            AllowDesignRealtime(true);
            var laneDisplayConfig = new LaneDisplayConfig()
            {
                splitterMain = splitContainerDisplayDirection.SplitterDistance,
                splitterCamera_Pic = splitContainerCameraPic.SplitterDistance,
                splitterLpr_DGV = splitContainerEventDirection.SplitterDistance,
            };
            return laneDisplayConfig;
        }

        public override void ChangeLaneDirectionConfig(LaneDirectionConfig config)
        {
            this.laneDirectionConfig = config;
            this.UcLaneTitle.UpdateSetting(this.laneDirectionConfig);

            switch (config.displayDirection)
            {
                case LaneDirectionConfig.EmDisplayDirection.HorizontalLeftToRight:
                    splitContainerDisplayDirection.Orientation = Orientation.Vertical;
                    break;
                case LaneDirectionConfig.EmDisplayDirection.HorizontalRightToLeft:
                    splitContainerDisplayDirection.Orientation = Orientation.Vertical;
                    break;
                case LaneDirectionConfig.EmDisplayDirection.VerTicalLeftToRight:
                    splitContainerDisplayDirection.Orientation = Orientation.Horizontal;
                    break;
                case LaneDirectionConfig.EmDisplayDirection.VerTicalRightToLeft:
                    splitContainerDisplayDirection.Orientation = Orientation.Horizontal;
                    break;
                default:
                    break;
            }

            switch (config.cameraPicDirection)
            {
                case LaneDirectionConfig.EmCameraPicFunction.Vertical:
                    splitContainerCameraPic.Orientation = Orientation.Horizontal;
                    splitContainerCameraPic.Panel1.Controls.Add(UcCameraList);
                    splitContainerCameraPic.Panel2.Controls.Add((Control)ucEventImageListIn);
                    break;
                case LaneDirectionConfig.EmCameraPicFunction.HorizontalLeftToRight:
                    splitContainerCameraPic.Orientation = Orientation.Vertical;
                    splitContainerCameraPic.Panel1.Controls.Add(UcCameraList);
                    splitContainerCameraPic.Panel2.Controls.Add((Control)ucEventImageListIn);
                    break;
                case LaneDirectionConfig.EmCameraPicFunction.HorizontalRightToLeft:
                    splitContainerCameraPic.Orientation = Orientation.Vertical;
                    splitContainerCameraPic.Panel1.Controls.Add((Control)ucEventImageListIn);
                    splitContainerCameraPic.Panel2.Controls.Add(UcCameraList);
                    break;
                default:
                    break;
            }

            //- Khung camera
            UcCameraList.KZUI_IsDisplayTitle = config.IsDisplayCameraTitle;
            UcCameraList.KZUI_IsDisplayPanoramaCamera = config.IsDisplayPanoramaCamera;
            UcCameraList.KZUI_IsDisplayFaceCamera = config.IsDisplayFaceCamera;
            UcCameraList.KZUI_IsDisplayVehicleCamera = config.IsDisplayVehicleCamera;
            UcCameraList.KZUI_IsDisplayOtherCamera = config.IsDisplayOtherCamera;
            switch (config.cameraDirection)
            {
                case LaneDirectionConfig.EmCameraDirection.Vertical:
                    UcCameraList.KZUI_ControlDirection = EmControlDirection.VERTICAL;
                    break;
                case LaneDirectionConfig.EmCameraDirection.Horizontal:
                    UcCameraList.KZUI_ControlDirection = EmControlDirection.HORIZONTAL;
                    break;
                default:
                    break;
            }

            //- Khung hình ảnh
            switch (config.picDirection)
            {
                case LaneDirectionConfig.EmPicDirection.Vertical:
                    ucEventImageListIn.Init(SizeMode, EmControlDirection.VERTICAL, false);
                    break;
                case LaneDirectionConfig.EmPicDirection.Horizontal:
                    ucEventImageListIn.Init(SizeMode, EmControlDirection.HORIZONTAL, false);
                    break;
                default:
                    break;
            }
            ucEventImageListIn.KZUI_IsDisplayTitle = config.IsDisplayImageTitle;
            ucEventImageListIn.KZUI_IsDisplayPanorama = config.IsDisplayPanoramaImage;
            ucEventImageListIn.KZUI_IsDisplayVehicle = config.IsDisplayVehicleImage;
            ucEventImageListIn.KZUI_IsDisplayFace = config.IsDisplayFaceImage;
            ucEventImageListIn.KZUI_IsDisplayOther = config.IsDisplayOtherImage;

            //Khung sự kiện
            ucEventInfoNew.KZUI_IsDisplayTitle = config.IsDisplayTitle;
            switch (config.eventDirection)
            {
                case LaneDirectionConfig.EmEventDirection.Vertical:
                    panelUcResult.Visible = false;
                    splitContainerEventDirection.Orientation = Orientation.Horizontal;

                    UcResult.Parent = splitContainerEventDirection.Panel1;
                    panelLpr.Parent = splitContainerEventDirection.Panel1;
                    ((Control)ucEventInfoNew).Parent = splitContainerEventDirection.Panel2;

                    ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
                    panelLpr.Dock = DockStyle.Fill;
                    if (config.displayDirection == LaneDirectionConfig.EmDisplayDirection.VerTicalLeftToRight ||
                             config.displayDirection == LaneDirectionConfig.EmDisplayDirection.VerTicalRightToLeft)
                    {
                        UcResult.Dock = DockStyle.Top;
                        UcResult.Padding = new Padding(0, 0, 0, 4);
                    }
                    else
                    {
                        UcResult.Dock = DockStyle.Bottom;
                        UcResult.Padding = new Padding(0, 8, 0, 0);
                    }
                    panelLpr.BringToFront();
                    splitContainerEventDirection.Panel1.Padding = new Padding(0, 0, 0, 0);
                    splitContainerEventDirection.Panel2.Padding = new Padding(0, 0, 0, 0);
                    break;
                case LaneDirectionConfig.EmEventDirection.HorizontalLeftToRight:
                    splitContainerEventDirection.Orientation = Orientation.Vertical;
                    panelUcResult.Visible = true;

                    UcResult.Padding = new Padding(0);
                    UcResult.Parent = panelUcResult;
                    panelLpr.Parent = splitContainerEventDirection.Panel1;
                    ((Control)ucEventInfoNew).Parent = splitContainerEventDirection.Panel2;

                    UcResult.Dock = DockStyle.Fill;
                    ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
                    panelLpr.Dock = DockStyle.Fill;
                    splitContainerEventDirection.Panel1.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.Panel2.Padding = new Padding(0, 4, 0, 4);
                    break;
                case LaneDirectionConfig.EmEventDirection.HorizontalRightToLeft:
                    splitContainerEventDirection.Orientation = Orientation.Vertical;
                    panelUcResult.Visible = true;

                    UcResult.Padding = new Padding(0);
                    UcResult.Parent = panelUcResult;
                    ((Control)ucEventInfoNew).Parent = splitContainerEventDirection.Panel1;
                    panelLpr.Parent = splitContainerEventDirection.Panel2;

                    UcResult.Dock = DockStyle.Fill;
                    ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
                    panelLpr.Dock = DockStyle.Fill;
                    splitContainerEventDirection.Panel1.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.Panel2.Padding = new Padding(0, 4, 0, 4);
                    break;
                default:
                    break;
            }

            //- Khung phím chức năng
            this.UcAppFunctions.KZUI_IsDisplayWriteTicket = config.IsDisplayWriteEventButton;
            this.UcAppFunctions.KZUI_IsDisplayRetakeImage = this.Lane.Loop && config.IsDisplayRetakeImageButton;
            this.UcAppFunctions.KZUI_IsDisplayGuestRegister = config.IsDisplayGuestRegisterButton;
            this.UcAppFunctions.KZUI_IsDisplayPrint = config.IsDisplayPrintButton;
            this.UcAppFunctions.KZUI_IsDisplayOpenBarrie = config.IsDisplayOpenBarrieButton;
            this.UcAppFunctions.KZUI_IsDisplayCloseBarrie = config.IsDisplayCloseBarrieButton;
            if (!config.IsDisplayWriteEventButton &&
                !config.IsDisplayRetakeImageButton &&
                !config.IsDisplayGuestRegisterButton &&
                !config.IsDisplayPrintButton &&
                !config.IsDisplayOpenBarrieButton &&
                !config.IsDisplayCloseBarrieButton
                )
            {
                panelAppFunction.Visible = false;
            }
            else
            {
                panelAppFunction.Visible = true;
            }
            //- Khung biển số
            if (UcPlateIn is not null)
            {
                ((Control)UcPlateIn).Dispose();
            }

            panelLpr.Controls.Clear();

            UcPlateIn = config.PlateInDirection switch
            {
                LaneDirectionConfig.EmPlateDirection.Vertical => new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = this.SizeMode, KZUI_Title = "BIỂN SỐ VÀO" },
                LaneDirectionConfig.EmPlateDirection.HorizontalLeftToRight => new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = this.SizeMode, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = true },
                LaneDirectionConfig.EmPlateDirection.HorizontalRightToLeft => new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = this.SizeMode, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = false },
                _ => new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = this.SizeMode, KZUI_Title = "BIỂN SỐ VÀO" },
            };
            panelLpr.Controls.Add((Control)UcPlateIn);
            ((Control)UcPlateIn).Dock = DockStyle.Fill;
            UcPlateIn.Init(KZUIStyles.CurrentResources.PlateIn.ToUpper(), defaultImg);
        }
        public override void LoadViewSetting(LaneDisplayConfig config)
        {
            this.laneDisplayConfig = config;

            splitContainerDisplayDirection.SplitterDistance = laneDisplayConfig.splitterMain;
            splitContainerCameraPic.SplitterDistance = laneDisplayConfig.splitterCamera_Pic;
            splitContainerEventDirection.SplitterDistance = laneDisplayConfig.splitterLpr_DGV;
        }
        public override void AllowDesignRealtime(bool isAllow)
        {
            if (isAllow)
            {
                this.Margin = new Padding(8);
            }
            else
            {
                this.Margin = new Padding(2);
            }
            this.IsAllowDesignRealtime = isAllow;
            foreach (var spliter in activeSpliters)
            {
                spliter.IsSplitterFixed = !isAllow;
                spliter.BarColor = isAllow ? Color.Red : Color.White;
                spliter.ArrowColor = isAllow ? Color.Red : Color.White;
                spliter.SplitterWidth = isAllow ? 11 : 4;
                spliter.Refresh();
            }
        }
    }
}