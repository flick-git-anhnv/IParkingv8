using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;

namespace iParkingv5.Controller.ZktecoDevices.PUSH
{
    public class ZktecoPUSH : IController
    {
        public CardDispenserStatus cardDispenserStatus { get; set; }
        public Bdk ControllerInfo { get; set; }

        public event CardEventHandler CardEvent;
        public event ControllerErrorEventHandler ErrorEvent;
        public event InputEventHandler InputEvent;
        public event ConnectStatusChangeEventHandler ConnectStatusChangeEvent;
        public event CancelEventHandler? CancelEvent;
        public event DeviceInfoChangeEventHandler DeviceInfoChangeEvent;

        public async Task<bool> ConnectAsync()
        {
            return ControllerInfo.IsConnect;
        }

        public async Task<bool> DisconnectAsync()
        {
            return true;
        }

        public async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
        {
            return true;
        }

        public void PollingStart()
        {
        }

        public void PollingStop()
        {
        }

        public async Task<bool> TestConnectionAsync()
        {
            return true;
        }
    }
}
