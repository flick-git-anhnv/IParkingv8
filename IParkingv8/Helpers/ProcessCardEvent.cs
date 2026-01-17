using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using Kztek.Control8.UserControls;
using Kztek.Tool;

namespace IParkingv8.Helpers
{
    public static class ProcessCardEvent
    {
        /// <summary>
        /// Lây và hiển thị hình ảnh tử camera + nhận dạng biển số
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="baseCardEventLog"></param>
        /// <param name="ucCameraList"></param>
        /// <param name="ucPlate"></param>
        /// <param name="ucEventImageList"></param>
        /// <param name="isCar"></param>
        /// <param name="overviewImg"></param>
        /// <param name="vehicleImg"></param>
        /// <param name="lprImage"></param>
        public static async Task<EventImagesInfo> GetLPRInfo(Lane lane, string baseCardEventLog, KZUI_UcCameraList ucCameraList, bool isCar, LoopLprResult? lastLoopLprResult)
        {
            int laneIndex = 0;
            var lanes = AppData.Lanes.ToList();
            for (int i = 0; i < lanes.Count(); i++)
            {
                if (lanes[i].Id == lane.Id)
                {
                    laneIndex = i;
                    break;
                }
            }
            if (laneIndex >= AppData.LprDetecter.Count)
            {
                laneIndex = 0;
            }
            var eventImagesInfo = new EventImagesInfo();
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseCardEventLog} - Get Plate"));
            var result = await ucCameraList.GetPlateAsync(lane.Id, AppData.LprDetecter[laneIndex], lane.Cameras, isCar);

            eventImagesInfo.VehicleImage = result.VehicleImage;
            eventImagesInfo.LprImage = result.LprImage;
            eventImagesInfo.PlateNumber = result.PlateNumber;

            if (string.IsNullOrEmpty(eventImagesInfo.PlateNumber) && !string.IsNullOrEmpty(lastLoopLprResult?.PlateNumber))
            {
                if (Math.Abs((lastLoopLprResult.CreatedAt - DateTime.Now).TotalSeconds) <= 10)
                {
                    eventImagesInfo.LprImage = lastLoopLprResult.LprImage;
                    eventImagesInfo.PlateNumber = lastLoopLprResult.PlateNumber;
                    eventImagesInfo.VehicleImage = lastLoopLprResult.VehicleImage;
                    lastLoopLprResult = null;
                }
            }
            eventImagesInfo.PanoramaImage ??= await ucCameraList.GetImageAsync(EmCameraPurpose.Panorama);

            return eventImagesInfo;
        }

        public class CheckActiveRegisterVehicleResult
        {
            public AccessKey? AccessKey { get; set; }
            public bool IsCheckByPlate { get; set; }
            public bool IsEmptyPlates { get; set; }
            public CustomerDto? Customer { get; set; }

            public CheckActiveRegisterVehicleResult(AccessKey? accessKey, bool isCheckByPlate)
            {
                AccessKey = accessKey;
                IsCheckByPlate = isCheckByPlate;
            }
            public static CheckActiveRegisterVehicleResult CreateDefault() => new(null, false);
        }
    }
}
