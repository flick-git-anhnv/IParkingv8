using iParkingv8.Object.Objects.Users;
using System.Runtime.CompilerServices;

namespace IParkingv8.API.Interfaces
{
    public interface IUserService
    {
        Task GetUserDetailAsync([CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "");
        Task<Tuple<List<User>, string>> GetUserDataAsync([CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "");

        Task<bool> ChangePassword(string userId, string username, string newPassword);
    }
}
