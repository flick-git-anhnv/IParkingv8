using iParkingv5.Lpr;
using iParkingv5.Objects.Events;
using iParkingv8.Object;
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
using iParkingv8.Reporting;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.Forms.SystemForms;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using IParkingv8.Helpers.CardProcess;
using IParkingv8.QRScreenController;
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
using static Kztek.Control8.UserControls.KZUI_UcResult;
using static Kztek.Object.InputTupe;

namespace IParkingv8.LaneUIs
{
    public partial class UcBaseLaneOut : UserControl, ILaneOut
    {
        public EmControlSizeMode SizeMode = EmControlSizeMode.SMALL;
        public bool IsBusy { get; set; }
        System.Timers.Timer? timerClearLog;

        #region Properties
        public MaskedUserControl MaskedDialog;
        public IDialog<ConfirmOutRequest, ConfirmOutResult> dialogConfirmOut;
        private LoopLprResult lastLoopLprResult;
        private Dictionary<string, DateTime> motionPlateNumbers = new Dictionary<string, DateTime>();
        //Control Define
        public List<Splitter> activeSpliters = [];
        public KZUI_UcLaneTitle UcLaneTitle { get; set; } = new KZUI_UcLaneTitle();
        public KZUI_UcCameraList UcCameraList { get; set; } = new KZUI_UcCameraList();
        public IKZUIEventImageListOut UcEventImageListOut { get; set; } = new KZUI_UcImageListInOut();
        public KZUI_UcResult UcResult { get; set; } = new KZUI_UcResult();
        public IKZUI_UcPlate UcPlateIn { get; set; } = new KZUI_UcPlateVertical();
        public IKZUI_UcPlate UcPlateOut { get; set; } = new KZUI_UcPlateVertical();
        public IDataInfo ucEventInfoNew { get; set; }
        public KZUI_Function UcAppFunctions { get; set; } = new KZUI_Function();
        public tblExitLog ExitLog { get; set; }

        #region Config
        public LaneDisplayConfig laneDisplayConfig;
        public LaneOptionalConfig laneOptionalConfig;
        public LaneDirectionConfig laneDirectionConfig = new();
        private LaneOutShortcutConfig? LaneOutShortcutConfig;
        private List<ControllerShortcutConfig> controllerShortcutConfigs = [];
        #endregion

        //OTHER
        public bool IsAllowDesignRealtime = false;
        private bool isAllowOpenBarrieManual = false;

        public Dictionary<string, int> currentPosition = [];

        public ExitData? lastEvent = null;

        public Image? lastVehicleImage = null;
        public AccessKey? lastAccessKey = null;
        public Image? lastOverviewImage = null;

        public List<CardEventArgs> LastCardEventDatas { get; set; } = [];
        public List<InputEventArgs> LastInputEventDatas { get; set; } = [];

        public Lane Lane { get; set; }

        public readonly Image? defaultImg = ImageHelper.Base64ToImage(AppData.DefaultImageBase64);
        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new(1, 1);
        public readonly SemaphoreSlim semaphoreSlimOnKeyPress = new(1, 1);

        private int allowOpenTime = 0;
        int timeRefresh = 0;

        public event OnChangeLaneEvent? OnChangeLaneEvent;

        public IQRDevice? QrDevice;
        public UcSelectVehicles ucSelectVehicles { get; set; }
        KZUI_UcEventRealtime ucEventRealtime;
        #endregion

        #region Forms
        public UcBaseLaneOut() { }
        public UcBaseLaneOut(Lane lane, KZUI_UcEventRealtime ucEventRealtime)
        {
            InitializeComponent();
            this.Lane = lane;
            this.ExitLog = new tblExitLog(this.Lane.Name, IparkingingPathManagement.baseBath);

            LoadLaneConfig();
            InitUI();

            ucSelectVehicles = new(false)
            {
                TargetControl = this,
                BorderRadius = 24,
            };

            this.ucEventRealtime = ucEventRealtime;
            this.Load += FrmBaseLaneOutLoad;
        }
        private void FrmBaseLaneOutLoad(object? sender, EventArgs e)
        {
            ClearView();
            Task.Run(new Action(() =>
            {
                LedHelper.DisplayDefaultLed(this.Lane.Id);
            }));
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
            laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.Lane.Id)) ?? new LaneDirectionConfig();
            LaneOutShortcutConfig = NewtonSoftHelper<LaneOutShortcutConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneShortcutConfigPath(this.Lane.Id))
                                  ?? new LaneOutShortcutConfig();
            controllerShortcutConfigs = NewtonSoftHelper<List<ControllerShortcutConfig>>.DeserializeObjectFromPath(IparkingingPathManagement.laneControllerShortcutConfigPath(this.Lane.Id))
                                   ?? [];

            if (!string.IsNullOrEmpty(this.laneOptionalConfig.StaticQRComport))
            {
                Task.Run(new Action(async () =>
                {
                    if (this.QrDevice == null)
                    {
                        this.QrDevice = QrDeviceFactory.CreateQRDevice(laneOptionalConfig);
                    }
                    else
                    {
                        if (this.QrDevice.serialPort.PortName != this.laneOptionalConfig.StaticQRComport ||
                            this.QrDevice.serialPort.BaudRate != this.laneOptionalConfig.StaticQRBaudrate)
                        {
                            await this.QrDevice.CloseAsync();

                            this.QrDevice.serialPort.PortName = this.laneOptionalConfig.StaticQRComport;
                            this.QrDevice.serialPort.BaudRate = this.laneOptionalConfig.StaticQRBaudrate;

                            await this.QrDevice.OpenAsync();
                        }
                    }
                }));
            }
            else
            {
                if (this.QrDevice != null)
                {
                    Task.Run(new Action(async () =>
                    {
                        await this.QrDevice.CloseAsync();
                    }));
                }
            }

        }
        #endregion

        #region Đóng Làn
        public virtual LaneDisplayConfig GetLaneDisplayConfig()
        {
            var laneDisplayConfig = new LaneDisplayConfig();
            laneDisplayConfig.splitterCamera_EventImageList = currentPosition.ContainsKey("splitterCamera_EventImageList")
                                                                   ? currentPosition["splitterCamera_EventImageList"] : 0;
            laneDisplayConfig.splitterCamera_EventImageList = currentPosition.ContainsKey("splitterImageList_Result ")
                                                          ? currentPosition["splitterImageList_Result "] : 0;
            laneDisplayConfig.splitterCamera_EventImageList = currentPosition.ContainsKey("splitterResult_plate ")
                                                          ? currentPosition["splitterResult_plate "] : 0;
            laneDisplayConfig.splitterCamera_EventImageList = currentPosition.ContainsKey("splitterPlate_EventInfo ")
                                                          ? currentPosition["splitterPlate_EventInfo"] : 0;
            laneDisplayConfig.splitterCamera_EventImageList = currentPosition.ContainsKey("splitterEventInfo_Function")
                                                          ? currentPosition["splitterEventInfo_Function"] : 0;
            return laneDisplayConfig;
        }
        #endregion

        #region Controls In Forms
        public bool SettingClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = this.Lane.Name, Description = $"User Click To Open Setting Screen" });
            if (AppData.IsNeedToConfirmPassword)
            {
                var frm = new FrmConfirmPassword();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                AppData.IsNeedToConfirmPassword = false;
            }

            new FrmLaneSetting(this, AppData.Leds, AppData.Cameras, this.Lane.ControlUnits, false, this.laneOptionalConfig, this.SizeMode, this.Size).ShowDialog();
            laneOptionalConfig = NewtonSoftHelper<LaneOptionalConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneOptionalConfig(this.Lane.Id)) ?? new LaneOptionalConfig();
            laneDirectionConfig = NewtonSoftHelper<LaneDirectionConfig>.DeserializeObjectFromPath(IparkingingPathManagement.appLaneDirectionConfigPath(this.Lane.Id)) ?? new LaneDirectionConfig();
            laneDirectionConfig ??= this.SizeMode switch
            {
                EmControlSizeMode.SMALL => LaneDirectionConfig.CreateDefaultOutSmallConfig(),
                _ => LaneDirectionConfig.CreateDefaultOutConfig(),
            };

            LaneOutShortcutConfig = NewtonSoftHelper<LaneOutShortcutConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneShortcutConfigPath(this.Lane.Id))
                                  ?? new LaneOutShortcutConfig();
            controllerShortcutConfigs = NewtonSoftHelper<List<ControllerShortcutConfig>>.DeserializeObjectFromPath(IparkingingPathManagement.laneControllerShortcutConfigPath(this.Lane.Id))
                                   ?? [];

            if (!string.IsNullOrEmpty(this.laneOptionalConfig.StaticQRComport))
            {
                Task.Run(new Action(async () =>
                {
                    if (this.QrDevice == null)
                    {
                        this.QrDevice = QrDeviceFactory.CreateQRDevice(laneOptionalConfig);
                    }
                    else
                    {
                        if (this.QrDevice.serialPort.PortName != this.laneOptionalConfig.StaticQRComport ||
                            this.QrDevice.serialPort.BaudRate != this.laneOptionalConfig.StaticQRBaudrate)
                        {
                            await this.QrDevice.CloseAsync();
                            this.QrDevice.serialPort.PortName = this.laneOptionalConfig.StaticQRComport;
                            this.QrDevice.serialPort.BaudRate = this.laneOptionalConfig.StaticQRBaudrate;

                            await this.QrDevice.OpenAsync();
                        }
                    }

                }));
            }
            else
            {
                if (this.QrDevice != null)
                {
                    Task.Run(new Action(async () =>
                    {
                        await this.QrDevice.CloseAsync();
                    }));
                }
            }

            this.ChangeLaneDirectionConfig(laneDirectionConfig);
            this.dialogConfirmOut.LaneOptionalConfig = laneOptionalConfig;
            return true;
        }
        public async Task<bool> OpenBarrieClick(object? sender)
        {
            ClearView();
            UcEventImageListOut.ClearView();
            UcPlateIn.ClearView();
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = $"{this.Lane.Name} User click to open barrie" });
            UcResult.DisplayResult(EmResultType.PROCESS, $"{DateTime.Now:HH:mm:ss} - {KZUIStyles.CurrentResources.CustomerCommandOpenBarrie}");
            _ = ControllerHelper.OpenAllBarrie(this);

            if (lastEvent == null || !isAllowOpenBarrieManual)
            {
                var taskLpr = LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
                var taskGetPanoramaImage = UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
                var taskFaceImage = UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
                var taskOtherImage = UcCameraList.GetImageAsync(EmCameraPurpose.Other);
                await Task.WhenAll(taskLpr, taskGetPanoramaImage, taskFaceImage, taskOtherImage);

                var panoramaImage = taskGetPanoramaImage.Result;
                var faceImage = taskGetPanoramaImage.Result;
                var otherImage = taskGetPanoramaImage.Result;
                var lprResult = taskLpr.Result;
                _ = Task.Run(new Action(() =>
                {
                    UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>() {
                        { EmImageType.PANORAMA, panoramaImage },
                        { EmImageType.VEHICLE, lprResult.VehicleImage },
                        { EmImageType.FACE, faceImage },
                        { EmImageType.OTHER, otherImage },
                    });
                    UcPlateOut.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                }));

                _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_KEYBOARD, this.UcCameraList, "");
            }
            return true;
        }

        public async Task<bool> CloseBarrieClick(object? sender)
        {
            ClearView();
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
        public async Task<bool> WriteOutClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = this.Lane.Name, Description = $"User Click To Open Write Out Screen" });
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandWriteOut);

            FrmReportIn frm = new(defaultImg, AppData.ApiServer, AppData.Lanes, true);

            if (frm.ShowDialog() != DialogResult.OK)
            {
                UcResult.DisplayResult(EmResultType.PROCESS, AppData.OEMConfig.AppName);
                return false;
            }

            AccessKey? identity = (await AppData.ApiServer.DataService.AccessKey.GetById(frm.selectedIdentityId))?.Item1;
            if (identity == null)
            {
                return false;
            }

            if (identity.Type == EmAccessKeyType.VEHICLE)
            {
                var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras);
                Image? panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
                Image? faceImage = await UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
                Image? otherImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Other);
                var images = new Dictionary<EmImageType, Image?>()
                {
                    {EmImageType.PANORAMA, panoramaImage },
                    {EmImageType.VEHICLE, lprResult.VehicleImage },
                    {EmImageType.PLATE_NUMBER, lprResult.LprImage },
                    {EmImageType.FACE, faceImage},
                    {EmImageType.OTHER, otherImage},
                };
                UcEventImageListOut.DisplayExitImage(images);
                UcPlateOut.DisplayLprResult(lprResult?.PlateNumber ?? "", lprResult?.LprImage);
                var eventOutData = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), this.Lane.Id, identity.Id, lprResult?.PlateNumber ?? "", identity.Collection.Id);
                if (eventOutData != null && eventOutData.Item1 != null)
                {
                    lastEvent = eventOutData.Item1;
                    await ExcecuteValidEvent(null, lprResult?.Vehicle, lprResult?.PlateNumber ?? "",
                                             images, lastEvent, new GeneralEventArgs(), false, true, false);
                    if (lastEvent != null)
                    {
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, lastEvent.PlateNumber, identity.Name, EmAlarmCode.ENTRY_CREATED_MANUALLY, this.UcCameraList, identity.Id);
                    }
                }
                else
                {
                    this.UcResult.DisplayResult(EmResultType.ERROR, eventOutData.Item2?.ToString() ?? "");
                }
            }
            else
            {
                foreach (ControllerInLane controllerInLane in Lane.ControlUnits)
                {
                    if (controllerInLane.Readers.Count == 0)
                    {
                        continue;
                    }

                    CardEventArgs ce = new()
                    {
                        EventTime = DateTime.Now,
                        DeviceId = controllerInLane.Id,
                        ReaderIndex = controllerInLane.Readers[0],
                        AllCardFormats = [identity.Code],
                        PreferCard = identity.Code,
                        DeviceType = EmParkingControllerType.NomalController

                    };

                    await OnNewEvent(ce);

                    if (lastEvent != null)
                    {
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, lastEvent.PlateNumber, identity.Name,
                                                        EmAlarmCode.ENTRY_CREATED_MANUALLY,
                                                        this.UcCameraList, identity.Id);
                    }
                    break;
                }
            }

            return true;
        }
        public async Task<bool> RetakeImageClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = this.Lane.Name, Description = $"User Click To Take Photo" });
            if (!Lane.Loop || Lane.ControlUnits == null)
            {
                return false;
            }

            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandCapture);
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
                _ = OnNewEvent(ie);
                return true;
            }
            return true;
        }
        private async Task<bool> PrintClick(object? sender)
        {
            SystemUtils.logger.SaveUserLog(new UserLog() { Action = this.Lane.Name, Description = $"User Click To Btn Print Ticket" });
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandPrintTicket);
            FocusOnTitle();
            if (lastEvent == null)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.InvalidEventInfo,
                                KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string printTemplatePath = IparkingingPathManagement.appPrintTemplateConfigPath(((EmPrintTemplate)AppData.AppConfig.PrintTemplate).ToString());
            if (!File.Exists(printTemplatePath))
            {
                MessageBox.Show(KZUIStyles.CurrentResources.InvalidPrintTemplate,
                                KZUIStyles.CurrentResources.ErrorTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            long fee = lastEvent.Amount - lastEvent.Entry.Amount - lastEvent.DiscountAmount;
            fee = Math.Max(fee, 0);
            AppData.Printer.PrintPhieuThu(File.ReadAllText(printTemplatePath), lastEvent.AccessKey.Name, lastEvent.AccessKey.Collection.Name, null,
                                                  lastEvent.Entry.DateTimeIn, lastEvent.DatetimeOut,
                                                  lastEvent.PlateNumber, TextFormatingTool.GetMoneyFormat(fee.ToString()), lastEvent.Amount);
            return true;
        }
        public async Task<bool> RegisterVehicleClick(object? sender)
        {
            return true;
        }
        #endregion

        #region Event
        private bool isCheckMotionEvent = false;
        public async void OnKeyPress(Keys keys)
        {
            await semaphoreSlimOnKeyPress.WaitAsync();
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"KeyPress {keys}"));
            try
            {
                if (keys == Keys.F9)
                {
                    this.AllowDesignRealtime(!IsAllowDesignRealtime);
                }

                await CheckLaneOutShortcutConfig(keys);
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

        private bool isExecuteCardEvent = false;
        public async Task OnNewEvent(EventArgs e)
        {
            if (e is MotionDetectEventArgs && isCheckMotionEvent)
            {
                return;
            }
            if (e is CardEventArgs ce)
            {
                if (isExecuteCardEvent)
                {
                    return;
                }
            }
            await semaphoreSlimOnNewEvent.WaitAsync();
            this.IsBusy = true;
            StopTimeRefreshUI();
            Stopwatch stw = Stopwatch.StartNew();

            try
            {
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
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Card Event", cardEvent));
                    this.UcLaneTitle.NotifyCardEvent();
                    if (AppData.AppConfig.CardTakeImageDelay > 0)
                    {
                        await Task.Delay(AppData.AppConfig.CardTakeImageDelay);
                    }
                    await ExcecuteCardEvent(cardEvent);
                }
                else if (e is InputEventArgs inputEvent)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Input Event", inputEvent));
                    this.UcLaneTitle.NotifyLoopEvent();
                    await ExecuteInputEvent(inputEvent);
                }
                //else if(e is CardBeTakenEventArgs cardBeTakenEvent)
                //{
                //    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Input CardBeTakenEvent", cardBeTakenEvent));
                //    this.UcLaneTitle.NotifyLoopEvent();
                //    await ExecuteInputEvent(cardBeTakenEvent);
                //}
                else if (e is MotionDetectEventArgs motionDetectEvent)
                {
                    isCheckMotionEvent = true;
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Motion Event"));
                    this.UcLaneTitle.NotifyMotionEvent();
                    await ExecuteMotionEvent(motionDetectEvent);
                    isCheckMotionEvent = false;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("OnNewEventOut Error", ex));
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
        /// <summary>
        /// Sử dụng cho máy nhả thẻ có nút yêu cầu hỗ trợ
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task OnNewStatus(EventArgs e)
        {
        }

        #region Xử Lý Sự Kiện Quẹt Thẻ
        public async Task ExcecuteCardEvent(CardEventArgs ce)
        {
            string baseLog = $"{this.Lane.Name}.CARD.{ce.PreferCard}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - START"));

            isAllowOpenBarrieManual = false;
            lastEvent = null;
            ClearView();

            UcResult.DisplayResult(EmResultType.PROCESS, $"{KZUIStyles.CurrentResources.ProcessChecking}: " + ce.PreferCard);

            #region Kiểm tra thông tin định danh
            var cardValidate = await CardBaseProcess.ValidateAccessKeyByCode(this.Lane, ce, baseLog);
            _ = AlarmProcess.InvokeCardValidateAsync(cardValidate, this.Lane, this.UcCameraList);
            SoundHelper.InvokeCardValidate(cardValidate, ce.DeviceId);

            if (cardValidate.CardValidateType != EmAlarmCode.NONE)
            {
                _ = ControllerHelper.RejectCard(this, ce.DeviceId);

                var data = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, UcCameraList.IsValidCarCamera(), lastLoopLprResult);
                this.UcPlateIn.ClearView();
                this.UcEventImageListOut.DisplayEntryImage(new Dictionary<EmImageType, Image?>()
                {
                    {EmImageType.PANORAMA, null },
                    {EmImageType.VEHICLE, null },
                });
                this.UcPlateOut.DisplayLprResult(data.PlateNumber, data.LprImage);
                this.UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>()
                {
                    {EmImageType.PANORAMA, data.PanoramaImage },
                    {EmImageType.VEHICLE, data.VehicleImage },
                    {EmImageType.FACE ,await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID)}
                });

                UcResult.DisplayResult(EmResultType.ERROR, UIBuiltInResourcesHelper.GetValue(cardValidate.DisplayAlarmMessageTag));
                if (cardValidate.AccessKey == null)
                {
                    ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, cardValidate.AccessKey, null, null, 0, "", true, true);
                }
                else
                {
                    ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, cardValidate.AccessKey, cardValidate.AccessKey.Collection, null, 0, null,
                                                    laneDirectionConfig.IsDisplayTitle, AppData.AppConfig.IsDisplayCustomerInfo);
                }
                return;
            }
            AccessKey accessKeyResponse = cardValidate.AccessKey!;
            #endregion

            if (ce.InputType == EmInputType.Card && ce.ReaderIndex == 2 && ce.DeviceType == EmParkingControllerType.Card_Dispenser)
            {
                if (accessKeyResponse.Collection.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                {
                    UcResult.DisplayResult(EmResultType.ERROR, "Vui lòng đưa thẻ vào khay nhả thẻ");
                    return;
                }
            }

            var collection = accessKeyResponse.Collection!;
            bool isCar = collection.GetVehicleType() == EmVehicleType.CAR;
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessReadingPlate);

            var result = await ProcessCardEvent.GetLPRInfo(Lane, baseLog, UcCameraList, isCar, lastLoopLprResult);

            string plateNumber = result.PlateNumber;
            Image? panoramaImage = result.PanoramaImage;
            Image? vehicleImage = result.VehicleImage;
            Image? lprImage = result.LprImage;
            Image? faceImage = await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);

            var images = new Dictionary<EmImageType, Image?>()
            {
                {EmImageType.PANORAMA, panoramaImage },
                {EmImageType.VEHICLE, vehicleImage },
                {EmImageType.PLATE_NUMBER, lprImage },
                {EmImageType.FACE, faceImage },
            };
            string exitId = Guid.NewGuid().ToString();
            this.ExitLog.InitEvent(exitId, accessKeyResponse.Id, this.Lane.Id, ce.DeviceId, plateNumber);

            ce.PlateNumber = plateNumber;
            string standardlizePlate = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            try
            {
                StopTimerCheckAllowOpenBarrie();
                var accessKeyGroupType = collection.GetAccessKeyGroupType();
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Start {accessKeyGroupType} Card Process"));

                switch (accessKeyGroupType)
                {
                    case EmAccessKeyGroupType.DAILY:
                        await ExecuteDAILYCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, images, exitId);
                        break;
                    case EmAccessKeyGroupType.MONTHLY:
                        await ExecuteMONTHCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, images, exitId);
                        break;
                    case EmAccessKeyGroupType.VIP:
                        await ExcecuteVIPCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, images, exitId);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                UcResult.DisplayResult(EmResultType.ERROR, $"{KZUIStyles.CurrentResources.SystemError}, {KZUIStyles.CurrentResources.TryAgain}");
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("ExcecuteCardEvent", ex));
                MessageBox.Show(ex.Message, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region DAILY CARD
        private async Task ExecuteDAILYCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlate,
                                                 CardEventArgs ce, Dictionary<EmImageType, Image?> images, string exitId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.DAILY.Card.{accessKey.Code}.{accessKey.Name}";
            UcResult.DisplayResult(EmResultType.PROCESS, $"{KZUIStyles.CurrentResources.ProcessCheckOut} " + accessKey.Name);
            this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.StartCheckOut);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, Lane.Id, accessKey.Id, standardlizePlate, collection.Id);
            var validateExitResponse = ValidateExitResponse(exitResponse, collection, plateNumber, images, accessKey, null, ce);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Check Out Normal Response", validateExitResponse));

            if (!validateExitResponse.IsValidEvent && !validateExitResponse.IsNeedConfirm)
            {
                this.ExitLog.DeleteEvent(exitId);
                _ = ControllerHelper.RejectCard(this, ce.DeviceId);
                return;
            }

            ExitData exit = validateExitResponse.EventData!;
            string errorMessage = validateExitResponse.ErrorMessage;

            bool isPayByQR = false;
            bool isDisplayLed = true;
            bool isNeedDisplayImage = true;
            if (!string.IsNullOrEmpty(errorMessage) || !exit.OpenBarrier)
            {
                isNeedDisplayImage = false;
                isDisplayLed = false;
                long fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                fee = Math.Max(fee, 0);
                LedHelper.DisplayLed(exit.PlateNumber, exit.DatetimeOut, accessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());

                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForConfirm);

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                DisplayEventImage(plateNumber, images);

                var result = await ConfirmOpenBarrieOutResult(exit, errorMessage, validateExitResponse.AlarmCode, plateNumber, "", ce.DeviceId, images);

                if (result == null || !result.IsConfirm)
                {
                    this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForDelete);
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    bool isDeleteSuccess = await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);
                    if (isDeleteSuccess)
                    {
                        this.ExitLog.DeleteEvent(exitId);
                    }

                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleExit);
                    _ = ControllerHelper.RejectCard(this, ce.DeviceId);
                    return;
                }
                else
                {
                    isPayByQR = result.IsPayByQR;
                    exit = result.ExitData!;
                    exit.PlateNumber = result.UpdatePlate.ToUpper();
                    UcPlateOut.DisplayNewPlate(exit.PlateNumber);
                }
            }
            lastEvent = exit;
            if (lastEvent.Collection != null)
            {
                collection = lastEvent.Collection;
            }
            _ = Task.Run(new Action(async () =>
             {
                 await ControllerHelper.CollecCard(this, ce.DeviceId);
                 await ControllerHelper.OpenBarrie(this, collection, ce.DeviceId, baseCardEventLog);
             }));

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            ce.EventTime = DateTime.Now;

            await ExcecuteValidEvent(accessKey, null, plateNumber, images, exit, ce, isPayByQR, isDisplayLed, isNeedDisplayImage);
            if (accessKey.Name.Contains("Temp"))
            {
                await AppData.ApiServer.DataService.AccessKey.DeleteByIdAsync(accessKey.Id);
            }
        }
        #endregion END DAILY CARD

        #region MONTH CARD
        private async Task ExecuteMONTHCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlateNumber,
                                                 CardEventArgs ce, Dictionary<EmImageType, Image?> images, string exitId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.Card.MONTH.{accessKey.Code}.{accessKey.Name}";

            var monthCardValidate = await CardMonthProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, this, this.ucSelectVehicles);
            if (monthCardValidate.MonthCardValidateType != EmMonthCardValidateType.SUCCESS)
            {
                if (monthCardValidate.RegisterVehicle == null)
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle);
                    ucEventInfoNew.DisplayEventInfo(DateTime.Now, DateTime.MaxValue, accessKey, accessKey.Collection, null, 0, "",
                                                    this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, standardlizePlateNumber, "", EmAlarmCode.ACCESS_KEY_INVALID, this.UcCameraList, accessKey.Id);
                }
                else
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleExit);
                }
                this.ExitLog.DeleteEvent(exitId);
                return;
            }

            AccessKey? registeredVehicle = monthCardValidate.RegisterVehicle;
            plateNumber = monthCardValidate.UpdatePlate;
            standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);

            bool isCheckOutByVehicle = monthCardValidate.IsCheckByPlate;
            string checkOutAccessId = (isCheckOutByVehicle ? registeredVehicle!.Id : accessKey.Id) ?? "";
            UcResult.DisplayResult(EmResultType.PROCESS, $"{KZUIStyles.CurrentResources.ProcessCheckOut} " + accessKey.Name);
            this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.StartCheckOut);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);
            var validateExitResponse = ValidateExitResponse(exitResponse, collection, plateNumber, images, accessKey, registeredVehicle, ce);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Event Out Response", validateExitResponse));

            if (!validateExitResponse.IsValidEvent && !validateExitResponse.IsNeedConfirm)
            {
                return;
            }

            ExitData exit = validateExitResponse.EventData!;
            string errorMessage = validateExitResponse.ErrorMessage;
            bool isPayByQR = false;
            bool isDisplayLed = true;
            bool isNeedDisplayImage = true;
            if (!string.IsNullOrEmpty(errorMessage) || !exit.OpenBarrier)
            {
                isNeedDisplayImage = false;
                isDisplayLed = false;
                long fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                fee = Math.Max(fee, 0);
                LedHelper.DisplayLed(exit.PlateNumber, exit.DatetimeOut, accessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());

                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForConfirm);

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));

                DisplayEventImage(plateNumber, images);

                var result = await ConfirmOpenBarrieOutResult(exit, errorMessage, validateExitResponse.AlarmCode, plateNumber, registeredVehicle?.Code ?? "", ce.DeviceId, images);

                if (result == null || !result.IsConfirm)
                {
                    this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForDelete);
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    bool isDeleteSuccess = await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);
                    if (isDeleteSuccess)
                    {
                        this.ExitLog.DeleteEvent(exitId);
                    }

                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleExit);
                    await ControllerHelper.RejectCard(this, ce.DeviceId);
                    return;
                }
                else
                {
                    exit = result.ExitData!;
                    exit.PlateNumber = result.UpdatePlate.ToUpper();
                    UcPlateOut.DisplayNewPlate(exit.PlateNumber);
                    isPayByQR = result.IsPayByQR;
                }
            }

            lastEvent = exit;
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessOpenBarrie);
            if (lastEvent.Collection != null)
            {
                collection = lastEvent.Collection;
            }
            await ControllerHelper.OpenBarrie(this, collection, ce.DeviceId, baseCardEventLog);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            ce.EventTime = DateTime.Now;
            await ExcecuteValidEvent(accessKey, accessKey.GetVehicleInfo(), plateNumber, images, exit, ce, isPayByQR, isDisplayLed, isNeedDisplayImage);
        }
        #endregion END MONTH CARD

        #region VIP CARD
        private async Task ExcecuteVIPCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlateNumber,
                                                CardEventArgs ce, Dictionary<EmImageType, Image?> images, string exitId)
        {
            string baseCardEventLog = $"{this.Lane.Name}.Card.VIP.{accessKey.Code}.{accessKey.Name}";

            var vipCardValidate = await CardVipProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, this, this.ucSelectVehicles);
            if (vipCardValidate.VipCardValidateType != EmVipCardValidateType.SUCCESS)
            {
                if (vipCardValidate.RegisterVehicle == null)
                {
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.AccessKeyVipNoVehicle);
                    ucEventInfoNew.DisplayEventInfo(null, DateTime.Now, accessKey, accessKey.Collection, null, 0, "",
                                                    this.laneDirectionConfig?.IsDisplayTitle ?? true, AppData.AppConfig.IsDisplayCustomerInfo);
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, standardlizePlateNumber, "", EmAlarmCode.ACCESS_KEY_INVALID, this.UcCameraList, accessKey.Id);
                }
                this.ExitLog.DeleteEvent(exitId);
                return;
            }

            AccessKey? registeredVehicle = vipCardValidate.RegisterVehicle;
            plateNumber = vipCardValidate.UpdatePlate;
            standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            bool isCheckOutByVehicle = vipCardValidate.IsCheckByPlate;
            string checkOutAccessId = (isCheckOutByVehicle ? registeredVehicle!.Id : accessKey.Id) ?? "";

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.StartCheckOut);
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);

            if (exitResponse == null)
            {
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true);
                this.ExitLog.DeleteEvent(exitId);
                return;
            }
            if (exitResponse.Item1 == null && exitResponse.Item2 == null)
            {
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false);
                this.ExitLog.DeleteEvent(exitId);
                return;
            }

            var exitData = exitResponse.Item1;
            var errorData = exitResponse.Item2;
            string errorMessage = "";
            EmAlarmCode alarmCode = EmAlarmCode.NONE;
            string alarmDescription = "";

            //Sự kiện lỗi, lấy thông tin lỗi và lưu lại cảnh báo
            if (errorData is not null)
            {
                this.ExitLog.DeleteEvent(exitId);
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    ExecuteSystemError(false);
                    return;
                }

                alarmCode = errorData.GetAbnormalCode();
                errorMessage = errorData.ToString();

                switch (alarmCode)
                {
                    case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.BLACK_LIST);
                        ExcecuteUnvalidEvent(accessKey, registeredVehicle, collection,
                                             plateNumber, images,
                                             DateTime.Now, null, KZUIStyles.CurrentResources.BlackedList, ce);

                        errorData.Payload ??= [];
                        if (errorData.Payload.TryGetValue("Blacklisted", out object? blackListObj) && blackListObj is JObject jObject)
                        {
                            BlackedList? blackList = jObject.ToObject<BlackedList>();
                            if (blackList != null)
                            {
                                alarmDescription = blackList.Note ?? "";
                            }
                        }

                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKey?.Id ?? "");
                        return;
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        if (accessKey != null)
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: {accessKey.Name} - {KZUIStyles.CurrentResources.AccesskeyCode}: {accessKey.Code} - {KZUIStyles.CurrentResources.AccesskeyNote}: " + (accessKey.Note ?? "");
                        }
                        else if (registeredVehicle != null)
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: " + registeredVehicle.Name + $" - {KZUIStyles.CurrentResources.VehicleCode}: " + registeredVehicle.Code;
                        }
                        ExcecuteUnvalidEvent(accessKey, registeredVehicle, collection,
                                             plateNumber, images,
                                             DateTime.Now, null, errorMessage, ce);
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKey?.Id ?? "");
                        return;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (registeredVehicle != null)
                        {
                            if (registeredVehicle.ExpireTime != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleExpiredDate}: " + registeredVehicle.ExpireTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
                            }
                        }
                        ExcecuteUnvalidEvent(accessKey, registeredVehicle, collection, plateNumber, images, DateTime.Now, null, errorMessage, ce);
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKey?.Id ?? "");
                        return;
                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        //Tạo 1 sự kiện vào cho sự kiện này
                        var eventInData = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);

                        if (eventInData == null || eventInData.Item1 == null)
                        {
                            ExecuteSystemError(true);
                        }
                        else
                        {
                            await ExcecuteVIPCardEvent(accessKey, collection, plateNumber, standardlizePlateNumber, ce, images, exitId);
                        }
                        return;
                    default:
                        ExcecuteUnvalidEvent(accessKey, registeredVehicle, collection, plateNumber, images, DateTime.Now, null, errorMessage, ce);
                        return;
                }
            }
            //Không lỗi thì kiểm tra có bị cảnh báo biển số hay không
            else
            {
                var alarmCodes = exitData!.GetAlarmCode();
                var errorMessages = exitData.ToVI() ?? [];
                errorMessage = string.Join(Environment.NewLine, errorMessages);

                string accessKeyId = "";
                foreach (var _alarmCode in alarmCodes)
                {
                    switch (_alarmCode)
                    {
                        case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                            var blackList = AppData.ApiServer.DataService.BlackListed.GetByCode(plateNumber.Replace(" ", "").Replace("-", "").Replace(".", ""));
                            if (blackList != null && blackList.Item1 != null)
                            {
                                alarmDescription = blackList.Item1.Note;
                            }
                            break;
                        case EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM:
                            if (registeredVehicle != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + registeredVehicle.Code;
                                accessKeyId = registeredVehicle.Id;
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
                            break;
                        case EmAlarmCode.PLATE_NUMBER_INVALID:
                            alarmDescription = $"{KZUIStyles.CurrentResources.PlateIn}: " + (exitData.Entry?.PlateNumber ?? "");
                            if (registeredVehicle != null)
                            {
                                accessKeyId = registeredVehicle.Id;
                            }
                            else if (accessKey != null)
                            {
                                accessKeyId = accessKey.Id;
                            }
                            else
                            {
                                alarmDescription = "";
                            }
                            break;
                        default:
                            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                            ExecuteSystemError(false);
                            break;
                    }
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, plateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);

                }

            }

            ExitData exit = exitResponse.Item1!;
            bool isPayByQR = false;
            bool isDisplayLed = true;
            bool isNeedDisplayImage = true;

            if (!string.IsNullOrEmpty(errorMessage) || !exit.OpenBarrier)
            {
                isNeedDisplayImage = false;
                isDisplayLed = false;
                long fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                fee = Math.Max(fee, 0);
                LedHelper.DisplayLed(exit.PlateNumber, exit.DatetimeOut, accessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());

                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForConfirm);

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));

                DisplayEventImage(plateNumber, images);
                var result = await ConfirmOpenBarrieOutResult(exit, errorMessage, alarmCode, plateNumber, registeredVehicle?.Code ?? "", ce.DeviceId, images);

                if (result == null || !result.IsConfirm)
                {
                    this.ExitLog.UpdateEvent(exitId, EmExitLocalDataLogStatus.WaitForDelete);
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    bool isDeleteSuccess = await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);
                    if (isDeleteSuccess)
                    {
                        this.ExitLog.DeleteEvent(exitId);
                    }
                    UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleExit);
                    return;
                }
                else
                {
                    isPayByQR = result.IsPayByQR;
                    exit = result.ExitData!;
                    exit.PlateNumber = result.UpdatePlate.ToUpper();
                    UcPlateOut.DisplayNewPlate(exit.PlateNumber);
                }
            }

            lastEvent = exit;
            if (lastEvent.Collection != null)
            {
                collection = lastEvent.Collection;
            }
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessOpenBarrie);
            await ControllerHelper.OpenBarrie(this, collection, ce.DeviceId, baseCardEventLog);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            ce.EventTime = DateTime.Now;
            await ExcecuteValidEvent(accessKey, accessKey.GetVehicleInfo(), plateNumber, images, exit, ce, isPayByQR, isDisplayLed, isNeedDisplayImage);
        }
        #endregion END VIP CARD

        #endregion

        #region Xử Lý Sự Kiện Loop
        public async Task ExecuteInputEvent(InputEventArgs ie)
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
                            await ExecuteLoopEvent(ie);
                        }
                        break;
                    }
                case EmInputType.Exit:
                    await ExecuteExitEvent(ie);
                    break;
                case EmInputType.Alarm:
                    break;
                default:
                    break;
            }

        }
        public async Task ExecuteMotionEvent(MotionDetectEventArgs motionEventArgs)
        {
            ControllerInLane? controllerInLane = null;
            foreach (ControllerInLane item in this.Lane.ControlUnits)
            {
                if (item.Barriers.Count > 0)
                {
                    controllerInLane = item;
                    motionEventArgs.DeviceId = controllerInLane.Id;
                    break;
                }
            }

            string baseLoopLog = $"{this.Lane.Name}.MotionEvent";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent(baseLoopLog + "- START"));

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, isForceCar: false);
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
            if (string.IsNullOrEmpty(lprResult.PlateNumber) || lprResult.PlateNumber.Length < 5)
            {
                //this.ucEventRealtime.ShowErrorMessage($"{DateTime.Now:HH:mm:ss} - {this.Lane.Name} - {lprResult.PlateNumber} {KZUIStyles.CurrentResources.InvalidPlateNumber}");
                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                return;
            }

            if (lprResult?.Vehicle == null && !this.laneOptionalConfig!.IsRegisterTurnVehicle)
            {
                this.ucEventRealtime.ShowErrorMessage($"{DateTime.Now:HH:mm:ss} - {this.Lane.Name} - {lprResult.PlateNumber} {KZUIStyles.CurrentResources.VehicleNotFound}");
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                return;
            }

            if (motionPlateNumbers.ContainsKey(lprResult.PlateNumber))
            {
                if (Math.Abs((DateTime.Now - motionPlateNumbers[lprResult.PlateNumber]).TotalSeconds) <= 10)
                {
                    return;
                }
                else
                {
                    _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
                    motionPlateNumbers[lprResult.PlateNumber] = DateTime.Now;
                }
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
            ClearView();

            UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, panoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
            UcPlateOut.DisplayLprResult(lprResult.PlateNumber ?? "", lprResult.LprImage);

            //Không tìm thấy thông tin xe trong hệ thống
            if (lprResult.Vehicle == null)
            {
                //Hiển thị hộp thoại cập nhật biển
                ConfirmPlateRequest request = new()
                {
                    PlateNumber = lprResult?.PlateNumber ?? "",
                    LprImage = lprResult?.LprImage,
                };

                var result = await this.Invoke(async () => await MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, new UcRegisterPlate(this.laneOptionalConfig)));

                bool isConfirm = result != null && result.IsConfirm;
                if (!isConfirm)
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }

                string updatePlate = result!.UpdatePlate;
                if (updatePlate == lprResult.PlateNumber)
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }
                lprResult.PlateNumber = updatePlate;
                UcPlateOut.DisplayNewPlate(updatePlate);
                var registeredVehicle = (await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(updatePlate))?.Item1;
                if (registeredVehicle == null || string.IsNullOrEmpty(registeredVehicle.Id))
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }
                lprResult.Vehicle = registeredVehicle;
            }

            Collection collection = lprResult.Vehicle.Collection!;
            var collectionType = collection.GetAccessKeyGroupType();

            if (!(collection?.GetActiveLanes()?.Contains(Lane.Id) ?? false))
            {
                string message = $"{collection?.Name} {KZUIStyles.CurrentResources.InvalidPermission}";
                UcResult.DisplayResult(EmResultType.ERROR, message);
                return;
            }

            if (!collection.GetExitByLoop())
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotAllowExityByPlate);
                return;
            }
            if (lprResult.Vehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                return;
            }

            if (await AppData.ApiServer.DataService.RegisterVehicle.IsBlackListAsync(lprResult.PlateNumber))
            {
                //Save Black List Alarm
                if (AppData.AppConfig.RejectBlackListVehicle)
                {
                    return;
                }
            }

            string exitId = Guid.NewGuid().ToString();
            var images = new Dictionary<EmImageType, Image?>()
            {
                {EmImageType.PANORAMA,panoramaImage },
                {EmImageType.VEHICLE, lprResult.VehicleImage },
                {EmImageType.PLATE_NUMBER, lprResult.LprImage },
                {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) }
            };
            Tuple<ExitData, string>? checkOutResult = null;
            switch (collectionType)
            {
                case EmAccessKeyGroupType.MONTHLY:
                    checkOutResult = await ExecuteLoopMONTH(motionEventArgs, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                case EmAccessKeyGroupType.DAILY:
                    checkOutResult = await ExecuteLoopDAILY(motionEventArgs, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                case EmAccessKeyGroupType.VIP:
                    checkOutResult = await ExecuteLoopVIP(motionEventArgs, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                default:
                    break;
            }

            if (checkOutResult == null) return;

            var exit = checkOutResult.Item1;
            string errorMessage = checkOutResult.Item2;
            bool isPayByQR = false;
            bool isDisplayLed = true;

            if (!exit.OpenBarrier)
            {
                isDisplayLed = false;
                long fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                fee = Math.Max(fee, 0);
                LedHelper.DisplayLed(exit.PlateNumber, exit.DatetimeOut, exit.AccessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());

                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                var entry = exit.Entry!;

                EventImageDto? panoramaInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.PANORAMA);
                EventImageDto? vehicleInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.VEHICLE);
                var taskVehicleIn = ImageHelper.DownloadBitmapAsync(vehicleInDto?.PresignedUrl);
                var taskPanoramaIn = ImageHelper.DownloadBitmapAsync(panoramaInDto?.PresignedUrl);
                var taskVoucherApply = AppData.ApiServer.PaymentService.GetAppliedVoucherDataAsync(entry.Id);
                await Task.WhenAll(taskVehicleIn, taskPanoramaIn, taskVoucherApply);

                SoundHelper.PlaySound(motionEventArgs.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string alarmMessage = string.IsNullOrEmpty(errorMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : errorMessage;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Request {alarmMessage}"));
                ConfirmOutRequest request = new()
                {
                    //Thông tin chung
                    User = StaticPool.SelectedUser,
                    ErrorMessage = alarmMessage,
                    AbnormalCode = EmAlarmCode.SYSTEM_ERROR,
                    DetectedPlate = lprResult.Vehicle.Code,
                    AlarmPlate = lprResult.Vehicle.Code,
                    //Thông tin vào
                    EntryImages = new Dictionary<EmImageType, Image?>()
                    {
                        {EmImageType.PANORAMA, taskPanoramaIn.Result },
                        {EmImageType.VEHICLE, taskVehicleIn.Result },
                    },
                    VoucherApplies = taskVoucherApply.Result ?? [],
                    //Thông tin ra
                    EventOut = exit,
                    ExitImages = new Dictionary<EmImageType, Image?>()
                    {
                        {EmImageType.PANORAMA, panoramaImage },
                        {EmImageType.VEHICLE, lprResult.VehicleImage },
                    },
                };
                var result = await this.UIInvokeAsync(() =>
                MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmOut));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    await ExecuteNotConfirmExit(exit, motionEventArgs, alarmMessage, baseLoopLog);
                    return;
                }

                if (lprResult.Vehicle.Name.Contains("Temp"))
                    await AppData.ApiServer.DataService.AccessKey.DeleteByIdAsync(lprResult.Vehicle.Id);
                isPayByQR = result.IsPayByQR;
                exit = result.ExitData;
            }

            lastEvent = exit;
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessOpenBarrie);
            if (lastEvent.Collection != null)
            {
                collection = lastEvent.Collection;
            }
            await ControllerHelper.OpenBarrie(this, collection, motionEventArgs.DeviceId, baseLoopLog);
            await ExcecuteValidEvent(null, lprResult.Vehicle, lprResult.PlateNumber,
                                    images, exit, motionEventArgs, isPayByQR, isDisplayLed, true);
        }

        #region EXIT EVENT
        public async Task ExecuteExitEvent(InputEventArgs ie)
        {
            SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.Application, EmSystemActionDetail.EXIT_EVENT, EmSystemActionType.INFO, $"{this.Lane.Name}.Exit.{ie.InputIndex}"));

            //_ = ControllerHelper.OpenAllBarrie(this);

            if (lastEvent == null || !isAllowOpenBarrieManual)
            {
                var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
                var panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
                this.Invoke(new Action(() =>
                {
                    UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, panoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                    UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                }));
                _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_BUTTON, this.UcCameraList, "");
            }
        }
        #endregion END EXIT EVENT

        #region LOOP EVENT
        public async Task ExecuteLoopEvent(GeneralEventArgs ie)
        {
            string baseLoopLog = "";
            if (ie is InputEventArgs)
            {
                baseLoopLog = $"{this.Lane.Name}.Loop.{((InputEventArgs)ie).InputIndex}";
            }
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            //UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessReadingPlate);
            var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras);
            //Kiểm tra nếu xe này vừa vào bằng thẻ thì bỏ qua
            if (lprResult != null && lprResult.Vehicle != null)
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
            if (this.laneOptionalConfig?.IsUseLoopImageForCardEvent ?? false)
            {
                lastLoopLprResult = lprResult;
                lastLoopLprResult.PanoramaImage = panoramaImage;
            }

            ClearView();
            UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, panoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
            UcPlateOut.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
            _ = AlarmProcess.SaveVehicleOnLoopEventAsync(Lane, imageBytes, lprResult.PlateNumber);
            if (string.IsNullOrEmpty(lprResult.PlateNumber) ||
                lprResult.PlateNumber.Length < 5 ||
                (lprResult.Vehicle == null && !this.laneOptionalConfig.IsRegisterTurnVehicle))
            {
                NotifyVehicleNotInSystem(baseLoopLog);
                return;
            }

            //Không tìm thấy thông tin xe trong hệ thống
            if (lprResult.Vehicle == null)
            {
                //Hiển thị hộp thoại cập nhật biển
                ConfirmPlateRequest request = new()
                {
                    PlateNumber = lprResult?.PlateNumber ?? "",
                    LprImage = lprResult?.LprImage,
                };

                var result = await this.Invoke(async () => await MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, new UcRegisterPlate(this.laneOptionalConfig)));

                bool isConfirm = result != null && result.IsConfirm;
                if (!isConfirm)
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }

                string updatePlate = result!.UpdatePlate;
                if (updatePlate == lprResult.PlateNumber)
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }
                lprResult.PlateNumber = updatePlate;
                UcPlateOut.DisplayNewPlate(updatePlate);
                var registeredVehicle = (await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(updatePlate))?.Item1;
                if (registeredVehicle == null || string.IsNullOrEmpty(registeredVehicle.Id))
                {
                    NotifyVehicleNotInSystem(baseLoopLog);
                    return;
                }
                lprResult.Vehicle = registeredVehicle;
            }

            Collection collection = lprResult.Vehicle.Collection!;
            var collectionType = collection.GetAccessKeyGroupType();

            if (!(collection?.GetActiveLanes()?.Contains(Lane.Id) ?? false))
            {
                string message = $"{collection?.Name} {KZUIStyles.CurrentResources.InvalidPermission}";
                UcResult.DisplayResult(EmResultType.ERROR, message);
                return;
            }

            if (!collection.GetExitByLoop())
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotAllowExityByPlate);
                return;
            }
            if (lprResult.Vehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleLocked);
                return;
            }

            if (await AppData.ApiServer.DataService.RegisterVehicle.IsBlackListAsync(lprResult.PlateNumber))
            {
                //Save Black List Alarm
                if (AppData.AppConfig.RejectBlackListVehicle)
                {
                    return;
                }
            }

            var images = new Dictionary<EmImageType, Image?>()
            {
                {EmImageType.PANORAMA,panoramaImage },
                {EmImageType.VEHICLE, lprResult.VehicleImage },
                {EmImageType.PLATE_NUMBER, lprResult.LprImage },
                {EmImageType.FACE, await this.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID) }
            };
            Tuple<ExitData, string>? checkOutResult = null;
            string exitId = Guid.NewGuid().ToString();
            switch (collectionType)
            {
                case EmAccessKeyGroupType.MONTHLY:
                    checkOutResult = await ExecuteLoopMONTH(ie, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                case EmAccessKeyGroupType.DAILY:
                    checkOutResult = await ExecuteLoopDAILY(ie, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                case EmAccessKeyGroupType.VIP:
                    checkOutResult = await ExecuteLoopVIP(ie, baseLoopLog, lprResult, collection, images, exitId);
                    break;
                default:
                    break;
            }

            if (checkOutResult == null) return;

            var exit = checkOutResult.Item1;
            string errorMessage = checkOutResult.Item2;
            bool isPayByQR = false;
            bool isDisplayLed = true;

            if (!exit.OpenBarrier)
            {
                isDisplayLed = false;
                long fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                fee = Math.Max(fee, 0);
                LedHelper.DisplayLed(exit.PlateNumber, exit.DatetimeOut, exit.AccessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());

                UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessConfirmRequest);
                var entry = exit.Entry!;

                EventImageDto? panoramaInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.PANORAMA);
                EventImageDto? vehicleInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.VEHICLE);
                var taskVehicleIn = ImageHelper.DownloadBitmapAsync(vehicleInDto?.PresignedUrl);
                var taskPanoramaIn = ImageHelper.DownloadBitmapAsync(panoramaInDto?.PresignedUrl);
                var taskVoucherApply = AppData.ApiServer.PaymentService.GetAppliedVoucherDataAsync(entry.Id);
                await Task.WhenAll(taskVehicleIn, taskPanoramaIn, taskVoucherApply);

                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                string alarmMessage = string.IsNullOrEmpty(errorMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : errorMessage;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Request {alarmMessage}"));
                ConfirmOutRequest request = new()
                {
                    //Thông tin chung
                    User = StaticPool.SelectedUser,
                    ErrorMessage = alarmMessage,
                    AbnormalCode = EmAlarmCode.SYSTEM_ERROR,
                    DetectedPlate = lprResult.Vehicle.Code,
                    AlarmPlate = lprResult.Vehicle.Code,
                    //Thông tin vào
                    EntryImages = new Dictionary<EmImageType, Image?>()
                    {
                        {EmImageType.PANORAMA,  taskPanoramaIn.Result},
                        {EmImageType.VEHICLE,  taskVehicleIn.Result},
                    },
                    VoucherApplies = taskVoucherApply.Result ?? [],
                    //Thông tin ra
                    EventOut = exit,
                    ExitImages = new Dictionary<EmImageType, Image?>()
                    {
                        {EmImageType.PANORAMA, panoramaImage},
                        {EmImageType.VEHICLE, lprResult.VehicleImage},
                    },
                };

                var result = await this.UIInvokeAsync(() =>
                    MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmOut));
                //var result = await this.Invoke(async () => await MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmOut));
                bool isConfirm = result?.IsConfirm ?? false;
                if (!isConfirm)
                {
                    await ExecuteNotConfirmExit(exit, ie, alarmMessage, baseLoopLog);
                    return;
                }
                exit = result.ExitData;
                isPayByQR = result.IsPayByQR;
            }

            lastEvent = exit;
            if (lastEvent.Collection != null)
            {
                collection = lastEvent.Collection;
            }
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessOpenBarrie);
            await ControllerHelper.OpenBarrie(this, collection, ie.DeviceId, baseLoopLog);
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
            if (lprResult.Vehicle.Name.Contains("Temp"))
            {
                await AppData.ApiServer.DataService.AccessKey.DeleteByIdAsync(lprResult.Vehicle.Id);
            }
            await ExcecuteValidEvent(null, lprResult.Vehicle, lprResult.PlateNumber,
                                    images, exit, ie, isPayByQR, isDisplayLed, false);
        }
        private async Task<Tuple<ExitData, string>?> ExecuteLoopDAILY(GeneralEventArgs ie, string baseLoopEventLog, LoopLprResult lprResult,
                                                                      Collection collection, Dictionary<EmImageType, Image?> images, string exitId)
        {
            string baseLoopLog = "";
            if (ie is InputEventArgs)
            {
                baseLoopLog = $"{this.Lane.Name}.DAILY.Loop.{((InputEventArgs)ie).InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            }
            StopTimerCheckAllowOpenBarrie();

            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckOut);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - SEND CHECK IN NORMAL REQUEST"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, this.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            var checkInOutResponse = ValidateExitResponse(exitResponse, collection, lprResult.PlateNumber, images, null, lprResult.Vehicle, ie);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - CHECK EVENT IN NORMAL RESPONSE", checkInOutResponse));

            if (!checkInOutResponse.IsValidEvent && !checkInOutResponse.IsNeedConfirm)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Invalid Event, End Process", checkInOutResponse));
                return null;
            }
            if (lprResult.Vehicle.Name.Contains("Temp") && checkInOutResponse.IsValidEvent && checkInOutResponse.EventData.OpenBarrier)
            {
                await AppData.ApiServer.DataService.AccessKey.DeleteByIdAsync(lprResult.Vehicle.Id);
            }
            return Tuple.Create<ExitData, string>(checkInOutResponse.EventData!, "");
        }
        private async Task<Tuple<ExitData, string>?> ExecuteLoopMONTH(GeneralEventArgs ie, string baseLoopEventLog, LoopLprResult lprResult, Collection collection, Dictionary<EmImageType, Image?> images, string exitId)
        {
            string errorMessage = "";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - START CHECK OUT"));
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckOut);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - SEND CHECK OUT NORMAL REQUEST"));
            var eventOutResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, this.Lane.Id, lprResult.Vehicle!.Id, lprResult.PlateNumber, collection.Id);
            var checkInOutResponse = ValidateExitResponse(eventOutResponse, collection, lprResult.PlateNumber, images, null, lprResult.Vehicle, ie);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - CHECK EVENT OUT NORMAL RESPONSE", checkInOutResponse));

            if (!checkInOutResponse.IsValidEvent && !checkInOutResponse.IsNeedConfirm)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopEventLog} - Invalid Event, End Process", checkInOutResponse));
                return null;
            }

            errorMessage = checkInOutResponse.ErrorMessage;
            return Tuple.Create<ExitData, string>(checkInOutResponse.EventData!, errorMessage);
        }
        private async Task<Tuple<ExitData, string>?> ExecuteLoopVIP(GeneralEventArgs ie, string baseLoopEventLog, LoopLprResult lprResult, Collection collection, Dictionary<EmImageType, Image?> images, string exitId)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - START CHECK OUT"));
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessCheckOut);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - SEND CHECK OUT NORMAL REQUEST"));
            var eventOutResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(exitId, this.Lane.Id, lprResult.Vehicle!.Id, lprResult.PlateNumber, collection.Id);
            if (eventOutResponse == null)
            {
                ExecuteSystemError(true);
                return null;
            }

            BaseErrorData? errorData = eventOutResponse.Item2;
            string errorMessage = string.Empty;
            if (errorData != null)
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    return null;
                }

                var _alarmCode = errorData.GetAbnormalCode();
                var _alarmMessage = errorData.ToString();
                errorMessage = _alarmMessage;
                switch (_alarmCode)
                {
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        _alarmMessage = collection.Name + " - " + _alarmMessage;
                        ExcecuteUnvalidEvent(null, lprResult.Vehicle, collection,
                                             lprResult.PlateNumber, images,
                                             DateTime.Now, null, _alarmMessage, ie);
                        return null;
                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        var eventInData = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), Lane.Id, lprResult.Vehicle.Id, lprResult.PlateNumber, collection.Id);
                        if (eventInData != null && eventInData.Item1 != null)
                        {
                            return await ExecuteLoopVIP(ie, baseLoopEventLog, lprResult, collection, images, exitId);
                        }
                        else
                        {
                            ExecuteSystemError(true);
                        }
                        return null;
                    default:
                        ExcecuteUnvalidEvent(null, lprResult.Vehicle, collection,
                                             lprResult.PlateNumber, images,
                                             DateTime.Now, null, errorMessage, ie);
                        return null;
                }
            }

            return Tuple.Create<ExitData, string>(eventOutResponse.Item1!, errorMessage);
        }
        private void NotifyVehicleNotInSystem(string baseLoopLog)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
            UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.VehicleNotFound);
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
            if (controllerShortcutConfigs == null)
            {
                return;
            }
            foreach (var controllerShortcutConfig in controllerShortcutConfigs)
            {
                foreach (var item in controllerShortcutConfig.KeySetByRelays)
                {
                    if (item.Value != (int)key)
                    {
                        continue;
                    }
                    string controllerId = controllerShortcutConfig.ControllerId;
                    int barrieIndex = item.Key;
                    foreach (iParkingv5.Controller.IController controller in AppData.IControllers)
                    {
                        if (controller.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                        {
                            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.CustomerCommandOpenBarrie + barrieIndex);
                            //Ra lệnh mở cửa
                            await controller.OpenDoor(100, barrieIndex);

                            //Lưu lại cảnh báo mở barrie bằng nút nhấn
                            if (lastEvent == null || !isAllowOpenBarrieManual)
                            {

                                if (lastEvent == null || !isAllowOpenBarrieManual)
                                {
                                    var lprResult = await LaneHelper.LoopLprDetection(this.UcCameraList, this.Lane.Id, this.Lane.Cameras, false);
                                    var panoramaImage = await UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
                                    this.Invoke(new Action(() =>
                                    {
                                        UcEventImageListOut.DisplayExitImage(new Dictionary<EmImageType, Image?>() { { EmImageType.PANORAMA, panoramaImage }, { EmImageType.VEHICLE, lprResult.VehicleImage }, });
                                        UcPlateIn.DisplayLprResult(lprResult.PlateNumber, lprResult.LprImage);
                                    }));
                                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_KEYBOARD, this.UcCameraList, "");
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra phím tắt điều khiển làn
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task CheckLaneOutShortcutConfig(Keys keys)
        {
            if (LaneOutShortcutConfig is null)
            {
                return;
            }
            var a = (int)keys;
            if ((int)keys == LaneOutShortcutConfig.ConfirmPlateKey && this.lastEvent != null)
            {
                await ExitPlateOnUI();
            }
            else if ((int)keys == LaneOutShortcutConfig.ReverseLane)
            {
                ReverseLane();
            }
            else if ((int)keys == LaneOutShortcutConfig.WriteOut)
            {
                await WriteOutClick(null);
            }
            else if ((int)keys == LaneOutShortcutConfig.ReSnapshotKey)
            {
                await RetakeImageClick(null);
            }
            else if ((int)keys == LaneOutShortcutConfig.PrintKey)
            {
                await PrintClick(null);
            }
        }
        private async Task ExitPlateOnUI()
        {
            if (lastEvent == null)
            {
                return;
            }
            var result = await this.UcPlateIn.UpdatePlate(lastEvent.Id, AppData.ApiServer, false, lastEvent.PlateNumber);
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

        #region BASE EVENT
        private CheckEventOutResponse ValidateExitResponse(Tuple<ExitData?, BaseErrorData?>? exitResponse, Collection collection,
                                                           string detectPlateNumber, Dictionary<EmImageType, Image?> images,
                                                           AccessKey? accessKey, AccessKey? vehicle, GeneralEventArgs e)
        {
            if (exitResponse == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true);
                return CheckEventOutResponse.CreateDefault();
            }
            if (exitResponse.Item1 == null && exitResponse.Item2 == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false);
                return CheckEventOutResponse.CreateDefault();
            }

            CheckEventOutResponse checkInOutResponse = new()
            {
                IsNeedConfirm = false,
                IsValidEvent = false,
                EventData = exitResponse.Item1,
                ErrorMessage = string.Empty,
                ErrorData = exitResponse.Item2,
                AlarmCode = EmAlarmCode.SYSTEM_ERROR,
            };

            var exitData = exitResponse.Item1;
            var errorData = exitResponse.Item2;
            string alarmDescription = "";

            if (errorData is null)
            {
                string accessKeyId = vehicle != null ? vehicle.Id : (accessKey?.GetVehicleInfo()?.Id ?? "");

                //Kiểm tra nếu nợ thẻ thì lưu cảnh báo
                if (exitData.UnreturnedAccessKey != null)
                {
                    var info = $"{KZUIStyles.CurrentResources.UnreturnedAccessKey}: " + exitData.UnreturnedAccessKey.Name + " / " + exitData.UnreturnedAccessKey.Code;
                    _ = AlarmProcess.SaveAlarmAsync(this.Lane, detectPlateNumber, info, EmAlarmCode.UNRETURNED_CARD, this.UcCameraList, accessKeyId);
                }

                var alarmCodes = exitData!.GetAlarmCode();
                //Sự kiện hợp lệ và không có cảnh báo
                if (exitData is not null && alarmCodes.Count == 0)
                {
                    checkInOutResponse.IsValidEvent = true;
                    checkInOutResponse.IsNeedConfirm = false;
                    return checkInOutResponse;
                }
                //Có cảnh báo
                alarmDescription = "";
                var errorMessage = string.Join(Environment.NewLine, exitData.ToVI());
                checkInOutResponse.ErrorMessage = errorMessage;


                if (alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM) ||
                    alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_INVALID) ||
                    alarmCodes.Contains(EmAlarmCode.PLATE_NUMBER_BLACKLISTED))
                {
                    foreach (var alarmCode in alarmCodes)
                    {
                        switch (alarmCode)
                        {
                            case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                                if (alarmCode == EmAlarmCode.PLATE_NUMBER_BLACKLISTED)
                                {
                                    var blackList = AppData.ApiServer.DataService.BlackListed.GetByCode(detectPlateNumber.Replace(" ", "").Replace("-", "").Replace(".", ""));
                                    if (blackList != null && blackList.Item1 != null)
                                    {
                                        alarmDescription = blackList.Item1.Note;
                                    }
                                }
                                break;
                            case EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM:
                                if (vehicle != null)
                                {
                                    alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + vehicle.Code;
                                }
                                else if (accessKey != null)
                                {
                                    alarmDescription = $"{KZUIStyles.CurrentResources.VehicleCode}: " + (accessKey.GetVehicleInfo()?.Code ?? "");
                                }
                                else
                                {
                                    alarmDescription = "";
                                }
                                checkInOutResponse.AlarmCode = EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM;
                                break;
                            case EmAlarmCode.PLATE_NUMBER_INVALID:
                                alarmDescription = $"{KZUIStyles.CurrentResources.PlateIn}: " + (exitData.Entry?.PlateNumber ?? "");
                                checkInOutResponse.AlarmCode = EmAlarmCode.PLATE_NUMBER_INVALID;
                                break;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, detectPlateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);
                    }

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
                        checkInOutResponse.ErrorMessage = string.Join("\r\n", exitData.ToVI());
                    }
                }
                else
                {
                    SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    checkInOutResponse.IsNeedConfirm = false;
                    checkInOutResponse.IsValidEvent = false;
                    ExecuteSystemError(false);

                    foreach (var alarmCode in alarmCodes)
                    {
                        _ = AlarmProcess.SaveAlarmAsync(this.Lane, detectPlateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKeyId);
                    }
                }

                return checkInOutResponse;
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
                        alarmDescription = KZUIStyles.CurrentResources.BlackedList;
                        ExcecuteUnvalidEvent(accessKey, vehicle, collection,
                                             detectPlateNumber, images,
                                             DateTime.Now, null, alarmDescription, e);
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
                        ExcecuteUnvalidEvent(accessKey, vehicle, collection,
                                             detectPlateNumber, images,
                                             DateTime.Now, null, checkInOutResponse.ErrorMessage, e);
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
                        ExcecuteUnvalidEvent(accessKey, vehicle, collection,
                                            detectPlateNumber, images,
                                            DateTime.Now, null, checkInOutResponse.ErrorMessage, e);
                        break;
                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.XE_CHUA_VAO_BAI);
                        ExcecuteUnvalidEvent(accessKey, vehicle, collection,
                                             detectPlateNumber, images,
                                             DateTime.Now, null, checkInOutResponse.ErrorMessage, e);
                        break;
                    default:
                        ExcecuteUnvalidEvent(accessKey, vehicle, collection,
                             detectPlateNumber, images, DateTime.Now, null, checkInOutResponse.ErrorMessage, e);
                        break;
                }

                _ = AlarmProcess.SaveAlarmAsync(this.Lane, detectPlateNumber, alarmDescription, alarmCode, this.UcCameraList, accessKey?.Id ?? "");

                checkInOutResponse.IsNeedConfirm = false;
                return checkInOutResponse;
            }
        }

        private async Task<ConfirmOutResult> ConfirmOpenBarrieOutResult(ExitData exitData, string errorMessage, EmAlarmCode abnormalCode,
                                                                        string detectedPlate, string registerPlate, string deviceId,
                                                                         Dictionary<EmImageType, Image?> images)
        {
            UcResult.DisplayResult(EmResultType.PROCESS, KZUIStyles.CurrentResources.ProcessGetEntryInfor);
            var entry = exitData.Entry!;

            EventImageDto? panoramaInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.PANORAMA);
            EventImageDto? vehicleInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.VEHICLE);
            EventImageDto? plateInDto = entry.Images?.FirstOrDefault(e => e.Type == EmImageType.PLATE_NUMBER);

            var taskVehicleIn = ImageHelper.DownloadBitmapAsync(vehicleInDto?.PresignedUrl);
            var taskPanoramaIn = ImageHelper.DownloadBitmapAsync(panoramaInDto?.PresignedUrl);
            var taskPlateIn = ImageHelper.DownloadBitmapAsync(plateInDto?.PresignedUrl);

            var taskVoucherApply = AppData.ApiServer.PaymentService.GetAppliedVoucherDataAsync(entry.Id);
            await Task.WhenAll(taskVehicleIn, taskPanoramaIn, taskVoucherApply);

            if (abnormalCode == EmAlarmCode.PLATE_NUMBER_INVALID)
            {
                SoundHelper.PlaySound(deviceId, EmSystemSoundType.BIEN_SO_VAO_RA_KHONG_KHOP_VUI_LONG_CHO_XAC_NHAN_BARRIE);
            }
            else if (abnormalCode == EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM)
            {
                SoundHelper.PlaySound(deviceId, EmSystemSoundType.BIEN_SO_DANG_KY_KHONG_KHOP_VUI_LONG_CHO_XAC_NHAN_BARRIE);
            }
            else
            {
                SoundHelper.PlaySound(deviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
            }
            string alarmMessage = string.IsNullOrEmpty(errorMessage) ? KZUIStyles.CurrentResources.ProcessConfirmOpenbarrie : errorMessage;
            string alarmPlate = abnormalCode == EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM ? registerPlate : exitData.Entry.PlateNumber;

            ConfirmOutRequest request = new()
            {
                //Thông tin chung
                User = StaticPool.SelectedUser,
                ErrorMessage = alarmMessage,
                AbnormalCode = abnormalCode,
                DetectedPlate = detectedPlate,
                AlarmPlate = alarmPlate,
                //Thông tin vào
                EntryImages = new Dictionary<EmImageType, Image?>() {
                    { EmImageType.PANORAMA, taskPanoramaIn.Result },
                    { EmImageType.VEHICLE, taskVehicleIn.Result },
                    { EmImageType.PLATE_NUMBER, taskPlateIn.Result },
                },
                VoucherApplies = taskVoucherApply.Result ?? [],
                //Thông tin ra
                EventOut = exitData,
                ExitImages = images
            };
            // AN TOÀN
            var result = await this.UIInvokeAsync(() =>
                MaskedUserControl.ShowDialog(this.MaskedDialog, request, this, this.dialogConfirmOut));

            return result;
        }

        private void ExecuteSystemError(bool isDisconnectServer)
        {
            UcPlateIn.ClearView();
            UcEventImageListOut.DisplayEntryImage([]);
            UcPlateIn.ClearView(); ;
            UcEventImageListOut.ClearView();
            if (isDisconnectServer)
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ServerDisconnected);
            }
            else
            {
                UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.SystemError);
            }
        }
        private void ExcecuteUnvalidEvent(AccessKey? accessKey, AccessKey? registerVehicle, Collection? collection,
                                          string detectPlate, Dictionary<EmImageType, Image?> images,
                                          DateTime eventTime, ExitData? eventOut, string errorMessage, GeneralEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (e != null)
                {
                    if (e.DeviceType == EmParkingControllerType.Card_Dispenser)
                    {
                        _ = ControllerHelper.RejectCard(this, e.DeviceId);
                    }
                }

            }));
            UcResult.DisplayResult(EmResultType.ERROR, errorMessage);
            long fee = 0;
            if (eventOut != null)
            {
                if (eventOut.Entry != null)
                {
                    fee = eventOut.Amount - eventOut.Entry.Amount;
                    fee = fee > 0 ? fee : 0;
                }
            }

            UcPlateIn.ClearView();
            UcEventImageListOut.DisplayEntryImage([]);
            UcPlateOut.DisplayLprResult(detectPlate, images.GetValueOrDefault(EmImageType.PLATE_NUMBER));
            UcEventImageListOut.DisplayExitImage(images);
            ucEventInfoNew.DisplayEventInfo(eventOut?.Entry?.DateTimeIn, DateTime.Now, accessKey, collection, registerVehicle, fee);
            LedHelper.DisplayDefaultLed(this.Lane.Id);
        }

        private async Task ExcecuteValidEvent(AccessKey? accessKey, AccessKey? registerVehicle,
                                              string detectedPlate, Dictionary<EmImageType, Image?> images, ExitData eventOut, GeneralEventArgs e, bool isPayByQR,
                                              bool isDisplayLed, bool isNeedDisplayImage)
        {
            StartTimerCheckAllowOpenBarrie();
            isAllowOpenBarrieManual = true;

            _ = Task.Run(new Action(async () =>
               {
                   await LaneHelper.SaveLocalImage(images, eventOut.Id.ToString(), this.Lane.Name);
                   if (isNeedDisplayImage)
                   {
                       this.UcPlateOut.DisplayLprResult(eventOut.PlateNumber, images.GetValueOrDefault(EmImageType.PLATE_NUMBER));
                       this.UcEventImageListOut.DisplayExitImage(images);
                   }
                   DisplayEntryImage(eventOut);

                   try
                   {
                       SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventOut.{eventOut.Id} - Save Image"));

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
                       _ = LaneHelper.SaveImage(imageDatas, false, eventOut.Id);
                       foreach (KeyValuePair<EmImageType, Image?> item in images)
                       {
                           if (item.Value is null)
                           {
                               continue;
                           }
                           item.Value.Dispose();
                       }
                   }
                   catch (Exception ex)
                   {
                       SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventOut.{eventOut.Id} - Error", ex));
                   }
               }));

            if (AppData.AppConfig.IsSendInvoice)
            {
                var invoiceData = (await AppData.ApiServer.InvoiceService.CreateInvoiceAsync(eventOut.Id))?.Item1;
                if (lastEvent != null)
                    lastEvent.Invoice = invoiceData;
            }
            _ = Task.Run(new Action(async () =>
            {
                this.ExitLog.UpdateEvent(eventOut.Id, EmExitLocalDataLogStatus.CreateTransaction);
                var exit = await AppData.ApiServer.ReportingService.Exit.GetExitByIdAsync(eventOut.Id);
                if (exit != null && !string.IsNullOrEmpty(exit.Id))
                {
                    var fee = exit.Amount - exit.Entry!.Amount - exit.DiscountAmount;
                    this.ExitLog.UpdateEvent(eventOut.Id, EmExitLocalDataLogStatus.CreateTransaction, fee.ToString());

                    if (!isPayByQR)
                    {
                        await AppData.ApiServer.PaymentService.CreateTransactionAsync(eventOut.Id, Math.Max(0, fee),
                                                                             TargetType.EXIT, OrderMethod.CASH, OrderProvider.None);
                    }
                    else
                    {
                        await AppData.ApiServer.PaymentService.CreateTransactionAsync(eventOut.Id, Math.Max(0, fee),
                                                                                TargetType.EXIT, OrderMethod.OFFLINE_QR, OrderProvider.None, $"{exit.AccessKey.Code} {exit.DatetimeOut:ddMMyyyy} {exit.DatetimeOut:HHmmss}");
                    }
                }
            }));

            SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.HEN_GAP_LAI);
            long fee = eventOut.Amount - eventOut.Entry.Amount - eventOut.DiscountAmount;
            fee = Math.Max(fee, 0);
            string resultText = fee > 0 ? KZUIStyles.CurrentResources.TakeMoney : KZUIStyles.CurrentResources.SeeYouAgain;
            if (eventOut.UnreturnedAccessKey != null)
            {
                var info = $"{KZUIStyles.CurrentResources.UnreturnedAccessKey}: " + eventOut.UnreturnedAccessKey.Name + " / " + eventOut.UnreturnedAccessKey.Code;
                UcResult.DisplayResult(EmResultType.WARNING, info);
                _ = AppData.ApiServer.OperatorService.Exit.UpdateNoteAsync(eventOut.Id, info);
            }
            else
            {
                UcResult.DisplayResult(EmResultType.SUCCESS, resultText);
            }
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventOut.{eventOut.Id} - Display Event Out Info"));
            ucEventInfoNew.DisplayEventInfo(eventOut.Entry.DateTimeIn, eventOut.DatetimeOut, accessKey, eventOut.Collection ?? eventOut.AccessKey?.Collection, registerVehicle, fee);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this.Lane.Name}.EventOut.{eventOut.Id} - Display Led"));
            _ = Task.Run(new Action(() =>
            {
                if (isDisplayLed)
                    LedHelper.DisplayLed(detectedPlate, eventOut.DatetimeOut, accessKey, KZUIStyles.CurrentResources.SeeYouAgain, this.Lane.Id, fee.ToString());
                else
                    LedHelper.DisplayDefaultLed(this.Lane.Id);
            }));
            lastEvent = eventOut;
        }
        private void DisplayEntryImage(ExitData eventOut)
        {
            if (eventOut is null || eventOut.Entry is null || eventOut.Entry.Images is null)
            {
                UcEventImageListOut.DisplayEntryImageData([]);
                UcPlateIn.ClearView();
                return;
            }

            this.BeginInvoke(new Action(() =>
            {
                EventImageDto? overviewImgData = eventOut.Entry.Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                EventImageDto? vehicleImgData = eventOut.Entry.Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
                EventImageDto? lprImgData = eventOut.Entry.Images.Where(e => e.Type == EmImageType.PLATE_NUMBER).FirstOrDefault();
                EventImageDto? faceImgData = eventOut.Entry.Images.Where(e => e.Type == EmImageType.FACE).FirstOrDefault();

                UcEventImageListOut.DisplayEntryImageData(new List<EventImageDto?>() { overviewImgData, vehicleImgData, faceImgData });
                UcPlateIn.DisplayLprResultData(eventOut.Entry.PlateNumber, lprImgData);
            }));
        }

        private async Task ExecuteNotConfirmExit(ExitData exit, GeneralEventArgs e, string alarmMessage, string baseLog)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Not Confirm {alarmMessage}"));
            SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
            await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exit.Id.ToString());
            UcResult.DisplayResult(EmResultType.ERROR, KZUIStyles.CurrentResources.ProccesNotConfirmVehicleExit);
        }
        #endregion END BASE EVENT
        #endregion

        #region Khởi Tạo Làn
        public void InitUI()
        {
            ucEventInfoNew = new ucEntryInfor(this.Lane);
            this.ucEventInfoNew = DataHelper.CreateDataInfor(EmViewOption.DataAndMoney, this.Lane);

            UcLaneTitle.Init(this.Lane, OpenBarrieClick, WriteOutClick, RetakeImageClick, SettingClick);
            UcCameraList.Init(this, AppData.Cameras, AppData.AppConfig.IsUseVirtualLoop, AppData.AppConfig.VirtualLoopMode, AppData.AppConfig.MotionAlarmLevel, AppData.AppConfig.cameraSDk);
            UcCameraList.KZUI_Title = KZUIStyles.CurrentResources.CameraOut;

            UcPlateIn.Init(KZUIStyles.CurrentResources.PlateIn.ToUpper(), defaultImg);
            UcPlateOut.Init(KZUIStyles.CurrentResources.PlateOut.ToUpper(), defaultImg);

            UcAppFunctions.InitView(this.Lane, KZUIStyles.CurrentResources.OpenBarrie,
                                               KZUIStyles.CurrentResources.WriteOut,
                                               KZUIStyles.CurrentResources.RetakeImage, KZUIStyles.CurrentResources.Print, "Đ/K khách ra");
            UcAppFunctions.InitFunction(OpenBarrieClick, RetakeImageClick, WriteOutClick, PrintClick, RegisterVehicleClick, CloseBarrieClick);

        }

        #endregion

        #region Timer
        private void TimerRefreshUI_Tick(object sender, EventArgs e)
        {
            timeRefresh++;
            if (timeRefresh >= AppData.AppConfig.TimeToDefautUI)
            {
                StopTimeRefreshUI();
                ClearView();
                this.UcPlateOut.ClearView();
                this.UcPlateIn.ClearView();
                this.UcEventImageListOut.ClearView();
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
        private void ClearView()
        {
            //UcEventImageListOut.ClearView();
            //UcPlateIn.ClearView();
            //UcPlateOut.ClearView();
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
                this.ExitLog.ClearLogAfterDays(2);
                this.timerClearLog.Enabled = true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Clear EntryLog", ex, EmSystemActionType.ERROR));
            }
        }

        public void DisplayEventImage(string plateNumber, Dictionary<EmImageType, Image?> images)
        {
            this.UcPlateOut.DisplayLprResult(plateNumber, images.GetValueOrDefault(EmImageType.PLATE_NUMBER));
            this.UcEventImageListOut.DisplayExitImage(images);
            this.UcEventImageListOut.DisplayEntryImage([]);
            this.UcPlateIn.ClearView();
        }
    }
}