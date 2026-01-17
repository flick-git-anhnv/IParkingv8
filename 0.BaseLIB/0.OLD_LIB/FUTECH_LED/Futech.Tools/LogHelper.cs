using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Futech.Tools.TextFormatingTools;

namespace Futech.Tools
{
    public class LogHelper
    {
        public class FunctionExcecute
        {
            public string FunctionName { get; set; } = string.Empty;

            public string FileName { get; set; } = string.Empty;

            public int LineNumber { get; set; } = 0;
        }
        public enum EmLogType
        {
            INFOR,
            WARN,
            ERROR,
        }

        public enum EmObjectLogType
        {
            System,
            SQL,

            Form,
            Control,

            Api,

            Controller,
            Camera,
            LED,

            UserProgress
        }

        #region Properties
        private static readonly object lockObject = new object();
        public static string SaveLogFolder = "";
        #endregion End Properties

        #region: Public Function

        public static void Log(EmLogType logType, EmObjectLogType objectLogType, string action = "",
                               string actionDetail = "", object description = null,
                               object obj = null, string specailName = "",
                               [CallerMemberName] string callerName = "",
                               [CallerLineNumber] int lineNumber = 0,
                               [CallerFilePath] string filePath = "")
        {

            FunctionExcecute function = new FunctionExcecute();
            function.FunctionName = callerName;
            function.LineNumber = lineNumber;
            function.FileName = filePath;

            if (string.IsNullOrEmpty(action))
            {
                action = callerName;
            }

            if (string.IsNullOrEmpty(actionDetail))
            {
                actionDetail = action;
            }

            Task.Run(() =>
            {
                DateTime logTime = DateTime.Now;
                string str_mo_ta = "";
                if (description != null)
                {
                    if (description is string)
                    {
                        str_mo_ta = description as string;
                    }
                    else
                    {
                        str_mo_ta = TextFormatingTool.BeautyJson(description);
                    }
                }
                string message = GetLogMessage(objectLogType, action, actionDetail, logTime, function, str_mo_ta);
                Log(logType, objectLogType, message, logTime, obj, specailName: specailName);
            });
        }
        #endregion:End Public Function

        #region: Private Function
        private static void Log(EmLogType logType, EmObjectLogType objectLogType, string message, DateTime logTime, object obj = null, int limitSize = 1000 * 1024, string specailName = "")
        {
            lock (lockObject)
            {
                if (string.IsNullOrEmpty(SaveLogFolder))
                {
                    SaveLogFolder = ".";
                }

                try
                {
                    message = message.Replace("Ex_Message", TextFormatingTool.BeautyJson(obj));

                    string InitPath = GetSavePath();
                    Directory.CreateDirectory(InitPath);

                    string pathFile = "";
                    string extend = string.IsNullOrEmpty(specailName) ? $@"\{objectLogType}_{logType}_{DateTime.Now.Hour}_1.html" :
                                                                        $@"\{specailName}_{objectLogType}_{logType}_{DateTime.Now.Hour}_1.html";

                    string basePath = Path.GetDirectoryName(InitPath) + extend;
                    pathFile = GetLogFilePathByLimitSize(basePath, limitSize);

                    string writeLogContent = string.Empty;
                    if (File.Exists(pathFile))
                    {
                        string oldContent = "";
                        using (StreamReader reader = new StreamReader(pathFile))
                        {
                            oldContent = reader.ReadToEnd();
                            oldContent = oldContent.Replace("<a href=\"#{next-id}\" class=\"move\">next</a>",
                                                          $@"<a href=""#{logTime:yyyy-MM-dd HH:mm:ss.fff}"" class = ""btn"">Next {logTime:yyyy-MM-dd HH:mm:ss.fff}</a>");
                            reader.Dispose();
                        }
                        string logContent = GetLogContent(message, logTime).TrimEnd(); ;
                        writeLogContent = oldContent.Replace("<p style=\"display: none;\">{log_content}</p>", logContent);
                    }
                    else
                    {
                        string initContent = InitContent();
                        string logContent = GetLogContent(message, logTime);
                        writeLogContent = initContent.Replace("<p style=\"display: none;\">{log_content}</p>", logContent);
                    }

                    using (StreamWriter writer = new StreamWriter(pathFile, false))
                    {
                        try
                        {
                            writer.WriteLine(writeLogContent);
                            writer.Dispose();
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
                    var temp = basePath.Split('_');
                    int currentIndex = int.Parse(temp[temp.Length - 1].Split('.')[0]);
                    int nextIndex = currentIndex + 1;
                    temp[temp.Length - 1] = nextIndex + ".html";
                    string updatePath = string.Join("_", temp);
                    return GetLogFilePathByLimitSize(updatePath, limitSizeInByte);
                }
                else
                {
                    return basePath;
                }
            }
            else
            {
                return basePath;
            }
        }
        static long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (fileInfo.Exists)
                {
                    return fileInfo.Length;
                }
                else
                {
                    // File not found
                    return -1;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file access
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }
        private static string GetSavePath()
        {
            string initPath = SaveLogFolder + $@"\logs\{DateTime.Now.Year}\{DateTime.Now.Month}\{DateTime.Now.Day}\";
            return initPath + @"\";
        }
        private static string InitContent()
        {
            try
            {
                string path = Path.Combine(SaveLogFolder + "/LogFormat/log.html");
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                GC.Collect();
            }
        }
        private static string GetLogItemContent()
        {
            try
            {
                string path = Path.Combine(SaveLogFolder + "/LogFormat/log_item.html");
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                GC.Collect();
            }
        }
        private static string GetLogContent(string logMessage, DateTime logTime)
        {
            string content = GetLogItemContent();
            content = content.Replace("{current-time}", logTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            content = content.Replace("{message}", logMessage);
            return content;
        }
        private static string GetLogMessage(EmObjectLogType objectLogType, string action,
                                            string actionDetail, DateTime logTime,
                                            FunctionExcecute functionExcecute, string description = "")
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($@"<table >
                        <tr>
                            <td><b>Thời Gian: </b></td>
                            <td style=""width: 100%;"">{logTime:dd/MM/yyyy HH:mm:ss}</td>
                        </tr>
                        <tr>
                          <td><b>Đối tượng: </b></td>
                          <td>{objectLogType}</td>
                        </tr>
                        <tr>
                            <td><b>Hành động: </b></td>
                            <td>{action}</td>
                        </tr>
                        <tr>
                            <td><b>Nội dung hành động: </b></td>
                            <td><p style=""color: red;"">{actionDetail}</p></td>
                        </tr>");
            if (!string.IsNullOrEmpty(description))
            {
                stringBuilder.AppendLine($@"<tr>
                            <td><b>Thông tin thêm: </b></td>
                            <td>{description}</td>
                        </tr>");
            }
            if (!string.IsNullOrEmpty(functionExcecute?.FunctionName))
            {
                stringBuilder.AppendLine($@" <tr>
                            <td><b>Function Name: </b></td>
                            <td>{functionExcecute?.FunctionName}</td>
                        </tr>");
            }
            if (!string.IsNullOrEmpty(functionExcecute?.FileName))
            {
                stringBuilder.AppendLine($@" <tr>
                            <td><b>File Name: </b></td>
                            <td>{functionExcecute?.FileName}</td>
                        </tr>");
            }
            if (!string.IsNullOrEmpty(functionExcecute?.LineNumber.ToString()))
            {
                stringBuilder.AppendLine($@"<tr>
                            <td><b>Line Number: </b></td>
                            <td>{functionExcecute?.LineNumber}</td>
                        </tr>");
            }
            stringBuilder.AppendLine($@"<tr>
                            <td><b>Object: </b></td>
                            <td>Ex_Message</td>
                        </tr>
                      </table>");
            return stringBuilder.ToString().TrimEnd();
        }
        #endregion: End Private Function
    }
}