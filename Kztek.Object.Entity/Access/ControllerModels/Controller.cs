using System;

namespace Kztek.Object
{
    public class Controller
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Ip { get; set; } = string.Empty;
        public int Port { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int Address { get; set; }
        public EmControllerType ControllerType { get; set; }
        public CommunicationType.EmCommunicationType CommunicationType { get; set; }
        public EmIdentifyMode IdentifyMode { get; set; }
        public EmDevicetype DeviceType { get; set; }
        public bool IsConnect { get; set; }

        public enum EmControllerType
        {
            NOT_SUPPORT,
            AOPU,
            HANNET,
            DAHUA,
            FINGERTEC,
            ZKTECO,
            KZE02,
            KZE05,
            KZE32,
            KZELV,
            HANET,
            Tiandy,
            KZIO0808,
            KZIO1616,
        }
        public enum EmDevicetype
        {
            Controller = 1,
            Camera = 2,
        }

        [Flags]
        public enum EmIdentifyMode
        {
            Card = 1,
            Finger = 2,
            Face = 4,
        }

        public EmControllerType GetControllerTypeByName(string controllerType)
        {
            foreach (EmControllerType item in Enum.GetValues(typeof(EmControllerType)))
            {
                string temp = item.ToString();
                if (item.ToString().ToUpper() == controllerType.ToUpper())
                {
                    return item;
                }
            }
            return EmControllerType.NOT_SUPPORT;
        }
    }
}
