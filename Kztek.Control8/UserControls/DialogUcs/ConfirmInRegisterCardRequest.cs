using iParkingv8.Object.Enums.Bases;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public class ConfirmInRegisterCardRequest
    {
        public string Code { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public string Collection { get; set; } = string.Empty;
        public Dictionary<EmImageType, Image?> Images { get; set; } = [];
    }
}
