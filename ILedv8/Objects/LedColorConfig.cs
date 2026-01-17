using iParkingv5.LedDisplay.Enums;

namespace ILedv8.Objects
{
    public class LedColorConfig
    {
        public string LedId { get; set; } = string.Empty;
        public int Line { get; set; } = 1;
        public EmLedColor DisplayColor { get; set; } = EmLedColor.GREEN;
        public EmLedColor ZeroColor { get; set; } = EmLedColor.RED;
        public EmFontSize FontSize { get; set; } = EmFontSize.FontSize_10;
        public int NumOfCharacterDisplay { get; set; } = 3;
    }
}
