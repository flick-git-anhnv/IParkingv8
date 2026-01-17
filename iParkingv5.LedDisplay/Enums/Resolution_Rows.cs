using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.Enums
{
    public class Resolution_Rows
    {
        [Flags]
        public enum EmResolutionRow
        {
            ROW_16,
            ROW_32,
            ROW_48,
            ROW_64,
            ROW_80,
            ROW_96,
            ROW_112,
            ROW_128,
            ROW_144,
            ROW_160,
        }

        //public static EM_ResolutionRow GetValidRow(int numberOfLine, EM_ModuleType moduleType)
        //{
        //    switch (moduleType)
        //    {
        //        case EM_ModuleType.P10FullColor:
        //        case EM_ModuleType.P10Red:
        //        case EM_ModuleType.P7_62_RGY:
        //            return EM_ResolutionRow.ROW_16;
        //    }
        //}
    }

}
