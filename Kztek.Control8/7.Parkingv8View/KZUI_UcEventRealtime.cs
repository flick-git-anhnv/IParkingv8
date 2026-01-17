using iParkingv5.Controller;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Licenses;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcEventRealtime : UserControl
    {
        private LicenseExpire? licenseExpire;
        private IAPIServer server;
        private readonly System.Timers.Timer? timerCheckServerConnection;
        private DateTime serverTime = DateTime.Now;
        public KZUI_UcEventRealtime()
        {
            InitializeComponent();
            timerCheckServerConnection = new System.Timers.Timer();
            timerCheckServerConnection.Elapsed += TimerCheckServerConnection_Tick;
            timerCheckServerConnection.Interval = 5000;
        }

        public void StopTimer()
        {
            timer1.Enabled = false;
            foreach (var item in panelMain.Controls.OfType<KZUI_UcControllerStatus>())
            {
                item.StopTimer();
            }
            foreach (var item in panelMain.Controls.OfType<KZUI_UcLedStatus>())
            {
                item.StopTimer();
            }
        }
        private int CountdownExpDate = 0;
        public string LastCardNumber { get; set; } = string.Empty;

        public void Init(string userName, List<IController> controllers, IEnumerable<Object.Led> leds, List<Lane> lanes,
                         LicenseExpire? licenseExpire, string version, string serverName, string lprType,
                         IAPIServer server)
        {
            this.licenseExpire = licenseExpire;
            this.server = server;
            lblUser.Text = userName;
            lblServerName.Text = serverName;
            lblVersion.Text = "v" + version;
            lblVersion.Width = lblVersion.PreferredWidth;

            lblLprType.Text = lprType;
            lblLprType.Width = lblLprType.PreferredWidth;

            lblServerName.Width = lblServerName.PreferredWidth;
            lblUser.Width = lblUser.PreferredWidth;

            for (int i = 0; i < lanes.Count; i++)
            {
                Lane? lane = lanes[i];
                Color backColor = Color.DarkGreen;
                if (lane.Type == (int)EmLaneType.LANE_OUT)
                {
                    backColor = Color.DarkRed;
                }
                Label lbl = new()
                {
                    Name = lane.Id,
                    AutoSize = false,
                    Text = "___",
                    Font = lblVersion.Font,
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(3, 0, 3, 0),
                    ForeColor = Color.White,
                    BackColor = backColor,
                };
                var preferredWidth = lbl.PreferredWidth;
                lbl.Width = preferredWidth;
                panelMain.Controls.Add(lbl);
                lbl.Dock = DockStyle.Right;
            }

            foreach (IController controller in controllers)
            {
                KZUI_UcControllerStatus kZUI_UcControllerStatus = new();
                panelMain.Controls.Add(kZUI_UcControllerStatus);
                kZUI_UcControllerStatus.Dock = DockStyle.Right;
                kZUI_UcControllerStatus.Init(controller);
            }
            foreach (var led in leds)
            {
                KZUI_UcLedStatus kZUI_UcLedStatus = new();
                panelMain.Controls.Add(kZUI_UcLedStatus);
                kZUI_UcLedStatus.Dock = DockStyle.Right;
                kZUI_UcLedStatus.Init(led);
            }


            lblLprType.SendToBack();
            lblVersion.SendToBack();
            lblRealtimeEvent.BringToFront();

            if (licenseExpire != null)
            {
                lblLicenseInfo.Visible = true;
                timer1.Enabled = true;
            }
            else
            {
                lblLicenseInfo.Visible = false;
                timer1.Enabled = false;
            }

            foreach (var item in panelMain.Controls.OfType<KZUI_UcControllerStatus>())
            {
                item.StartTimer();
            }

            foreach (var item in panelMain.Controls.OfType<KZUI_UcLedStatus>())
            {
                item.StartTimer();
            }

            timerCheckServerConnection?.Start();
        }

        public void ShowEvent(string message)
        {
            lblRealtimeEvent.Invoke(new Action(() =>
            {
                if (lblRealtimeEvent.ForeColor != Color.Black)
                {
                    lblRealtimeEvent.ForeColor = Color.Black;
                }
                lblRealtimeEvent.Text = message;
                lblRealtimeEvent.Refresh();
            }));
        }
        public void ShowErrorMessage(string message)
        {
            lblRealtimeEvent.Invoke(new Action(() =>
            {
                if (lblRealtimeEvent.ForeColor != Color.Red)
                {
                    lblRealtimeEvent.ForeColor = Color.Red;
                }
                lblRealtimeEvent.Text = message;
                lblRealtimeEvent.Refresh();
            }));
        }

        private async void timerCheckLicense_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime expiredate = DateTime.Parse(this.licenseExpire!.ExpireDate.Replace("-", "/"));
            if (serverTime > expiredate || now > expiredate)
            {
                lblLicenseInfo.Text = $"{KZUIStyles.CurrentResources.LicenseValid}: " + 0 + $" {KZUIStyles.CurrentResources.Day}";
                lblLicenseInfo.Refresh();
                lblLicenseInfo.ForeColor = Color.Red;
                lblLicenseInfo.Width = lblLicenseInfo.PreferredWidth;
                CountdownExpDate++;
                if (CountdownExpDate >= 10)
                    MessageBox.Show("License expired!");

                if (CountdownExpDate > 10 * 60)
                {
                    MessageBox.Show("License expired!");
                    Environment.Exit(Environment.ExitCode);
                }
            }
            else
            {
                //if (DateTime.Now.Minute % 2 == 0)
                {
                    long ddiff = DateUI.DateDiff(DateInterval.Day, serverTime, expiredate);
                    if (ddiff < 15)
                    {
                        lblLicenseInfo.Text = $"{KZUIStyles.CurrentResources.LicenseValid}: " + ddiff + $" {KZUIStyles.CurrentResources.Day}";
                        lblLicenseInfo.Refresh();
                        lblLicenseInfo.Width = lblLicenseInfo.PreferredWidth;
                    }
                    else
                    {
                        lblLicenseInfo.Text = "-";
                        lblLicenseInfo.Refresh();
                        lblLicenseInfo.Width = lblLicenseInfo.PreferredWidth;
                    }
                }
            }
        }
        //private bool isFirstDisconnect = false;
        private int disconnectTime = 0;
        private async void TimerCheckServerConnection_Tick(object? sender, EventArgs e)
        {
            timerCheckServerConnection!.Enabled = false;
            try
            {
                var serverTime = await this.server.DataService.SystemConfig.GetServerTime();
                if (serverTime != null)
                {
                    lblServerName.Invoke(new Action(() =>
                    {
                        lblServerName.ForeColor = ColorManagement.SuccessColor;
                    }));
                    //isFirstDisconnect = false;
                    disconnectTime = 0;
                }
                else
                {
                    if (disconnectTime >= 15)
                    {
                        lblServerName.Invoke(new Action(() =>
                        {
                            lblServerName.ForeColor = ColorManagement.ErrorColor;
                        }));
                        //if (!isFirstDisconnect)
                        //{
                        //    isFirstDisconnect = true;
                        //    lblServerName.Invoke(new Action(() =>
                        //    {
                        //        MessageBox.Show("Mất kết nối tới máy chủ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }));
                        //}
                    }
                    else
                    {
                        disconnectTime++;
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                timerCheckServerConnection.Enabled = true;
            }
        }

        public void UpdateLprType(string lprType)
        {
            lblLprType.BeginInvoke(new Action(() =>
            {
                lblLprType.Text = lprType;
                lblLprType.Width = lblLprType.PreferredWidth;
                lblLprType.Refresh();
            }));
        }

        public void UpdateDuration(string laneId, long duration)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDuration(laneId, duration)));
                return;
            }
            foreach (var item in panelMain.Controls.OfType<Label>())
            {
                if (item.Name != laneId)
                {
                    continue;
                }
                item.Text = duration.ToString() + "ms";
                item.Width = item.PreferredWidth;
            }
        }

        private void lblRealtimeEvent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LastCardNumber))
            {
                if (this.IsHandleCreated && this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        try
                        {
                            Clipboard.SetText(this.LastCardNumber);
                        }
                        catch (Exception)
                        {
                        }
                    }));
                }
                else
                {
                    try
                    {
                        Clipboard.SetText(this.LastCardNumber);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
