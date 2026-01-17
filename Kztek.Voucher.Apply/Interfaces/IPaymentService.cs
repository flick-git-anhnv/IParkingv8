using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IParkingv8.API.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ApplyVoucherEntryAsync(string voucherData, string entryId, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true);
    }
}
