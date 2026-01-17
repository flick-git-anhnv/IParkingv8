using iParkingv5.Objects.Events;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.ucDataGridViewInfo;
using Kztek.Object;
using Kztek.Tool;
using static Kztek.Object.InputTupe;

namespace IParkingv8.Helpers.CardProcess
{
    public class CardBaseProcess
    {
        public static bool CheckNewCardEvent(CardEventArgs ce, out int thoiGianCho, ref List<CardEventArgs> LastCardEventDatas)
        {
            LastCardEventDatas ??= [];
            thoiGianCho = 0;
            foreach (CardEventArgs oldEvent in LastCardEventDatas)
            {
                if (ce.IsInWaitingTime(oldEvent, AppData.AppConfig.MinDelayCardTime, out thoiGianCho))
                {
                    return false;
                }
            }

            List<CardEventArgs> deleteEvents = [];
            foreach (var item in LastCardEventDatas)
            {
                foreach (string card in ce.AllCardFormats)
                {
                    if (item.AllCardFormats.Contains(card))
                    {
                        deleteEvents.Add(item);
                    }
                }
            }
            foreach (var item in deleteEvents)
            {
                LastCardEventDatas.Remove(item);
            }
            deleteEvents.Clear();

            LastCardEventDatas.Add(ce);
            return true;
        }
        public static async Task<BaseCardValidate> ValidateAccessKeyByCode(Lane lane, CardEventArgs ce, string baseLog)
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Check Validate Access Key: {ce.PreferCard}"));
            BaseCardValidate cardValidate = BaseCardValidate.CreateDefault();
            var accessKey = await AppData.ApiServer.DataService.AccessKey.GetByCodeAsync(ce.PreferCard, accessKeyType: (EmAccessKeyType)ce.Type);
            if (accessKey == null)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Mất kết nối đến máy chủ"));
                cardValidate.CardValidateType = EmAlarmCode.SYSTEM_ERROR;
                cardValidate.AlarmDescription = KZUIStyles.CurrentResources.ServerDisconnected;
                cardValidate.DisplayAlarmMessageTag = nameof(KZUIStyles.CurrentResources.ServerDisconnected);
                return cardValidate;
            }

            var errorData = accessKey.Item2;
            var accessKeyResponse = accessKey.Item1;
            cardValidate.AccessKey = accessKeyResponse;
            if (errorData == null && accessKeyResponse == null)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateCardEvent($"{baseLog} - Không convert được dữ liệu từ server"));
                cardValidate.CardValidateType = EmAlarmCode.SYSTEM_ERROR;
                cardValidate.AlarmDescription = KZUIStyles.CurrentResources.SystemError;
                cardValidate.DisplayAlarmMessageTag = nameof(KZUIStyles.CurrentResources.SystemError);
                return cardValidate;
            }

            if (errorData != null)
            {
                cardValidate.CardValidateType = EmAlarmCode.ACCESS_KEY_NOT_FOUND;
                cardValidate.AlarmDescription = $"{KZUIStyles.CurrentResources.AccesskeyCode}: {ce.PreferCard}";
                cardValidate.DisplayAlarmMessageTag = nameof(KZUIStyles.CurrentResources.AccessKeyNotFound);
                return cardValidate;
            }

            if (accessKeyResponse!.Status != EmAccessKeyStatus.IN_USE)
            {
                string type = accessKeyResponse.GetTypeName();
                string status = accessKeyResponse.GetStatusName();
                string note = accessKeyResponse.Note ?? "";

                cardValidate.CardValidateType = EmAlarmCode.ACCESS_KEY_LOCKED;
                cardValidate.AlarmDescription = note;
                cardValidate.DisplayAlarmMessageTag = nameof(KZUIStyles.CurrentResources.AccessKeyLocked);
                return cardValidate;
            }

            if (!(accessKeyResponse.Collection?.GetActiveLanes()?.Contains(lane.Id) ?? false))
            {
                cardValidate.CardValidateType = EmAlarmCode.ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE;
                cardValidate.AlarmDescription = $"{accessKeyResponse.Name} - {accessKeyResponse?.Collection?.Name} - {lane.Name} - {KZUIStyles.CurrentResources.InvalidPermission}";
                cardValidate.DisplayAlarmMessageTag = nameof(KZUIStyles.CurrentResources.InvalidPermission);
                return cardValidate;
            }

            cardValidate.CardValidateType = EmAlarmCode.NONE;
            cardValidate.AlarmDescription = "";
            cardValidate.DisplayAlarmMessageTag = "";
            return cardValidate;
        }

        /// <summary>
        /// Sử dụng để cập nhật lại thông tin nhóm dịnh danh khi dùng máy nhả thẻ 1 nút - làn hỗn hợp
        /// </summary>
        /// <param name="ce"></param>
        /// <param name="baseLog"></param>
        /// <param name="controllerInLane"></param>
        /// <param name="accessKeyResponse"></param>
        /// <param name="isCheckCardDipenserSuccess"></param>
        /// <returns></returns>
        public static async Task<Tuple<AccessKey?, bool>> CheckCardDispenserCollection(
            Lane lane, CardEventArgs ce, string baseLog,
            AccessKey? accessKeyResponse, IDataInfo ucEventInInfo, KZUI_UcCameraList ucCameraListIn,
            KZUI_UcResult ucResult, LaneDirectionConfig laneDirectionConfig)
        {
            //Thiết bị không phải máy nhả thẻ thì auto đúng
            if (ce.DeviceType != EmParkingControllerType.Card_Dispenser || ce.InputType != EmInputType.Button)
            {
                return Tuple.Create<AccessKey?, bool>(accessKeyResponse, true);
            }

            var configPath = IparkingingPathManagement.laneControllerReaderConfigPath(lane.Id, ce.DeviceId, ce.ButtonIndex);
            var config = NewtonSoftHelper<CardFormatConfig>.DeserializeObjectFromPath(configPath);

            //Không có config thì auto đúng
            if (config == null || string.IsNullOrEmpty(config.CardGroupId))
            {
                return Tuple.Create<AccessKey?, bool>(accessKeyResponse, true);
            }

            //Thông tin nhóm thẻ giống nhau thì auto đúng
            if (accessKeyResponse!.Collection!.Id.ToString().Equals(config.CardGroupId, StringComparison.CurrentCultureIgnoreCase))
            {
                return Tuple.Create<AccessKey?, bool>(accessKeyResponse, true);
            }

            var colleciton = (await AppData.ApiServer.DataService.AccessKeyCollection.GetByIdAsync(config.CardGroupId))?.Item1;
            accessKeyResponse.Collection = colleciton;

            return Tuple.Create<AccessKey?, bool>(accessKeyResponse, true);
        }
    }
}
