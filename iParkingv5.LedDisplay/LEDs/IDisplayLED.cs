using Kztek.Object;

namespace iParkingv5.LedDisplay.LEDs
{
    public interface IDisplayLED
    {
        bool Connect(Led led);
        void SendToLED(ParkingData parkingData, LedDisplayConfig ledDisplayConfig);
    }
}
