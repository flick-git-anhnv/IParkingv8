namespace Kztek.Object.Entity.ConfigObjects
{
    public class MQTTConfig
    {
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Topic { get; set; } = "#";
    }
}
