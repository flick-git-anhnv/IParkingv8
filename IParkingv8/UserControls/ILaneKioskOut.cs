using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.RabbitMQ;

namespace IParkingv8.UserControls
{
    public interface ILaneKioskOut : ILaneOut
    {
        void ApplyConfirmResult(EventRequest eventRequest);
        void ApplyPaymentResult(PaymentResult paymentResult);
        void NotifyLastMessage();
    }
}
