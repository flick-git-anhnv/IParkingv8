using iParkingv5.Objects.Events;
using iParkingv8.Object.Objects.Devices;

namespace Kztek.Control8
{
    public class MotionDetectEventArgs : GeneralEventArgs
    {
        public Camera? DetectCamera { get; init; }
        public Image? EventImage { get; set; }
    }
}
