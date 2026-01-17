using iParkingv8.Auth;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Implementation.v8;
using IParkingv8.Forms;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using Kztek.Tool;
using KztekKeyRegister;
using Sunny.UI;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;

namespace IParkingv8
{
    internal static class Program
    {
        private const string appName = "IP_DA_V5_WD";
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
            //new frmTest().ShowDialog();
            //return;
            //try
            //{

            //    new TestOpenVINO.Form1().ShowDialog();
            //    return;
            //}
            //catch (Exception exx)
            //{

            //    throw;
            //}

            //var z = new KztekLpr();
            //var image = Image.FromFile(@"C:\Users\VietAnh\Desktop\2.jpeg");
            //var b = z.Recognizer(ImageHelper.GetByteArrayFromImage(image, ImageFormat.Jpeg));

            CultureInfo culture = new("vi-VN");
            //CultureInfo culture = new("en-US");
            //CultureInfo culture = new("lo-LA");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            KZUIStyles.BuiltInResources.TryAdd(new vi_VN_KZResources().CultureInfo.LCID, new vi_VN_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new en_US_KZResources().CultureInfo.LCID, new en_US_KZResources());
            KZUIStyles.BuiltInResources.TryAdd(new lo_LA_KZResources().CultureInfo.LCID, new lo_LA_KZResources());
            KZUIStyles.CultureInfo = culture;

        StartApp:
            {
                LoadConfigs();
                if (AppData.AppConfig.IsStartWithWindow)
                {
                    ApplicationEx.AddToStartup();
                }
                else
                {
                    ApplicationEx.RemoveFromStartup();
                }
                Application.ThreadException += (s, e) =>
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Application.Thread Exception", e.Exception));
                };
                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("CurrentDomain.UnhandledException", e));
                };
                TaskScheduler.UnobservedTaskException += (s, e) =>
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("TaskScheduler.UnobservedTaskException", e.Exception));
                    e.SetObserved();
                };
                using (Mutex mutex = new(true, appName, out bool ownmutex))
                {
                    if (ownmutex)
                    {
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Application Start"));

                        bool isValidConfigs = false;
                        isValidConfigs = AppData.ServerConfig != null || AppData.AppConfig != null || AppData.LprConfig != null;
                        if (!isValidConfigs)
                        {
                            MessageBox.Show(KZUIStyles.CurrentResources.ServerConfigInvalid,
                                            KZUIStyles.CurrentResources.ErrorTitle,
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            Environment.Exit(0);
                            return;
                        }

                        bool isCheckKey = AppData.AppConfig.IsCheckKey;
                        if (isCheckKey)
                        {
                            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Check Key"));
                            var frmLicenseValidatorForm = new LicenseValidatorForm();
                            frmLicenseValidatorForm.Init(appName);
                            try
                            {
                                if (frmLicenseValidatorForm.LoadSavedLicense() == null)
                                {
                                    bool isOpenActiveForm = MessageBox.Show(KZUIStyles.CurrentResources.ActiveLicenseRequired,
                                                                            KZUIStyles.CurrentResources.InfoTitle,
                                                                            MessageBoxButtons.YesNo,
                                                                            MessageBoxIcon.Information) == DialogResult.Yes;

                                    if (isOpenActiveForm)
                                    {
                                        frmLicenseValidatorForm.ShowActivateForm();
                                    }
                                    else
                                    {
                                        Application.Exit();
                                        return;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                bool isOpenActiveForm = MessageBox.Show(KZUIStyles.CurrentResources.ActiveLicenseRequired,
                                                                        KZUIStyles.CurrentResources.InfoTitle,
                                                                        MessageBoxButtons.YesNo,
                                                                        MessageBoxIcon.Information) == DialogResult.Yes;

                                if (isOpenActiveForm)
                                {
                                    frmLicenseValidatorForm.ShowActivateForm();
                                    if (!frmLicenseValidatorForm.LicenseActivated)
                                    {
                                        MessageBox.Show(KZUIStyles.CurrentResources.ActiveLicenseError + " " + ex.Message,
                                                        KZUIStyles.CurrentResources.InfoTitle,
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                                        Application.Exit();
                                        return;
                                    }
                                }
                                else
                                {
                                    Application.Exit();
                                    return;
                                }
                            }
                        }

                        CheckForUpdate();

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
            LoadLprConfig();
            LoadOEMConfig();
            LoadThirdPartyConfig();
            LoadPaymentConfig();
        }

        private static void LoadServerConfig()
        {
            try
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Serrver Config"));
                AppData.ServerConfig = NewtonSoftHelper<iParkingv8.Object.Objects.Systems.ServerConfig>.DeserializeObjectFromPath(IparkingingPathManagement.serverConfigPath);
                AppData.RabbitMQConfig = NewtonSoftHelper<RabbitMQConfig>.DeserializeObjectFromPath(IparkingingPathManagement.rabbitmqConfigPath)
                                         ?? new RabbitMQConfig();
                AppData.MqttConfig = NewtonSoftHelper<MQTTConfig>.DeserializeObjectFromPath(IparkingingPathManagement.mqttConfigPath)
                                     ?? new MQTTConfig();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Serrver Config", ex));
                MessageBox.Show(KZUIStyles.CurrentResources.ServerConfigInvalid + ex.Message + "\r\n" + ex.InnerException?.Message,
                                KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void LoadAppConfig()
        {
            AppData.AppConfig = NewtonSoftHelper<AppOption>.DeserializeObjectFromPath(IparkingingPathManagement.appOptionConfigPath) ?? new AppOption();
            SystemUtils.logger = LoggerFactory.CreateLoggerService(AppData.AppConfig.LogServiceType, IparkingingPathManagement.baseBath);
            SystemUtils.logger.IsSaveLog = AppData.AppConfig.IsSaveLog;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load App Config"));
        }
        private static void LoadLprConfig()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Lpr Config"));
            AppData.LprConfig = NewtonSoftHelper<LprConfig>.DeserializeObjectFromPath(IparkingingPathManagement.lprConfigPath)
                                ?? new LprConfig();
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

            var image = ImageHelper.GetCloneImageFromPath(AppData.OEMConfig.LogoPath);
            if (image is null)
            {
                MessageBox.Show("Hình ảnh mặc định không hợp lệ");
                Application.Exit();
                Environment.Exit(0);
            }
            var resizeImg = ImageHelper.ResizeImage(image, 320, 320 * image.Height / image.Width);
            ImageHelper.ImageToBase64(resizeImg, out string base64, out int size, ImageFormat.Png);
            AppData.DefaultImageBase64 = base64;
        }
        private static void LoadThirdPartyConfig()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Third Party Config"));
            AppData.ThirdPartyConfig = NewtonSoftHelper<ThirdPartyConfig>.DeserializeObjectFromPath(IparkingingPathManagement.thirtPartyConfigPath)
                                       ?? new ThirdPartyConfig();
        }
        private static void LoadPaymentConfig()
        {
            AppData.PaymentKioskConfig = NewtonSoftHelper<PaymentKioskConfig>.DeserializeObjectFromPath(IparkingingPathManagement.paymentKioskConfigPath);
        }
        #endregion

        private static void CheckForUpdate()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Check Update"));
            if (string.IsNullOrEmpty(AppData.AppConfig.CheckForUpdatePath)) return;

            if (!Directory.Exists(AppData.AppConfig.CheckForUpdatePath)) return;

            try
            {
                bool isHavingUpdate = false;
                string[] updatefiles = Directory.GetFiles(AppData.AppConfig.CheckForUpdatePath, "*", SearchOption.AllDirectories);
                List<string> realUpdateFiles = [];
                foreach (string file in updatefiles)
                {
                    realUpdateFiles.Add(Path.GetFileName(file));
                }

                string[] currentVersionFiles = Directory.GetFiles(Application.StartupPath, "*", SearchOption.AllDirectories);
                List<string> realCurrentFiles = [];
                foreach (string file in currentVersionFiles)
                {
                    string relativePath = file[Application.StartupPath.Length..];
                    realCurrentFiles.Add(Path.GetFileName(file));
                }

                for (int i = 0; i < realUpdateFiles.Count; i++)
                {
                    string fileName = realUpdateFiles[i];
                    if (realCurrentFiles.Contains(fileName))
                    {
                        string currentFilePath = currentVersionFiles.Where(e => e.Contains(fileName)).FirstOrDefault() ?? "";
                        string updateFilePath = updatefiles[i];

                        string? currentFilePathVersion = null;
                        string? updateFilePathVersion = null;
                        try
                        {
                            FileVersionInfo currentFileVersionInfo = FileVersionInfo.GetVersionInfo(currentFilePath);
                            currentFilePathVersion = currentFileVersionInfo?.FileVersion;
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            FileVersionInfo updateFileVersionInfo = FileVersionInfo.GetVersionInfo(updateFilePath);
                            updateFilePathVersion = updateFileVersionInfo?.FileVersion;
                        }
                        catch (Exception)
                        {
                        }

                        if (currentFilePathVersion != updateFilePathVersion)
                        {
                            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(
                                                             $"Update {fileName} From {currentFilePathVersion} To {updateFilePathVersion}"));
                            isHavingUpdate = true;
                            string newFilePath = Path.Combine(Application.StartupPath, fileName + "_bak_" + currentFilePathVersion + "_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));
                            File.Move(currentFilePath, newFilePath);
                            File.Copy(updateFilePath, currentFilePath);
                            while (!File.Exists(currentFilePath))
                            {
                                Thread.Sleep(10);
                            }
                        }
                        else if (updateFilePathVersion == null && currentFilePathVersion == null)
                        {
                            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Copy New {fileName}"));

                            File.Delete(currentFilePath);
                            File.Copy(updateFilePath, currentFilePath);
                        }
                    }
                    //THÊM FILE CHƯA CÓ
                    else
                    {
                        isHavingUpdate = true;
                        string updateFilePath = updatefiles[i];

                        string destinationDirectory = Path.Combine(Application.StartupPath, Path.GetDirectoryName(fileName)!);
                        Directory.CreateDirectory(destinationDirectory);

                        File.Copy(updateFilePath, Path.Combine(Application.StartupPath, fileName));
                    }
                }

                if (isHavingUpdate)
                {
                    //Fix cung bo restart()
                    //Application.Restart();
                    Application.Exit();
                    Environment.Exit(0);
                    Application.DoEvents();
                    return;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Check Update", ex));
                MessageBox.Show(ex.Message);
            }
        }
        private static void OpenLoadingPage()
        {
            FrmLoading frm = new();
            frm.Show();
        }
    }
}