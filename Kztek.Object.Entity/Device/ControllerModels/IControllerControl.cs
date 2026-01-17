using System;
using static Kztek.Object.Events;

namespace Kztek.Object
{
    /// <summary>
    /// Quản lý các hành động của 1 thiết bị
    /// </summary>
    public interface IControllerControl
    {
        public event ConnectStatusChangeEventHandler? onConnectStatusChangeEvent;
        public event OnDownloadEmployee? onDownloadEmployeeEvent;
        public event OnDeleteEmployee? onDeleteEmployeeEvent;
        public event OnEventComeHandler? onEventComeEvent;
        public event OnErrorEventHandler? onErrorEventHandler;
        public event OnInputEventHandler? OnInputEvent;
        Controller controller { get; set; }
        void Init(Controller controller);

        bool Connect();
        bool Disconnect();

        void PollingStart();
        void PollingStop();

        void SynchronousTime(DateTime time);

        void GetEvent();
        void GetEvent(DateTime startTime, DateTime endTime);

        EmployeeResult DownloadUser(Staff employee);
        EmployeeResult DownloadUser(Staff employee, RegisterMode.EmRegisterMode mode);
        EmployeeResult DeleteUser(Staff employee);

        bool SetRelay(int relay);
    }
}
