using Futech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace Futech.Objects.SupremaCS40
{
    public class SupremaCS40
    {

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;

        protected IntPtr sdkContext = IntPtr.Zero;

        //private API.OnDeviceFound cbOnDeviceFound = null;
        //private API.OnDeviceAccepted cbOnDeviceAccepted = null;
        //private API.OnDeviceConnected cbOnDeviceConnected = null;
        //private API.OnDeviceDisconnected cbOnDeviceDisconnected = null;

        private API.OnLogReceived cbOnLogReceived = null;

        private UInt32 deviceID = 0;
        //CTO
        public SupremaCS40()
        {
            sdkContext = API.BS2_AllocateContext();
        }

        //}
        // Line ID
        private int lineID = 0;
        public int LineID
        {
            set { lineID = value; }
        }

        // delay time to receive data from reader
        private int delaytime = 300;
        public int DelayTime
        {
            get { return delaytime; }
            set { delaytime = value; }
        }

        // Controller Address
        private byte address = 0x00;
        public int Address
        {
            set { address = Convert.ToByte(ByteUntils.DecimalToBase(value, 16), 16); }
        }

        private bool isconnect = false;
        public bool IsConnect
        {
            get { return isconnect; }
            set { isconnect = value; }
        }

        private string _ip = "";
        private int _port = 0;
        // all controller in line
        private ControllerCollection controllers = null;
        public ControllerCollection Controllers
        {
            set { controllers = value; }
        }

        public bool Connect(string ip, int port)
        {
            try
            {
                BS2ErrorCode resultInit = (BS2ErrorCode)API.BS2_Initialize(sdkContext);

                if (resultInit != BS2ErrorCode.BS_SDK_SUCCESS) return false;

                //cbOnDeviceFound = new API.OnDeviceFound(DeviceFound);
                //cbOnDeviceAccepted = new API.OnDeviceAccepted(DeviceAccepted);
                //cbOnDeviceConnected = new API.OnDeviceConnected(DeviceConnected);
                //cbOnDeviceDisconnected = new API.OnDeviceDisconnected(DeviceDisconnected);

                //BS2ErrorCode resultInit_Event = (BS2ErrorCode)API.BS2_SetDeviceEventListener(sdkContext,
                //                                cbOnDeviceFound,
                //                                cbOnDeviceAccepted,
                //                                cbOnDeviceConnected,
                //                                cbOnDeviceDisconnected);

                //if (resultInit_Event != BS2ErrorCode.BS_SDK_SUCCESS) return false;

                IntPtr ptrIPAddr = Marshal.StringToHGlobalAnsi(ip);

                BS2ErrorCode resultConnect = (BS2ErrorCode)API.BS2_ConnectDeviceViaIP(sdkContext, ptrIPAddr, (ushort)port, out deviceID);

                if (resultConnect != BS2ErrorCode.BS_SDK_SUCCESS) return false;

                Marshal.FreeHGlobal(ptrIPAddr);

                cbOnLogReceived = new API.OnLogReceived(RealtimeLogReceived);

                BS2ErrorCode resultLog = (BS2ErrorCode)API.BS2_StartMonitoringLog(sdkContext, deviceID, cbOnLogReceived);

                if (resultLog != BS2ErrorCode.BS_SDK_SUCCESS) return false;

                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show("Connect: "+ex.Message);
                return false;
            }
        }

        //void DeviceFound(UInt32 deviceID)
        //{

        //}

        //void DeviceAccepted(UInt32 deviceID)
        //{

        //}

        //void DeviceConnected(UInt32 deviceID)
        //{

        //}

        //void DeviceDisconnected(UInt32 deviceID)
        //{

        //}

        void RealtimeLogReceived(UInt32 deviceID, IntPtr log)
        {
            if (log != IntPtr.Zero)
            {
                BS2Event eventLog = (BS2Event)Marshal.PtrToStructure(log, typeof(BS2Event));
                //Console.WriteLine(Util.GetLogMsg(eventLog));

                if (eventLog.code != 4354) return;

                var cardId = Encoding.ASCII.GetString(eventLog.userID).TrimEnd('\0');
                long _cardId = 0;
                long.TryParse(cardId, out _cardId);
                var cardNumber = _cardId.ToString("X");
                while((cardNumber.Length % 2) != 0)
                {
                    cardNumber = "0" + cardNumber;
                }
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(eventLog.dateTime);

                foreach (Futech.Objects.Controller controller in controllers)
                {
                    CardEventArgs ex = new CardEventArgs();

                    ex.Date = dateTime.ToString("yyyy/MM/dd");
                    ex.Time = dateTime.ToString("HH:mm:ss");

                    ex.LineID = controller.LineID;
                    ex.LineCode = controller.LineCode;
                    ex.ControllerAddress = controller.Address;
                    ex.CardNumber = cardNumber;

                    CardEvent?.Invoke(this, ex);
                    break;
                }
            }
        }

        public bool DisConnect()
        {
            API.BS2_StopMonitoringLog(sdkContext, deviceID);
            API.BS2_ReleaseContext(sdkContext);
            sdkContext = IntPtr.Zero;
            return false;
        }

        public bool Reconnect()
        {
            return Connect(_ip, _port);
            //return false;
        }

        public bool Status()
        {
            try
            {
                //return AcsTcp.Active();
                Ping ping = new Ping();
                PingReply reply = null;
                reply = ping.Send(_ip, 200);
                if (reply != null && reply.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        //OpenDoor
        public bool OpenDoor(byte door)
        {
            List<UInt32> doorIDList = new List<UInt32>() { door };

            IntPtr doorIDObj = Marshal.AllocHGlobal(4 * doorIDList.Count);
            IntPtr curDoorIDObj = doorIDObj;
            foreach (UInt32 item in doorIDList)
            {
                Marshal.WriteInt32(curDoorIDObj, (Int32)item);
                curDoorIDObj = (IntPtr)((long)curDoorIDObj + 4);
            }
            byte doorFlag = (byte)BS2DoorFlagEnum.NONE;

            BS2ErrorCode result = (BS2ErrorCode)API.BS2_UnlockDoor(sdkContext, deviceID, doorFlag, doorIDObj, (UInt32)doorIDList.Count);

            Marshal.FreeHGlobal(doorIDObj);

            if (result != BS2ErrorCode.BS_SDK_SUCCESS) return false;
            else return true;
        }

        //OpenDoor Alarm
        public void OpenDoorAlarm()
        {
            //return AcsTcp.OpenDoorA(1);
        }

        public void CloseDoorAlarm()
        {
            //return AcsTcp.CloseDoor(1);
        }
    }
}
