using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Futech.Objects.KZE16AccessControl
{
    public partial class KZE16SettingPage : Form, IControllerSettingPage
    {
        private KZE16 kzE16 = new KZE16();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public KZE16SettingPage()
        {
            InitializeComponent();
            kzE16.CardEvent += new CardEventHandler(Fat810W_CardEvent);
            kzE16.InputEvent += Fat810W_InputEvent;
            this.Load += FAT810WSettingPage_Load;
            this.FormClosing += FAT810WSettingPage_FormClosing;
            btnInitDefault.Click += btnInitDefault_Click;
        }

        // Line ID
        public int LineID
        {
            set { kzE16.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { kzE16.Address = value; }
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

        public bool IsConnect
        {
            get { return kzE16.IsConnect; }
            set { }
        }

        // Delay time property
        public int DelayTime
        {
            set { kzE16.DelayTime = value; }
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
            set
            {
                controllers = value;
                kzE16.controllers = controllers;
            }
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
            return kzE16.Connect(comPort, (short)baudRate, communicationType);
        }

        public bool DisConnect()
        {
            return kzE16.Disconnect();
        }

        // Start
        public void PollingStart()
        {
            kzE16.PollingStart(controllers);
        }

        private void Fat810W_InputEvent(object sender, InputEventArgs e)
        {
            InputEvent?.Invoke(this, e);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            kzE16.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            kzE16.PollingStop();
            // Fat810W.CardEvent -= new CardEventHandler(Fat810W_CardEvent);
        }

        private void Fat810W_CardEvent(object sender, CardEventArgs e)
        {
            CardEvent?.Invoke(this, e);
        }

        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            if (memoryID < 0)
                return false;
            return true;// Fat810W.DownloadCard(employee.CardNumber, employee.Passwords, memoryID, "01", timezoneID);
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            if (memoryID < 0)
                return false;
            return true;// Fat810W.DeleteCard(memoryID);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        public bool Unlock(int delay)
        {
            kzE16.OpenRelay(1);
            Thread.Sleep(100);
            kzE16.OpenRelay(1);
            //Thread.Sleep(100);
            //Fat810W.OpenDoor();
            return true;
        }

        public bool Unlock2(int outputNo, int delay)
        {
            kzE16.OpenRelay(outputNo);
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return IsConnect;
        }

        private void FAT810WSettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
            Controller controller = controllers.GetControllerByAddress(kzE16.Address);
        }

        private void FAT810WSettingPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            PollingStart();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (kzE16.Reset())
                MessageBox.Show("Đã khởi động lại bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi khởi động lại bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btnInitDefault_Click(object sender, EventArgs e)
        {
            if (kzE16.ResetDefault())
                MessageBox.Show("Đã khởi tạo lại mặc định bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi khởi tạo lại mặc định bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void DelAllEvent()
        {
        }
        public string GetInputState()
        {
            return "";
        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            string timeSet = dtpDate.Value.ToString("yyyy/MM/dd") + dtpTime.Value.ToString("HHmmss");
            bool isSetSuccess = kzE16.SetDateTime(timeSet);
            if (isSetSuccess)
            {
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại");
            }
        }

        private void btnDateTimeUpload_Click(object sender, EventArgs e)
        {
            string time = kzE16.GetDateTime();
            try
            {
                DateTime dtpDateTime = DateTime.Parse(time);
                dtpDate.Value = dtpDateTime;
                dtpTime.Value = dtpDateTime;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            string version = kzE16.GetVersion();
            lbControllerInfo.Text = version;
        }


        private void GetInputFunction(int inputIndex)
        {
            kzE16.GetInputFunction(inputIndex, out bool isEnable, out int relay);
            if (relay == -1)
            {
                MessageBox.Show("Nhận Thông Tin Thất Bại");
                return;
            }

            switch (inputIndex)
            {
                case 1:
                    cbControlStateExitBtn1.SelectedIndex = isEnable ? 0 : 1;
                    cbRelayExitBtn1.SelectedIndex = relay - 1;
                    break;
                case 2:
                    cbControlStateExitBtn2.SelectedIndex = isEnable ? 0 : 1;
                    cbRelayExitBtn2.SelectedIndex = relay - 1;
                    break;
                case 3:
                    cbControlStateExitMsg1.SelectedIndex = isEnable ? 0 : 1;
                    cbRelayExitMsg1.SelectedIndex = relay - 1;
                    break;
                case 4:
                    cbControlStateExitMsg2.SelectedIndex = isEnable ? 0 : 1;
                    cbRelayExitMsg2.SelectedIndex = relay - 1;
                    break;
                default:
                    break;
            }
        }

        private void SetInputFunction(int inputIndex)
        {
            bool isEnable = false;
            int relay = 1;
            if (relay == -1)
            {
                MessageBox.Show("Nhận Thông Tin Thất Bại");
                return;
            }

            switch (inputIndex)
            {
                case 1:
                    isEnable = cbControlStateExitBtn1.SelectedIndex == 0 ;
                    relay = cbRelayExitBtn1.SelectedIndex + 1;
                    break;
                case 2:
                    isEnable = cbControlStateExitBtn2.SelectedIndex == 0 ;
                    relay = cbRelayExitBtn2.SelectedIndex  + 1;
                    break;
                case 3:
                    isEnable = cbControlStateExitMsg1.SelectedIndex == 0;
                    relay = cbRelayExitMsg1.SelectedIndex  + 1;
                    break;
                case 4:
                    isEnable = cbControlStateExitMsg2.SelectedIndex == 0;
                    relay = cbRelayExitMsg2.SelectedIndex  + 1;
                    break;
                default:
                    break;
            }

            if (kzE16.SetInputFunction(inputIndex, isEnable, relay))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin thất bại");
            }
        }

        private void GetInputFunciton_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GetInputFunction(int.Parse(btn.Tag.ToString()));
        }

        private void SetInputFunciton_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SetInputFunction(int.Parse(btn.Tag.ToString()));
        }
    }
}