using Kztek.Tool;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Kztek.Api
{
    public class ApiHelper
    {
        public static string startupPath = "";
        public static int timeOut = 10000;
        public static string tokenHeader = "token";
        public const int max_send_times = 2;

        public static async Task<RestResponse> GeneralBasicAuthJsonAPI(string apiUrl, string username, string password, object data,
                                                                       Dictionary<string, string> headerValues, Dictionary<string, string> requiredParams,
                                                                       int timeOut, Method method,
                                                                       [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest()
            {
                RequestFormat = DataFormat.Json,
                Method = method,
                Timeout = TimeSpan.FromMilliseconds(timeOut),
                Authenticator = new HttpBasicAuthenticator(username, password),
            };
            if (data != null)
                request.AddJsonBody(data);

            request.AddHeaders(headerValues);
            foreach (KeyValuePair<string, string> kvp in requiredParams)
            {
                request.AddQueryParameter(kvp.Key, kvp.Value);
            }

            var response = client.Execute(request);
            return response;
        }

        public static ApiResponse GeneralJsonAPI(string apiUrl, object data, Dictionary<string, string>? headerValues,
                                            Dictionary<string, string>? requiredParams, int timeOutInMilisecond, Method method,
                                            [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            for (int i = 0; i < max_send_times; i++)
            {
                using var client = new RestClient(apiUrl);
                var request = new RestRequest
                {
                    Method = method,
                    Timeout = TimeSpan.FromMilliseconds(timeOutInMilisecond),
                    RequestFormat = DataFormat.Json
                };
                if (data != null)
                    request.AddJsonBody(data);

                if (headerValues != null)
                {
                    foreach (KeyValuePair<string, string> item in headerValues)
                    {
                        request.AddHeader(item.Key, item.Value);
                    }
                }

                if (requiredParams != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requiredParams)
                    {
                        request.AddQueryParameter(kvp.Key, kvp.Value);
                    }
                }

                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    if (i == max_send_times - 1)
                    {
                        return new ApiResponse()
                        {
                            IsSuccess = false,
                            ApiStatusCode = (int)response.StatusCode,
                            ErrorMessage = response.ErrorMessage ?? "",
                            Response = response.Content ?? ""
                        };
                    }
                    continue;
                }

                return new ApiResponse()
                {
                    IsSuccess = true,
                    ApiStatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ErrorMessage ?? "",
                    Response = response.Content ?? ""
                };
            }

            return new ApiResponse()
            {
                IsSuccess = false,
                ApiStatusCode = -1,
                ErrorMessage = "",
                Response = ""
            };

        }

        public static ApiResponse GeneralStringJsonAPI(string apiUrl, string data, Dictionary<string, string>? headerValues,
                                            Dictionary<string, string> requiredParams, int timeOut, Method method, bool isSaveLog = true,
                                            [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {

            for (int i = 0; i < max_send_times; i++)
            {
                var client = new RestClient(apiUrl);
                var request = new RestRequest
                {
                    Timeout = TimeSpan.FromMilliseconds(timeOut),
                    Method = method
                };
                if (data != null)
                    request.AddParameter("application/json", data, ParameterType.RequestBody);

                if (headerValues != null)
                    request.AddHeaders(headerValues);

                if (requiredParams != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requiredParams)
                    {
                        request.AddQueryParameter(kvp.Key, kvp.Value);
                    }
                }

                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    if (i == max_send_times - 1)
                    {
                        return new ApiResponse()
                        {
                            IsSuccess = false,
                            ApiStatusCode = (int)response.StatusCode,
                            ErrorMessage = response.ErrorMessage ?? "",
                            Response = response.Content ?? ""
                        };
                    }
                    continue;
                }

                return new ApiResponse()
                {
                    IsSuccess = true,
                    ApiStatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ErrorMessage ?? "",
                    Response = response.Content ?? ""
                };
            }
            return new ApiResponse()
            {
                IsSuccess = false,
                ApiStatusCode = -1,
                ErrorMessage = "",
                Response = ""
            };
        }

        public static string RemoveImageBase64(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    return string.Empty;
                }
                var jObj = JObject.Parse(json);

                // Xóa toàn bộ mảng images
                if (jObj["images"] != null)
                {
                    jObj["images"] = new JArray(); // hoặc jObj.Remove("images");
                }

                // Nếu trong entry có nested images
                if (jObj["entry"] != null && jObj["entry"]["images"] != null)
                {
                    jObj["entry"]["images"] = new JArray();
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(jObj);
            }
            catch
            {
                return json; // Trả lại nguyên gốc nếu có lỗi khi parse
            }
        }

        public static async Task<ApiResponse> GeneralJsonAPIAsync(string apiUrl, object? data,
                                                                  Dictionary<string, string>? headerValues,
                                                                  Dictionary<string, string>? requiredParams,
                                                                  int timeOutInMilisecond, Method method,
                                                                  [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            string apiId = Guid.NewGuid().ToString();
            string errorMessage = string.Empty;

            for (int i = 0; i < max_send_times; i++)
            {
                DateTime startTime = DateTime.Now;
                ApiLogDetail apiLogDetail = new ApiLogDetail()
                {
                    EndPoint = apiUrl,
                    ApiLogId = apiId,
                    CreatedDate = startTime,
                    ApiMethod = method.ToString(),
                    Headers = headerValues,
                    Params = requiredParams,
                    Body = data,
                    Description = $"START {i + 1}",
                    Action = Object.EmSystemAction.MainServer,
                    ActionType = Object.EmSystemActionType.INFO,
                };
                SystemUtils.logger.SaveAPILogDetail(apiLogDetail, callerName, lineNumber, filePath);

                using var client = new RestClient(apiUrl);
                var request = new RestRequest
                {
                    Method = method,
                    RequestFormat = DataFormat.Json
                };
                if (timeOutInMilisecond > 0)
                {
                    request.Timeout = TimeSpan.FromMilliseconds(timeOutInMilisecond);
                }

                if (data != null)
                {
                    request.AddJsonBody(data);
                }

                if (headerValues != null)
                {
                    foreach (KeyValuePair<string, string> item in headerValues)
                    {
                        request.AddHeader(item.Key, item.Value);
                    }
                }

                if (requiredParams != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requiredParams)
                    {
                        request.AddQueryParameter(kvp.Key, kvp.Value);
                    }
                }

                var response = await client.ExecuteAsync(request);

                DateTime endTime = DateTime.Now;

                ApiLogDetail apiLogDetail2 = new ApiLogDetail()
                {
                    EndPoint = apiUrl,
                    ApiLogId = apiId,
                    CreatedDate = endTime,
                    ApiMethod = method.ToString(),
                    Headers = headerValues,
                    Params = requiredParams,
                    Body = data,
                    ResponseStatus = (int)response.StatusCode,
                    ResponseContent = response.Content,
                    Ex = response.ErrorException,
                    Description = $"End {i + 1}",
                    Action = Object.EmSystemAction.MainServer,
                    ActionType = Object.EmSystemActionType.INFO,
                };
                SystemUtils.logger.SaveAPILogDetail(apiLogDetail2, callerName, lineNumber, filePath);

                ApiLog apiLog = new ApiLog()
                {
                    Id = apiId,
                    EndPoint = apiUrl,
                    StartTime = startTime,
                    EndTime = endTime,
                    ApiMethod = method.ToString(),
                    Headers = headerValues,
                    Params = requiredParams,
                    Body = data,
                    ResponseStatus = (int)response.StatusCode,
                    ResponseContent = response.Content,
                    Ex = response.ErrorException,
                    Action = Object.EmSystemAction.MainServer_Sumary,
                    ActionType = Object.EmSystemActionType.INFO,
                };
                SystemUtils.logger.SaveAPILog(apiLog, callerName, lineNumber, filePath);

                if (response.IsSuccessful)
                {
                    return new ApiResponse()
                    {
                        IsSuccess = true,
                        ApiStatusCode = (int)response.StatusCode,
                        ErrorMessage = response.ErrorMessage ?? "",
                        Response = response.Content ?? ""
                    };
                }
                var logResponse = response;
                //Không nhận được phản hồi từ server (Time out, ...)
                if (string.IsNullOrEmpty(response.Content))
                {
                    if (i == max_send_times - 1)
                    {
                        return new ApiResponse()
                        {
                            IsSuccess = false,
                            ApiStatusCode = (int)response.StatusCode,
                            ErrorMessage = response.ErrorMessage ?? "",
                            Response = ""
                        };
                    }
                    errorMessage = "Empty Response";
                    continue;
                }
                //Nhận được phản hồi từ server
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ApiStatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ErrorMessage ?? "",
                    Response = response.Content
                };

            }
            return new ApiResponse()
            {
                IsSuccess = false,
                ApiStatusCode = -1,
                ErrorMessage = "",
                Response = ""
            };
        }
        public static async Task<ApiResponse> GeneralJsonAPIAsync(string apiUrl, object? data,
                                                                     Dictionary<string, string>? headerValues,
                                                                     Dictionary<string, string>? requiredParams,
                                                                     Dictionary<string, object>? formData,
                                                                     int timeOutInMilisecond, Method method, bool isSaveLog = true,
                                                                     [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            string apiId = Guid.NewGuid().ToString();
            string errorMessage = string.Empty;

            for (int i = 0; i < max_send_times; i++)
            {
                DateTime startTime = DateTime.Now;
                if (isSaveLog)
                {
                    ApiLogDetail apiLogDetail = new ApiLogDetail()
                    {
                        EndPoint = apiUrl,
                        ApiLogId = apiId,
                        CreatedDate = startTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = formData,
                        Description = $"START {i + 1}",
                        Action = Object.EmSystemAction.MainServer,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILogDetail(apiLogDetail, callerName, lineNumber, filePath);
                }

                var client = new RestClient(apiUrl);
                var request = new RestRequest
                {
                    Method = method,
                    RequestFormat = DataFormat.Json
                };
                if (timeOutInMilisecond > 0)
                {
                    request.Timeout = TimeSpan.FromMilliseconds(timeOutInMilisecond);
                }

                if (data != null)
                {
                    request.AddJsonBody(data);
                }


                if (formData != null)
                {
                    foreach (var kv in formData)
                    {
                        if (kv.Value is byte[] fileBytes)
                        {
                            request.AddFile(kv.Key, fileBytes, $"{kv.Key}.jpeg", "image/jpeg"); // đổi đuôi tùy file
                        }
                        else
                        {
                            request.AddParameter(kv.Key, kv.Value?.ToString());
                        }
                    }
                }

                if (headerValues != null)
                {
                    foreach (KeyValuePair<string, string> item in headerValues)
                    {
                        request.AddHeader(item.Key, item.Value);
                    }
                }

                if (requiredParams != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requiredParams)
                    {
                        request.AddQueryParameter(kvp.Key, kvp.Value);
                    }
                }

                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {

                }
                if (isSaveLog)
                {
                    DateTime endTime = DateTime.Now;

                    ApiLogDetail apiLogDetail = new ApiLogDetail()
                    {
                        EndPoint = apiUrl,
                        ApiLogId = apiId,
                        CreatedDate = endTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = data,
                        ResponseStatus = (int)response.StatusCode,
                        ResponseContent = response.Content,
                        Ex = response.ErrorException,
                        Description = $"End {i + 1}",
                        Action = Object.EmSystemAction.MainServer,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILogDetail(apiLogDetail, callerName, lineNumber, filePath);

                    ApiLog apiLog = new ApiLog()
                    {
                        Id = apiId,
                        EndPoint = apiUrl,
                        StartTime = startTime,
                        EndTime = endTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = data,
                        ResponseStatus = (int)response.StatusCode,
                        ResponseContent = response.Content,
                        Ex = response.ErrorException,
                        Action = Object.EmSystemAction.MainServer_Sumary,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILog(apiLog, callerName, lineNumber, filePath);
                }

                if (response.IsSuccessful)
                {
                    return new ApiResponse()
                    {
                        IsSuccess = true,
                        ApiStatusCode = (int)response.StatusCode,
                        ErrorMessage = response.ErrorMessage ?? "",
                        Response = response.Content ?? ""
                    };
                }
                //Không nhận được phản hồi từ server (Time out, ...)
                if (string.IsNullOrEmpty(response.Content))
                {
                    if (i == max_send_times - 1)
                    {
                        return new ApiResponse()
                        {
                            IsSuccess = false,
                            ApiStatusCode = (int)response.StatusCode,
                            ErrorMessage = response.ErrorMessage ?? "",
                            Response = ""
                        };
                    }
                    errorMessage = "Empty Response";
                    continue;
                }
                //Nhận được phản hồi từ server
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ApiStatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ErrorMessage ?? "",
                    Response = response.Content
                };

            }
            return new ApiResponse()
            {
                IsSuccess = false,
                ApiStatusCode = -1,
                ErrorMessage = "",
                Response = ""
            };
        }
        public static ApiResponse GeneralJsonAPI(string apiUrl, object? data,
                                                                    Dictionary<string, string>? headerValues,
                                                                    Dictionary<string, string>? requiredParams,
                                                                    Dictionary<string, object>? formData,
                                                                    int timeOutInMilisecond, Method method, bool isSaveLog = true,
                                                                    [CallerMemberName] string callerName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            string apiId = Guid.NewGuid().ToString();
            string errorMessage = string.Empty;

            for (int i = 0; i < max_send_times; i++)
            {
                DateTime startTime = DateTime.Now;
                if (isSaveLog)
                {
                    ApiLogDetail apiLogDetail = new ApiLogDetail()
                    {
                        EndPoint = apiUrl,
                        ApiLogId = apiId,
                        CreatedDate = startTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = formData,
                        Description = $"START {i + 1}",
                        Action = Object.EmSystemAction.MainServer,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILogDetail(apiLogDetail, callerName, lineNumber, filePath);
                }

                var client = new RestClient(apiUrl);
                var request = new RestRequest
                {
                    Method = method,
                    RequestFormat = DataFormat.Json
                };
                if (timeOutInMilisecond > 0)
                {
                    request.Timeout = TimeSpan.FromMilliseconds(timeOutInMilisecond);
                }

                if (data != null)
                {
                    request.AddJsonBody(data);
                }


                if (formData != null)
                {
                    foreach (var kv in formData)
                    {
                        if (kv.Value is byte[] fileBytes)
                        {
                            request.AddFile(kv.Key, fileBytes, $"{kv.Key}.jpeg", "image/jpeg"); // đổi đuôi tùy file
                        }
                        else
                        {
                            request.AddParameter(kv.Key, kv.Value?.ToString());
                        }
                    }
                }

                if (headerValues != null)
                {
                    foreach (KeyValuePair<string, string> item in headerValues)
                    {
                        request.AddHeader(item.Key, item.Value);
                    }
                }

                if (requiredParams != null)
                {
                    foreach (KeyValuePair<string, string> kvp in requiredParams)
                    {
                        request.AddQueryParameter(kvp.Key, kvp.Value);
                    }
                }

                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {

                }
                if (isSaveLog)
                {
                    DateTime endTime = DateTime.Now;

                    ApiLogDetail apiLogDetail = new ApiLogDetail()
                    {
                        EndPoint = apiUrl,
                        ApiLogId = apiId,
                        CreatedDate = endTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = data,
                        ResponseStatus = (int)response.StatusCode,
                        ResponseContent = response.Content,
                        Ex = response.ErrorException,
                        Description = $"End {i + 1}",
                        Action = Object.EmSystemAction.MainServer,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILogDetail(apiLogDetail, callerName, lineNumber, filePath);

                    ApiLog apiLog = new ApiLog()
                    {
                        Id = apiId,
                        EndPoint = apiUrl,
                        StartTime = startTime,
                        EndTime = endTime,
                        ApiMethod = method.ToString(),
                        Headers = headerValues,
                        Params = requiredParams,
                        Body = data,
                        ResponseStatus = (int)response.StatusCode,
                        ResponseContent = response.Content,
                        Ex = response.ErrorException,
                        Action = Object.EmSystemAction.MainServer_Sumary,
                        ActionType = Object.EmSystemActionType.INFO,
                    };
                    SystemUtils.logger.SaveAPILog(apiLog, callerName, lineNumber, filePath);
                }

                if (response.IsSuccessful)
                {
                    return new ApiResponse()
                    {
                        IsSuccess = true,
                        ApiStatusCode = (int)response.StatusCode,
                        ErrorMessage = response.ErrorMessage ?? "",
                        Response = response.Content ?? ""
                    };
                }
                //Không nhận được phản hồi từ server (Time out, ...)
                if (string.IsNullOrEmpty(response.Content))
                {
                    if (i == max_send_times - 1)
                    {
                        return new ApiResponse()
                        {
                            IsSuccess = false,
                            ApiStatusCode = (int)response.StatusCode,
                            ErrorMessage = response.ErrorMessage ?? "",
                            Response = ""
                        };
                    }
                    errorMessage = "Empty Response";
                    continue;
                }
                //Nhận được phản hồi từ server
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ApiStatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ErrorMessage ?? "",
                    Response = response.Content
                };

            }
            return new ApiResponse()
            {
                IsSuccess = false,
                ApiStatusCode = -1,
                ErrorMessage = "",
                Response = ""
            };
        }

    }
}
