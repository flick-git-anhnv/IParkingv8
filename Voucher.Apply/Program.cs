using iParkingv8.Ultility;
using Kztek.Tool;
using Kztek.Voucher.Apply;

namespace Voucher.Apply
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

            IparkingingPathManagement.baseBath = Application.StartupPath;
            SystemUtils.logger = LoggerFactory.CreateLoggerService(Kztek.Object.EmLogServiceType.OFFLINE_DB, IparkingingPathManagement.baseBath);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load App Config"));

            Application.Run(new Form1());
        }
    }
}