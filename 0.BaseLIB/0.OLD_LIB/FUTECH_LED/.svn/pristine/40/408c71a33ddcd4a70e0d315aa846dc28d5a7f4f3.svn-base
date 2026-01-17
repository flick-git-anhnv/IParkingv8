using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Futech.Tools;

namespace Futech.Objects
{
    public partial class PP3760SettingPage : Form, IControllerSettingPage
    {
        private PP3760 pp3760 = new PP3760();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public PP3760SettingPage()
        {
            InitializeComponent();

            cbxReaderMode.SelectedIndex = 0;
            cbxComparingCardNoMode.SelectedIndex = 1;
        }

        // Line ID
        public int LineID
        {
            set { pp3760.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { pp3760.Address = value; }
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
            set { pp3760.DelayTime = value; }
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
                    if (timezone.ID < 8)
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
                return pp3760.CommPortOpen(comPort, (short)baudRate);
            }
            else if (communicationType == 1)
            {

            }
            return false;
        }

        public bool DisConnect()
        {
            if (communicationType == 0)
            {
                return pp3760.CommPortClose();
            }
            else if (communicationType == 1)
            {

            }
            return false;
        }

        // Start
        public void PollingStart()
        {
            pp3760.CardEvent += new CardEventHandler(pp3760_CardEvent);
            pp3760.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            pp3760.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            pp3760.PollingStop();
            pp3760.CardEvent -= new CardEventHandler(pp3760_CardEvent);
        }

        private void pp3760_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        // Download Card 
        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            return pp3760.DownloadCard(employee.CardNumber, employee.Passwords, timezoneID);
        }

        // Delete Card
        public bool DeleteCard(Employee employee, int memoryID)
        {
            return pp3760.DeleteCard(employee.CardNumber, employee.Passwords);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        public bool Unlock(int delay)
        {
            return pp3760.OpenDoor();
        }


        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return pp3760.OpenDoor();
        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            if (pp3760.DownloadDate(datetime) && pp3760.DownloadTime(datetime))
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnInitialController_Click(object sender, EventArgs e)
        {
            if (pp3760.Init())
            {
                System.Threading.Thread.Sleep(1000);
                if (pp3760.SystemInitial(true))
                    MessageBox.Show("Đã khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Đã xảy ra lỗi khi khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnInitialControllerAsOffline_Click(object sender, EventArgs e)
        {
            if (pp3760.Init())
            {
                System.Threading.Thread.Sleep(1000);
                if (pp3760.SystemInitial(false))
                    MessageBox.Show("Đã khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Đã xảy ra lỗi khi khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi khởi tạo Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDownloadParameter_Click(object sender, EventArgs e)
        {
            pp3760.Init();
            System.Threading.Thread.Sleep(1000);
            if (pp3760.OnlineMode((cbxComparingCardNoMode.SelectedIndex == 0) ? true : false))
                MessageBox.Show("Đã thiết lập chế độ Comparing Card No Mode trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadCardCompareMode((chkComparingCardNo.Checked == true) ? 1 : 0))
                MessageBox.Show("Đã thiết lập chế độ Comparing Card trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadTimezoneComparingMode((chkComparingTimezone.Checked == true) ? 1 : 0))
                MessageBox.Show("Đã thiết lập chế độ Comparing Timezone trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadAntiPassbackMode((chkAntiPassback.Checked == true) ? 1 : 0))
                MessageBox.Show("Đã thiết lập chế độ AntiPassback trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadErrorCodeMode((chkErrorCode.Checked == true) ? 1 : 0))
                MessageBox.Show("Đã thiết lập chế độ Error Code trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadForbiddenRepeatReading((int)numForbiddenReadingCard.Value))
                MessageBox.Show("Đã đặt thời gian không cho quẹt thẻ lặp lại trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadReaderMode(cbxReaderMode.SelectedIndex))
                MessageBox.Show("Đã thiết lập chế độ đầu đọc trong Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (pp3760.DownloadDoorReleaseTime((int)numDoorReleaseTime.Value))
                MessageBox.Show("Đã thiết lập thời gian trễ của Rơle trên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbxTimezone_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset
            this.tabPage3.Controls.Remove(graphControl);
            graphControl = new GraphControl();
            graphControl.Size = new Size(356, 244);
            graphControl.Location = new Point(3, 46);
            this.tabPage3.Controls.Add(graphControl);
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

        // Timezone Download
        private void btnTimezoneDownload_Click(object sender, EventArgs e)
        {
            Timezone timezone = timezones.GetTimezoneByName(cbxTimezone.SelectedItem.ToString());
            if (timezone != null)
            {
                if (pp3760.DownloadTimezone(timezone))
                {
                    MessageBox.Show("Đã cập nhật Timezone ' " + timezone.Name + " ' lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi giao tiếp với Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void PP3760SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void PP3760SettingPage_FormClosing(object sender, FormClosingEventArgs e)
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