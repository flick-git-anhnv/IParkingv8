using System.Collections.Generic;

namespace Kztek.Object
{
    // Code them

    public class CardDispenserService
    {
        public int ButtonIndex { get; set; }
        public string CardbeTaken { get; set; }
        public string CardRevertedInTrayValue { get; set; }
        public string CardRevertedInTrayIndex { get; set; }
        public string ButtonAbnormalValue { get; set; }
        public string ButtonAbnormalIndex { get; set; }

    }
    public class CardDispenserOnChange
    {
        public bool StatusProcessing { get; set; } = true;
        public List<StateCardDispenser> ListStatusDispenser { get; set; } = new List<StateCardDispenser>();
        public ArrayInputDispenser ArrayInputDispenser { get; set; }
        public string ErrorString { get; set; } = string.Empty;
    }
    public class StateCardDispenser
    {
        public int DispenserIndex { get; set; } = 1;
        public bool IsCardEmptyDispenser { get; set; } = false;
        public bool IsLessCardDispenser { get; set; } = false;
        public bool IsCardErrorDispenser { get; set; } = false;
    }
    public class ArrayInputDispenser
    {
        public int Alarm { get; set; } = 0;
        public bool DIn1 { get; set; } = false;
        public bool DIn2 { get; set; } = false;
        public bool DIn3 { get; set; } = false;
        public bool DIn4 { get; set; } = false;

    }

}
