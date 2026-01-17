using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Parkings;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public class ConfirmInRequest
    {
        public string Message { get; set; } = string.Empty;
        public string RegisterPlate { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public AccessKey? AccessKey { get; set; }
        public EmAlarmCode AbnormalCode { get; set; }

        public Dictionary<EmImageType, Image?> Images { get; set; } = [];
    }
}
