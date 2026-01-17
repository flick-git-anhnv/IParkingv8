using iParkingv8.Object.Enums.Parkings;

namespace ILedv8.Objects
{
    public class LedLaneConfig
    {
        public string LedId { get; set; } = string.Empty;
        public int Line { get; set; }
        public int MaxNumber { get; set; }
        public List<EmVehicleType> SupportVehicles { get; set; } = [];
        public List<string> LaneIds { get; set; } = [];
    }
}
