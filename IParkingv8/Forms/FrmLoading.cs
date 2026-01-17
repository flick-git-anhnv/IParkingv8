using iParkingv5.Controller;
using iParkingv5.LprDetecter.LprDetecters;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility.Style;
using IParkingv8.Printer;
using Kztek.Tool;
using static iParkingv5.Controller.ControllerFactory;

namespace IParkingv8.Forms
{
    public partial class FrmLoading : Form
    {
        #region Properties
        private readonly Dictionary<string, Func<Task<bool>>> loadingWorks = [];
        private List<string> displayMessages = [];
        private bool isWaiting = false;
        private int currentDisplayIndex = 0;
        #endregion End Properties

        #region Forms
        public FrmLoading()
        {
            InitializeComponent();
            this.BackgroundImage.Save("abc.jpeg");
            this.Text = KZUIStyles.CurrentResources.FrmLoading;

            loadingWorks.Add(KZUIStyles.CurrentResources.LoadDeviceConfig, GetDeviceConfig);
            loadingWorks.Add(KZUIStyles.CurrentResources.ConnectToController, ConnectToController);
            loadingWorks.Add(KZUIStyles.CurrentResources.InitLprEngine, CreateLpr);
            loadingWorks.Add(KZUIStyles.CurrentResources.LoadAccessKeyCollection, LoadAccessKeyCollection);

            this.Shown += FrmLoading_Shown;
            this.FormClosing += FrmLoading_FormClosing;
        }
        private void FrmLoading_Shown(object? sender, EventArgs e)
        {
            timerLoading.Enabled = true;
        }
        private void FrmLoading_FormClosing(object? sender, FormClosingEventArgs e)
        {
            this.FormClosing -= FrmLoading_FormClosing;
            timerDisplayLoadingMessage.Enabled = false;
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion End Forms

        #region Private Function
        public async Task<bool> GetDeviceConfig()
        {
            lblMessage.Text = KZUIStyles.CurrentResources.LoadDeviceConfig;
            lblMessage.Refresh();
            StartTimerDisplayLoadingMessage();
            var deviceResponse = await AppData.ApiServer.DeviceService!.GetDeviceDataAsync();
            if (deviceResponse == null)
            {
                StopTimerDisplayLoadingMessage();
                lblMessage.Text = KZUIStyles.CurrentResources.InvalidDeviceConfig;
                lblMessage.Refresh();
                await Task.Delay(TimeSpan.FromSeconds(1));
                await GetDeviceConfig();
            }
            else
            {
                List<string> validIps = NetWorkTools.GetLocalIPAddress();
                validIps.Add(Environment.MachineName.ToUpper());
                var computer = deviceResponse.GetComputerByIp(validIps);
                if (computer == null)
                {
                    StopTimerDisplayLoadingMessage();
                    lblMessage.Text = KZUIStyles.CurrentResources.LoadDeviceConfig;
                    lblMessage.Refresh();
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    await GetDeviceConfig();
                }
                else
                {
                    AppData.Computer = computer;
                    var lanes = deviceResponse.GetLanesByComputer(AppData.Computer);
                    var cameras = deviceResponse.GetCamerasByComputer(AppData.Computer);
                    var controllers = deviceResponse.GetBDKsByComputer(AppData.Computer);
                    var leds = deviceResponse.GetLedsByConputer(AppData.Computer);
                    var gates = deviceResponse.GetGatesByComputer(AppData.Computer);

                    AppData.Gate = gates.Count > 0 ? gates[0] : null;
                    AppData.Lanes = lanes;
                    AppData.Cameras = cameras;
                    AppData.Controllers = controllers;
                    AppData.Leds = leds;
                }
            }
            StopTimerDisplayLoadingMessage();
            return true;
        }
        public async Task<bool> ConnectToController()
        {
            lblMessage.Text = KZUIStyles.CurrentResources.ConnectToController;
            lblMessage.Refresh();
            StartTimerDisplayLoadingMessage();

            foreach (Bdk bdk in AppData.Controllers)
            {
                if (bdk.Type == (int)EmControllerType.Dahua)
                {
                    continue;
                }
                var controller =
                    ControllerFactory.CreateController(bdk.Id, bdk.Comport, bdk.Baudrate, bdk.Type, bdk.CommunicationType,
                                                       bdk.Name, AppData.AppConfig?.MinDelayCardTime ?? 5, ((EmPrintTemplate)AppData.AppConfig.PrintTemplate).ToString(), bdk.Code);
                if (controller != null)
                {
                    AppData.IControllers.Add(controller);
                    await controller.ConnectAsync();
                }
            }
            StopTimerDisplayLoadingMessage();
            return true;
        }
        private async Task<bool> LoadAccessKeyCollection()
        {
            currentDisplayIndex = 0;
            lblMessage.Text = KZUIStyles.CurrentResources.LoadAccessKeyCollection;
            lblMessage.Refresh();
            StartTimerDisplayLoadingMessage();
            AppData.DailyAccessKeyCollections = [];

            var identityGroups = (await AppData.ApiServer.DataService.AccessKeyCollection.GetAllAsync()).Item1;
            if (identityGroups != null)
            {
                foreach (var item in identityGroups)
                {
                    AppData.AccessKeyCollections.Add(item);
                    if (item.GetAccessKeyGroupType() != EmAccessKeyGroupType.DAILY)
                    {
                        continue;
                    }
                    AppData.DailyAccessKeyCollections.Add(item);
                }
            }

            StopTimerDisplayLoadingMessage();
            lblMessage!.Text = "";
            lblMessage.Refresh();
            return true;
        }
        private async Task<bool> CreateLpr()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            await Task.Delay(1);
            lblMessage.Text = KZUIStyles.CurrentResources.InitLprEngine;
            lblMessage.Refresh();
            StartTimerDisplayLoadingMessage();
            AppData.LprDetecter = new List<ILpr?>();
            AppData.LprConfig ??= new Kztek.Object.LprConfig()
            {
                LPRDetecterType = Kztek.Object.LprDetecter.EmLprDetecter.KztekLpr
            };
            var source = AppData.LprConfig.Url.Split(";");
            foreach (var item in source)
            {
                AppData.LprConfig.Url = item;
                var detecter = LprFactory.CreateLprDetecter(AppData.LprConfig, null);
                await detecter!.CreateLprAsync(AppData.LprConfig);
                AppData.LprDetecter.Add(detecter);
            }

            //if (AppData.LprConfig.LPRDetecterType != Kztek.Object.LprDetecter.EmLprDetecter.KztekLpr)
            //{
            //    var kztekLprConfig = new Kztek.Object.LprConfig()
            //    {
            //        LPRDetecterType = Kztek.Object.LprDetecter.EmLprDetecter.KztekLpr,
            //    };
            //    AppData.KztekDetecter = LprFactory.CreateLprDetecter(kztekLprConfig, null);
            //    await AppData.KztekDetecter!.CreateLprAsync(kztekLprConfig);
            //}

            StopTimerDisplayLoadingMessage();
            return AppData.LprDetecter != null;
        }
        private static List<string> CreateDisplayListMessage(string message)
        {
            List<string> result =
                    [
                        message,
                        message + " .",
                        message + " ..",
                        message + " ..."
                    ];
            return result;
        }
        #endregion

        #region Timer
        private async void TimerLoading_Tick(object sender, EventArgs e)
        {
            timerLoading.Enabled = false;
            foreach (KeyValuePair<string, Func<Task<bool>>> actions in loadingWorks)
            {
                bool isSuccess = await actions.Value();
                if (!isSuccess)
                {
                    string error = KZUIStyles.CurrentResources.SystemError;
                    string tryAgain = KZUIStyles.CurrentResources.TryAgain;
                    lblMessage!.Text = $"{actions.Key}: {error}. {tryAgain}";
                    lblMessage.ForeColor = Color.DarkRed;
                    lblMessage.Refresh();
                    return;
                }
            }
            timerDisplayLoadingMessage.Enabled = false;
            this.FormClosing -= FrmLoading_FormClosing;

            AppData.Printer = PrinterFactory.CreatePrinter((EmPrintTemplate)AppData.AppConfig.PrintTemplate);

            bool isNeedToChooseLane = false;
            if (AppData.Lanes.Count() > 1)
            {
                foreach (var item in AppData.Lanes)
                {
                    if (!string.IsNullOrEmpty(item.ReverseLaneId))
                    {
                        isNeedToChooseLane = true;
                        break;
                    }
                }
            }

            if (!isNeedToChooseLane)
            {
                FrmMain frm = new(AppData.Lanes)
                {
                    Owner = this.Owner
                };
                frm.Show();
            }
            else
            {
                FrmSelectLaneMode frm = new()
                {
                    Owner = this.Owner,
                };
                frm.Show();
            }

            this.Close();
            GC.Collect();
        }

        private void StartTimerDisplayLoadingMessage()
        {
            displayMessages = CreateDisplayListMessage(lblMessage.Text);
            this.currentDisplayIndex = 0;
            this.isWaiting = true;
            timerDisplayLoadingMessage.Enabled = true;
        }
        private void StopTimerDisplayLoadingMessage()
        {
            this.currentDisplayIndex = 0;
            this.isWaiting = false;
            timerDisplayLoadingMessage.Enabled = false;
        }
        private void TimerDisplayLoadingMessage_Tick(object sender, EventArgs e)
        {
            if (!isWaiting)
            {
                return;
            }

            lblMessage!.Text = displayMessages[currentDisplayIndex];
            lblMessage.Refresh();
            currentDisplayIndex++;
            if (currentDisplayIndex > displayMessages.Count - 1)
            {
                currentDisplayIndex = 0;
            }
        }
        #endregion
    }
}
