using iParking.ConfigurationManager.Forms;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using System.Globalization;

namespace iParking.ConfigurationManager
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
            CultureInfo culture = new("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            KZUIStyles.BuiltInResources.TryAdd(new vi_VN_KZResources().CultureInfo.LCID, new vi_VN_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new en_US_KZResources().CultureInfo.LCID, new en_US_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new lo_LA_KZResources().CultureInfo.LCID, new lo_LA_KZResources());
            KZUIStyles.CultureInfo = culture;
            Application.Run(new FrmConnectionConfig());
        }
    }
}