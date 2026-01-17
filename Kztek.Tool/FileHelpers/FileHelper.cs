using System;
using System.IO;

namespace Kztek.Tool
{
    public static class FileHelper
    {
        public static long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    return fileInfo.Length;
                }
            }
            finally
            {
                GC.Collect();
            }
            return -1;
        }
    }
}
