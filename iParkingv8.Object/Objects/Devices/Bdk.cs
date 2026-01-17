using Kztek.Object;

namespace iParkingv8.Object.Objects.Devices
{
    public class Bdk
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int CommunicationType { get; set; }
        public string Comport { get; set; } = string.Empty;
        public string Baudrate { get; set; } = string.Empty;

        private int type = 0;
        public int TypeCode
        {
            get => type; set
            {
                type = value;
            }
        }
        public int Type
        {
            get => type;
            set
            {
                type = value;
            }
        }

        public string ComputerId { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        public string CreatedUtc { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public object UpdatedUtc { get; set; } = string.Empty;
        public object UpdatedBy { get; set; } = string.Empty;

        public bool IsConnect { get; set; }
        public int OutputCount { get; set; }
        public List<CardFormatConfig> CardFormatConfigs { get; set; } = [];
    }

}
