using Futech.Tools.TextFormatingTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Futech.Tools
{
    public class LogHelperv2
    {
        public class FunctionExcecute
        {
            public string FunctionName { get; set; } = string.Empty;


            public string FileName { get; set; } = string.Empty;


            public int LineNumber { get; set; } = 0;

        }

        public enum EM_LogType
        {
            SYSTEM_INFOR,
            SYSTEM_WARNING,
            SYSTEM_ERROR,
            API_INFOR,
            API_WARNING,
            API_ERROR,
            CAMERA_INFOR,
            CAMERA_WARNING,
            CAMERA_ERROR,
            CONTROLLER_INFOR,
            CONTROLLER_WARNING,
            CONTROLLER_ERROR,
            LPR_INFOR,
            LPR_WARNING,
            LPR_ERROR,
            IPC_INFOR,
            IPC_WARNING,
            IPC_ERROR,
            BLACK_LIST_INFOR,
            Application_Progress
        }

        private static object lockObject = new object();

        public static string SaveLogFolder = "";

        private static Dictionary<string, StreamWriter> streamWriters = new Dictionary<string, StreamWriter>();

        public static void Log(EM_LogType logType, string message, string startUpPath, DateTime logTime, object obj = null, int limitLength = 0, string specailName = "")
        {
            lock (lockObject)
            {
                try
                {
                    if (obj != null)
                    {
                        message += TextFormatingTool.BeautyJson(obj);
                    }

                    string savePath = GetSavePath(logType, startUpPath);
                    Directory.CreateDirectory(savePath);
                    string text = "";
                    int limitSizeInByte = 1024000;
                    if (string.IsNullOrEmpty(specailName))
                    {
                        string basePath = Path.GetDirectoryName(savePath) + "\\" + logType.ToString() + "_" + DateTime.Now.Hour + "_1.html";
                        text = GetLogFilePathByLimitSize(basePath, limitSizeInByte);
                    }
                    else
                    {
                        string basePath2 = Path.GetDirectoryName(savePath) + "\\" + specailName + "_" + logType.ToString() + "_" + DateTime.Now.Hour + "_1.html";
                        text = GetLogFilePathByLimitSize(basePath2, limitSizeInByte);
                    }

                    if (File.Exists(text))
                    {
                        string text2 = "";
                        using (StreamReader streamReader = new StreamReader(text))
                        {
                            text2 = streamReader.ReadToEnd();
                            text2 = text2.Replace("<a href=\"#{next-id}\" class=\"move\">next</a>", "<a href=\"#" + logTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\" class = \"btn\">Next " + logTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "</a>");
                            streamReader.Dispose();
                        }

                        string newValue = GetLogContent(logType, message, logTime).TrimEnd();
                        text2 = text2.Replace("<p style=\"display: none;\">{log_content}</p>", newValue);
                        using (StreamWriter streamWriter = new StreamWriter(text, append: false))
                        {
                            try
                            {
                                streamWriter.WriteLine(text2);
                                streamWriter.Dispose();
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                    string text3 = InitContent();
                    string logContent = GetLogContent(logType, message, logTime);
                    text3 = text3.Replace("<p style=\"display: none;\">{log_content}</p>", logContent);
                    using (StreamWriter streamWriter2 = new StreamWriter(text, append: false))
                    {
                        try
                        {
                            streamWriter2.WriteLine(text3);
                            streamWriter2.Dispose();
                        }
                        catch
                        {
                        }
                    }
                }
                finally
                {
                    GC.Collect();
                }
            }
        }

        public static string GetLogFilePathByLimitSize(string basePath, int limitSizeInByte)
        {
            if (File.Exists(basePath))
            {
                if (GetFileSize(basePath) > limitSizeInByte)
                {
                    string[] array = basePath.Split('_');
                    int num = int.Parse(array[array.Length - 1].Split('.')[0]);
                    array[array.Length - 1] = num + 1 + ".html";
                    string basePath2 = string.Join("_", array);
                    return GetLogFilePathByLimitSize(basePath2, limitSizeInByte);
                }

                return basePath;
            }

            return basePath;
        }

        private static long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    return fileInfo.Length;
                }

                return -1L;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1L;
            }
        }

        public static string GetSavePath(EM_LogType logType, string startUpPath)
        {
            string text = startUpPath + $"\\logs\\{DateTime.Now.Year}\\{DateTime.Now.Month}\\{DateTime.Now.Day}\\";
            return text + "\\";
        }

        public static string InitContent()
        {
            try
            {
                string path = Path.Combine(SaveLogFolder + "/LogFormat/log.html");
                using (StreamReader streamReader = new StreamReader(path))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetLogItemContent()
        {
            try
            {
                string path = Path.Combine(SaveLogFolder + "/LogFormat/log_item.html");
                using (StreamReader streamReader = new StreamReader(path))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetLogContent(EM_LogType logType, string message, DateTime logTime)
        {
            string displayColor = GetDisplayColor(logType);
            FunctionExcecute functionExcecute = GetFunctionExcecute();
            string logItemContent = GetLogItemContent();
            logItemContent = logItemContent.Replace("{current-time}", logTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            string empty = string.Empty;
            empty = (logType.ToString().ToLower().Contains("infor") ? "info" : ((!logType.ToString().ToLower().Contains("error")) ? "warn" : "error"));
            logItemContent = logItemContent.Replace("{log-type}", empty);
            logItemContent = logItemContent.Replace("{log-name}", logType.ToString());
            logItemContent = logItemContent.Replace("{function-name}", functionExcecute.FunctionName);
            logItemContent = logItemContent.Replace("{file-name}", functionExcecute.FileName);
            logItemContent = logItemContent.Replace("{line-number}", functionExcecute.LineNumber.ToString());
            return logItemContent.Replace("{message}", message);
        }

        private static string GetDisplayColor(EM_LogType logType)
        {
            string text = "green";
            if (logType.ToString().ToLower().Contains("error"))
            {
                return "red";
            }

            if (logType.ToString().ToLower().Contains("warn"))
            {
                return "yellow";
            }

            return "green";
        }

        public static void Logger_SystemError(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.SYSTEM_ERROR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_SystemInfor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.SYSTEM_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_SystemWarn(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.SYSTEM_WARNING, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_BlackListInfor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.BLACK_LIST_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_API_Error(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.API_ERROR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_API_Infor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.API_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_API_Warning(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.API_WARNING, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_CONTROLLER_Error(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CONTROLLER_ERROR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_CONTROLLER_Infor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CONTROLLER_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_CONTROLLER_Warning(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CONTROLLER_WARNING, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_Camera_Error(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CAMERA_ERROR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_Camera_Infor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CAMERA_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_Camera_Warning(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.CAMERA_WARNING, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_LPR_Error(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.LPR_ERROR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_LPR_Infor(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.LPR_INFOR, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_LPR_Warning(string errorMessage, string startUpPath, object obj = null)
        {
            Task.Run(delegate
            {
                Log(EM_LogType.LPR_WARNING, errorMessage, startUpPath, DateTime.Now, obj);
            });
        }

        public static void Logger_IPC_Error(string errorMessage, string startUpPath, int limitLength)
        {
            string logContent = GetLogContent(EM_LogType.IPC_INFOR, errorMessage, DateTime.Now);
            string text = startUpPath + $"\\logs\\{DateTime.Now.Month}\\{DateTime.Now.Day}\\";
            text = text + EM_LogType.IPC_INFOR.ToString() + "\\";
            lock (lockObject)
            {
                try
                {
                    Directory.CreateDirectory(text);
                    int num = Directory.GetFiles(text, "*", SearchOption.AllDirectories).Length;
                    if (num == 0)
                    {
                        num = 1;
                    }

                    string path = Path.GetDirectoryName(text) + "\\" + EM_LogType.IPC_ERROR.ToString() + num + ".html";
                    if (!File.Exists(path))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(path, append: true))
                        {
                            try
                            {
                                streamWriter.WriteLine(logContent);
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                    string text2 = File.ReadAllText(path);
                    if (text2.Length < limitLength)
                    {
                        return;
                    }

                    path = Path.GetDirectoryName(text) + "\\" + EM_LogType.IPC_ERROR.ToString() + (num + 1) + ".html";
                    using (StreamWriter streamWriter2 = new StreamWriter(path, append: true))
                    {
                        try
                        {
                            streamWriter2.WriteLine(logContent);
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    GC.Collect();
                }
            }
        }

        public static void Logger_IPC_Infor(string message, string startUpPath, int limitLength)
        {
            string logContent = GetLogContent(EM_LogType.IPC_INFOR, message, DateTime.Now);
            string text = startUpPath + $"\\logs\\{DateTime.Now.Month}\\{DateTime.Now.Day}\\";
            text = text + EM_LogType.IPC_INFOR.ToString() + "\\";
            lock (lockObject)
            {
                try
                {
                    Directory.CreateDirectory(text);
                    int num = Directory.GetFiles(text, "*", SearchOption.AllDirectories).Length;
                    if (num == 0)
                    {
                        num = 1;
                    }

                    string path = Path.GetDirectoryName(text) + "\\" + EM_LogType.IPC_INFOR.ToString() + num + ".html";
                    if (!File.Exists(path))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(path, append: true))
                        {
                            try
                            {
                                streamWriter.WriteLine(logContent);
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                    }

                    string text2 = File.ReadAllText(path);
                    if (text2.Length >= limitLength)
                    {
                        path = Path.GetDirectoryName(text) + "\\" + EM_LogType.IPC_INFOR.ToString() + (num + 1) + ".html";
                        using (StreamWriter streamWriter2 = new StreamWriter(path, append: true))
                        {
                            try
                            {
                                streamWriter2.WriteLine(logContent);
                                return;
                            }
                            catch
                            {
                                return;
                            }
                        }
                            
                    }

                    using (StreamWriter streamWriter3 = new StreamWriter(path, append: true))
                    {
                        try
                        {
                            streamWriter3.WriteLine(logContent);
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    GC.Collect();
                }
            }
        }

        public static void Logger_IPC_Warning(string errorMessage, string startUpPath, int limitLength)
        {
            Log(EM_LogType.IPC_WARNING, errorMessage, startUpPath, DateTime.Now);
        }

        public static void Logger_Application_Progress(string message, object obj = null)
        {
            if (obj != null)
            {
                Log(EM_LogType.Application_Progress, message + " : " + TextFormatingTool.BeautyJson(obj), SaveLogFolder, DateTime.Now);
            }
            else
            {
                Log(EM_LogType.Application_Progress, message, SaveLogFolder, DateTime.Now);
            }
        }

        public static FunctionExcecute GetFunctionExcecute()
        {
            StackTrace stackTrace = new StackTrace();
            int skipFrames = 4;
            StackFrame stackFrame = new StackFrame(skipFrames, fNeedFileInfo: true);
            MethodBase method = stackFrame.GetMethod();
            return new FunctionExcecute
            {
                FunctionName = stackFrame.GetMethod().Name,
                FileName = stackFrame.GetFileName(),
                LineNumber = stackFrame.GetFileLineNumber()
            };
        }

        public static void SaveErrorLog(string errorTittle, Exception ex = null)
        {
            if (ex == null)
            {
                Logger_SystemError(errorTittle, SaveLogFolder);
                Logger_Application_Progress(errorTittle);
            }
            else
            {
                Logger_SystemError(errorTittle, SaveLogFolder, ex);
                Logger_Application_Progress(errorTittle, ex);
            }
        }
    }
}
