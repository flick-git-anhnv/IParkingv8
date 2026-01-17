using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParkingv5.Controller.ZktecoDevices.PULL
{
    public class PULLHELPER
    {
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

        public enum EM_OperationID
        {
            OutputOperation = 1,
            CancelAlarm = 2,
            RestartDevice = 3,
            EnableOrDisableNormalOpenState = 4,
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
        public enum EM_ControllerParam
        {
            SERIALNUMBER = 0,
            LOCKCOUNT = 1,
            READERCOUNT = 2,
            AUXINCOUNT = 3,
            AUXOUTCOUNT = 4,
            COMPWD = 5,
            IPADDRESS = 6,
            GATEIPADDRESS = 7,
            RS232BAUDRATE = 8,
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

        public enum EM_ErrorCode
        {
            NOERROR = 0,
            THE_COMMAND_NOT_SEND_SUCCESFULLY = -1,
            THE_COMMAND_HAS_NO_RESPONSE = -2,
            THE_BUFFER_HAS_NO_RESPONSE = -3,
            THE_DECOMPRESSION_FAILS = -4,
            READ_DATA_LENGTH_NOT_CORRECT = -5,
            THE_DECOMPRESSION_LENGTH_FAILS = -6,
            THE_COMMAND_REPEATED = -7,
            THE_CONNECTION_NOT_AUTHORIZED = -8,
            THE_CRC_FAIL = -9,
            PULLSDK_CANNOT_RESOLVE_DATA = -10,
            DATA_PARAMETER_ERROR = -11,
            THE_COMMAND_EXCECUTE_FAIL = -12,
            THE_COMMAND_ISNOT_AVAILABLE = -13,
            THE_PASSWORD_ISNOT_CORRECT = -14,
            FAIL_TO_WRITE_FILE = -15,
            FAIL_TO_READ_FILE = -16,
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
            HOST_ERROR = -306,
            CONNECTION_ATTEMP_FAIL = -307,
            RESOURCES_TEMP_INVALID = 10035,
            AN_OPERATION_FAIL = 10038,
            CONNECTION_RESET_BY_PEER = 10054,
            CONNECTION_TIMEOUT = 10060,
            CONNECTION_REFUSED = 10061,
            NO_ROUTE_TO_HOST = 10065
        }
        public IntPtr ConnectByTCP(string ip, string port, int timeOut, string password, ref IntPtr userID)
        {
            try
            {
                string _params = $"protocol=TCP,ipaddress={ip},port={port},timeout={timeOut},passwd={password}";
                userID = PULLSDK.Connect(_params);
                return userID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return IntPtr.Zero;
            }
        }

        public IntPtr ConnectByRS485(string comPort, int baudRate, int deviceID, int timeOut, string password, ref IntPtr userID)
        {
            string _params = $"protocol=RS485,port={comPort},baudRate={baudRate},deviced={deviceID},timeout={timeOut},passwd={password}";
            userID = PULLSDK.Connect(_params);
            return userID;
        }

        public void Disconnect(ref IntPtr userID)
        {
            if (userID != IntPtr.Zero)
            {
                PULLSDK.Disconnect(userID);
                userID = IntPtr.Zero;
            }
        }
        public string SetDeviceParam(IntPtr userID, string itemValues)
        {
            if (PULLSDK.SetDeviceParam(userID, itemValues) == 0)
            {
                return "Success";
            }
            int error = PULLSDK.PullLastError();
            return GetLastErrorMessage((EM_ErrorCode)error);
        }

        public int GetDeviceParam(IntPtr userID, ref byte buffer, int buffersize, string itemvalues)
        {
            return PULLSDK.GetDeviceParam(userID, ref buffer, buffersize, itemvalues);
        }

        public int PullLastError()
        {
            return PULLSDK.PullLastError();
        }

        public int ControlDevice(IntPtr userID, int operationid, int param1, int param2, int param3, int param4, string options)
        {
            return PULLSDK.ControlDevice(userID, operationid, param1, param2, param3, param4, options);
        }

        //Get Device Data
        public int GetDeviceData(IntPtr userID, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options)
        {
            return PULLSDK.GetDeviceData(userID, ref buffer, buffersize, tablename, filename, filter, options);
        }

        public string GetUserInforByPIN(IntPtr userID, string PIN)
        {
            string userCardInfor = "";
            string userDoorInfor = "";
            string fingerInfor = "";
            int ret = 0;
            int Buffersize = 10 * 1024 * 1024;
            byte[] buffer = new byte[Buffersize];
            string tblName = EM_FunctionTableNam.user.ToString();
            string filter = "Pin=" + PIN;
            ret = PULLSDK.GetDeviceData(userID, ref buffer[0], Buffersize, tblName, "*", filter, "");
            if (ret >= 0)
            {
                string result = Encoding.ASCII.GetString(buffer);
                string[] subREs = result.Split("\r\n");
                if (subREs.Length >= 3)
                {
                    userCardInfor = subREs[1];
                }
                else
                {
                    return "";
                }
                //Get DoorInfor
                tblName = EM_FunctionTableNam.userauthorize.ToString();
                Array.Clear(buffer, 0, buffer.Length);
                ret = PULLSDK.GetDeviceData(userID, ref buffer[0], Buffersize, tblName, "*", filter, "");
                if (ret >= 0)
                {
                    result = Encoding.ASCII.GetString(buffer);
                    subREs = result.Split("\r\n");
                    if (subREs.Length >= 3)
                    {
                        userDoorInfor = subREs[1].Split(",")[1] + "," + subREs[1].Split(",")[2];
                    }
                    else
                    {
                        userDoorInfor = "" + "," + "";
                    }
                    //Get Finger Infor
                    Array.Clear(buffer, 0, buffer.Length);
                    tblName = EM_FunctionTableNam.templatev10.ToString();
                    ret = PULLSDK.GetDeviceData(userID, ref buffer[0], Buffersize, tblName, "*", filter, "");
                    if (ret >= 0)
                    {
                        result = Encoding.ASCII.GetString(buffer);
                        subREs = result.Split("\r\n");
                        if (subREs.Length >= 3)
                        {
                            fingerInfor = subREs[1].Split(",")[5];
                        }
                        return userCardInfor + "," + userDoorInfor + "," + fingerInfor;
                    }
                }
            }
            return "";
        }

        //Get CardNumber
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

        //Delete 
        public int DeleteDeviceData(IntPtr userID, string tablename, string data, string options)
        {
            return PULLSDK.DeleteDeviceData(userID, tablename, data, options);
        }
        public int DeleteUserByPIN(IntPtr userID, int PIN)
        {
            int ret = 0;
            string tablename = EM_FunctionTableNam.user.ToString();
            string data = "Pin=" + PIN;
            string options = "";
            return PULLSDK.DeleteDeviceData(userID, tablename, data, options);
        }

        //download user
        public bool downloaduser(IntPtr userid, int pin, string cardno, string fingertemplate,
            string starttime, string endtime, string password, int door)
        {
            int ret = 0;
            string datas = "";
            string options = "";
            datas = "Pin=" + pin + "\tCardNo=" + cardno + "\tPassword=" + password + "\tStartTime=" + starttime + "\tEndTime=" + endtime;
            ret = PULLSDK.SetDeviceData(userid, EM_FunctionTableNam.user.ToString(), datas, options);
            if (ret == 0)
            {
                datas = "Pin=" + pin + "\tFingerID=" + 3 + "\tValid=1\tTemplate=" + fingertemplate;
                ret = PULLSDK.SetDeviceData(userid, EM_FunctionTableNam.templatev10.ToString(), datas, options);
                if (ret == 0)
                {
                    ret = PULLSDK.SetDeviceData(userid, EM_FunctionTableNam.templatev10.ToString(), datas, options);
                    if (ret == 0)
                    {
                        datas = "Pin=" + pin + "\tAuthorizeDoorId=" + 7 + "\tAuthorizeTimezoneId=" + 1;
                        ret = PULLSDK.SetDeviceData(userid, EM_FunctionTableNam.userauthorize.ToString(), datas, options);
                        if (ret == 0)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        public int GetUserPin(IntPtr userID)
        {
            int userCount = PULLSDK.GetDeviceDataCount(userID, "user", "", "");
            if (userCount >= 0)
            {
                return userCount + 1;
            }
            return 0;
        }

        public int SearchDevice(ref byte buffer)
        {
            return PULLSDK.SearchDevice("UDP", "255.255.255.0", ref buffer);
        }
    }
}
