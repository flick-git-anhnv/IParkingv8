namespace Kztek.Object
{
    public class AppSetting
    {
        public bool IsAutoLogin { get; set; }
        public bool IsHideView { get; set; }
        public bool IsStartWithWindow { get; set; }
        public bool IsAutoRestartApp { get; set; }
        public bool IsAutoDownloadUser { get; set; }
        public ServerSetting ServerSetting { get; set; } = new ServerSetting();
        public string FaceServerUrl { get; set; } = string.Empty;
        public int LogServiceType { get; set; }
    }
}
