using iParkingv8.Object.Objects.Payments;
using IParkingv8.Cash.Controllers;

namespace IParkingv8.Cash.Factory
{
    public static class CashDeviceFactory
    {
        public static ICashController CreateController(EmCashControllerType controllerType, PaymentKioskConfig paymentKioskConfig)
        {
            switch (controllerType)
            {
                case EmCashControllerType.CBA9:
                    return new CBA9ControllersV2(paymentKioskConfig);
                default:
                    return null;
            }
        }
    }
}
