namespace Kztek.Object
{
    public class CommunicationType
    {
        public enum EmCommunicationType
        {
            TCP_IP,
            SERIAL,
            USB
        }

        public static bool IS_TCP(EmCommunicationType communicationType)
        {
            return communicationType == EmCommunicationType.TCP_IP;
        }

        public static string ToString(EmCommunicationType communicationType)
        {
            switch (communicationType)
            {
                case EmCommunicationType.TCP_IP:
                    return "TCP IP";
                case EmCommunicationType.SERIAL:
                    return "Serial";
                case EmCommunicationType.USB:
                    return "USB";
                default:
                    return "";
            }
        }
    }
}
