using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Futech.Tools;

namespace Futech.Objects
{
    public partial class E01SettingPage : Form, IControllerSettingPage
    {
        private E01 e01 = new E01();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;

        public E01SettingPage()
        {
            InitializeComponent();
            try
            {
                cbxRelay.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        // Line ID
        public int LineID
        {
            set { e01.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { e01.Address = value; }
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
            get { return e01.IsConnect; }
            set { isconnect = value; }
        }

        // Delay time property
        public int DelayTime
        {
            set { e01.DelayTime = value; }
        }

        private int downloadTime = 1500;
        public int DownloadTime
        {
            set
            {
                downloadTime = value;
            }
        }

        // all controller in line
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
            }
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
            return e01.Connect(comPort, (short)baudRate, communicationType);
        }

        public bool DisConnect()
        {
            return e01.Disconnect();
        }

        // Start Polling
        public void PollingStart()
        {
            e01.CardEvent += new CardEventHandler(e01_CardEvent);
            e01.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            e01.SignalToStop();
        }

        // Stop Polling
        public void PollingStop()
        {
            e01.PollingStop();
            e01.CardEvent -= new CardEventHandler(e01_CardEvent);
        }

        private void e01_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            if (memoryID < 0)
                return false;
            return true;// e01.DownloadCard(employee.CardNumber, employee.Passwords, memoryID, "01", timezoneID);
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            if (memoryID < 0)
                return false;
            return true;// e01.DeleteCard(memoryID);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        public bool Unlock(int delay)
        {
            return e01.Unlock(0, delay);
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return e01.Unlock(outputNo, delay);
        }

        // Test connection
        public bool TestConnection()
        {
            return IsConnect;
        }

        private void E01SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
            // Thong tin dau doc (so ban ghi su kien, version)
            btnGetVersion_Click(null, null);
        }

        private void E01SettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // DateTime Download
        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            if (e01.DateTimeDownload(dtpDate.Value, dtpTime.Value))
                MessageBox.Show("Đã đặt lại thời gian lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt lại ngày giờ lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // DateTime Upload
        private void btnDateTimeUpload_Click(object sender, EventArgs e)
        {
            string temp = e01.DateTimeUpload();
            if (temp != "")
            {
                dtpDate.Value = Convert.ToDateTime(temp);
                dtpTime.Value = Convert.ToDateTime(temp);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi nhận ngày giờ từ Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            lbControllerInfo.Text = e01.GetVersion();
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            Unlock2(cbxRelay.SelectedIndex, 300);
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