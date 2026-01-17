using iParkingv5.Lpr;
using iParkingv5.Objects.Events;
using iParkingv8.Object.Enums.Bases;
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
using Kztek.Tool;
using Newtonsoft.Json.Linq;

namespace IParkingv8.LaneUIs.KioskOut
{
    public class KioskOutCardPresenter(KioskOutBasePresenter basePresenter)
    {
        public const int READER_DAILY = 1;
        public const int READER_MONTHLY = 2;

        public async Task ExcecuteCardEvent(CardEventArgs ce)
        {
            basePresenter.OpenHomePage();

            if (Environment.MachineName != "LEGION-PRO")
            {
                basePresenter._mainView.ShowLoadingIndicator("", KZUIStyles.CurrentResources.ProcessChecking);
            }
            string baseLog = $"{basePresenter._mainView.Lane.Name}.CARD.{ce.PreferCard}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - START"));
            List<CardEventArgs> tempList = basePresenter.LastCardEventDatas;
            bool isNewCardEvent = CardBaseProcess.CheckNewCardEvent(ce, out int thoiGianCho, ref tempList);
            basePresenter.LastCardEventDatas = tempList;
            //if (!isNewCardEvent)
            //{
            //    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - In Waiting Time: {thoiGianCho}s"));
            //    RejectCard(ce.ReaderIndex, ce.DeviceId);
            //    _ = basePresenter.ShowNotifyDialog(EmLanguageKey.NOTIFY_ACCESS_KEY_IN_WAITING_TIME,
            //                                       KZUIStyles.CurrentResources.TryAgainLater,
            //                                       EmImageDialogType.Infor, null);
            //    return;
            //}

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
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                if (cardValidate.AccessKey != null)
                {
                    bool _isCar = cardValidate.AccessKey.Collection?.GetVehicleType() == EmVehicleType.CAR;
                    if (cardValidate.AccessKey.Collection?.GetAccessKeyGroupType() == EmAccessKeyGroupType.DAILY)
                    {
                        var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, _isCar, null);
                        _ = basePresenter.ShowNotifyDailyDialog(cardValidate.DisplayAlarmMessageTag, KZUIStyles.CurrentResources.TryAgainLater,
                                                                EmImageDialogType.Error,
                                                               _result.PlateNumber, cardValidate.AccessKey,
                                                               _result.VehicleImage, _result.PanoramaImage);
                    }
                    else
                    {
                        var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, _isCar, null);
                        _ = basePresenter.ShowNotifyMonthlyDialog(cardValidate.DisplayAlarmMessageTag, KZUIStyles.CurrentResources.TryAgainLater, EmImageDialogType.Error,
                                                                _result.PlateNumber, cardValidate.AccessKey, cardValidate.AccessKey.GetVehicleInfo(),
                                                                _result.VehicleImage, _result.PanoramaImage);
                    }
                }
                else
                {
                    var _result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, false, null);
                    _ = basePresenter.ShowNotifyDailyDialog(cardValidate.DisplayAlarmMessageTag, KZUIStyles.CurrentResources.TryAgainLater,
                                                            EmImageDialogType.Error,
                                                            _result.PlateNumber, cardValidate.AccessKey,
                                                            _result.VehicleImage, _result.PanoramaImage);
                }
                return;
            }
            AccessKey accessKeyResponse = cardValidate.AccessKey!;
            #endregion

            var collection = accessKeyResponse.Collection!;
            bool isCar = collection.GetVehicleType() == EmVehicleType.CAR;

            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessReadingPlate);
            var result = await ProcessCardEvent.GetLPRInfo(basePresenter._mainView.Lane, baseLog, basePresenter._mainView.UcCameraList, isCar, null);

            //Kiểm tra xem có sử dụng đúng vị trí trả thẻ không
            //Thẻ tháng sẽ cần quẹt vào đầu đọc thẻ tháng
            //Thẻ lượt cần đưa vào khe trả thẻ
            switch (collection.GetAccessKeyGroupType())
            {
                //Thẻ lượt nhưng quẹt thẻ vào đầu đọc thẻ tháng
                case EmAccessKeyGroupType.DAILY:
                    if (ce.ReaderIndex != READER_DAILY)
                    {
                        basePresenter._mainView.HideLoadingIndicator();
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.INVALID_READER_DAILY);
                        _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.InvalidReaderDailyTitle),
                                                                nameof(KZUIStyles.CurrentResources.InvalidReaderDailySubTitle),
                                                                EmImageDialogType.Error,
                                                                result.PlateNumber, cardValidate.AccessKey,
                                                                result.VehicleImage, result.PanoramaImage);
                        return;
                    }
                    break;
                //Thẻ tháng nhưng đưa vào khe trả thẻ
                case EmAccessKeyGroupType.MONTHLY:
                case EmAccessKeyGroupType.VIP:
                    if (ce.ReaderIndex != READER_MONTHLY)
                    {
                        basePresenter._mainView.HideLoadingIndicator();
                        _ = ControllerHelper.RejectCard(basePresenter._mainView, ce.DeviceId);
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.INVALID_READER_MONTHLY);
                        _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.InvalidReaderMonthlyTitle,
                                                                  KZUIStyles.CurrentResources.InvalidReaderMonthlySubTitle, EmImageDialogType.Error,
                                                                  result.PlateNumber, cardValidate.AccessKey, accessKeyResponse.GetVehicleInfo(),
                                                                  result.VehicleImage, result.PanoramaImage);
                        return;
                    }
                    break;
                default:
                    break;
            }

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
                        await ExecuteDAILYCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, panoramaImage, vehicleImage, lprImage);
                        break;
                    case EmAccessKeyGroupType.MONTHLY:
                        await ExecuteMONTHCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, panoramaImage, vehicleImage, lprImage);
                        break;
                    case EmAccessKeyGroupType.VIP:
                        await ExcecuteVIPCardEvent(accessKeyResponse, collection, plateNumber, standardlizePlate, ce, panoramaImage, vehicleImage, lprImage);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                basePresenter._mainView.HideLoadingIndicator();
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("ExcecuteCardEvent", ex));
                basePresenter.ExecuteSystemError(false, plateNumber, result.VehicleImage, result.PanoramaImage, null, null);
            }
        }

        #region DAILY CARD
        private async Task ExecuteDAILYCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlate,
                                                 CardEventArgs ce, Image? panoramaImage, Image? vehicleImage, Image? lprImage)
        {
            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckOut);
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.DAILY.Card.{accessKey.Code}.{accessKey.Name}";
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, accessKey.Id, standardlizePlate, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            var validateExitResponse = await basePresenter.ValidateExitResponse(exitResponse, collection, plateNumber, accessKey, null, ce, vehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Check Out Normal Response", validateExitResponse));

            if (!validateExitResponse.IsValidEvent && !validateExitResponse.IsNeedConfirm)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                return;
            }

            ExitData exit = validateExitResponse.EventData!;
            string errorMessage = validateExitResponse.ErrorMessage;
            LedHelper.DisplayLed(plateNumber, DateTime.Now, accessKey, "Hẹn gặp lại ", basePresenter._mainView.Lane.Id, exit.Amount.ToString());
            //Nếu có cảnh báo hoặc ko tự động mở barrie thì cần xác nhận
            if (!string.IsNullOrEmpty(errorMessage) || !exit.OpenBarrier)
            {
                //Cập nhật lại thông tin thiết bị (Server không trả về) 
                exit.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };
                var appliedVoucherDataIn = await AppData.ApiServer.PaymentService.GetAppliedVoucherDataAsync(exit.Entry.Id) ?? [];
                exit.Vouchers = appliedVoucherDataIn;

                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                //Không có cảnh báo nhưng không tự động mở barrie - Kiểm tra xem phát sinh phí thì yêu cầu thanh toán
                if (string.IsNullOrEmpty(errorMessage))
                {
                    //Phát sinh phí thì yêu cầu thanh toán
                    //Không phát sinh phí thì chờ xác nhận mở barrie
                    long fee = exit.Amount - exit.DiscountAmount - exit.Entry!.Amount;
                    //Yêu cầu thanh toán
                    if (fee > 0)
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VUI_LONG_THANH_TOAN_PHI_GUI_XE);
                        result = await basePresenter.ShowConfirmPaymentRequest(exit, vehicleImage ?? panoramaImage);

                        if (result.IsConfirm)
                        {
                            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VUI_LONG_CHON_HINH_THUC_THANH_TOAN);
                            result = await basePresenter.PayParkingFeeAsync(exit, (Control)basePresenter._mainView);
                        }
                    }
                    //Chờ xác nhận mở barrie
                    else
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                        basePresenter.NotifyConfirmRequest("Xác nhận mở barrie", vehicleImage, panoramaImage, exit);
                        result = await basePresenter.ShowConfirmDailyPlate(exit, vehicleImage ?? panoramaImage,
                                                    nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie),
                                                    (Control)basePresenter._mainView);
                        basePresenter.lastMessage = string.Empty;
                    }
                }

                //Có cảnh báo, hiển thị cảnh báo
                else
                {
                    //B1: Hiển thị cảnh báo biển số
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                    basePresenter.NotifyConfirmRequest(validateExitResponse.AlarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ? "Biển số vào ra không khớp" : "Biển số không khớp với biển đăng ký",
                                                        vehicleImage, panoramaImage, exit);

                    var messageKey = validateExitResponse.AlarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ?
                                                       nameof(KZUIStyles.CurrentResources.PlateInOutNotSame) :
                                                       nameof(KZUIStyles.CurrentResources.PlateNotMatchWithSystem);
                    result = await basePresenter.ShowConfirmDailyPlate(exit, vehicleImage ?? panoramaImage, messageKey, (Control)basePresenter._mainView);
                    basePresenter.lastMessage = string.Empty;
                    if (result.IsConfirm)
                    {
                        //B2: Hiển thị yêu cầu thanh toán nếu có phát sinh phí
                        long fee = exit.Amount - exit.DiscountAmount - exit.Entry!.Amount;
                        if (fee > 0)
                        {
                            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VUI_LONG_THANH_TOAN_PHI_GUI_XE);
                            result = await basePresenter.ShowConfirmPaymentRequest(exit, vehicleImage ?? panoramaImage);
                            if (result.IsConfirm)
                            {
                                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VUI_LONG_CHON_HINH_THUC_THANH_TOAN);
                                result = await basePresenter.PayParkingFeeAsync(exit, (Control)basePresenter._mainView);
                            }
                        }
                    }
                }

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    LedHelper.DisplayDefaultLed(basePresenter._mainView.Lane.Id);
                    _ = ControllerHelper.RejectCard(basePresenter._mainView, ce.DeviceId);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    string exitId = exit.Id;
                    await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyCancelRequest(exit);
                    }
                    else
                    {
                        SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                        _ = basePresenter.ShowNotifyDailyDialog(nameof(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie),
                                                                nameof(KZUIStyles.CurrentResources.TryAgain),
                                                                EmImageDialogType.Error, plateNumber, accessKey, vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            //Sự kiện hợp lệ ra lệnh thu thẻ, lưu thông tin sự kiện, mở barrie, và hiển thị thông báo
            basePresenter.lastEvent = exit;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            CollectCard(ce.ReaderIndex, ce.DeviceId);
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);
            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.HEN_GAP_LAI);

            await basePresenter.ExcecuteValidEvent(accessKey, null, collection, plateNumber,
                                                  ce.EventTime, panoramaImage, vehicleImage, lprImage, exit);


        }
        #endregion END DAILY CARD

        #region MONTH CARD
        private async Task ExecuteMONTHCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlateNumber,
                                                 CardEventArgs ce, Image? panoramaImage, Image? vehicleImage, Image? lprImage)
        {
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.Card.MONTH.{accessKey.Code}.{accessKey.Name}";

            var monthCardValidate = await CardMonthProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, basePresenter._mainView, basePresenter._mainView.ucSelectVehicles);
            if (monthCardValidate.MonthCardValidateType != EmMonthCardValidateType.SUCCESS)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                basePresenter._mainView.HideLoadingIndicator();
                if (monthCardValidate.RegisterVehicle == null)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_MONTH_NO_VEHICLE);
                    _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.ErrorTitle, KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle,
                                                              EmImageDialogType.Error, plateNumber, accessKey, null, vehicleImage, panoramaImage);
                }
                return;
            }

            AccessKey registeredVehicle = monthCardValidate.RegisterVehicle!;
            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VEHICLE_LOCKED);
                _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.ErrorTitle, KZUIStyles.CurrentResources.VehicleLocked,
                                                          EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                return;
            }

            plateNumber = monthCardValidate.UpdatePlate;
            standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);

            bool isCheckOutByVehicle = monthCardValidate.IsCheckByPlate;
            string checkOutAccessId = (isCheckOutByVehicle ? registeredVehicle!.Id : accessKey.Id) ?? "";

            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckOut);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);
            basePresenter._mainView.HideLoadingIndicator();

            var validateExitResponse = await basePresenter.ValidateExitResponse(exitResponse, collection, plateNumber, accessKey, registeredVehicle, ce, vehicleImage, panoramaImage);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Check Event Out Response", validateExitResponse));

            if (!validateExitResponse.IsValidEvent && !validateExitResponse.IsNeedConfirm)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                return;
            }

            ExitData exit = validateExitResponse.EventData!;
            string errorMessage = validateExitResponse.ErrorMessage;
            LedHelper.DisplayLed(plateNumber, DateTime.Now, accessKey, "Hẹn gặp lại ", basePresenter._mainView.Lane.Id, exit.Amount.ToString());

            if (!string.IsNullOrEmpty(errorMessage) || !exit.OpenBarrier)
            {
                //Cập nhật lại thông tin thiết bị (Server không trả về) 
                exit.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;
                //Không có cảnh báo nhưng không tự động mở barrie - Chờ xác nhận mở barrie
                if (string.IsNullOrEmpty(errorMessage))
                {
                    basePresenter.NotifyConfirmRequest("Xác nhận mở barrie", vehicleImage, panoramaImage, exit);
                    result = await basePresenter.ShowConfirmMonthlyPlate(exit, vehicleImage ?? panoramaImage,
                                                                            nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie),
                                                                         (Control)basePresenter._mainView);
                    basePresenter.lastMessage = string.Empty;
                }

                //Có cảnh báo, hiển thị cảnh báo
                else
                {
                    //B1: Hiển thị cảnh báo biển số
                    basePresenter.NotifyConfirmRequest(validateExitResponse.AlarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ?
                                                "Biển số vào ra không khớp" : "Biển số không khớp với biển đăng ký",
                                                vehicleImage, panoramaImage, exit);

                    var messageTag = validateExitResponse.AlarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ?
                                                           nameof(KZUIStyles.CurrentResources.PlateInOutNotSame) :
                                                           nameof(KZUIStyles.CurrentResources.PlateNotMatchWithSystem);
                    result = await basePresenter.ShowConfirmMonthlyPlate(exit, vehicleImage ?? panoramaImage,
                                                                         messageTag,
                                                                         (Control)basePresenter._mainView);
                    basePresenter.lastMessage = string.Empty;
                }

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    LedHelper.DisplayDefaultLed(basePresenter._mainView.Lane.Id);
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    string exitId = exit.Id;
                    await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyCancelRequest(exit);
                    }
                    else
                    {
                        await basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie, KZUIStyles.CurrentResources.TryAgain,
                                               EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            //Sự kiện hợp lệ ra lệnh thu thẻ, lưu thông tin sự kiện, mở barrie, và hiển thị thông báo
            basePresenter.lastEvent = exit;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            CollectCard(ce.ReaderIndex, ce.DeviceId);
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);
            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.HEN_GAP_LAI);

            await basePresenter.ExcecuteValidEvent(accessKey, registeredVehicle!, collection, plateNumber, ce.EventTime, panoramaImage, vehicleImage, lprImage, exit);
        }
        #endregion END MONTH CARD

        #region VIP CARD
        private async Task ExcecuteVIPCardEvent(AccessKey accessKey, Collection collection, string plateNumber, string standardlizePlateNumber,
                                                CardEventArgs ce, Image? panoramaImage, Image? vehicleImage, Image? lprImage)
        {
            string baseCardEventLog = $"{basePresenter._mainView.Lane.Name}.Card.VIP.{accessKey.Code}.{accessKey.Name}";

            var vipCardValidate = await CardVipProcess.ValidateAccessKeyByCode(accessKey, standardlizePlateNumber, basePresenter._mainView, basePresenter._mainView.ucSelectVehicles);
            if (vipCardValidate.VipCardValidateType != EmVipCardValidateType.SUCCESS)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.ACCESS_KEY_MONTH_NO_VEHICLE);

                if (vipCardValidate.RegisterVehicle == null)
                {
                    _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.ErrorTitle,
                                                              KZUIStyles.CurrentResources.AccessKeyMonthNoVehicle,
                                                EmImageDialogType.Error, plateNumber, accessKey, null, vehicleImage, panoramaImage);
                }
                return;
            }

            AccessKey? registeredVehicle = vipCardValidate.RegisterVehicle!;
            if (registeredVehicle.Status == EmAccessKeyStatus.LOCKED)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                basePresenter._mainView.HideLoadingIndicator();
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.VEHICLE_LOCKED);
                _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.ErrorTitle,
                                                          KZUIStyles.CurrentResources.VehicleLocked,
                                                          EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                return;
            }

            plateNumber = vipCardValidate.UpdatePlate;
            standardlizePlateNumber = PlateHelper.StandardlizePlateNumber(plateNumber, true);
            bool isCheckOutByVehicle = vipCardValidate.IsCheckByPlate;
            string checkOutAccessId = (isCheckOutByVehicle ? registeredVehicle!.Id : accessKey.Id) ?? "";

            basePresenter._mainView.UpdateLoadingIndicator("", KZUIStyles.CurrentResources.ProcessCheckOut);
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Send Check Out Normal Request"));
            var exitResponse = await AppData.ApiServer.OperatorService.Exit.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);

            if (exitResponse == null)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU);
                basePresenter.ExecuteSystemError(true, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                return;
            }
            if (exitResponse.Item1 == null && exitResponse.Item2 == null)
            {
                RejectCard(ce.ReaderIndex, ce.DeviceId);
                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                return;
            }

            var exitData = exitResponse.Item1;
            var errorData = exitResponse.Item2;
            EmAlarmCode alarmCode = EmAlarmCode.NONE;
            string alarmDescription = "";
            string alarmMessage = "";
            //Sự kiện lỗi, lấy thông tin lỗi và lưu lại cảnh báo
            if (errorData is not null)
            {
                if (errorData.Fields == null || errorData.Fields.Count == 0)
                {
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                    return;
                }

                alarmCode = errorData.GetAbnormalCode();
                alarmMessage = errorData.ToString();

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
                        break;
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
                        break;
                    case EmAlarmCode.ENTRY_NOT_FOUND:
                        //Tạo 1 sự kiện vào cho sự kiện này
                        var eventInData = await AppData.ApiServer.OperatorService.Entry.CreateAsync(Guid.NewGuid().ToString(), basePresenter._mainView.Lane.Id, checkOutAccessId, standardlizePlateNumber, collection.Id);

                        if (eventInData == null || eventInData.Item1 == null)
                        {
                            basePresenter.ExecuteSystemError(true, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        }
                        else
                        {
                            await ExcecuteVIPCardEvent(accessKey, collection, plateNumber, standardlizePlateNumber, ce, panoramaImage, vehicleImage, lprImage);
                        }
                        return;
                    default:
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmMessage, alarmCode, basePresenter._mainView.UcCameraList, accessKey?.Id);
                        basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                        return;
                }
            }
            //Không lỗi thì kiểm tra có bị cảnh báo biển số hay không
            else
            {
                var alarmCodes = exitData!.GetAlarmCode();
                if (alarmCodes.Count > 0)
                {

                    var errorMessages = exitData.ToVI() ?? [];
                    alarmMessage = string.Join(Environment.NewLine, errorMessages);

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
                                alarmCode = EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM;
                                if (registeredVehicle != null)
                                {
                                    alarmDescription = "BSĐK: " + registeredVehicle.Code;
                                    accessKeyId = registeredVehicle.Id;
                                }
                                else if (accessKey != null)
                                {
                                    alarmDescription = "BSĐK: " + (accessKey.GetVehicleInfo()?.Code ?? "");
                                    accessKeyId = accessKey.Id;
                                }
                                else
                                {
                                    alarmDescription = "";
                                }
                                break;
                            case EmAlarmCode.PLATE_NUMBER_INVALID:
                                alarmCode = EmAlarmCode.PLATE_NUMBER_INVALID;
                                alarmDescription = "BS vào: " + (exitData.Entry?.PlateNumber ?? "");
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
                                basePresenter.ExecuteSystemError(false, plateNumber, vehicleImage, panoramaImage, accessKey, registeredVehicle);
                                break;
                        }
                        _ = AlarmProcess.SaveAlarmAsync(basePresenter._mainView.Lane, plateNumber, alarmDescription, _alarmCode, basePresenter._mainView.UcCameraList, accessKeyId);

                    }
                }
            }

            ExitData exit = exitResponse.Item1!;
            LedHelper.DisplayLed(plateNumber, DateTime.Now, accessKey, "Hẹn gặp lại ", basePresenter._mainView.Lane.Id, exit.Amount.ToString());

            if (!string.IsNullOrEmpty(alarmMessage) || !exit.OpenBarrier)
            {
                //Cập nhật lại thông tin thiết bị (Server không trả về) 
                exit.Device = new BaseDevice() { Name = basePresenter._mainView.Lane.Name, Id = basePresenter._mainView.Lane.Id };

                SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE);
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Show Confirm Open Barrie Request"));
                BaseKioskResult result;

                //Không có cảnh báo nhưng không tự động mở barrie - Chờ xác nhận mở barrie
                if (string.IsNullOrEmpty(alarmMessage))
                {
                    basePresenter.NotifyConfirmRequest("Xác nhận mở barrie", vehicleImage, panoramaImage, exit);
                    result = await basePresenter.ShowConfirmMonthlyPlate(exit, vehicleImage ?? panoramaImage, nameof(KZUIStyles.CurrentResources.WaitConfirmOpenBarrie), (Control)basePresenter._mainView);
                    basePresenter.lastMessage = string.Empty;
                }

                //Có cảnh báo, hiển thị cảnh báo
                else
                {
                    //B1: Hiển thị cảnh báo biển số
                    basePresenter.NotifyConfirmRequest(alarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ?
                                                "Biển số vào ra không khớp" : "Biển số không khớp với biển đăng ký",
                                         vehicleImage, panoramaImage, exit);

                    var messageTag = alarmCode == EmAlarmCode.PLATE_NUMBER_INVALID ?
                                                       nameof(KZUIStyles.CurrentResources.PlateInOutNotSame) :
                                                        nameof(KZUIStyles.CurrentResources.PlateNotMatchWithSystem);
                    result = await basePresenter.ShowConfirmMonthlyPlate(exit, vehicleImage ?? panoramaImage, messageTag, (Control)basePresenter._mainView);
                    basePresenter.lastMessage = string.Empty;
                }

                //Nếu bảo vệ không xác nhận hoặc khách hàng bấm back thì revert sự kiện và ra lệnh nhả thẻ
                //Nếu khách hàng bấm back thì gửi lệnh hủy yêu cầu xác nhận lên server
                //Nếu bảo vệ không xác nhận thì hiển thị thông báo cho khách hàng
                if (!result.IsConfirm)
                {
                    LedHelper.DisplayDefaultLed(basePresenter._mainView.Lane.Id);
                    SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE);
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Not Confirm Open Barrie"));
                    string exitId = exit.Id;
                    await AppData.ApiServer.OperatorService.Exit.DeleteByIdAsync(exitId);

                    if (result.kioskUserType == EmKioskUserType.Customer)
                    {
                        basePresenter.NotifyCancelRequest(exit);
                    }
                    else
                    {
                        _ = basePresenter.ShowNotifyMonthlyDialog(KZUIStyles.CurrentResources.SecurityNotConfirmOpenBarrie, KZUIStyles.CurrentResources.TryAgain,
                                       EmImageDialogType.Error, plateNumber, accessKey, registeredVehicle, vehicleImage, panoramaImage);
                    }
                    return;
                }
            }

            //Sự kiện hợp lệ ra lệnh thu thẻ, lưu thông tin sự kiện, mở barrie, và hiển thị thông báo
            basePresenter.lastEvent = exit;
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Display Valid Event"));
            CollectCard(ce.ReaderIndex, ce.DeviceId);
            ControllerHelper.OpenBarrie(basePresenter._mainView, collection, ce.DeviceId, baseCardEventLog);
            SoundHelper.PlaySound(ce.DeviceId, EmSystemSoundType.HEN_GAP_LAI);

            await basePresenter.ExcecuteValidEvent(accessKey, registeredVehicle!, collection, plateNumber,
                       ce.EventTime, panoramaImage, vehicleImage, lprImage, exit);
        }
        #endregion END VIP CARD

        private bool IsDailyReader(int reader) => reader == READER_DAILY;
        private bool RejectCard(int reader, string deviceId)
        {
            if (!IsDailyReader(reader))
            {
                return true;
            }
            _ = ControllerHelper.RejectCard(basePresenter._mainView, deviceId);
            return true;
        }
        private bool CollectCard(int reader, string deviceId)
        {
            if (!IsDailyReader(reader))
            {
                return true;
            }
            _ = ControllerHelper.CollecCard(basePresenter._mainView, deviceId);
            return true;
        }
    }
}
