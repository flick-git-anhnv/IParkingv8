using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Futech.Objects
{
    public partial class PFP3700SettingPage : Form, IControllerSettingPage
    {
        private PFP3700 pfp3700 = new PFP3700();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public PFP3700SettingPage()
        {
            InitializeComponent();
        }

        // Line ID
        public int LineID
        {
            set { pfp3700.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { pfp3700.Address = value; }
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
            set { pfp3700.DelayTime = value; }
        }

        public int DownloadTime
        {
            set { pfp3700.DownloadTime = value; }
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
                return pfp3700.CommPortOpen(comPort, (short)baudRate);
            }
            else if (communicationType == 1)
            {

            }

            return false;
        }

        public bool DisConnect()
        {
            return pfp3700.CommPortClose();
        }

        // Start
        public void PollingStart()
        {
            pfp3700.CardEvent += new CardEventHandler(pfp3700_CardEvent);
            pfp3700.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            pfp3700.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            pfp3700.PollingStop();
            pfp3700.CardEvent -= new CardEventHandler(pfp3700_CardEvent);
        }

        private void pfp3700_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            return DownloadFinger(employee);
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            return pfp3700.DeleteFinger(int.Parse(employee.CardNumber), 1);
        }

        private bool DownloadFinger(Employee employee)
        {
            bool result = false;
            if (employee.Fingers1.Length > 0)
                result = pfp3700.DownloadFinger(employee.Fingers1, 1);
            if (employee.Fingers2.Length > 0)
                result = pfp3700.DownloadFinger(employee.Fingers2, 2);

            return result;
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            if (fingerID <= 2)
                return pfp3700.UploadFinger(int.Parse(cardNumber), fingerID);
            else
                return "";
        }

        public bool Unlock(int delay)
        {
            return false;
        }


        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return pfp3700.TestConnection();
        }

        private void btnDateTimeUpLoad_Click(object sender, EventArgs e)
        {
            string datetime = pfp3700.UploadDateTime();
            if (datetime != null)
            {
                txtDate.Text = Convert.ToDateTime(datetime).ToString("dd/MM/yyyy");
                txtTime.Text = Convert.ToDateTime(datetime).ToString("HH:mm:ss");
            }
            else
                MessageBox.Show("Không thể kết nối tới Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            if (pfp3700.DownloadDateTime(datetime))
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PFP3700SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void PFP3700SettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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