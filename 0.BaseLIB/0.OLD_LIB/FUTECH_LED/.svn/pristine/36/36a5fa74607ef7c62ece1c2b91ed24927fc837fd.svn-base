using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.Tools
{
    public class ConfigFileIO
    {
        public static List<T> ReadConfigFile<T>(string filepath) 
        {
            if (File.Exists(filepath))
            {
                using (var stream = new StreamReader(filepath))
                {
                    var data = stream.ReadToEnd();
                    if (!String.IsNullOrEmpty(data))
                    {
                        try
                        {
                            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(data);
                            return config;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            return null;
        } 
        
        public static bool SaveConfig<T>(List<T> obj, string filepath) 
        {
            try
            {
                if (!File.Exists(filepath))
                {
                    var filestream = File.Create(filepath);
                    filestream.Close();
                    filestream.Dispose();
                }
                using (var stream = new StreamWriter(filepath))
                {
                    var data = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                    stream.Write(data);
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
