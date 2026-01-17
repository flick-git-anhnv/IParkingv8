namespace Kztek.Object.Entity.ConfigObjects
{
    public class ServerConfig
    {
        public EmApiVersion Version { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
