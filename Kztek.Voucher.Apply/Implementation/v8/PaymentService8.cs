using IParkingv8.API.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static IParkingv8.API.Implementation.v8.ApiUrlManagement;

namespace IParkingv8.API.Implementation.v8
{
    public class PaymentService8 : IPaymentService
    {
        public IAuth Auth { get; set; }
        public PaymentService8(IAuth auth)
        {
            Auth = auth;
        }
        public async Task<bool> ApplyVoucherEntryAsync(string voucherCode, string accessKeyCode, int timeOut = 10000, [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "", bool isSaveLog = true)
        {
            string server = Auth.ServerConfig.ApiUrl.StandardlizeServerName();
            string apiUrl = $"{server}{ApiUrlManagement.GetInitRoute(EmApiObjectType.DISCOUNTS)}";
            string baseLog = $"Apply Discount Entry {accessKeyCode} - {voucherCode}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(baseLog), callerName, lineNumber, filePath, isSaveLog);
            Dictionary<string, string> headers = new()
            {
                { "Authorization","Bearer " + Auth.ApiAuthResult.AccessToken  }
            };

            var body = new
            {
                VoucherCode = voucherCode,
                AccessKeyCode = accessKeyCode,
                TransactionTargetType = "Entry",
            };
            var apiResponse = await ApiHelper.GeneralJsonAPIAsync(apiUrl, body, headers, null, timeOut, Method.Post, isSaveLog, callerName, lineNumber, filePath);

            if (apiResponse.IsSuccess)
            {
                try
                {
                    var voucherDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<VoucherDetail>(apiResponse.Response);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Success"), callerName, lineNumber, filePath, isSaveLog);
                    return Tuple.Create<VoucherDetail?, BaseErrorData?>(voucherDetail, null);
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
                    return await ApplyVoucherEntryAsync(voucherCode, accessKeyCode, timeOut, callerName, lineNumber, filePath, isSaveLog);
                }
                else
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{baseLog} Error Code", apiResponse.ApiStatusCode, ActionType: EmSystemActionType.ERROR), callerName, lineNumber, filePath, isSaveLog);
                    var errorData = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseErrorData>(apiResponse.Response);
                    if (errorData != null)
                    {
                        errorData.Route = "discounts";
                    }
                    return Tuple.Create<VoucherDetail?, BaseErrorData?>(null, errorData);
                }
            }
        }
    }
}
