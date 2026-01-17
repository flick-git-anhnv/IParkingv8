using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.Enums
{
    public enum EmDisplayMode
    {
        ONE_COLOR_EACH_LINE,
        MULTY_COLOR_EACH_LINE
    }
    public class LedDisplayModes
    {
        public static EmDisplayMode GetValidDisplayModeDisplayMode(EmModuleType moduleType)
        {
            switch (moduleType)
            {
                case EmModuleType.P10FullColor:
                case EmModuleType.P7_62_RGY:
                    return EmDisplayMode.MULTY_COLOR_EACH_LINE;
                default:
                    return EmDisplayMode.ONE_COLOR_EACH_LINE;
            }
        }
    }
}
