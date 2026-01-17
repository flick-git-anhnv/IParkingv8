namespace iParkingv5.Controller.KztekDevices
{
    public class KZTEK_CMD
    {
        #region: FIRMWARE

        /// <summary>
        /// Get Firmware: All KZTEK Device
        /// </summary>
        /// <returns></returns>
        public static string GetFirmwareCMD()
        {
            return "GetFirmwareVersion?/";
        }

        #endregion: END FIRMWARE

        #region: EVENT

        /// <summary>
        /// Get Event: KZ-E02.NET V3.0
        /// </summary>
        /// <returns></returns>
        public static string GetEventCMD()
        {
            return "GetEvent?/";
        }

        /// <summary>
        /// Delete Event: KZ-E02.NET V3.0
        /// </summary>
        /// <returns></returns>
        public static string DeleteEventCMD()
        {
            return "DeleteEvent?/";
        }

        /// <summary>
        /// Get Card Event: E05 Net v3.4
        /// </summary>
        /// <returns></returns>
        public static string GetCardEventCMD()
        {
            return "GetCardEvent?/";
        }

        /// <summary>
        /// Delete card Event: E05 Net v3.4
        /// </summary>
        /// <returns></returns>
        public static string DeleteCardEventCMD()
        {
            return "DeleteCardEvent?/";
        }


        #endregion: END EVENT
        #region: INIT
        public static string InitUserMemoryCMD()
        {
            return "InitUserMemory?/";
        }
        public static string InitEventMemoryCMD()
        {
            return "InitEventMemory?/";
        }
        #endregion
        #region: USER

        /// <summary>
        /// Download User: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="memoryID"></param>
        /// <param name="cardNumber"></param>
        /// <param name="cardLen"></param>
        /// <param name="timeZoneID"></param>
        /// <param name="doors"></param>
        /// <returns></returns>
        public static string DownloadUserCMD(int memoryID, string cardNumber, int cardLen, int timeZoneID, string doors)
        {
            return "DownloadUser?/UserID=" + memoryID + "/LenCard=" + cardLen + "/Card=" + cardNumber + "/Pin=12345678/Mode=0/" + "TimeZone=" + timeZoneID + "/Door=" + doors;
        }

        /// <summary>
        /// Delete User: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="memoryID"></param>
        /// <returns></returns>
        public static string DeleteUserCMD(int memoryID)
        {
            return $"DeleteUser?/UserID={memoryID}";
        }

        /// <summary>
        /// Get User: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="memoryID"></param>
        /// <returns></returns>
        public static string GetUserByIdCMD(string memoryID)
        {
            return $"GetUser?/UserID={memoryID}";
        }

        /// <summary>
        /// Get All User: E05 Net v3.4, KZ-E02.NET V3.0, KZE32 Net v3.6
        /// </summary>
        /// <returns></returns>
        public static string GetAllUserCMD()
        {
            return "GetAllUser?/";
        }

        #endregion: END USER

        #region: TIMEZONE

        /// <summary>
        /// Set Timezone: KZ-E02.NET V3.0
        /// </summary>
        /// <param name="timezoneID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string SetTimezoneCMD(int timezoneID, DateTime startTime, DateTime endTime)
        {
            string _startTime = startTime.Hour.ToString("00") + startTime.Minute.ToString("00");
            string _endTime = endTime.Hour.ToString("00") + endTime.Minute.ToString("00");
            string timezoneTime = _startTime + ":" + endTime;
            return " SetTimeZone?/TZ" + timezoneID + "=" + timezoneTime;
        }

        /// <summary>
        /// Get Timezone: KZ-E02.NET V3.0
        /// </summary>
        /// <param name="timezoneID"></param>
        /// <returns></returns>
        public static string GetTimezoneCMD(int timezoneID)
        {
            return "GetTimeZone?/TimeZone=TZ" + timezoneID;
        }

        #endregion: END TIMEZONE

        #region: DATETIME

        /// <summary>
        /// Set Datetime: E05 Net v3.4,  KZ-E02.NET V3.0
        /// </summary>
        /// <param name="dt">Format:YYYYMMDDhhmmss</param>
        /// <returns></returns>
        public static string SetDateTimeCMD(string dt)
        {
            return $"SetDateTime?/" + dt;
        }

        /// <summary>
        /// Set Date Time: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SetDateTimeCMD(DateTime dt)
        {
            string year = dt.Year.ToString("0000");
            string month = dt.Month.ToString("00");
            string day = dt.Day.ToString("00");
            string hour = dt.Hour.ToString("00");
            string minute = dt.Minute.ToString("00");
            string second = dt.Second.ToString("00");
            string dtStr = year + month + day + hour + minute + second;
            return $"SetDateTime?/" + dtStr;
        }

        /// <summary>
        /// Get Date Time:: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeCMD()
        {
            return "GetDateTime?/";
        }

        #endregion: END DATETIME

        #region: ANTI PASS BACK
        /// <summary>
        /// Get ANti Pass Back: KZ-E02.NET V3.0
        /// </summary>
        /// <param name="lockID"></param>
        /// <returns></returns>
        public static string GetAntiPassBackCMD(int lockID)
        {
            return "GetAntiPassBack?/AntiPassBackLock=Lock" + lockID;
        }

        /// <summary>
        /// Set Anti Pass Back: KZ-E02.NET V3.0
        /// </summary>
        /// <param name="lockID"></param>
        /// <param name="isSet"></param>
        /// <returns></returns>
        public static string SetAntiPassBackCMD(int lockID, int mode)
        {
            string cmd = $"SetAntiPassBack?/AntiPassBackLock{lockID}={mode}";
            return cmd;
        }

        #endregion: END ANTI PASS BACK

        #region: RELAY
        /// <summary>
        /// Open Relay: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="relayIndex"></param>
        /// <returns></returns>
        public static string OpenRelayCMD(int relayIndex)
        {
            return $"SetRelay?/Relay={relayIndex:00}/State=ON";
        }
        public static string OpenRelayCMD_MT166(int relayIndex)
        {
            //SetRelay?/RelayOpen=ON/
            return $"SetRelay?/RelayOpen=ON/";

            //BỒ XUYÊN THÁI BÌNH
            //return $"SetRelay?/Relay={relayIndex}/State=ON";
        }
        public static string CloseRelayCMD_MT166(int relayIndex)
        {
            return $"SetRelay?/RelayClose=ON/";
        }
        /// <summary>
        /// Open Multy Relay: 1 to 64: E05 Net v3.6
        /// </summary>
        /// <param name="relays"></param>
        /// <returns></returns>
        public static string OpenArrayRelayCMD(List<int> relays)
        {
            string CMD = "SetArrRelay?/ArrRelay=";

            string openDoors = KzHelper.GetOpenDoor(relays, KzHelper.EM_ByteLength._8BYTE);
            return CMD + openDoors;
        }

        /// <summary>
        /// Close Relay: E05 Net v3.4
        /// </summary>
        /// <param name="relayIndex"></param>
        /// <returns></returns>
        public static string CloseRelayCMD(int relayIndex)
        {
            return $"SetRelay?/Relay={relayIndex:00}/State=OFF";
        }

        /// <summary>
        /// Set Relay Keep Open Tine: E05 Net v3.4, KZ-E02.NET V3.0
        /// </summary>
        /// <param name="DelayTimeInMiliSecond"></param>
        /// <returns></returns>
        public static string SetRelayDelayTimeCMD(int DelayTimeInMiliSecond)
        {
            if (DelayTimeInMiliSecond >= 1 && DelayTimeInMiliSecond <= 120000)
            {
                return $"SetRelayDelayTime?/Time={DelayTimeInMiliSecond}";
            }
            return "SetRelayDelayTime?/Time=100";
        }

        #endregion: END RELAY

        #region: TCP_IP

        /// <summary>
        /// Search Device Infor: ALL KZTEK DEVICE
        /// </summary>
        /// <returns></returns>
        public static string AutoDetectCMD()
        {
            return "AutoDetect?";
        }

        /// <summary>
        /// Change Network Setting: ALL KZTEK DEVICE
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="subnetMask"></param>
        /// <param name="defaultGateWay"></param>
        /// <param name="HostMac"></param>
        /// <returns></returns>
        public static string ChangeIPCMD(string IP, string subnetMask, string defaultGateWay, string HostMac)
        {
            return $"ChangeIP?/IP={IP}/SubnetMask={subnetMask}/DefaultGateWay={defaultGateWay}/HostMac={HostMac}/";
        }

        /// <summary>
        /// Change Mac Addr:  ALL KZTEK DEVICE
        /// </summary>
        /// <param name="hostMac"></param>
        /// <returns></returns>
        public static string Get_ChangeMacAddressCmd(string hostMac)
        {
            return $"ChangeMacAddress?/Mac={hostMac}";
        }

        #endregion: END TCP_IP

        #region: SYSTEM
        /// <summary>
        /// Clear Assigned Card and Card Event Memory: ALL KZTEK DEVICE
        /// </summary>
        /// <returns></returns>
        public static string Get_InitCardEventCmd()
        {
            return "InitCardEvent?/";
        }

        /// <summary>
        /// Factory Reset: ALL KZTEK DEVICE
        /// </summary>
        /// <returns></returns>
        public static string ResetDefaultCmd()
        {
            return "ResetDefault?/";
        }

        /// <summary>
        /// Restart Device: KZE05 Net v3.4
        /// </summary>
        /// <returns></returns>
        public static string RestartDeviceCMD()
        {
            return "ResetDevice?/";
        }
        /// <summary>
        /// Restart Device: KZLOck32
        /// </summary>
        /// <returns></returns>
        public static string GetInputStateE02()
        {
            return $"GetInputState?/";
        }
        public static string GetInputStateCMD(int input)
        {
            return $"GetInputState?/Input={input}";
        }
        public static string GetAllInputStateCMD()
        {
            return "GetAllInputState?/";
        }
        public static string GetIRSensorStatusCMD(int door)
        {
            return $"GetIRSensorStatus?/Door={door}";
        }
        public static string GetAllIRSensorStatusCMD()
        {
            return "GetAllIRSensorStatus?/";
        }
        #endregion: END SYSTEM
    }
}