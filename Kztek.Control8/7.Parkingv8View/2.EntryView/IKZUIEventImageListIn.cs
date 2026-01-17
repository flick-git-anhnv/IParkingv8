using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;

namespace Kztek.Control8.UserControls
{
    public interface IKZUIEventImageListIn
    {
        bool KZUI_IsDisplayTitle { get; set; }
        bool KZUI_IsDisplayPanorama { get; set; }
        bool KZUI_IsDisplayVehicle { get; set; }
        bool KZUI_IsDisplayFace { get; set; }
        bool KZUI_IsDisplayOther { get; set; }


        void Init(string title, string titlePanorama, string titleVehicle,
                  string titleFace, string titleOther,
                  Image? defaultImage);

        void Init(EmControlSizeMode controlSizeMode, EmControlDirection controlDirection, bool isSpaceBetween);

        void ClearView();

        void DisplayImage(Dictionary<EmImageType, Image?> images);
        void DisplayImageData(List<EventImageDto> imageDatas);
    }
}
