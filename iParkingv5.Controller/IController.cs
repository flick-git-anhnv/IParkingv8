using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;

namespace iParkingv5.Controller
{
    public interface ICloseBarrie
    {
        Task<bool> CloseBarrie2();
    }
    public interface IController
    {
        CardDispenserStatus cardDispenserStatus { get; set; }
        Bdk ControllerInfo { get; set; }

        #region Event
        event CardEventHandler CardEvent;
        //event FingerEventHandler FingerEvent;
        event ControllerErrorEventHandler ErrorEvent;
        event InputEventHandler InputEvent;
        event ConnectStatusChangeEventHandler ConnectStatusChangeEvent;
        public event CancelEventHandler? CancelEvent;
        event DeviceInfoChangeEventHandler DeviceInfoChangeEvent;
        void PollingStart();
        void PollingStop();
        #endregion End Event

        #region: CONNECT
        Task<bool> TestConnectionAsync();
        Task<bool> ConnectAsync();
        Task<bool> DisconnectAsync();
        #endregion: END CONNECT

        #region DATE - TIME
        //Task<DateTime> GetDateTime();
        //Task<bool> SetDateTime(DateTime time);
        //Task<bool> SyncDateTime();
        #endregion END DATE - TIME

        #region:TCP_IP
        //GET
        //Task<string> GetIPAsync();
        //Task<string> GetMacAsync();
        //Task<string> GetDefaultGatewayAsync();
        //Task<int> GetPortAsync();
        //Task<string> GetComkeyAsync();

        //SET
        //Task<bool> SetMacAsync(string macAddr);
        //Task<bool> SetNetWorkInforAsync(string ip, string subnetMask, string defaultGateway, string macAddr);
        //Task<bool> SetComKeyAsync(string comKey);
        #endregion: END TCP_IP

        #region System
        //Task<bool> ClearMemory();
        //Task<bool> RestartDevice();
        //Task<bool> ResetDefault();
        #endregion End System

        #region Door Control
        Task<bool> OpenDoor(int timeInMilisecond, int relayIndex);

        //Task<bool> AddFinger(List<string> fingerDatas, string customerName, int userId);
        //Task<bool> ModifyFinger(List<string> fingerDatas, string customerName, int userId);
        //Task<bool> DeleteFinger(string userId, int fingerIndex);
        #endregion End Region
    }
    public class CardDispenserStatus
    {
        public bool IsWorking { get; set; } = true;
        /// <summary>
        /// Trạng thái của các khay nha the, bat dau tu 1
        /// </summary>
        public Dictionary<int, EmCardDispenserStatus> DispensersStatus { get; set; } = new Dictionary<int, EmCardDispenserStatus>();
        public List<string> GetStatusStr()
        {
            List<string> status = new List<string>();
            foreach (KeyValuePair<int, EmCardDispenserStatus> item in this.DispensersStatus)
            {
                string baseStr = $"Khay {item.Key} ";
                string statusStr = "";
                switch (item.Value)
                {
                    case EmCardDispenserStatus.ConThe:
                        statusStr = "Còn thẻ";
                        break;
                    case EmCardDispenserStatus.SapHetThe:
                        statusStr = "Sắp hết thẻ";
                        break;
                    case EmCardDispenserStatus.HetThe:
                        statusStr = "Hết thẻ";
                        break;
                    case EmCardDispenserStatus.LoiNhaThe:
                        statusStr = "Lỗi nhả thẻ";
                        break;
                    default:
                        statusStr = "";
                        break;
                }
                statusStr = baseStr + statusStr;
                status.Add(statusStr);
            }
            return status;
        }
        public bool IsError()
        {
            foreach (KeyValuePair<int, EmCardDispenserStatus> item in this.DispensersStatus)
            {
                switch (item.Value)
                {
                    case EmCardDispenserStatus.ConThe:
                        break;
                    case EmCardDispenserStatus.SapHetThe:
                        break;
                    case EmCardDispenserStatus.HetThe:
                        break;
                    case EmCardDispenserStatus.LoiNhaThe:
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }
        public bool IsWarning()
        {
            foreach (KeyValuePair<int, EmCardDispenserStatus> item in this.DispensersStatus)
            {
                switch (item.Value)
                {
                    case EmCardDispenserStatus.ConThe:
                        break;
                    case EmCardDispenserStatus.SapHetThe:
                        return true;
                    case EmCardDispenserStatus.HetThe:
                        return true;
                    case EmCardDispenserStatus.LoiNhaThe:
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }

    }
    public enum EmCardDispenserStatus
    {
        ConThe,
        SapHetThe,
        HetThe,
        LoiNhaThe,
    }
}
