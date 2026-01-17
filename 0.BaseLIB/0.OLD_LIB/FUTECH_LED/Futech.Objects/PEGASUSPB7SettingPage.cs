using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Futech.Tools;

using System.Net.NetworkInformation;

namespace Futech.Objects
{
    public partial class PEGASUSPB7SettingPage : Form, IControllerSettingPage
    {
        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        private string comPort = "";
        private int baudRate = 0;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;

        private string[] eventStatus = new string[] { "Access Granted", "UnregisterID", "TimeSchedule Error", "AntiPassBack Error(IN)", "AntiPassBack Error(OUT)", "Password Error", "Access Door Error", "Fingger Error", "Duress Mode", "Door Open", "Door Close" };

        private ControllerType controllerType = null;

        public PEGASUSPB7SettingPage()
        {
            InitializeComponent();
        }

        // Line ID
        private int lineID = 0;
        public int LineID
        {
            set { lineID = value; }
        }

        // Controller Address property
        private int address = 1;
        public int Address
        {
            set { address = value; }
        }

        private int controllerTypeID = 26;
        private string devideModel = "PB7";
        public int ControllerTypeID
        {
            set
            {
                controllerTypeID = value;
            }
        }

        private int communicationType = 0;
        public int CommunicationType
        {
            set { communicationType = value; }
        }

        private bool isconnect = false;
        public bool IsConnect
        {
            get { return isconnect; }
            set { isconnect = value; }
        }

        // Delay time property
        private int delayTime = 300;
        public int DelayTime
        {
            set { delayTime = value; }
        }

        private int downloadTime = 300;
        public int DownloadTime
        {
            set { downloadTime = value; }
        }

        // all controllers in line
        private ControllerCollection controllers = new ControllerCollection();
        public ControllerCollection Controllers
        {
            set { controllers = value; }
        }

        // all timezones
        private TimezoneCollection timezones = new TimezoneCollection();
        public TimezoneCollection Timezones
        {
            set
            {
                timezones = value;
                foreach (Timezone timezone in value)
                {
                    cbxTimezone.Items.Add(timezone.Name);
                }
                if (timezones.Count > 0)
                    cbxTimezone.SelectedIndex = 0;
            }
        }

        // all controllerTypes
        private ControllerTypeCollection controllerTypes = new ControllerTypeCollection();
        public ControllerTypeCollection ControllerTypes
        {
            set
            {
                controllerTypes = value;
                controllerType = controllerTypes.GetControllerTypeByID(controllerTypeID);
                if (controllerType != null)
                {
                    devideModel = controllerType.Name;
                }
            }
        }

        private int licenseKey = 1263;
        public int LicenseKey
        {
            get { return licenseKey; }
            set { licenseKey = value; }
        }

        // all blackLists
        private BlackListCollection blackLists = new BlackListCollection();
        public BlackListCollection BlackLists
        {
            get { return blackLists; }
            set { blackLists = value; }
        }

        public bool Connect(string comPort, int baudRate)
        {
            if (isconnect)
                DisConnect();
            isconnect = false;
            try
            {
                this.comPort = comPort;
                this.baudRate = baudRate;
                if (controllers.Count > 0)
                {
                    address = controllers[0].Address;
                    if (!int.TryParse(controllers[0].Description, out licenseKey))
                        licenseKey = 1263;

                    if (communicationType == 0)
                    {
                        axFKAttend.DisConnect();
                        int comPortID = Convert.ToInt32(comPort.Substring(3));
                        if (axFKAttend.ConnectComm(address, comPortID, baudRate, "", 0, licenseKey) == 1)
                        {
                            isconnect = true;
                            return true;
                        }
                    }
                    else if (communicationType == 1)
                    {
                        Ping pingSender = new Ping();
                        PingReply reply = null;
                        reply = pingSender.Send(comPort, 1000);
                        if (reply != null && reply.Status == IPStatus.Success)
                        {
                            if (axFKAttend.ConnectNet(address, reply.Address.ToString(), baudRate, 15000, 0, 0, licenseKey) == 1)
                            {
                                isconnect = true;
                                return true;
                            }
                        }
                    }
                    else if (communicationType == 2)
                    {
                        if (axFKAttend.ConnectUSB(address, licenseKey) == 1)
                        {
                            isconnect = true;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public bool DisConnect()
        {
            try
            {
                //if (isconnect)
                //{
                isconnect = false;
                axFKAttend.DisConnect();
                return true;
                //}
            }
            catch
            {

            }
            return false;
        }

        // Start
        public void PollingStart()
        {
            if (thread == null)
            {
                // create events
                stopEvent = new ManualResetEvent(false);

                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
            }
        }

        // is Running
        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, so free resources
                    Free();
                }
                return false;
            }
        }

        // Signal thread to stop work
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }

        // Wait for thread stop
        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();

                Free();
            }
        }

        // Free resources
        private void Free()
        {
            thread = null;

            // release events
            stopEvent.Close();
            stopEvent = null;
        }

        // Stop
        public void PollingStop()
        {
            if (this.Running)
            {
                thread.Abort();
                WaitForStop();
            }
        }

        // Worker thread
        int timeout = 0;
        DateTime dtnow = DateTime.Now;
        bool isFirstTime = false;
        public void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    if (isconnect)
                    {
                        foreach (Futech.Objects.Controller controller in controllers)
                        {
                            Thread.Sleep(downloadTime * 1000);
                            dtnow = DateTime.Now;
                            // Chi ket noi den dau doc nay va lay du lieu khi thoi gian nam khoang tu 6h sang den 8h toi
                            if (dtnow.Hour <= 19 && dtnow.Hour >= 6)
                            {
                                isFirstTime = true;
                                if (axFKAttend.LoadGeneralLogData(1) == 1)
                                {
                                    int enrollNo = 0;
                                    DateTime datetime = DateTime.MinValue;
                                    int verifyMode = 0;
                                    int io = 0;
                                    while (axFKAttend.GetGeneralLogData(ref enrollNo, ref verifyMode, ref io, ref datetime) == 1)
                                    {
                                        //timeout = 0;
                                        CardEventArgs e = new CardEventArgs();
                                        e.LineID = controller.LineID;
                                        e.ControllerAddress = controller.Address;
                                        e.CardNumber = enrollNo.ToString();
                                        e.Date = datetime.ToString("yyyy/MM/dd");
                                        e.Time = datetime.ToString("HH:mm:ss");
                                        e.ReaderIndex = 1;
                                        e.EventStatus = "Access Granted";
                                        if (verifyMode == 10)
                                            e.EventStatus = "Door Close";
                                        else if (verifyMode == 11)
                                            e.EventStatus = "Hand Open";
                                        else if (verifyMode == 12)
                                            e.EventStatus = "Open by PC";
                                        else if (verifyMode == 13)
                                            e.EventStatus = "Close by PC";
                                        else if (verifyMode == 14)
                                            e.EventStatus = "Iregal Open";
                                        else if (verifyMode == 15)
                                            e.EventStatus = "Iregal Close";
                                        else if (verifyMode == 16)
                                            e.EventStatus = "Cover Open";
                                        else if (verifyMode == 15)
                                            e.EventStatus = "Cover Close";
                                        else if (verifyMode == 15)
                                            e.EventStatus = "Door Open";
                                        else if (verifyMode == 15)
                                            e.EventStatus = "Door Open as threat";
                                        CardEvent(this, e);
                                    }

                                    Thread.Sleep(300);
                                    //if (isconnect)
                                    //{
                                    //    axFKAttend.EmptyGeneralLogData();
                                    //    Thread.Sleep(300);
                                    //}
                                }
                                else
                                {
                                    timeout = timeout + 1;
                                    if (timeout > 5)
                                    {
                                        isconnect = false;
                                        timeout = 0;
                                    }
                                }
                            }
                            else
                            {
                                // Tu dong tat tu may tinh
                                if (isFirstTime)
                                {
                                    if (axFKAttend.PowerOffDevice() == 1)
                                        isFirstTime = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(downloadTime * 1000);
                        //timeout = timeout + 1;
                        //if (timeout > 10)
                        //{
                        //    // Reconnect to readers
                        //    timeout = 0;
                        Connect(this.comPort, this.baudRate);
                        //}
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        // Download Card
        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            DownloadFinger(employee);
            DownloadCard(employee);

            return SetUserInfo(employee);
        }

        // Delete Card
        public bool DeleteCard(Employee employee, int memoryID)
        {
            DeleteUserData(employee);
            DeleteFinger(employee);
            DeleteCard(employee);

            return DeletePassword(employee);
        }

        private bool DownloadFinger(Employee employee)
        {
            bool complete = false;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (employee.Fingers1 != "" || employee.Fingers10 != "")
                        if (axFKAttend.PutEnrollDataWithString(enrollNo, 0, 0, employee.Fingers1 + employee.Fingers10) == 1)
                        {
                            axFKAttend.SaveEnrollData();
                            axFKAttend.EnableDevice(1);
                            complete = true;
                        }
                    if (employee.Fingers2 != "" || employee.Fingers9 != "")
                        if (axFKAttend.PutEnrollDataWithString(enrollNo, 1, 0, employee.Fingers2 + employee.Fingers9) == 1)
                        {
                            axFKAttend.SaveEnrollData();
                            axFKAttend.EnableDevice(1);
                            complete = true;
                        }
                    if (employee.Fingers3 != "" || employee.Fingers8 != "")
                        if (axFKAttend.PutEnrollDataWithString(enrollNo, 2, 0, employee.Fingers3 + employee.Fingers8) == 1)
                        {
                            axFKAttend.SaveEnrollData();
                            axFKAttend.EnableDevice(1);
                            complete = true;
                        }
                    if (employee.Fingers4 != "" || employee.Fingers7 != "")
                        if (axFKAttend.PutEnrollDataWithString(enrollNo, 3, 0, employee.Fingers4 + employee.Fingers7) == 1)
                        {
                            axFKAttend.SaveEnrollData();
                            axFKAttend.EnableDevice(1);
                            complete = true;
                        }
                    if (employee.Fingers5 != "" || employee.Fingers6 != "")
                        if (axFKAttend.PutEnrollDataWithString(enrollNo, 4, 0, employee.Fingers5 + employee.Fingers6) == 1)
                        {
                            axFKAttend.SaveEnrollData();
                            axFKAttend.EnableDevice(1);
                            complete = true;
                        }
                    //if (employee.Fingers6 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 5, 0, employee.Fingers6) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                    //if (employee.Fingers7 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 6, 0, employee.Fingers7) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                    //if (employee.Fingers8 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 7, 0, employee.Fingers8) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                    //if (employee.Fingers9 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 8, 0, employee.Fingers9) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                    //if (employee.Fingers10 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 9, 0, employee.Fingers10) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                    axFKAttend.EnableDevice(1);
                }
            }
            return complete;
        }

        private bool DeleteFinger(Employee employee)
        {
            bool complete = false;
            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (axFKAttend.DeleteEnrollData(enrollNo, 0) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 1) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 2) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 3) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 4) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 5) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 6) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 7) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 8) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    if (axFKAttend.DeleteEnrollData(enrollNo, 9) == 1)
                    {
                        complete = true;
                        axFKAttend.EnableDevice(1);
                    }
                    axFKAttend.EnableDevice(1);
                }
            }
            return complete;
        }

        // Get Finger
        public string GetFinger(string cardNumber, int fingerID)
        {
            string template = "";
            int privilege = 0;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(cardNumber, out enrollNo))
                {
                    if (axFKAttend.GetEnrollDataWithString(enrollNo, fingerID - 1, ref privilege, ref template) == 1)
                    {
                        return template;
                    }
                }
            }
            return template;
        }

        private bool DownloadCard(Employee employee)
        {
            bool complete = false;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (employee.CardNumber1 != "")
                        if(axFKAttend.PutEnrollDataWithString(enrollNo, 11, 0, employee.CardNumber1) == 1)
                    {
                        axFKAttend.SaveEnrollData();
                        axFKAttend.EnableDevice(1);
                        complete = true;
                    }

                    //if (employee.CardNumber2 != "" && axFKAttend.PutEnrollDataWithString(enrollNo, 11, 0, employee.CardNumber2) == 1)
                    //{
                    //    axFKAttend.SaveEnrollData();
                    //    complete = true;
                    //}
                }
            }

            return complete;
        }

        private bool DownloadPassword(Employee employee)
        {
            bool complete = false;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (employee.Passwords != "")
                        if(axFKAttend.PutEnrollDataWithString(enrollNo, 10, 0, employee.Passwords) == 1)
                    {
                        axFKAttend.SaveEnrollData();
                        axFKAttend.EnableDevice(1);
                        complete = true;
                    }
                }
            }

            return complete;
        }

        private bool DeleteCard(Employee employee)
        {
            bool complete = false;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (employee.Passwords != "")
                        if(axFKAttend.DeleteEnrollData(enrollNo, 11) == 1)
                    {
                        axFKAttend.SaveEnrollData();
                        axFKAttend.EnableDevice(1);
                        complete = true;
                    }
                }
            }

            return complete;
        }

        private bool DeletePassword(Employee employee)
        {
            bool complete = false;

            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (employee.Passwords != "")
                        if(axFKAttend.DeleteEnrollData(enrollNo, 10) == 1)
                    {
                        axFKAttend.SaveEnrollData();
                        axFKAttend.EnableDevice(1);
                        complete = true;
                    }
                }
            }

            return complete;
        }

        // Unlock
        public bool Unlock(int delay)
        {
            if (isconnect && axFKAttend.SetDoorStatus(1) == 1)
            {
                Thread.Sleep(5000);
                axFKAttend.SetDoorStatus(2);
                return true;
            }
            return false;
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            string serialNumber = "";
            if (axFKAttend.GetProductData(1, ref serialNumber) == 1)
                return true;
            isconnect = false;
            return false;
        }

        private bool SetUserInfo(Employee employee)
        {
            if (isconnect)
            {
                int enrollNo = 0;
                string employeeName = employee.Code;
                if (employeeName.Length > 9)
                    employeeName = employeeName.Substring(0, 9);
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (axFKAttend.SetUserName(enrollNo, employeeName) == 1)
                    {
                        axFKAttend.EnableDevice(1);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DeleteUserData(Employee employee)
        {
            //if (isconnect)
            //{
            //    if (controllerTypeID >= 19)
            //    {
            //        if (axBioBridgeSDK.SSR_DeleteUserData(employee.CardNumber) == 0)
            //        {
            //            return true;
            //        }
            //    }
            //    else
            //    {
            //        int enrollNo = 0;
            //        if (int.TryParse(employee.CardNumber, out enrollNo))
            //        {
            //            if (axBioBridgeSDK.DeleteUserData(enrollNo) == 0)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}
            return false;
        }

        // Set user Verify Type, avaibability: M2, R2
        int[] verType = new int[15] { 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142 };
        private bool SetUserVerType(Employee employee)
        {
            //if (isconnect)
            //{
            //    int enrollNo = 0;
            //    if (int.TryParse(employee.CardNumber, out enrollNo))
            //    {
            //        if (axBioBridgeSDK.SetUserVerType(enrollNo, verType[employee.VerifyTypeID]) == 0)
            //        {
            //            return true;
            //        }
            //    }

            //}
            return false;
        }

        private bool SetUserGroup(Employee employee, int timezoneID)
        {
            //if (isconnect)
            //{
            //    int enrollNo = 0;
            //    if (int.TryParse(employee.CardNumber, out enrollNo))
            //    {
            //        if (axBioBridgeSDK.SetUserGroup(enrollNo, timezoneID) == 0)
            //        {
            //            return true;
            //        }
            //    }

            //}
            return false;
        }

        private bool SetUserTimezone(Employee employee, int timezoneID)
        {
            if (isconnect)
            {
                int enrollNo = 0;
                if (int.TryParse(employee.CardNumber, out enrollNo))
                {
                    if (axFKAttend.SetUserPassTimeWithString(enrollNo, timezoneID, timezoneID.ToString("00") + " 00 00") == 1)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private void btnDateTimeUpLoad_Click(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.MinValue;

            if (isconnect && axFKAttend.GetDeviceTime(ref datetime) == 1)
            {
                txtDate.Text = datetime.ToString("dd/MM/yyyy");
                txtTime.Text = datetime.ToString("HH:mm:ss");
            }
            else
                MessageBox.Show("Xảy ra lỗi khi nhận ngày giờ từ Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToString("yyyy/MM/dd") + " " + dtpTime.Value.ToString("HH:mm:ss"));
            if (isconnect && axFKAttend.SetDeviceTime(datetime) == 1)
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClearAllData_Click(object sender, EventArgs e)
        {
            if (isconnect && axFKAttend.ClearKeeperData() == 1)
                MessageBox.Show("Đã khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClearAdministrator_Click(object sender, EventArgs e)
        {
            if (isconnect && axFKAttend.EmptyGeneralLogData() == 1)
                MessageBox.Show("Đã xóa tất cả các sự kiện trên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi xóa tất cả các sự kiện trên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cbxTimezone_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset
            this.tabPage2.Controls.Remove(graphControl);
            graphControl = new GraphControl();
            graphControl.Size = new Size(356, 244);
            graphControl.Location = new Point(3, 46);
            this.tabPage2.Controls.Add(graphControl);
            //
            Timezone timezone = timezones.GetTimezoneByName(cbxTimezone.SelectedItem.ToString());
            if (timezone != null)
            {
                string[] mon = timezone.Mon.Split('-');
                string[] tue = timezone.Tue.Split('-');
                string[] wed = timezone.Wed.Split('-');
                string[] thu = timezone.Thu.Split('-');
                string[] fri = timezone.Fri.Split('-');
                string[] sat = timezone.Sat.Split('-');
                string[] sun = timezone.Sun.Split('-');
                graphControl.AddLine(DateUI.GetTimeNumber(mon[0]), 1, DateUI.GetTimeNumber(mon[1]), 1);
                graphControl.AddLine(DateUI.GetTimeNumber(tue[0]), 2, DateUI.GetTimeNumber(tue[1]), 2);
                graphControl.AddLine(DateUI.GetTimeNumber(wed[0]), 3, DateUI.GetTimeNumber(wed[1]), 3);
                graphControl.AddLine(DateUI.GetTimeNumber(thu[0]), 4, DateUI.GetTimeNumber(thu[1]), 4);
                graphControl.AddLine(DateUI.GetTimeNumber(fri[0]), 5, DateUI.GetTimeNumber(fri[1]), 5);
                graphControl.AddLine(DateUI.GetTimeNumber(sat[0]), 6, DateUI.GetTimeNumber(sat[1]), 6);
                graphControl.AddLine(DateUI.GetTimeNumber(sun[0]), 7, DateUI.GetTimeNumber(sun[1]), 7);
                //
                graphControl.Invalidate();
            }
        }

        private void btnTimezoneDownload_Click(object sender, EventArgs e)
        {
            Timezone timezone = timezones.GetTimezoneByName(cbxTimezone.SelectedItem.ToString());
            if (timezone != null)
            {
                string timezoneString = "";
                timezoneString += TimezoneInDay(timezone.Sun);
                timezoneString += " " + TimezoneInDay(timezone.Mon);
                timezoneString += " " + TimezoneInDay(timezone.Tue);
                timezoneString += " " + TimezoneInDay(timezone.Wed);
                timezoneString += " " + TimezoneInDay(timezone.Thu);
                timezoneString += " " + TimezoneInDay(timezone.Fri);
                timezoneString += " " + TimezoneInDay(timezone.Sat);
                
                // Set timezone info
                if (isconnect && axFKAttend.SetPassTimeWithString(timezone.ID + 1, timezoneString) == 1)
                {
                    // Set Group Timezone
                    int timezoneID = timezone.ID + 1;
                    string groupTimezone = timezoneID.ToString("00") + " 00 00";
                    axFKAttend.SetGroupPassTimeWithString(timezoneID, groupTimezone);

                    string unlockGroup = timezoneID.ToString("00") + " 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
                    axFKAttend.SetGroupMatchWithString(unlockGroup);

                    MessageBox.Show("Đã cập nhật Timezone ' " + cbxTimezone.Text + " ' lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi giao tiếp với Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private string TimezoneInDay(string timezoneInDay)
        {
            string[] temp = timezoneInDay.Split('-');
            string[] t1 = temp[0].Split(':');
            string[] t2 = temp[1].Split(':');
            string tmp = ByteUntils.DecimalToBase(int.Parse(t1[0]), 16) + " " + ByteUntils.DecimalToBase(int.Parse(t1[1]), 16) + " " + ByteUntils.DecimalToBase(int.Parse(t2[0]), 16) + " " + ByteUntils.DecimalToBase(int.Parse(t2[1]), 16);
            return tmp;
        }

        private void PEGASUSFINGERSettingPage_Load(object sender, EventArgs e)
        {
            cbxDeviceStatus.SelectedIndex = 0;
            PollingStop();
        }

        private void PEGASUSFINGERSettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetDeviceStatus_Click(object sender, EventArgs e)
        {
            int dwValue = 0;
            if (isconnect && axFKAttend.GetDeviceStatus(cbxDeviceStatus.SelectedIndex + 1, ref dwValue) == 1)
            {
                txtValue.Text = dwValue.ToString();
                //MessageBox.Show("Đã nhận được thông số từ Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                txtValue.Text = "";
                MessageBox.Show("Đã xảy ra lỗi khi nhận các thông số từ Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void DelAllEvent()
        {

        }
    }
}