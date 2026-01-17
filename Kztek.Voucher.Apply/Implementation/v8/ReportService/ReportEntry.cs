using iParkingv8.Object.Objects.Events;
using IParkingv8.API.Interfaces;
using IParkingv8.API.Objects;
using Kztek.Api;
using Kztek.Object;
using Kztek.Tool;
using RestSharp;
using System.Runtime.CompilerServices;
using static IParkingv8.API.Implementation.v8.ApiUrlManagement;
using static IParkingv8.API.Objects.Filter;

namespace IParkingv8.API.Implementation.v8.ReportService
{
    public class ReportEntry(IAuth auth) : IReportEntry
    {
        public IAuth Auth { get; set; } = auth;
        public async Task<EntryData?> GetEntryByAccessKeyIdAsync(string accessId, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true)
        {
            string server = Auth.ServerConfig.ApiUrl.StandardlizeServerName();
            string apiUrl = $"{server}{GetInitRoute(EmApiObjectType.ENTRIES)}";
            string baseLog = $"Get Entry By AccessKeyId {accessId}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(baseLog), callerName, lineNumber, filePath, isSaveLog);
            Dictionary<string, string> headers = new()
            {
                { "Authorization","Bearer " + Auth.ApiAuthResult.AccessToken  }
            };
            var parameters = new Dictionary<string, string>
            {
                { "accessKeyId", accessId },
                { "presignedUrl", "true" }
            };
            var apiResponse = await ApiHelper.GeneralJsonAPIAsync(apiUrl, null, headers, parameters, timeOut, Method.Get, isSaveLog, callerName, lineNumber, filePath);
            if (apiResponse.IsSuccess)
            {
                try
                {
                    var report = Newtonsoft.Json.JsonConvert.DeserializeObject<EntryData?>(apiResponse.Response);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Success"), callerName, lineNumber, filePath, isSaveLog);
                    return report;
                }
                catch (Exception ex)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Error: " + apiResponse.Response, ex, ActionType: EmSystemActionType.ERROR), callerName, lineNumber, filePath, isSaveLog);
                    return null;
                }
            }
            else
            {
                if (apiResponse.ApiStatusCode == 401)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Error", "Un Auth", ActionType: EmSystemActionType.WARNING), callerName, lineNumber, filePath, isSaveLog);
                    await Auth.LoginAsync();
                    return await GetEntryByAccessKeyIdAsync(accessId, timeOut, accessId, lineNumber, filePath, isSaveLog);
                }
                else
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Error Code", apiResponse.ApiStatusCode, ActionType: EmSystemActionType.ERROR), callerName, lineNumber, filePath, isSaveLog);
                    return null;
                }
            }
        }
    }
}
