using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.Forms;
using Kztek.Control8.UserControls;
using System.Globalization;

namespace Kztek.Control8
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
            CultureInfo culture = new("lo-LA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            KZUIStyles.BuiltInResources.TryAdd(new vi_VN_KZResources().CultureInfo.LCID, new vi_VN_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new en_US_KZResources().CultureInfo.LCID, new en_US_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new lo_LA_KZResources().CultureInfo.LCID, new lo_LA_KZResources());
            KZUIStyles.CultureInfo = culture;
        }
    }
}