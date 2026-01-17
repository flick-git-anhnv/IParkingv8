using iParkingv8.Object.Enums.CMS;
namespace iParkingv8.Object.Objects.CMS
{
    public class ControlUnitMonitor
    {
        public string Name { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public EmDeviceStatus Status { get; set; }
        public List<string> Message { get; set; } = [];
    }
}
