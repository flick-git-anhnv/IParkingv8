using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Objects
{
    public partial class CS2000SettingPage : Form, IControllerSettingPage
    {
        private CS2000 cs2000 = new CS2000();
        private Controller controller = null;

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public CS2000SettingPage()
        {
            InitializeComponent();
        }

        // Line ID
        public int LineID
        {
            get { return cs2000.LineID; }
            set { cs2000.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            get { return cs2000.Address; }
            set { cs2000.Address = value; }
        }

        private int controllerTypeID = 0;
        public int ControllerTypeID
        {
            set { controllerTypeID = value; }
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
        public int DelayTime
        {
            set { cs2000.DelayTime = value; }
        }

        public int DownloadTime
        {
            set { cs2000.DownloadTime = value; }
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
            set{ timezones = value; }
        }

        // all controllerTypes
        private ControllerTypeCollection controllerTypes = new ControllerTypeCollection();
        public ControllerTypeCollection ControllerTypes
        {
            set { controllerTypes = value; }
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
            if (communicationType == 0)
            {
                return cs2000.CommPortOpen(comPort, (short)baudRate);
            }
            else if (communicationType == 1)
            {

            }

            return false;
        }

        public bool DisConnect()
        {
            return cs2000.CommPortClose();
        }

        // Start
        public void PollingStart()
        {
            cs2000.CardEvent += new CardEventHandler(cs2000_CardEvent);
            cs2000.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            cs2000.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            cs2000.PollingStop();
            cs2000.CardEvent -= new CardEventHandler(cs2000_CardEvent);
        }

        private void cs2000_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
            Timezone timezone = timezones.GetTimezoneByID(timezoneID);
            if(timezone != null)
                return cs2000.DownloadCard(employee.CardNumber, timezone, pin_HI, pin_LO);
            return false;
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
            return cs2000.DeleteCard(employee.CardNumber, pin_HI, pin_LO);
        }

        public bool Unlock(int delay)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
            return cs2000.OpenDoor(pin_HI, pin_LO);
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
            return cs2000.OpenDoor(pin_HI, pin_LO);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        private void btnDateTimeUpLoad_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);

            string readerModel = "";
            string readerDate = "";
            string readerTime = "";
            int numEvent = 0;
            int readerRelay = 0;
            int doorRelay = 0;

            cs2000.UploadSettings(cs2000.Address, ref readerModel, ref readerDate, ref readerTime, ref numEvent, ref readerRelay, ref doorRelay, pin_HI, pin_LO);
            
            if (readerModel != "")
            {
                txtReaderModel.Text = readerModel;
                string datetime = readerDate + " " + readerTime;
                txtDate.Text = Convert.ToDateTime(datetime).ToString("dd/MM/yyyy");
                txtTime.Text = Convert.ToDateTime(datetime).ToString("HH:mm:ss");
                txtReaderRelay.Text = readerRelay.ToString();
                txtDoorRelay.Text = doorRelay.ToString();
                txtNumEvent.Text = numEvent.ToString();
            }
            else
                MessageBox.Show("Không thể kết nối tới Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);

            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            if (cs2000.DownloadDateTime(datetime, pin_HI, pin_LO))
            {
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CS2000SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void CS2000SettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownloadRelayTime_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);

            int readerRelay = (int)numReaderRelay.Value;
            int doorRelay = (int)numDoorRelay.Value;
            if (cs2000.DownloadRelayTime(readerRelay, doorRelay, pin_HI, pin_LO))
            {
                MessageBox.Show("Đã đặt các thông số lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt các thông số lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);
            cs2000.OpenDoor(pin_HI, pin_LO);
        }

        private void btnDeleteAllCard_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);

            if (cs2000.DeleteAllCard(pin_HI, pin_LO))
                MessageBox.Show("Đã xóa hết tất cả các thẻ trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi xóa hết các thẻ trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDeleteAllEvent_Click(object sender, EventArgs e)
        {
            controller = controllers.GetControllerByAddress(LineID, Address);
            byte pin_HI = Convert.ToByte(controller.Description.Substring(0, 2), 16);
            byte pin_LO = Convert.ToByte(controller.Description.Substring(2, 2), 16);

            if (cs2000.DeleteAllEvent(cs2000.Address, pin_HI, pin_LO))
                MessageBox.Show("Đã xóa hết tất cả các sự kiện trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi xóa hết các sự kiện trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void DelAllEvent()
        {
        }
        public string GetInputState()
        {
            return "";
        }
    }
}