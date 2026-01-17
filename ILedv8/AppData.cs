using ILedv8.Objects;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Objects.Devices;
using IParkingv8.API.Implementation.v8;
using Kztek.Object;

namespace ILedv8
{
    public static class AppData
    {
        public static int splitterCamera_EventImageList = 224;
        public static int splitterImageList_Result = 444;
        public static int splitterResult_plate = 488;
        public static int splitterPlate_EventInfo = 625;
        public static int splitterEventInfo_Function = 956;
        public static bool IsNeedToConfirmPassword = true;

        public static iParkingv8.Object.Objects.Systems.ServerConfig? ServerConfig;
        public static AppOption AppConfig = new();

        public static ApiServerv8 ApiServer;

        public static Computer? Computer;

        public static List<Led> Leds;
        public static List<Lane> Lanes;
        public static DelayConfig delayConfig;
    }
}
