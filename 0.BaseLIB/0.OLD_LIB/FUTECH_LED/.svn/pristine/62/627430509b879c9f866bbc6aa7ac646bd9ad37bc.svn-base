using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Objects
{
    public partial class DS3000SettingPage : Form, IControllerSettingPage
    {
        private DS3000 ds3000 = new DS3000();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public DS3000SettingPage()
        {
            InitializeComponent();
        }

        // Line ID
        public int LineID
        {
            set { ds3000.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { ds3000.Address = value; }
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
            set { ds3000.DelayTime = value; }
        }

        public int DownloadTime
        {
            set { ds3000.DownloadTime = value; }
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
                return ds3000.CommPortOpen(comPort, (short)baudRate);
            }
            else if (communicationType == 1)
            {

            }

            return false;
        }

        public bool DisConnect()
        {
            return ds3000.CommPortClose();
        }

        // Start
        public void PollingStart()
        {
            ds3000.CardEvent += new CardEventHandler(ds3000_CardEvent);
            ds3000.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            ds3000.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            ds3000.PollingStop();
            ds3000.CardEvent -= new CardEventHandler(ds3000_CardEvent);
        }

        private void ds3000_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            Timezone timezone = timezones.GetTimezoneByID(timezoneID);
            if(timezone != null)
                return ds3000.DownloadCard(employee.CardNumber, memoryID, timezone);
            return false;
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            return ds3000.DeleteCard(memoryID);
        }

        public bool Unlock(int delay)
        {
            return ds3000.OpenDoor();
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return ds3000.OpenDoor();
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        private void btnDateTimeUpLoad_Click(object sender, EventArgs e)
        {
            string readerModel = "";
            string readerDate = "";
            string readerTime = "";
            int numEvent = 0;
            int readerRelay = 0;
            int doorRelay = 0;

            ds3000.UploadSettings(ds3000.Address, ref readerModel, ref readerDate, ref readerTime, ref numEvent, ref readerRelay, ref doorRelay);
            
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
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            int readerRelay = (int)numReaderRelay.Value;
            int doorRelay = (int)numDoorRelay.Value;
            if (ds3000.DownloadSettings(datetime, readerRelay, doorRelay))
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void DS3000SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void DS3000SettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestCE_Click(object sender, EventArgs e)
        {
            string CEModel = "";
            if (!ds3000.TestCE(ref CEModel))
                MessageBox.Show("Đã xảy ra lỗi khi kết nối đến Central Unit!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            lblCE.Text = CEModel;
        }

        private void btnDeleteAllCard_Click(object sender, EventArgs e)
        {
            if(ds3000.DeleteAllCard())
                MessageBox.Show("Đã xóa hết tất cả các thẻ trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi xóa hết các thẻ trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDeleteAllEvent_Click(object sender, EventArgs e)
        {
            if (ds3000.DeleteAllEvent(ds3000.Address))
                MessageBox.Show("Đã xóa hết tất cả các sự kiện trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi xóa hết các sự kiện trên đầu đọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            ds3000.OpenDoor();
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