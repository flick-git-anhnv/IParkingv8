using iParkingv5.Controller;

namespace v6_window
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
            var a = CardFactory.StandardlizedCardNumber("C3A4D6", new Kztek.Object.CardFormatConfig()
            {
                OutputFormat = Kztek.Object.CardFormat.EmCardFormat.PROXI_MA_SAU,
            });
        }
    }
}