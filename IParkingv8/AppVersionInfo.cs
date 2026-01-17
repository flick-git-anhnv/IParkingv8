using iParkingv8.Ultility;
using System.Diagnostics;

namespace IParkingv8
{
    public class AppVersionInfo
    {
        public string ConfigurationManager { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParking.ConfigurationManager.dll")).FileVersion!.ToString();
        public string Controller { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParkingv5.Controller.dll")).FileVersion!.ToString();
        public string LedDisplay { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParkingv5.LedDisplay.dll")).FileVersion!.ToString();
        public string Lpr { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParkingv5.Lpr.dll")).FileVersion!.ToString();
        public string Main { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IParkingv8.dll")).FileVersion!.ToString();
        public string API { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IParkingv8.API.dll")).FileVersion!.ToString();
        public string Auth { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParkingv8.Auth.dll")).FileVersion!.ToString();
        public string Object { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "iParkingv8.Object.dll")).FileVersion!.ToString();
        public string Printer { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IParkingv8.Printer.dll")).FileVersion!.ToString();
        public string Reporting { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IParkingv8.Reporting.dll")).FileVersion!.ToString();
        public string Ultility { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IPaking.Ultility.dll")).FileVersion!.ToString();
        public string BaseAPI { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "Kztek.Api.dll")).FileVersion!.ToString();
        public string BaseCamera { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "Kztek.Cameras.dll")).FileVersion!.ToString();
        public string Control { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "Kztek.Control8.dll")).FileVersion!.ToString();
        public string BaseObject { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "Kztek.Object.Entity.dll")).FileVersion!.ToString();
        public string BaseTool { get; set; } = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "Kztek.Tool.dll")).FileVersion!.ToString();

    }

}
