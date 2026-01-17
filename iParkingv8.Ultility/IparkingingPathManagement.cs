namespace iParkingv8.Ultility
{
    public class IparkingingPathManagement
    {
        #region APP CONFIG PATH
        public static string baseBath = string.Empty;
        public static string serverConfigPath => baseBath + "/configs/app/server.txt";
        public static string rabbitmqConfigPath => baseBath + "/configs/app/rabbitMQ.txt";
        public static string mqttConfigPath => baseBath + "/configs/app/mqtt.txt";
        public static string lprConfigPath => baseBath + "/configs/app/lpr.txt";
        public static string appOptionConfigPath => baseBath + "/configs/app/option.txt";
        public static string oemConfigPath => baseBath + "/configs/app/oem.txt";
        public static string thirtPartyConfigPath => baseBath + "/configs/app/thirtParty.txt";
        public static string paymentKioskConfigPath => baseBath + "/configs/app/paymentKiosk.txt";

        public static string appDisplayConfigPath(string laneID) => baseBath + $"/configs/app/{laneID}/displayConfig.txt";
        public static string appLaneDirectionConfigPath(string laneId) => baseBath + $"/configs/app/{laneId}/displayDirection.txt";
        public static string appActiveLaneConfigPath() => baseBath + $"/configs/app/activeLane.txt";
        public static string appPrintTemplateConfigPath(string printTemplateName) => baseBath + $"/configs/app/print/{printTemplateName}.html";
        public static string appPrintTemplateRevenuePath(string printTemplateName) => baseBath + $"/configs/app/print/{printTemplateName}Revenue.html";
        public static string appShiftHandOver(string printTemplateName) => baseBath + $"/configs/app/print/{printTemplateName}ShiftHandOver.html";
        #endregion END APP CONFIG PATH

        #region LANE CONFIG PATH
        public static string laneShortcutConfigPath(string laneId) => baseBath + $"/configs/{laneId}/lane/baseShortcut.txt";
        public static string laneControllerShortcutConfigPath(string laneId) => baseBath + $"/configs/{laneId}/lane/controllerShortcut.txt";
        public static string laneOptionalConfig(string laneId) => Path.Combine(baseBath + $"configs/{laneId}/lane/optional.txt");

        public static string laneLedConfigPath(string laneId, string ledId) => baseBath + $"/configs/{laneId}/led/{ledId}.txt";
        public static string laneLedDefaultConfigPath(string laneId, string ledId) => baseBath + $"/configs/{laneId}/led/{ledId}_default.txt";

        public static string laneCameraConfigPath(string laneId, string cameraId) => baseBath + $"/configs/{laneId}/camera/{cameraId}_lpr.txt";
        public static string laneCameraLoopConfigPath(string laneId, string cameraId) => baseBath + $"/configs/{laneId}/camera/{cameraId}_loop.txt";
        public static string laneControllerReaderConfigPath(string laneId, string controllerId, int readerIndex) => baseBath + $"/configs/{laneId}/{controllerId}_{readerIndex}/reader_config.txt";
        public static string laneControllerBarrieConfigPath(string laneId, string controllerId, int barrieIndex) => baseBath + $"/configs/{laneId}/{controllerId}_barrie{barrieIndex}/barrie_config.txt";
        #endregion END LANE CONFIG PATH

        #region Haus
        public static string hausQRPath() => baseBath + $"/configs/app/print/haus_qr.html";
        #endregion

        #region ILEDv8 CONFIG
        public static string ledColorDisplayConfig(string ledIp, int line) => baseBath + $"/configs/iled/{ledIp}_{line}_display.txt";
        public static string ledLaneConfig(string ledIp, int line) => baseBath + $"/configs/iled/{ledIp}_{line}_lanes.txt";
        public static string ledDelayConfig() => baseBath + $"/configs/iled/delay.txt";
        #endregion
    }
}