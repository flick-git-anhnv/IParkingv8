using System.Collections.Generic;

namespace Kztek.Object
{
    public class Staff
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public FaceInfo? Face { get; set; } = null;
        public FingerInfo? Finger { get; set; } = null;
        public CardInfo? Card { get; set; } = null;
        public Dictionary<string, Dictionary<string, Shift>> workingTimeConfigData { get; set; }
             = new Dictionary<string, Dictionary<string, Shift>>();
    }
    public class FaceInfo
    {
        public List<string> FaceData { get; set; } = new List<string>();
    }
    public class FingerInfo
    {
        public List<string> FingerData { get; set; } = new List<string>();
    }
    public class CardInfo
    {
        public string CardNumber { get; set; } = string.Empty;
        public EmCardType CardType { get; set; } = EmCardType.Mifare;
    }

    public enum EmCardType
    {
        Proximity_Left,
        ProximityRight,
        Mifare,
        ultra,
    }
}
