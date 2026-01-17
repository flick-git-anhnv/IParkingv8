using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.Enums
{
    [Flags]
    public enum EmLedColor
    {
        UNKNOWN = 0,
        RED = 1,
        GREEN = 2,
        YELLOW = 4,
        BLUE = 8,
        PURPLE = 16,
        GREEN_BLUE = 32,
        WHITE = 64,
        ALL = 128,
    }
    public class LedColors
    {
        public static EmLedColor GetValidColorByModuleType(EmModuleType moduleType)
        {
            switch (moduleType)
            {
                case EmModuleType.P10Red:
                case EmModuleType.P10Red_Outdoor:
                    return EmLedColor.RED;
                case EmModuleType.P10FullColor:
                case EmModuleType.P10FullColor_Outdoor:
                    return EmLedColor.ALL;
                case EmModuleType.P7_62_RGY:
                    return EmLedColor.RED | EmLedColor.GREEN | EmLedColor.YELLOW;
                default:
                    return EmLedColor.UNKNOWN;
            }
        }
    }
}
