using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using IParkingv8.Helpers.CardProcess;
using Kztek.Control8.UserControls;

namespace IParkingv8.Helpers.Alarms
{
    public class AlarmProcess
    {
        public static async Task InvokeCardValidateAsync(BaseCardValidate cardValidate, Lane lane, KZUI_UcCameraList ucCameraList)
        {
            if (cardValidate.CardValidateType == EmAlarmCode.NONE)
            {
                return;
            }
            var imageDatas = await ucCameraList.GetAllCameraImageAsync(lane.Cameras);
            var alarmResponse = await AppData.ApiServer.OperatorService.Alarm.CreateAsync(lane.Id, "", cardValidate.CardValidateType,
                                                                                          cardValidate.AlarmDescription ?? "",
                                                                                          cardValidate.AccessKey?.Id ?? "");
            await LaneHelper.SaveAlarmImage(imageDatas, alarmResponse?.Id ?? "");
        }


        public static async Task SaveVehicleOnLoopEventAsync(Lane lane, Dictionary<EmImageType, List<List<byte>>> images, string plateNumber)
        {
            if (!AppData.AppConfig.IsSaveVehicleOnLoop)
                return;

            var alarmResponse = await AppData.ApiServer.OperatorService.Alarm.CreateAsync(lane.Id, plateNumber, EmAlarmCode.VEHICLE_ON_LOOP, "", "");
            if (alarmResponse != null && !string.IsNullOrEmpty(alarmResponse.Id))
            {
                await LaneHelper.SaveAlarmImage(images, alarmResponse.Id);
            }
            //await SaveAlarmAsync(lane, plateNumber panoramaImage, vehicleImage, lprImage, plateNumber);
            //try
            //{
            //    var alarmResponse = await AppData.ApiServer.OperatorService.Alarm
            //        .CreateAsync(lane.Id, plateNumber, EmAlarmCode.VEHICLE_ON_LOOP, "", "")
            //        .ConfigureAwait(false);

            //    if (alarmResponse == null || string.IsNullOrEmpty(alarmResponse.Id))
            //        return;

            //    // Tạo các task KHÔNG-NULL: nếu không có ảnh -> trả về Task.FromResult<List<byte>?>(null)
            //    Task<List<byte>?> panoTask =
            //        panoramaImage != null
            //            ? panoramaImage.GetByteArrayFromImageAsync()
            //            : Task.FromResult<List<byte>?>(null);

            //    Task<List<byte>?> vehicleTask =
            //        vehicleImage != null
            //            ? vehicleImage.GetByteArrayFromImageAsync()
            //            : Task.FromResult<List<byte>?>(null);

            //    Task<List<byte>?> plateTask =
            //         lprImage != null
            //            ? lprImage.GetByteArrayFromImageAsync()
            //            : Task.FromResult<List<byte>?>(null);

            //    // Chạy song song an toàn (không có phần tử null trong WhenAll)
            //    var results = await Task.WhenAll(panoTask, vehicleTask, plateTask).ConfigureAwait(false);

            //    var imageDatas = new Dictionary<EmImageType, List<List<byte>>>();

            //    if (results[0] is { Count: > 0 } panoBytes)
            //    {
            //        imageDatas[EmImageType.PANORAMA] = new List<List<byte>> { panoBytes };
            //    }

            //    if (results[1] is { Count: > 0 } vehicleBytes)
            //    {
            //        imageDatas[EmImageType.VEHICLE] = new List<List<byte>> { vehicleBytes };
            //    }

            //    if (results[2] is { Count: > 0 } plateBytes)
            //    {
            //        imageDatas[EmImageType.PLATE_NUMBER] = new List<List<byte>> { plateBytes };
            //    }

            //    // Nếu không có ảnh nào, có thể bỏ qua upload ảnh (tuỳ policy)
            //    if (imageDatas.Count > 0)
            //    {
            //        await LaneHelper.SaveAlarmImage(imageDatas, alarmResponse.Id);
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        public static async Task SaveAlarmAsync(Lane lane, string plateNumber, string description, EmAlarmCode abnormalCode, KZUI_UcCameraList ucCameraList, string? accessKeyId)
        {
            var alarmResponse = await AppData.ApiServer.OperatorService.Alarm.CreateAsync(lane.Id, plateNumber, abnormalCode, description, accessKeyId ?? "");
            if (alarmResponse != null && !string.IsNullOrEmpty(alarmResponse.Id))
            {
                var imageDatas = await ucCameraList.GetAllCameraImageAsync(lane.Cameras);
                await LaneHelper.SaveAlarmImage(imageDatas, alarmResponse.Id);
            }
        }
    }
}
