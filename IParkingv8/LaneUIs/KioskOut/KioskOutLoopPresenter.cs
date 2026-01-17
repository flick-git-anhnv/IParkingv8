using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.Style;
using IParkingv8.Helpers;
using IParkingv8.Helpers.Alarms;
using Kztek.Object;
using Kztek.Tool;
using System.Drawing.Imaging;
using static Kztek.Object.InputTupe;

namespace IParkingv8.LaneUIs.KioskOut
{
    public class KioskOutLoopPresenter(KioskOutBasePresenter basePresenter)
    {
        #region Xử Lý Sự Kiện Loop
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
        #endregion END EXIT EVENT

        #region LOOP EVENT
        public async Task ExecuteLoopEvent(InputEventArgs ie)
        {
            string baseLoopLog = $"{basePresenter._mainView.Lane.Name}.Loop.{ie.InputIndex}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent(baseLoopLog + "- START"));
            basePresenter.OpenHomePage();
            basePresenter._mainView.ShowLoadingIndicator("", "");

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - START DETECT PLATE"));
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessReadingPlate);

            var lprResult = await LaneHelper.LoopLprDetection(basePresenter._mainView.UcCameraList, basePresenter._mainView.Lane.Id, basePresenter._mainView.Lane.Cameras);

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
            _ = AlarmProcess.SaveVehicleOnLoopEventAsync(basePresenter._mainView.Lane,imageBytes, lprResult.PlateNumber);
            if (string.IsNullOrEmpty(lprResult.PlateNumber) || lprResult.PlateNumber.Length < 5 || lprResult.Vehicle == null)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - VEHICLE NULL - END"));
                basePresenter._mainView.HideLoadingIndicator();
                _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.InfoTitle),
                                                        nameof(KZUIStyles.CurrentResources.InvalidPlateNumber),
                                                        EmImageDialogType.Infor, lprResult.PlateNumber, null, lprResult?.VehicleImage, panoramaImage);
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_TRA_THE);
                return;
            }

            Collection collection = lprResult.Vehicle.Collection!;
            var collectionType = collection.GetAccessKeyGroupType();

            if (lprResult.Vehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VEHICLE_LOCKED);
                _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.InfoTitle, KZUIStyles.CurrentResources.VehicleLocked, EmImageDialogType.Infor,
                                            lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, lprResult.PanoramaImage);
                return;
            }


            if (!collection.GetExitByLoop())
            {
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_TRA_THE);

                _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.InfoTitle, KZUIStyles.CurrentResources.VehicleNotAllowEntryByPlate, EmImageDialogType.Infor,
                                                          lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, lprResult.PanoramaImage);
                return;
            }

            if (!(collection?.GetActiveLanes()?.Contains(basePresenter._mainView.Lane.Id) ?? false))
            {
                basePresenter._mainView.HideLoadingIndicator();
                string message = $"{collection?.Name} không được phép sử dụng với làn {basePresenter._mainView.Lane.Name}";
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopLog} - {message} - END"));
                _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.InvalidPermission, KZUIStyles.CurrentResources.TryAgain,
                                    EmImageDialogType.Error,
                                    lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, panoramaImage);
                return;
            }

            Tuple<ExitData, string>? checkOutResult = null;
            switch (collectionType)
            {
                case EmAccessKeyGroupType.MONTHLY:
                    checkOutResult = await ExecuteLoopMONTH(ie, baseLoopLog, lprResult, collection, panoramaImage);
                    break;
                case EmAccessKeyGroupType.VIP:
                    checkOutResult = await ExecuteLoopVIP(ie, baseLoopLog, lprResult, collection, panoramaImage);
                    break;
                default:
                    break;
            }

            if (checkOutResult == null) return;

            var exit = checkOutResult.Item1;
            LedHelper.DisplayLed(basePresenter.lastEvent.PlateNumber, DateTime.Now, basePresenter.lastEvent.AccessKey, "Hẹn gặp lại ", basePresenter._mainView.Lane.Id, exit.Amount.ToString());
            if (!exit.OpenBarrier)
            {
                //Cập nhật lại thông tin thiết bị (Server không trả về) 
                exit.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };

                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                basePresenter.NotifyConfirmRequest("Xác nhận mở barrie", lprResult.VehicleImage, panoramaImage, exit);
                result = await basePresenter.ShowConfirmMonthlyPlate(exit, lprResult.VehicleImage ?? panoramaImage,
                        nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie),
                        (Control)basePresenter._mainView);
                basePresenter.lastMessage = string.Empty;

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopLog} - Not Confirm Open Barrie"));
                    string exitId = exit.Id;
                    await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyCancelRequest(exit);
                    }
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie, KZUIStyles.CurrentResources.TryAgain,
                                              EmImageDialogType.Error, lprResult.PlateNumber, null, lprResult.Vehicle, lprResult.VehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            basePresenter.lastEvent = exit;
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ie.DeviceId, baseLoopLog);
            SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.HEN_GAP_LAI);

            await basePresenter.ExcecuteValidEvent(null, lprResult.Vehicle, collection, lprResult.PlateNumber,
                                                   ie.EventTime, panoramaImage, lprResult.VehicleImage, lprResult.LprImage, exit);
        }
        private async Task<Tuple<ExitData, string>?> ExecuteLoopMONTH(GeneralEventArgs ie, string baseLoopEventLog, LoopLprResult lprResult, Collection collection, Image? panoramaImage)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - START CHECK OUT"));
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopEventLog} - Send Check Out Normal Request"));
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckOut + " " + lprResult.PlateNumber);

            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle!.Id, lprResult.PlateNumber, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            var validateExitResponse = await basePresenter.ValidateExitResponse(exitResponse, collection, lprResult.PlateNumber, null, lprResult.Vehicle, ie, lprResult.VehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLoopEventLog} - Check Event Out Response", validateExitResponse));

            if (!validateExitResponse.IsValidEvent && !validateExitResponse.IsNeedConfirm)
            {
                return null;
            }

            return Tuple.Create<ExitData, string>(validateExitResponse.EventData!, validateExitResponse.ErrorMessage);
        }
        private async Task<Tuple<ExitData, string>?> ExecuteLoopVIP(GeneralEventArgs ie, string baseLoopEventLog, LoopLprResult lprResult, Collection collection, Image? panoramaImage)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateLoopEvent($"{baseLoopEventLog} - SEND CHECK OUT NORMAL REQUEST"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle!.Id, lprResult.PlateNumber, collection.Id);
            if (exitResponse == null)
            {
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                basePresenter.ExecuteSystemError(true, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                return null;
            }
            if (exitResponse.Item1 == null && exitResponse.Item2 == null)
            {
                SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                return null;
            }

            var exitData = exitResponse.Item1;
            var errorData = exitResponse.Item2;
            EmAlarmCode alarmCode = EmAlarmCode.NONE;
            string alarmDescription;
            string alarmMessage = "";

            //Sự kiện lỗi, lấy thông tin lỗi và lưu lại cảnh báo
            if (errorData is not null)
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                    return null;
                }

                alarmCode = errorData.GetAbnormalCode();
                alarmMessage = errorData.ToString();
                switch (alarmCode)
                {
                    case EmAlarmCode.ACCESS_KEY_LOCKED:
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                        alarmDescription = "Tên: " + lprResult.Vehicle.Name + " - BSĐK: " + lprResult.Vehicle.Code;
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.PlateNumber, alarmDescription, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle.Id);
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyLocked), lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        break;
                    case EmAlarmCode.ACCESS_KEY_EXPIRED:
                        alarmDescription = "";
                        SoundHelper.PlaySound(ie.DeviceId, EmSystemSoundType.ACCESS_KEY_EXPIRED);
                        if (lprResult.Vehicle.ExpireTime != null)
                        {
                            alarmDescription = "Ngày hết hạn: " + lprResult.Vehicle.ExpireTime.Value.ToString("HH:mm:ss dd/MM/yyyy");
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.PlateNumber, alarmDescription, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle.Id);
                        basePresenter.ExecuteUnvalidEvent(nameof(KZUIStyles.CurrentResources.AccessKeyExpired), lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        break;

                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        var eventInData = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, lprResult.Vehicle.Id, lprResult.PlateNumber, collection.Id);
                        if (eventInData != null && eventInData.Item1 != null)
                        {
                            return await ExecuteLoopVIP(ie, baseLoopEventLog, lprResult, collection, panoramaImage);
                        }
                        else
                        {
                            basePresenter.ExecuteSystemError(true, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        }
                        return null;
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, lprResult.PlateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, lprResult.Vehicle.Id);
                        basePresenter.ExecuteSystemError(false, lprResult.PlateNumber, lprResult.VehicleImage, panoramaImage, null, lprResult.Vehicle);
                        return null;
                }
            }

            return Tuple.Create<ExitData, string>(exitData!, alarmMessage);
        }
        #endregion END LOOP EVENT

        #endregion
    }
}
