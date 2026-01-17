using System;
using System.Collections.Generic;
using static Kztek.Object.Events;

namespace Kztek.Object
{

    /// <summary>
    /// Quản lý hành động đối với các thiết bị sử dụng
    /// </summary>
    public interface IController
    {
        event ConnectStatusChangeEventHandler? onConnectStatusChangeEvent;
        event OnDownloadEmployee? onDownloadEmployeeEvent;
        event OnDeleteEmployee? onDeleteEmployeeEvent;
        event OnEventComeHandler? onEventComeEvent;
        event OnErrorEventHandler? onErrorEventHandler;
        List<IControllerControl> Init(List<Controller> controllers);

        bool Connect();
        bool Disconnect();

        void PollingStart();
        void PollingStop();

        void SynchronousTime(DateTime time);

        void DownloadUser(Staff employee, List<Controller> controllers);
        void DownloadUser(Staff employee, RegisterMode.EmRegisterMode mode, List<Controller> controllers);
        void DeleteUser(Staff employee, List<Controller> controllers);

        void DeleteController(Controller controller);
    }
}
