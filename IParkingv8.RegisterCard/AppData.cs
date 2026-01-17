using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Payments;
using IParkingv8.API.Implementation.v8;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;

namespace IParkingv8
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
        public static RabbitMQConfig RabbitMQConfig;
        public static MQTTConfig MqttConfig;
        public static LprConfig LprConfig = new();
        public static AppOption AppConfig = new();
        public static OEMConfig OEMConfig = new();
        public static ThirdPartyConfig ThirdPartyConfig = new();

        public static ApiServerv8 ApiServer;

        public static Computer? Computer;
        public static Gate? Gate;

        public static List<Lane> Lanes = [];
        public static List<Camera> Cameras = [];
        public static List<Led> Leds = [];
        public static List<Bdk> Controllers = [];
        public static List<Collection> DailyAccessKeyCollections = [];
        public static List<Collection> AccessKeyCollections = [];

        public static List<AccessKey> accessKeys = [];

        public static List<iParkingv5.Controller.IController> IControllers = [];

        public static string DefaultImageBase64 = string.Empty;
        public static PaymentKioskConfig PaymentKioskConfig = new();
        //public static User? User;

    }
}
