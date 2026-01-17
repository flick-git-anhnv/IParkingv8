using Kztek.Object;

namespace Kztek.Tool
{
    public class LoggerFactory
    {
        public static ILogger CreateLoggerService(EmLogServiceType logServiceType, string saveLogFolder)
        {
            switch (logServiceType)
            {
                case EmLogServiceType.OFFLINE_FILE:
                    return new LogToFile(saveLogFolder);
                case EmLogServiceType.OFFLINE_DB:
                    return new LogToSQLite(saveLogFolder);
                default:
                    return new LogToSQLite(saveLogFolder);
            }
        }
    }
}
