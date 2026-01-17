using iParkingv5.LedDisplay.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.LEDs
{
    public class LedDisplayConfig
    {
        public string LedId { get; set; } = string.Empty;
        public EmLedDirectionDisplayMode DirectionDisplayMode { get; set; } = EmLedDirectionDisplayMode.Exit;
        public Dictionary<int, DisplayStepDetail> LedDisplaySteps { get; set; } = new Dictionary<int, DisplayStepDetail>();
    }
    public class DisplayStepDetail
    {
        public Dictionary<int, LineConfig> DisplayDatas { get; set; } = new Dictionary<int, LineConfig>();
        public int DelayTime { get; set; }
    }
}
