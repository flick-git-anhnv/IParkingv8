using iParkingv8.Auth;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Implementation.v8;
using Kztek.Tool;
using System.Diagnostics;
using System.Globalization;

namespace IParkingv8.RegisterCard
{
    internal static class Program
    {

        private const string appName = "IP_DA_V5_REGISTER_CARD";

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
            //CultureInfo culture = new("en-US");
            //CultureInfo culture = new("lo-LA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            KZUIStyles.BuiltInResources.TryAdd(new vi_VN_KZResources().CultureInfo.LCID, new vi_VN_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new en_US_KZResources().CultureInfo.LCID, new en_US_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new lo_LA_KZResources().CultureInfo.LCID, new lo_LA_KZResources());
            KZUIStyles.CultureInfo = culture;

        //KzDictionary.LoadVi([]);
        //KzDictionary.LoadEn([]);
        //Application.Run(new FrmTestCam());
        //return;
        StartApp:
            {

                LoadConfigs();
                using (Mutex mutex = new(true, appName, out bool ownmutex))
                {
                    if (ownmutex)
                    {
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Application Start"));

                        bool isValidConfigs = false;
                        isValidConfigs = AppData.ServerConfig != null || AppData.AppConfig != null || AppData.LprConfig != null;
                        if (!isValidConfigs)
                        {
                            MessageBox.Show("Không tìm thấy cấu hình server hoặc cấu hình không hợp lệ!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Exit();
                            Environment.Exit(0);
                            return;
                        }


                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("OPEN LOADING SCREEN"));
                        AppData.ApiServer = new ApiServerv8(AppData.ServerConfig!);
                        Application.Run(new FrmLogin(AppData.ApiServer, OpenLoadingPage, false, AppData.ServerConfig!.Username, AppData.ServerConfig.Password));
                    }
                    else
                    {

                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Duplicate App Running"));
                        Process currentProcess = Process.GetCurrentProcess();
                        foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
                        {
                            try
                            {
                                if (process.Id != currentProcess.Id &&
                                    process.MainModule?.FileName == currentProcess.MainModule?.FileName)
                                {
                                    process.Kill();
                                    process.WaitForExit();
                                    goto StartApp;
                                }
                            }
                            catch (Exception ex)
                            {
                                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Duplicate App Running", ex));
                                goto StartApp;
                            }
                        }
                    }
                }
            }
        }

        #region Load Config
        private static void LoadConfigs()
        {
            LoadAppConfig();
            LoadServerConfig();
            LoadOEMConfig();
            LoadThirdPartyConfig();
        }

        private static void LoadServerConfig()
        {
            try
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Serrver Config"));
                AppData.ServerConfig = NewtonSoftHelper<iParkingv8.Object.Objects.Systems.ServerConfig>.DeserializeObjectFromPath(IparkingingPathManagement.serverConfigPath);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Serrver Config", ex));
                MessageBox.Show("LoadServer: " + ex.Message + "\r\n" + ex.InnerException?.Message);
            }
        }
        private static void LoadAppConfig()
        {
            AppData.AppConfig = NewtonSoftHelper<AppOption>.DeserializeObjectFromPath(IparkingingPathManagement.appOptionConfigPath) ?? new AppOption();
            SystemUtils.logger = LoggerFactory.CreateLoggerService(AppData.AppConfig.LogServiceType, IparkingingPathManagement.baseBath);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load App Config"));
        }
        private static void LoadOEMConfig()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load OEM Config"));
            AppData.OEMConfig = NewtonSoftHelper<OEMConfig>.DeserializeObjectFromPath(IparkingingPathManagement.oemConfigPath)
                                ?? new OEMConfig();
            if (!File.Exists(AppData.OEMConfig.LogoPath))
            {
                AppData.OEMConfig.LogoPath = Path.Combine(Application.StartupPath, "Resources/defaultImage.png");
            }
            AppData.DefaultImageBase64 = ImageHelper.FileToBase64(AppData.OEMConfig.LogoPath);
        }
        private static void LoadThirdPartyConfig()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Third Party Config"));
            AppData.ThirdPartyConfig = NewtonSoftHelper<ThirdPartyConfig>.DeserializeObjectFromPath(IparkingingPathManagement.thirtPartyConfigPath)
                                       ?? new ThirdPartyConfig();
        }
        #endregion

        private static void OpenLoadingPage()
        {
            Form1 frm = new();
            frm.Show();
        }
    }
}