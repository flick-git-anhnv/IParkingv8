using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Controller
    {
        public int nextPackage = 0;
        public bool isFirstPackage = false;
        public int eventCount = 0;

        // Constructor
        public Controller()
        {
 
        }

        // ID property
        private int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        // FID property
        private int fid = 0;
        public int FID
        {
            get { return fid; }
            set { fid = value; }
        }

        // Code property
        private string code = "";
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        // Name property
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // LineID property
        private int lineID = 0;
        public int LineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        // LineCode property
        private string linecode = "";
        public string LineCode
        {
            get { return linecode; }
            set { linecode = value; }
        }

        // Address property
        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }

        // Controller Type
        private int controllerTypeID = 0;
        public int ControllerTypeID
        {
            get { return controllerTypeID; }
            set { controllerTypeID = value; }
        }

        // Reader1 Type: 0 = vào, 1 = ra
        private int reader1Type = 0;
        public int Reader1Type
        {
            get { return reader1Type; }
            set { reader1Type = value; }
        }

        // Reader2 Type
        private int reader2Type = 1;
        public int Reader2Type
        {
            get { return reader2Type; }
            set { reader2Type = value; }
        }

        // TimeAttendance property
        private bool timeAttendance = true;
        public bool TimeAttendance
        {
            get { return timeAttendance; }
            set { timeAttendance = value; }
        }

        // Description property
        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // Value
        private int value1 = 0;
        private byte value11 = 0x00, value12 = 0x00, value13 = 0x00, value14 = 0x00;
        public int Value1
        {
            get { return value1; }
            set
            {
                value1 = value;
                byte[] bytes = BitConverter.GetBytes(value);
                value11 = bytes[0];
                value12 = bytes[1];
                value13 = bytes[2];
                value14 = bytes[3];
            }
        }

        // Gia tri moi
        private int newvalue1 = 0;
        public int NewValue1
        {
            get { return newvalue1; }
            set { newvalue1 = value; }
        }

        // Lan dau tien ket noi den thiet bi
        private bool isFirstLogin = true;
        public bool IsFirstLogin
        {
            get { return isFirstLogin; }
            set { isFirstLogin = value; }
        }

        private bool isconnect = false;
        public bool IsConnect
        {
            get { return isconnect; }
            set { isconnect = value; }
        }

        private bool isinactive = false;
        public bool IsInActive
        {
            get { return isinactive; }
            set { isinactive = value; }
        }

        private int numevent = 1;
        public int NumEvent
        {
            get { return numevent; }
            set { numevent = value; }
        }

        private string areacode = "";
        public string AreaCode
        {
            get { return areacode; }
            set { areacode = value; }
        }

        private bool ispc = false;
        public bool IsPC
        {
            get { return ispc; }
            set { ispc = value; }
        }

        private string systemcode = "";
        public string SystemCode
        {
            get { return systemcode; }
            set { systemcode = value; }
        }

        private string keyA = "";
        public string KeyA
        {
            get { return keyA; }
            set { keyA = value; }
        }

        private string keyB = "";
        public string KeyB
        {
            get { return keyB; }
            set { keyB = value; }
        }


        // Gia tri thiet lap 1
        private int refValue1 = 0;
        public int RefValue1
        {
            get { return refValue1; }
            set { refValue1 = value; }
        }

        // Gia tri thiet lap 2
        private int refValue2 = 0;
        public int RefValue2
        {
            get { return refValue2; }
            set { refValue2 = value; }
        }

        private int refValue3 = 0;
        public int RefValue3
        {
            get { return refValue3; }
            set { refValue3 = value; }
        }

        // all blackLists
        private BlackListCollection blackLists = new BlackListCollection();
        public BlackListCollection BlackLists
        {
            get { return blackLists; }
            set { blackLists = value; }
        }

        private int plan = 0;
        public int Plan
        {
            get { return plan; }
            set { plan = value; }
        }

        private int cycletime = 0;
        public int CycleTime
        {
            get { return cycletime; }
            set { cycletime = value; }
        }

        private int totalplan = 0;
        public int TotalPlan
        {
            get { return totalplan; }
            set { totalplan = value; }
        }

        private int totalgoal = 0;
        public int TotalGoal
        {
            get { return totalgoal; }
            set { totalgoal = value; }
        }

        private int totalresult = 0;
        public int TotalResult
        {
            get { return totalresult; }
            set { totalresult = value; }
        }

        private bool ispause = false;
        public bool IsPause
        {
            get { return ispause;}
            set { ispause = value; }
        }

        private bool isstop = false;
        public bool IsStop
        {
            get { return isstop; }
            set { isstop = value; }
        }
    }
}
