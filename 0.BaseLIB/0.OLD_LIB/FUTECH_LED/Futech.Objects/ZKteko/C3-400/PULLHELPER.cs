using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Futech.Objects
{
    public class PULLHELPER
    {
        private const int BUFFERSIZE = 1 * 1024 * 1024;
        private static Dictionary<EM_EventType, string> EventType_str = new Dictionary<EM_EventType, string>()
        {
            {EM_EventType.NORMAL_PUNCH_OPEN," Normal Punch Open" },
            {EM_EventType.PUNCH_DURING_NORMAL_OPEN_TIMEZONE," Punch during Normal Open TimeZone" },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_PUNCH_CARD," First Card Normal Open (PunchCard)" },
            {EM_EventType.MULTI_CARD_OPEN_PUNCH_CARD,"  Multi-Card Open (Punching Card)" },
            {EM_EventType.EMERGENCY_PASSWORD_OPEN,"Emergency Password Open " },
            {EM_EventType.OPEN_DURING_NORMAL_OPEN_TIMEZONE,"Open during Normal Open TimeZone" },
            {EM_EventType.LINKAGE_EVENT_TRIGGERED," Linkage Event Triggered" },
            {EM_EventType.CANCEL_ALARM,"Cancel Alarm " },
            {EM_EventType.REMOTE_OPENING,"Remote Opening " },
            {EM_EventType.REMOTE_CLOSING,"Remote Closing " },
            {EM_EventType.DISABLE_INTRADAY_NORMAL_OPEN_TIMEZONE,"Disable Intraday Normal Open TimeZone " },
            {EM_EventType.ENABLE_INTRADAY_NORMAL_OPEN_TIMEZONE," Enable Intraday Normal Open TimeZone" },
            {EM_EventType.OPEN_AUXILIARY_OUTPUT,"Open Auxiliary Output " },
            {EM_EventType.CLOSE_AUXILIARY_OUTPUT,"Close Auxiliary Output " },
            {EM_EventType.PRESS_FINGERPRINT_OPEN," Press Fingerprint Open" },
            {EM_EventType.MULTI_CARD_OPEN_PRESS_FINGER,"lti-Card Open (Press Fingerprint) " },
            {EM_EventType.PRESS_FINGER_DURING_NORMAL_OPEN_TIMEZONE,"Press Fingerprint during NormalOpen Time Zone " },
            {EM_EventType.CARD_PLUS_FINGERPRINT_OPEN,"Card plus Fingerprint Open " },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_PRESS_FINGER," First Card Normal Open (PressFingerprint)" },
            {EM_EventType.FIRST_CARD_NORMAL_OPEN_CARD_AND_FINGER," First Card Normal Open (Card plusFingerprint)" },
            {EM_EventType.TOO_SHORT_PUNCH_INTERVAL,"Too Short Punch Interval " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_PUNCH_CARD," Door Inactive Time Zone (PunchCard)" },
            {EM_EventType.ILLEGAL_TIMEZONE," Illegal Time Zone" },
            {EM_EventType.ACCESS_DENINED,"Access Denied " },
            {EM_EventType.ANTI_PASSBACK,"Anti-Passback " },
            {EM_EventType.INTERLOCK,"Interlock " },
            {EM_EventType.MULTICARD_AUTHENTICATION_PUNCH_CARD,"Multi-Card Authentication (PunchingCard) " },
            {EM_EventType.UNREGISTER_CARD," Unregistered Card" },
            {EM_EventType.OPENING_TIMEOUT," Opening Timeout:" },
            {EM_EventType.CARD_EXPIRED,"Card Expired " },
            {EM_EventType.PASSWORD_ERROR,"Password Error " },
            {EM_EventType.TOO_SHORT_PRESS_FINGERPRINT_INTERVAL," Too Short Fingerprint PressingInterval" },
            {EM_EventType.MULTICARD_AUTHENTICATION_PRESSFINGERPRINT," Multi-Card Authentication (PressFingerprint)" },
            {EM_EventType.FINGERPRINT_EXPIRED,"Fingerprint Expired " },
            {EM_EventType.UNREGISTER_FINGERRINT,"Unregistered Fingerprint " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_PRESS_FINGERPRINT,"Door Inactive Time Zone (PressFingerprint) " },
            {EM_EventType.DOOR_INACTIVE_TIMEZONE_EXITBUTTON,"Door Inactive Time Zone (ExitButton) " },
            {EM_EventType.FAIL_CLOSE_DURING_NORMAL_OPENTIMEZONE,"Failed to Close during Normal OpenTime Zone " },
            {EM_EventType.DURESS_PASSWORD_OPNE,"Duress Password Open " },
            {EM_EventType.OPENED_ACCIDENTALLY,"Opened Accidentally " },
            {EM_EventType.DURESS_FINGERPRINT_OPEN,"Duress Fingerprint Open " },
            {EM_EventType.DOOR_OPENED_CORRECTLY,"Door Opened Correctly " },
            {EM_EventType.DOOR_CLOSED_CORRECTLY,"Door Closed Correctly " },
            {EM_EventType.EXIT_BUTTON_OPEN,"Exit button Open " },
            {EM_EventType.MULTI_CARD_OPEN_CARD_PLUS_FINGER,"Multi-Card Open (Card plusFingerprint) " },
            {EM_EventType.NOMAL_OPNE_TIMEZONE_OVER,"Normal Open Time Zone Over " },
            {EM_EventType.REMOTE_NORMAL_OPENING,"Remote Normal Opening " },
            {EM_EventType.DEVICE_START,"Device Start " },
            {EM_EventType.AUXILIARY_INPUT_DISCONNECTED,"Auxiliary Input Disconnected" },
            
            {EM_EventType.AUXILIARY_INPUT_SHORTED,"Auxiliary Input Shorted " },
            {EM_EventType.ACTUALLY_OBTAIN_DOOR_AND_ALARM_STATUS,"Actually that obtain door statusand alarm status " }
        };
        private static Dictionary<EM_ErrorCode, string> ErrorCode_Str = new Dictionary<EM_ErrorCode, string>()
        {
            {EM_ErrorCode.NOERROR,"No Error" },
            {EM_ErrorCode.THE_COMMAND_NOT_SEND_SUCCESFULLY,"The command is not sent successfully" },
            {EM_ErrorCode.THE_COMMAND_HAS_NO_RESPONSE,"The command has no response" },
            {EM_ErrorCode.THE_BUFFER_HAS_NO_RESPONSE,"The buffer is not enough" },
            {EM_ErrorCode.THE_DECOMPRESSION_FAILS,"The decompression fails" },
            {EM_ErrorCode.READ_DATA_LENGTH_NOT_CORRECT,"The length of the read data is not correct" },
            {EM_ErrorCode.THE_DECOMPRESSION_LENGTH_FAILS,"The length of the decompressed data is not consistent with the expected length" },
            {EM_ErrorCode.THE_COMMAND_REPEATED,"The command is repeated" },
            {EM_ErrorCode.THE_CONNECTION_NOT_AUTHORIZED,"The connection is not authorized" },
            {EM_ErrorCode.THE_CRC_FAIL,"Data error: The CRC result is failure" },
            {EM_ErrorCode.PULLSDK_CANNOT_RESOLVE_DATA,"Data error: PullSDK cannot resolve the data" },
            {EM_ErrorCode.DATA_PARAMETER_ERROR,"Data parameter error" },
            {EM_ErrorCode.THE_COMMAND_EXCECUTE_FAIL,"The command is not executed correctly" },
            {EM_ErrorCode.THE_COMMAND_ISNOT_AVAILABLE,"Command error: This command is not available" },
            {EM_ErrorCode.THE_PASSWORD_ISNOT_CORRECT,"The communication password is not correct" },
            {EM_ErrorCode.FAIL_TO_WRITE_FILE,"Fail to write the file" },
            {EM_ErrorCode.FAIL_TO_READ_FILE,"Fail to read the file" },
            {EM_ErrorCode.THE_FILE_DOESNOT_EXIST,"The file does not exist" },
            {EM_ErrorCode.UNKNOWN_ERROW,"Unknown error" },
            {EM_ErrorCode.THE_TABLE_STRUCT_DOES_NOT_EXIST,"The table structure does not exist" },
            {EM_ErrorCode.THE_CONDITION_FIELD_ISNOT_EXIST,"In the table structure, the Condition field does not exit" },
            {EM_ErrorCode.TOTAL_NUMBER_OF_FIELD_ISNOT_CONSISTENT,"The total number of fields is not consistent" },
            {EM_ErrorCode.SEQUENCE_OF_FIELD_ISNOT_CONSISTENT,"The sequence of fields is not consistent" },
            {EM_ErrorCode.REALTIME_EVENT_DATA_ERROR,"Real-time event data error" },
            {EM_ErrorCode.DATA_RESOLUTION_ERROR,"Data errors occur during data resolution" },
            {EM_ErrorCode.DATA_OVERFLOW,"Data overflow: The delivered data is more than 4 MB in length"},
            {EM_ErrorCode.GET_TABLE_STRUCTURE_FAIL,"Fail to get the table structure" },
            {EM_ErrorCode.INVALID_OPTION,"Invalid options" },
            {EM_ErrorCode.LIBRARY_LOAD_FAIL,"LoadLibrary failure" },
            {EM_ErrorCode.INTERFACE_INVOKE_FAIL,"Fail to invoke the interface" },
            {EM_ErrorCode.COMMUNICATION_INIT_FAIL,"Communication initialization fails" },
            {EM_ErrorCode.SERIAL_INTERFACE_START_ERROR,@"Start of a serial interface agent program fails and the cause generally relies in inexistence or occupation of the serial interface." },
            {EM_ErrorCode.TCP_IP_VERSION_ERROR,"Requested TCP/IP version error" },
            {EM_ErrorCode.VERSION_NUMBER_INCORRECT,"Incorrect version number" },
            {EM_ErrorCode.GET_PROTOCOL_TYPE_FAIL,"Fail to get the protocol type" },
            {EM_ErrorCode.SOCKET_INVALID,"Invalid SOCKET" },
            {EM_ErrorCode.SOCKET_ERROR," SOCKET error" },
            {EM_ErrorCode.HOST_ERROR,"HOST error" },
            {EM_ErrorCode.CONNECTION_ATTEMP_FAIL,"Connection attempt failed" },
            {EM_ErrorCode.RESOURCES_TEMP_INVALID,"Resources temporarily unavailable." },
            {EM_ErrorCode.AN_OPERATION_FAIL,"Resources temporarily unavailable." },
            {EM_ErrorCode.CONNECTION_RESET_BY_PEER,"Connection reset by peer." },
            {EM_ErrorCode.CONNECTION_TIMEOUT,"Connection timed out" },
            {EM_ErrorCode.CONNECTION_REFUSED,"Connection refused" },
            {EM_ErrorCode.NO_ROUTE_TO_HOST,"No route to host" },
        };
        private static Dictionary<EM_ControllerParam, string> ControllerParam_Str = new Dictionary<EM_ControllerParam, string>()
        {
            {EM_ControllerParam.SERIALNUMBER,"~SerialNumber" },
            {EM_ControllerParam.LOCKCOUNT,"LockCount" },
            {EM_ControllerParam.READERCOUNT,"ReaderCount" },
            {EM_ControllerParam.AUXINCOUNT,"AuxInCount" },
            {EM_ControllerParam.AUXOUTCOUNT,"AuxOutCount" },
            {EM_ControllerParam.COMPWD,"ComPwd" },
            {EM_ControllerParam.IPADDRESS,"IPAdress" },
            {EM_ControllerParam.GATEIPADDRESS,"GATEIPAddress" },
            {EM_ControllerParam.RS232BAUDRATE,"RS232Baudrate" },
            {EM_ControllerParam.NETMASK,"NetMask" },
            {EM_ControllerParam.ANTIPASSBACK,"AntiPassback" },
            {EM_ControllerParam.INTERLOCK,"InterLock" },

            {EM_ControllerParam.DOOR1FORCEPASSWORD,"Door1ForcePassWord" },
            {EM_ControllerParam.DOOR2FORCEPASSWORD,"Door2ForcePassWord" },
            {EM_ControllerParam.DOOR3FORCEPASSWORD,"Door3ForcePassWord" },
            {EM_ControllerParam.DOOR4FORCEPASSWORD,"Door4ForcePassWord" },

            {EM_ControllerParam.DOOR1SUPPERPASSWORD,"Door1SupperPassWord" },
            {EM_ControllerParam.DOOR2SUPPERPASSWORD,"Door2SupperPassWord" },
            {EM_ControllerParam.DOOR3SUPPERPASSWORD,"Door3SupperPassWord" },
            {EM_ControllerParam.DOOR4SUPPERPASSWORD,"Door4SupperPassWord" },

            {EM_ControllerParam.DOOR1CLOSEANDLOCK,"Door1CloseAndLock" },
            {EM_ControllerParam.DOOR2CLOSEANDLOCK,"Door2CloseAndLock" },
            {EM_ControllerParam.DOOR3CLOSEANDLOCK,"Door3CloseAndLock" },
            {EM_ControllerParam.DOOR4CLOSEANDLOCK,"Door4CloseAndLock" },

            {EM_ControllerParam.DOOR1SENSORTYPE,"Door1SensorType" },
            {EM_ControllerParam.DOOR2SENSORTYPE,"Door2SensorType" },
            {EM_ControllerParam.DOOR3SENSORTYPE,"Door3SensorType" },
            {EM_ControllerParam.DOOR4SENSORTYPE,"Door4SensorType" },

            {EM_ControllerParam.DOOR1DRIVERTIME,"Door1DriverTime" },
            {EM_ControllerParam.DOOR2DRIVERTIME,"Door2DriverTime" },
            {EM_ControllerParam.DOOR3DRIVERTIME,"Door3DriverTime" },
            {EM_ControllerParam.DOOR4DRIVERTIME,"Door4DriverTime" },

            {EM_ControllerParam.DOOR1DETECTORTIME,"Door1DetectorTime" },
            {EM_ControllerParam.DOOR2DETECTORTIME,"Door2DetectorTime" },
            {EM_ControllerParam.DOOR3DETECTORTIME,"Door3DetectorTime" },
            {EM_ControllerParam.DOOR4DETECTORTIME,"Door4DetectorTime" },

            {EM_ControllerParam.DOOR1VERIFYTYPE,"Door1VerifyType" },
            {EM_ControllerParam.DOOR2VERIFYTYPE,"Door2VerifyType" },
            {EM_ControllerParam.DOOR3VERIFYTYPE,"Door3VerifyType" },
            {EM_ControllerParam.DOOR4VERIFYTYPE,"Door4VerifyType" },

            {EM_ControllerParam.DOOR1MULTICARDOPENDOOR,"Door1MultiCardOpenDoor" },
            {EM_ControllerParam.DOOR2MULTICARDOPENDOOR,"Door1MultiCardOpenDoor" },
            {EM_ControllerParam.DOOR3MULTICARDOPENDOOR,"Door1MultiCardOpenDoor" },
            {EM_ControllerParam.DOOR4MULTICARDOPENDOOR,"Door1MultiCardOpenDoor" },

            {EM_ControllerParam.DOOR1FRISTCARDOPENDOOR,"Door1FirstCardOpenDoor" },
            {EM_ControllerParam.DOOR2FRISTCARDOPENDOOR,"Door2FirstCardOpenDoor" },
            {EM_ControllerParam.DOOR3FRISTCARDOPENDOOR,"Door3FirstCardOpenDoor" },
            {EM_ControllerParam.DOOR4FRISTCARDOPENDOOR,"Door4FirstCardOpenDoor" },

            {EM_ControllerParam.DOOR1VALIDTZ,"Door1ValidTZ" },
            {EM_ControllerParam.DOOR2VALIDTZ,"Door2ValidTZ" },
            {EM_ControllerParam.DOOR3VALIDTZ,"Door3ValidTZ" },
            {EM_ControllerParam.DOOR4VALIDTZ,"Door4ValidTZ" },

            {EM_ControllerParam.DOOR1KEEPOPENTIMEZONE,"Door1KeepOpenTimeZone" },
            {EM_ControllerParam.DOOR2KEEPOPENTIMEZONE,"Door2KeepOpenTimeZone" },
            {EM_ControllerParam.DOOR3KEEPOPENTIMEZONE,"Door3KeepOpenTimeZone" },
            {EM_ControllerParam.DOOR4KEEPOPENTIMEZONE,"Door4KeepOpenTimeZone" },

            {EM_ControllerParam.WATCHDOG,"WatchDog" },
            {EM_ControllerParam.DOOR4TODOOR2,"Door4ToDoor2" },

            {EM_ControllerParam.DOOR1_CANCEL_KEEP_OPENDAY,"Door1CancelKeepOpenDay" },
            {EM_ControllerParam.DOOR2_CANCEL_KEEP_OPENDAY,"Door2CancelKeepOpenDay" },
            {EM_ControllerParam.DOOR3_CANCEL_KEEP_OPENDAY,"Door3CancelKeepOpenDay" },
            {EM_ControllerParam.DOOR4_CANCEL_KEEP_OPENDAY,"Door4CancelKeepOpenDay" },

            {EM_ControllerParam.BACKUP_TIME,"BackupTime" },
            {EM_ControllerParam.REBOOT,"Reboot" },
            {EM_ControllerParam.DATETIME,"DateTime" },
            {EM_ControllerParam.INBIO_TWOWAY,"InBIOTowWay" },
            {EM_ControllerParam.ZKFBVERSION,"~ZKFPVersion" },
            {EM_ControllerParam.DSTF,"~DSTF" },
            {EM_ControllerParam.DAYLIGHTSAVINGTIMEON,"DaylightSavingTimeOn" },
            {EM_ControllerParam.DLSTMODE,"DLSTMode" },
            {EM_ControllerParam.DAYLIGHTSAVINGTIME,"DaylightSavingTime" },
            {EM_ControllerParam.STANDARDTIME,"StandardTime" },
            {EM_ControllerParam.WEEKOFMONTH1,"WeekOfMonth1" },
            {EM_ControllerParam.WEEKOFMONTH2,"WeekOfMonth2" },
            {EM_ControllerParam.WEEKOFMONTH3,"WeekOfMonth3" },
            {EM_ControllerParam.WEEKOFMONTH4,"WeekOfMonth4" },
            {EM_ControllerParam.WEEKOFMONTH5,"WeekOfMonth5" },
            {EM_ControllerParam.WEEKOFMONTH6,"WeekOfMonth6" },
            {EM_ControllerParam.WEEKOFMONTH7,"WeekOfMonth7" },
            {EM_ControllerParam.WEEKOFMONTH8,"WeekOfMonth8" },
            {EM_ControllerParam.WEEKOFMONTH9,"WeekOfMonth9" },
            {EM_ControllerParam.WEEKOFMONTH10,"WeekOfMonth10" },
        };
        public string GetLastErrorMessage(EM_ErrorCode errorCode)
        {
            string result = string.Empty;
            ErrorCode_Str.TryGetValue(errorCode, out result);
            return result;
        }
        public string GetEventType(EM_EventType eventType)
        {
            string result = string.Empty;
            EventType_str.TryGetValue(eventType, out result);
            return result;
        }
        public enum EM_OperationID
        {
            OutputOperation=1,
            CancelAlarm=2,
            RestartDevice =3,
            EnableOrDisableNormalOpenState =4,
        }
        public enum EM_FunctionTableNam
        {
            user,
            userauthorize,
            timezone,
            transaction,
            firstcard,
            multicard,
            templatev10
        }
        public enum EM_ControllerParam { 
            SERIALNUMBER =0,
            LOCKCOUNT=1,
            READERCOUNT = 2,
            AUXINCOUNT =3,
            AUXOUTCOUNT=4,
            COMPWD =5,
            IPADDRESS =6,
            GATEIPADDRESS=7,
            RS232BAUDRATE =8,
            NETMASK,
            ANTIPASSBACK,
            INTERLOCK,

            DOOR1FORCEPASSWORD,
            DOOR2FORCEPASSWORD,
            DOOR3FORCEPASSWORD,
            DOOR4FORCEPASSWORD,

            DOOR1SUPPERPASSWORD,
            DOOR2SUPPERPASSWORD,
            DOOR3SUPPERPASSWORD,
            DOOR4SUPPERPASSWORD,

            DOOR1CLOSEANDLOCK,
            DOOR2CLOSEANDLOCK,
            DOOR3CLOSEANDLOCK,
            DOOR4CLOSEANDLOCK,

            DOOR1SENSORTYPE,
            DOOR2SENSORTYPE,
            DOOR3SENSORTYPE,
            DOOR4SENSORTYPE,

            DOOR1DRIVERTIME,
            DOOR2DRIVERTIME,
            DOOR3DRIVERTIME,
            DOOR4DRIVERTIME,

            DOOR1DETECTORTIME,
            DOOR2DETECTORTIME,
            DOOR3DETECTORTIME,
            DOOR4DETECTORTIME,

            DOOR1VERIFYTYPE,
            DOOR2VERIFYTYPE,
            DOOR3VERIFYTYPE,
            DOOR4VERIFYTYPE,

            DOOR1MULTICARDOPENDOOR,
            DOOR2MULTICARDOPENDOOR,
            DOOR3MULTICARDOPENDOOR,
            DOOR4MULTICARDOPENDOOR,

            DOOR1FRISTCARDOPENDOOR,
            DOOR2FRISTCARDOPENDOOR,
            DOOR3FRISTCARDOPENDOOR,
            DOOR4FRISTCARDOPENDOOR,

            DOOR1VALIDTZ,
            DOOR2VALIDTZ,
            DOOR3VALIDTZ,
            DOOR4VALIDTZ,

            DOOR1KEEPOPENTIMEZONE,
            DOOR2KEEPOPENTIMEZONE,
            DOOR3KEEPOPENTIMEZONE,
            DOOR4KEEPOPENTIMEZONE,

            DOOR1INTERTIME,
            DOOR2INTERTIME,
            DOOR3INTERTIME,
            DOOR4INTERTIME,

            WATCHDOG,
            DOOR4TODOOR2,

            DOOR1_CANCEL_KEEP_OPENDAY,
            DOOR2_CANCEL_KEEP_OPENDAY,
            DOOR3_CANCEL_KEEP_OPENDAY,
            DOOR4_CANCEL_KEEP_OPENDAY,

            BACKUP_TIME,

            REBOOT,
            DATETIME,
            INBIO_TWOWAY,
            ZKFBVERSION,
            DSTF,
            DAYLIGHTSAVINGTIMEON,
            DLSTMODE,
            DAYLIGHTSAVINGTIME,
            STANDARDTIME,
            WEEKOFMONTH1,
            WEEKOFMONTH2,
            WEEKOFMONTH3,
            WEEKOFMONTH4,
            WEEKOFMONTH5,
            WEEKOFMONTH6,
            WEEKOFMONTH7,
            WEEKOFMONTH8,
            WEEKOFMONTH9,
            WEEKOFMONTH10,
        }
        public enum EM_EventType
        {
            NORMAL_PUNCH_OPEN,
            PUNCH_DURING_NORMAL_OPEN_TIMEZONE,
            FIRST_CARD_NORMAL_OPEN_PUNCH_CARD,
            MULTI_CARD_OPEN_PUNCH_CARD,
            EMERGENCY_PASSWORD_OPEN,
            OPEN_DURING_NORMAL_OPEN_TIMEZONE,
            LINKAGE_EVENT_TRIGGERED,
            CANCEL_ALARM,
            REMOTE_OPENING,
            REMOTE_CLOSING,
            DISABLE_INTRADAY_NORMAL_OPEN_TIMEZONE,
            ENABLE_INTRADAY_NORMAL_OPEN_TIMEZONE,
            OPEN_AUXILIARY_OUTPUT,
            CLOSE_AUXILIARY_OUTPUT,
            PRESS_FINGERPRINT_OPEN,
            MULTI_CARD_OPEN_PRESS_FINGER,
            PRESS_FINGER_DURING_NORMAL_OPEN_TIMEZONE,
            CARD_PLUS_FINGERPRINT_OPEN,
            FIRST_CARD_NORMAL_OPEN_PRESS_FINGER,
            FIRST_CARD_NORMAL_OPEN_CARD_AND_FINGER,
            TOO_SHORT_PUNCH_INTERVAL,
            DOOR_INACTIVE_TIMEZONE_PUNCH_CARD,
            ILLEGAL_TIMEZONE,
            ACCESS_DENINED,
            ANTI_PASSBACK,
            INTERLOCK,
            MULTICARD_AUTHENTICATION_PUNCH_CARD,
            UNREGISTER_CARD,
            OPENING_TIMEOUT,
            CARD_EXPIRED,
            PASSWORD_ERROR,
            TOO_SHORT_PRESS_FINGERPRINT_INTERVAL,
            MULTICARD_AUTHENTICATION_PRESSFINGERPRINT,
            FINGERPRINT_EXPIRED,
            UNREGISTER_FINGERRINT,
            DOOR_INACTIVE_TIMEZONE_PRESS_FINGERPRINT,
            DOOR_INACTIVE_TIMEZONE_EXITBUTTON,
            FAIL_CLOSE_DURING_NORMAL_OPENTIMEZONE,
            DURESS_PASSWORD_OPNE,
            OPENED_ACCIDENTALLY,
            DURESS_FINGERPRINT_OPEN,
            DOOR_OPENED_CORRECTLY,
            DOOR_CLOSED_CORRECTLY,
            EXIT_BUTTON_OPEN,
            MULTI_CARD_OPEN_CARD_PLUS_FINGER,
            NOMAL_OPNE_TIMEZONE_OVER,
            REMOTE_NORMAL_OPENING,
            DEVICE_START,
            AUXILIARY_INPUT_DISCONNECTED,
            AUXILIARY_INPUT_SHORTED,
            ACTUALLY_OBTAIN_DOOR_AND_ALARM_STATUS
        }
        public enum EM_ErrorCode
        {
            NOERROR = 0,
            THE_COMMAND_NOT_SEND_SUCCESFULLY = -1,
            THE_COMMAND_HAS_NO_RESPONSE = -2,
            THE_BUFFER_HAS_NO_RESPONSE = -3,
            THE_DECOMPRESSION_FAILS =-4,
            READ_DATA_LENGTH_NOT_CORRECT = -5,
            THE_DECOMPRESSION_LENGTH_FAILS = -6,
            THE_COMMAND_REPEATED = -7,
            THE_CONNECTION_NOT_AUTHORIZED = -8,
            THE_CRC_FAIL =-9,
            PULLSDK_CANNOT_RESOLVE_DATA=-10,
            DATA_PARAMETER_ERROR =-11,
            THE_COMMAND_EXCECUTE_FAIL=-12,
            THE_COMMAND_ISNOT_AVAILABLE = -13,
            THE_PASSWORD_ISNOT_CORRECT =-14,
            FAIL_TO_WRITE_FILE = -15,
            FAIL_TO_READ_FILE=-16,
            THE_FILE_DOESNOT_EXIST = -17,
            UNKNOWN_ERROW = -99,
            THE_TABLE_STRUCT_DOES_NOT_EXIST = -100,
            THE_CONDITION_FIELD_ISNOT_EXIST = -101,
            TOTAL_NUMBER_OF_FIELD_ISNOT_CONSISTENT = -102,
            SEQUENCE_OF_FIELD_ISNOT_CONSISTENT = -103,
            REALTIME_EVENT_DATA_ERROR = -104,
            DATA_RESOLUTION_ERROR = -105,
            DATA_OVERFLOW = -106,
            GET_TABLE_STRUCTURE_FAIL = -107,
            INVALID_OPTION = -108,
            LIBRARY_LOAD_FAIL = -201,
            INTERFACE_INVOKE_FAIL = -202,
            COMMUNICATION_INIT_FAIL = -203,
            SERIAL_INTERFACE_START_ERROR = -206,
            TCP_IP_VERSION_ERROR = -301,
            VERSION_NUMBER_INCORRECT = -302,
            GET_PROTOCOL_TYPE_FAIL = -303,
            SOCKET_INVALID = -304,
            SOCKET_ERROR = -305,
            HOST_ERROR =-306,
            CONNECTION_ATTEMP_FAIL=-307,
            RESOURCES_TEMP_INVALID = 10035,
            AN_OPERATION_FAIL = 10038,
            CONNECTION_RESET_BY_PEER = 10054,
            CONNECTION_TIMEOUT = 10060,
            CONNECTION_REFUSED = 10061,
            NO_ROUTE_TO_HOST = 10065
        }
        public int PullLastError()
        {
            return PULLSDK.PullLastError();
        }

        #region: Connect Behavior
        /// <summary>
        /// Connect Device By TCP
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="timeOut"></param>
        /// <param name="password"></param>
        /// <param name="userHandel"></param>
        /// <returns></returns>
        public IntPtr ConnectByTCP(string ip, int port, int timeOut, string password, ref IntPtr userHandel)
        {
            string _params = $"protocol=TCP,ipaddress={ip},port={port},timeout={timeOut},passwd={password}";
            userHandel = PULLSDK.Connect(_params);
            return userHandel;
        }
        /// <summary>
        /// Connect Device By RS485
        /// </summary>
        /// <param name="comPort"></param>
        /// <param name="baudRate"></param>
        /// <param name="deviceID"></param>
        /// <param name="timeOut"></param>
        /// <param name="password"></param>
        /// <param name="userHandel"></param>
        /// <returns></returns>
        public IntPtr ConnectByRS485(string comPort, int baudRate, int deviceID, int timeOut, string password, ref IntPtr userHandel)
        {
            string _params = $"protocol=RS485,port={comPort},baudRate={baudRate}bps,deviced={deviceID},timeout={timeOut},passwd={password}";
            userHandel = PULLSDK.Connect(_params);
            return userHandel;
        }
        /// <summary>
        /// Disconnect Device
        /// </summary>
        /// <param name="userHandel">returned by connect Function</param>
        public void Disconnect(ref IntPtr userHandel)
        {
            if (userHandel != IntPtr.Zero)
            {
                PULLSDK.Disconnect(userHandel);
                userHandel = IntPtr.Zero;
            }
        }
        #endregion

        #region: Device Parameter
        //Get Device Param
        /// <summary>
        /// Get Device Parameter: IPAddress, Baudrate
        /// </summary>
        /// <param name="userHandle">The Handle which is returned by success connection</param>
        /// <param name="buffer">The Buffer used to receive returned data</param>
        /// <param name="buffersize">the size of used buffer</param>
        /// <param name="items">The Parameter of device to reads Ex:DeviceID,Door1InterTime</param>
        /// <returns></returns>
        public int GetDeviceParam(IntPtr userHandle, ref byte buffer, int buffersize, string items)
        {
            return PULLSDK.GetDeviceParam(userHandle, ref buffer, buffersize, items);
        }
        public int GetDeviceParam(IntPtr userHandle, ref byte buffer, int buffersize, string[] items)
        {
            string itemList = "";
            string[] empty = new string[items.Length];
            CombineArrayToString(items, empty, ",", ref itemList);
            return PULLSDK.GetDeviceParam(userHandle, ref buffer, buffersize, itemList);
        }
        public string GetDeviceParam(IntPtr userHandle, EM_ControllerParam controllerParam)
        {
            int Buffersize = 10 * 1024 * 1024;
            byte[] buffer = new byte[Buffersize];
            string item = GetControllerParam(controllerParam);
            if (userHandle != IntPtr.Zero)
            {
                int ret = GetDeviceParam(userHandle, ref buffer[0], Buffersize, item);
                if (ret >= 0)
                {
                    string response = Encoding.Default.GetString(buffer);
                    string[] value = response.Split(',');
                    string[] subValue = value[0].Split('=');
                    if (subValue.Length >= 2)
                    {
                        if (controllerParam == EM_ControllerParam.DATETIME)
                        {
                            return GetDateTime(Convert.ToInt32(subValue[1]));
                        }
                        return subValue[1];
                    }
                    return "";
                }
                else
                {
                    return GetLastErrorMessage((EM_ErrorCode)ret);
                }
            }
            else
            {
                return "";
            }
        }
        //Set Device Param
        /// <summary>
        /// Change Device param: IP, baudrate,....
        /// </summary>
        /// <param name="userHandle">returned by connect function</param>
        /// <param name="itemValues">Ex:IpAddress=192.168.1.1,Baudrate=38400</param>
        /// <returns></returns>
        public string SetDeviceParam(IntPtr userHandle, string itemValues)
        {
            if (PULLSDK.SetDeviceParam(userHandle, itemValues) == 0)
            {
                return "Success";
            }
            int error = PULLSDK.PullLastError();
            return GetLastErrorMessage((EM_ErrorCode)error);
        }
        /// <summary>
        /// Change Device param: IP, baudrate,....
        /// </summary>
        /// <param name="userHandel">returned by connect Function</param>
        /// <param name="item">List Item set</param>
        /// <param name="value">List value of item. value length must be same as item length</param>
        /// <returns></returns>
        public string SetDeviceParam(IntPtr userHandel, string[] item, string[] value)
        {
            string itemValues = "";
            CombineArrayToString(item, value, ",", ref itemValues);
            if (PULLSDK.SetDeviceParam(userHandel, itemValues) == 0)
            {
                return "Success";
            }
            int error = PULLSDK.PullLastError();
            return GetLastErrorMessage((EM_ErrorCode)error);
        }
        #endregion

        #region: Control Device
        public int ControlDevice(IntPtr userHandle, int operationid, int param1, int param2, int param3, int param4, string options)
        {
            return PULLSDK.ControlDevice(userHandle, operationid, param1, param2, param3, param4, options);
        }
        #endregion

        #region: Device Data
        #region: Get
        /// <summary>
        /// GetDeviceData
        /// </summary>
        /// <param name="userHandle"></param>
        /// <param name="buffer"></param>
        /// <param name="buffersize"></param>
        /// <param name="tablename"></param>
        /// <param name="filename"></param>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public int GetDeviceData(IntPtr userHandle, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options)
        {
            return PULLSDK.GetDeviceData(userHandle, ref buffer, buffersize, tablename, filename, filter, options);
        }
        /// <summary>
        /// GetDeviceData
        /// </summary>
        /// <param name="_userHandle"></param>
        /// <param name="tblName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public string GetDeviceData(IntPtr _userHandle, string tblName, string filter)
        {
            int ret = 0;
            string fieldName = "*";
            byte[] responseBuffer = new byte[BUFFERSIZE];
            string response = "";
            ret = PULLSDK.GetDeviceData(_userHandle, ref responseBuffer[0], BUFFERSIZE, tblName, fieldName, filter, "");
            if (ret >= 0)
            {
                response = Encoding.ASCII.GetString(responseBuffer);
            }
            GC.Collect();
            return response;
        }
        #region: Get By Pin
        private string GetUserCardInforByPin(IntPtr userHandle, string pin)
        {
            string result = GetDeviceData(userHandle, EM_FunctionTableNam.user.ToString(), "Pin=" + pin);
            if (result != "")
            {
                string[] subRes = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (subRes.Length < 3)
                {
                    return "";
                }
                return subRes[1];
            }
            return "";
        }
        private string GetUserDoorInforByPin(IntPtr userHandle, string pin)
        {
            string result = GetDeviceData(userHandle, EM_FunctionTableNam.userauthorize.ToString(), "Pin=" + pin);
            if (result != "")
            {
                string[] subRes = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (subRes.Length >= 3)
                {
                    return subRes[1].Split(',')[1] + "," + subRes[1].Split(',')[2];
                }
            }
            return "0" + "," + "0";
        }
        private List<string> GetUserFingerByPin(IntPtr userHandle, string pin, List<string> fingers)
        {
            string result = GetDeviceData(userHandle, EM_FunctionTableNam.templatev10.ToString(), "Pin=" + pin);
            if (result != "")
            {
                string[] subRes = result.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (subRes.Length >= 3)
                {
                    for(int i = 1; i < subRes.Length - 1; i++)
                    {
                        fingers.Add(subRes[i].Split(',')[5]);
                    }
                }
                return fingers;
            }
            return null;
        }
        public Employee GetUserInforByPIN(IntPtr userHandle, string pin)
        {
            Employee employee = new Employee();
            string userCardInfor = GetUserCardInforByPin(userHandle, pin);
            string userDoorInfor = GetUserDoorInforByPin(userHandle, pin);
            List<string> fingers = new List<string>();
            GetUserFingerByPin(userHandle, pin, fingers);
            if (userCardInfor == "")
            {
                return null;
            }
            //Card
            //employee.CardNumber = userCardInfor.Split(',')[1].ToString();
            //employee.Password = userCardInfor.Split(',')[3].ToString();
            //employee.StartTime = userCardInfor.Split(',')[5].ToString();
            //employee.EndTime = userCardInfor.Split(',')[6].ToString();
            ////Door
            //string[] doors = userDoorInfor.Split(',');
            //employee.TimeZoneID = Convert.ToInt32(userDoorInfor.Split(',')[0].ToString());
            //employee.DoorID = Convert.ToInt32(userDoorInfor.Split(',')[1].ToString());
            //Finger
            if (fingers == null)
            {
                return employee;
            }
            employee.Fingers1 = fingers.Count > 0 ? fingers[0] : "";
            employee.Fingers2 = fingers.Count > 1 ? fingers[1] : "";
            employee.Fingers3 = fingers.Count > 2 ? fingers[2] : "";
            employee.Fingers4 = fingers.Count > 3 ? fingers[3] : "";
            employee.Fingers5 = fingers.Count > 4 ? fingers[4] : "";
            employee.Fingers6 = fingers.Count > 5 ? fingers[5] : "";
            employee.Fingers7 = fingers.Count > 6 ? fingers[6] : "";
            employee.Fingers8 = fingers.Count > 7 ? fingers[7] : "";
            employee.Fingers9 = fingers.Count > 8 ? fingers[8] : "";
            employee.Fingers10 = fingers.Count > 9 ? fingers[9] : "";

            return employee;

        }
        #endregion

        #region: Get All
        private List<Employee> GetAllUserCardInfor(IntPtr userHandle, List<Employee> searchEmployees)
        {
            //string response = GetDeviceData(userHandle, EM_FunctionTableNam.user.ToString(), "");
            //if (response != "")
            //{
            //    //UID,CardNo,Pin,Password,Group,StartTime,EndTime,Name,SuperAuthorize
            //    string[] subResponse = response.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //    if (subResponse.Length > 2)
            //    {
            //        for (int i = 1; i < subResponse.Length - 1; i++)
            //        {
            //            Employee user = new Employee();
            //            string[] userInfor = subResponse[i].Split(',');
            //            user.UserID = userInfor[2].ToString();
            //            user.CardNumber = userInfor[1].ToString();
            //            user.Password = userInfor[3].ToString();
            //            user.StartTime = userInfor[5].ToString();
            //            user.EndTime = userInfor[6].ToString();
            //            searchEmployees.Add(user);
            //        }
            //        return searchEmployees;
            //    }
            //    return null;
            //}
            return null;
        }
        private List<Employee> GetAllUserDoorInfor(IntPtr userHandle, List<Employee> searchEmployees)
        {
            //if (searchEmployees != null)
            //{
            //    foreach (Employee employee in searchEmployees)
            //    {
            //        string userDoorInfor = GetUserDoorInforByPin(userHandle, employee.UserID);
            //        employee.TimeZoneID = userDoorInfor.Split(',')[0] == "" ? 0 : Convert.ToInt32(userDoorInfor.Split(',')[0].ToString());
            //        employee.DoorID = userDoorInfor.Split(',')[1] == "" ? 0 : Convert.ToInt32(userDoorInfor.Split(',')[1].ToString());
            //    }
            //}
            //return searchEmployees;
            return null;
        }
        private List<Employee> GetAllUserFingerInfor(IntPtr userHandle, List<Employee> searchEmployees)
        {
            //if (searchEmployees != null)
            //{
            //    foreach (Employee employee in searchEmployees)
            //    {
            //        List<string> fingers = new List<string>();
            //        GetUserFingerByPin(userHandle, employee.UserID, fingers);
            //        if (fingers == null)
            //        {
            //            continue;
            //        }
            //        employee.Fingers1 = fingers.Count > 0 ? fingers[0] : "";
            //        employee.Fingers2 = fingers.Count > 1 ? fingers[1] : "";
            //        employee.Fingers3 = fingers.Count > 2 ? fingers[2] : "";
            //        employee.Fingers4 = fingers.Count > 3 ? fingers[3] : "";
            //        employee.Fingers5 = fingers.Count > 4 ? fingers[4] : "";
            //        employee.Fingers6 = fingers.Count > 5 ? fingers[5] : "";
            //        employee.Fingers7 = fingers.Count > 6 ? fingers[6] : "";
            //        employee.Fingers8 = fingers.Count > 7 ? fingers[7] : "";
            //        employee.Fingers9 = fingers.Count > 8 ? fingers[8] : "";
            //        employee.Fingers10 = fingers.Count > 9 ? fingers[9] : "";
            //    }
            //}
            //return searchEmployees;
            return null;

        }
        public List<Employee> GetMultyUserInforByPin(IntPtr userHandle, List<Employee> searchEmployees)
        {
            //List<Employee> emps = new List<Employee>();
            //foreach(Employee employee in searchEmployees)
            //{
            //    Employee result = GetUserInforByPIN(userHandle, employee.UserID);
            //    if (result != null)
            //    {
            //        emps.Add(result);
            //    }
            //}
            //return emps;
            return null;
        }
        public List<Employee> GetAllUser(IntPtr userHandle, List<Employee> searchEmployees)
        {
            GetAllUserCardInfor(userHandle, searchEmployees);
            GetAllUserDoorInfor(userHandle, searchEmployees);
            GetAllUserFingerInfor(userHandle, searchEmployees);
            return searchEmployees;
        }
        #endregion
        #endregion

        #region:Delete
        public int DeleteDeviceData(IntPtr userHandle, string tablename, string data, string options)
        {
            return PULLSDK.DeleteDeviceData(userHandle, tablename, data, options);
        }
        public bool DeleteUserByPIN(IntPtr userHandle, string pin)
        {
            if (PULLSDK.DeleteDeviceData(userHandle, EM_FunctionTableNam.user.ToString(), "Pin=" + pin, "") >= 0)
            {
                if (PULLSDK.DeleteDeviceData(userHandle, EM_FunctionTableNam.templatev10.ToString(), "Pin=" + pin, "") >= 0)
                {
                    if (PULLSDK.DeleteDeviceData(userHandle, EM_FunctionTableNam.userauthorize.ToString(), "Pin=" + pin, "") >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region: Download
        public bool DownloadUser(IntPtr userHandle, string pin, string cardno, Employee employee, string starttime, string endtime, string password, int door, int timeZoneID)
        {
            int ret = 0;
            string datas = "";
            string options = "";
            datas = "Pin=" + pin + "\tCardNo=" + cardno + "\tPassword=" + password + "\tStartTime=" + starttime + "\tEndTime=" + endtime;
            ret = PULLSDK.SetDeviceData(userHandle, EM_FunctionTableNam.user.ToString(), datas, options);
            if (ret == 0)
            {
                List<string> fingerTemplateList = new List<string>();
                fingerTemplateList.Add(employee.Fingers1);
                fingerTemplateList.Add(employee.Fingers2);
                fingerTemplateList.Add(employee.Fingers3);
                fingerTemplateList.Add(employee.Fingers4);
                fingerTemplateList.Add(employee.Fingers5);
                fingerTemplateList.Add(employee.Fingers6);
                fingerTemplateList.Add(employee.Fingers7);
                fingerTemplateList.Add(employee.Fingers8);
                fingerTemplateList.Add(employee.Fingers9);
                fingerTemplateList.Add(employee.Fingers10);
                datas = "";
                int fingerIndex = Convert.ToInt32(pin);
                for (int i = 0; i < fingerTemplateList.Count; i++)
                {
                    if (fingerTemplateList[i] != "")
                    {
                        if (datas != "")
                        {
                            datas += "\r\n";
                        }
                        datas += "Pin=" + pin + "\tFingerID=" + fingerIndex + "\tValid=1\tTemplate=" + fingerTemplateList[i];
                        fingerIndex++;
                    }
                }
                ret = PULLSDK.SetDeviceData(userHandle, EM_FunctionTableNam.templatev10.ToString(), datas, options);
                if (ret == 0)
                {
                    datas = "Pin=" + pin + "\tAuthorizeDoorId=" + door + "\tAuthorizeTimezoneId=" + timeZoneID;
                    ret = PULLSDK.SetDeviceData(userHandle, EM_FunctionTableNam.userauthorize.ToString(), datas, options);
                    if (ret == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    DeleteUserByPIN(userHandle, pin);
                }

            }
            return false;
        }
        public int SetDeviceData(IntPtr userID, string tablename, string data, string options)
        {
            return PULLSDK.SetDeviceData(userID, tablename, data, options);
        }
        #endregion

        #endregion

        #region:Event
        //Card Number
        public string GetCardNumber(IntPtr userID, ref bool isCancel)
        {
            int ret = 0, i = 0, buffersize = 256;
            string str = "";
            string[] tmp = null;
            byte[] buffer = new byte[256];
            while (!isCancel)
            {
                if (tmp == null || tmp[2].Length != 7)
                {
                    ret = PULLSDK.GetRTLog(userID, ref buffer[0], buffersize);
                    if (ret >= 0)
                    {
                        str = Encoding.Default.GetString(buffer);
                        tmp = str.Split(',');
                    }
                }
                else
                {
                    return tmp[2];
                }
                Thread.Sleep(1000);
            }
            return "";

        }
        #endregion

        private void CombineArrayToString(string[] array1, string[] array2, string seperaterStr, ref string response)
        {
            try
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    response = response + array1[i] + "=" + array2[i];
                    if (i < array1.Length - 1)
                    {
                        response += seperaterStr;
                    }
                }
            }
            catch
            {
                response = "";
            }
        }
        public string GetControllerParam(EM_ControllerParam controllerParam)
        {
            string result = string.Empty;
            ControllerParam_Str.TryGetValue(controllerParam, out result);
            return result;
        }
        public static string GetDateTime(int dateTime)
        {
            int second = dateTime % 60;
            int minute = (dateTime / 60) % 60;
            int hour = (dateTime / 3600) % 24;
            int day = (dateTime / 86400) % 31 + 1;
            int month = (dateTime / 2678400) % 12 + 1;
            int year = (dateTime / 32140800) + 2000;
            return year + ":" + month + ":" + day + "-" + hour + ":" + minute + ":" + second;
        }


    }
}
