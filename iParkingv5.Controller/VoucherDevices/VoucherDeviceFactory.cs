namespace iParkingv5.Controller.VoucherDevices
{
    public static class VoucherDeviceFactory
    {
        public static IVoucherController CreateController(EmKioskControllerType controllerType)
        {
            return new QR500();

            switch (controllerType)
            {
                case EmKioskControllerType.QR500:
                    return new QR500();
                default:
                    return null;
            }
        }
    }
}
