using iParkingv8.Object.Objects.Parkings;

namespace IParkingv8
{
    public class LoopLprResult
    {
        public DateTime CreatedAt = DateTime.Now;
        public Image? PanoramaImage { get; set; } = null;
        public Image? VehicleImage { get; set; } = null;
        public Image? LprImage { get; set; } = null;
        public AccessKey? Vehicle { get; set; } = null;
        public string PlateNumber { get; set; } = string.Empty;
    }
}
