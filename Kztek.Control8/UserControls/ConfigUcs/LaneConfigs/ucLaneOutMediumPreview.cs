using iParkingv8.Object.ConfigObjects.LaneConfigs;
using IParkingv8.UserControls;

namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    public partial class ucLaneOutMediumPreview : UserControl, IPreviewControl
    {
        IKZUI_UcPlate ucPlateIn;
        IKZUI_UcPlate ucPlateOut;

        public ucLaneOutMediumPreview()
        {
            InitializeComponent();

            ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.Now,
                                            new iParkingv8.Object.Objects.Parkings.AccessKey() { Name = "Thẻ 1", Code = "Mã thẻ" },
                                            new iParkingv8.Object.Objects.Parkings.Collection() { Code = "", Name = "Nhóm thẻ 1" },
                                            new iParkingv8.Object.Objects.Parkings.AccessKey()
                                            {
                                                Name = "Phương tiện",
                                                Code = "30A12345",
                                                Customer = new iParkingv8.Object.Objects.Parkings.CustomerDto()
                                                {
                                                    Name = "Khách hàng 1",
                                                    Address = "Địa chỉ 1",
                                                    PhoneNumber = "0123xxxxxx"
                                                }
                                            },
                                            10000, "Note");
        }

        public void Init(ILane lane)
        {
            kzuI_UcLaneTitle1.Init(lane.Lane);
        }

        public void RefreshConfig(LaneDirectionConfig config)
        {
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
                    splitContainerCameraPic.Panel1.Controls.Add(this.UcCameraList);
                    splitContainerCameraPic.Panel2.Controls.Add((Control)this.UcEventImageListOut);
                    splitContainerCameraPic.SplitterDistance = splitContainerCameraPic.Height / 2;
                    break;
                case LaneDirectionConfig.EmCameraPicFunction.HorizontalLeftToRight:
                    splitContainerCameraPic.Orientation = Orientation.Vertical;
                    splitContainerCameraPic.Panel1.Controls.Add(UcCameraList);
                    splitContainerCameraPic.Panel2.Controls.Add((Control)UcEventImageListOut);
                    splitContainerCameraPic.SplitterDistance = splitContainerCameraPic.Width / 2;
                    break;
                case LaneDirectionConfig.EmCameraPicFunction.HorizontalRightToLeft:
                    splitContainerCameraPic.Orientation = Orientation.Vertical;
                    splitContainerCameraPic.Panel1.Controls.Add((Control)UcEventImageListOut);
                    splitContainerCameraPic.Panel2.Controls.Add(UcCameraList);
                    splitContainerCameraPic.SplitterDistance = splitContainerCameraPic.Width / 2;
                    break;
                default:
                    break;
            }

            //- Khung camera
            UcCameraList.KZUI_IsDisplayTitle = config.IsDisplayCameraTitle;
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
            UcEventImageListOut.KZUI_IsDisplayTitle = config.IsDisplayImageTitle;
            UcEventImageListOut.KZUI_IsDisplayPanorama = config.IsDisplayPanoramaImage;
            UcEventImageListOut.KZUI_IsDisplayVehicle = config.IsDisplayVehicleImage;
            UcEventImageListOut.KZUI_IsDisplayFace = config.IsDisplayFaceImage;
            UcEventImageListOut.KZUI_IsDisplayOther = config.IsDisplayOtherImage;

            //- Khung sự kiện
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
                    splitContainerEventDirection.SplitterDistance = splitContainerEventDirection.Height / 2;
                    break;
                case LaneDirectionConfig.EmEventDirection.HorizontalLeftToRight:
                    splitContainerEventDirection.Orientation = Orientation.Vertical;
                    panelUcResult.Visible = true;

                    UcResult.Parent = panelUcResult;
                    panelLpr.Parent = splitContainerEventDirection.Panel1;
                    ((Control)ucEventInfoNew).Parent = splitContainerEventDirection.Panel2;

                    UcResult.Dock = DockStyle.Fill;
                    ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
                    panelLpr.Dock = DockStyle.Fill;
                    splitContainerEventDirection.Panel1.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.Panel2.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.SplitterDistance = splitContainerEventDirection.Width / 2;
                    break;
                case LaneDirectionConfig.EmEventDirection.HorizontalRightToLeft:
                    splitContainerEventDirection.Orientation = Orientation.Vertical;
                    panelUcResult.Visible = true;

                    UcResult.Parent = panelUcResult;
                    ((Control)ucEventInfoNew).Parent = splitContainerEventDirection.Panel1;
                    panelLpr.Parent = splitContainerEventDirection.Panel2;

                    UcResult.Dock = DockStyle.Fill;
                    ((Control)ucEventInfoNew).Dock = DockStyle.Fill;
                    panelLpr.Dock = DockStyle.Fill;
                    splitContainerEventDirection.Panel1.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.Panel2.Padding = new Padding(0, 4, 0, 4);
                    splitContainerEventDirection.SplitterDistance = splitContainerEventDirection.Width / 2;
                    break;
                default:
                    break;
            }

            //- Khung phím chức năng
            //- khung biển số xe

            if (ucPlateIn is not null)
            {
                ((Control)ucPlateIn).Dispose();
            }
            if (ucPlateOut is not null)
            {
                ((Control)ucPlateOut).Dispose();
            }
            panelPlateIn.Controls.Clear();
            switch (config.PlateInDirection)
            {
                case LaneDirectionConfig.EmPlateDirection.Vertical:
                    ucPlateIn = new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO" };
                    break;
                case LaneDirectionConfig.EmPlateDirection.HorizontalLeftToRight:
                    ucPlateIn = new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = true };
                    break;
                case LaneDirectionConfig.EmPlateDirection.HorizontalRightToLeft:
                    ucPlateIn = new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = false };
                    break;
                default:
                    ucPlateIn = new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO" };
                    break;
            }

            panelPlateIn.Controls.Add((Control)ucPlateIn);
            ((Control)ucPlateIn).Dock = DockStyle.Fill;
            panelPlateOut.Controls.Clear();

            switch (config.PlateOutDirection)
            {
                case LaneDirectionConfig.EmPlateDirection.Vertical:
                    ucPlateOut = new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ RA" };
                    break;
                case LaneDirectionConfig.EmPlateDirection.HorizontalLeftToRight:
                    ucPlateOut = new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ RA", KZUI_IsLeftoToRight = true };
                    break;
                case LaneDirectionConfig.EmPlateDirection.HorizontalRightToLeft:
                    ucPlateOut = new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ RA", KZUI_IsLeftoToRight = false };
                    break;
                default:
                    ucPlateOut = new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ RA" };
                    break;
            }
            panelPlateOut.Controls.Add((Control)ucPlateOut);
            ((Control)ucPlateOut).Dock = DockStyle.Fill;

            this.kzuI_Function1.KZUI_IsDisplayWriteTicket = config.IsDisplayWriteEventButton;
            this.kzuI_Function1.KZUI_IsDisplayRetakeImage = config.IsDisplayRetakeImageButton;
            this.kzuI_Function1.KZUI_IsDisplayGuestRegister = config.IsDisplayGuestRegisterButton;
            this.kzuI_Function1.KZUI_IsDisplayPrint = config.IsDisplayPrintButton;
            this.kzuI_Function1.KZUI_IsDisplayOpenBarrie = config.IsDisplayOpenBarrieButton;
            this.kzuI_Function1.KZUI_IsDisplayCloseBarrie = config.IsDisplayCloseBarrieButton;
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

            switch (config.optionViewData)
            {
                case LaneDirectionConfig.EmViewOption.OnlyData:
                    this.ucEventInfoNew.KZUI_IsDisplayMoney = false;
                    break;
                case LaneDirectionConfig.EmViewOption.DataAndMoney:
                    this.ucEventInfoNew.KZUI_IsDisplayMoney = true;
                    break;
                default:
                    this.ucEventInfoNew.KZUI_IsDisplayMoney = false;
                    break;
            }

            this.ucEventInfoNew.KZUI_IsDisplayTitle = config.IsDisplayTitle;
        }
    }
}
