using iParkingv8.Object.Objects.Parkings;
using System.Drawing;

namespace iParkingv8.Object.Objects.Kiosk
{
    public enum EmImageDialogType
    {
        Error,
        Infor,
        Success
    }
    public class KioskDialogRequest(string Title, string subTitle, string backTitle,
                              EmImageDialogType dialogType, Image? dislayImage)
    {
        public string TitleLanguageTag { get; set; } = Title;
        public string SubTitleLanguageTag { get; set; } = subTitle;
        public string BackTitleTag { get; set; } = backTitle;
        public EmImageDialogType DialogType { get; set; } = dialogType;
        public Image? DisplayImage { get; set; } = dislayImage;
    }

    public class KioskDialogDialyRequest(string title, string subTitle,
                                   EmImageDialogType dialogType,
                                   string detectedPlate, AccessKey? accessKey,
                                   Image? vehicleImage, Image? panoramaImage)
    {
        public string TitleTag { get; set; } = title;
        public string SubTitleTag { get; set; } = subTitle;

        public EmImageDialogType DialogType { get; set; } = dialogType;

        public Image? VehicleImage { get; set; } = vehicleImage;
        public Image? PanoramaImage { get; set; } = panoramaImage;

        public string DetectedPlate { get; set; } = detectedPlate;
        public AccessKey? AccessKey { get; set; } = accessKey;
    }

    public class KioskDialogMonthlyRequest(string TitleTag, string subTitletag,
                                      EmImageDialogType dialogType,
                                      string detectedPlate, AccessKey? accessKey, AccessKey? registerVehicle,
                                      Image? vehicleImage, Image? panoramaImage)
    {
        public string TitleTag { get; set; } = TitleTag;
        public string SubTitleTag { get; set; } = subTitletag;
        public EmImageDialogType DialogType { get; set; } = dialogType;

        public Image? VehicleImage { get; set; } = vehicleImage;
        public Image? PanoramaImage { get; set; } = panoramaImage;

        public string DetectedPlate { get; set; } = detectedPlate;
        public AccessKey? AccessKey { get; set; } = accessKey;
        public AccessKey? RegisterVehicle { get; set; } = registerVehicle;
    }
}
