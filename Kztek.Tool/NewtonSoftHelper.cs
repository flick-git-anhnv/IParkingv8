using Kztek.Object;
using Kztek.Object.Entity.Log;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Kztek.Tool
{
    public class NewtonSoftHelper<T> where T : class
    {
        public static T? GetBaseResponse(string jsonData,
                                        EmLogType logType = EmLogType.ERROR,
                                        EmObjectLogType doi_tuong_tac_dong = EmObjectLogType.Api,
                                        [CallerMemberName] string callerName = "",
                                        [CallerLineNumber] int lineNumber = 0,
                                        [CallerFilePath] string filePath = "")
        {
            try
            {
                T response = JsonConvert.DeserializeObject<T>(jsonData, new JsonSerializerSettings()
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                })!;
                if (response == null)
                {
                    TblSystemLog1.SaveLog(EmSystemAction.Application, EmSystemActionDetail.GET,
                                         $"{typeof(T).Name} - {jsonData}", null, callerName, lineNumber, filePath);
                }
                return response;
            }
            catch (Exception ex)
            {
                TblSystemLog1.SaveLog(EmSystemAction.Application, EmSystemActionDetail.GET,
                                     $"{typeof(T).Name} - {jsonData}", ex, callerName, lineNumber, filePath);
                return null;
            }

        }

        public static T? DeserializeObjectFromPath(string path)
        {
            if (!File.Exists(path)) return null;
            string configContent = File.ReadAllText(path, encoding: System.Text.Encoding.UTF8);
            if (string.IsNullOrEmpty(configContent)) return null;
            try
            {
                return JsonConvert.DeserializeObject<T>(configContent);
            }
            catch (Exception ex)
            {
                TblSystemLog1.SaveLog(EmSystemAction.Application, EmSystemActionDetail.GET,
                                     $"{typeof(T).Name} - Deseiralize", ex);
                return null;
            }
        }

        public static bool SaveConfig(T? obj, string savePath)
        {
            if (obj == null)
            {
                return false;
            }
            string directoryName = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            if (!File.Exists(savePath))
            {
                using (File.Create(savePath)) { }
            }

            using (StreamWriter writer = new StreamWriter(savePath, false, encoding: System.Text.Encoding.UTF8))
            {
                try
                {
                    string value = JsonConvert.SerializeObject(obj, Formatting.Indented);
                    writer.WriteLine(value);
                    writer.Dispose();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}
