using iParkingv5.Controller.KztekDevices.BR_CTRL;

namespace iParkingv5.Controller.KztekDevices.KZE02NETController
{
    public interface IBollardCtrl
    {
        event OnBarrieChangeStatusEventHandler? OnBollardChangeStatusChangeEvent;
        bool CloseBarrie();
        bool OpenBarrie();
        bool StopBarrie();
    }
}
