using iParkingv8.Object.Enums.CMS;

namespace iParkingv8.Object.Objects.CMS
{
    /// <summary>
    /// Dữ liệu trạng thái cập nhật lên rabbit
    /// </summary>
    public class ComputerMonitorMessage
    {
        public EmMonitorMessageType MessageType { get; set; } = EmMonitorMessageType.Heartbeat;
        public ComputerMonitor? Computer { get; set; }
    }
}
