using System;
using System.Collections.Generic;
using System.Text;

namespace iParkingv5.LedDisplay.Enums
{
    public enum EmModuleType
    {
        Unknown = 0,
        P10FullColor = 1,
        P10Red = 2,
        P7_62_RGY = 3,
        SerialLed = 4,
        LedMatrixV1_2UDP = 5,
        P10FullColor_Outdoor = 6,
        P10Red_Outdoor = 7,
        P10_RG_OutDoor = 8,
    }

    public class ModuleTypes
    {
        public EmModuleType ModuleType { get; set; }

        public ModuleTypes(EmModuleType moduleType)
        {
            ModuleType = moduleType;
        }

        private string GetString(int module_Type)
        {
            switch (module_Type)
            {
                case 1:
                    {
                        return "P10 Full Color";
                    }
                case 2:
                    {
                        return "P10 Red";
                    }
                case 3:
                    {
                        return "P7 62 RGY";
                    }
                case 4:
                    {
                        return "Serial Led";
                    }
                case 5:
                    {
                        return "LedMatrix V11.2 UDP";
                    }
                case 6:
                    {
                        return "P10FullColor Outdoor";
                    }
                case 7:
                    {
                        return "P10Red Outdoor";
                    }
                case 8:
                    {
                        return "P10 RG";
                    }
                default:
                    return "Unknown";
            }
        }
        private string GetString(EmModuleType module_Type)
        {
            return GetString((int)module_Type);
        }

        public override string ToString()
        {
            return GetString(this.ModuleType);
        }
    }
}