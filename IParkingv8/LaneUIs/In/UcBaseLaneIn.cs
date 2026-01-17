using iParkingv5.Lpr;
using iParkingv5.Objects.Events;
using iParkingv8.Object.ConfigObjects.ControllerConfigs;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.Forms;
using IParkingv8.Forms.DataForms;
using IParkingv8.Forms.SystemForms;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using IParkingv8.Helpers.CardProcess;
using IParkingv8.UserControls;
using Kztek.Control8;
using Kztek.Control8.Forms;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Control8.UserControls.ucDataGridViewInfo;
using Kztek.Object;
using Kztek.Tool;
using Kztek.Tool.LogHelpers.LocalData;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Drawing.Imaging;
using static iParkingv8.Object.ConfigObjects.LaneConfigs.LaneDirectionConfig;
using static IParkingv8.Forms.FrmRegisterClient;
using static Kztek.Control8.UserControls.KZUI_UcResult;
using static Kztek.Object.InputTupe;

namespace IParkingv8.LaneUIs
{
    public partial class UcBaseLaneIn : UserControl, ILaneIn
    {
        public EmControlSizeMode SizeMode = EmControlSizeMode.SMALL;
        System.Timers.Timer? timerClearLog;

        public IDialog<ConfirmInRequest, ConfirmPlateResult> dialogConfirmIn;
        public IDialog<ConfirmInRegisterCardRequest, ConfirmInRegisterCardResult> dialogConfirmInRegister;
        public MaskedUserControl MaskedDialog;
        private Dictionary<string, DateTime> motionPlateNumbers = [];

        private LoopLprResult lastLoopLprResult;

        public List<Splitter> activeSpliters = [];
        public bool IsAllowDesignRealtime = false;
        public Dictionary<string, int> currentPosition = [];

        public KZUI_UcLaneTitle UcLaneTitle { get; set; } = new KZUI_UcLaneTitle();

        public KZUI_UcCameraList UcCameraList { get; set; } = new KZUI_UcCameraList();

        public IKZUIEventImageListIn ucEventImageListIn { get; set; } = new KZUI_UcImageList();

        public KZUI_UcResult UcResult { get; set; } = new KZUI_UcResult();

        public IKZUI_UcPlate UcPlateIn { get; set; } = new KZUI_UcPlateVertical();
        public IDataInfo ucEventInfoNew { get; set; }
        public KZUI_Function UcAppFunctions { get; set; } = new KZUI_Function();
        public tblEntryLog EntryLog { get; set; }

        #region Config
        public List<ControllerShortcutConfig>? controllerShortcutConfigs = null;
        public LaneInShortcutConfig? laneInShortcutConfig = null;
        public LaneOptionalConfig? laneOptionalConfig = null;

        public LaneDisplayConfig? laneDisplayConfig = null;
        public LaneDirectionConfig? laneDirectionConfig = null;
        #endregion

        #region Properties
        public Lane Lane { get; set; }
        public List<CardEventArgs> LastCardEventDatas { get; set; } = [];
        public List<InputEventArgs> LastInputEventDatas { get; set; } = [];

        private EntryData? lastEvent = null;
        public bool isAllowOpenBarrieManual = false;
        public bool isAllowTakeCard = false;

        public readonly Image? defaultImg = ImageHelper.Base64ToImage(AppData.DefaultImageBase64);

        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new(1, 1);
        public readonly SemaphoreSlim semaphoreSlimOnKeyPress = new(1, 1);
        public event OnChangeLaneEvent? OnChangeLaneEvent;

        private int allowOpenTime = 0;

        int timeRefresh = 0;

        public UcSelectVehicles ucSelectVehicles { get; set; }
        public bool IsBusy { get; set; }

        KZUI_UcEventRealtime ucEventRealtime;
        #endregion

        #region Forms
        public UcBaseLaneIn()
        {
            InitializeComponent();
        }
        public UcBaseLaneIn(Lane lane, KZUI_UcEventRealtime ucEventRealtime)
        {
            InitializeComponent();
            this.Lane = lane;

            this.EntryLog = new tblEntryLog(this.Lane.Name, IparkingingPathManagement.baseBath);
            LoadLaneConfig();
            InitUI();

            ucSelectVehicles = new UcSelectVehicles(true)
            {
                TargetControl = this,
                BorderRadius = 24
            };

            this.ucEventRealtime = ucEventRealtime;
            this.Load += FrmBaseLaneIn_Load;
        }

        private async void FrmBaseLaneIn_Load(object? sender, EventArgs e)
        {
            ClearInfoView();
            _ = Task.Run(new Action(() =>
            {
                LedHelper.DisplayDefaultLed(this.Lane.Id);
            }));
            var top1EntryIdWaitForConfirm = this.EntryLog.GetTop1EntryIdWaitForConfirm();
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Delete uncomplete entry event: {top1EntryIdWaitForConfirm}"));
            if (!string.IsNullOrEmpty(top1EntryIdWaitForConfirm))
            {
                bool isDeleteSuccess = await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(top1EntryIdWaitForConfirm);
                if (isDeleteSuccess)
                {
                    this.EntryLog.DeleteEvent(top1EntryIdWaitForConfirm);
                }
            }
            StartTimer();
        }
        public void Stop()
        {
            UcCameraList.Stop();
            timerCheckAllowOpenBarrie.Stop();
            timerRefreshUI.Stop();
            timerClearLog?.Stop();
        }
        #endregion

        #region Khởi Tạo Làn
        private void LoadLaneConfig()
        {
            laneOptionalConfig = NewtonSoftHelper<LaneOptionalConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneOptionalConfig(this.Lane.Id)) ?? new LaneOptionalConfig();

            laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.Lane.Id));
            laneDirectionConfig ??= this.SizeMode switch
            {
                EmControlSizeMode.SMALL => LaneDirectionConfig.CreateDefaultInSmallConfig(),
                _ => LaneDirectionConfig.CreateDefaultInConfig(),
            };

            laneInShortcutConfig = NewtonSoftHelper<LaneInShortcutConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneShortcutConfigPath(this.Lane.Id))
                                  ?? new LaneInShortcutConfig();
            controllerShortcutConfigs = NewtonSoftHelper<List<ControllerShortcutConfig>>.DeserializeObjectFromPath(IparkingingPathManagement.laneControllerShortcutConfigPath(this.Lane.Id))
                                   ?? [];
        }
        #endregion

        #region Controls In Forms
        public bool SettingClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to setting page" });

            if (AppData.IsNeedToConfirmPassword)
            {
                var frmConfirmPassword = new FrmConfirmPassword();
                if (frmConfirmPassword.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                AppData.IsNeedToConfirmPassword = false;
            }

            new FrmLaneSetting(this, AppData.Leds, AppData.Cameras, this.Lane.ControlUnits, true, this.laneOptionalConfig!, this.SizeMode, this.Size).ShowDialog();

            laneOptionalConfig = NewtonSoftHelper<LaneOptionalConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneOptionalConfig(this.Lane.Id)) ?? new LaneOptionalConfig();
            laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.Lane.Id));
            laneDirectionConfig ??= this.SizeMode switch
            {
                EmControlSizeMode.SMALL => LaneDirectionConfig.CreateDefaultInSmallConfig(),
                _ => LaneDirectionConfig.CreateDefaultInConfig(),
            };

            laneInShortcutConfig = NewtonSoftHelper<LaneInShortcutConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneShortcutConfigPath(this.Lane.Id))
                                   ?? new LaneInShortcutConfig();

            controllerShortcutConfigs = NewtonSoftHelper<List<ControllerShortcutConfig>>.DeserializeObjectFromPath(IparkingingPathManagement.laneControllerShortcutConfigPath(this.Lane.Id))
                                   ?? [];

            laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.Lane.Id)) ?? new LaneDirectionConfig();
            this.ChangeLaneDirectionConfig(laneDirectionConfig);
            this.dialogConfirmIn.LaneOptionalConfig = laneOptionalConfig;

            return true;
        }
        public async Task<bool> OpenBarrieClick(object? sender)
        {
            ClearInfoView();
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to open barrie" });
            UcResult.DisplayResult(EmResultType.PROCESS, $"{DateTime.Now:HH:mm:ss} - {KZUIStyles.CurrentResources.CustomerCommandOpenBarrie}");
            _ = ControllerHelper.OpenAllBarrie(this);

            if (lastEvent == null || !isAllowOpenBarrieManual)
            {
                var data = await ProcessCardEvent.GetLPRInfo(Lane, "", UcCameraList, UcCameraList.IsValidCarCamera(), lastLoopLprResult);
                var images = new Dictionary<EmImageType, Image?>
                {
                    {EmImageType.PANORAMA, data.PanoramaImage },
                    {EmImageType.VEHICLE, data.VehicleImage },
                    {EmImageType.PLATE_NUMBER, data.LprImage },
                    {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) },
                    {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                };
                DisplayEventImage(images, data.PlateNumber);
                _ = AlarmProcess.SaveAlarmAsync(this.Lane, data.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_KEYBOARD, this.UcCameraList, "");
            }
            return true;
        }
        public async Task<bool> CloseBarrieClick(object? sender)
        {
            ClearInfoView();
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to Close barrie" });

            foreach (var item in AppData.IControllers)
            {
                foreach (ControllerInLane controllerInLane in Lane.ControlUnits)
                {
                    if (controllerInLane.Id != item.ControllerInfo.Id)
                    {
                        continue;
                    }
                    if (controllerInLane.Barriers.Count == 0)
                    {
                        continue;
                    }

                    foreach (var reader in controllerInLane.Readers)
                    {
                        var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(this.Lane.Id, controllerInLane.Id, reader);
                        var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                        new CardFormatConfig()
                                        {
                                            ReaderIndex = reader,
                                            OutputFormat = CardFormat.EmCardFormat.HEXA,
                                            OutputOption = CardFormat.EmCardFormatOption.Min_8,
                                        };
                        if (!string.IsNullOrEmpty(config.CloseBarrieIndex))
                        {
                            return await item.OpenDoor(100, int.Parse(config.CloseBarrieIndex));
                        }
                    }
                }
            }
            return true;
        }

        public async Task<bool> WriteInClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to write in" });
            UcResult.DisplayResult(EmResultType.PROCESS, $"{DateTime.Now:HH:mm:ss} - {KZUIStyles.CurrentResources.CustomerCommandWriteIn}");
            FrmSelectCard frm = new();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                UcResult.DisplayResult(EmResultType.PROCESS, AppData.OEMConfig.AppName);
                return false;
            }

            foreach (ControllerInLane controllerInLane in Lane.ControlUnits)
            {
                if (controllerInLane.Barriers.Count == 0)
                {
                    continue;
                }

                CardEventArgs ce = new()
                {
                    EventTime = DateTime.Now,
                    DeviceId = controllerInLane.Id,
                    AllCardFormats = [frm.SelectedAccessKeyCode],
                    PreferCard = frm.SelectedAccessKeyCode,
                };

                await OnNewEvent(ce);

                if (lastEvent != null)
                {
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, lastEvent.PlateNumber, frm.SelectedAccessKeyCode, EmAlarmCode.ENTRY_CREATED_MANUALLY, this.UcCameraList, "");
                    break;
                }
                break;
            }
            return true;
        }
        public async Task<bool> RetakeImageClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to retake image" });
            if (!Lane.Loop || Lane.ControlUnits == null)
            {
                return false;
            }
            UcResult.DisplayResult(EmResultType.PROCESS, $"{DateTime.Now:HH:mm:ss} - {KZUIStyles.CurrentResources.CustomerCommandCapture}");

            foreach (ControllerInLane controllerInLane in Lane.ControlUnits)
            {
                if (controllerInLane.Barriers.Count == 0)
                {
                    continue;
                }
                InputEventArgs ie = new()
                {
                    DeviceId = controllerInLane.Id,
                    InputType = EmInputType.Loop,
                    EventTime = DateTime.Now
                };
                await OnNewEvent(ie);
                return true;
            }
            return true;
        }
        private bool isInRegisterMode = false;
        private iParkingv5.Controller.IController? StopControllerEvent(string controllerId)
        {
            this.isInRegisterMode = true;
            foreach (var iController in AppData.IControllers)
            {
                if (!iController.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                return iController;
            }
            return null;
        }
        private void StartControllerEvent(string controllerId)
        {
            this.isInRegisterMode = false;
            foreach (var iController in AppData.IControllers)
            {
                if (!iController.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                break;
            }
        }

        public async Task<bool> RegisterClientClick(object? sender)
        {
            foreach (ControllerInLane _controllerInLane in this.Lane.ControlUnits)
            {
                if (_controllerInLane.Barriers.Count == 0)
                {
                    continue;
                }

                var controller = StopControllerEvent(_controllerInLane.Id);
                if (controller == null)
                {
                    continue;
                }

                var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, true);
                if (string.IsNullOrEmpty(lprResult.PlateNumber))
                {
                    lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
                }
                FrmRegisterClient frm = new(this.Lane, controller, lprResult.PlateNumber);
                frm.ShowDialog();

                if (frm.DialogResult != DialogResult.OK)
                {
                    StartControllerEvent(_controllerInLane.Id);
                    return true;
                }

                var customerInfo = frm.RegisterCustomerInfo!;
                string note = $"Tên: {customerInfo.Name};Biển số Xe: {customerInfo.PlateNumber};Lý do: {customerInfo.Reason};Hình thức: {CustomerType.ToDisplayString(customerInfo.CustomerType)};Giờ đăng ký:{DateTime.Now.ToString("HH-mm-ss dd/MM/yyyy")}";

                CardEventArgs ce = new()
                {
                    EventTime = DateTime.Now,
                    DeviceId = _controllerInLane.Id,
                    ReaderIndex = _controllerInLane.Readers[0],
                    PreferCard = frm.SelectIdentity,
                    InputType = InputTupe.EmInputType.Alarm,
                    Note = note,
                    PlateNumber = customerInfo.PlateNumber,
                    Type = (int)frm.AccessKeyType,
                };
                StartControllerEvent(_controllerInLane.Id);
                await this.OnNewEvent(ce);
            }

            return true;
        }
        #endregion

        #region Event
        private bool isExecuteCardEvent = false;
        private bool isCheckMotionEvent = false;
        public async Task OnNewEvent(EventArgs e)
        {
            if (this.isInRegisterMode)
            {
                return;
            }
            if (e is MotionDetectEventArgs && isCheckMotionEvent)
            {
                return;
            }
            if (e is CardEventArgs && isExecuteCardEvent)
            {
                return;
            }
            await semaphoreSlimOnNewEvent.WaitAsync();
            this.IsBusy = true;
            StopTimeRefreshUI();
            Stopwatch stw = Stopwatch.StartNew();
            try
            {
                string eventId = Guid.NewGuid().ToString();
                if (e is CardEventArgs cardEvent)
                {
                    this.UcLaneTitle.NotifyCardEvent();
                    List<CardEventArgs> tempList = LastCardEventDatas;
                    bool isNewCardEvent = CardBaseProcess.CheckNewCardEvent(cardEvent, out int thoiGianCho, ref tempList);
                    this.LastCardEventDatas = tempList;
                    if (!isNewCardEvent)
                    {
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{cardEvent.PreferCard} - In Waiting Time: {thoiGianCho}s"));
                        this.ucEventRealtime.ShowErrorMessage($"{this.Lane.Name} - {cardEvent.PreferCard} {KZUIStyles.CurrentResources.AccessKeyInWaitingTime} {thoiGianCho}s");
                        return;
                    }

                    //Sử dụng cho Metropolis - leto: Không muốn hiển thị trạng thái thẻ không có trong hệ thống
                    if (!AppData.AppConfig.IsDisplayNotExistCardNotify)
                    {
                        var accessKey = await AppData.ApiServer.DataService.AccessKey.GetByCodeAsync(cardEvent.PreferCard);
                        if (accessKey != null && accessKey.Item2 != null)
                        {
                            this.ucEventRealtime.ShowErrorMessage($"{this.Lane.Name} - {cardEvent.PreferCard} {KZUIStyles.CurrentResources.AccessKeyNotFound}");
                            return;
                        }
                    }

                    isExecuteCardEvent = true;
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name} New Card Event", cardEvent));
                    if (AppData.AppConfig.CardTakeImageDelay > 0)
                    {
                        await Task.Delay(AppData.AppConfig.CardTakeImageDelay);
                    }
                    await ExecuteCardEvent(cardEvent, eventId);
                }
                else if (e is InputEventArgs inputEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name} New Input Event", inputEvent));
                    this.UcLaneTitle.NotifyLoopEvent();
                    await ExecuteInputEvent(inputEvent, eventId);
                }
                else if (e is ControllerErrorEventArgs errorEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}New Error Event", errorEvent));
                    await ExecuteEventError(errorEvent);
                }
                else if (e is CardCancelEventArgs cancelEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}New Cancel Event", cancelEvent));
                    await ExecuteCancelEvent(cancelEvent);
                }
                else if (e is MotionDetectEventArgs motionDetectEvent)
                {
                    isCheckMotionEvent = true;
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name} New Motion Event"));
                    this.UcLaneTitle.NotifyMotionEvent();
                    string deviceId = "";
                    foreach (ControllerInLane controllerInLane in Lane.ControlUnits)
                    {
                        if (controllerInLane.Barriers.Count == 0)
                        {
                            continue;
                        }
                        deviceId = controllerInLane.Id;
                    }
                    motionDetectEvent.DeviceId = deviceId;
                    await ExecuteMotionEvent(motionDetectEvent, eventId);
                }
                else if (e is CardBeTakenEventArgs cardBeTakenEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Input CardBeTakenEvent", cardBeTakenEvent));
                    this.UcLaneTitle.NotifyLoopEvent();
                    await ExecuteCardbeTaken(cardBeTakenEvent);
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name} OnNewEvent Error", ex));
            }
            finally
            {
                stw.Stop();
                this.ucEventRealtime.UpdateDuration(this.Lane.Id, stw.ElapsedMilliseconds);
                this.IsBusy = false;
                isExecuteCardEvent = false;
                isCheckMotionEvent = false;
                this.UcLaneTitle.NotifyNoneEvent();
                semaphoreSlimOnNewEvent.Release();
                StartTimerRefreshUI();
                GC.Collect();
            }
        }
        public async void OnKeyPress(Keys keys)
        {
            await semaphoreSlimOnKeyPress.WaitAsync();
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"KeyPress {keys}"));
            try
            {
                if (keys == Keys.F9)
                {
                    AllowDesignRealtime(!IsAllowDesignRealtime);

                }
                await CheckLaneInShortcutConfig(keys);
                await CheckControllerShortcutConfig(keys);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"KeyPress {keys} Error", ex));
            }
            finally
            {
                semaphoreSlimOnKeyPress.Release();
            }
        }
        public async Task OnNewStatus(EventArgs e)
        {

        }

        #region Xử Lý Sự Kiện Chuyển Động
        public async Task ExecuteMotionEvent(MotionDetectEventArgs ie, string eventId)
        {
            string baseLoopLog = $"{this.Lane.Name}.MotionEvent";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START"));

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, isForceCar: false);

            //Kiểm tra nếu xe này vừa vào bằng thẻ thì bỏ qua
            if (lprResult.Vehicle != null)
            {
                foreach (var cardEvent in this.LastCardEventDatas)
                {
                    var lastEntryData = lprResult.Vehicle.Metrics
                                        .Where(e => e.Code.Equals(cardEvent.PreferCard, StringComparison.CurrentCultureIgnoreCase))
                                        .FirstOrDefault();
                    if (lastEntryData is not null)
                    {
                        double v = Math.Abs((DateTime.Now - cardEvent.EventTime).TotalSeconds);
                        if (v < AppData.AppConfig.MinDelayCardTime)
                        {
                            this.ucEventRealtime.ShowErrorMessage($"{this.Lane.Name} - Phương tiện vừa vào bằng {lastEntryData.Name} trước đó. Thử lại sau {(int)(AppData.AppConfig.MinDelayCardTime - v)}s");
                            return;
                        }
                    }
                }
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - DISPLAY EVENT IMAGE"));
            Image? panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
            Image? faceImage = await UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
            Image? otherImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Other);
            lprResult.PanoramaImage = panoramaImage;

            Dictionary<EmImageType, List<List<byte>>> imageBytes = new Dictionary<EmImageType, List<List<byte>>>()
            {
                { EmImageType.PANORAMA, [ImageHelper.GetByteArrayFromImage(panoramaImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.VEHICLE, [ImageHelper.GetByteArrayFromImage(lprResult.VehicleImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.PLATE_NUMBER, [ImageHelper.GetByteArrayFromImage(lprResult.LprImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.FACE, [ImageHelper.GetByteArrayFromImage(faceImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.OTHER, [ImageHelper.GetByteArrayFromImage(otherImage, ImageFormat.Jpeg)?.ToList()] },
            };
            Dictionary<EmImageType, Image?> images = new()
            {
                { EmImageType.PANORAMA, panoramaImage },
                { EmImageType.VEHICLE, lprResult.VehicleImage },
                { EmImageType.PLATE_NUMBER, lprResult.LprImage },
                { EmImageType.FACE, faceImage},
                { EmImageType.OTHER, otherImage },
            };

            if (string.IsNullOrWhiteSpace(lprResult.PlateNumber) || lprResult.PlateNumber.Length < 5)
            {
                this.ucEventRealtime.ShowErrorMessage($"{DateTime.Now:HH:mm:ss} - {this.Lane.Name} - {lprResult.PlateNumber} {KZUIStyles.CurrentResources.InvalidPlateNumber}");
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));


                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                return;
            }
            if (lprResult.Vehicle == null && !this.laneOptionalConfig!.IsRegisterTurnVehicle)
            {
                this.ucEventRealtime.ShowErrorMessage($"{DateTime.Now:HH:mm:ss} - {this.Lane.Name} - {lprResult.PlateNumber} {KZUIStyles.CurrentResources.VehicleNotFound}");
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                return;
            }

            if (motionPlateNumbers.ContainsKey(lprResult.PlateNumber))
            {
                double duration = Math.Abs((DateTime.Now - motionPlateNumbers[lprResult.PlateNumber]).TotalSeconds);
                if (duration <= 10)
                {
                    return;
                }
                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                motionPlateNumbers[lprResult.PlateNumber] = DateTime.Now;
            }
            else
            {
                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                motionPlateNumbers.Add(lprResult.PlateNumber, DateTime.Now);
            }

            foreach (var item in motionPlateNumbers.Keys)
            {
                double duration = Math.Abs((DateTime.Now - motionPlateNumbers[lprResult.PlateNumber]).TotalSeconds);
                if (duration >= 10)
                {
                    motionPlateNumbers.Remove(item);
                }
            }

            ClearInfoView();

            ucEventImageListIn.DisplayImage(images);
            UcPlateIn.DisplayLprResult(lprResult.PlateNumber ?? "", lprResult.LprImage);

            if (lprResult.Vehicle == null)
            {
                //Hiển thị hộp thoại cập nhật biển
                ConfirmPlateRequest request = new()
                {
                    PlateNumber = lprResult.PlateNumber ?? "",
                    LprImage = lprResult.LprImage,
                };

                var result = await this.Invoke(async () => await MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, new UcRegisterPlate(this.laneOptionalConfig)));
                bool isConfirm = result != null && result.IsConfirm;
                if (!isConfirm)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotFound);
                    return;
                }

                string updatePlate = result!.UpdatePlate;
                if (updatePlate == lprResult.PlateNumber)
                {
                    bool isRegisterSuccess = await RegisterDailyLoopVehicle(lprResult, baseLoopLog);
                    if (!isRegisterSuccess) return;
                }
                else
                {
                    lprResult.PlateNumber = updatePlate;
                    UcPlateIn.DisplayNewPlate(updatePlate);

                    var registeredVehicle = (await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(updatePlate))?.Item1;
                    if (registeredVehicle != null && !string.IsNullOrEmpty(registeredVehicle.Id))
                    {
                        lprResult.Vehicle = registeredVehicle;
                    }
                    else
                    {
                        bool isRegisterSuccess = await RegisterDailyLoopVehicle(lprResult, baseLoopLog);
                        if (!isRegisterSuccess) return;
                    }
                }
            }

            Collection collection = lprResult.Vehicle!.Collection!;
            var accessKeyType = collection.GetAccessKeyGroupType();
            bool isAllowEntryByLoop = collection.GetEntryByLoop();

            if (!isAllowEntryByLoop && accessKeyType != EmAccessKeyGroupType.DAILY)
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotAllowEntryByPlate);
                return;
            }

            switch (accessKeyType)
            {
                case EmAccessKeyGroupType.DAILY:
                    await ExecuteDAILYLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                case EmAccessKeyGroupType.MONTHLY:
                    await ExecuteMONTHLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                case EmAccessKeyGroupType.VIP:
                    await ExecuteVIPLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                default:
                    break;
            }
        }
        #endregion

        #region Xử Lý Sự Kiện Quẹt Thẻ -- OK
        public async Task ExecuteCardEvent(CardEventArgs ce, string eventId)
        {
            string baseLog = $"{this.Lane.Name}.CARD.{ce.PreferCard}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - START"));

            isAllowOpenBarrieManual = false;
            ClearInfoView();

            UcResult.DisplayResult(EmResultType.PROCESS, $"{KZUIStyles.CurrentResources.ProcessChecking}: " + ce.PreferCard);

            #region Kiểm tra thông tin định danh
            lastEvent = null;
            var cardValidate = await CardBaseProcess.ValidateAccessKeyByCode(Lane, ce, baseLog);
            _ = AlarmProcess.InvokeCardValidateAsync(cardValidate, this.Lane, this.UcCameraList);

            if (cardValidate.CardValidateType != EmAlarmCode.NONE)
            {
                if (cardValidate.AccessKey == null)
                {
                    if (cardValidate.CardValidateType == EmAlarmCode.ACCESS_KEY_NOT_FOUND)
                    {
                        var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(this.Lane.Id, ce.DeviceId, ce.ReaderIndex);
                        var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath) ??
                                        new CardFormatConfig()
                                        {
                                            ReaderIndex = ce.ReaderIndex,
                                            OutputFormat = CardFormat.EmCardFormat.HEXA,
                                            OutputOption = CardFormat.EmCardFormatOption.Min_8,
                                        };

                        var data = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, UcCameraList.IsValidCarCamera(), lastLoopLprResult);
                        DisplayEventImage(new Dictionary<EmImageType, Image?>
                        {
                            {EmImageType.PANORAMA, data.PanoramaImage },
                            {EmImageType.VEHICLE, data.VehicleImage },
                            {EmImageType.PLATE_NUMBER, data.LprImage },
                            {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) },
                            {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                        }, data.PlateNumber);

                        if (string.IsNullOrEmpty(config.DailyCardGroupIdManual))
                        {
                            UcResult.DisplayResult(EmResultType.ERROR, UIBuiltInResourcesHelper.GetValue(cardValidate.DisplayAlarmMessageTag));
                            SoundHelper.InvokeCardValidate(cardValidate, ce.DeviceId);
                            ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, null, null, null, 0, "",
                                                            laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                            return;
                        }

                        //Ghi vé thủ công
                        var collectionRegister = AppData.AccessKeyCollections.Where(e => e.Id == config.DailyCardGroupIdManual)?.FirstOrDefault();

                        ConfirmInRegisterCardRequest request = new()
                        {
                            Code = ce.PreferCard,
                            Collection = collectionRegister?.Name ?? "",
                            PlateNumber = data.PlateNumber,
                            Images = new Dictionary<EmImageType, Image?>
                            {
                                {EmImageType.PANORAMA, data.PanoramaImage },
                                {EmImageType.VEHICLE, data.VehicleImage },
                                {EmImageType.PLATE_NUMBER, data.LprImage },
                                {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) },
                                {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                            }
                        };
                        var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmInRegister));
                        bool isConfirm = result?.IsConfirm ?? false;
                        if (!isConfirm)
                        {
                            UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleEntry);
                            return;
                        }

                        UcResult.DisplayResult(EmResultType.PROCESS, "Đang tạo định danh");
                        AccessKey? newAccessKey = new AccessKey()
                        {
                            Name = "TEMP_" + ce.PreferCard,
                            Code = ce.PreferCard,
                            Collection = new Collection()
                            {
                                Id = config.DailyCardGroupIdManual,
                            },
                            Type = EmAccessKeyType.CARD,
                        };
                        newAccessKey = (await AppData.ApiServer.DataService.AccessKey.CreateAsync(newAccessKey))?.Item1;
                        if (string.IsNullOrEmpty(newAccessKey?.Id))
                        {
                            ExecuteSystemError(false);
                            return;
                        }
                        await ExecuteCardEvent(ce, eventId);
                        return;
                    }
                    else
                    {
                        UcResult.DisplayResult(EmResultType.ERROR, UIBuiltInResourcesHelper.GetValue(cardValidate.DisplayAlarmMessageTag));
                        SoundHelper.InvokeCardValidate(cardValidate, ce.DeviceId);
                        ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, null, null, null, 0, "",
                                                        laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                        return;
                    }
                }
                else
                {
                    var data = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, UcCameraList.IsValidCarCamera(), lastLoopLprResult);
                    DisplayEventImage(new Dictionary<EmImageType, Image?>
                    {
                        {EmImageType.PANORAMA, data.PanoramaImage },
                        {EmImageType.VEHICLE, data.VehicleImage },
                        {EmImageType.PLATE_NUMBER, data.LprImage },
                        {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) } ,
                        {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                    }, data.PlateNumber);

                    UcResult.DisplayResult(EmResultType.ERROR, UIBuiltInResourcesHelper.GetValue(cardValidate.DisplayAlarmMessageTag));
                    SoundHelper.InvokeCardValidate(cardValidate, ce.DeviceId);
                    ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, cardValidate.AccessKey, cardValidate.AccessKey.Collection,
                                                    cardValidate.AccessKey.GetVehicleInfo(), 0, cardValidate.AccessKey.Note ?? "",
                                                    laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                    return;
                }
            }
            AccessKey? accessKeyResponse = cardValidate.AccessKey!;
            #endregion

            #region Cập nhật thông tin đổi nhóm định danh cho máy nhả thẻ 2 nút
            bool isCheckCardDipenserSuccess;

            (accessKeyResponse, isCheckCardDipenserSuccess) = await CardBaseProcess.CheckCardDispenserCollection(this.Lane, ce, baseLog,
                                                                                     accessKeyResponse, ucEventInfoNew, UcCameraList, this.UcResult, laneDirectionConfig!);
            if (!isCheckCardDipenserSuccess || accessKeyResponse == null)
            {
                var data = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, UcCameraList.IsValidCarCamera(), lastLoopLprResult);
                DisplayEventImage(new Dictionary<EmImageType, Image?>
                {
                    {EmImageType.PANORAMA, data.PanoramaImage },
                    {EmImageType.VEHICLE, data.VehicleImage },
                    {EmImageType.PLATE_NUMBER, data.LprImage },
                    {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) },
                    {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                }, data.PlateNumber);
                return;
            }
            #endregion

            var collection = accessKeyResponse.Collection!;
            bool isCar = collection.GetVehicleType() == EmVehicleType.CAR;

            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessReadingPlate);
            var eventImageInfo = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, isCar, lastLoopLprResult);

            string plateNumber = eventImageInfo.PlateNumber;
            Image? panoramaImage = eventImageInfo.PanoramaImage;
            Image? vehicleImage = eventImageInfo.VehicleImage;
            Image? lprImage = eventImageInfo.LprImage;

            var images = new Dictionary<EmImageType, Image?>
            {
                {EmImageType.PANORAMA, panoramaImage },
                {EmImageType.VEHICLE, vehicleImage},
                {EmImageType.PLATE_NUMBER, lprImage },
                {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) } ,
                {EmImageType.OTHER, await this.UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
            };

            this.EntryLog.InitEvent(eventId, accessKeyResponse.Id, this.Lane.Id, ce.DeviceId, plateNumber);

            try
            {
                StopTimerCheckAllowOpenBarrie();
                var accessKeyGroupType = collection.GetAccessKeyGroupType();
                string standardlizePlate = PlateHelper.StandardlizePlateNumber(plateNumber, true);
                if (!string.IsNullOrEmpty(ce.PlateNumber))
                {
                    standardlizePlate = PlateHelper.StandardlizePlateNumber(ce.PlateNumber, true);
                }
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Start {accessKeyGroupType} Card Process"));

                switch (accessKeyGroupType)
                {
                    case EmAccessKeyGroupType.DAILY:
                        await ExecuteDAILYCardEvent(accessKeyResponse, ce, images, standardlizePlate, collection, eventId);
                        break;
                    case EmAccessKeyGroupType.MONTHLY:
                        await ExecuteMONTHCardEvent(accessKeyResponse, ce, images, standardlizePlate, collection, eventId);
                        break;
                    case EmAccessKeyGroupType.VIP:
                        await ExcecuteVIPCardEvent(accessKeyResponse, ce, images,
                                                    standardlizePlate, collection, eventId);
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                ClearInfoView();
                UcResult.DisplayResult(EmResultType.ERROR, $"Ex-IN-001 - {KZUIStyles.CurrentResources.SystemError}, {KZUIStyles.CurrentResources.TryAgain}");
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Ex001 - Check In Error", ex));
                MessageBox.Show(ex.Message, $"Ex-IN-001 - {KZUIStyles.CurrentResources.SystemError}", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region DAILY CARD
        private async Task ExecuteDAILYCardEvent(AccessKey accessKey, CardEventArgs ce, Dictionary<EmImageType, Image?> images,
                                                 string plateNumber, Collection collection, string eventId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.DAILY.Card.{accessKey.Code}.{accessKey.Name}";
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckIn);
            this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.StartCheckIn);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Normal Request"));
            var entryResponse = await AppData.ApiServer.OperatorService!.Entry.CreateAsync(eventId, Lane.Id, accessKey.Id, plateNumber, collection.Id);
            var validateEntryResponse = ValidateEntryResponse(entryResponse, plateNumber, accessKey.Collection!, images, accessKey, null, ce, false);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Check In Normal Response", validateEntryResponse));

            if (!validateEntryResponse.IsValidEvent && !validateEntryResponse.IsNeedConfirm)
            {
                this.EntryLog.DeleteEvent(eventId);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Invalid Event, End Process", validateEntryResponse));
                return;
            }

            var entry = validateEntryResponse.EventData!;

            string errorMessage = string.Empty;
            if ((string.IsNullOrEmpty(plateNumber) || plateNumber.Length <= 4) && AppData.AppConfig.IsRequiredDAILYPlateIn)
            {
                errorMessage = KZUIStyles.CurrentResources.ProcessInvalidDailyVehicleIn;
            }

            bool isNeedDisplayImage = true;
            bool isNeedDisplayLed = true;
            if (!string.IsNullOrEmpty(errorMessage) || !entry.OpenBarrier)
            {
                isNeedDisplayLed = false;
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, accessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");
                isNeedDisplayImage = false;
                DisplayEventImage(images, plateNumber);
                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForConfirm);
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string confirmMessage = string.IsNullOrEmpty(errorMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : errorMessage;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Request {confirmMessage}"));

                ConfirmInRequest request = new()
                {
                    AbnormalCode = validateEntryResponse.AlarmCode,
                    Message = confirmMessage,
                    AccessKey = accessKey,
                    PlateNumber = plateNumber,
                    RegisterPlate = string.Empty,
                    Images = images,
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForDelete);
                    await ExecuteNotConfirmEntry(entry, ce, confirmMessage, baseCardEventLog);
                    return;
                }
            }

            lastEvent = entry;

            ce.EventTime = DateTime.Now;
            if (!string.IsNullOrEmpty(ce.Note))
            {
                await AppData.ApiServer.OperatorService.Entry.UpdateNoteAsync(ce.Note, entry.Id);
            }
            if (ce.InputType == EmInputType.Button)
            {
                this.isAllowTakeCard = true;
            }
            await ExcecuteValidEvent(accessKey, null, collection, images, ce.EventTime, entry, ce.DeviceId, baseCardEventLog, isNeedDisplayImage, ce.InputType != EmInputType.Button, isNeedDisplayLed);
        }
        #endregion END DAILY CARD

        #region MONTH CARD
        private async Task ExecuteMONTHCardEvent(AccessKey accessKey, CardEventArgs ce, Dictionary<EmImageType, Image?> images,
                                                 string plateNumber, Collection collection, string eventId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.MONTH.Card.{accessKey.Code}.{accessKey.Name}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Valid Register Vehicle - Event MonthCard"));

            var monthCardValidate = await CardMonthProcess.ValidateAccessKeyByCode(accessKey, plateNumber, this, this.ucSelectVehicles);
            if (monthCardValidate.MonthCardValidateType != EmMonthCardValidateType.SUCCESS)
            {
                DisplayEventImage(images, plateNumber);

                if (monthCardValidate.RegisterVehicle == null)
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle);
                    ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, accessKey, accessKey.Collection, null, 0, "",
                                                    this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, "", EmAlarmCode.ACCESS_KEY_INVALID, this.UcCameraList, accessKey.Id);
                }
                else
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleEntry);
                }
                this.EntryLog.DeleteEvent(eventId);
                return;
            }
            AccessKey registeredVehicle = monthCardValidate.RegisterVehicle!;

            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                DisplayEventImage(images, plateNumber);
                UcResult.DisplayResult(EmResultType.ERROR, registeredVehicle.Note ?? KZUIStyles.CurrentResources.VehicleLocked);
                ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, accessKey, accessKey.Collection, registeredVehicle, 0, "",
                                                this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, registeredVehicle.Note ?? KZUIStyles.CurrentResources.VehicleLocked, EmAlarmCode.ACCESS_KEY_INVALID, this.UcCameraList, accessKey.Id);
                this.EntryLog.DeleteEvent(eventId);
                return;
            }

            plateNumber = PlateHelper.StandardlizePlateNumber(monthCardValidate.UpdatePlate, true);
            bool isCheckInByVehicle = monthCardValidate.IsCheckByPlate;

            string checkInAccessKeyId = isCheckInByVehicle ? registeredVehicle.Id : accessKey.Id;
            await ExecuteMONTH_VEHICLE_CardEvent(baseCardEventLog, checkInAccessKeyId, accessKey, registeredVehicle, images, ce,
                                                 isCheckInByVehicle, plateNumber, collection, eventId);
        }

        /// <summary>
        /// Xử lý logic trường hợp thẻ tháng có gắn phương tiện
        /// </summary>
        /// <param name="baseCardEventLog"></param>
        /// <param name="checkInAccessKeyId"></param>
        /// <param name="accessKey"></param>
        /// <param name="registeredVehicle"></param>
        /// <param name="plateNumber"></param>
        /// <param name="ce"></param>
        /// <param name="isCheckInByVehicle"></param>
        /// <param name="panoramaImage"></param>
        /// <param name="vehicleImage"></param>
        /// <param name="lprImage"></param>
        /// <param name="plateNumber"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        private async Task ExecuteMONTH_VEHICLE_CardEvent(string baseCardEventLog, string checkInAccessKeyId, AccessKey accessKey, AccessKey registeredVehicle,
                                                          Dictionary<EmImageType, Image?> images, CardEventArgs ce, bool isCheckInByVehicle, string plateNumber, Collection collection, string eventId)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Request"));
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckIn);
            this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.StartCheckIn);

            var entryResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, Lane.Id, checkInAccessKeyId, plateNumber, collection.Id);
            var validateEntryResponse = ValidateEntryResponse(entryResponse, plateNumber, collection, images, accessKey, registeredVehicle, ce, isCheckInByVehicle);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Event In Response", validateEntryResponse));

            if (!validateEntryResponse.IsValidEvent && !validateEntryResponse.IsNeedConfirm)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Invalid Event, End Process", validateEntryResponse));
                this.EntryLog.DeleteEvent(eventId);
                return;
            }

            var entry = validateEntryResponse.EventData!;
            string confirmMessage = validateEntryResponse.IsNeedConfirm ? validateEntryResponse.ErrorMessage : string.Empty;
            bool isNeedDisplayImage = true;
            bool isNeedDisplayLed = true;
            if (!string.IsNullOrEmpty(confirmMessage) || !entry.OpenBarrier)
            {
                isNeedDisplayLed = false;
                isNeedDisplayImage = false;
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, accessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");

                DisplayEventImage(images, plateNumber);
                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForConfirm);

                string alarmMessage = string.IsNullOrEmpty(confirmMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : confirmMessage;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Plate Request"));

                ConfirmInRequest request = new()
                {
                    AbnormalCode = validateEntryResponse.AlarmCode,
                    Message = alarmMessage,
                    AccessKey = accessKey,
                    PlateNumber = plateNumber,
                    RegisterPlate = registeredVehicle.Code,
                    Images = images
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                if (result == null || !result.IsConfirm)
                {
                    this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForDelete);
                    await ExecuteNotConfirmEntry(entry, ce, alarmMessage, baseCardEventLog);
                    return;
                }
                else
                {
                    if (!result.UpdatePlate.Equals(entry.PlateNumber, StringComparison.CurrentCultureIgnoreCase))
                    {
                        _ = AppData.ApiServer.OperatorService.Entry.UpdatePlateAsync(entry.Id, result.UpdatePlate.ToUpper(), entry.PlateNumber);
                        UcPlateIn.DisplayNewPlate(result.UpdatePlate.ToUpper());
                        entry.PlateNumber = result.UpdatePlate.ToUpper();
                    }
                }
            }

            lastEvent = entry;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            ce.EventTime = DateTime.Now;
            this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.Entry);
            await ExcecuteValidEvent(accessKey, registeredVehicle!, collection, images,
                                     ce.EventTime, entry, ce.DeviceId, baseCardEventLog, isNeedDisplayImage, true, isNeedDisplayLed);
        }
        #endregion END MONTH CARD

        #region VIP CARD
        private async Task ExcecuteVIPCardEvent(AccessKey accessKey, CardEventArgs ce, Dictionary<EmImageType, Image?> images, string plateNumber, Collection collection, string eventId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.Card.{ce.PreferCard}";

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Valid Register Vehicle - Event VipCard"));

            var vipCardValidate = await CardVipProcess.ValidateAccessKeyByCode(accessKey, plateNumber, this, this.ucSelectVehicles);
            if (vipCardValidate.VipCardValidateType != EmVipCardValidateType.SUCCESS)
            {
                DisplayEventImage(images, plateNumber);
                if (vipCardValidate.RegisterVehicle == null)
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.AccessKeyVipNoVehicle);
                    ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, accessKey, accessKey.Collection, null, 0, "",
                                                    this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, "", EmAlarmCode.ACCESS_KEY_INVALID, this.UcCameraList, accessKey.Id);
                }
                this.EntryLog.DeleteEvent(eventId);
                return;
            }
            await ExecuteVIP_VEHICLE_CardEvent(vipCardValidate, baseCardEventLog, accessKey, collection, ce, images, eventId);
        }
        private async Task ExecuteVIP_VEHICLE_CardEvent(VipCardValidate vipCardValidate, string baseCardEventLog,
                                                        AccessKey accessKey, Collection collection, CardEventArgs ce, Dictionary<EmImageType, Image?> images,
                                                         string eventId)
        {
            AccessKey registeredVehicle = vipCardValidate.RegisterVehicle!;
            string plateNumber = vipCardValidate.UpdatePlate;
            string standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            bool isCheckInByVehicle = vipCardValidate.IsCheckByPlate;

            string checkInAccessKeyId = (isCheckInByVehicle ? registeredVehicle.Id : accessKey.Id) ?? "";

            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                DisplayEventImage(images, plateNumber);
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, accessKey, accessKey.Collection, registeredVehicle, 0, "",
                                                this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                this.EntryLog.DeleteEvent(eventId);
                return;
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Request"));
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckIn);
            this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.StartCheckIn);
            var entryResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, Lane.Id, checkInAccessKeyId, standardlizePlateNumber, collection.Id);

            if (entryResponse == null)
            {
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true);
                this.EntryLog.DeleteEvent(eventId);
                return;
            }
            if (entryResponse.Item1 == null && entryResponse.Item2 == null)
            {
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false);
                this.EntryLog.DeleteEvent(eventId);
                return;
            }

            string confirmMessage = "";
            var errorData = entryResponse.Item2;
            if (errorData is not null)
            {
                this.EntryLog.DeleteEvent(eventId);
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    ExecuteSystemError(false);
                    return;
                }
                var alarmCode = errorData.GetAbnormalCode();
                var alarmMessage = errorData.ToString();
                switch (alarmCode)
                {
                    case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.BLACK_LIST);
                        alarmMessage = KZUIStyles.CurrentResources.BlackedList;
                        ExecuteUnvalidEvent(accessKey, registeredVehicle, collection, images, plateNumber, DateTime.Now, alarmMessage, ce);
                        errorData.Payload ??= [];
                        if (errorData.Payload.TryGetValue("Blacklisted", out object? blackListObj) && blackListObj is JObject jObject)
                        {
                            BlackedList? blackList = jObject.ToObject<BlackedList>();
                            if (blackList != null)
                            {
                                alarmMessage = blackList.Note ?? "";
                            }
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmMessage, alarmCode, this.UcCameraList, accessKey?.Id);
                        return;
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        alarmMessage = accessKey.Name + " - " + alarmMessage;
                        ExecuteUnvalidEvent(accessKey, registeredVehicle, collection, images, plateNumber, DateTime.Now, alarmMessage, ce);
                        alarmMessage = $"{KZUIStyles.CurrentResources.AccesskeyName}: " + accessKey!.Name + $" - {KZUIStyles.CurrentResources.AccesskeyCode}: " + accessKey!.Code + " - " + (accessKey.Note ?? "");
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmMessage, alarmCode, this.UcCameraList, accessKey?.Id);
                        return;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        var eventOutData = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), Lane.Id, checkInAccessKeyId, standardlizePlateNumber, collection.Id);
                        if (eventOutData == null)
                        {
                            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                            ExecuteSystemError(true);
                            return;
                        }
                        errorData = eventOutData.Item2;
                        if (errorData != null)
                        {
                            alarmMessage = errorData.ToString();
                            ExecuteUnvalidEvent(accessKey, registeredVehicle, collection, images, plateNumber, DateTime.Now, alarmMessage, ce);
                            return;
                        }
                        await ExecuteVIP_VEHICLE_CardEvent(vipCardValidate, baseCardEventLog, accessKey, collection, ce, images, eventId);
                        return;
                    default:
                        ExecuteUnvalidEvent(accessKey, null, accessKey.Collection!, images, plateNumber, DateTime.Now, alarmMessage, ce);
                        return;
                }
            }
            else
            {
                var alarmCodes = entryResponse.Item1!.GetAlarmCode();
                if (alarmCodes.Count > 0)
                {
                    string accessKeyId = registeredVehicle.Id;
                    string alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + registeredVehicle.Code;

                    foreach (EmAlarmCode alarmCode in alarmCodes)
                    {
                        if (alarmCode == EmAlarmCode.PLATE_NUMBER_BLACKLISTED)
                        {
                            var blackList = AppData.ApiServer.DataService.BlackListed.GetByCode(plateNumber.Replace(" ", "").Replace("-", "").Replace(".", ""));
                            if (blackList != null && blackList.Item1 != null)
                            {
                                alarmDescription = blackList.Item1.Note;
                            }
                        }
                        else
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + registeredVehicle.Code;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);
                    }

                    if (alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM) ||
                        alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_INVALID) ||
                        alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_BLACKLISTED))
                    {
                        confirmMessage = string.Join(Environment.NewLine, entryResponse.Item1.ToVI());
                    }
                    else
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                        ExecuteSystemError(false);
                        return;
                    }
                }
            }

            var entry = entryResponse.Item1!;
            bool isNeedDisplayLed = true;
            if (!string.IsNullOrEmpty(confirmMessage) || !entry.OpenBarrier)
            {
                isNeedDisplayLed = false;
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, accessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");

                DisplayEventImage(images, plateNumber);
                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForConfirm);

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string displayAlarmMessage = string.IsNullOrEmpty(confirmMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : confirmMessage;
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Plate Request"));
                ConfirmInRequest request = new()
                {
                    AbnormalCode = EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM,
                    Message = displayAlarmMessage,
                    AccessKey = accessKey,
                    PlateNumber = standardlizePlateNumber,
                    RegisterPlate = registeredVehicle?.Code ?? "",
                    Images = images
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                bool isConfirm = result?.IsConfirm ?? false;
                plateNumber = result?.UpdatePlate ?? "";

                if (!isConfirm)
                {
                    this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.WaitForDelete);
                    await ExecuteNotConfirmEntry(entry, ce, displayAlarmMessage, baseCardEventLog);
                    return;
                }
                else
                {
                    _ = AppData.ApiServer.OperatorService.Entry.UpdatePlateAsync(entry.Id, result.UpdatePlate.ToUpper(), entry.PlateNumber);
                    UcPlateIn.DisplayNewPlate(result.UpdatePlate.ToUpper());
                    entry.PlateNumber = result.UpdatePlate.ToUpper();
                }
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Force Request"));
            }
            lastEvent = entry;
            this.EntryLog.UpdateEvent(eventId, EmEntryLocalDataLogStatus.Entry);
            ce.EventTime = DateTime.Now;

            bool isNeedDisplayImage = string.IsNullOrEmpty(confirmMessage) && entry.OpenBarrier;
            await ExcecuteValidEvent(accessKey, registeredVehicle, collection, images, ce.EventTime, lastEvent, ce.DeviceId, baseCardEventLog, isNeedDisplayImage, true, isNeedDisplayLed);
        }
        #endregion

        #endregion END Xử Lý Sự Kiện Quẹt Thẻ

        #region Xử Lý Sự Kiện Loop
        public async Task ExecuteInputEvent(InputEventArgs ie, string eventId)
        {
            switch (ie.InputType)
            {
                case EmInputType.Loop:
                    {
                        if (Lane.Loop)
                        {
                            if (AppData.AppConfig.LoopDelay > 0)
                            {
                                await Task.Delay(AppData.AppConfig.LoopDelay);
                            }
                            await ExecuteLoopEvent(ie, eventId);
                        }
                        break;
                    }
                case EmInputType.Exit:
                    await ExecuteExitEvent(ie, eventId);
                    break;
                case EmInputType.Alarm:
                    break;
                default:
                    break;
            }
        }

        #region EXIT EVENT
        public async Task ExecuteExitEvent(InputEventArgs ie, string eventId)
        {
            SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.Application, EmSystemActionDetail.EXIT_EVENT, EmSystemActionType.INFO, $"{this.Lane.Name}.Exit.{ie.InputIndex}"));
            Image? panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
            Image? faceImage = await UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
            Image? otherImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Other);
            var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
            
            ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() {
                { EmImageType.PANORAMA, panoramaImage},
                { EmImageType.VEHICLE, lprResult.VehicleImage},
                { EmImageType.FACE, faceImage},
                { EmImageType.OTHER, otherImage},
            });
            UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
            if (lastEvent == null || !this.isAllowOpenBarrieManual)
            {
                _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_BUTTON, this.UcCameraList, "");
            }
        }
        #endregion END EXIT EVENT

        #region LOOP EVENT
        public async Task ExecuteLoopEvent(InputEventArgs ie, string eventId)
        {
            string baseLoopLog = $"{this.Lane.Name}.Loop.{ie.InputIndex}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            LoopLprResult lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras);

            //Kiểm tra nếu xe này vừa vào bằng thẻ thì bỏ qua
            if (lprResult.Vehicle != null)
            {
                foreach (var cardEvent in this.LastCardEventDatas)
                {
                    var lastEntryData = lprResult.Vehicle.Metrics
                                        .Where(e => e.Code.Equals(cardEvent.PreferCard, StringComparison.CurrentCultureIgnoreCase))
                                        .FirstOrDefault();
                    if (lastEntryData is not null)
                    {
                        double v = Math.Abs((DateTime.Now - cardEvent.EventTime).TotalSeconds);
                        if (v < AppData.AppConfig.MinDelayCardTime)
                        {
                            this.ucEventRealtime.ShowErrorMessage($"{this.Lane.Name} - Phương tiện vừa vào bằng {lastEntryData.Name} trước đó. Thử lại sau {(int)(AppData.AppConfig.MinDelayCardTime - v)}s");
                            return;
                        }
                    }
                }
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - DISPLAY EVENT IMAGE"));
            Image? panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
            Image? faceImage = await UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
            Image? otherImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Other);
            lprResult.PanoramaImage = panoramaImage;

            Dictionary<EmImageType, List<List<byte>>> imageBytes = new()
            {
                { EmImageType.PANORAMA, [ImageHelper.GetByteArrayFromImage(panoramaImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.VEHICLE, [ImageHelper.GetByteArrayFromImage(lprResult.VehicleImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.PLATE_NUMBER, [ImageHelper.GetByteArrayFromImage(lprResult.LprImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.FACE, [ImageHelper.GetByteArrayFromImage(faceImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.OTHER, [ImageHelper.GetByteArrayFromImage(otherImage, ImageFormat.Jpeg)?.ToList()] },
            };
            Dictionary<EmImageType, Image?> images = new()
            {
                { EmImageType.PANORAMA, panoramaImage },
                { EmImageType.VEHICLE, lprResult.VehicleImage },
                { EmImageType.PLATE_NUMBER, lprResult.LprImage },
                { EmImageType.FACE, faceImage},
                { EmImageType.OTHER, otherImage },
            };
            _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
            ClearInfoView();

            if (this.laneOptionalConfig?.IsUseLoopImageForCardEvent ?? false)
            {
                lastLoopLprResult = lprResult;
                lastLoopLprResult.PanoramaImage = panoramaImage;
            }

            if (string.IsNullOrWhiteSpace(lprResult.PlateNumber) ||
                lprResult.PlateNumber.Length < 5 ||
                (lprResult.Vehicle == null && !this.laneOptionalConfig!.IsRegisterTurnVehicle))
            {
                ucEventImageListIn.DisplayImage(images);
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotFound);
                return;
            }

            if (lprResult.Vehicle is null)
            {
                ConfirmPlateRequest request = new()
                {
                    PlateNumber = lprResult.PlateNumber ?? "",
                    LprImage = lprResult.LprImage,
                };

                var result = await this.Invoke(async () => await MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, new UcRegisterPlate(this.laneOptionalConfig)));

                bool isConfirm = result != null && result.IsConfirm;
                if (!isConfirm)
                {
                    ucEventImageListIn.DisplayImage(images);
                    UcPlateIn.DisplayLprResult(lprResult.PlateNumber ?? "", lprResult.LprImage);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotFound);
                    return;
                }

                string updatePlate = result!.UpdatePlate;
                if (updatePlate == lprResult.PlateNumber)
                {
                    bool isRegisterSuccess = await RegisterDailyLoopVehicle(lprResult, baseLoopLog);
                    if (!isRegisterSuccess)
                    {
                        ucEventImageListIn.DisplayImage(images);
                        UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                        return;
                    }
                }
                else
                {
                    lprResult.PlateNumber = updatePlate;
                    UcPlateIn.DisplayNewPlate(updatePlate);

                    var registeredVehicle = (await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(updatePlate))?.Item1;
                    if (registeredVehicle != null && !string.IsNullOrEmpty(registeredVehicle.Id))
                    {
                        lprResult.Vehicle = registeredVehicle;
                    }
                    else
                    {
                        bool isRegisterSuccess = await RegisterDailyLoopVehicle(lprResult, baseLoopLog);
                        if (!isRegisterSuccess)
                        {
                            ucEventImageListIn.DisplayImage(images);
                            UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                            return;
                        }
                    }
                }
            }

            Collection collection = lprResult.Vehicle!.Collection!;
            var accessKeyType = collection.GetAccessKeyGroupType();
            bool isValidIdentityPlate = collection.GetEntryByLoop();

            if (!(collection?.GetActiveLanes()?.Contains(Lane.Id) ?? false))
            {
                ucEventImageListIn.DisplayImage(images);
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                string message = $"{collection?.Name} {KZUIStyles.CurrentResources.InvalidPermission}";
                UcResult.DisplayResult(EmResultType.ERROR, message);
                return;
            }

            if (lprResult.Vehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                ucEventImageListIn.DisplayImage(images);
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                return;
            }

            if (!isValidIdentityPlate && accessKeyType != EmAccessKeyGroupType.DAILY)
            {
                ucEventImageListIn.DisplayImage(images);
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotAllowEntryByPlate);
                return;
            }
            switch (accessKeyType)
            {
                case EmAccessKeyGroupType.DAILY:
                    await ExecuteDAILYLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                case EmAccessKeyGroupType.MONTHLY:
                    await ExecuteMONTHLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                case EmAccessKeyGroupType.VIP:
                    await ExecuteVIPLoopEvent(ie, lprResult, collection, images, eventId);
                    return;
                default:
                    break;
            }
        }

        //--LOOP - XE LƯỢT
        private async Task<bool> RegisterDailyLoopVehicle(LoopLprResult lprResult, string baseLoopLog)
        {
            // Xử lý quy trình của thẻ lượt
            //Đăng ký 1 phương tiện, có loại là loại được config, code = biển số nhận diện
            AccessKey accessKey = new()
            {
                Code = PlateHelper.StandardlizePlateNumber(lprResult.PlateNumber, true),
                Type = EmAccessKeyType.VEHICLE,
                Collection = new Collection() { Id = laneOptionalConfig!.TurnCollectionId },
                Name = "Temp_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss_ffff") + lprResult.PlateNumber,
            };
            lprResult.Vehicle = (await AppData.ApiServer.DataService.AccessKey.CreateAsync(accessKey)).Item1;
            if (lprResult.Vehicle == null)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotFound);
                return false;
            }
            lprResult.Vehicle.Collection = AppData.DailyAccessKeyCollections.Where(e => e.Id == laneOptionalConfig.TurnCollectionId).FirstOrDefault();
            return true;
        }
        private async Task ExecuteDAILYLoopEvent(GeneralEventArgs ie, LoopLprResult lprResult, Collection collection, Dictionary<EmImageType, Image?> images, string eventId)
        {
            string baseLoopLog = "";
            if (ie is InputEventArgs inputEvent)
            {
                baseLoopLog = $"{this.Lane.Name}.DAILY.Loop.{inputEvent.InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            }
            StopTimerCheckAllowOpenBarrie();

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - SEND CHECK IN NORMAL REQUEST"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, this.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            var checkInOutResponse = ValidateEntryResponse(eventInResponse, lprResult.PlateNumber, collection, images, null, lprResult.Vehicle, ie, true);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - CHECK EVENT IN NORMAL RESPONSE", checkInOutResponse));

            if (!checkInOutResponse.IsValidEvent && !checkInOutResponse.IsNeedConfirm)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Invalid Event, End Process", checkInOutResponse));
                return;
            }

            var entry = checkInOutResponse.EventData!;
            bool isNeedDisplayLed = true;
            if (!entry.OpenBarrier)
            {
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, entry.AccessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");
                isNeedDisplayLed = false;
                ucEventImageListIn.DisplayImage(images);
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string alarmMessage = KZUIStyles.CurrentResources.ProcessOpenBarrie;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Request {alarmMessage}"));
                ConfirmInRequest request = new()
                {
                    AbnormalCode = EmAlarmCode.NONE,
                    Message = alarmMessage,
                    AccessKey = lprResult.Vehicle,
                    PlateNumber = lprResult.PlateNumber ?? "",
                    RegisterPlate = lprResult.Vehicle?.Code ?? "",
                    Images = images,
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    await ExecuteNotConfirmEntry(entry, ie, alarmMessage, baseLoopLog);
                    return;
                }
            }

            lastEvent = entry;
            bool isNeedDisplayImage = entry.OpenBarrier;
            await ExcecuteValidEvent(null, lprResult.Vehicle, collection, images, ie.EventTime,
                                     entry, ie.DeviceId, baseLoopLog, isNeedDisplayImage, true, isNeedDisplayLed);
        }

        //--LOOP - XE THÁNG
        private async Task ExecuteMONTHLoopEvent(GeneralEventArgs ie, LoopLprResult lprResult, Collection collection, Dictionary<EmImageType, Image?> images, string eventId)
        {
            string baseLoopLog = "";
            if (ie is InputEventArgs inputEvent)
            {
                baseLoopLog = $"{this.Lane.Name}.MONTH.Loop.{inputEvent.InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START CHECK IN"));
            this.UcResult.DisplayResult(EmResultType.PROCESS, $"{KZUIStyles.CurrentResources.ProcessCheckIn} " + lprResult.PlateNumber);
            if (lprResult.Vehicle!.Status == EmAccessKeyStatus.LOCKED)
            {
                ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, lprResult.PanoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, null, lprResult.Vehicle.Collection, lprResult.Vehicle, 0, "",
                                                this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                return;
            }
            StopTimerCheckAllowOpenBarrie();

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - SEND CHECK IN NORMAL REQUEST"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, this.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            var checkInOutResponse = ValidateEntryResponse(eventInResponse, lprResult.PlateNumber, collection, images, null, lprResult.Vehicle, ie, true);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - CHECK EVENT IN NORMAL RESPONSE", checkInOutResponse));

            if (!checkInOutResponse.IsValidEvent && !checkInOutResponse.IsNeedConfirm) return;

            var entry = checkInOutResponse.EventData!;
            bool isNeedDisplayLed = true;
            if (!entry.OpenBarrier)
            {
                isNeedDisplayLed = false;
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, entry.AccessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");

                ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, lprResult.PanoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);

                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string confirmMessage = KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Request {confirmMessage}"));

                ConfirmInRequest request = new()
                {
                    AbnormalCode = EmAlarmCode.NONE,
                    Message = confirmMessage,
                    AccessKey = lprResult.Vehicle,
                    PlateNumber = lprResult.PlateNumber ?? "",
                    RegisterPlate = lprResult.Vehicle?.Code ?? "",
                    Images = images,
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    _ = ExecuteNotConfirmEntry(entry, ie, confirmMessage, baseLoopLog);
                    return;
                }
            }

            lastEvent = entry;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - Display Valid Event"));

            //Thêm thẻ gắn với xe vào danh sách chờ, tránh trường hợp sử dụng loop sau lại nhận thẻ xa
            foreach (AccessKey item in lprResult.Vehicle!.Metrics)
            {
                if (item.Type == EmAccessKeyType.CARD)
                {
                    foreach (var controlUnit in this.Lane.ControlUnits)
                    {
                        if (!controlUnit.Id.Equals(ie.DeviceId, StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }
                        foreach (var reader in controlUnit.Readers)
                        {
                            CardEventArgs ce = new()
                            {
                                DeviceId = ie.DeviceId,
                                PreferCard = item.Code,
                                AllCardFormats = [item.Code],
                                ReaderIndex = reader,
                                EventTime = DateTime.Now,
                            };
                            List<CardEventArgs> tempList = LastCardEventDatas;
                            bool isNewCardEvent = CardBaseProcess.CheckNewCardEvent(ce, out int thoiGianCho, ref tempList);
                            this.LastCardEventDatas = tempList;
                        }
                    }
                }
            }
            bool isNeedDisplayImage = entry.OpenBarrier;
            await ExcecuteValidEvent(null, lprResult.Vehicle, collection, images, ie.EventTime,
                                     entry, ie.DeviceId, baseLoopLog, isNeedDisplayImage, true, isNeedDisplayLed);
        }

        //--LOOP - XE VIP
        private async Task ExecuteVIPLoopEvent(GeneralEventArgs ie, LoopLprResult lprResult, Collection collection, Dictionary<EmImageType, Image?> images, string eventId)
        {
            string baseLoopLog = "";
            if (ie is InputEventArgs inputEvent)
            {
                baseLoopLog = $"{this.Lane.Name}.VIP.Loop.{inputEvent.InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            }
            StopTimerCheckAllowOpenBarrie();

            if (lprResult.Vehicle!.Status == EmAccessKeyStatus.LOCKED)
            {
                ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, lprResult.PanoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, null, lprResult.Vehicle.Collection, lprResult.Vehicle, 0, "",
                                                this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                return;
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Send Check In Normal Request"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            if (eventInResponse == null)
            {
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true);
                return;
            }

            var errorData = eventInResponse.Item2;
            //Nếu xe chưa vào bãi thì vào ra bth
            //Nếu xe đã vào bãi, thì ghi ra
            if (errorData != null)
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    ExecuteSystemError(false);
                    return;
                }
                var alarmCode = errorData.GetAbnormalCode();
                var alarmMessage = errorData.ToString();

                switch (alarmCode)
                {
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        alarmMessage = collection.Name + " - " + alarmMessage;
                        ExecuteUnvalidEvent(null, lprResult.Vehicle, collection, images, lprResult.PlateNumber,
                                            DateTime.Now, alarmMessage, ie);
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, this.UcCameraList, lprResult.Vehicle?.Id);
                        return;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        alarmMessage = collection.Name + " - " + alarmMessage;
                        ExecuteUnvalidEvent(null, lprResult.Vehicle, collection, images, lprResult.PlateNumber,
                                            DateTime.Now, alarmMessage, ie);
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, this.UcCameraList, lprResult.Vehicle?.Id);
                        return;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        {
                            var _controllerInLane = (from e in this.Lane.ControlUnits where e.Id == ie.DeviceId select e).FirstOrDefault();
                            var eventOutData = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
                            if (eventOutData == null)
                            {
                                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                                ExecuteSystemError(true);
                                return;
                            }
                            errorData = eventOutData.Item2;
                            if (errorData != null)
                            {
                                alarmCode = errorData.GetAbnormalCode();
                                alarmMessage = errorData.ToString();
                                ExecuteUnvalidEvent(null, lprResult.Vehicle, collection, images, lprResult.PlateNumber, DateTime.Now, alarmMessage, ie);
                                return;
                            }
                            eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(eventId, Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
                            if (eventInResponse == null || eventInResponse.Item2 != null)
                            {
                                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                                ExecuteSystemError(true);
                                return;
                            }
                            break;
                        }
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, this.UcCameraList, lprResult.Vehicle?.Id);
                        ExecuteUnvalidEvent(lprResult.Vehicle, null, lprResult.Vehicle!.Collection!, images, lprResult.PlateNumber,
                                            DateTime.Now, alarmMessage, ie);
                        return;
                }
            }

            var entry = eventInResponse.Item1!;
            bool isNeedDisplayLed = true;
            if (!entry.OpenBarrier)
            {
                isNeedDisplayLed = false;
                LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, entry.AccessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");

                ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, lprResult.PanoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string displayAlarmMessage = KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie;
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Plate Request"));
                ConfirmInRequest request = new()
                {
                    AbnormalCode = EmAlarmCode.NONE,
                    Message = displayAlarmMessage,
                    AccessKey = lprResult.Vehicle,
                    PlateNumber = lprResult.PlateNumber ?? "",
                    RegisterPlate = lprResult.Vehicle?.Code ?? "",
                    Images = images,
                };
                var result = await this.UIInvokeAsync(() => MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmIn));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    await ExecuteNotConfirmEntry(entry, ie, displayAlarmMessage, baseLoopLog);
                    return;
                }
            }

            lastEvent = entry;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - Display Valid Event"));
            bool isNeedDisplayImage = entry.OpenBarrier;
            await ExcecuteValidEvent(null, lprResult.Vehicle, collection, images, ie.EventTime,
                                     lastEvent, ie.DeviceId, baseLoopLog, isNeedDisplayImage, true, isNeedDisplayLed);
        }
        #endregion END LOOP EVENT
        #endregion

        #region Xử Lý Sự Kiện Người Dùng
        /// <summary>
        /// Kiểm tra phím tắt điều khiển controller
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task CheckControllerShortcutConfig(Keys key)
        {
            if (this.controllerShortcutConfigs == null)
                return;

            foreach (ControllerShortcutConfig controllerShortcutConfig in controllerShortcutConfigs)
            {
                foreach (var item in controllerShortcutConfig.KeySetByRelays)
                {
                    if (item.Value != (int)key)
                    {
                        continue;
                    }
                    string controllerId = controllerShortcutConfig.ControllerId;
                    int barrieIndex = item.Key;

                    iParkingv5.Controller.IController? controller = AppData.IControllers.FirstOrDefault(e => e.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase));
                    if (controller == null)
                    {
                        continue;
                    }

                    UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandOpenBarrie + barrieIndex);

                    await controller.OpenDoor(100, barrieIndex);

                    if (lastEvent != null && isAllowOpenBarrieManual)
                    {
                        continue;
                    }
                    var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
                    this.Invoke(new Action(async () =>
                    {
                        ucEventImageListIn.DisplayImage(new Dictionary<EmImageType, Image?>() {
                                { EmImageType.PANORAMA, await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama) },
                                { EmImageType.FACE, await UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) },
                                { EmImageType.OTHER, await UcCameraList.GetImageAsync(EmCameraPurpose.Other) },
                                { EmImageType.VEHICLE, lprResult.VehicleImage },
                        });
                        UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                    }));
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_KEYBOARD, this.UcCameraList, "");
                }
            }
        }

        /// <summary>
        /// Kiểm tra phím tắt điều khiển làn
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task CheckLaneInShortcutConfig(Keys key)
        {
            if (laneInShortcutConfig == null)
                return;
            if ((int)key == laneInShortcutConfig.ConfirmPlateKey && this.lastEvent != null)
            {
                await EditPlateOnUI();
            }
            else if ((int)key == laneInShortcutConfig.ReserveLane)
            {
                ReverseLane();
            }
            else if ((int)key == laneInShortcutConfig.WriteIn)
            {
                await WriteInClick(null);
            }
            else if ((int)key == laneInShortcutConfig.ReSnapshotKey)
            {
                await RetakeImageClick(null);
            }
        }
        private async Task EditPlateOnUI()
        {
            if (lastEvent == null)
            {
                return;
            }
            var result = await this.UcPlateIn.UpdatePlate(lastEvent.Id, AppData.ApiServer, true, lastEvent.PlateNumber);
            if (result == null) return;
            if (result == true)
            {
                UcResult.DisplayResult(EmResultType.SUCCESS, $"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.SuccessTitle}");
            }
            else
            {
                UcResult.DisplayResult(EmResultType.ERROR, $"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.ErrorTitle}, {KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.TryAgain}");
            }
            FocusOnTitle();
        }
        private void ReverseLane()
        {
            if (string.IsNullOrEmpty(this.Lane.ReverseLaneId?.ToString()))
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.InvalidReserverLaneConfig);
                return;
            }
            var config = GetLaneDisplayConfig();
            NewtonSoftHelper<LaneDisplayConfig>.SaveConfig(config, IparkingingPathManagement.appDisplayConfigPath(this.Lane.Id));
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandReserverLane);
            OnChangeLaneEventInvoke(this);
            return;
        }
        protected void OnChangeLaneEventInvoke(object e)
        {
            OnChangeLaneEvent?.Invoke(e);
        }
        #endregion

        private async Task ExecuteNotConfirmEntry(EntryData entry, GeneralEventArgs e, string alarmMessage, string baseLog)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Not Confirm {alarmMessage}"));
            UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleEntry);
            SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
            bool isDeleteSuccess = await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id.ToString());
            if (isDeleteSuccess)
            {
                this.EntryLog.DeleteEvent(entry.Id);
            }
        }
        public async Task ExecuteCancelEvent(CardCancelEventArgs ec)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Nhận sử kiện nuốt thẻ từ máy nhả thẻ"));
            if (lastEvent != null)
            {
                await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(lastEvent.Id.ToString());
                SoundHelper.PlaySound(ec.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_NHAN_NUT_LAY_THE);
            }
        }
        /// <summary>
        /// Sự kiện thẻ rút ra khỏi máy nhả thẻ
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        public async Task ExecuteCardbeTaken(CardBeTakenEventArgs ie)
        {
            if (!isAllowTakeCard)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Rút thẻ nhưng sự kiện trước không hợp lệ"));
                return;
            }
            this.isAllowTakeCard = false;
            ControllerInLane? controllerInLane = (from _controllerInLane in this.Lane.ControlUnits
                                                  where _controllerInLane.Id == ie.DeviceId
                                                  select _controllerInLane).FirstOrDefault();

            _ = ControllerHelper.OpenBarrieByControllerId(ie.DeviceId, this, null);
            await Task.Delay(1);
        }
        public async Task ExecuteEventError(ControllerErrorEventArgs error)
        {
            //string nameError = error.ErrorFunc.ToString();
            //// Cảnh báo với máy nhả thẻ
            //if (error.DispenserError != null)
            //{
            //    if (error.DispenserError.IsCardErrorDispenser)
            //    {
            //        // Play Sound nhả thẻ bị lỗi
            //        BaseLane.PlaySound(error.DeviceId, SoundType.EmSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY_VAO);
            //    }
            //    else if (error.DispenserError.IsCardEmptyDispenser)
            //    {
            //        // Play sound hết thẻ
            //        BaseLane.PlaySound(error.DeviceId, SoundType.EmSoundType.THIET_BI_HET_THE);
            //    }
            //    else if (error.DispenserError.IsLessCardDispenser)
            //    {
            //        // Play sound gần hết thẻ
            //    }
            //    await Task.Delay(100);
            //}
        }

        #region BASE EVENT
        private CheckEventResponse ValidateEntryResponse(Tuple<EntryData?, BaseErrorData?>? entryResponse, string plateNumber, Collection collection,
                                                         Dictionary<EmImageType, Image?> images,
                                                         AccessKey? accessKey, AccessKey? vehicle, GeneralEventArgs e, bool isPlate)
        {
            if (entryResponse == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true);
                return CheckEventResponse.CreateDefault();
            }
            if (entryResponse.Item1 == null && entryResponse.Item2 == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false);
                return CheckEventResponse.CreateDefault();
            }

            CheckEventResponse checkInOutResponse = new()
            {
                IsNeedConfirm = false,
                IsValidEvent = false,
                EventData = entryResponse.Item1,
                ErrorMessage = string.Empty,
                ErrorData = entryResponse.Item2,
            };

            var entryData = entryResponse.Item1;
            var errorData = entryResponse.Item2;

            string alarmDescription = "";

            if (errorData is null)
            {
                //Sự kiện hợp lệ
                if (entryData is not null && entryData.GetAlarmCode().Count == 0)
                {
                    checkInOutResponse.IsValidEvent = true;
                    checkInOutResponse.IsNeedConfirm = false;
                    return checkInOutResponse;
                }
                //Lỗi thì kiểm tra có bị cảnh báo biển số hay không
                else
                {
                    var alarmCodes = entryData!.GetAlarmCode();
                    string accessKeyId = "";
                    if (vehicle != null)
                    {
                        alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + vehicle.Code;
                        accessKeyId = vehicle.Id;
                    }
                    else if (accessKey != null)
                    {
                        alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + (accessKey.GetVehicleInfo()?.Code ?? "");
                        accessKeyId = accessKey.Id;
                    }
                    else
                    {
                        alarmDescription = "";
                    }
                    foreach (var alarmCode in alarmCodes)
                    {
                        if (alarmCode == EmAlarmCode.PLATE_NUMBER_BLACKLISTED)
                        {
                            var blackList = AppData.ApiServer.DataService.BlackListed.GetByCode(plateNumber.Replace(" ", "").Replace("-", "").Replace(".", ""));
                            if (blackList != null && blackList.Item1 != null)
                            {
                                alarmDescription = blackList.Item1.Note;
                            }
                        }
                        else
                        {
                            if (vehicle != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + vehicle.Code;
                                accessKeyId = vehicle.Id;
                            }
                            else if (accessKey != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + (accessKey.GetVehicleInfo()?.Code ?? "");
                                accessKeyId = accessKey.Id;
                            }
                            else
                            {
                                alarmDescription = "";
                            }
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);
                    }

                    if (alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM) ||
                       alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_INVALID) ||
                       alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_BLACKLISTED))
                    {
                        if (alarmCodes.Count == 1 && alarmCodes[0] == EmAlarmCode.PLATE_NUMBER_BLACKLISTED)
                        {
                            checkInOutResponse.IsNeedConfirm = false;
                            checkInOutResponse.IsValidEvent = true;
                            checkInOutResponse.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            checkInOutResponse.IsNeedConfirm = true;
                            checkInOutResponse.IsValidEvent = false;
                            checkInOutResponse.ErrorMessage = string.Join("\r\n", entryData.ToVI());
                        }
                    }
                    else
                    {
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                        checkInOutResponse.IsNeedConfirm = false;
                        checkInOutResponse.IsValidEvent = false;
                        ExecuteSystemError(false);
                    }
                    return checkInOutResponse;
                }
            }
            //Sự kiện lỗi, lấy thông tin lỗi và lưu lại cảnh báo
            else
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    ExecuteSystemError(false);
                    return checkInOutResponse;
                }

                var alarmCode = errorData.GetAbnormalCode();
                checkInOutResponse.ErrorMessage = errorData.ToString();

                switch (alarmCode)
                {
                    case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.BLACK_LIST);
                        ExecuteUnvalidEvent(accessKey, vehicle, collection, images, plateNumber, DateTime.Now, checkInOutResponse.ErrorMessage, e);

                        errorData.Payload ??= [];
                        if (errorData.Payload.TryGetValue("Blacklisted", out object? blackListObj) && blackListObj is JObject jObject)
                        {
                            BlackedList? blackList = jObject.ToObject<BlackedList>();
                            if (blackList != null)
                            {
                                alarmDescription = blackList.Note ?? "";
                            }
                        }
                        break;
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        if (accessKey != null)
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: " + accessKey.Name + $" - {KZUIStyles.CurrentResources.AccesskeyCode}: " + accessKey.Code + $" - {KZUIStyles.CurrentResources.AccesskeyNote}: " + (accessKey.Note ?? "");
                        }
                        else if (vehicle != null)
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: " + vehicle.Name + $" - {KZUIStyles.CurrentResources.VehicleCode}: " + vehicle.Code;
                        }
                        ExecuteUnvalidEvent(accessKey, vehicle, collection, images, plateNumber, DateTime.Now, checkInOutResponse.ErrorMessage, e);
                        break;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (vehicle != null)
                        {
                            if (vehicle.ExpireTime != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleExpiredDate}: " + vehicle.ExpireTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
                            }
                        }
                        ExecuteUnvalidEvent(accessKey, vehicle, collection, images, plateNumber, DateTime.Now, checkInOutResponse.ErrorMessage, e);
                        break;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.XE_DA_VAO_BAI);

                        errorData.Payload ??= [];
                        EntryData? eventInInfo = null;
                        if (errorData.Payload.ContainsKey("Entry"))
                        {
                            string? temp = errorData.Payload["Entry"]?.ToString();
                            if (!string.IsNullOrEmpty(temp))
                            {
                                try
                                {
                                    eventInInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<EntryData>(temp);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        if (eventInInfo == null) break;
                        alarmDescription = $"{KZUIStyles.CurrentResources.TimeIn}: " + ((EntryData)eventInInfo).DateTimeIn.ToString("HH:mm:ss dd/MM/yyyy");

                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventIn.{((EntryData)eventInInfo).Id}  - Xe đã vào bãi"));
                        if (((EntryData)eventInInfo).Images != null)
                        {
                            EventImageDto? overviewImgData = ((EntryData)eventInInfo).Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                            EventImageDto? vehicleImgData = ((EntryData)eventInInfo).Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
                            EventImageDto? lprImgData = ((EntryData)eventInInfo).Images.Where(e => e.Type == EmImageType.PLATE_NUMBER).FirstOrDefault();
                            EventImageDto? faceImgData = ((EntryData)eventInInfo).Images.Where(e => e.Type == EmImageType.FACE).FirstOrDefault();

                            ucEventImageListIn.DisplayImageData([overviewImgData, vehicleImgData, faceImgData]);
                            UcPlateIn.DisplayLprResultData(((EntryData)eventInInfo).PlateNumber ?? "", lprImgData);
                        }

                        AccessKey? registerVehicle = isPlate ? vehicle : accessKey?.GetVehicleInfo();

                        ExecuteUnvalidEvent(accessKey, registerVehicle, collection, images, plateNumber, ((EntryData)eventInInfo).DateTimeIn, checkInOutResponse.ErrorMessage, e);
                        break;
                    default:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                        ExecuteUnvalidEvent(accessKey, vehicle, collection, images, plateNumber, DateTime.Now, checkInOutResponse.ErrorMessage, e);
                        break;
                }

                string accessKeyId = accessKey?.Id ?? vehicle?.Id ?? "";
                _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);

                checkInOutResponse.IsNeedConfirm = false;
                checkInOutResponse.IsValidEvent = false;
                return checkInOutResponse;
            }
        }
        private void ExecuteSystemError(bool isDisconnectServer)
        {
            ClearEventImage();
            if (isDisconnectServer)
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ServerDisconnected);
            }
            else
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.SystemError);
            }
            LedHelper.DisplayDefaultLed(this.Lane.Id);
        }
        private void ExecuteUnvalidEvent(AccessKey? accessKey, AccessKey? vehicle, Collection collection, Dictionary<EmImageType, Image?> images, string plateNumber,
                                         DateTime eventTime, string errorMessage, GeneralEventArgs e)
        {
            DisplayEventImage(images, plateNumber);
            UcResult.DisplayResult(EmResultType.ERROR, errorMessage);
            ucEventInfoNew.DisplayEventInfo(eventTime, DateTime.MaxValue, accessKey, collection, vehicle, 0, "",
                                            laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
            LedHelper.DisplayDefaultLed(this.Lane.Id);
        }
        private async Task ExcecuteValidEvent(AccessKey? accessKey, AccessKey? registerVehicle, Collection collection,
                                              Dictionary<EmImageType, Image?> images, DateTime eventTime,
                                              EntryData entry, string deviceId, string baseLog, bool isNeedDisplayImage, bool isOpenBarrie, bool isNeedDisplayLed)
        {
            try
            {
                this.EntryLog.UpdateEvent(entry.Id, EmEntryLocalDataLogStatus.Entry);
                StartTimerCheckAllowOpenBarrie();
                isAllowOpenBarrieManual = true;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventIn.{entry.Id} - Save Image"));

                _ = Task.Run(new Action(async () =>
                   {
                       if (isNeedDisplayImage)
                       {
                           DisplayEventImage(images, entry.PlateNumber);
                       }
                       await LaneHelper.SaveLocalImage(images, entry.Id.ToString(), this.Lane.Name);

                       _ = Task.Run(new Action(async () =>
                       {
                           var task1 = images.GetValueOrDefault(EmImageType.PANORAMA).GetByteArrayFromImageAsync();
                           var task2 = images.GetValueOrDefault(EmImageType.VEHICLE).GetByteArrayFromImageAsync();
                           var task3 = images.GetValueOrDefault(EmImageType.PLATE_NUMBER).GetByteArrayFromImageAsync();
                           var task4 = images.GetValueOrDefault(EmImageType.FACE).GetByteArrayFromImageAsync();
                           await Task.WhenAll(task1, task2, task3, task4);

                           var imageDatas = new Dictionary<EmImageType, List<List<byte>>>
                           {
                                { EmImageType.PANORAMA, new List<List<byte>>(){ task1.Result } },
                                { EmImageType.VEHICLE,new List<List<byte>>(){ task2.Result } },
                                { EmImageType.PLATE_NUMBER, new List<List<byte>>(){ task3.Result } },
                                { EmImageType.FACE, new List<List<byte>>(){ task4.Result } }
                           };

                           _ = LaneHelper.SaveImage(imageDatas, true, entry.Id.ToString());
                           foreach (KeyValuePair<EmImageType, Image?> item in images)
                           {
                               if (item.Value is null)
                               {
                                   continue;
                               }
                               item.Value.Dispose();
                           }
                       }));
                   }));

                _ = Task.Run(new Action(async () =>
                 {
                     if (isOpenBarrie)
                     {
                         UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessOpenBarrie);
                         await ControllerHelper.OpenBarrie(this, collection, deviceId, baseLog);
                     }

                     _ = Task.Run(new Action(() =>
                     {
                         if (isNeedDisplayLed)
                         {
                             SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventIn.{entry.Id} - Display Led"));
                             LedHelper.DisplayLed(entry.PlateNumber, eventTime, accessKey, KZUIStyles.CurrentResources.Welcome, this.Lane.Id, "0");
                         }
                         else
                         {
                             SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventIn.{entry.Id} - Display Default Led"));
                             LedHelper.DisplayDefaultLed(this.Lane.Id);
                         }
                     }));
                     SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventIn.{entry.Id} - Display Event In Info"));
                     ucEventInfoNew.DisplayEventInfo(eventTime, DateTime.MaxValue, accessKey, collection, registerVehicle, 0, "",
                                                     laneDirectionConfig!.IsDisplayTitle, AppData.AppConfig.IsDisplayCustomerInfo);

                     lastEvent = entry;

                     SoundHelper.PlaySound(deviceId, EmSystemSoundType.XIN_MOI_QUA);
                     UcResult.DisplayResult(EmResultType.SUCCESS, KZUIStyles.CurrentResources.Welcome);
                 }));
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("EX002 - Display Event Info Error", ex));
            }
        }
        #endregion END BASE EVENT

        #endregion

        #region Khởi Tạo Làn
        public void InitUI()
        {
            ucEventInfoNew = new ucExitInfor(this.Lane);
            this.ucEventInfoNew = DataHelper.CreateDataInfor(EmViewOption.OnlyData, this.Lane);

            UcLaneTitle.Init(this.Lane, OpenBarrieClick, WriteInClick, RetakeImageClick, SettingClick);
            UcCameraList.Init(this, AppData.Cameras, AppData.AppConfig.IsUseVirtualLoop, AppData.AppConfig.VirtualLoopMode, AppData.AppConfig.MotionAlarmLevel, AppData.AppConfig.cameraSDk);
            UcCameraList.KZUI_Title = KZUIStyles.CurrentResources.CameraIn;

            UcPlateIn.Init(KZUIStyles.CurrentResources.PlateIn.ToUpper(), defaultImg);

            ucEventImageListIn.Init(KZUIStyles.CurrentResources.TitlePicIn,
                                    KZUIStyles.CurrentResources.TitlePanoramaIn,
                                    KZUIStyles.CurrentResources.TitleVehicleIn,
                                    KZUIStyles.CurrentResources.TitleFaceIn,
                                    KZUIStyles.CurrentResources.TitleOtherIn,
                                    defaultImg);

            UcAppFunctions.InitView(this.Lane, KZUIStyles.CurrentResources.OpenBarrie,
                                    KZUIStyles.CurrentResources.WriteIn,
                                    KZUIStyles.CurrentResources.RetakeImage,
                                    KZUIStyles.CurrentResources.Print, "Đ/K khách vào");
            UcAppFunctions.InitFunction(OpenBarrieClick, RetakeImageClick, WriteInClick, null, RegisterClientClick, CloseBarrieClick);
        }
        #endregion

        #region Timer
        private void TimerRefreshUI_Tick(object sender, EventArgs e)
        {
            timeRefresh++;
            if (timeRefresh >= AppData.AppConfig.TimeToDefautUI)
            {
                //MessageBox.Show("Làm mới giao diện");
                StopTimeRefreshUI();
                ClearInfoView();
                this.UcPlateIn.ClearView();
                this.ucEventImageListIn.ClearView();
            }
        }
        private void StopTimeRefreshUI()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(StopTimeRefreshUI);
            }
            timerRefreshUI.Enabled = false;
            timeRefresh = 0;
        }
        private void StartTimerRefreshUI()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(StartTimerRefreshUI);
                return;
            }
            if (AppData.AppConfig.TimeToDefautUI > 0)
            {
                timerRefreshUI.Enabled = true;
            }
        }

        private void StartTimerCheckAllowOpenBarrie()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(StartTimerCheckAllowOpenBarrie);
                return;
            }
            timerCheckAllowOpenBarrie.Enabled = true;
            allowOpenTime = 0;
        }
        private void StopTimerCheckAllowOpenBarrie()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(StopTimerCheckAllowOpenBarrie);
                return;
            }
            timerCheckAllowOpenBarrie.Enabled = false;
            allowOpenTime = 0;
        }
        private void TimerCheckAllowOpenBarrie_Tick(object sender, EventArgs e)
        {
            try
            {
                allowOpenTime++;
                if (allowOpenTime > AppData.AppConfig.AllowBarrieDelayOpenTime)
                {
                    timerCheckAllowOpenBarrie.Enabled = false;
                    this.isAllowOpenBarrieManual = false;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Implement
        public void ClearEventImage()
        {
            UcPlateIn.ClearView();
            ucEventImageListIn.ClearView();
        }
        private void ClearInfoView()
        {
            ucEventInfoNew.ClearView();
            UcResult.DisplayResult(EmResultType.PROCESS, AppData.OEMConfig.AppName);
            FocusOnTitle();
        }
        private void FocusOnTitle()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.BeginInvoke(FocusOnTitle);
                return;
            }
            try
            {
                if (UcLaneTitle.IsHandleCreated && UcLaneTitle.Visible && UcLaneTitle.Enabled)
                {
                    this.ActiveControl = UcLaneTitle;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Đóng Làn
        public virtual LaneDisplayConfig GetLaneDisplayConfig()
        {
            var laneDisplayConfig = new LaneDisplayConfig
            {
                splitterCamera_EventImageList = currentPosition.ContainsKey("splitterCamera_EventImageList")
                                                ? currentPosition["splitterCamera_EventImageList"] : 0,
                splitterImageList_Result = currentPosition.ContainsKey("splitterImageList_Result")
                                           ? currentPosition["splitterImageList_Result"] : 0,
                splitterResult_plate = currentPosition.ContainsKey("splitterResult_plate")
                                       ? currentPosition["splitterResult_plate"] : 0,
                splitterPlate_EventInfo = currentPosition.ContainsKey("splitterPlate_EventInfo")
                                          ? currentPosition["splitterPlate_EventInfo"] : 0,
                splitterEventInfo_Function = currentPosition.ContainsKey("splitterEventInfo_Function")
                                             ? currentPosition["splitterEventInfo_Function"] : 0
            };
            return laneDisplayConfig;
        }
        #endregion

        public virtual void ChangeLaneDirectionConfig(LaneDirectionConfig config) { }
        public virtual void LoadViewSetting(LaneDisplayConfig config)
        {
            this.laneDisplayConfig = config;
        }
        public virtual void AllowDesignRealtime(bool isAllow)
        {
            IsAllowDesignRealtime = isAllow;
            foreach (Splitter item in activeSpliters)
            {
                item.BackColor = IsAllowDesignRealtime ? Color.Red : Color.White;
                item.Enabled = IsAllowDesignRealtime;
            }
        }

        private void StartTimer()
        {
            timerClearLog = new System.Timers.Timer
            {
                Interval = 15000
            };
            timerClearLog.Elapsed += TimerClearLog_Tick;
            timerClearLog.Start();
        }
        private void TimerClearLog_Tick(object? sender, EventArgs e)
        {
            try
            {
                this.timerClearLog!.Enabled = false;
                this.EntryLog.ClearLogAfterDays(2);
                this.timerClearLog.Enabled = true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Clear EntryLog", ex, EmSystemActionType.ERROR));
            }
        }

        public void DisplayEventImage(Dictionary<EmImageType, Image?> images, string plateNumber)
        {
            UcPlateIn.DisplayLprResult(plateNumber, images.GetValueOrDefault(EmImageType.PLATE_NUMBER));
            ucEventImageListIn.DisplayImage(images);
        }
    }
}