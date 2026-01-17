using IParkingv8.API.Implementation.v8.ReportService;

namespace IParkingv8.API.Interfaces
{
    public interface IReportService
    {
        IAuth Auth { get; set; }
        IReportEntry Entry { get; set; }
    }
}
