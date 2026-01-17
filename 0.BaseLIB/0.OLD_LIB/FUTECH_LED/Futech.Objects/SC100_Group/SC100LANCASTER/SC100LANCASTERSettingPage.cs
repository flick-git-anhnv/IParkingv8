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
    public partial class SC100LANCASTERSettingPage : Form, IControllerSettingPage
    {
        private SC100LANCASTER sc100 = new SC100LANCASTER();

        public event CardEventHandler CardEvent;
        public event InputEventHandler InputEvent;
        public SC100LANCASTERSettingPage()
        {
            InitializeComponent();
            try
            {
                // Tham so mac dinh
                for (int i = 0; i < 256; i++)
                {
                    cbMID.Items.Add(i.ToString("000"));
                    cbFID.Items.Add(i.ToString("000"));
                }
                cbMID.SelectedIndex = 0;
                txtSystemCode.Text = "00000000";
                cbFID.SelectedIndex = 0;
                cbAdvance.SelectedIndex = 0;
                cbPayMode.SelectedIndex = 0;
                txtValue.Text = "0";
                txtDelayTime.Text = "0";
                txtDO1PeriodTime.Text = "0";
                cbLEDStatusOfSuscess.SelectedIndex = 1;

                cbxRelay.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        // Line ID
        public int LineID
        {
            set { sc100.LineID = value; }
        }

        // Controller Address
        public int Address
        {
            set { sc100.Address = value; }
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
            get { return sc100.IsConnect; }
            set { isconnect = value; }
        }

        // Delay time property
        public int DelayTime
        {
            set { sc100.DelayTime = value; }
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
            return sc100.Connect(comPort, (short)baudRate, communicationType);
        }

        public bool DisConnect()
        {
            return sc100.Disconnect();
        }

        // Start Polling
        public void PollingStart()
        {
            sc100.CardEvent += new CardEventHandler(sc100_CardEvent);
            sc100.InputEvent += new InputEventHandler(sc100_InputEvent);
            sc100.PollingStart(controllers);
        }

        void sc100_InputEvent(object sender, InputEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            if (InputEvent != null)
            {
                InputEvent(this, e);
            }
        }

        // Signal to Stop
        public void SignalToStop()
        {
            sc100.SignalToStop();
        }

        // Stop Polling
        public void PollingStop()
        {
            sc100.PollingStop();
            sc100.CardEvent -= new CardEventHandler(sc100_CardEvent);
            sc100.InputEvent -= new InputEventHandler(sc100_InputEvent);
        }

        private void sc100_CardEvent(object sender, CardEventArgs e)
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
            return true;// sc100.DownloadCard(employee.CardNumber, employee.Passwords, memoryID, "01", timezoneID);
        }

        public bool DeleteCard(Employee employee, int memoryID)
        {
            if (memoryID < 0)
                return false;
            return true;// sc100.DeleteCard(memoryID);
        }

        public string GetFinger(string cardNumber, int fingerID)
        {
            return "";
        }

        public bool Unlock(int delay)
        {
            return sc100.Unlock(1, delay);
        }

        public bool Unlock2(int outputNo, int delay)
        {
            return sc100.Unlock(outputNo, delay);
        }

        // Test connection
        public bool TestConnection()
        {
            return IsConnect;
        }

        private void SC100SettingPage_Load(object sender, EventArgs e)
        {
            PollingStop();
            // Tham so he thong
            btnLoad_Click(null, null);
            // Thong tin dau doc (so ban ghi su kien, version)
            btnGetVersion_Click(null, null);

            Controller controller = controllers.GetControllerByAddress(sc100.Address);
            txtSystemCode.Text = controller.SystemCode;
            txtKeyA.Text = controller.KeyA;
            txtKeyB.Text = controller.KeyB;
        }

        private void SC100SettingPage_FormClosing(object sender, FormClosingEventArgs e)
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
            if (sc100.DateTimeDownload(dtpDate.Value, dtpTime.Value))
                MessageBox.Show("Đã đặt lại thời gian lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt lại ngày giờ lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // DateTime Upload
        private void btnDateTimeUpload_Click(object sender, EventArgs e)
        {
            string temp = sc100.DateTimeUpload();
            if (temp != "")
            {
                dtpDate.Value = Convert.ToDateTime(temp);
                dtpTime.Value = Convert.ToDateTime(temp);
            }
            else
                MessageBox.Show("Đã xảy ra lỗi khi nhận ngày giờ từ Bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Set KeyA
        private void btnWriteKeyA_Click(object sender, EventArgs e)
        {
            if (sc100.SetKeyA(txtKeyA.Text))
                MessageBox.Show("Đã đặt lại Key A lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi đặt lại Key A lên bộ điều khiển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Recover Record
        private void btnRecoverRecord_Click(object sender, EventArgs e)
        {
            if (sc100.RecoverRecord())
                MessageBox.Show("Đã khôi phục lại dữ liệu đã bị xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Đã xảy ra lỗi khi khôi phục lại dữ liệu đã bị xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string[] message = sc100.GetRegister();
            if (message != null)
            {
                cbMID.Text = ByteUI.BaseToDecimal(message[1], 16).ToString("000");
                txtSystemCode.Text = sc100.SystemCode;// message[7] + message[6] + message[5] + message[4];
                cbFID.Text = ByteUI.BaseToDecimal(message[8], 16).ToString("000");

                byte[] bytes = ByteUI.Bytes(Convert.ToByte(ByteUI.BaseToDecimal(message[9], 16)));
                cbAdvance.SelectedIndex = bytes[0];
                cbPayMode.SelectedIndex = bytes[1];

                txtValue.Text = ByteUI.BaseToDecimal(message[14] + message[13] + message[12] + message[11], 16).ToString();
                txtDelayTime.Text = ByteUI.BaseToDecimal(message[16] + message[15], 16).ToString();
                txtDO1PeriodTime.Text = ByteUI.BaseToDecimal(message[18] + message[17], 16).ToString();

                cbLEDStatusOfSuscess.SelectedIndex = ByteUI.BaseToDecimal(message[21], 16);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //sc100.Address = Address;
            sc100.SystemCode = txtSystemCode.Text;
            sc100.FID = Convert.ToInt32(cbFID.Text);
            sc100.Advance = cbAdvance.SelectedIndex;
            sc100.PayMode = cbPayMode.SelectedIndex;
            sc100.Value1 = int.Parse(txtValue.Text);
            sc100.DO1DelayTime = int.Parse(txtDelayTime.Text);
            sc100.DO1PeriodTime = int.Parse(txtDO1PeriodTime.Text);
            sc100.LEDStatusOfSuccess = cbLEDStatusOfSuscess.SelectedIndex;

            sc100.SetRegister(Convert.ToInt32(cbMID.Text));
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            lbControllerInfo.Text = sc100.GetVersion();
        }

        private void btnAddToBlackList_Click(object sender, EventArgs e)
        {
            lsbBlackList.Items.Add(txtBlackList.Text);
        }

        private void btnRemoveFromBlackList_Click(object sender, EventArgs e)
        {
            if (lsbBlackList.SelectedItem != null)
                lsbBlackList.Items.Remove(lsbBlackList.SelectedItem);
        }

        private void btnReadBlackList_Click(object sender, EventArgs e)
        {
            BlackListCollection blacklists = new BlackListCollection();
            sc100.ReadBlackList(ref blacklists);
            lsbBlackList.Items.Clear();
            foreach (BlackList blacklist in blacklists)
            {
                lsbBlackList.Items.Add(blacklist.CardNumber);
            }
        }

        private void btnWriteBlackList_Click(object sender, EventArgs e)
        {
            BlackListCollection blacklists = new BlackListCollection();
            BlackList blacklist = new BlackList();
            // Xoa truoc khi luu
            sc100.WriteBlackList(blacklists);

            // Luu black list
            for(int i=0;i<lsbBlackList.Items.Count;i++)
            {
                blacklist = new BlackList();
                blacklist.CardNumber = lsbBlackList.Items[i].ToString();
                blacklists.Add(blacklist);
            }
            sc100.WriteBlackList(blacklists);
        }

        private void btnClearBalckList_Click(object sender, EventArgs e)
        {
            lsbBlackList.Items.Clear();
            btnWriteBlackList_Click(null, null);
        }

        private void lsbBlackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbBlackList.SelectedItem != null)
                txtBlackList.Text = lsbBlackList.SelectedItem.ToString();
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