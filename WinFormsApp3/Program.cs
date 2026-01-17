using TestOpenVINO;

namespace WinFormsApp3
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
            var z = new KztekLpr();
            var image = File.ReadAllBytes(@"C:\Users\VietAnh\Desktop\2.jpeg");
            var b = z.Recognizer(image);

            Application.Run(new Form1());
        }
    }
}