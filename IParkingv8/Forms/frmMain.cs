using iParkingv5.Controller;
using iParkingv5.Controller.KztekDevices;
using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv5.Objects.Events;
using iParkingv8.Auth;
using iParkingv8.Object;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.CMS;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.CMS;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Faces;
using iParkingv8.Object.Objects.Helpers;
using iParkingv8.Object.Objects.Licenses;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;
using iParkingv8.Reporting;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using IParkingv8.Forms.DataForms;
using IParkingv8.Helpers;
using IParkingv8.LaneUIs.KioskIn;
using IParkingv8.LaneUIs.KioskOut;
using IParkingv8.Reporting;
using IParkingv8.UserControls;
using Kztek.Control8.Forms;
using Kztek.Object;
using Kztek.Tool;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IParkingv8.Forms
{
    public partial class FrmMain : Form
    {
        #region Properties
        private List<Lane> lanes = [];
        public List<ILane> ilanes = [];

        //-- TIMER
        System.Timers.Timer? timerClearLog;
        System.Timers.Timer? timerStartApp;
        System.Timers.Timer? timerHeartbeat;
        System.Timers.Timer? timerUpdateDeviceStatus;
        System.Timers.Timer? timerCheckLprServer;

        #region RabbitMQ
        public static IConnection? conn;
        public static IModel? channel;
        readonly List<string> monitoringTask = [];
        #endregion

        #region CMS
        ComputerMonitorMessage heartbeatMessage = new();
        string lastCMSMessage = "";
        #endregion

        #region Socket
        public static Socket? socket_listener;
        private CancellationTokenSource? ctsSocket;
        private bool isHaveRestartRequest = false;
        #endregion
        #endregion

        private HttpListener httpListenerServer = null;
        public static Dictionary<string, List<string>> cmds = new Dictionary<string, List<string>>();
        const string device_init_connection = "iclock/cdata";
        const string device_registry_connection = "iclock/registry";
        const string download_configuration = "iclock/push";

        #region Forms
        public FrmMain(IEnumerable<Lane> lanes)
        {
            InitializeComponent();

            var screenWidth = Screen.AllScreens[0].Bounds.Width;
            if (screenWidth <= 1366)
            {
                kzuI_UcAppMenu1.Height = 28;
                kzuI_UcEventRealtime1.Height = 28;
            }

            var temp = Screen.FromControl(this).Bounds;
            this.Size = new Size((int)System.Windows.SystemParameters.WorkArea.Width, (int)System.Windows.SystemParameters.WorkArea.Height);// Screen.FromControl(this).Bounds;
            this.Location = new Point(0, 0);
            this.KeyPreview = true;
            this.lanes = lanes.ToList();
            //AppData.LprDetecter!.onLprErrorEvent += LprDetecter_onLprErrorEvent;
            this.FormClosing += FrmMain_FormClosing;

            var projectName = StaticPool.configs.Where(e => e.Id == "company.name").FirstOrDefault();
            if (projectName != null)
            {
                StaticPool.LicenseExpire = GetLicenseExpireByName(projectName.Value);
            }

            this.Load += FrmMain_Load;
        }

        private void FrmMain_Load(object? sender, EventArgs e)
        {
            timerLoading.Enabled = true;
        }

        public class ZkCMD
        {
            public string DeviceId { get; set; } = string.Empty;
            public string CMD { get; set; } = string.Empty;
        }
        static string GetBodyValue(string body, string key)
        {
            if (string.IsNullOrEmpty(body)) return "";

            foreach (var part in body.Split(new[] { '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (part.StartsWith(key + "=", StringComparison.OrdinalIgnoreCase))
                    return part.Substring(key.Length + 1).Trim();
            }
            return "";
        }
        private async void OnRequest(IAsyncResult result)
        {
            var listener = (HttpListener)result.AsyncState;
            var context = listener.EndGetContext(result);

            var query = context.Request.QueryString;
            string? sn = query is null ? "" : query["SN"];
            sn = sn ?? "";
            DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn}");
            foreach (var item in AppData.IControllers)
            {
                if (item.ControllerInfo.Code == sn)
                {
                    item.ControllerInfo.IsConnect = true;
                }
            }
            if (!cmds.ContainsKey(sn))
            {
                cmds.Add(sn, []);
            }

            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/plain";

            using (var writer = new StreamWriter(context.Response.OutputStream))
            {
                if (context.Request.Url.AbsoluteUri.Contains("table"))
                {
                    if (context.Request.HasEntityBody)
                    {
                        using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                        {
                            string requestBody = reader.ReadToEnd();
                            var lines = requestBody.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var line in lines)
                            {
                                var cardNo = GetBodyValue(line, "cardno");
                                if (!string.IsNullOrEmpty(cardNo))
                                {
                                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess(line));
                                    cardNo = cardNo.ToUpper();
                                    string readerIndex = GetBodyValue(line, "inoutstatus");
                                    int.TryParse(readerIndex, out int readerIndexInt);
                                    DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - {line}");
                                    readerIndexInt += 1;

                                    string controllerId = AppData.Controllers.FirstOrDefault(e => e.Code == sn)?.Id ?? "";
                                    if (!string.IsNullOrEmpty(controllerId))
                                    {

                                        string baseCard = cardNo;
                                        bool isValidReader = false;
                                        foreach (ILane iLane in this.ilanes)
                                        {
                                            if (!iLane.Lane.IsContainReader(controllerId, readerIndexInt, out ControllerInLane? controller))
                                            {
                                                continue;
                                            }
                                            CardEventArgs e = new()
                                            {
                                                EventTime = DateTime.Now,
                                                DeviceId = controllerId,
                                                DeviceName = sn,
                                                Type = (int)EmAccessKeyType.CARD,
                                                DeviceCode = "",
                                                ReaderIndex = readerIndexInt,
                                            };
                                            isValidReader = true;
                                            e.PreferCard = baseCard;
                                            var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(iLane.Lane.Id, e.DeviceId, readerIndexInt);
                                            var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                                            new CardFormatConfig()
                                                            {
                                                                ReaderIndex = readerIndexInt,
                                                                OutputFormat = CardFormat.EmCardFormat.HEXA,
                                                                OutputOption = CardFormat.EmCardFormatOption.Min_8,
                                                            };
                                            e.PreferCard = CardFactory.StandardlizedCardNumber(e.PreferCard, config);
                                            e.AllCardFormats.Add(e.PreferCard);

                                            string Message = $"{DateTime.Now:HH:mm:ss} Lane: {iLane.Lane.Name} READER: {e.ReaderIndex} Button: {e.ButtonIndex}, CARD: {e.PreferCard} Controller: " + e.DeviceName;
                                            kzuI_UcEventRealtime1.ShowEvent(Message);
                                            kzuI_UcEventRealtime1.LastCardNumber = e.PreferCard;
                                            iLane.OnNewEvent(e);
                                        }
                                        if (!isValidReader)
                                        {
                                            string Message = KZUIStyles.CurrentResources.InvalidReaderConfig;
                                            kzuI_UcEventRealtime1.ShowEvent(Message);
                                        }
                                    }
                                }
                            }

                            string time = GetBodyValue(requestBody, "time");


                            writer.Write("OK");
                        }
                    }
                }
                else if (context.Request.Url.AbsoluteUri.Contains(device_init_connection))
                {
                    DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - INIT CONNECTION");
                    string message = $"registry=ok\r\nRegistryCode={sn}\r\nSessionId={sn}";
                    writer.Write(message);
                    await Task.Delay(10);
                }
                else if (context.Request.Url.AbsoluteUri.Contains(device_registry_connection))
                {
                    DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - DEVICE REGISTRY");
                    string message = $"RegistryCode={sn}";
                    writer.Write(message);
                    await Task.Delay(10);
                }
                else if (context.Request.Url.AbsoluteUri.Contains(download_configuration))
                {
                    DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - DOWNLOAD CONFIGURATION");
                    string message = "ServerVersion=1\r\nPushVersion=3.1.2";
                    writer.Write(message);
                    await Task.Delay(10);
                }
                else if (context.Request.Url.AbsoluteUri.Contains("iclock/getrequest"))
                {
                    string cmd = GetCMD(sn);
                    DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - GET REQUEST {cmd}");
                    writer.Write(cmd);
                }
                else
                {
                    if (context.Request.HasEntityBody)
                    {
                        using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                        {
                            string requestBody = reader.ReadToEnd();
                            DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - {requestBody}");
                        }
                    }
                    else
                    {
                        DisplayStatus($"{DateTime.Now:HH:mm:ss}- REC: {sn} - {context.Request.Url.AbsoluteUri}");
                    }
                    string message = "OK";
                    writer.Write(message);
                }
            }

            listener.BeginGetContext(OnRequest, listener);
        }
        private static string GetCMD(string sn)
        {
            lock (cmds)
            {
                if (!cmds.ContainsKey(sn) || (cmds.ContainsKey(sn) && cmds[sn].Count == 0))
                {
                    return "C:221:CONTROL DEVICE 01";
                }
                var cmd = cmds[sn][0];
                cmds[sn].RemoveAt(0);
                return cmd;
            }
        }
        public static void AddCMD(string sn, int lockIndex)
        {
            lock (cmds)
                if (!cmds.ContainsKey(sn))
                {
                    cmds.Add(sn, []);
                }
            cmds[sn].Add($"C:221:CONTROL DEVICE 010{lockIndex}010101");
        }
        private void LprDetecter_onLprErrorEvent(object sender)
        {
            //this.kzuI_UcEventRealtime1.UpdateLprType("Kztek-LPR");
            //AppData.LprDetecter = AppData.KztekDetecter;
            //if (timerCheckLprServer != null)
            //{
            //    timerCheckLprServer.Start();
            //}
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (ilanes == null || ilanes.Count == 0)
            {
                return false;
            }
            foreach (var item in this.ilanes)
            {
                item.OnKeyPress(keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            foreach (var item in ilanes)
            {
                if (item.IsBusy)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (e.CloseReason == CloseReason.UserClosing || (e.CloseReason == CloseReason.ApplicationExitCall && !this.isHaveRestartRequest))
            {
                bool isCloseApp = MessageBox.Show(KZUIStyles.CurrentResources.ProcessConfirmCloseApp, KZUIStyles.CurrentResources.InfoTitle,
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                             == DialogResult.Yes;
                if (!isCloseApp)
                {
                    e.Cancel = true;
                    return;
                }
            }

            this.FormClosing -= FrmMain_FormClosing;
            foreach (var item in ilanes)
            {
                var position = ucViewGrid1.table?.GetPositionFromControl((Control)item) ?? new TableLayoutPanelCellPosition(0, 0);
                var displayConfig = item.GetLaneDisplayConfig();
                displayConfig.DisplayIndex = position.Column;
                NewtonSoftHelper<LaneDisplayConfig>.SaveConfig(displayConfig, IparkingingPathManagement.appDisplayConfigPath(item.Lane.Id));
            }
            kzuI_UcAppMenu1.StopTimer();
            kzuI_UcEventRealtime1.StopTimer();
            StopTimer();
            if (isHaveRestartRequest)
            {
                return;
            }
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion

        #region Report
        private void KzuI_UcAppMenu1_AccessKey(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report Access Keys" });
            new FrmAccessKeys().ShowDialog();
        }
        private void KzuI_UcAppMenu1_Vehicle(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report Registered Vehicles" });
            new FrmVehicles().ShowDialog();
        }
        private void KzuI_UcAppMenu1_Customer(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report Customers" });
            new FrmCustomers().ShowDialog();
        }
        private void KzuI_UcAppMenu1_ReportRevenue(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report Revenue" });
            new FrmRevenueByIdentityGroup(AppData.ApiServer, AppData.Printer, AppData.AppConfig, AppData.AccessKeyCollections).ShowDialog();
        }
        private void KzuI_UcAppMenu1_ReportHandOver(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report Handover" });
            new frmShiftHandOver(AppData.ApiServer, AppData.Printer, AppData.AppConfig, AppData.AccessKeyCollections).ShowDialog();
            FrmLogin.updateLoginTime();
        }

        private void KzuI_UcAppMenu1_ReportOut(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report out" });
            new FrmReportInOut(AppData.ApiServer, ImageHelper.Base64ToImage(AppData.DefaultImageBase64),
                               AppData.Printer, AppData.AppConfig, AppData.Lanes, AppData.DailyAccessKeyCollections).Show(this);
        }
        private void KzuI_UcAppMenu1_ReportIn(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to report in" });
            new FrmReportIn(ImageHelper.Base64ToImage(AppData.DefaultImageBase64), AppData.ApiServer, AppData.Lanes).Show(this);
        }

        private void KzuI_UcAppMenu1_OnAlarm(object? sender, EventArgs e)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = "User click to alarm report" });
            new FrmReportAlarm(ImageHelper.Base64ToImage(AppData.DefaultImageBase64), AppData.ApiServer, AppData.Lanes).Show(this);
        }
        #endregion

        #region Private Funciton
        private void CreateUI()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Create UI"));
            DisplayStatus(KZUIStyles.CurrentResources.InitView);
            ucViewGrid1.UpdateRowSetting(1, this.lanes.Count());
            List<int> validPosition = [];
            for (int i = 0; i < this.lanes.Count(); i++)
            {
                validPosition.Add(i);
            }
            TableLayoutPanel table = ucViewGrid1.table!;

            typeof(Control).InvokeMember("DoubleBuffered",
                             System.Reflection.BindingFlags.SetProperty |
                             System.Reflection.BindingFlags.Instance |
                             System.Reflection.BindingFlags.NonPublic,
                             null, table, new object[] { true });

            table.Refresh();
            Dictionary<int, ILane> controls = [];

            foreach (Lane lane in lanes)
            {
                if (lane.Type == (int)EmLaneType.KIOSK_IN || lane.Type == (int)EmLaneType.KIOSK_OUT)
                {
                    kzuI_UcAppMenu1.Visible = false;
                    kzuI_UcEventRealtime1.Visible = false;
                }

                var config = NewtonSoftHelper<LaneDisplayConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appDisplayConfigPath(lane.Id)) ??
                                                                                           LaneDisplayConfig.CreateDefault(1920, lane.Id, validPosition[0]);
                config.DisplayIndex = Math.Min(config.DisplayIndex, this.lanes.Count() - 1);
                if (config.DisplayIndex < 0)
                {
                    config.DisplayIndex = 0;
                }
                if (controls.ContainsKey(config.DisplayIndex))
                {
                    config.DisplayIndex = validPosition[0];
                }
                var laneUI = LaneFactory.CreateLane(lane, lanes.Count(), config, channel, this.kzuI_UcEventRealtime1);
                if (laneUI != null)
                {
                    laneUI.OnChangeLaneEvent += LaneUI_OnChangeLaneEvent;
                    ilanes.Add(laneUI);

                    if ((EmLaneType)lane.Type == EmLaneType.LANE_IN)
                    {
                        table.ColumnStyles[config.DisplayIndex] = new ColumnStyle(SizeType.Percent, 40);
                    }
                    else
                    {
                        table.ColumnStyles[config.DisplayIndex] = new ColumnStyle(SizeType.Percent, 60);
                    }
                    controls.Add(config.DisplayIndex, laneUI);
                    validPosition.Remove(config.DisplayIndex);
                    table.Refresh();
                }
            }
            List<Lane> orderLanes = new List<Lane>();
            foreach (var item in controls.OrderBy(x => x.Key))
            {
                orderLanes.Add(item.Value.Lane);
                table.Controls.Add((Control)item.Value, item.Key, 0);
                ((Control)item.Value).Dock = DockStyle.Fill;
                table.Refresh();
                var directionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(item.Value.Lane.Id)) ?? new LaneDirectionConfig();
                var config = NewtonSoftHelper<LaneDisplayConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appDisplayConfigPath(item.Value.Lane.Id)) ??
                             LaneDisplayConfig.CreateDefault(1920, item.Value.Lane.Id, item.Key);

                item.Value.ChangeLaneDirectionConfig(directionConfig);
                item.Value.LoadViewSetting(config);
            }
            this.lanes = orderLanes;
            StartSocketServer();
            timerRestartSocket.Enabled = true;
        }
        private async Task StartControllers()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Start Controller"));
            foreach (var controller in AppData.IControllers)
            {
                //bool isConnectSuccess = await controller.ConnectAsync();
                controller.CardEvent += Controller_CardEvent;
                controller.ErrorEvent += Controller_ErrorEvent;
                controller.InputEvent += Controller_InputEvent;
                controller.CancelEvent += Controller_CancelEvent;
                controller.ConnectStatusChangeEvent += Controller_ConnectStatusChangeEvent;
                controller.DeviceInfoChangeEvent += Controller_DeviceInfoChangeEvent;

                if (controller is BaseDispenserDevice dispenser_controller)
                {
                    dispenser_controller.CardBeTaken += Dispenser_controller_CardBeTaken;
                }
                if (controller is ICardDispenserv2 cardDispenserv2)
                {
                    cardDispenserv2.OnCardInRFEvent += CardDispenserv2_OnCardInRFEvent;
                    cardDispenserv2.OnCardNotSupportEvent += CardDispenserv2_OnCardNotSupportEvent;
                }
                controller.PollingStart();
            }
        }

        private void CardDispenserv2_OnCardNotSupportEvent(object sender, CardNotSupportEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    ShowForm();
                    foreach (ILane iLane in this.ilanes)
                    {
                        if (iLane is not IKioskOutView kioskOut)
                        {
                            continue;
                        }

                        iLane.OnNewEvent(e);
                    }
                }
                catch (Exception ex)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("", ex));
                }
            }));

        }

        //--MQTT
        public async void ConnectToMQTT()
        {
            if (string.IsNullOrEmpty(AppData.MqttConfig?.Url))
            {
                return;
            }
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Connect To MQTT"));
            string broker = AppData.MqttConfig.Url;
            int port = 1883;
            string clientId = Guid.NewGuid().ToString();
            string username = AppData.MqttConfig.Username;
            string password = AppData.MqttConfig.Password;
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithCredentials(username, password)
                .WithClientId(clientId)
                .WithCleanSession()
                .Build();
            var connectResult = await mqttClient.ConnectAsync(options);
            if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
            {
                await mqttClient.SubscribeAsync(AppData.MqttConfig.Topic);
                mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;
            }
        }
        private Task MqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                string topic = e.ApplicationMessage.Topic;
                string payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
                try
                {
                    ShowForm();

                    SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.INFO, "MQTT Message", $"{topic} - {payload}"));

                    FaceMessage? faceMessage = JsonConvert.DeserializeObject<FaceMessage>(payload);
                    if (faceMessage == null)
                    {
                        return;
                    }

                    CardEventArgs ce = new()
                    {
                        EventTime = faceMessage.EventTime,
                        PreferCard = faceMessage.UserId,
                        DeviceId = faceMessage.DeviceId,
                        DeviceName = faceMessage.DeviceId,
                    };

                    foreach (var item in ilanes)
                    {
                        var laneCodes = item.Lane.Code.Split(";");
                        if (!laneCodes.Contains(ce.DeviceId))
                        {
                            continue;
                        }
                        foreach (var controlUnit in item.Lane.ControlUnits)
                        {
                            if (controlUnit.Barriers.Count == 0)
                            {
                                continue;
                            }

                            ce.AllCardFormats.Add(ce.PreferCard);
                            ce.DeviceId = controlUnit.Id;
                            DisplayStatus($"{DateTime.Now:HH:mm:ss} MQTT - CARD: {ce.PreferCard} Controller: " + ce.DeviceName);
                            item.OnNewEvent(ce);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.INFO, $"MQTT Message {topic} - {payload}", ex));
                }
            }));

            return Task.CompletedTask;
        }

        //--RABBITMQ
        private void ConnectToRabbitMQ()
        {
            if (string.IsNullOrEmpty(AppData.RabbitMQConfig?.RabbitMqUrl))
            {
                return;
            }
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Connect To Rabbit MQ"));
            try
            {
                ConnectionFactory factory = new()
                {
                    HostName = AppData.RabbitMQConfig.RabbitMqUrl,
                    Port = AppData.RabbitMQConfig.Port,
                    UserName = AppData.RabbitMQConfig.RabbitMqUsername,
                    Password = AppData.RabbitMQConfig.RabbitMqPassword
                };
                conn = factory.CreateConnection();
                channel = conn.CreateModel();

                CreateRequiredQueue();
                MonitorControllerEvent();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"RabbitMQ Connect Error", ex, EmSystemActionType.ERROR));
                MessageBox.Show(ex.Message, "Lỗi kết nối Rabbit MQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CreateRequiredQueue()
        {
            if (channel == null)
            {
                return;
            }
            //Register Exchange
            //channel.ExchangeDeclare(RabbitMQHelper.EXCHANGE_ACCESS_EVENT_NAME, ExchangeType.Topic, true, false, null);
            //channel.ExchangeDeclare(RabbitMQHelper.EXCHANGE_CMS_MONITOR_NAME, ExchangeType.Topic, true, false, null);
            //channel.ExchangeDeclare(RabbitMQHelper.EXCHANGE_CMS_OPERATOR_NAME, ExchangeType.Topic, true, false, null);
            //channel.ExchangeDeclare(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, ExchangeType.Direct, true, false, null);

            //Regsiter Queue
            RabbitMQHelper.queueMonitorDeviceBaseName = $"queue.monitor.{Environment.MachineName}.{AppData.Computer!.Id}";
            RabbitMQHelper.queueOperatorBaseName = $"queue.operator.{Environment.MachineName}.{AppData.Computer.Id}";
            RabbitMQHelper.controllerEventInitQueueName = $"queue.controller.event.{Environment.MachineName}.{AppData.Computer.Id}";
            RabbitMQHelper.queueControlCenterBaseName = $"queue.ControlCenter.{Environment.MachineName}.{AppData.Computer.Id}";
            RabbitMQHelper.queueMonitorPaymentQueueBaseName = $"queue.payment.monitor.{Environment.MachineName}.{AppData.Computer.Id}";

            channel.QueueDeclare(RabbitMQHelper.queueMonitorPaymentQueueBaseName, true, false, false, null);
            channel.QueueDeclare(RabbitMQHelper.controllerEventInitQueueName, true, false, false, null);
            channel.QueueDeclare(RabbitMQHelper.queueMonitorDeviceBaseName, true, false, false, null);
            channel.QueueDeclare(RabbitMQHelper.queueOperatorBaseName, true, false, false, null);
            channel.QueueDeclare(RabbitMQHelper.queueControlCenterBaseName, true, false, false, null);

            channel.QueueBind(RabbitMQHelper.controllerEventInitQueueName, RabbitMQHelper.EXCHANGE_ACCESS_EVENT_NAME, "all");
            channel.QueueBind(RabbitMQHelper.queueMonitorPaymentQueueBaseName, RabbitMQHelper.EXCHANGE_PAYMENT_NAME, "all");
            channel.QueueBind(RabbitMQHelper.queueMonitorDeviceBaseName, RabbitMQHelper.EXCHANGE_CMS_MONITOR_NAME, AppData.Computer.Id.ToLower());
            channel.QueueBind(RabbitMQHelper.queueOperatorBaseName, RabbitMQHelper.EXCHANGE_CMS_OPERATOR_NAME, AppData.Computer.Id.ToLower());
            channel.QueueBind(RabbitMQHelper.queueControlCenterBaseName, RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer.Id.ToLower());
        }
        public void MonitorControllerEvent()
        {
            var controllerEventConsumer = new EventingBasicConsumer(channel);
            var monitorConsumer = new EventingBasicConsumer(channel);
            var operatorConsumer = new EventingBasicConsumer(channel);
            var controlCenterConsumer = new EventingBasicConsumer(channel);
            var paymentConsumer = new EventingBasicConsumer(channel);

            monitoringTask.Add(channel!.BasicConsume(RabbitMQHelper.controllerEventInitQueueName, true, controllerEventConsumer));
            monitoringTask.Add(channel.BasicConsume(RabbitMQHelper.queueMonitorDeviceBaseName, true, monitorConsumer));
            monitoringTask.Add(channel.BasicConsume(RabbitMQHelper.queueOperatorBaseName, true, operatorConsumer));
            monitoringTask.Add(channel.BasicConsume(RabbitMQHelper.queueControlCenterBaseName, true, controlCenterConsumer));
            monitoringTask.Add(channel.BasicConsume(RabbitMQHelper.queueMonitorPaymentQueueBaseName, true, paymentConsumer));

            controllerEventConsumer.Received += ControllerEventConsumer_Received;
            monitorConsumer.Received += MonitorConsumer_Received;
            operatorConsumer.Received += OperatorConsumer_Received;
            controlCenterConsumer.Received += ControlCenterConsumer_Received;
            paymentConsumer.Received += PaymentConsumer_Received;
        }

        private void ControlCenterConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            string payLoad = Encoding.ASCII.GetString(e.Body.ToArray());
            EventRequest? eventRequest = JsonConvert.DeserializeObject<EventRequest>(payLoad);
            if (eventRequest == null)
            {
                return;
            }
            if (eventRequest.SendFrom == (int)EmRequestSendFrom.APP)
            {
                return;
            }
            this.Invoke(new Action(() =>
            {
                foreach (var item in this.ilanes)
                {
                    if (item is not IKioskOutView && item is not IKioskInView)
                    {
                        continue;
                    }
                    if (eventRequest.Type == "GET_CURRENT_REQUEST")
                    {
                        if (item is IKioskOutView)
                        {
                            ((IKioskOutView)item).NotifyLastMessage();
                        }
                        else
                        {
                            ((IKioskInView)item).NotifyLastMessage();
                        }
                    }
                    else
                    {
                        if (item is IKioskOutView)
                        {
                            ((IKioskOutView)item).ApplyConfirmResult(eventRequest);
                        }
                        else
                        {
                            ((IKioskInView)item).ApplyConfirmResult(eventRequest);
                        }
                    }
                }
            }));
        }

        private void MonitorConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            string payLoad = Encoding.ASCII.GetString(e.Body.ToArray());
        }
        private void OperatorConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                string payLoad = Encoding.ASCII.GetString(e.Body.ToArray());
                OperatorMessage? operatorMessage = JsonConvert.DeserializeObject<OperatorMessage>(payLoad);
                if (operatorMessage == null) { return; }
                switch (operatorMessage.MessageType)
                {
                    case EmOperatorMessageType.Confirm:
                        foreach (var item in ilanes)
                        {
                            if (item.Lane.Id.Equals(operatorMessage.LaneId ?? "", StringComparison.OrdinalIgnoreCase))
                            {
                                _ = ControllerHelper.OpenAllBarrie(item);
                            }
                        }
                        break;
                    case EmOperatorMessageType.Support:
                        break;
                    case EmOperatorMessageType.RefreshStatus:
                        channel!.BasicPublish(RabbitMQHelper.EXCHANGE_CMS_MONITOR_NAME, AppData.Computer!.Id, null,
                                              Encoding.UTF8.GetBytes(lastCMSMessage));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }



        }

        public class AccessMessage
        {
            public string DeviceCode { get; set; } = string.Empty;
            public string DeviceId { get; set; } = string.Empty;
            public string DeviceName { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public string PreferCard { get; set; } = string.Empty;
            public int Type { get; set; }
        }
        private void ControllerEventConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            string payload = Encoding.ASCII.GetString(e.Body.ToArray());
            this.Invoke(new Action(async () =>
            {
                try
                {
                    ShowForm();
                    SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.INFO, "RabbitMQ Message - ControlCenter", payload));
                    AccessMessage accessMessage = JsonConvert.DeserializeObject<AccessMessage>(payload)!;
                    CardEventArgs ce = new()
                    {
                        EventTime = DateTime.Now,
                        DeviceId = accessMessage.DeviceId,
                        DeviceName = accessMessage.DeviceName,
                        Type = accessMessage.Type,
                        DeviceCode = accessMessage.DeviceCode
                    };
                    string code = string.IsNullOrEmpty(accessMessage.Code) ? accessMessage.PreferCard : accessMessage.Code;
                    if (accessMessage.Type == (int)EmAccessKeyType.CARD)
                    {
                        ce.PreferCard = code;
                    }
                    else
                    {
                        //var accessKey = AppData.ApiServer.DataService.AccessKey.GetByUserId(code);
                        //var data = accessKey is not null && accessKey.Data.Count > 0 ? accessKey.Data[0] : null;
                        //ce.PreferCard = data?.Code ?? "";

                        var accessKey = (await AppData.ApiServer.DataService.AccessKey.GetById(code))?.Item1;
                        ce.PreferCard = accessKey?.Code ?? "";
                    }

                    foreach (var item in ilanes)
                    {
                        bool isValidDeviceCode = !string.IsNullOrEmpty(ce.DeviceCode) && item.Lane.Code.Contains(ce.DeviceCode);
                        bool isValidDeviceName = !string.IsNullOrEmpty(ce.DeviceName) && item.Lane.Code.Contains(ce.DeviceName);
                        bool isValidDeviceId = !string.IsNullOrEmpty(ce.DeviceId) && item.Lane.Code.Contains(ce.DeviceId);
                        if (!isValidDeviceCode && !isValidDeviceName && !isValidDeviceId)
                        {
                            continue;
                        }
                        foreach (var controlUnit in item.Lane.ControlUnits)
                        {
                            if (controlUnit.Barriers.Count == 0)
                            {
                                continue;
                            }
                            ce.AllCardFormats.Add(ce.PreferCard);
                            ce.DeviceId = controlUnit.Id;
                            string device = "";

                            if (string.IsNullOrEmpty(ce.DeviceName))
                            {
                                if (string.IsNullOrEmpty(ce.DeviceCode))
                                {
                                    device = ce.DeviceId;
                                }
                                else
                                {
                                    device = ce.DeviceCode;
                                }
                            }
                            else
                            {
                                device = ce.DeviceName;
                            }

                            DisplayStatus($"{DateTime.Now:HH:mm:ss} RABBIT MQ - {(EmAccessKeyType)ce.Type}: {ce.PreferCard} : {ce.PreferCard} Controller: {device}");
                            item.OnNewEvent(ce);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.ERROR, payload, ex));
                }
            }));
        }
        private void PaymentConsumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            try
            {
                string payload = Encoding.ASCII.GetString(e.Body.ToArray());
                SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.INFO, "RabbitMQ Message - PaymentResult", payload));
                PaymentResult? result = Newtonsoft.Json.JsonConvert.DeserializeObject<PaymentResult>(payload);
                if (result == null || string.IsNullOrEmpty(result?.Id))
                {
                    return;
                }

                this.Invoke(new Action(() =>
                {
                    foreach (var item in this.ilanes)
                    {
                        if (item is not IKioskOutView)
                        {
                            continue;
                        }
                        ((IKioskOutView)item).ApplyPaymentResult(result);
                    }
                }));
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.MessageQueue, EmSystemActionDetail.GET, EmSystemActionType.INFO, "RabbitMQ Message - PaymentResult", ex));
            }
        }
        private void DisplayStatus(string status)
        {
            kzuI_UcEventRealtime1.ShowEvent(status);
        }
        #endregion

        #region Timer
        private void StartTimer()
        {
            timerCheckLprServer = new System.Timers.Timer()
            {
                Interval = 1000
            };
            timerCheckLprServer.Elapsed += TimerCheckLprServer_Elapsed;
            timerClearLog = new System.Timers.Timer
            {
                Interval = 60000
            };
            timerClearLog.Elapsed += TimerClearLog_Tick;
            timerClearLog.Start();

            timerHeartbeat = new System.Timers.Timer
            {
                Interval = 5000
            };
            timerHeartbeat.Elapsed += TimerHeartbeat_Tick;
            timerHeartbeat.Start();

            timerUpdateDeviceStatus = new System.Timers.Timer
            {
                Interval = 15000
            };
            timerUpdateDeviceStatus.Elapsed += TimerUpdateDeviceStatusToServer_Tick;
            timerUpdateDeviceStatus.Start();
        }
        private void StopTimer()
        {
            timerClearLog?.Stop();
            timerStartApp?.Stop();
            timerHeartbeat?.Stop();
            timerUpdateDeviceStatus?.Stop();
            timerRestartSocket?.Stop();
        }
        private async void TimerLoading_Tick(object? sender, EventArgs e)
        {
            timerLoading.Enabled = false;
            kzuI_UcAppMenu1.Init(ImageHelper.GetCloneImageFromPath(AppData.OEMConfig.LogoPath), AppData.ApiServer, (EmPrintTemplate)AppData.AppConfig.PrintTemplate, !StaticPool.SelectedUser.Upn.Contains("BV", StringComparison.CurrentCultureIgnoreCase));
            kzuI_UcAppMenu1.ReportIn += KzuI_UcAppMenu1_ReportIn;
            kzuI_UcAppMenu1.OnAlarm += KzuI_UcAppMenu1_OnAlarm;
            kzuI_UcAppMenu1.ReportOut += KzuI_UcAppMenu1_ReportOut;
            kzuI_UcAppMenu1.ReportRevenue += KzuI_UcAppMenu1_ReportRevenue;
            kzuI_UcAppMenu1.Customer += KzuI_UcAppMenu1_Customer;
            kzuI_UcAppMenu1.Vehicle += KzuI_UcAppMenu1_Vehicle;
            kzuI_UcAppMenu1.AccessKey += KzuI_UcAppMenu1_AccessKey;
            kzuI_UcAppMenu1.ReportHandOver += KzuI_UcAppMenu1_ReportHandOver;
            kzuI_UcAppMenu1.ChangePassword += KzuI_UcAppMenu1_ChangePassword;
            kzuI_UcAppMenu1.OnRestart += KzuI_UcAppMenu1_OnRestart;

            CreateUI();

            DisplayStatus(KZUIStyles.CurrentResources.InitTimer);
            StartTimer();

            ConnectToRabbitMQ();
            ConnectToMQTT();
            DisplayStatus(KZUIStyles.CurrentResources.ConnectToController);
            await StartControllers();

            string appversion = FileVersionInfo.GetVersionInfo(Path.Combine(IparkingingPathManagement.baseBath, "IParkingv8.dll")).FileVersion?.ToString() ?? "";

            string lprType = AppData.LprConfig.LPRDetecterType == LprDetecter.EmLprDetecter.AmericalLpr ? "LPR-AI2" :
                             AppData.LprConfig.LPRDetecterType == LprDetecter.EmLprDetecter.KztekLPRAIServer ? "LPR-AI1" : "LPR";
            kzuI_UcEventRealtime1.Init(StaticPool.SelectedUser?.Upn ?? string.Empty,
                                       AppData.IControllers, AppData.Leds, this.lanes, StaticPool.LicenseExpire,
                                       appversion, AppData.ServerConfig.ServerName, lprType, AppData.ApiServer);


            DisplayStatus("Start Listener: 8089");
            try
            {
                HttpListener httpListenerServer = new HttpListener();
                httpListenerServer.Prefixes.Add("http://*:8089/");
                httpListenerServer.Start();
                httpListenerServer.BeginGetContext(OnRequest, httpListenerServer);

                DisplayStatus(string.Empty);
                this.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Start Listener Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void KzuI_UcAppMenu1_OnRestart(object? sender, EventArgs e)
        {
            this.isHaveRestartRequest = true;
            Application.Restart();
        }
        private void KzuI_UcAppMenu1_ChangePassword(object? sender, EventArgs e)
        {
            new FrmChangePassword(AppData.ApiServer).ShowDialog();
        }

        private async void TimerCheckLprServer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            bool isNeedStartTimer = true;
            try
            {
                timerCheckLprServer.Enabled = false;
                //if (AppData.LprConfig.LPRDetecterType == LprDetecter.EmLprDetecter.AmericalLpr ||
                //    AppData.LprConfig.LPRDetecterType == LprDetecter.EmLprDetecter.KztekLPRAIServer)
                //{
                //    var tempLpr = LprFactory.CreateLprDetecter(AppData.LprConfig, null);
                //    if (tempLpr is null)
                //    {
                //        return;
                //    }

                //    bool isValidLprServer = await tempLpr.IsValidLprServer();
                //    if (isValidLprServer)
                //    {
                //        AppData.LprDetecter = tempLpr;
                //        isNeedStartTimer = false;
                //        this.kzuI_UcEventRealtime1.UpdateLprType("LPR-AI");
                //    }
                //}
            }
            catch (Exception)
            {
                isNeedStartTimer = true;
            }
            finally
            {
                if (isNeedStartTimer)
                {
                    timerCheckLprServer.Enabled = true;
                }
            }

        }
        private async void TimerClearLog_Tick(object? sender, EventArgs e)
        {
            try
            {
                this.timerClearLog!.Enabled = false;
                SystemUtils.logger.ClearLogAfterDay(AppData.AppConfig.NumLogKeepDays * -1);
                await LaneHelper.DeleteLocalImagesAfterDays(0);
                this.timerClearLog.Enabled = true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Clear Log", ex, EmSystemActionType.ERROR));
            }
        }
        private void TimerHeartbeat_Tick(object? sender, EventArgs e)
        {
            if (channel == null)
            {
                return;
            }
            try
            {
                var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                string message = JsonConvert.SerializeObject(heartbeatMessage, setting);
                channel.BasicPublish(RabbitMQHelper.EXCHANGE_CMS_MONITOR_NAME, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(message));
            }
            catch (Exception)
            {
            }

        }
        private async void TimerUpdateDeviceStatusToServer_Tick(object? sender, EventArgs e)
        {
            if (channel == null) return;
            try
            {
                timerUpdateDeviceStatus.Enabled = false;
                ComputerMonitorMessage computerMonitorMessage = new ComputerMonitorMessage();
                computerMonitorMessage.MessageType = EmMonitorMessageType.Monitor;
                computerMonitorMessage.Computer = new ComputerMonitor()
                {
                    Name = AppData.Computer!.Name,
                    Ip = AppData.Computer!.IpAddress,
                    Status = EmDeviceStatus.Online,
                    Cameras = new List<CameraMonitor>(),
                    ControlUnits = new List<ControlUnitMonitor>(),
                };
                foreach (var item in AppData.Cameras)
                {
                    bool isConnect = await NetWorkTools.IsPingSuccessAsync(item.IpAddress, 500);
                    computerMonitorMessage.Computer.Cameras.Add(new CameraMonitor()
                    {
                        Ip = item.IpAddress,
                        Name = item.Name,
                        Status = isConnect ? EmDeviceStatus.Online : EmDeviceStatus.Offline,
                        Message = isConnect ? new List<string>() { KZUIStyles.CurrentResources.Connected } :
                                              new List<string>() { KZUIStyles.CurrentResources.Disconnected },
                    });
                }
                foreach (iParkingv5.Controller.IController item in AppData.IControllers)
                {
                    bool isConnect = await NetWorkTools.IsPingSuccessAsync(item.ControllerInfo.Comport, 500);
                    if (isConnect)
                    {
                        if (item.GetType() == typeof(MT166_CardDispenser) ||
                            item.GetType() == typeof(MT166_CardDispenserv8) ||
                            item.GetType() == typeof(MT166_CardDispenserv8_2))
                        {
                            EmDeviceStatus status = EmDeviceStatus.Online;
                            if (!item.cardDispenserStatus.IsWorking)
                            {
                                computerMonitorMessage.Computer.ControlUnits.Add(new ControlUnitMonitor()
                                {
                                    Ip = item.ControllerInfo.Comport,
                                    Name = item.ControllerInfo.Name,
                                    Status = EmDeviceStatus.Offline,
                                    Message = new List<string>() { KZUIStyles.CurrentResources.StopMode },
                                });
                            }
                            else
                            {
                                EmDeviceStatus controllerStatus = EmDeviceStatus.Online;
                                bool isError = item.cardDispenserStatus.IsError();
                                bool isWarning = item.cardDispenserStatus.IsWarning();

                                if (isError)
                                {
                                    controllerStatus = EmDeviceStatus.Error;
                                }
                                else if (isWarning)
                                {
                                    controllerStatus = EmDeviceStatus.Warning;
                                }
                                computerMonitorMessage.Computer.ControlUnits.Add(new ControlUnitMonitor()
                                {
                                    Ip = item.ControllerInfo.Comport,
                                    Name = item.ControllerInfo.Name,
                                    Status = controllerStatus,
                                    Message = item.cardDispenserStatus.GetStatusStr(),
                                });
                            }
                        }
                        else
                        {
                            computerMonitorMessage.Computer.ControlUnits.Add(new ControlUnitMonitor()
                            {
                                Ip = item.ControllerInfo.Comport,
                                Name = item.ControllerInfo.Name,
                                Status = EmDeviceStatus.Online,
                                Message = new List<string>() { KZUIStyles.CurrentResources.Connected },
                            });
                        }
                    }
                    else
                    {
                        computerMonitorMessage.Computer.ControlUnits.Add(new ControlUnitMonitor()
                        {
                            Ip = item.ControllerInfo.Comport,
                            Name = item.ControllerInfo.Name,
                            Status = EmDeviceStatus.Offline,
                            Message = new List<string>() { KZUIStyles.CurrentResources.Disconnected },
                        });
                    }

                }
                var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                string message = JsonConvert.SerializeObject(computerMonitorMessage, setting);
                if (lastCMSMessage != message)
                {
                    lastCMSMessage = message;
                    channel.BasicPublish(RabbitMQHelper.EXCHANGE_CMS_MONITOR_NAME, AppData.Computer.Id, null, Encoding.UTF8.GetBytes(message));
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                timerUpdateDeviceStatus.Enabled = true;
            }

        }
        #endregion

        #region Controller Event
        private void Controller_DeviceInfoChangeEvent(object sender, DeviceInfoChangeArgs e)
        {
        }
        private void Controller_ConnectStatusChangeEvent(object sender, ConnectStatusCHangeEventArgs e)
        {
            try
            {
                //if (e.DispenserOnChange.ArrayInputDispenser == null) return;
                //if (e.DispenserOnChange.ArrayInputDispenser.Alarm == null) return;
                foreach (ILane iLane in ilanes)
                {
                    // Dùng để phân biệt nút gọi hỗ trợ Alarm = 1 với nhiều làn
                    //if (!iLane.Lane.IsContainAlarm(e.DeviceId, e.DispenserOnChange.ArrayInputDispenser.Alarm))
                    //    continue;

                    iLane.OnNewStatus(e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception Controller_ConnectStatusChangeEvent {ex}");
            }

        }
        private void Controller_ErrorEvent(object sender, ControllerErrorEventArgs e)
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceName = e.DeviceName,
                Cmd = e.CMD,
                Description = e.ErrorString,
                Response = e.ErrorFunc,
            });
        }
        private void Controller_InputEvent(object sender, InputEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                ShowForm();
                foreach (ILane iLane in ilanes)
                {
                    if (!iLane.Lane.IsContainInput(e.DeviceId, e.InputIndex))
                    {
                        continue;
                    }
                    string message = $"{DateTime.Now:HH:mm:ss} Lane: {iLane.Lane.Name} {e.InputType}: {e.InputIndex} Controller: " + e.DeviceName;
                    kzuI_UcEventRealtime1.ShowEvent(message);
                    iLane.OnNewEvent(e);
                }
            }));

        }
        private void Controller_CardEvent(object sender, CardEventArgs e)
        {
            try
            {
                ShowForm();
                string baseCard = e.PreferCard;
                bool isValidReader = false;
                foreach (ILane iLane in this.ilanes)
                {
                    if (!iLane.Lane.IsContainReader(e.DeviceId, e.ReaderIndex, out ControllerInLane? controller))
                    {
                        continue;
                    }
                    isValidReader = true;
                    e.PreferCard = baseCard;
                    var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(iLane.Lane.Id, e.DeviceId, e.ReaderIndex);
                    var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                    new CardFormatConfig()
                                    {
                                        ReaderIndex = e.ReaderIndex,
                                        OutputFormat = CardFormat.EmCardFormat.HEXA,
                                        OutputOption = CardFormat.EmCardFormatOption.Min_8,
                                    };
                    e.PreferCard = CardFactory.StandardlizedCardNumber(e.PreferCard, config);
                    e.AllCardFormats.Add(e.PreferCard);

                    string Message = $"{DateTime.Now:HH:mm:ss} Lane: {iLane.Lane.Name} READER: {e.ReaderIndex} Button: {e.ButtonIndex}, CARD: {e.PreferCard} Controller: " + e.DeviceName;
                    kzuI_UcEventRealtime1.ShowEvent(Message);
                    kzuI_UcEventRealtime1.LastCardNumber = e.PreferCard;
                    iLane.OnNewEvent(e);
                }
                if (!isValidReader)
                {
                    string Message = KZUIStyles.CurrentResources.InvalidReaderConfig;
                    kzuI_UcEventRealtime1.ShowEvent(Message);
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("", ex));
            }

        }
        private void Controller_CancelEvent(object sender, CardCancelEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                ShowForm();
                foreach (ILane iLane in this.ilanes)
                {
                    iLane.OnNewEvent(e);
                }
            }));
        }
        private void Dispenser_controller_CardBeTaken(object sender, CardBeTakenEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                ShowForm();
                foreach (ILane iLane in this.ilanes)
                {
                    iLane.OnNewEvent(e);
                }
            }));
        }
        private void CardDispenserv2_OnCardInRFEvent(object sender, CardOnRFEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    ShowForm();
                    string baseCard = e.PreferCard;
                    foreach (ILane iLane in this.ilanes)
                    {
                        if (iLane is not IKioskInView kioskIn)
                        {
                            continue;
                        }

                        e.PreferCard = baseCard;
                        var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(iLane.Lane.Id, e.DeviceId, e.ReaderIndex);
                        var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                        new CardFormatConfig()
                                        {
                                            ReaderIndex = e.ReaderIndex,
                                            OutputFormat = CardFormat.EmCardFormat.HEXA,
                                            OutputOption = CardFormat.EmCardFormatOption.Min_8,
                                        };
                        e.PreferCard = CardFactory.StandardlizedCardNumber(e.PreferCard, config);
                        e.AllCardFormats.Add(e.PreferCard);
                        this.Invoke(new Action(() =>
                        {
                            iLane.OnNewEvent(e);
                        }));
                    }
                }
                catch (Exception ex)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("", ex));
                }
            }));

        }
        private void ShowForm()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(ShowForm);
                return;
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                try
                {
                    this.WindowState = FormWindowState.Maximized;
                    this.Show();
                    this.Activate();
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion End Controller Event

        private readonly object lockLane = new();
        private void LaneUI_OnChangeLaneEvent(object sender)
        {
            lock (lockLane)
            {
                ILane ilane = (sender as ILane)!;
                ilane.OnChangeLaneEvent -= LaneUI_OnChangeLaneEvent;
                string updateLaneId = ilane.Lane.ReverseLaneId;

                var position = ucViewGrid1.table?.GetPositionFromControl((Control)ilane) ?? new TableLayoutPanelCellPosition(0, 0);
                var displayConfig = ilane.GetLaneDisplayConfig();
                displayConfig.DisplayIndex = position.Column;
                NewtonSoftHelper<LaneDisplayConfig>.SaveConfig(displayConfig, IparkingingPathManagement.appDisplayConfigPath(ilane.Lane.Id));
                ((Control)ilane).Parent.Controls.Remove((Control)ilane);

                this.lanes.Remove(ilane.Lane);
                this.ilanes.Remove(ilane);
                ilane.Stop();

                Lane? updateLane = null;
                foreach (var item in AppData.Lanes)
                {
                    if (item.Id == updateLaneId)
                    {
                        updateLane = item;
                        this.lanes.Add(item);
                        break;
                    }
                }

                TableLayoutPanel table = ucViewGrid1.table!;
                var directionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(updateLaneId)) ?? new LaneDirectionConfig();
                var config = NewtonSoftHelper<LaneDisplayConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appDisplayConfigPath(updateLaneId)) ??
                             LaneDisplayConfig.CreateDefault(1920, updateLaneId, position.Column);
                config.DisplayIndex = Math.Min(position.Column, this.lanes.Count - 1);
                var laneUI = LaneFactory.CreateLane(updateLane!, lanes.Count, config, channel, this.kzuI_UcEventRealtime1);
                if (laneUI != null)
                {
                    laneUI.OnChangeLaneEvent += LaneUI_OnChangeLaneEvent;
                    ilanes.Add(laneUI);

                    if ((EmLaneType)updateLane.Type == EmLaneType.LANE_IN)
                    {
                        table.ColumnStyles[config.DisplayIndex] = new ColumnStyle(SizeType.Percent, 40);
                    }
                    else
                    {
                        table.ColumnStyles[config.DisplayIndex] = new ColumnStyle(SizeType.Percent, 60);
                    }
                    table.Controls.Add((Control)laneUI, config.DisplayIndex, 0);
                    ((Control)laneUI).Dock = DockStyle.Fill;
                    laneUI.ChangeLaneDirectionConfig(directionConfig);
                    table.Refresh();
                }
            }
        }

        private async void timerRestartSocket_Tick(object sender, EventArgs e)
        {
            try
            {
                timerRestartSocket.Enabled = false;
                ctsSocket?.Cancel();
                socket_listener?.Close();
                socket_listener?.Dispose();
                StartSocketServer();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                timerRestartSocket.Enabled = true;
            }
        }
        private async Task PollingReceiveSocketMessage(CancellationToken ctsToken)
        {
            while (!ctsToken.IsCancellationRequested)
            {
                try
                {
                    int recv;
                    byte[] data = new byte[1024];
                    while (true)
                    {
                        var socket = socket_listener!.Accept();
                        try
                        {
                            #region: __________________________Receive command__________________________
                            data = new byte[1024];
                            recv = socket.Receive(data);
                            socket.Shutdown(SocketShutdown.Receive);
                            string receiceMessage = Encoding.ASCII.GetString(data, 0, recv);
                            #endregion

                            if (!string.IsNullOrEmpty(receiceMessage))
                            {
                                if (receiceMessage == "GetVersion?/")
                                {
                                    socket.Send(Encoding.UTF8.GetBytes(TextFormatingTool.BeautyJson(new AppVersionInfo())));
                                }
                                else if (receiceMessage == "RestartSoftware?/")
                                {
                                    socket.Send(Encoding.UTF8.GetBytes("RestartSoftware?/OK"));
                                    socket.Shutdown(SocketShutdown.Send);
                                    SystemUtils.logger.Disconnect();
                                    string executablePath = Application.ExecutablePath;
                                    Process.Start(new ProcessStartInfo(executablePath)
                                    {
                                        UseShellExecute = true // Đặt true có thể giúp với một số vấn đề quyền hoặc tìm file
                                    });

                                    Application.Exit(); // Cho WinForms, để xử lý dọn dẹp UI
                                    Environment.Exit(0); // Thoát tiến trình hiện tại ngay lập tức
                                    return;
                                }
                                else if (receiceMessage == "CheckUpdate?/")
                                {
                                    bool isHaveUpdate = CheckForUpdate(out List<string> updateDetails);
                                    socket.Send(Encoding.UTF8.GetBytes(isHaveUpdate ? "Có bản update:\r\n" + string.Join("\r\n", updateDetails.ToArray()) : "Phiên bản hiện tại đã là mới nhất"));
                                }
                                else if (receiceMessage.Contains("GetLogByQuery?/"))
                                {
                                    string query = receiceMessage.Substring("GetLogByQuery?/".Length);
                                    string logData = GetLogFile(query);
                                    socket.Send(Encoding.UTF8.GetBytes(logData));
                                }
                                else if (receiceMessage.Contains("ClearLog?/"))
                                {
                                    string clearTimeStr = receiceMessage.Substring("ClearLog?/".Length);
                                    DateTime clearTime = DateTime.Now;
                                    try
                                    {
                                        clearTime = DateTime.Parse(clearTimeStr);
                                        ClearLog(clearTime);
                                        socket.Send(Encoding.UTF8.GetBytes("Clear Completed"));
                                    }
                                    catch (Exception)
                                    {
                                        socket.Send(Encoding.UTF8.GetBytes("Wrong Datetime format"));
                                    }
                                }
                            }
                            else
                            {
                                socket.Send(Encoding.UTF8.GetBytes("Received Empty Message"));
                            }
                            socket.Shutdown(SocketShutdown.Send);
                        }
                        catch (Exception ex)
                        {
                            socket.Send(Encoding.UTF8.GetBytes(ex.Message));
                            socket.Shutdown(SocketShutdown.Send);
                        }

                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    GC.Collect();
                    await Task.Delay(500, ctsToken);
                }
            }
        }
        private void StartSocketServer()
        {
            int port = 100;
            var LocalPort = port;
            var localEndPoint = new IPEndPoint(IPAddress.Any, LocalPort);
            socket_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket_listener.Bind(localEndPoint);
                socket_listener.Listen(10);
                ctsSocket = new CancellationTokenSource();
                Task.Run(() => PollingReceiveSocketMessage(ctsSocket.Token));
            }
            catch (Exception ex)
            {
            }
        }

        private static bool CheckForUpdate(out List<string> updateDetail)
        {
            updateDetail = new List<string>();
            if (string.IsNullOrEmpty(AppData.AppConfig.CheckForUpdatePath)) return false;

            if (!Directory.Exists(AppData.AppConfig.CheckForUpdatePath)) return false;

            try
            {
                bool isHavingUpdate = false;
                // Get all files in the specified path and its subdirectories
                string[] updatefiles = Directory.GetFiles(AppData.AppConfig.CheckForUpdatePath, "*", SearchOption.AllDirectories);
                List<string> realUpdateFiles = new List<string>();
                foreach (string file in updatefiles)
                {
                    realUpdateFiles.Add(Path.GetFileName(file));
                }

                string[] currentVersionFiles = Directory.GetFiles(Application.StartupPath, "*", SearchOption.AllDirectories);
                List<string> realCurrentFiles = new List<string>();
                foreach (string file in currentVersionFiles)
                {
                    string relativePath = file.Remove(0, Application.StartupPath.Length);
                    realCurrentFiles.Add(Path.GetFileName(file));
                }

                for (int i = 0; i < realUpdateFiles.Count; i++)
                {
                    string fileName = realUpdateFiles[i];
                    if (realCurrentFiles.Contains(fileName))
                    {
                        string currentFilePath = Path.Combine(Application.StartupPath, fileName);
                        string updateFilePath = updatefiles[i];

                        FileVersionInfo currentFileVersionInfo = FileVersionInfo.GetVersionInfo(currentFilePath);
                        string currentFilePathVersion = currentFileVersionInfo.FileVersion!;

                        FileVersionInfo updateFileVersionInfo = FileVersionInfo.GetVersionInfo(updateFilePath);
                        string updateFilePathVersion = updateFileVersionInfo.FileVersion!;

                        if (currentFilePathVersion != updateFilePathVersion)
                        {
                            updateDetail.Add(fileName + " " + currentFilePathVersion + " - UPDATE: " + updateFilePathVersion);
                            isHavingUpdate = true;
                        }
                        //update file text
                        else if (updateFilePathVersion == null && currentFilePathVersion == null)
                        {
                            System.IO.File.Delete(currentFilePath);
                            System.IO.File.Copy(updateFilePath, currentFilePath);
                        }
                    }
                    //THÊM FILE CHƯA CÓ
                    else
                    {
                        updateDetail.Add(fileName + " - ADD");
                        isHavingUpdate = true;
                    }
                }

                if (isHavingUpdate)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void ClearLog(DateTime time)
        {
            SystemUtils.logger.ClearLogBeforeTime(time);
        }

        private static string GetLogFile(string query)
        {
            string data = SystemUtils.logger.GetLogByQuery(query);
            return data;
        }
        public static LicenseExpire? GetLicenseExpireByName(string name)
        {
            var result = new LicenseExpire() { ProjectName = "FUTECH", IsExpire = true, ExpireDate = "2017/03/18" };
            var listlic = new List<string>();
            var newlic = ProjectsExprireDateLoad();
            listlic.AddRange(newlic);

            var oldLicense = listlic.Where(x => x.Split(';')[0] == name).LastOrDefault();
            if (oldLicense != null)
            {
                var lic = oldLicense.Split(';');
                result = new LicenseExpire()
                {
                    ProjectName = lic[0],
                    IsExpire = bool.Parse(lic[1]),
                    ExpireDate = lic[2]
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static List<string> ProjectsExprireDateLoad()
        {
            List<string> result = [];
            try
            {
                using (var fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "lic.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        var encrypted_content = sr.ReadToEnd();
                        if (string.IsNullOrEmpty(encrypted_content))
                        {
                            return result;
                        }
                        var decrypted_content = CryptoProvider.SimpleDecryptWithPassword(encrypted_content, SecurityModel.License_Key);
                        var licData = JsonConvert.DeserializeObject<List<MN_License>>(decrypted_content) ?? [];

                        foreach (var lic in licData)
                        {
                            var expireDate = DateTime.Now;
                            DateTime.TryParseExact(lic.ExpireDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out expireDate);
                            if (expireDate == DateTime.MinValue)
                            {
                                DateTime.TryParseExact(lic.ExpireDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expireDate);
                                expireDate = expireDate.AddHours(10);
                            }
                            result.Add($"{lic.ProjectName};{lic.IsExpire};{expireDate.ToString("yyyy/MM/dd HH:mm")}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }

            return result;
        }
    }
}
