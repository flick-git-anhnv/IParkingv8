using iParkingv8.Object.ConfigObjects.LaneConfigs;
using IParkingv8.UserControls;

namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    public partial class ucLaneInMediumPreview : UserControl, IPreviewControl
    {
        IKZUI_UcPlate UcPlateIn;
        EmControlSizeMode SizeMode = EmControlSizeMode.MEDIUM;
        public ucLaneInMediumPreview()
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
            this.UcAppFunctions.KZUI_IsDisplayRetakeImage = config.IsDisplayRetakeImageButton;
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
                LaneDirectionConfig.EmPlateDirection.Vertical => new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO" },
                LaneDirectionConfig.EmPlateDirection.HorizontalLeftToRight => new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = true },
                LaneDirectionConfig.EmPlateDirection.HorizontalRightToLeft => new KZUI_UcPlateHorizontal() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO", KZUI_IsLeftoToRight = false },
                _ => new KZUI_UcPlateVertical() { KZUI_ControlSizeMode = EmControlSizeMode.SMALL, KZUI_Title = "BIỂN SỐ VÀO" },
            };
            panelLpr.Controls.Add((Control)UcPlateIn);
            ((Control)UcPlateIn).Dock = DockStyle.Fill;
        }
    }
}
