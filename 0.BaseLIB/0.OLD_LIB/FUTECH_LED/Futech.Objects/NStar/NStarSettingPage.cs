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
    public partial class NStarSettingPage : Form, IControllerSettingPage
    {
        private NStar nstar = new NStar();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public NStarSettingPage()
        {
            InitializeComponent();

            cbxRelay.SelectedIndex = 0;
            cbxRelay1.SelectedIndex = 0;
            cbxRelay2.SelectedIndex = 1;
        }

        // Line ID
        public int LineID
        {
            set { nstar.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { nstar.Address = value; }
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
            set { nstar.DelayTime = value; }
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
                return nstar.CommPortOpen(comPort, (short)baudRate);
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
                return nstar.CommPortClose();
            }
            else if (communicationType == 1)
            {

            }
            return false;
        }

        // Start
        public void PollingStart()
        {
            nstar.CardEvent += new CardEventHandler(nstar_CardEvent);
            nstar.PollingStart(controllers);
        }

        // Signal to Stop
        public void SignalToStop()
        {
            nstar.SignalToStop();
        }

        // Stop
        public void PollingStop()
        {
            nstar.PollingStop();
            nstar.CardEvent -= new CardEventHandler(nstar_CardEvent);
        }

        private void nstar_CardEvent(object sender, CardEventArgs e)
        {
            if (CardEvent != null)
            {
                CardEvent(this, e);
            }
        }

        // Download Card 
        public bool DownloadCard(Employee employee, int timezoneID, int memoryID)
        {
            return nstar.DownloadCard(employee.CardNumber, employee.Passwords, timezoneID);
        }

        // Delete Card
        public bool DeleteCard(Employee employee, int memoryID)
        {
            return nstar.DeleteCard(employee.CardNumber, employee.Passwords);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        public bool Unlock(int delay)
        {
            return nstar.Pulse(1);
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return false;
        }

        // Test connection
        public bool TestConnection()
        {
            return nstar.Pulse(1);
        }

        private void btnDateTimeDownload_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString());
            if (nstar.DownloadDateTime(datetime))
                MessageBox.Show("Đã đặt ngày giờ lên các Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt ngày giờ lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (nstar.Lock(cbxRelay.SelectedIndex + 1))
                MessageBox.Show("Lock OK!");
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (nstar.UnLock(cbxRelay.SelectedIndex + 1))
                MessageBox.Show("UnLock OK!");
        }

        private void btnShunt_Click(object sender, EventArgs e)
        {
            if (nstar.Shunt())
                MessageBox.Show("Shunt OK!");
        }

        private void btnUnShunt_Click(object sender, EventArgs e)
        {
            if (nstar.UnShunt())
                MessageBox.Show("UnShunt OK!");
        }

        private void btnPulse_Click(object sender, EventArgs e)
        {
            if (nstar.Pulse(1))
                MessageBox.Show("Pulse OK!");
        }

        private void btnDownloadParameter_Click(object sender, EventArgs e)
        {
            bool[] cardFormat = new bool[] { chkCardFormat1.Checked, chkCardFormat2.Checked, chkCardFormat3.Checked, chkCardFormat4.Checked, chkCardFormat5.Checked };
            if (nstar.DownloadConfiguration(chkAntiPassback.Checked, cardFormat))
                MessageBox.Show("Đã thiết lập cấu hình lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi thiết lập cấu hình lên Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                nstar.DownloadTimezone(timezone);
                MessageBox.Show("Timezone OK!");
            }
        }

        private void btnUpdateR1_Click(object sender, EventArgs e)
        {
            if (nstar.DownloadOutput(1, cbxRelay1.SelectedIndex + 1, (int)numRelay1.Value))
                MessageBox.Show("Đã thiết lập cấu hình đầu đọc 1.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi thiết lập cấu hình đầu đọc 1.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnUpdateR2_Click(object sender, EventArgs e)
        {
            if (nstar.DownloadOutput(1, cbxRelay1.SelectedIndex + 1, (int)numRelay1.Value))
                MessageBox.Show("Đã thiết lập cấu hình đầu đọc 2.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Đã xảy ra lỗi khi thiết lập cấu hình đầu đọc 2.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void NStarSettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
        }

        private void NStarSettingPage_FormClosing(object sender, FormClosingEventArgs e)
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