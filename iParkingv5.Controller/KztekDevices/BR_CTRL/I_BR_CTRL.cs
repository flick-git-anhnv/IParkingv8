namespace iParkingv5.Controller.KztekDevices.BR_CTRL
{
    public interface I_BR_CTRL
    {
        event OnBarrieChangeStatusEventHandler? OnBarrieChangeStatusChangeEvent;
        bool CloseBarrie();
        bool OpenBarrie();
        bool StopBarrie();
    }
}
