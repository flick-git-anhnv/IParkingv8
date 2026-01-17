using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Sounds;
using iParkingv8.Ultility.Style;
using IParkingv8.Helpers.CardProcess;
using Kztek.Object;
using Kztek.Tool;
using IController = iParkingv5.Controller.IController;

namespace IParkingv8.Helpers
{
    public class SoundHelper
    {
        public static void PlaySoundMedia(EmSystemSoundType sound)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Play Sound " + sound));
            string languageCode = KZUIStyles.CultureInfo.Name == "vi-VN" ? "Vi" : "En";
            var path = Path.Combine(Application.StartupPath, $"Voice\\{languageCode}\\{sound}.wav");
            if (!File.Exists(path))
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Play Sound Error: " + path));
                return;
            }
            System.Media.SoundPlayer player = new(path);
            player.Play();
            player.Dispose();
        }
        public static async void PlaySound(string controllerId, EmSystemSoundType soundType)
        {
            try
            {
                switch (AppData.AppConfig.SoundMode)
                {
                    case EmSoundMode.NO:
                        return;
                    case EmSoundMode.PlaySoundFromPC:
                        PlaySoundMedia(soundType);
                        break;
                    case EmSoundMode.PlaySoundFromController:
                        if (string.IsNullOrWhiteSpace(controllerId))
                        {
                            return;
                        }
                        foreach (IController controller in AppData.IControllers)
                        {
                            if (controller is not ICardDispenser)
                            {
                                continue;
                            }
                            if (!controller.ControllerInfo.Id.Equals(controllerId, StringComparison.CurrentCultureIgnoreCase))
                            {
                                continue;
                            }
                            SystemUtils.logger.SaveDeviceLog(
                                        new DeviceLog()
                                        {
                                            DeviceId = controllerId,
                                            DeviceName = "",
                                            Cmd = "Play Sound02",
                                            Description = "False"
                                        }
                                    );
                            int soundIndex = 0;
                            switch (soundType)
                            {
                                case EmSystemSoundType.ACCESS_KEY_EXPIRED:
                                    soundIndex = 5;
                                    break;
                                case EmSystemSoundType.ACCESS_KEY_LOCKED:
                                    break;
                                case EmSystemSoundType.ACCESS_KEY_MONTH_NO_VEHICLE:
                                    break;
                                case EmSystemSoundType.MAT_KET_NOI_DEN_MAY_CHU:
                                    break;
                                case EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY:
                                    break;
                                case EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_NHAN_NUT_LAY_THE:
                                    soundIndex = 1;
                                    break;
                                case EmSystemSoundType.VUI_LONG_QUET_THE_THANG_HOAC_TRA_THE:
                                    soundIndex = 1;
                                    break;
                                case EmSystemSoundType.DINH_DANH_KHONG_CO_TRONG_HE_THONG:
                                    break;
                                case EmSystemSoundType.SAI_QUYEN_TRUY_CAP:
                                    break;
                                case EmSystemSoundType.WAIT_CONFIRM_OPEN_BARRIE:
                                    break;
                                case EmSystemSoundType.KHONG_NHAN_DIEN_DUOC_BIEN_SO:
                                    break;
                                case EmSystemSoundType.BIEN_SO_VAO_RA_KHONG_KHOP_VUI_LONG_CHO_XAC_NHAN_BARRIE:
                                    break;
                                case EmSystemSoundType.BIEN_SO_DANG_KY_KHONG_KHOP_VUI_LONG_CHO_XAC_NHAN_BARRIE:
                                    break;
                                case EmSystemSoundType.NOT_CONFIRM_OPEN_BARRIE:
                                    break;
                                case EmSystemSoundType.XE_DA_VAO_BAI:
                                    soundIndex = 6;
                                    break;
                                case EmSystemSoundType.XIN_MOI_QUA:
                                    soundIndex = 2;
                                    break;
                                case EmSystemSoundType.HEN_GAP_LAI:
                                    soundIndex = 1;
                                    break;
                                case EmSystemSoundType.XIN_MOI_LAY_THE:
                                    soundIndex = 1;
                                    break;
                                case EmSystemSoundType.XE_CHUA_VAO_BAI:
                                    break;
                                case EmSystemSoundType.INVALID_READER_DAILY:
                                    break;
                                case EmSystemSoundType.INVALID_READER_MONTHLY:
                                    break;
                                case EmSystemSoundType.BLACK_LIST:
                                    break;
                                case EmSystemSoundType.VEHICLE_LOCKED:
                                    break;
                                case EmSystemSoundType.VUI_LONG_THANH_TOAN_PHI_GUI_XE:
                                    break;
                                default:
                                    break;
                            }
                            await ((ICardDispenser)controller).SetAudio(soundIndex);

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Play Sound Error", ex));
            }
        }
        public static void InvokeCardValidate(BaseCardValidate cardValidate, string deviceId)
        {
            switch (cardValidate.CardValidateType)
            {
                case EmAlarmCode.SYSTEM_ERROR:
                    PlaySound(deviceId, EmSystemSoundType.GAP_LOI_TRONG_QUA_TRINH_XU_LY);
                    break;
                case EmAlarmCode.ACCESS_KEY_NOT_FOUND:
                    PlaySound(deviceId, EmSystemSoundType.DINH_DANH_KHONG_CO_TRONG_HE_THONG);
                    break;
                case EmAlarmCode.ACCESS_KEY_LOCKED:
                    PlaySound(deviceId, EmSystemSoundType.ACCESS_KEY_LOCKED);
                    break;
                case EmAlarmCode.ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE:
                    PlaySound(deviceId, EmSystemSoundType.SAI_QUYEN_TRUY_CAP);
                    break;
                default:
                    break;
            }
        }
    }
}
