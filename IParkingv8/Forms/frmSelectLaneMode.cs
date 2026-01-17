using iParkingv8.Object.Objects.Devices;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Tool;

namespace IParkingv8.Forms
{
    public partial class FrmSelectLaneMode : Form, KzITranslate
    {
        #region Properties
        int waitTimes = 0;
        const int WAIT_TIME = 30;
        #endregion End Properties

        #region Forms
        public FrmSelectLaneMode()
        {
            InitializeComponent();

            Translate();

            this.Load += FrmSelectLaneMode_Load;
            this.FormClosing += FrmSelectLaneMode_FormClosing;
        }
        private void FrmSelectLaneMode_Load(object? sender, EventArgs e)
        {
            InitUI();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btnConfirm.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void FrmSelectLaneMode_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion End Forms

        #region Controls In Form
        private void ChbSelectAll_CheckedChanged(object? sender, EventArgs e)
        {
            timerAutoSelectLane.Enabled = false;
            lblStatus.Visible = false;
            foreach (CheckBox item in panelActiveLanes.Controls)
            {
                item.Checked = chbSelectAll.Checked;
            }
        }
        private void ChbSelectAll_Click(object? sender, EventArgs e)
        {
            timerAutoSelectLane.Enabled = false;
            lblStatus.Visible = false;
        }
        private void Chb_Click(object? sender, EventArgs e)
        {
            timerAutoSelectLane.Enabled = false;
            lblStatus.Visible = false;
        }
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            this.FormClosing -= FrmSelectLaneMode_FormClosing;

            timerAutoSelectLane.Enabled = false;
            lblStatus.Visible = false;

            List<Lane> lanes = [];
            List<string> activeLaneIds = [];
            for (int i = 0; i < panelActiveLanes.Controls.Count; i++)
            {
                if (panelActiveLanes.Controls[i] is not CheckBox chb)
                {
                    continue;
                }
                if (!chb.Checked) { continue; }
                string laneId = chb.Name;
                activeLaneIds.Add(laneId);
            }
            if (activeLaneIds.Count == 0)
            {
                MessageBox.Show(KZUIStyles.CurrentResources.ChooseLaneRequired, KZUIStyles.CurrentResources.InfoTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            foreach (var lane in AppData.Lanes)
            {
                if (activeLaneIds.Contains(lane.Id))
                {
                    lanes.Add(lane);
                }
            }
            NewtonSoftHelper<List<string>>.SaveConfig(activeLaneIds, IparkingingPathManagement.appActiveLaneConfigPath());

            activeLaneIds.Clear();
            FrmMain frm = new(lanes)
            {
                Owner = this.Owner
            };
            frm.Show();
            this.Close();
            GC.Collect();
            return true;
        }
        #endregion End Controls In Form

        #region Timer
        private void TimerAutoSelectLane_Tick(object sender, EventArgs e)
        {
            waitTimes++;
            if (waitTimes >= WAIT_TIME)
            {
                timerAutoSelectLane.Enabled = false;
                lblStatus.Visible = false;
                btnConfirm.PerformClick();
            }
            else
            {
                lblStatus.Text = $"{KZUIStyles.CurrentResources.AutoOpenHomePage} {WAIT_TIME - waitTimes} s";
            }
        }
        #endregion End Timer

        public void Translate()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(Translate);
                return;
            }

            this.Text = KZUIStyles.CurrentResources.FrmSelectLane;
            lblTitle.Text = KZUIStyles.CurrentResources.ChooseLaneRequired;
            chbSelectAll.Text = KZUIStyles.CurrentResources.SelectAll;
            lblStatus.Text = KZUIStyles.CurrentResources.AutoOpenHomePage;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
        }
        private void InitUI()
        {
            panelActiveLanes.AutoScroll = true;
            List<string> activeLaneIds = NewtonSoftHelper<List<string>>.DeserializeObjectFromPath(IparkingingPathManagement.appActiveLaneConfigPath()) ?? [];
            int index = 0;
            foreach (var item in AppData.Lanes)
            {
                CheckBox chb = new()
                {
                    Name = item.Id,
                    Text = item.Name
                };
                panelActiveLanes.Controls.Add(chb);
                chb.Location = new Point(0, (chb.Height + TextManagement.ROOT_SIZE) * index);
                chb.AutoSize = true;
                chb.Click += Chb_Click;
                if (activeLaneIds.Contains(chb.Name))
                {
                    chb.Checked = true;
                }
                index++;
            }

            btnConfirm.OnClickAsync += BtnConfirm_Click;
            chbSelectAll.Click += ChbSelectAll_Click;
            chbSelectAll.CheckedChanged += ChbSelectAll_CheckedChanged;
        }
    }
}
