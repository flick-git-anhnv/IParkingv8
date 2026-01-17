using iParkingv5.Lpr.Objects;
using iParkingv5.LprDetecter.LprDetecters;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.CameraConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using IParkingv8.UserControls;
using Kztek.Tool;
using System.ComponentModel;
using System.Diagnostics;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcCameraList : UserControl
    {
        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        this.KZUI_Spacing = SizeManagement.SMALL_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        this.KZUI_Spacing = SizeManagement.MEDIUM_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.LARGE:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        this.KZUI_Spacing = SizeManagement.LARRGE_BORDER_RADIUS;
                        break;
                    default:
                        break;
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Title"), Description("Display Title")]
        public string KZUI_Title
        {
            get => lblTitle.Text;
            set { lblTitle.Text = value; }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayTitle
        {
            get => lblTitle.Visible;
            set { lblTitle.Visible = value; }
        }

        private bool isDisplayPanoramaCamera = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayPanoramaCamera
        {
            get => isDisplayPanoramaCamera;
            set
            {
                isDisplayPanoramaCamera = value;
                foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (item.CameraPurpose == EmCameraPurpose.Panorama)
                    {
                        item.Visible = isDisplayPanoramaCamera;
                    }
                }
            }
        }

        private bool isDisplayVehicleCamera = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayVehicleCamera
        {
            get => isDisplayVehicleCamera;
            set
            {
                isDisplayVehicleCamera = value;
                foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (item.CameraPurpose == EmCameraPurpose.CarLpr || item.CameraPurpose == EmCameraPurpose.MotorLpr)
                    {
                        item.Visible = isDisplayVehicleCamera;
                    }
                }
            }
        }

        private bool isDisplayFaceCamera = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayFaceCamera
        {
            get => isDisplayFaceCamera;
            set
            {
                isDisplayFaceCamera = value;
                foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (item.CameraPurpose == EmCameraPurpose.FaceID)
                    {
                        item.Visible = isDisplayFaceCamera;
                    }
                }
            }
        }

        private bool isDisplayOtherCamera = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayOtherCamera
        {
            get => isDisplayOtherCamera;
            set
            {
                isDisplayOtherCamera = value;
                foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (item.CameraPurpose == EmCameraPurpose.Other)
                    {
                        item.Visible = isDisplayOtherCamera;
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Spacing"), Description("Display Spacing")]
        public int KZUI_Spacing
        {
            get => panelCameras.Spacing;
            set
            {
                panelCameras.BorderRadius = value;
                panelCameras.RefreshUI(value);
                foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    item.BorderRadius = value;
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Control Direction"), Description("Display Spacing")]
        public EmControlDirection KZUI_ControlDirection
        {
            get => panelCameras.KZUI_ControlDirection;
            set { panelCameras.KZUI_ControlDirection = value; }
        }

        public KZUI_UcCameraList()
        {
            InitializeComponent();
            this.BackColor = ColorManagement.ControlBackgroud;
            panelCameraList.BackColor = ColorManagement.ControlBackgroud;
            panelCameraList.FillColor = ColorManagement.AppBackgroudColor;
            this.DoubleBuffered = true;
        }

        public void Init(ILane lane, IEnumerable<Camera> allCameras, bool isUseVirtualLoop, EmVirtualLoopMode virtualLoopMode, double alarmLevel, int cameraSDK)
        {
            foreach (var item in lane.Lane.Cameras.OrderBy(e => e.Purpose))
            {
                foreach (Camera camera in allCameras)
                {
                    if (camera.Id != item.Id)
                    {
                        continue;
                    }
                    KZUI_UcCameraView uc = new();
                    uc.Init(lane, camera, (EmCameraPurpose)item.Purpose, cameraSDK);
                    if (!isUseVirtualLoop)
                    {
                        uc.StartViewer(false, 0);
                    }
                    else
                    {
                        switch (virtualLoopMode)
                        {
                            case EmVirtualLoopMode.Overview:
                                if (item.Purpose == (int)EmCameraPurpose.Panorama)
                                {
                                    uc.StartViewer(true, alarmLevel);
                                }
                                else
                                {
                                    uc.StartViewer(false, 0);

                                }
                                break;
                            case EmVirtualLoopMode.Lpr:
                                if (item.Purpose == (int)EmCameraPurpose.MotorLpr ||
                                    item.Purpose == (int)EmCameraPurpose.CarLpr)
                                {
                                    uc.StartViewer(true, alarmLevel);
                                }
                                else
                                {
                                    uc.StartViewer(false, 0);
                                }
                                break;
                            case EmVirtualLoopMode.Both:
                                uc.StartViewer(true, alarmLevel);
                                break;
                            default:
                                break;
                        }
                    }
                    panelCameras.Controls.Add(uc);
                }
            }
            panelCameras.RefreshUI(8);
        }

        //public async Task<Image?> GetPanoramaImageAsync(List<CameraInLane> cameraInLanes)
        //{
        //    return await Task.Run(() =>
        //    {
        //        foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
        //        {
        //            if (uc.CameraPurpose == EmCameraPurpose.Panorama)
        //            {
        //                return uc.GetFullCurrentImage();
        //            }
        //        }
        //        return null;
        //    });
        //}
        public async Task<Image?> GetImageAsync(EmCameraPurpose purpose)
        {
            return await Task.Run(() =>
            {
                foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (uc.CameraPurpose == purpose)
                    {
                        return uc.GetFullCurrentImage();
                    }
                }
                return null;
            });
        }

        public async Task<Dictionary<EmImageType, List<List<byte>>>> GetAllCameraImageAsync(List<CameraInLane> cameraInLanes)
        {
            var imageData = new Dictionary<EmImageType, List<List<byte>>>();
            foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
            {
                var motorImage = uc.GetFullCurrentImage();
                var motorImageData = await motorImage.ImageToByteArrayAsync();
                var imageType = EmImageType.VEHICLE;
                switch (uc.CameraPurpose)
                {
                    case EmCameraPurpose.MotorLpr:
                    case EmCameraPurpose.CarLpr:
                        imageType = EmImageType.VEHICLE;
                        break;
                    case EmCameraPurpose.FaceID:
                        imageType = EmImageType.FACE;
                        break;
                    case EmCameraPurpose.Panorama:
                    case EmCameraPurpose.Other:
                        imageType = EmImageType.PANORAMA;
                        break;
                    default:
                        break;
                }

                if (!imageData.TryGetValue(imageType, out List<List<byte>>? value))
                {
                    imageData.Add(imageType, [motorImageData]);
                }
                else
                {
                    value.Add(motorImageData);
                }
            }
            return imageData;
        }
        public async Task<Dictionary<EmImageType, Image?>> GetAllCameraImagesAsync(List<CameraInLane> cameraInLanes)
        {
            var imageData = new Dictionary<EmImageType, Image?>();
            foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
            {
                var motorImage = uc.GetFullCurrentImage();
                var imageType = EmImageType.VEHICLE;
                switch (uc.CameraPurpose)
                {
                    case EmCameraPurpose.MotorLpr:
                    case EmCameraPurpose.CarLpr:
                        imageType = EmImageType.VEHICLE;
                        break;
                    case EmCameraPurpose.FaceID:
                        imageType = EmImageType.FACE;
                        break;
                    case EmCameraPurpose.Panorama:
                        imageType = EmImageType.PANORAMA;
                        break;
                    case EmCameraPurpose.Other:
                        imageType = EmImageType.OTHER;
                        break;
                    default:
                        break;
                }

                if (!imageData.TryGetValue(imageType, out Image? value))
                {
                    imageData.Add(imageType, motorImage);
                }
                else
                {
                    imageData[imageType] = value;
                }
            }
            return imageData;
        }

        public async Task<DetectLprResult> GetPlateAsync(string laneId, ILpr? lprDetecter,
                                                         List<CameraInLane> cameraInLanes, bool isCar)
        {
            var result = new DetectLprResult();
            try
            {
                Rectangle? config = null;
                int angle = 0;
                foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
                {
                    if (uc.CameraPurpose == EmCameraPurpose.Panorama || uc.CameraPurpose == EmCameraPurpose.Other) continue;
                    if (isCar && uc.CameraPurpose == EmCameraPurpose.MotorLpr) continue;
                    if (!isCar && uc.CameraPurpose == EmCameraPurpose.CarLpr) continue;

                    string cameraInLaneId = uc.CurrentCamera?.ID ?? string.Empty;

                    CameraConfig? cameraConfig = NewtonSoftHelper<CameraConfig>.DeserializeObjectFromPath(
                                                 IparkingingPathManagement.laneCameraConfigPath(laneId, cameraInLaneId));
                    if (cameraConfig != null)
                    {
                        config = cameraConfig.DetectRegion == null ? null : new Rectangle()
                        {
                            X = cameraConfig.DetectRegion.X,
                            Y = cameraConfig.DetectRegion.Y,
                            Width = cameraConfig.DetectRegion.Width,
                            Height = cameraConfig.DetectRegion.Height
                        };
                        angle = cameraConfig.RotateAngle;
                    }

                    //uc.ActiveDetectPlateMode();
                    var vehicleImage = uc.GetFullCurrentImage();
                    //Stopwatch stopwatch = Stopwatch.StartNew();
                    result = await lprDetecter!.GetPlateNumberAsync(vehicleImage, isCar, config, angle, false);
                    //stopwatch.Stop();
                    //uc.DisplayPlateResult(result.PlateNumber, stopwatch.ElapsedMilliseconds);
                    if (!string.IsNullOrEmpty(result.PlateNumber))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Detect LPR Error" + ex));
            }

            return result;
        }
        public async Task<DetectLprResult> GetPlateAsync(string laneId, ILpr? lprDetecter,
                                                   CameraInLane cameraInLane, bool isCar)
        {
            var result = new DetectLprResult();
            try
            {
                Rectangle? config = null;
                int angle = 0;
                KZUI_UcCameraView? uc = (panelCameras.Controls.OfType<KZUI_UcCameraView>()).Where(
                            e =>
                            e.CurrentCamera is not null &&
                            e.CurrentCamera.ID == cameraInLane.Id &&
                            ((e.CameraPurpose == EmCameraPurpose.MotorLpr && !isCar) ||
                             (e.CameraPurpose == EmCameraPurpose.CarLpr && isCar))).FirstOrDefault();
                if (uc is null)
                {
                    return new DetectLprResult();
                }

                uc.ActiveDetectPlateMode();
                CameraConfig? cameraConfig = NewtonSoftHelper<CameraConfig>.DeserializeObjectFromPath(
                                               IparkingingPathManagement.laneCameraConfigPath(laneId, cameraInLane.Id));
                if (cameraConfig != null)
                {
                    config = cameraConfig.DetectRegion == null ? null : new Rectangle()
                    {
                        X = cameraConfig.DetectRegion.X,
                        Y = cameraConfig.DetectRegion.Y,
                        Width = cameraConfig.DetectRegion.Width,
                        Height = cameraConfig.DetectRegion.Height
                    };
                    angle = cameraConfig.RotateAngle;
                }

                var vehicleImage = uc.GetFullCurrentImage();
                Stopwatch stopwatch = Stopwatch.StartNew();
                result = await lprDetecter!.GetPlateNumberAsync(vehicleImage, isCar, config, angle, false);
                stopwatch.Stop();
                uc.DisplayPlateResult(result.PlateNumber, stopwatch.ElapsedMilliseconds);
                return result;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Detect LPR Error" + ex));
            }

            return result;
        }
        public void Stop()
        {
            foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
            {
                uc.Stop();
            }
        }

        public bool IsValidCarCamera()
        {
            bool isValid = false;
            foreach (var item in panelCameras.Controls.OfType<KZUI_UcCameraView>())
            {
                if (item.CameraPurpose == EmCameraPurpose.CarLpr)
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        //public async Task<List<Image>> GetFaceImages()
        //{
        //    var images = new List<Image>();
        //    await Task.Run(() =>
        //    {
        //        foreach (KZUI_UcCameraView uc in panelCameras.Controls.OfType<KZUI_UcCameraView>())
        //        {
        //            if (uc.CameraPurpose == EmCameraPurpose.FaceID)
        //            {
        //                var image = uc.GetFullCurrentImage();
        //                if (image != null)
        //                {
        //                    images.Add(image);
        //                }
        //            }
        //        }
        //    });
        //    return images;
        //}
        //public KZUI_UcCameraView? GetFaceCameraView()
        //{
        //    foreach (var uc in this.panelCameras.Controls.OfType<KZUI_UcCameraView>())
        //    {
        //        if (uc.CameraPurpose != EmCameraPurpose.FaceID)
        //        {
        //            continue;
        //        }
        //        return uc;
        //    }
        //    return null;
        //}
    }
}
