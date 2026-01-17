using Kztek.Tool;

namespace KztekLprDetectionTest
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
            SystemUtils.logger = LoggerFactory.CreateLoggerService(Kztek.Object.EmLogServiceType.OFFLINE_DB, Application.StartupPath);

            Application.Run(new Form1());
        }
    }
}