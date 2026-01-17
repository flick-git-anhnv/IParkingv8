using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;

namespace Kztek.Control8.UserControls
{
    public interface IKZUIEventImageListOut
    {
        bool KZUI_IsDisplayTitle { get; set; }
        bool KZUI_IsDisplayPanorama { get; set; }
        bool KZUI_IsDisplayVehicle { get; set; }
        bool KZUI_IsDisplayFace { get; set; }
        bool KZUI_IsDisplayOther { get; set; }


        void Init(Image? defaultImage);
        void Init(EmControlSizeMode controlSizeMode);

        void ClearView();

        void DisplayEntryImage(Dictionary<EmImageType, Image?> images);
        void DisplayExitImage(Dictionary<EmImageType, Image?> images);

        void DisplayEntryImageData(List<EventImageDto> imageDatas);
        void DisplayExitImageData(List<EventImageDto> imageDatas);
    }
}
