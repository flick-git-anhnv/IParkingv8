using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Objects
{
    public partial class RCP4000SettingPage : Form, IControllerSettingPage
    {
        private RCP4000 rcp4000 = new RCP4000();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public RCP4000SettingPage()
        {
            InitializeComponent();
        }

        // Line ID
        public int LineID
        {
            get { return rcp4000.LineID; }
            set { rcp4000.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            get { return rcp4000.Address; }
            set { rcp4000.Address = value; }
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
            set { rcp4000.DelayTime = value; }
        }

        public int DownloadTime
        {
            set { rcp4000.DownloadTime = value; }
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
                return rcp4000.CommPortOpen(comPort, (short)baudRate);
            }
            else if (communicationType == 1)
            {

            }

            return false;
        }

        public bool DisConnect()
        {
            return rcp4000.CommPortClose();
        }

        // Start
        public void PollingStart()
        {
            rcp4000.CardEvent += new CardEventHandler(rcp4000_CardEvent);
            rcp4000.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            rcp4000.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            rcp4000.PollingStop();
            rcp4000.CardEvent -= new CardEventHandler(rcp4000_CardEvent);
        }

        private void rcp4000_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            return true;
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            return true;
        }

        public bool Unlock(int delay)
        {
            return true;
        }


        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return true;
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

            rcp4000.UploadSettings(rcp4000.Address, ref readerModel, ref readerDate, ref readerTime, ref numEvent);
            
            if (readerModel != "")
            {
                txtReaderModel.Text = readerModel;
                string datetime = readerDate + " " + readerTime;
                txtDate.Text = Convert.ToDateTime(datetime).ToString("dd/MM/yyyy");
                txtTime.Text = Convert.ToDateTime(datetime).ToString("HH:mm:ss");
                txtNumEvent.Text = numEvent.ToString();
            }
            else
                MessageBox.Show("Không thể kết nối tới Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            if (rcp4000.DownloadDateTime(datetime))
            {
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void RCP4000SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void RCP4000SettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteAllEvent_Click(object sender, EventArgs e)
        {
            if (rcp4000.DeleteAllEvent(rcp4000.Address))
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