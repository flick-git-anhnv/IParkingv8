using iParkingv5.Lpr;
using iParkingv5.Lpr.Objects;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Devices;
using iParkingv8.Object.Objects.Devices;
using Kztek.Control8.UserControls;
using System.Drawing.Imaging;

namespace IParkingv8.Helpers
{
    public static class LaneHelper
    {
        /// <summary>
        /// Lưu hình ảnh lên server
        /// Không sử dụng lưu cùng lúc nhiều ảnh, tránh quá tải đường truyền
        /// </summary>
        /// <param name="imagesInfo"></param>
        /// <param name="imageDatas"></param>
        /// <returns></returns>
        public static async Task SaveImage(Dictionary<EmImageType, List<List<byte>>> imageDatas, bool isEventIn, string eventId)
        {
            if (imageDatas == null) return;

            foreach (KeyValuePair<EmImageType, List<List<byte>>> item in imageDatas)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    var imgInfo = item.Value[i];
                    if (isEventIn)
                    {
                        await AppData.ApiServer.OperatorService.Entry.SaveImageAsync(imgInfo, eventId, item.Key);
                    }
                    else
                    {
                        await AppData.ApiServer.OperatorService.Exit.SaveImageAsync(imgInfo, eventId, item.Key);
                    }
                }
            }
        }

        public static async Task SaveAlarmImage(Dictionary<EmImageType, List<List<byte>>> imageDatas, string alarmId)
        {
            if (imageDatas == null || string.IsNullOrEmpty(alarmId)) return;

            List<Task> tasks = [];
            foreach (KeyValuePair<EmImageType, List<List<byte>>> item in imageDatas)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    var imgInfo = item.Value[i];
                    await AppData.ApiServer.OperatorService.Alarm.SaveImageAsync(imgInfo, alarmId, item.Key);
                }
            }
        }
        public static async Task<DetectLprResult> TryGetPlateAsync(KZUI_UcCameraList uc, string laneId, List<CameraInLane> cameraInLanes)
        {
            int laneIndex = 0;
            var lanes = AppData.Lanes.ToList();
            for (int i = 0; i < lanes.Count(); i++)
            {
                if (lanes[i].Id == laneId)
                {
                    laneIndex = i;
                    break;
                }
            }
            if (laneIndex >= AppData.LprDetecter.Count)
            {
                laneIndex = 0;
            }
            var result = await uc.GetPlateAsync(laneId, AppData.LprDetecter[laneIndex], cameraInLanes, true);

            if (!string.IsNullOrEmpty(result.PlateNumber))
            {
                return result;
            }

            var resultMotor = await uc.GetPlateAsync(laneId, AppData.LprDetecter[laneIndex], cameraInLanes, false);
            if (!string.IsNullOrEmpty(resultMotor.PlateNumber))
            {
                return resultMotor;
            }

            if (result.VehicleImage == null)
            {
                result.VehicleImage = resultMotor.VehicleImage;
            }
            return result;
        }
        public static async Task<DetectLprResult> TryGetPlateAsync(KZUI_UcCameraList uc, string laneId, CameraInLane cameraInLane)
        {
            int laneIndex = 0;
            var lanes = AppData.Lanes.ToList();
            for (int i = 0; i < lanes.Count(); i++)
            {
                if (lanes[i].Id == laneId)
                {
                    laneIndex = i;
                    break;
                }
            }
            if (laneIndex >= AppData.LprDetecter.Count)
            {
                laneIndex = 0;
            }
            var result = await uc.GetPlateAsync(laneId, AppData.LprDetecter[laneIndex], cameraInLane, true);

            if (!string.IsNullOrEmpty(result.PlateNumber))
            {
                return result;
            }

            var resultMotor = await uc.GetPlateAsync(laneId, AppData.LprDetecter[laneIndex], cameraInLane, false);
            if (!string.IsNullOrEmpty(resultMotor.PlateNumber))
            {
                return resultMotor;
            }

            if (result.VehicleImage == null)
            {
                result.VehicleImage = resultMotor.VehicleImage;
            }
            return result;
        }

        public static async Task<LoopLprResult> LoopLprDetection(KZUI_UcCameraList uc, string laneId, List<CameraInLane> cameras,
                                                                 bool isCheckPlateInServer = true, bool isForceCar = false)
        {
            LoopLprResult lprResult = new();
            int retry = 0;
            if (AppData.LprConfig.RetakePhotoTimes == 0)
            {
                AppData.LprConfig.RetakePhotoTimes = 1;
            }
            while (retry < AppData.LprConfig.RetakePhotoTimes)
            {
                if (isCheckPlateInServer)
                {
                    foreach (var cameraInLane in cameras)
                    {
                        if (cameraInLane.Purpose == (int)EmCameraPurpose.Panorama)
                        {
                            continue;
                        }
                        if (isForceCar)
                        {
                            if (cameraInLane.Purpose == (int)EmCameraPurpose.MotorLpr)
                            {
                                continue;
                            }
                        }
                        var result = await TryGetPlateAsync(uc, laneId, cameraInLane);
                        lprResult.LprImage = result.LprImage;
                        lprResult.VehicleImage = result.VehicleImage;
                        lprResult.PlateNumber = (result.PlateNumber ?? "").Trim();

                        if (!string.IsNullOrEmpty(lprResult.PlateNumber))
                        {
                            string standardlizePlate = PlateHelper.StandardlizePlateNumber(lprResult.PlateNumber, true);

                            var registeredVehicle = await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(standardlizePlate);
                            if (registeredVehicle != null && registeredVehicle.Item1 != null)
                            {
                                lprResult.Vehicle = registeredVehicle.Item1;
                                return lprResult;
                            }
                        }
                    }
                    retry++;
                    if (retry == AppData.LprConfig.RetakePhotoTimes)
                    {
                        return lprResult;
                    }
                    else
                        await Task.Delay(AppData.LprConfig.RetakePhotoDelay);
                }
                else
                {
                    var result = await TryGetPlateAsync(uc, laneId, cameras);
                    lprResult.LprImage = result.LprImage;
                    lprResult.VehicleImage = result.VehicleImage;
                    lprResult.PlateNumber = (result.PlateNumber ?? "").Trim();

                    if (!isCheckPlateInServer)
                    {
                        return lprResult;
                    }

                    if (!string.IsNullOrEmpty(lprResult.PlateNumber))
                    {
                        string standardlizePlate = PlateHelper.StandardlizePlateNumber(lprResult.PlateNumber, true);

                        var registeredVehicle = await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(standardlizePlate);
                        if (registeredVehicle != null && registeredVehicle.Item1 != null)
                        {
                            lprResult.Vehicle = registeredVehicle.Item1;
                            return lprResult;
                        }
                    }

                    retry++;
                    if (retry == AppData.LprConfig.RetakePhotoTimes)
                    {
                        return lprResult;
                    }
                    await Task.Delay(AppData.LprConfig.RetakePhotoDelay);
                }

            }

            return lprResult;
        }
        private const long JpegQuality = 85L; // dùng long
        public static async Task<List<SaveResult?>> SaveLocalImage(Dictionary<EmImageType, Image?> images, string eventId, string laneName)
        {
            return new List<SaveResult?>();
            var now = DateTime.Now;
            // Nên dùng Path.Combine để tránh dấu '/'
            string folder = Path.Combine(Application.StartupPath,
                                         "LocalImages",
                                         now.ToString("yyyy"),
                                         now.ToString("MM"),
                                         now.ToString("dd"), laneName, eventId);
            Directory.CreateDirectory(folder);

            var results = await Task.WhenAll(
                SaveOneAsync(images.GetValueOrDefault(EmImageType.PANORAMA), folder, "pano", eventId),
                SaveOneAsync(images.GetValueOrDefault(EmImageType.VEHICLE), folder, "vehicle", eventId),
                SaveOneAsync(images.GetValueOrDefault(EmImageType.PLATE_NUMBER), folder, "lpr", eventId),
                SaveOneAsync(images.GetValueOrDefault(EmImageType.FACE), folder, "face", eventId)
            );
            return results.Where(r => r != null).ToList();
        }
        private static async Task<SaveResult?> SaveOneAsync(Image? img, string folder, string prefix, string eventId)
        {
            if (img == null || img.Width == 0 || img.Height == 0) return null;

            // Tạo bitmap "an toàn": 32bpp nếu cần alpha, ngược lại 24bpp
            bool needAlpha = img is Bitmap b && Image.IsAlphaPixelFormat(b.PixelFormat);
            using var safe = MakeSafeBitmap(img, needAlpha);

            string ext = needAlpha ? "png" : "jpg";
            string idPart = TryGuid(eventId, out var gid) ? gid.ToString("N") : Sanitize(eventId);
            string fileName = $"{prefix}.{ext}";
            string path = Path.Combine(folder, fileName);

            using var ms = new MemoryStream();
            if (!needAlpha)
            {
                var jpegEncoder = GetEncoder(ImageFormat.Jpeg);
                if (jpegEncoder != null)
                {
                    using var encParams = new EncoderParameters(1);
                    encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, JpegQuality);
                    safe.Save(ms, jpegEncoder, encParams);
                }
                else
                {
                    safe.Save(ms, ImageFormat.Jpeg);
                }
            }
            else
            {
                safe.Save(ms, ImageFormat.Png);
            }

            ms.Position = 0;
            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 64 * 1024, useAsync: true);
            await ms.CopyToAsync(fs).ConfigureAwait(false);
            await fs.FlushAsync().ConfigureAwait(false);

            return new SaveResult { Path = path, Type = prefix };
        }
        public static async Task DeleteLocalImagesAfterDays(int days)
        {
            try
            {
                DateTime beforeDay = DateTime.Now.AddDays(-1 * days);
                string path = Path.Combine(Application.StartupPath, $"LocalImages/{beforeDay.Year}/{beforeDay.Month:00}/{beforeDay.Day:00}");
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception)
            {
            }
        }

        private static Bitmap MakeSafeBitmap(Image src, bool withAlpha)
        {
            var pf = withAlpha ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb;
            var bmp = new Bitmap(src.Width, src.Height, pf);
            using var g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(src, 0, 0); // copy pixel-by-pixel, tránh vấn đề stride/indexed
            return bmp;
        }

        private static ImageCodecInfo? GetEncoder(ImageFormat format)
            => ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == format.Guid);

        private static bool TryGuid(string s, out Guid g) => Guid.TryParse(s, out g);

        private static string Sanitize(string s)
        {
            var bad = Path.GetInvalidFileNameChars();
            return new string(s.Select(ch => bad.Contains(ch) ? '_' : ch).ToArray());
        }

        public class SaveResult
        {
            public string? Path { get; set; }
            public string Type { get; set; } = string.Empty;
        }
    }
}