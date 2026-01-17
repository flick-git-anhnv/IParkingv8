using static Kztek.Object.LprDetecter;

namespace Kztek.Object
{
    public class LprConfig
    {
        public EmLprDetecter LPRDetecterType { get; set; } = EmLprDetecter.KztekLPRAIServer;
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsDetectVehicleType { get; set; }

        public int RetakePhotoDelay { get; set; } = 300;
        public int RetakePhotoTimes { get; set; } = 5;
    }
}
