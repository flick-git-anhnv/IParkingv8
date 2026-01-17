using static Kztek.Object.CommunicationType;

namespace Kztek.Object.Entity.Device
{
    public class NetworkInfo
    {
        public EmCommunicationType CommunicationType { get; set; }

        public int Address { get; set; } = 0;
        public int Baudrate { get; set; }
        public string Comport { get; set; } = string.Empty;

        public static NetworkInfo CreateDefault()
        {
            return new NetworkInfo()
            {
                CommunicationType = EmCommunicationType.TCP_IP,
                Address = 0,
                Baudrate = 100,
                Comport = "127.0.0.1"
            };
        }
    }
}
