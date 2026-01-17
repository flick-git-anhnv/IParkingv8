using ILedv8.Forms;
using ILedv8.Objects;
using iParkingv8.Auth;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Ultility;
using IParkingv8.API.Implementation.v8;
using Kztek.Tool;
using KztekKeyRegister;
using System.Diagnostics;
using System.Globalization;

namespace ILedv8
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

            CultureInfo culture = new("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

        StartApp:
            {
                LoadConfigs();
                using (Mutex mutex = new(true, appName, out bool ownmutex))
                {
                    if (ownmutex)
                    {
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Application Start"));

                        bool isValidConfigs = false;
                        isValidConfigs = AppData.ServerConfig != null || AppData.AppConfig != null;
                        if (!isValidConfigs)
                        {
                            MessageBox.Show("Không tìm thấy cấu hình server hoặc cấu hình không hợp lệ!", "Thông báo",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                    bool isOpenActiveForm = MessageBox.Show("Ứng dụng chưa được kích hoạt, bạn có muốn kích hoạt phần mềm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;

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
                                bool isOpenActiveForm = MessageBox.Show("Ứng dụng chưa được kích hoạt, bạn có muốn kích hoạt phần mềm?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;

                                if (isOpenActiveForm)
                                {
                                    frmLicenseValidatorForm.ShowActivateForm();
                                    if (!frmLicenseValidatorForm.LicenseActivated)
                                    {
                                        MessageBox.Show("Kích hoạt không thành công " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadDelayConfig();
        }

        private static void LoadDelayConfig()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Load Delay Config"));
            AppData.delayConfig = NewtonSoftHelper<DelayConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledDelayConfig()) ?? new DelayConfig();

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

                        // Get the destination directory path
                        string destinationDirectory = Path.Combine(Application.StartupPath, Path.GetDirectoryName(fileName)!);
                        // Create the directory if it doesn't exist
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