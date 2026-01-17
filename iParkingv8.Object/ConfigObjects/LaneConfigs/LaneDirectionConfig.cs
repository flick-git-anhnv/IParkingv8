using iParkingv8.Ultility.Style;
using System.Reflection;

namespace iParkingv8.Object.ConfigObjects.LaneConfigs
{
    /// <summary>
    /// Thông tin hướng hiển thị trên phần giao diện
    /// </summary>
    public class LaneDirectionConfig
    {
        public enum EmDisplayDirection
        {
            HorizontalLeftToRight = 0,
            HorizontalRightToLeft = 1,
            VerTicalLeftToRight = 2,
            VerTicalRightToLeft = 3,
        }
        public static string ToDisplayString(EmDisplayDirection displayDirection)
        {
            switch (displayDirection)
            {
                case EmDisplayDirection.HorizontalLeftToRight:
                    return KZUIStyles.CurrentResources.HorizontalLeftToRight;
                case EmDisplayDirection.HorizontalRightToLeft:
                    return KZUIStyles.CurrentResources.HorizontalRightToLeft;
                case EmDisplayDirection.VerTicalLeftToRight:
                    return KZUIStyles.CurrentResources.VerticalLeftToRight;
                case EmDisplayDirection.VerTicalRightToLeft:
                    return KZUIStyles.CurrentResources.VerticalRightToLeft;
                default:
                    return displayDirection.ToString();
            }
        }
        public enum EmCameraDirection
        {
            Vertical = 0,
            Horizontal = 1,
        }
        public static string ToDisplayString(EmCameraDirection cameraDirection)
        {
            return cameraDirection switch
            {
                EmCameraDirection.Vertical => KZUIStyles.CurrentResources.Vertical,
                EmCameraDirection.Horizontal => KZUIStyles.CurrentResources.Horizontal,
                _ => cameraDirection.ToString(),
            };
        }
        public enum EmPicDirection
        {
            Vertical = 0,
            Horizontal = 1,
        }
        public static string ToDisplayString(EmPicDirection cameraDirection)
        {
            return cameraDirection switch
            {
                EmPicDirection.Vertical => KZUIStyles.CurrentResources.Vertical,
                EmPicDirection.Horizontal => KZUIStyles.CurrentResources.Horizontal,
                _ => cameraDirection.ToString(),
            };
        }

        public enum EmPlateDirection
        {
            Vertical = 0,
            HorizontalLeftToRight = 1,
            HorizontalRightToLeft = 2,
        }
        public static string ToDisplayString(EmPlateDirection direction)
        {
            return direction switch
            {
                EmPlateDirection.Vertical => KZUIStyles.CurrentResources.Vertical,
                EmPlateDirection.HorizontalLeftToRight => KZUIStyles.CurrentResources.HorizontalLeftToRight,
                EmPlateDirection.HorizontalRightToLeft => KZUIStyles.CurrentResources.HorizontalRightToLeft,
                _ => direction.ToString(),
            };
        }

        public enum EmCameraPicFunction
        {
            Vertical = 0,
            HorizontalLeftToRight = 1,
            HorizontalRightToLeft = 2,
        }

        public static string ToDisplayString(EmCameraPicFunction cameraPicDirection)
        {
            return cameraPicDirection switch
            {
                EmCameraPicFunction.Vertical => KZUIStyles.CurrentResources.Vertical,
                EmCameraPicFunction.HorizontalLeftToRight => KZUIStyles.CurrentResources.HorizontalLeftToRight,
                EmCameraPicFunction.HorizontalRightToLeft => KZUIStyles.CurrentResources.HorizontalRightToLeft,
                _ => cameraPicDirection.ToString(),
            };
        }
        public enum EmEventDirection
        {
            Vertical = 0,
            HorizontalLeftToRight = 1,
            HorizontalRightToLeft = 2,
        }

        public static string ToDisplayString(EmEventDirection eventDirection)
        {
            return eventDirection switch
            {
                EmEventDirection.Vertical => KZUIStyles.CurrentResources.Vertical,
                EmEventDirection.HorizontalLeftToRight => KZUIStyles.CurrentResources.HorizontalLeftToRight,
                EmEventDirection.HorizontalRightToLeft => KZUIStyles.CurrentResources.HorizontalRightToLeft,
                _ => eventDirection.ToString(),
            };
        }

        public enum EmViewOption
        {
            OnlyData,
            DataAndMoney,
        }
        public EmDisplayDirection displayDirection = EmDisplayDirection.VerTicalLeftToRight;
        public EmCameraPicFunction cameraPicDirection = EmCameraPicFunction.HorizontalLeftToRight;

        #region CAMERA
        public EmCameraDirection cameraDirection = EmCameraDirection.Vertical;
        public bool IsDisplayCameraTitle { get; set; } = true;
        public bool IsDisplayPanoramaCamera { get; set; } = true;
        public bool IsDisplayFaceCamera { get; set; } = true;
        public bool IsDisplayVehicleCamera { get; set; } = true;
        public bool IsDisplayOtherCamera { get; set; } = true;
        #endregion

        #region IMAGE
        public EmPicDirection picDirection = EmPicDirection.Vertical;
        public bool IsDisplayImageTitle { get; set; } = true;
        public bool IsDisplayPanoramaImage { get; set; } = true;
        public bool IsDisplayFaceImage { get; set; } = true;
        public bool IsDisplayVehicleImage { get; set; } = true;
        public bool IsDisplayOtherImage { get; set; } = true;
        #endregion

        #region EVENT
        public EmEventDirection eventDirection = EmEventDirection.HorizontalLeftToRight;
        public EmViewOption optionViewData = EmViewOption.OnlyData;
        public bool IsDisplayTitle { get; set; } = true;
        #endregion

        #region BUTTONS
        public bool IsDisplayWriteEventButton { get; set; } = true;
        public bool IsDisplayRetakeImageButton { get; set; } = true;
        public bool IsDisplayGuestRegisterButton { get; set; } = true;

        public bool IsDisplayOpenBarrieButton { get; set; } = false;
        public bool IsDisplayPrintButton { get; set; } = false;
        public bool IsDisplayCloseBarrieButton { get; set; } = false;
        public bool IsDisplayIconButtonOnly { get; set; } = false;
        #endregion

        #region PLATES
        public EmPlateDirection PlateInDirection = EmPlateDirection.Vertical;
        public EmPlateDirection PlateOutDirection = EmPlateDirection.Vertical;
        #endregion

        public static LaneDirectionConfig CreateDefaultInConfig()
        {
            return new LaneDirectionConfig()
            {
                displayDirection = EmDisplayDirection.VerTicalLeftToRight,
                cameraPicDirection = EmCameraPicFunction.HorizontalLeftToRight,

                #region CAMERA
                cameraDirection = EmCameraDirection.Vertical,
                IsDisplayCameraTitle = true,
                IsDisplayPanoramaCamera = true,
                IsDisplayVehicleCamera = true,
                IsDisplayFaceCamera = false,
                IsDisplayOtherCamera = false,
                #endregion

                #region IMAGE
                picDirection = EmPicDirection.Vertical,
                IsDisplayImageTitle = true,
                IsDisplayPanoramaImage = true,
                IsDisplayVehicleImage = true,
                IsDisplayFaceImage = false,
                IsDisplayOtherImage = false,
                #endregion

                #region EVENT
                eventDirection = EmEventDirection.HorizontalLeftToRight,
                IsDisplayTitle = true,
                optionViewData = EmViewOption.OnlyData,
                #endregion

                #region BUTTONS
                IsDisplayWriteEventButton = true,
                IsDisplayRetakeImageButton = true,
                IsDisplayOpenBarrieButton = true,
                IsDisplayPrintButton = false,

                IsDisplayGuestRegisterButton = false,
                IsDisplayCloseBarrieButton = false,
                IsDisplayIconButtonOnly = false,
                #endregion
            };
        }
        public static LaneDirectionConfig CreateDefaultInSmallConfig()
        {
            return new LaneDirectionConfig()
            {
                displayDirection = EmDisplayDirection.VerTicalLeftToRight,
                cameraPicDirection = EmCameraPicFunction.Vertical,

                #region CAMERA
                cameraDirection = EmCameraDirection.Vertical,
                IsDisplayCameraTitle = true,
                IsDisplayPanoramaCamera = true,
                IsDisplayVehicleCamera = true,
                IsDisplayFaceCamera = false,
                IsDisplayOtherCamera = false,
                #endregion

                #region IMAGE
                picDirection = EmPicDirection.Vertical,
                IsDisplayImageTitle = true,
                IsDisplayPanoramaImage = true,
                IsDisplayVehicleImage = true,
                IsDisplayFaceImage = false,
                IsDisplayOtherImage = false,
                #endregion

                #region EVENT
                eventDirection = EmEventDirection.Vertical,
                IsDisplayTitle = true,
                optionViewData = EmViewOption.OnlyData,
                #endregion

                #region BUTTONS
                IsDisplayWriteEventButton = true,
                IsDisplayRetakeImageButton = true,
                IsDisplayOpenBarrieButton = true,
                IsDisplayPrintButton = false,

                IsDisplayGuestRegisterButton = false,
                IsDisplayCloseBarrieButton = false,
                IsDisplayIconButtonOnly = false,
                #endregion
            };
        }

        public static LaneDirectionConfig CreateDefaultOutConfig()
        {
            return new LaneDirectionConfig()
            {
                displayDirection = EmDisplayDirection.VerTicalLeftToRight,
                cameraPicDirection = EmCameraPicFunction.Vertical,

                #region CAMERA
                cameraDirection = EmCameraDirection.Vertical,
                IsDisplayCameraTitle = true,
                IsDisplayPanoramaCamera = true,
                IsDisplayVehicleCamera = true,
                IsDisplayFaceCamera = false,
                IsDisplayOtherCamera = false,
                #endregion

                #region IMAGE
                picDirection = EmPicDirection.Vertical,
                IsDisplayImageTitle = true,
                IsDisplayPanoramaImage = true,
                IsDisplayVehicleImage = true,
                IsDisplayFaceImage = false,
                IsDisplayOtherImage = false,
                #endregion

                #region EVENT
                eventDirection = EmEventDirection.HorizontalLeftToRight,
                IsDisplayTitle = true,
                optionViewData = EmViewOption.DataAndMoney,
                #endregion

                #region BUTTONS
                IsDisplayWriteEventButton = true,
                IsDisplayRetakeImageButton = true,
                IsDisplayOpenBarrieButton = true,
                IsDisplayPrintButton = true,

                IsDisplayGuestRegisterButton = false,
                IsDisplayCloseBarrieButton = false,
                IsDisplayIconButtonOnly = false,
                #endregion
            };

        }
        public static LaneDirectionConfig CreateDefaultOutSmallConfig()
        {
            return new LaneDirectionConfig()
            {
                displayDirection = EmDisplayDirection.VerTicalLeftToRight,
                cameraPicDirection = EmCameraPicFunction.Vertical,

                #region CAMERA
                cameraDirection = EmCameraDirection.Vertical,
                IsDisplayCameraTitle = true,
                IsDisplayPanoramaCamera = true,
                IsDisplayVehicleCamera = true,
                IsDisplayFaceCamera = false,
                IsDisplayOtherCamera = false,
                #endregion

                #region IMAGE
                picDirection = EmPicDirection.Vertical,
                IsDisplayImageTitle = true,
                IsDisplayPanoramaImage = true,
                IsDisplayVehicleImage = true,
                IsDisplayFaceImage = false,
                IsDisplayOtherImage = false,
                #endregion

                #region EVENT
                eventDirection = EmEventDirection.Vertical,
                IsDisplayTitle = true,
                optionViewData = EmViewOption.DataAndMoney,
                #endregion

                #region BUTTONS
                IsDisplayWriteEventButton = true,
                IsDisplayRetakeImageButton = true,
                IsDisplayOpenBarrieButton = true,
                IsDisplayPrintButton = true,

                IsDisplayGuestRegisterButton = false,
                IsDisplayCloseBarrieButton = false,
                IsDisplayIconButtonOnly = false,
                #endregion
            };

        }

        public bool IsSameConfig(LaneDirectionConfig config)
        {
            if (ReferenceEquals(this, config)) return true;
            if (config is null) return false;

            var type = typeof(LaneDirectionConfig);

            // Lấy tất cả public instance properties có getter
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Where(p => p.CanRead);

            foreach (var p in props)
            {
                var v1 = p.GetValue(this);
                var v2 = p.GetValue(config);

                if (!Equals(v1, v2))
                    return false;
            }

            return true;
        }
    }
}
