using iParkingv5.Objects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParkingv5.Controller.VoucherDevices
{
    public interface IVoucherController
    {
        bool IsConnect { get; set; }
        bool Connect(string comport, int baudrate, int communicationType);
        bool Disconnect();
        // CardEvent 
        event CardEventHandler? CardEvent;
        event CardEventHandler? VoucherEvent;
        // InputEvent 
        event InputEventHandler? InputEvent;
        void PollingStart();
        void PollingStop();
    }
}
