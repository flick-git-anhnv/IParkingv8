using iParkingv5.Lpr;
using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.Style;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using IParkingv8.Helpers.CardProcess;
using Kztek.Object;
using Kztek.Tool;
using Newtonsoft.Json.Linq;
using static Kztek.Object.InputTupe;

namespace IParkingv8.LaneUIs.KioskIn
{
    public class KioskInCardPresenter(KioskInBasePresenter basePresenter)
    {
        public async Task CardOnRFEventArgs(CardOnRFEventArgs e)
        {
            string baseLog = $"{basePresenter._mainView.Lane.Name}.CARD_ON_RF.{e.PreferCard}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"New Card On RF Event", e));

            //Thẻ không hợp lệ
            var cardValidate = await CardBaseProcess.ValidateAccessKeyByCode(basePresenter._mainView.Lane, new CardEventArgs()
            {
                PreferCard = e.PreferCard,
                Type = (int)EmAccessKeyType.CARD
            }, baseLog);
            if (cardValidate.CardValidateType != EmAlarmCode.NONE)
            {
                ControllerHelper.RaLenhThuThe(e.DeviceId);
                return;
            }

            //Thẻ không phải thẻ lượt
            if (cardValidate.AccessKey?.Collection?.GetAccessKeyGroupType() != EmAccessKeyGroupType.DAILY)
            {
                ControllerHelper.RaLenhThuThe(e.DeviceId);
                return;
            }

            //Xe đã vào bãi
            var entry = await AppData.ApiServer.ReportingService.Entry.GetEntryByAccessKeyIdAsync(cardValidate.AccessKey!.Id);
            if (entry != null && !string.IsNullOrEmpty(entry.Id))
            {
                ControllerHelper.RaLenhThuThe(e.DeviceId);
                return;
            }
        }

        /// <summary>
        /// Ko cần chặn trường hợp quẹt thẻ lượt vào ô thẻ tháng
        /// Tránh trường hợp phần cứng lỗi phải quẹt thẻ thủ công để cho khách vào
        /// </summary>
        /// <param name="ce"></param>
        /// <returns></returns>
        public async Task OnNewCardEvent(CardEventArgs ce)
        {
            basePresenter._mainView.OpenHomePage();
            basePresenter._mainView.ShowLoadingIndicator("", "");
            //if (ce.InputType == EmInputType.ButtonAbnormal)
            //{
            //    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"Loop không được kích hoạt - Check BS xe hợp lệ"));
            //    basePresenter._mainView.UpdateLoadingIndicator("", "Nhận dạng biển số");
            //    var checkVehicleImage = await IsValidPlateAsync();
            //    if (checkVehicleImage == null)
            //    {
            //        basePresenter._mainView.HideLoadingIndicator();
            //        _ = basePresenter.ShowNotifyDialog(EmLanguageKey.NOTIFY_INVALID_PLATE,
            //                             KZUIStyles.CurrentResources.TryAgain, EmImageDialogType.Infor, checkVehicleImage);
            //        return;
            //    }
            //}

            string baseLog = $"{basePresenter._mainView.Lane.Name}.CARD.{ce.PreferCard}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - START"));

            basePresenter.isAllowOpenBarrieManual = false;

            #region Kiểm tra thông tin định danh
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessChecking);
            basePresenter.lastEvent = null;
            var cardValidate = await CardBaseProcess.ValidateAccessKeyByCode(basePresenter._mainView.Lane, ce, baseLog);
            _ = AlarmProcess.InvokeCardValidateAsync(cardValidate, basePresenter._mainView.Lane, basePresenter._mainView.UcCameraList);
            SoundHelper.InvokeCardValidate(cardValidate, ce.DeviceId);
            if (cardValidate.CardValidateType != EmAlarmCode.NONE)
            {
                basePresenter._mainView.HideLoadingIndicator();

                //Kiểm tra nếu thẻ được nhả ở giữa khay thì ra lệnh thu thẻ
                //Nếu quẹt vào đầu đọc thẻ tháng thì bỏ qua
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }

                if (cardValidate.AccessKey != null)
                {
                    bool _isCar = cardValidate.AccessKey.Collection?.GetVehicleType() == EmVehicleType.CAR;
                    var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, _isCar, null);
                    //Định danh lượt không hợp lệ
                    if (cardValidate.AccessKey.Collection?.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                    {
                        _ = basePresenter.ShowNotifyDailyDialog(cardValidate.DisplayAlarmMessageTag,
                                                               nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error,
                                                               _result.PlateNumber, cardValidate.AccessKey,
                                                               _result.VehicleImage, _result.PanoramaImage);
                    }
                    //Định danh tháng không hợp lệ
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(cardValidate.DisplayAlarmMessageTag,
                                                                  nameof(KZUIStyles.CurrentResources.TryAgainLater), EmImageDialogType.Error,
                                                                  _result.PlateNumber, cardValidate.AccessKey, cardValidate.AccessKey.GetVehicleInfo(),
                                                                  _result.VehicleImage, _result.PanoramaImage);
                    }
                }
                //Không đọc được thông tin định danh
                else
                {
                    bool _isCar = false;
                    foreach (var item in basePresenter._mainView.Lane.Cameras)
                    {
                        switch ((EmCameraPurpose)item.Purpose)
                        {
                            case EmCameraPurpose.CarLpr:
                                _isCar = true;
                                break;
                            default:
                                break;
                        }
                    }
                    var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, _isCar, null);
                    _ = basePresenter.ShowNotifyDailyDialog(cardValidate.DisplayAlarmMessageTag, "--- " + ce.PreferCard + " ---",
                                                            EmImageDialogType.Error,
                                                            _result.PlateNumber,
                                                            null, _result.VehicleImage, _result.PanoramaImage);
                }
                return;
            }
            AccessKey accessKeyResponse = cardValidate.AccessKey!;
            #endregion

            #region Cập nhật thông tin nhóm định danh cho máy nhả thẻ
            bool isCheckCardDipenserSuccess;
            (accessKeyResponse, isCheckCardDipenserSuccess) = await CardBaseProcess.CheckCardDispenserCollection(
                                                                                     basePresenter._mainView.Lane, ce, baseLog,
                                                                                     accessKeyResponse,
                                                                                     basePresenter._mainView.ucEventInfoNew,
                                                                                     basePresenter._mainView.UcCameraList,
                                                                                     basePresenter._mainView.UcResult,
                                                                                     null);
            if (!isCheckCardDipenserSuccess || accessKeyResponse == null)
            {
                basePresenter._mainView.HideLoadingIndicator();
                //Kiểm tra nếu thẻ được nhả ở giữa khay thì ra lệnh thu thẻ
                //Nếu quẹt vào đầu đọc thẻ tháng thì bỏ qua
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, false, null);
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle),
                                                        nameof(KZUIStyles.CurrentResources.SystemError), EmImageDialogType.Error,
                                                        _result.PlateNumber, accessKeyResponse, _result.VehicleImage, null);
                return;
            }
            #endregion

            var collection = accessKeyResponse.Collection!;
            bool isCar = collection.GetVehicleType() == EmVehicleType.CAR;

            basePresenter._mainView.UpdateLoadingIndicator(accessKeyResponse.Name, KZUIStyles.CurrentResources.ProcessReadingPlate);
            var result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, isCar, null);
            var plateNumber = result.PlateNumber;
            var panoramaImage = result.PanoramaImage;
            var vehicleImage = result.VehicleImage;
            var lprImage = result.LprImage;

            ce.PlateNumber = plateNumber;
            string standardlizePlate = PlateHelper.StandardlizePlateNumber(plateNumber, true);

            try
            {
                var accessKeyGroupType = collection.GetAccessKeyGroupType();
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Start {accessKeyGroupType} Card Process"));

                switch (accessKeyGroupType)
                {
                    case EmAccessKeyGroupType.DAILY:
                        await ExecuteDAILYCardEvent(accessKeyResponse, plateNumber, ce, panoramaImage, vehicleImage, lprImage, standardlizePlate, collection);
                        break;
                    case EmAccessKeyGroupType.MONTHLY:
                        await ExecuteMONTHCardEvent(accessKeyResponse, plateNumber, ce, panoramaImage, vehicleImage, lprImage, standardlizePlate, collection);
                        break;
                    case EmAccessKeyGroupType.VIP:
                        await ExcecuteVIPCardEvent(accessKeyResponse, plateNumber, ce, panoramaImage, vehicleImage, lprImage, standardlizePlate, collection);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                basePresenter._mainView.HideLoadingIndicator();
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("ExcecuteCardEvent", ex));
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                basePresenter.ExecuteSystemError(false, plateNumber, result.VehicleImage, result.PanoramaImage, null, null);
            }
        }

        #region DAILY CARD
        private async Task ExecuteDAILYCardEvent(AccessKey accessKey, string plateNumber, CardEventArgs ce,
                                                 Image? panoramaImage, Image? vehicleImage, Image? lprImage,
                                                 string standardlizePlateNumber, Collection collection)
        {
            string eventId = Guid.NewGuid().ToString();
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.DAILY.Card.{accessKey.Code}.{accessKey.Name}";

            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckIn);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Normal Request"));
            var entryResponse = await AppData.ApiServer.OperatorService!.Entry.CreateAsync(eventId, basePresenter._mainView.Lane.Id, accessKey.Id, standardlizePlateNumber, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            var validateEntryResponse = basePresenter.ValidateEntryResponse(entryResponse, accessKey.Collection!, plateNumber, accessKey, null, ce, false, vehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Check In Normal Response", validateEntryResponse));

            bool isInvalidEvent = !validateEntryResponse.IsValidEvent && !validateEntryResponse.IsNeedConfirm;
            if (isInvalidEvent)
            {
                //Trường hợp lỗi phải quẹt thẻ lượt vào ô thẻ tháng để cho khách vào
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Invalid Event, End Process", validateEntryResponse));
                return;
            }

            var entry = validateEntryResponse.EventData!;

            BaseKioskResult result;
            bool isInValidDailyPlate = plateNumber.Length < 5 && AppData.AppConfig.IsRequiredDAILYPlateIn;
            LedHelper.DisplayLed(entry.PlateNumber, DateTime.Now, entry.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
            if (!entry.OpenBarrier || isInValidDailyPlate)
            {
                //Cập nhật lại thông tin thiết bị (Server không trả về) 
                entry.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                entry.AccessKey = accessKey;

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);

                //Gửi yêu cầu xác nhận lên server
                string notifyServerMessage = !isInValidDailyPlate ? "Xác nhận mở barrie" : "Cảnh báo biển số";
                basePresenter.NotifyRabbitMQConfirmRequest(notifyServerMessage, vehicleImage, panoramaImage, entry);

                string messageTag = !isInValidDailyPlate ?
                                      nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie) :
                                      nameof(KZUIStyles.CurrentResources.InvalidPlateNumber);
                //Hiển thị giao diện chờ server xác nhận
                result = await basePresenter.ShowConfirmOpenBarrieView(entry, accessKey, basePresenter._mainView.Lane,
                                                                       vehicleImage, panoramaImage, messageTag);
                basePresenter.lastMessage = string.Empty;

                //Kiểm tra kết quả phản hồi từ server
                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id);

                    //Người dùng chủ động hủy yêu cầu
                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyRabbitMQCancelRequest(entry);
                    }
                    //Server không xác nhận
                    else
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                        _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie),
                                                                nameof(KZUIStyles.CurrentResources.TryAgain),
                                                                EmImageDialogType.Error, plateNumber, accessKey,
                                                                vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = entry;

            //Trường hợp khách hàng bấm nút lấy thẻ, ra lệnh nhả thẻ, chờ khách lấy thẻ rồi mới mở barrie
            //Tránh khách quên lấy thẻ
            if (ce.InputType == EmInputType.Button)
            {
                // Nếu nút nhấn nhả thẻ -> Đợi rút thẻ ra mới mở cửa
                ControllerHelper.RaLenhNhaThe(ce.DeviceId);
                basePresenter.isOpenBarrierValid = true;
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.XIN_MOI_LAY_THE);
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.InfoTitle),
                                                       nameof(KZUIStyles.CurrentResources.TakeCardRequired),
                                                        EmImageDialogType.Success, plateNumber, accessKey,
                                                         vehicleImage, panoramaImage);
                var task1 = panoramaImage.GetByteArrayFromImageAsync();
                var task2 = vehicleImage.GetByteArrayFromImageAsync();
                var task3 = lprImage.GetByteArrayFromImageAsync();
                await Task.WhenAll(task1, task2, task3);

                var imageDatas = new Dictionary<EmImageType, List<List<byte>>>
                {
                    { EmImageType.PANORAMA, new List<List<byte>>(){ task1.Result } },
                    { EmImageType.VEHICLE,new List<List<byte>>(){ task2.Result } },
                    { EmImageType.PLATE_NUMBER, new List<List<byte>>(){ task3.Result } }
                };

                _ = LaneHelper.SaveImage(imageDatas, true, entry.Id.ToString());
            }
            //Trường hợp quẹt thẻ thủ công
            else
            {
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.XIN_MOI_QUA);
                ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);
                await basePresenter.ExcecuteValidEvent(accessKey, null, collection, plateNumber, ce.EventTime, panoramaImage, vehicleImage, lprImage, entry);
            }
        }
        #endregion

        #region MONTH CARD
        private async Task ExecuteMONTHCardEvent(AccessKey accessKey, string plateNumber, CardEventArgs ce,
                                                 Image? panoramaImage, Image? vehicleImage, Image? lprImage,
                                                 string standardlizePlateNumber, Collection collection)
        {
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.MONTH.Card.{accessKey.Code}.{accessKey.Name}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Valid Register Vehicle - Event MonthCard"));

            var monthCardValidate = await CardMonthProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, basePresenter._mainView, basePresenter._mainView.ucSelectVehicles);
            if (monthCardValidate.MonthCardValidateType != EmMonthCardValidateType.SUCCESS)
            {
                //Thẻ tháng lỗi, nếu khách đút nhầm vào khay thẻ lượt thì ra lệnh nhả lại thẻ cho khách
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhNhaThe(ce.DeviceId);
                }
                basePresenter._mainView.HideLoadingIndicator();
                if (monthCardValidate.RegisterVehicle == null)
                {
                    _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle), nameof(KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle),
                                                              EmImageDialogType.Error, plateNumber, accessKey, null, vehicleImage, panoramaImage);
                }
                return;
            }

            AccessKey registeredVehicle = monthCardValidate.RegisterVehicle!;
            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                basePresenter._mainView.HideLoadingIndicator();
                _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle), nameof(KZUIStyles.CurrentResources.VehicleLocked),
                                           EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                return;
            }

            plateNumber = monthCardValidate.UpdatePlate;
            standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            bool isCheckInByVehicle = monthCardValidate.IsCheckByPlate;

            string checkInAccessKeyId = (isCheckInByVehicle ? registeredVehicle.Id : accessKey.Id) ?? "";
            await ExecuteMONTH_VEHICLE_CardEvent(baseCardEventLog, checkInAccessKeyId, accessKey, registeredVehicle, plateNumber, ce,
                                                 isCheckInByVehicle, panoramaImage, vehicleImage, lprImage, standardlizePlateNumber, collection);

        }
        private async Task ExecuteMONTH_VEHICLE_CardEvent(string baseCardEventLog, string checkInAccessKeyId, AccessKey accessKey, AccessKey registeredVehicle,
                                             string plateNumber, CardEventArgs ce, bool isCheckInByVehicle,
                                             Image? panoramaImage, Image? vehicleImage, Image? lprImage, string standardlizePlateNumber, Collection collection)
        {
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckIn);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Normal Request"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkInAccessKeyId, standardlizePlateNumber, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            CheckEventResponse? validateEntryResponse = basePresenter.ValidateEntryResponse(eventInResponse, collection, plateNumber,
                                                                                            accessKey, registeredVehicle, ce, isCheckInByVehicle, vehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Event In Response", validateEntryResponse));

            bool isInvalidEvent = !validateEntryResponse.IsValidEvent && !validateEntryResponse.IsNeedConfirm;
            if (isInvalidEvent)
            {
                //Thẻ tháng lỗi, nếu khách đút nhầm vào khay thẻ lượt thì ra lệnh nhả lại thẻ cho khách
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhNhaThe(ce.DeviceId);
                }
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Invalid Event, End Process", validateEntryResponse));
                return;
            }

            var entry = validateEntryResponse.EventData!;
            string confirmMessage = validateEntryResponse.IsNeedConfirm ? validateEntryResponse.ErrorMessage : string.Empty;

            LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, basePresenter.lastEvent.DateTimeIn, basePresenter.lastEvent.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
            if (!string.IsNullOrEmpty(confirmMessage) || !entry.OpenBarrier)
            {
                entry.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                entry.AccessKey = accessKey;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                //Gửi yêu cầu xác nhận lên server
                if (string.IsNullOrEmpty(confirmMessage))
                {
                    basePresenter.NotifyRabbitMQConfirmRequest("Xác nhận mở barrie", vehicleImage, panoramaImage, entry);
                    result = await basePresenter.ShowConfirmOpenBarrieView(entry, accessKey, basePresenter._mainView.Lane, vehicleImage,
                                                                           panoramaImage,
                                                                           nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie));
                }
                else
                {
                    basePresenter.NotifyRabbitMQConfirmRequest("Biển số không khớp với biển đăng ký", vehicleImage, panoramaImage, entry);
                    result = await basePresenter.ShowConfirmPlateView(entry, accessKey, basePresenter._mainView.Lane, vehicleImage, panoramaImage);
                }

                basePresenter.lastMessage = string.Empty;

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id);

                    //Người dùng chủ động hủy yêu cầu
                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyRabbitMQCancelRequest(entry);
                    }
                    //Server không xác nhận
                    else
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                        _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie),
                                                                  nameof(KZUIStyles.CurrentResources.TryAgain),
                                                                  EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle,
                                                                  vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = entry;

            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.XIN_MOI_QUA);
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            await basePresenter.ExcecuteValidEvent(accessKey, registeredVehicle!, collection, plateNumber,
                                                   ce.EventTime, panoramaImage, vehicleImage, lprImage, entry);
        }
        #endregion END MONTH CARD

        #region VIP CARD
        private async Task ExcecuteVIPCardEvent(AccessKey accessKey, string plateNumber, CardEventArgs ce,
                                                Image? panoramaImage, Image? vehicleImage, Image? lprImage, string standardlizePlateNumber, Collection collection)
        {
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.Card.{ce.PreferCard}";

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Valid Register Vehicle - Event VipCard"));

            var vipCardValidate = await CardVipProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, basePresenter._mainView, basePresenter._mainView.ucSelectVehicles);
            if (vipCardValidate.VipCardValidateType != EmVipCardValidateType.SUCCESS)
            {
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                basePresenter._mainView.HideLoadingIndicator();
                if (vipCardValidate.RegisterVehicle == null)
                {
                    _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle),
                                                              nameof(KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle),
                                                EmImageDialogType.Error, plateNumber, accessKey, null, vehicleImage, panoramaImage);
                }
                return;
            }

            AccessKey registeredVehicle = vipCardValidate.RegisterVehicle!;
            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                basePresenter._mainView.HideLoadingIndicator();
                _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.ErrorTitle), nameof(KZUIStyles.CurrentResources.VehicleLocked),
                                          EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                return;
            }

            await ExecuteVIP_VEHICLE_CardEvent(vipCardValidate, baseCardEventLog, accessKey, collection, ce,
                                               panoramaImage, vehicleImage, lprImage);
        }

        private async Task ExecuteVIP_VEHICLE_CardEvent(VipCardValidate vipCardValidate, string baseCardEventLog,
                                                        AccessKey accessKey, Collection collection, CardEventArgs ce,
                                                        Image? panoramaImage, Image? vehicleImage, Image? lprImage)
        {
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckIn);
            AccessKey registeredVehicle = vipCardValidate.RegisterVehicle!;
            string plateNumber = vipCardValidate.UpdatePlate;
            string standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            bool isCheckInByVehicle = vipCardValidate.IsCheckByPlate;

            string checkInAccessKeyId = (isCheckInByVehicle ? registeredVehicle.Id : accessKey.Id) ?? "";

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check In Request"));
            var entryResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkInAccessKeyId, standardlizePlateNumber, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            if (entryResponse == null)
            {
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                basePresenter.ExecuteSystemError(true, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                return;
            }
            if (entryResponse.Item1 == null && entryResponse.Item2 == null)
            {
                if (ce.ReaderIndex != KioskInBasePresenter.MONTHLY_READER)
                {
                    ControllerHelper.RaLenhThuThe(ce.DeviceId);
                }
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                return;
            }

            string confirmMessage = "";
            var errorData = entryResponse.Item2;
            if (errorData is not null)
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                    return;
                }
                var alarmCode = errorData.GetAbnormalCode();
                var alarmMessage = errorData.ToString();
                switch (alarmCode)
                {
                    case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.BLACK_LIST);
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.BlackedList), plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        errorData.Payload ??= [];
                        if (errorData.Payload.TryGetValue("Blacklisted", out object? blackListObj) && blackListObj is JObject jObject)
                        {
                            BlackedList? blackList = jObject.ToObject<BlackedList>();
                            if (blackList != null)
                            {
                                alarmMessage = blackList.Note ?? "";
                            }
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, accessKey?.Id);
                        return;
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        if (accessKey != null)
                        {
                            alarmMessage = "Tên: " + accessKey.Name + " - Mã: " + accessKey.Code + " - Ghi chú: " + (accessKey.Note ?? "");
                        }
                        else if (registeredVehicle != null)
                        {
                            alarmMessage = "Tên: " + registeredVehicle.Name + " - BSĐK: " + registeredVehicle.Code;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, accessKey?.Id);
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyLocked), plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        return;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (registeredVehicle != null)
                        {
                            if (registeredVehicle.ExpireTime != null)
                            {
                                alarmMessage = "Ngày hết hạn: " + registeredVehicle.ExpireTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
                            }
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, accessKey?.Id);
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyExpired), plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        return;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        var eventOutData = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkInAccessKeyId, standardlizePlateNumber, collection.Id);
                        if (eventOutData == null)
                        {
                            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                            basePresenter.ExecuteSystemError(true, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                            return;
                        }
                        errorData = eventOutData.Item2;
                        if (errorData != null)
                        {
                            alarmMessage = errorData.ToString();
                            basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                            return;
                        }
                        await ExecuteVIP_VEHICLE_CardEvent(vipCardValidate, baseCardEventLog, accessKey, collection, ce,
                                                           panoramaImage, vehicleImage, lprImage);
                        return;
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, accessKey?.Id);
                        basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        return;
                }
            }
            else
            {
                var alarmCodes = entryResponse.Item1!.GetAlarmCode();
                if (alarmCodes.Count > 0)
                {
                    string accessKeyId = registeredVehicle.Id;
                    string alarmDescription = "BSĐK: " + registeredVehicle.Code;
                    accessKeyId = registeredVehicle.Id;

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
                            alarmDescription = "BSĐK: " + registeredVehicle.Code;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmDescription, alarmCode, basePresenter._mainView.UcCameraList, accessKeyId);
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
                        basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        return;
                    }
                }
            }

            var entry = entryResponse.Item1!;

            LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, basePresenter.lastEvent.DateTimeIn, basePresenter.lastEvent.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
            if (!string.IsNullOrEmpty(confirmMessage) || !entry.OpenBarrier)
            {
                entry.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                entry.AccessKey = accessKey;

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                if (string.IsNullOrEmpty(confirmMessage))
                {
                    basePresenter.NotifyRabbitMQConfirmRequest("Xác nhận mở barrie", vehicleImage, panoramaImage, entry);
                    result = await basePresenter.ShowConfirmOpenBarrieView(entry, accessKey, basePresenter._mainView.Lane,
                                                                           vehicleImage, panoramaImage,
                                                                           nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie));
                    basePresenter.lastMessage = string.Empty;
                }
                //Có cảnh báo, hiển thị cảnh báo
                else
                {
                    //B1: Hiển thị cảnh báo biển số
                    basePresenter.NotifyRabbitMQConfirmRequest("Biển số không khớp với biển đăng ký", vehicleImage, panoramaImage, entry);
                    result = await basePresenter.ShowConfirmPlateView(entry, accessKey, basePresenter._mainView.Lane, vehicleImage, panoramaImage);
                    basePresenter.lastMessage = string.Empty;
                }

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyRabbitMQCancelRequest(entry);
                    }
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie), nameof(KZUIStyles.CurrentResources.TryAgain),
                                           EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = entry;

            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.XIN_MOI_QUA);
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            await basePresenter.ExcecuteValidEvent(accessKey, registeredVehicle!, collection, plateNumber,
                                                   ce.EventTime, panoramaImage, vehicleImage, lprImage, entry);
        }
        #endregion

        #region CARD BE TAKEN
        public async Task ExcecuteCardbeTaken(CardBeTakenEventArgs ie)
        {
            basePresenter.OpenHomePage();

            SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.Application, EmSystemActionDetail.CARD_BE_TAKEN, EmSystemActionType.INFO, $"{basePresenter._mainView.Lane.Name}.Exit.{ie.InputIndex}"));

            if (!basePresenter.isOpenBarrierValid || basePresenter.lastEvent == null)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Rút thẻ nhưng sự kiện trước không hợp lệ"));
                return;
            }
            basePresenter.isOpenBarrierValid = false;

            _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.Welcome),
                                                    nameof(KZUIStyles.CurrentResources.KioskInHaveAGoodDay),
                                                    EmImageDialogType.Success, "", null, null, null);

            SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.XIN_MOI_QUA);

            ControllerInLane? controllerInLane = (from _controllerInLane in basePresenter._mainView.Lane.ControlUnits
                                                  where _controllerInLane.Id == ie.DeviceId
                                                  select _controllerInLane).FirstOrDefault();

            _ = ControllerHelper.OpenBarrieByControllerId(ie.DeviceId, basePresenter._mainView, null);

            //LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, basePresenter.lastEvent.DateTimeIn, basePresenter.lastEvent.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
        }
        #endregion

        //public async Task<Image?> IsValidPlateAsync()
        //{
        //    var result = await basePresenter._mainView.UcCameraList.GetPlateAsync(basePresenter._mainView.Lane.Id, AppData.LprDetecter!, basePresenter._mainView.Lane.Cameras, true);

        //    if (string.IsNullOrEmpty(result.PlateNumber))
        //    {
        //        result = await basePresenter._mainView.UcCameraList.GetPlateAsync(basePresenter._mainView.Lane.Id, AppData.LprDetecter!, basePresenter._mainView.Lane.Cameras, false);
        //    }
        //    if (string.IsNullOrEmpty(result.PlateNumber) || result.PlateNumber.Length <= 5)
        //    {
        //        return null;
        //    }

        //    foreach (var item in basePresenter._mainView.LastCardEventDatas)
        //    {
        //        if (item.PlateNumber == result.PlateNumber)
        //        {
        //            return null;
        //        }
        //    }

        //    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"Loop ở chế độ cần đè - Biển số hợp lệ plate = {result.PlateNumber}"));
        //    return result.VehicleImage;
        //}
    }
}
