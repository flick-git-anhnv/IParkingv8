using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IPaymentConfigView
    {
        PaymentKioskConfig? GetConfig();
        void SetConfig(PaymentKioskConfig? config);
    }
}
