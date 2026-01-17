using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using IParkingv8.UserControls;
using Kztek.Tool;
using static iParkingv8.Object.ConfigObjects.LaneConfigs.LaneDirectionConfig;

namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    public partial class UcLaneDirectionViewConfig : UserControl
    {
        #region Properties
        private readonly string laneId = string.Empty;
        private ILane lane;
        IPreviewControl? previewControl = null;
        private EmControlSizeMode sizeMode;
        Size previewSize;
        #endregion End Properties

        #region Forms
        public UcLaneDirectionViewConfig(ILane lane, EmControlSizeMode sizeMode, Size size)
        {
            InitializeComponent();
            this.laneId = lane.Lane.Id;
            this.lane = lane;
            this.sizeMode = sizeMode;
            this.previewSize = size;
            List<EmCameraPurpose> purposes = [];
            foreach (var cameraInLane in lane.Lane.Cameras)
            {
                var cameraPurpose = (EmCameraPurpose)cameraInLane.Purpose;
                purposes.Add(cameraPurpose);
            }

            chbIsDisplayPanoramaCamera.Enabled = chbIsDisplayPanoramaImage.Enabled = purposes.Contains(EmCameraPurpose.Panorama);
            chbIsDisplayVehicleCamera.Enabled = chbIsDisplayVehicleImage.Enabled = purposes.Contains(EmCameraPurpose.CarLpr) || purposes.Contains(EmCameraPurpose.MotorLpr);
            chbIsDisplayFaceCamera.Enabled = chbIsDisplayFaceImage.Enabled = purposes.Contains(EmCameraPurpose.FaceID);
            chbIsDisplayOtherCamera.Enabled = chbIsDisplayOtherImage.Enabled = purposes.Contains(EmCameraPurpose.Other);

            chbIsDisplayRetakeImageButton.Enabled = lane.Lane.Loop;

            switch ((EmLaneType)lane.Lane.Type)
            {
                case EmLaneType.LANE_IN:
                    previewControl = sizeMode switch
                    {
                        EmControlSizeMode.SMALL => new ucLaneInSmallPreview(),
                        _ => new ucLaneInMediumPreview(),
                    };
                    break;
                case EmLaneType.LANE_OUT:
                    previewControl = sizeMode switch
                    {
                        EmControlSizeMode.SMALL => new ucLaneOutSmallPreview(),
                        _ => new ucLaneOutMediumPreview(),
                    };
                    break;
                default:
                    break;
            }
            if (previewControl is not null)
            {
                panelPreview.Controls.Add((Control)previewControl);
                ((Control)previewControl).Dock = DockStyle.Fill;
                previewControl.Init(lane);
            }
            this.Load += UcLaneDirectionConfig_Load;
        }
        private void UcLaneDirectionConfig_Load(object? sender, EventArgs e)
        {
            if (panelPreview.Width > this.previewSize.Width)
            {
                panelPreview.Width = this.previewSize.Width;
            }
            Translate();

            CreateUI();

            //EmDisplayDirection
            cbDisplayDirection.Items.Clear();
            foreach (EmDisplayDirection displayDirection in Enum.GetValues(typeof(EmDisplayDirection)))
            {
                cbDisplayDirection.Items.Add(LaneDirectionConfig.ToDisplayString(displayDirection));
            }

            //EmCameraPicFunction
            cbCameraPicDirection.Items.Clear();
            foreach (EmCameraPicFunction cameraPicDirection in Enum.GetValues(typeof(EmCameraPicFunction)))
            {
                cbCameraPicDirection.Items.Add(LaneDirectionConfig.ToDisplayString(cameraPicDirection));
            }

            //EmCameraDirection
            cbCameraDirection.Items.Clear();
            foreach (EmCameraDirection cameraDirection in Enum.GetValues(typeof(EmCameraDirection)))
            {
                cbCameraDirection.Items.Add(LaneDirectionConfig.ToDisplayString(cameraDirection));
            }

            //EmPicDirection
            cbPicDiection.Items.Clear();
            foreach (EmPicDirection picDirection in Enum.GetValues(typeof(EmPicDirection)))
            {
                cbPicDiection.Items.Add(LaneDirectionConfig.ToDisplayString(picDirection));
            }

            //EmEventDirection
            cbEventDirection.Items.Clear();
            foreach (EmEventDirection eventDirection in Enum.GetValues(typeof(EmEventDirection)))
            {
                cbEventDirection.Items.Add(LaneDirectionConfig.ToDisplayString(eventDirection));
            }

            //EmPlateDirection
            cbPlateInDirection.Items.Clear();
            cbPlateOutDirection.Items.Clear();
            foreach (EmPlateDirection direction in Enum.GetValues(typeof(EmPlateDirection)))
            {
                cbPlateInDirection.Items.Add(LaneDirectionConfig.ToDisplayString(direction));
                cbPlateOutDirection.Items.Add(LaneDirectionConfig.ToDisplayString(direction));
            }

            //EmViewOption
            cbEventTemplate.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(EmViewOption)))
            {
                cbEventTemplate.Items.Add(item);
            }

            LaneDirectionConfig? laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.laneId));
            laneDirectionConfig ??= (EmLaneType)lane.Lane.Type switch
            {
                EmLaneType.LANE_IN or EmLaneType.KIOSK_IN => this.sizeMode switch
                {
                    EmControlSizeMode.SMALL => LaneDirectionConfig.CreateDefaultInConfig(),
                    _ => LaneDirectionConfig.CreateDefaultInSmallConfig(),
                },
                _ => this.sizeMode switch
                {
                    EmControlSizeMode.SMALL => LaneDirectionConfig.CreateDefaultOutConfig(),
                    _ => LaneDirectionConfig.CreateDefaultOutSmallConfig(),
                },
            };

            cbDisplayDirection.SelectedIndex = (int)laneDirectionConfig.displayDirection > cbDisplayDirection.Items.Count - 1 ?
                                                            -1 : (int)laneDirectionConfig.displayDirection;
            cbCameraPicDirection.SelectedIndex = (int)laneDirectionConfig.cameraPicDirection > cbCameraPicDirection.Items.Count - 1 ?
                                                            -1 : (int)laneDirectionConfig.cameraPicDirection;
            //- Khung camera
            cbCameraDirection.SelectedIndex = (int)laneDirectionConfig.cameraDirection > cbCameraDirection.Items.Count - 1 ?
                                                           -1 : (int)laneDirectionConfig.cameraDirection;
            chbIsDisplayTitleCamera.Checked = laneDirectionConfig.IsDisplayCameraTitle;
            laneDirectionConfig.IsDisplayPanoramaCamera = chbIsDisplayPanoramaCamera.Checked = chbIsDisplayPanoramaCamera.Enabled && laneDirectionConfig.IsDisplayPanoramaCamera;
            laneDirectionConfig.IsDisplayFaceCamera = chbIsDisplayFaceCamera.Checked = chbIsDisplayFaceCamera.Enabled && laneDirectionConfig.IsDisplayFaceCamera;
            laneDirectionConfig.IsDisplayVehicleCamera = chbIsDisplayVehicleCamera.Checked = chbIsDisplayVehicleCamera.Enabled && laneDirectionConfig.IsDisplayVehicleCamera;
            laneDirectionConfig.IsDisplayOtherCamera = chbIsDisplayOtherCamera.Checked = chbIsDisplayOtherCamera.Enabled && laneDirectionConfig.IsDisplayOtherCamera;

            //- Khung hình ảnh
            cbPicDiection.SelectedIndex = (int)laneDirectionConfig.picDirection > cbPicDiection.Items.Count - 1 ?
                                                        -1 : (int)laneDirectionConfig.picDirection;
            chbIsDisplayTitleImage.Checked = laneDirectionConfig.IsDisplayImageTitle;
            laneDirectionConfig.IsDisplayPanoramaImage = chbIsDisplayPanoramaImage.Checked = chbIsDisplayPanoramaImage.Enabled && laneDirectionConfig.IsDisplayPanoramaImage;
            laneDirectionConfig.IsDisplayFaceImage = chbIsDisplayFaceImage.Checked = chbIsDisplayFaceImage.Enabled && laneDirectionConfig.IsDisplayFaceImage;
            laneDirectionConfig.IsDisplayVehicleImage = chbIsDisplayVehicleImage.Checked = chbIsDisplayVehicleImage.Enabled && laneDirectionConfig.IsDisplayVehicleImage;
            laneDirectionConfig.IsDisplayOtherImage = chbIsDisplayOtherImage.Checked = chbIsDisplayOtherImage.Enabled && laneDirectionConfig.IsDisplayOtherImage;

            //- Khung sự kiện
            cbEventDirection.SelectedIndex = (int)laneDirectionConfig.eventDirection > cbEventDirection.Items.Count - 1 ?
                                                        -1 : (int)laneDirectionConfig.eventDirection;
            cbEventTemplate.SelectedIndex = (int)laneDirectionConfig.optionViewData > cbEventTemplate.Items.Count - 1 ?
                                                            -1 : (int)laneDirectionConfig.optionViewData;
            chbIsDisplayEventTitle.Checked = laneDirectionConfig.IsDisplayTitle;

            //- Khung phím chức năng
            chbIsDisplayWriteEventButton.Checked = laneDirectionConfig.IsDisplayWriteEventButton;
            chbIsDisplayRetakeImageButton.Checked = chbIsDisplayRetakeImageButton.Enabled && laneDirectionConfig.IsDisplayRetakeImageButton;
            chbIsDisplayGuestRegisterButton.Checked = laneDirectionConfig.IsDisplayGuestRegisterButton;

            chbIsDisplayOpenBarrieButton.Checked = laneDirectionConfig.IsDisplayOpenBarrieButton;
            chbIsDisplayCloseBarrieButton.Checked = laneDirectionConfig.IsDisplayCloseBarrieButton;
            chbIsDisplayPrint.Checked = laneDirectionConfig.IsDisplayPrintButton;

            //- khung biển số xe
            cbPlateInDirection.SelectedIndex = (int)laneDirectionConfig.PlateInDirection > cbPlateInDirection.Items.Count - 1 ?
                                                       -1 : (int)laneDirectionConfig.PlateInDirection;
            cbPlateOutDirection.SelectedIndex = (int)laneDirectionConfig.PlateOutDirection > cbPlateOutDirection.Items.Count - 1 ?
                                               -1 : (int)laneDirectionConfig.PlateOutDirection;

            this.previewControl?.RefreshConfig(laneDirectionConfig);
        }
        #endregion End Forms

        #region Controls In Form
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            LaneDirectionConfig laneDirectionConfig = GetCurrentConfig();
            NewtonSoftHelper<LaneDirectionConfig>.SaveConfig(laneDirectionConfig, IparkingingPathManagement.appLaneDirectionConfigPath(this.laneId));
            MessageBox.Show(KZUIStyles.CurrentResources.SaveConfigSuccess, KZUIStyles.CurrentResources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private LaneDirectionConfig GetCurrentConfig()
        {
            return new()
            {
                displayDirection = (EmDisplayDirection)cbDisplayDirection.SelectedIndex,
                cameraPicDirection = (EmCameraPicFunction)cbCameraPicDirection.SelectedIndex,
                //- Khung camera
                cameraDirection = (EmCameraDirection)cbCameraDirection.SelectedIndex,
                IsDisplayCameraTitle = chbIsDisplayTitleCamera.Checked,
                IsDisplayPanoramaCamera = chbIsDisplayPanoramaCamera.Checked,
                IsDisplayFaceCamera = chbIsDisplayFaceCamera.Checked,
                IsDisplayVehicleCamera = chbIsDisplayVehicleCamera.Checked,
                IsDisplayOtherCamera = chbIsDisplayOtherCamera.Checked,

                //- Khung hình ảnh
                picDirection = (EmPicDirection)cbPicDiection.SelectedIndex,
                IsDisplayImageTitle = chbIsDisplayTitleImage.Checked,
                IsDisplayPanoramaImage = chbIsDisplayPanoramaImage.Checked,
                IsDisplayFaceImage = chbIsDisplayFaceImage.Checked,
                IsDisplayVehicleImage = chbIsDisplayVehicleImage.Checked,
                IsDisplayOtherImage = chbIsDisplayOtherImage.Checked,

                //- Khung sự kiện
                eventDirection = (EmEventDirection)cbEventDirection.SelectedIndex,
                optionViewData = (EmViewOption)cbEventTemplate.SelectedIndex,
                IsDisplayTitle = chbIsDisplayEventTitle.Checked,

                //- Khung phím chức năng
                IsDisplayWriteEventButton = chbIsDisplayWriteEventButton.Checked,
                IsDisplayRetakeImageButton = chbIsDisplayRetakeImageButton.Checked,
                IsDisplayGuestRegisterButton = chbIsDisplayGuestRegisterButton.Checked,

                IsDisplayOpenBarrieButton = chbIsDisplayOpenBarrieButton.Checked,
                IsDisplayCloseBarrieButton = chbIsDisplayCloseBarrieButton.Checked,
                IsDisplayPrintButton = chbIsDisplayPrint.Checked,

                //- khung biển số xe
                PlateInDirection = (EmPlateDirection)cbPlateInDirection.SelectedIndex,
                PlateOutDirection = (EmPlateDirection)cbPlateOutDirection.SelectedIndex,
            };
        }
        private void RefreshViewConfig(object sender, EventArgs e)
        {
            this.previewControl?.RefreshConfig(GetCurrentConfig());
        }
        #endregion End Controls In Form

        #region Private Function
        private void CreateUI()
        {
            btnConfirm.Text = KZUIStyles.CurrentResources.Save;
            btnConfirm.OnClickAsync += BtnConfirm_Click;
        }

        public void Translate()
        {
            lblDisplayDirectionTitle.Text = KZUIStyles.CurrentResources.DisplayDirection;
            lblCameraDirectionTitle.Text = KZUIStyles.CurrentResources.CameraRegion;
            lblPicDirectionTitle.Text = KZUIStyles.CurrentResources.PicRegion;
            lblCameraPicDirectionTitle.Text = KZUIStyles.CurrentResources.CameraPicRegion;
            lblEventDirectionTitle.Text = KZUIStyles.CurrentResources.LprEventRegion;
            lblTypeTemplateTitle.Text = KZUIStyles.CurrentResources.EventRegion;

            chbIsDisplayOpenBarrieButton.Text = KZUIStyles.CurrentResources.AllowOpenBarrieManual;
            chbIsDisplayWriteEventButton.Text = KZUIStyles.CurrentResources.AllowWriteTicketManual;
            chbIsDisplayRetakeImageButton.Text = KZUIStyles.CurrentResources.AllowRetakeImageManual;
            chbIsDisplayEventTitle.Text = KZUIStyles.CurrentResources.DisplayTitle;
        }
        #endregion End Private Function
    }
}
