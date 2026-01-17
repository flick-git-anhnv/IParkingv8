using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.Style;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using Kztek.Object;
using Kztek.Tool;
using System.Drawing.Imaging;
using static Kztek.Object.InputTupe;

namespace IParkingv8.LaneUIs.KioskIn
{
    public class KioskInLoopPresenter(KioskInBasePresenter basePresenter)
    {
        /// <summary>
        /// Làn cho phép sử dụng loop thì kiểm tra sự kiện loop
        /// Không thì thông báo cho người dùng bấm nút lấy thẻ || quẹt thẻ tháng
        /// </summary>
        /// <param name="ie"></param>
        /// <returns></returns>
        public async Task ExecuteInputEvent(InputEventArgs ie)
        {
            switch (ie.InputType)
            {
                case EmInputType.Loop:
                    {
                        if (basePresenter._mainView.Lane.Loop)
                        {
                            if (AppData.AppConfig.LoopDelay > 0)
                            {
                                await Task.Delay(AppData.AppConfig.LoopDelay);
                            }
                            await ExecuteLoopEvent(ie);
                        }
                        else
                        {
                            SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_NHAN_NUT_LAY_THE);
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

        #region LOOP EVENT
        public async Task ExecuteLoopEvent(InputEventArgs ie)
        {
            string baseLoopLog = $"{basePresenter._mainView.Lane.Name}.Loop.{ie.InputIndex}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START"));
            basePresenter.OpenHomePage();

            basePresenter._mainView.ShowLoadingIndicator("", KZUIStyles.CurrentResources.ProcessReadingPlate);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            var lprResult = await LaneHelper.LoopLprDetection(basePresenter._mainView.UcCameraList, basePresenter._mainView.Lane.Id,
                                                              basePresenter._mainView.Lane.Cameras);

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - DISPLAY EVENT IMAGE"));
            Image? panoramaImage = await basePresenter._mainView.UcCameraList.GetImageAsync(EmCameraPurpose.Panorama);
            Image? faceImage = await basePresenter._mainView.UcCameraList.GetImageAsync(EmCameraPurpose.FaceID);
            Image? otherImage = await basePresenter._mainView.UcCameraList.GetImageAsync(EmCameraPurpose.Other);
            lprResult.PanoramaImage = panoramaImage;

            Dictionary<EmImageType, List<List<byte>>> imageBytes = new()
            {
                { EmImageType.PANORAMA, [ImageHelper.GetByteArrayFromImage(panoramaImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.VEHICLE, [ImageHelper.GetByteArrayFromImage(lprResult.VehicleImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.PLATE_NUMBER, [ImageHelper.GetByteArrayFromImage(lprResult.LprImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.FACE, [ImageHelper.GetByteArrayFromImage(faceImage, ImageFormat.Jpeg)?.ToList()] },
                { EmImageType.OTHER, [ImageHelper.GetByteArrayFromImage(otherImage, ImageFormat.Jpeg)?.ToList()] },
            };
            _ = AlarmProcess.SaveVehicleOnLoopEventAsync(basePresenter._mainView.Lane, imageBytes, lprResult.PlateNumber);

            if (lprResult == null || lprResult.Vehicle == null ||
                string.IsNullOrEmpty(lprResult.PlateNumber) || lprResult.PlateNumber.Length < 5)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                basePresenter._mainView.HideLoadingIndicator();
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.InfoTitle),
                                                        nameof(KZUIStyles.CurrentResources.InvalidPlateNumber),
                                                        EmImageDialogType.Infor, lprResult?.PlateNumber ?? "", null,
                                                        lprResult?.VehicleImage, panoramaImage);
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_NHAN_NUT_LAY_THE);
                return;
            }

            Collection collection = lprResult.Vehicle!.Collection!;
            var accessKeyType = collection.GetAccessKeyGroupType();
            bool isValidIdentityPlate = collection.GetEntryByLoop();

            //Kiểm tra phương tiện có bị khóa không
            if (lprResult.Vehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VEHICLE_LOCKED);
                _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.InfoTitle),
                                                          nameof(KZUIStyles.CurrentResources.VehicleLocked), EmImageDialogType.Infor,
                                                          lprResult.PlateNumber, null, lprResult.Vehicle,
                                                          lprResult.VehicleImage, lprResult.PanoramaImage);
                return;
            }

            //Kiểm tra quyền truy cập
            if (!(collection?.GetActiveLanes()?.Contains(basePresenter._mainView.Lane.Id) ?? false))
            {
                basePresenter._mainView.HideLoadingIndicator();
                string message = $"{collection?.Name} không được phép sử dụng với làn {basePresenter._mainView.Lane.Name}";
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - {message} - END"));
                _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.InvalidPermission),
                                    nameof(KZUIStyles.CurrentResources.TryAgain),
                                    EmImageDialogType.Error,
                                    lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, panoramaImage);
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.SAI_QUYEN_TRUY_CAP);
                return;
            }

            //Kiểm tra nhóm không được phép vào bằng biển số ==> yêu cầu quẹt thẻ
            if (!isValidIdentityPlate && accessKeyType != EmAccessKeyGroupType.DAILY)
            {
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_NHAN_NUT_LAY_THE);
                _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.InfoTitle),
                                                          nameof(KZUIStyles.CurrentResources.VehicleNotAllowEntryByPlate), EmImageDialogType.Infor,
                                                          lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, lprResult.PanoramaImage);
                return;
            }

            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckIn + " " + lprResult.PlateNumber);
            switch (accessKeyType)
            {
                case EmAccessKeyGroupType.MONTHLY:
                    await ExecuteMONTHLoopEvent(ie, lprResult, collection, panoramaImage);
                    break;
                case EmAccessKeyGroupType.VIP:
                    await ExecuteVIPLoopEvent(ie, lprResult, collection, panoramaImage);
                    break;
                default:
                    break;
            }
        }

        private async Task ExecuteMONTHLoopEvent(InputEventArgs ie, LoopLprResult lprResult, Collection collection, Image? panoramaImage)
        {
            string baseLoopLog = $"{basePresenter._mainView.Lane.Name}.MONTH.Loop.{ie.InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START CHECK IN"));

            basePresenter._mainView.StopTimerCheckAllowOpenBarrie();

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - SEND CHECK IN NORMAL REQUEST"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            var checkInOutResponse = basePresenter.ValidateEntryResponse(eventInResponse, collection, lprResult.PlateNumber, null, lprResult.Vehicle, ie, true, lprResult.VehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - CHECK EVENT IN NORMAL RESPONSE", checkInOutResponse));

            if (!checkInOutResponse.IsValidEvent && !checkInOutResponse.IsNeedConfirm) return;

            LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, basePresenter.lastEvent.DateTimeIn, basePresenter.lastEvent.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
            var entry = checkInOutResponse.EventData!;

            if (!entry.OpenBarrier)
            {
                entry.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                entry.AccessKey = lprResult.Vehicle;

                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                basePresenter.NotifyRabbitMQConfirmRequest("Xác nhận mở barrie", lprResult.VehicleImage, panoramaImage, entry);
                result = await basePresenter.ShowConfirmOpenBarrieView(entry, lprResult.Vehicle, basePresenter._mainView.Lane,
                                                                       lprResult.VehicleImage, panoramaImage,
                                                                       nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie));
                basePresenter.lastMessage = string.Empty;

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Not Confirm Open Barrie"));
                    await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyRabbitMQCancelRequest(entry);
                    }
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie),
                                                                  nameof(KZUIStyles.CurrentResources.TryAgain),
                                                                  EmImageDialogType.Error, lprResult.PlateNumber, null,
                                                                  lprResult.Vehicle, lprResult.VehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = entry;

            SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.XIN_MOI_QUA);

            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ie.DeviceId, baseLoopLog);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - Display Valid Event"));
            await basePresenter.ExcecuteValidEvent(null, lprResult.Vehicle, collection, lprResult.PlateNumber, ie.EventTime,
                                                   panoramaImage, lprResult.VehicleImage, lprResult.LprImage, entry);
        }
        private async Task ExecuteVIPLoopEvent(InputEventArgs ie, LoopLprResult lprResult, Collection collection, Image? panoramaImage)
        {
            string baseLoopLog = $"{basePresenter._mainView.Lane.Name}.VIP.Loop.{ie.InputIndex}.{lprResult.Vehicle!.Name}.{lprResult.Vehicle.Code}";
            basePresenter._mainView.StopTimerCheckAllowOpenBarrie();
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Send Check In Normal Request"));
            var eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            if (eventInResponse == null)
            {
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                basePresenter.ExecuteSystemError(true, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
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
                    basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                    return;
                }
                var alarmCode = errorData.GetAbnormalCode();
                var alarmMessage = errorData.ToString();

                switch (alarmCode)
                {
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        alarmMessage = collection.Name + " - " + alarmMessage;
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyLocked), lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle?.Id);
                        return;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        alarmMessage = collection.Name + " - " + alarmMessage;
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyExpired), lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle?.Id);
                        return;
                    case EmAlarmCode.ENTRY_DUPLICATED:
                        {
                            var _controllerInLane = (from e in basePresenter._mainView.Lane.ControlUnits where e.Id == ie.DeviceId select e).FirstOrDefault();
                            var eventOutData = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
                            if (eventInResponse == null)
                            {
                                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                                basePresenter.ExecuteSystemError(true, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                                return;
                            }
                            errorData = eventInResponse.Item2;
                            if (errorData != null)
                            {
                                alarmCode = errorData.GetAbnormalCode();
                                alarmMessage = errorData.ToString();
                                basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                                return;
                            }
                            eventInResponse = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle.Id, lprResult.Vehicle.Code, collection.Id);
                            if (eventInResponse == null || eventInResponse.Item2 != null)
                            {
                                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                                basePresenter.ExecuteSystemError(true, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                                return;
                            }
                            break;
                        }
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.Vehicle.Code, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle?.Id);
                        basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        return;
                }
            }

            var entry = eventInResponse.Item1!;

            LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, basePresenter.lastEvent.DateTimeIn, basePresenter.lastEvent.AccessKey, "Xin mời qua", basePresenter._mainView.Lane.Id, "0");
            if (!entry.OpenBarrier)
            {
                entry.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                entry.AccessKey = lprResult.Vehicle;

                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                basePresenter.NotifyRabbitMQConfirmRequest("Xác nhận mở barrie", lprResult.VehicleImage, panoramaImage, entry);
                result = await basePresenter.ShowConfirmOpenBarrieView(entry, lprResult.Vehicle, basePresenter._mainView.Lane,
                                                                       lprResult.VehicleImage, panoramaImage,
                                                                       nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie));
                basePresenter.lastMessage = string.Empty;

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Not Confirm Open Barrie"));
                    await AppData.ApiServer.OperatorService.Entry.DeleteByIdAsync(entry.Id);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyRabbitMQCancelRequest(entry);
                    }
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie),
                                                                  nameof(KZUIStyles.CurrentResources.TryAgain),
                                                                  EmImageDialogType.Error, lprResult.PlateNumber, null, lprResult.Vehicle,
                                                                  lprResult.VehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = entry;

            SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.XIN_MOI_QUA);

            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ie.DeviceId, baseLoopLog);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - Display Valid Event"));
            await basePresenter.ExcecuteValidEvent(null, lprResult.Vehicle, collection, lprResult.PlateNumber, ie.EventTime,
                                   panoramaImage, lprResult.VehicleImage, lprResult.LprImage, entry);
        }
        #endregion

        #region EXIT EVENT
        public async Task ExecuteExitEvent(InputEventArgs ie)
        {
            SystemUtils.logger.SaveSystemLog(new SystemLog(EmSystemAction.Application, EmSystemActionDetail.EXIT_EVENT, EmSystemActionType.INFO, $"{basePresenter._mainView.Lane.Name}.Exit.{ie.InputIndex}"));
            var lprResult = await LaneHelper.LoopLprDetection(basePresenter._mainView.UcCameraList, basePresenter._mainView.Lane.Id, basePresenter._mainView.Lane.Cameras, false);
            if (basePresenter.lastEvent == null || !basePresenter.isAllowOpenBarrieManual)
            {
                _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.PlateNumber, "", EmAlarmCode.BARRIER_OPENED_BY_BUTTON, basePresenter._mainView.UcCameraList, "");
            }
        }
        #endregion
    }
}
