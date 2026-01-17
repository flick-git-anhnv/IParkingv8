using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv5.Objects.Events;
using iParkingv6.Objects.Datas;
using Kztek.Object;
using Kztek.Tool;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static Kztek.Object.CommunicationType;

namespace iParkingv5.Controller.KztekDevices
{
    public abstract class BaseDispenserDevice : BaseKzDevice
    {
        public event CardBeTakenHandler? CardBeTaken;

        protected void OnCardBeTakenEvent(CardBeTakenEventArgs e)
        {
            CardBeTaken?.Invoke(this, e);
        }
    }
}

