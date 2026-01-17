using Kztek.Tool;

namespace TestCash
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            SystemUtils.logger = LoggerFactory.CreateLoggerService(Kztek.Object.EmLogServiceType.OFFLINE_DB, "");
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load App Config"));

            Application.Run(new frmTestCash());
        }
    }
}