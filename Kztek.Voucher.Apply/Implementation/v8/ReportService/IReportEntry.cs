using IParkingv8.API.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IParkingv8.API.Implementation.v8.ReportService
{
    public interface IReportEntry
    {
        IAuth Auth { get; set; }
        Task<EntryData?> GetEntryByAccessKeyIdAsync(string accessId, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);
    }
}
