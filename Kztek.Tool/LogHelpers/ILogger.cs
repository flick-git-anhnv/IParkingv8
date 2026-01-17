using System;
using System.Runtime.CompilerServices;

namespace Kztek.Tool
{
    public interface ILogger
    {
        string SaveLogFolder { get; set; }
        bool IsSaveLog { get; set; }

        void InitLogService();
        void Disconnect();

        void SaveAPILog(ApiLog apiLog,
                        [CallerMemberName] string callerName = "",
                        [CallerLineNumber] int lineNumber = 0,
                        [CallerFilePath] string filePath = "");
        void SaveAPILogDetail(ApiLogDetail apiLogDetail,
                             [CallerMemberName] string callerName = "",
                             [CallerLineNumber] int lineNumber = 0,
                             [CallerFilePath] string filePath = "");
        void SaveDeviceLog(DeviceLog deviceLog,
                          [CallerMemberName] string callerName = "",
                          [CallerLineNumber] int lineNumber = 0,
                          [CallerFilePath] string filePath = "");
        void SaveDeviceStatusLog(DeviceStatusLog deviceStatusLog,
                                [CallerMemberName] string callerName = "",
                                [CallerLineNumber] int lineNumber = 0,
                                [CallerFilePath] string filePath = "");
        void SaveSystemLog(SystemLog systemLog,
                                [CallerMemberName] string callerName = "",
                                [CallerLineNumber] int lineNumber = 0,
                                [CallerFilePath] string filePath = "");
        void SaveUserLog(UserLog userLog,
                                [CallerMemberName] string callerName = "",
                                [CallerLineNumber] int lineNumber = 0,
                                [CallerFilePath] string filePath = "");

        void ClearLogAfterDay(int day);
        void ClearLogBeforeTime(DateTime time);
        string GetLogByQuery(string query);
    }
}
