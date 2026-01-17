using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Payments;
using IParkingv8.QRScreenController.QRView;

namespace IParkingv8.QRScreenController
{
    public static class QrDeviceFactory
    {
        public static IQRDevice? CreateQRDevice(LaneOptionalConfig config)
        {
            switch ((EmStaticQRDevice)config.StaticQRType)
            {
                case EmStaticQRDevice.QRVIEW:
                    return new QRViewDevice(config);
                default:
                    return null;
            }
        }
    }
}
