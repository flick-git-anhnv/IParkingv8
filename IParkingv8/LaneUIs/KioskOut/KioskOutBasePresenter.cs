using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Helpers;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.Forms;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using Kztek.Control8.KioskOut.ConfirmPlatePresenter;
using Kztek.Control8.KioskOut.PaymentPresenter.ConfirmPayment;
using Kztek.Control8.KioskOut.PaymentPresenter.KioskPayment;
using Kztek.Object;
using Kztek.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System.Drawing.Imaging;
using System.Text;

namespace IParkingv8.LaneUIs.KioskOut
{
    public class KioskOutBasePresenter
    {
        public IKioskOutView _mainView;
        public bool isAllowOpenBarrieManual = false;
        public ExitData? lastEvent = null;
        public string lastMessage = string.Empty;

        //public readonly UcConfirmKiosk ucBaseReport;
        public readonly IAPIServer _apiServer;

        public readonly KioskOutPaymentPresenter _kioskOutPaymentPresenter;
        public readonly KioskOutConfirmPaymentPresenter _kioskOutConfirmPaymentPresenter;
        public readonly ConfirmPlateDailyPresenter _kioskOutConfirmPlateDailyPresenter;
        public readonly ConfirmPlateMonthlyPresenter _kioskOutConfirmPlateMonthlyPresenter;
        public List<CardEventArgs> LastCardEventDatas { get; set; } = [];

        public KioskOutBasePresenter(IKioskOutView mainView,
            IAPIServer apiServer,
            KioskOutPaymentPresenter _kioskOutPaymentPresenter, KioskOutConfirmPaymentPresenter _kioskOutConfirmPaymentPresenter,
            ConfirmPlateDailyPresenter _kioskOutConfirmPlateDailyPresenter, ConfirmPlateMonthlyPresenter _kioskOutConfirmPlateMonthlyPresenter,
            List<CardEventArgs> LastCardEventDatas)
        {
            _mainView = mainView;
            _apiServer = apiServer;

            this._kioskOutPaymentPresenter = _kioskOutPaymentPresenter;
            this._kioskOutConfirmPaymentPresenter = _kioskOutConfirmPaymentPresenter;
            this._kioskOutConfirmPlateDailyPresenter = _kioskOutConfirmPlateDailyPresenter;
            this._kioskOutConfirmPlateMonthlyPresenter = _kioskOutConfirmPlateMonthlyPresenter;

            this.LastCardEventDatas = LastCardEventDatas;
        }


        public async Task<CheckEventOutResponse> ValidateExitResponse(Tuple<ExitData?, BaseErrorData?>? exitResponse,
                       Collection collection, string detectPlateNumber,
                       AccessKey? accessKey, AccessKey? vehicle, GeneralEventArgs e, Image? vehicleImage, Image? panoramaImage)
        {
            if (exitResponse == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                ExecuteSystemError(true, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                return CheckEventOutResponse.CreateDefault();
            }

            if (exitResponse.Item1 == null && exitResponse.Item2 == null)
            {
                SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
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
                if (exitData!.UnreturnedAccessKey != null)
                {
                    var info = "Nợ thẻ: " + exitData.UnreturnedAccessKey.Name + " / " + exitData.UnreturnedAccessKey.Code;
                    _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, info, EmAlarmCode.UNRETURNED_CARD, this._mainView.UcCameraList, accessKeyId);
                }

                //Sự kiện hợp lệ
                if (exitData is not null && exitData.GetAlarmCode().Count == 0)
                {
                    checkInOutResponse.IsValidEvent = true;
                    checkInOutResponse.IsNeedConfirm = false;
                    return checkInOutResponse;
                }

                //Có cảnh báo
                var alarmCodes = exitData!.GetAlarmCode();
                alarmDescription = "";

                var errorMessage = string.Join(Environment.NewLine, exitData.ToVI());
                checkInOutResponse.ErrorMessage = errorMessage;

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
                        checkInOutResponse.ErrorMessage = string.Join("\r\n", exitData.ToVI());
                    }

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
                                    alarmDescription = "BSĐK: " + vehicle.Code;
                                }
                                else if (accessKey != null)
                                {
                                    alarmDescription = "BSĐK: " + (accessKey.GetVehicleInfo()?.Code ?? "");
                                }
                                else
                                {
                                    alarmDescription = "";
                                }
                                checkInOutResponse.AlarmCode = EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM;
                                break;
                            case EmAlarmCode.PLATE_NUMBER_INVALID:
                                alarmDescription = "BS vào: " + (exitData.Entry?.PlateNumber ?? "");
                                checkInOutResponse.AlarmCode = EmAlarmCode.PLATE_NUMBER_INVALID;
                                break;
                            default:
                                break;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKeyId);
                    }
                }
                else
                {
                    SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    checkInOutResponse.IsNeedConfirm = false;
                    checkInOutResponse.IsValidEvent = false;
                    ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);

                    foreach (var alarmCode in alarmCodes)
                    {
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKeyId);
                    }
                }

                return checkInOutResponse;

            }
            //Sự kiện lỗi, ra lệnh nhả thẻ, lấy thông tin lỗi và lưu lại cảnh báo
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
                        alarmDescription = "Biển số đen: ";
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
                            alarmDescription = "Tên: " + accessKey.Name + " - Mã: " + accessKey.Code + " - Ghi chú: " + (accessKey.Note ?? "");
                        }
                        else if (vehicle != null)
                        {
                            alarmDescription = "Tên: " + vehicle.Name + " - BSĐK: " + vehicle.Code;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKey?.Id ?? "");
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyLocked), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (vehicle != null)
                        {
                            if (vehicle.ExpireTime != null)
                            {
                                alarmDescription = "Ngày hết hạn: " + vehicle.ExpireTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
                            }
                        }
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKey?.Id ?? "");
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyExpired), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        SoundHelper.PlaySound(e.DeviceId, EmSystemSoundType.XE_CHUA_VAO_BAI);
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKey?.Id ?? "");
                        ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.EntryNotFound), detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(this._mainView.Lane, detectPlateNumber, alarmDescription, alarmCode, this._mainView.UcCameraList, accessKey?.Id ?? "");
                        ExecuteSystemError(false, detectPlateNumber, vehicleImage, panoramaImage, accessKey, vehicle);
                        break;
                }

                checkInOutResponse.IsNeedConfirm = false;
                return checkInOutResponse;
            }
        }

        public void ExecuteSystemError(bool isDisconnectWithServer, string detectedPlate, Image? vehicleImage, Image? panoramaImage,
                                       AccessKey? accessKey, AccessKey? registerVehicle)
        {
            LedHelper.DisplayDefaultLed(this._mainView.Lane.Id);
            var key = isDisconnectWithServer ? nameof(KZUIStyles.CurrentResources.ServerDisconnected) :
                                               nameof(KZUIStyles.CurrentResources.SystemError);
            if (accessKey != null)
            {
                if (accessKey.Collection?.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                {
                    _ = ShowNotifyDailyDialog(key, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error,
                                              detectedPlate, accessKey, vehicleImage, panoramaImage);
                }
                else
                {
                    _ = ShowNotifyMonthlyDialog(key, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error,
                                              detectedPlate, accessKey, registerVehicle, vehicleImage, panoramaImage);
                }
                return;
            }
            _ = ShowNotifyDailyDialog(key, nameof(KZUIStyles.CurrentResources.TryAgain), EmImageDialogType.Error,
                                      detectedPlate, accessKey, vehicleImage, panoramaImage);
            return;
        }
        public void ExecuteUnvalidEvent(string messageTag, string detectedPlate, Image? vehicleImage, Image? panoramaImage, AccessKey? accessKey, AccessKey? registerVehicle)
        {
            LedHelper.DisplayDefaultLed(this._mainView.Lane.Id);

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
            _ = ShowNotifyDailyDialog(messageTag, KZUIStyles.CurrentResources.TryAgainLater, EmImageDialogType.Error,
                                      detectedPlate, accessKey, vehicleImage, panoramaImage);
            return;

        }

        public async Task ExcecuteValidEvent(AccessKey? accessKey, AccessKey? registerVehicle, Collection collection, string detectPlate,
                                               DateTime eventTime, Image? overviewImg, Image? vehicleImg, Image? lprImage, ExitData eventOut)
        {
            isAllowOpenBarrieManual = true;
            await AppData.ApiServer.PaymentService.CreateTransactionAsync(eventOut.Id, eventOut.Amount - eventOut.Entry!.Amount - eventOut.DiscountAmount,
                                                                          TargetType.EXIT,
                                                                          OrderMethod.CASH,
                                                                          OrderProvider.None);


            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{this._mainView.Lane.Name}.EventOut.{eventOut.Id} - Save Image"));
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

            _ = LaneHelper.SaveImage(imageDatas, false, eventOut.Id.ToString());
            if (registerVehicle != null)
            {
                _ = this.ShowNotifyMonthlyDialog(
                                     nameof(KZUIStyles.CurrentResources.KioskOutValidEvent),
                                     nameof(KZUIStyles.CurrentResources.SeeYouAgain),
                                     EmImageDialogType.Success, detectPlate,
                                     accessKey, registerVehicle,
                                     vehicleImg, overviewImg);
            }
            else
            {
                _ = this.ShowNotifyDailyDialog(
                                     nameof(KZUIStyles.CurrentResources.KioskOutValidEvent),
                                     nameof(KZUIStyles.CurrentResources.SeeYouAgain),
                                     EmImageDialogType.Success, detectPlate, accessKey,
                                     vehicleImg, overviewImg);
            }
            LedHelper.DisplayDefaultLed(this._mainView.Lane.Id);
        }

        public async Task ShowNotifyDailyDialog(string titleTag, string subTitleTag, EmImageDialogType dialogType,
                                                string detectedPlate, AccessKey? accessKey,
                                                Image? vehicleImage, Image? panoramaImage)
        {
            var request = new KioskDialogDialyRequest(
                titleTag, subTitleTag,
                 dialogType,
                 detectedPlate, accessKey,
                 vehicleImage, panoramaImage
                );

            this._mainView.OpenDialogPage();
            await this._mainView.ShowDailyNotifyDialog(request);
            this._mainView.OpenHomePage();
        }
        public async Task ShowNotifyMonthlyDialog(string titleTag, string subTitletag, EmImageDialogType dialogType,
                                                string detectedPlate, AccessKey? accessKey, AccessKey? registerVehicle,
                                                Image? vehicleImage, Image? panoramaImage)
        {
            var request = new KioskDialogMonthlyRequest(
               titleTag, subTitletag,
                 dialogType,
                 detectedPlate, accessKey, registerVehicle,
                 vehicleImage, panoramaImage
                );

            this._mainView.OpenDialogPage();
            await this._mainView.ShowMonthlyNotifyDialog(request);
            this._mainView.OpenHomePage();
        }

        public async Task<BaseKioskResult> ShowConfirmMonthlyPlate(ExitData exitData, Image? displayImage, string messageTag, Control parent)
        {
            this._mainView.OpenDialogPage();
            var control = (Control)this._mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await _kioskOutConfirmPlateMonthlyPresenter.ShowConfirmPlateAsync(exitData, displayImage, messageTag, parent));
                this._mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await _kioskOutConfirmPlateMonthlyPresenter.ShowConfirmPlateAsync(exitData, displayImage, messageTag, parent);
                this._mainView.OpenHomePage();
                return result;
            }
        }
        public async Task<BaseKioskResult> ShowConfirmDailyPlate(ExitData exitData, Image? displayImage, string messageTag, Control parent)
        {
            this._mainView.OpenDialogPage();

            var control = (Control)this._mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await _kioskOutConfirmPlateDailyPresenter.ShowConfirmPlateAsync(exitData, displayImage, messageTag, parent));
                this._mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await _kioskOutConfirmPlateDailyPresenter.ShowConfirmPlateAsync(exitData, displayImage, messageTag, parent);
                this._mainView.OpenHomePage();
                return result;
            }
        }
        public async Task<BaseKioskResult> ShowConfirmPaymentRequest(ExitData exitData, Image? displayImage)
        {
            this._mainView.OpenDialogPage();

            var control = (Control)this._mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await _kioskOutConfirmPaymentPresenter.RunConfirmPaymentProcessAsync(exitData, displayImage));
                this._mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await _kioskOutConfirmPaymentPresenter.RunConfirmPaymentProcessAsync(exitData, displayImage);
                this._mainView.OpenHomePage();
                return result;
            }
        }
        public async Task<BaseKioskResult> PayParkingFeeAsync(ExitData exitData, Control parent)
        {
            this._mainView.OpenDialogPage();
            var control = (Control)this._mainView;
            if (control.IsHandleCreated && control.InvokeRequired)
            {
                var result = await control.Invoke(async () => await _kioskOutPaymentPresenter.RunPaymentProcessAsync(exitData, parent));
                this._mainView.OpenHomePage();
                return result;
            }
            else
            {
                var result = await _kioskOutPaymentPresenter.RunPaymentProcessAsync(exitData, parent);
                this._mainView.OpenHomePage();
                return result;
            }
        }

        public void NotifyLastMessage()
        {
            if (string.IsNullOrEmpty(lastMessage))
            {
                return;
            }
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(lastMessage));
        }
        public void NotifyConfirmRequest(string confirmMessage, Image? vehicleImage, Image? panoramaImage, ExitData exit)
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
                Data = exit,
                DeviceId = AppData.Computer!.Id,
                Images = images.Where(x => !string.IsNullOrEmpty(x.Base64)).ToList(),
                Message = confirmMessage,
                Action = (int)EmRequestAction.CONFIRM,
                RequestId = exit.Id,
                TargetType = (int)EmRequestTargetType.EXIT
            };
            var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string message = JsonConvert.SerializeObject(eventRequest, setting);
            lastMessage = message;
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(message));
        }
        public void NotifyCancelRequest(ExitData exit)
        {
            var eventRequest = new EventRequest()
            {
                Data = exit,
                DeviceId = AppData.Computer!.Id,
                Message = "",
                Action = (int)EmRequestAction.CANCEL,
                RequestId = exit.Id,
                TargetType = (int)EmRequestTargetType.EXIT
            };
            var setting = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            string message = JsonConvert.SerializeObject(eventRequest, setting);
            lastMessage = message;
            FrmMain.channel?.BasicPublish(RabbitMQHelper.EXCHANGE_CONTROL_CENTER, AppData.Computer!.Id, null, Encoding.UTF8.GetBytes(message));
            lastMessage = string.Empty;
        }

        public void ApplyConfirmResult(EventRequest eventRequest)
        {
            _kioskOutConfirmPlateDailyPresenter.ApplyResult(eventRequest.Action == (int)EmRequestAction.CONFIRM, eventRequest.RequestId);
            _kioskOutConfirmPlateMonthlyPresenter.ApplyResult(eventRequest.Action == (int)EmRequestAction.CONFIRM, eventRequest.RequestId);
        }
        public void ApplyPaymentResult(PaymentResult paymentResult)
        {
            _kioskOutPaymentPresenter.ApplyPaymentResult(paymentResult);
        }

        public void OpenHomePage()
        {
            this._mainView.OpenHomePage();
        }
        public void Translate()
        {
            _kioskOutPaymentPresenter?.Translate();
            _kioskOutConfirmPaymentPresenter?.Translate();
            _kioskOutConfirmPlateDailyPresenter?.Translate();
            _kioskOutConfirmPlateMonthlyPresenter?.Translate();
        }
    }
}
