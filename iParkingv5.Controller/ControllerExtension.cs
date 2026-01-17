using iParkingv6.Objects.Datas;
using Kztek.Tool;

namespace iParkingv5.Controller
{
    public static class ControllerExtension
    {
        public static void SaveDeviceLog(this Bdk bdk, string cmd, string response)
        {
            if (bdk is null || SystemUtils.logger is null)
            {
                return;
            }
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceId = bdk.Id,
                DeviceName = $"{bdk.Name} - {bdk.Comport} - {bdk.Baudrate}",
                Cmd = cmd,
                Response = response
            });
        }
    }
}
