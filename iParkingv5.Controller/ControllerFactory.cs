using iParkingv5.Controller.Aopu;
using iParkingv5.Controller.KztekDevices.BR_CTRL;
using iParkingv5.Controller.KztekDevices.KZE02NETControllerv2;
using iParkingv5.Controller.KztekDevices.KZE16AccessControl;
using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv5.Controller.ZktecoDevices.PULL;
using iParkingv5.Controller.ZktecoDevices.PUSH;
using iParkingv6.Objects.Datas;

namespace iParkingv5.Controller
{
    public class ControllerFactory
    {
        public static IController? CreateController(Bdk bdk, string printTemplate)
        {
            return (EmControllerType)bdk.Type switch
            {
                EmControllerType.IDTECK => null,
                EmControllerType.KZE02_NET => new KzE02Netv2() { ControllerInfo = bdk },
                EmControllerType.KZE16_NET => new KzE16Net() { ControllerInfo = bdk },
                // Fixmex: Fix cung BDK
                EmControllerType.MT166 => printTemplate != "GELEX" ? new MT166_CardDispenserv8_2() { ControllerInfo = bdk } : new MT166_CardDispenserVerificationMode() { ControllerInfo = bdk },
                EmControllerType.INGRESSUS => new ZktecoPull() { ControllerInfo = bdk },
                EmControllerType.E02_NET => new AopuController() { ControllerInfo = bdk },
                EmControllerType.SC200 => new SC200Devices.SC200() { ControllerInfo = bdk },
                //EmControllerType.Dahua => new DahuaAccessControl(bdk.Id) { ControllerInfo = bdk },
                EmControllerType.MT166_v8 => printTemplate != "GELEX" ? new MT166_CardDispenserv8_2() { ControllerInfo = bdk } : new MT166_CardDispenserVerificationMode() { ControllerInfo = bdk },
                EmControllerType.ZKTECO_PUSH => new ZktecoPUSH() { ControllerInfo = bdk },
                _ => null,
            };
        }
        public static IController? CreateController(string id, string ip, string port, int type, int communicationType, string name, int eventDelay, string printTemplate, string code)
        {
            var controller = new Bdk()
            {
                Id = id,
                Comport = ip,
                Baudrate = port.ToString(),
                Type = type,
                CommunicationType = communicationType,
                Name = name,
                Code = code
            };

            return (EmControllerType)controller.Type switch
            {
                EmControllerType.IDTECK => null,
                EmControllerType.KZE02_NET => new KzE02Netv2() { ControllerInfo = controller },
                EmControllerType.KZE16_NET => new KzE16Net() { ControllerInfo = controller },
                // Fixmex: Fix cung BDK
                EmControllerType.MT166 => printTemplate != "GELEX" ? new MT166_CardDispenserv8_2() { ControllerInfo = controller } : new MT166_CardDispenserVerificationMode() { ControllerInfo = controller },
                EmControllerType.INGRESSUS => new ZktecoPull() { ControllerInfo = controller },
                EmControllerType.E02_NET => new AopuController() { ControllerInfo = controller },
                EmControllerType.SC200 => new SC200Devices.SC200() { ControllerInfo = controller },
                //EmControllerType.Dahua => new DahuaAccessControl(controller.Id) { ControllerInfo = controller },
                EmControllerType.MT166_v8 => printTemplate != "GELEX" ? new MT166_CardDispenserv8_2() { ControllerInfo = controller } : new MT166_CardDispenserVerificationMode() { ControllerInfo = controller },
                EmControllerType.BarrierController => new BR_LAN_CTRL() { ControllerInfo = controller },
                EmControllerType.ZKTECO_PUSH => new ZktecoPUSH() { ControllerInfo = controller },
                _ => null,
            };
        }

        public enum EmControllerType
        {
            IDTECK = 0,
            KZE02_NET = 1,
            KZE16_NET = 2,
            MT166 = 3,
            INGRESSUS = 4,
            E02_NET = 5,
            SC200 = 6,
            Dahua = 7,
            ZKTECO_PUSH = 8,
            MT166_v8 = 9,
            BarrierController = 17,
        }
    }
}
