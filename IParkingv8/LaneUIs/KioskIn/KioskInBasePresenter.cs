using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Helpers;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.RabbitMQ;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.Forms;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using Kztek.Control8.KioskIn.ConfirmOpenBarrie;
using Kztek.Control8.KioskIn.ConfirmPlate;
using Kztek.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System.Drawing.Imaging;
using System.Text;

namespace IParkingv8.LaneUIs.KioskIn
{
    public class KioskInBasePresenter
    {
        public const int DAILY_READER = 1;
        public const int MONTHLY_READER = 2;
        #region Properties
        public IKioskInView _mainView;

        public readonly IAPIServer _apiServer;
        public readonly ConfirmOpenBarrieKioskInPresenter confirmOpenBarriePresenter;
        public readonly ConfirmPlateMonthlyKioskInPresenter confirmPlateMonthlyKioskInPresenter;

        public bool isAllowOpenBarrieManual = false;
        public EntryData? lastEvent = null;
        public string lastMessage = string.Empty;
        public bool isOpenBarrierValid = false;
        #endregion

        public KioskInBasePresenter(IKioskInView mainView, IAPIServer apiServer,
            ConfirmOpenBarrieKioskInPresenter confirmOpenBarriePresenter, 
            ConfirmPlateMonthlyKioskInPresenter confirmPlateMonthlyKioskInPresenter)
        {
            _mainView = mainView;
            _apiServer = apiServer;
            this.confirmOpenBarriePresenter = confirmOpenBarriePresenter;
            this.confirmPlateMonthlyKioskInPresenter = confirmPlateMonthlyKioskInPresenter;
        }

        #region RabbitMQ
        //Gửi yêu cầu
        public void NotifyRabbitMQConfirmRequest(string confirmMessage, Image? vehicleImage, Image? panoramaImage, EntryData entry)
        {
            var images = new List<EventImageDto>();
            ImageHelper.ImageToBase64(vehicleImage, out string vehicleBase64, out _, ImageFormat.Jpeg);
            ImageHelper.ImageToBase64(panoramaImage, out string panoramaBase64, out _, ImageFormat.Jpeg);
            images.Add(new EventImageDto()
            {
                Type = EmImageType.VEHICLE,
                Base64 = vehicleBase64
            });
            images.Add(new EventImageDto()
            {
                Type = EmImageType.PANORAMA,
                Base64 = panoramaBase64
            });

            var eventRequest = new EventRequest()
            {
                Data = entry,
                DeviceId = AppData.Computer!.Id,
                Images = images.Where(x => !string.IsNullOrEmpty(x.Base64)).ToList(),
                Message = confirmMessage,
                Action = (int)EmRequestAction.CONFIRM,
                RequestId = entry.Id,
                TargetType = (int)EmRequestTargetType.ENTRY
            };
            var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string message = JsonConvert.SerializeObject(eventRequest, setting);
            lastMessage = message;
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(message));
        }
        public void NotifyRabbitMQCancelRequest(EntryData entry)
        {
            var eventRequest = new EventRequest()
            {
                Data = entry,
                DeviceId = AppData.Computer!.Id,
                Message = "",
                Action = (int)EmRequestAction.CANCEL,
                RequestId = entry.Id,
                TargetType = (int)EmRequestTargetType.ENTRY
            };
            var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string message = JsonConvert.SerializeObject(eventRequest, setting);
            lastMessage = message;
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(message));
            lastMessage = string.Empty;
        }

        //Nhận lệnh từ server
        public void NotifyLastMessage()
        {
            if (string.IsNullOrEmpty(lastMessage))
            {
                return;
            }
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(lastMessage));
        }
        #endregion

        #region Event Check
        /// <summary>
        /// Kiểm tra kết quả trả về từ server xem sự kiện có hợp lệ không
        /// </summary>
        /// <param name="entryResponse"></param>
        /// <param name="collection"></param>
        /// <param name="detectPlateNumber"></param>
        /// <param name="accessKey"></param>
        /// <param name="vehicle"></param>
        /// <param name="e"></param>
        /// <param name="isPlate"></param>
        /// <returns></returns>
        public CheckEventResponse ValidateEntryResponse(Tuple<EntryData?, BaseErrorData?>? entryResponse, Collection collection, string detectPlateNumber,
                                                        AccessKey? accessKey, AccessKey? vehicle, GeneralEventArgs e, bool isPlate,
                                                        Image? vehicleImage, Image? panoramaImage)
        {
            if (entryResponse == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                return CheckEventResponse.CreateDefault();
            }
            if (entryResponse.Item1 == null && entryResponse.Item2 == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
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
                var alarmCodes = entryData!.GetAlarmCode();
                //Sự kiện hợp lệ
                if (entryData is not null && alarmCodes.Count == 0)
                {
                    checkInOutResponse.IsValidEvent = true;
                    checkInOutResponse.IsNeedConfirm = false;
                    return checkInOutResponse;
                }
                //Lỗi thì kiểm tra có bị cảnh báo biển số hay không
                else
                {
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
                            var blackList = AppData.ApiServer.DataService.BlackListed.GetByCode(detectPlateNumber.Replace(" ", "").Replace("-", "").Replace(".", ""));
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
                        _ = AlarmProcess.SaveAlarmAsync(_mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, _mainView.UcCameraList, accessKeyId);
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
                        ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
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
                    ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                    return checkInOutResponse;
                }

                var alarmCode = errorData.GetAbnormalCode();
                checkInOutResponse.ErrorMessage = errorData.ToString();

                switch (alarmCode)
                {
                    case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.BLACK_LIST);
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.BlackedList), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);

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
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: {accessKey.Name} - {KZUIStyles.CurrentResources.AccesskeyCode}: {accessKey.Code} - {KZUIStyles.CurrentResources.AccesskeyNote}: {accessKey.Note ?? ""}";
                        }
                        else if (vehicle != null)
                        {
                            alarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyName}: {vehicle.Name} - {KZUIStyles.CurrentResources.VehicleCode}: {vehicle.Code}";
                        }
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyLocked), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (vehicle != null)
                        {
                            if (vehicle.ExpireTime != null)
                            {
                                alarmDescription = $"{KZUIStyles.CurrentResources.VehicleExpiredDate}: {vehicle.ExpireTime.Value:HH:mm:ss dd/MM/yyyy}";
                            }
                        }
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyExpired), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.XE_DA_VAO_BAI);

                        errorData.Payload ??= [];
                        EntryData? eventInInfo = null;
                        if (errorData.Payload.TryGetValue("Entry", out object? eventInInfoObj) && eventInInfoObj is JObject eventInInfojObject)
                        {
                            eventInInfo = eventInInfojObject.ToObject<EntryData>();
                        }

                        if (eventInInfo == null) break;
                        alarmDescription = "Giờ vào: " + eventInInfo.DateTimeIn.ToString("HH:mm:ss dd/MM/yyyy");

                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{_mainView.Lane.Name}.EventIn.{eventInInfo.Id}  - Xe đã vào bãi"));
                        if (eventInInfo.Images != null)
                        {
                            EventImageDto? overviewImgData = eventInInfo.Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                            EventImageDto? vehicleImgData = eventInInfo.Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
                            EventImageDto? lprImgData = eventInInfo.Images.Where(e => e.Type == EmImageType.PLATE_NUMBER).FirstOrDefault();

                            string? url = string.IsNullOrEmpty(overviewImgData?.PresignedUrl) ? vehicleImgData?.PresignedUrl : overviewImgData.PresignedUrl;
                        }

                        AccessKey? registerVehicle = isPlate ? vehicle : accessKey?.GetVehicleInfo();
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.EntryDupplicated), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    default:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                        ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                }

                string accessKeyId = accessKey?.Id ?? vehicle?.Id ?? "";
                _ = AlarmProcess.SaveAlarmAsync(_mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, _mainView.UcCameraList, accessKeyId);

                checkInOutResponse.IsNeedConfirm = false;
                checkInOutResponse.IsValidEvent = false;
                return checkInOutResponse;
            }
        }
        public void ExecuteSystemError(bool isDisconnectWithServer, string detectedPlate, Image? vehicleImage, Image? panoramaImage, AccessKey? accessKey, AccessKey? registerVehicle)
        {
            LedHelper.DisplayDefaultLed(_mainView.Lane.Id);
            var titleTag = isDisconnectWithServer ? nameof(KZUIStyles.CurrentResources.ServerDisconnected) :
                                                    nameof(KZUIStyles.CurrentResources.SystemError);
            if (accessKey != null)
            {
                if (accessKey.Collection?.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                {
                    _ = ShowNotifyDailyDialog(titleTag, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error,
                                              detectedPlate, accessKey, vehicleImage, panoramaImage);
                }
                else
                {
                    _ = ShowNotifyMonthlyDialog(titleTag, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error,
                                              detectedPlate, accessKey, registerVehicle, vehicleImage, panoramaImage);
                }
                return;
            }
            _ = ShowNotifyDailyDialog(titleTag, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error, detectedPlate, accessKey, vehicleImage, panoramaImage);
            return;
        }
        public void ExecuteUnvalidEvent(string messageTag, string detectedPlate, Image? vehicleImage,
                                         Image? panoramaImage, AccessKey? accessKey, AccessKey? registerVehicle)
        {

            LedHelper.DisplayDefaultLed(_mainView.Lane.Id);
            if (registerVehicle != null)
            {
                _ = ShowNotifyMonthlyDialog(messageTag, nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error,
                                              detectedPlate, accessKey, registerVehicle, vehicleImage, panoramaImage);
                return;
            }
            if (accessKey != null)
            {
                if (accessKey.Collection?.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                {
                    _ = ShowNotifyDailyDialog(messageTag, nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error,
                                              detectedPlate, accessKey, vehicleImage, panoramaImage);
                }
                else
                {
                    _ = ShowNotifyMonthlyDialog(messageTag, nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error,
                                              detectedPlate, accessKey, registerVehicle, vehicleImage, panoramaImage);
                }
                return;
            }
            _ = ShowNotifyDailyDialog(messageTag, nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error, detectedPlate, accessKey, vehicleImage, panoramaImage);
            return;

        }
        public async Task ExcecuteValidEvent(AccessKey? accessKey, AccessKey? registerVehicle, Collection collection, string detectPlate,
                                             DateTime eventTime, Image? overviewImg, Image? vehicleImg, Image? lprImage, EntryData eventIn)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{_mainView.Lane.Name}.EventIn.{eventIn.Id} - Save Image"));
            var task1 = overviewImg.GetByteArrayFromImageAsync();
            var task2 = vehicleImg.GetByteArrayFromImageAsync();
            var task3 = lprImage.GetByteArrayFromImageAsync();
            await Task.WhenAll(task1, task2, task3);

            var imageDatas = new Dictionary<EmImageType, List<List<byte>>>
                {
                    { EmImageType.PANORAMA, new List<List<byte>>(){ task1.Result } },
                    { EmImageType.VEHICLE,new List<List<byte>>(){ task2.Result } },
                    { EmImageType.PLATE_NUMBER, new List<List<byte>>(){ task3.Result } }
                };

            _ = LaneHelper.SaveImage(imageDatas, true, eventIn.Id.ToString());
            if (registerVehicle != null)
            {
                _ = ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.Welcome),
                                                nameof(KZUIStyles.CurrentResources.KioskInHaveAGoodDay),
                                                 EmImageDialogType.Success, detectPlate,
                                                 accessKey, registerVehicle,
                                                 vehicleImg, overviewImg);
            }
            else
            {
                _ = ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.Welcome),
                                               nameof(KZUIStyles.CurrentResources.KioskInHaveAGoodDay),
                                               EmImageDialogType.Success, detectPlate, accessKey,
                                               vehicleImg, overviewImg);
            }
            LedHelper.DisplayDefaultLed(_mainView.Lane.Id);
        }
        #endregion

        #region Dialog
        //Hiển thị thông báo có thêm thông tin định danh, không cần quan tâm tới kết quả trả về
        public async Task ShowNotifyDailyDialog(string titleTag, string subTitleTag, EmImageDialogType dialogType,
                                                 string detectedPlate, AccessKey? accessKey,
                                                 Image? vehicleImage, Image? panoramaImage)
        {
            var request = new KioskDialogDialyRequest(
                           titleTag, subTitleTag, dialogType, detectedPlate, accessKey,
                            vehicleImage, panoramaImage
               );

            _mainView.OpenDialogPage();
            await _mainView.ShowDailyNotifyDialog(request);
            _mainView.OpenHomePage();
        }
        public async Task ShowNotifyMonthlyDialog(string titleTag, string subTitleTag, EmImageDialogType dialogType,
                                                 string detectedPlate, AccessKey? accessKey, AccessKey? registerVehicle,
                                                 Image? vehicleImage, Image? panoramaImage)
        {
            var request = new KioskDialogMonthlyRequest(
                            titleTag, subTitleTag,
                            dialogType,
                            detectedPlate, accessKey, registerVehicle,
                            vehicleImage, panoramaImage);
            _mainView.OpenDialogPage();
            await _mainView.ShowMonthlyNotifyDialog(request);
            _mainView.OpenHomePage();
        }

        //Thông báo cần xác nhận
          public async Task<BaseKioskResult> ShowConfirmOpenBarrieView(EntryData entryData, AccessKey accessKey, Lane lane,
                                                                     Image? vehicleImage, Image? panoramaImage,
                                                                     string titleTag)
        {
            _mainView.OpenDialogPage();
            var control = (Control)_mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await confirmOpenBarriePresenter.ShowDialogAsync(entryData, accessKey, lane, vehicleImage, panoramaImage, titleTag));
                _mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await confirmOpenBarriePresenter.ShowDialogAsync(entryData, accessKey, lane, vehicleImage, panoramaImage, titleTag);
                _mainView.OpenHomePage();
                return result;
            }

        }
        public async Task<BaseKioskResult> ShowConfirmPlateView(EntryData entryData, AccessKey accessKey, Lane lane,
                                                                Image? vehicleImage, Image? panoramaImage)
        {
            _mainView.OpenDialogPage();
            var control = (Control)_mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await confirmPlateMonthlyKioskInPresenter.ShowConfirmPlateAsync(entryData, accessKey, lane, vehicleImage, panoramaImage));
                _mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await confirmPlateMonthlyKioskInPresenter.ShowConfirmPlateAsync(entryData, accessKey, lane, vehicleImage, panoramaImage);
                _mainView.OpenHomePage();
                return result;
            }
        }
        #endregion

        public void OpenHomePage()
        {
            _mainView.OpenHomePage();
        }
        public void Translate()
        {
            confirmOpenBarriePresenter?.Translate();
            confirmPlateMonthlyKioskInPresenter?.Translate();
        }
    }
}
