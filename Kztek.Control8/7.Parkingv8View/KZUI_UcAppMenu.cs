using iParkingv8.Object;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcAppMenu : UserControl
    {
        private IAPIServer? ApiServer;
        public event EventHandler? ReportIn;
        public event EventHandler? ReportOut;
        public event EventHandler? ReportRevenue;
        public event EventHandler? ReportHandOver;
        public event EventHandler? Customer;
        public event EventHandler? AccessKey;
        public event EventHandler? Vehicle;
        public event EventHandler? ChangePassword;
        public event EventHandler? OnRestart;
        public event EventHandler? OnAlarm;

        System.Timers.Timer? TimerUpdateTime;

        public KZUI_UcAppMenu()
        {
            InitializeComponent();

            this.BackColor = ColorManagement.AppBackgroudColor;
            panelMain.BackColor = ColorManagement.AppBackgroudColor;
            panelMain.FillColor = ColorManagement.AppBackgroudColor;

            tsmiReportIn.Click += TsmiReportIn_Click;
            tsmiReportOut.Click += TsmiReportOut_Click;
            tsmiRevenue.Click += TsmiRevenue_Click;
            tsmiHandOver.Click += TsmiHandOver_Click;

            tsmiAccessKey.Click += TsmiAccessKey_Click;
            tsmiCustomer.Click += TsmiCustomer_Click;
            tsmiVehicle.Click += TsmiVehicle_Click;

            tsmiAlarm.Click += TsmiAlarm_Click;
            tsmiChangePassword.Click += TsmiChangePassword_Click;
        }


        private void TsmiChangePassword_Click(object? sender, EventArgs e)
        {
            ChangePassword?.Invoke(this, EventArgs.Empty);
        }

        private void TsmiHandOver_Click(object? sender, EventArgs e)
        {
            ReportHandOver?.Invoke(sender, e);
        }

        private void TsmiVehicle_Click(object? sender, EventArgs e)
        {
            Vehicle?.Invoke(this, EventArgs.Empty);
        }
        private void TsmiCustomer_Click(object? sender, EventArgs e)
        {
            Customer?.Invoke(this, EventArgs.Empty);
        }
        private void TsmiAccessKey_Click(object? sender, EventArgs e)
        {
            AccessKey?.Invoke(this, EventArgs.Empty);
        }

        private void TsmiRevenue_Click(object? sender, EventArgs e)
        {
            ReportRevenue?.Invoke(this, EventArgs.Empty);
        }
        private void TsmiReportOut_Click(object? sender, EventArgs e)
        {
            ReportOut?.Invoke(this, EventArgs.Empty);
        }
        private void TsmiReportIn_Click(object? sender, EventArgs e)
        {
            ReportIn?.Invoke(this, EventArgs.Empty);
        }

        private void TsmiAlarm_Click(object? sender, EventArgs e)
        {
            OnAlarm?.Invoke(this, EventArgs.Empty);
        }
        public void Init(Image oemImage, IAPIServer ApiServer, EmPrintTemplate printTemplate, bool isDisplayMenu)
        {

            bool isTNG = (printTemplate == EmPrintTemplate.TNG_MINHCAU || printTemplate == EmPrintTemplate.TNG_VIETDUC) && !isDisplayMenu;
            if (isTNG)
            {
                miReport.DropDownItems.Remove(tsmiReportIn);
                miReport.DropDownItems.Remove(tsmiReportOut);
                miReport.DropDownItems.Remove(tsmiRevenue);

                miData.DropDownItems.Remove(tsmiAccessKey);
                miData.DropDownItems.Remove(tsmiCustomer);
                //miData.DropDownItems.Remove(tsmiVehicle);
                miData.Visible = true;
                miReport.Visible = false;
            }
            else
                CheckPermission();

            miSystem.Text = KZUIStyles.CurrentResources.MiSystem;
            tsmiChangePassword.Text = KZUIStyles.CurrentResources.MiChangePassword;
            tsmiLogOut.Text = KZUIStyles.CurrentResources.MiLogOut;
            tsmiExit.Text = KZUIStyles.CurrentResources.MiExit;

            miReport.Text = KZUIStyles.CurrentResources.MiReport;
            tsmiReportIn.Text = KZUIStyles.CurrentResources.MiReportIn;
            tsmiReportOut.Text = KZUIStyles.CurrentResources.MiReportOut;
            tsmiRevenue.Text = KZUIStyles.CurrentResources.MiReportRevenue;
            tsmiHandOver.Text = KZUIStyles.CurrentResources.MiReportHandOver;

            miData.Text = KZUIStyles.CurrentResources.MiData;
            tsmiAccessKey.Text = KZUIStyles.CurrentResources.MiAccessKeyList;
            tsmiCustomer.Text = KZUIStyles.CurrentResources.MiCustomerList;
            tsmiVehicle.Text = KZUIStyles.CurrentResources.MiVehicleList;

            if (printTemplate == EmPrintTemplate.BaoSon || printTemplate == EmPrintTemplate.MAXCOM || printTemplate == EmPrintTemplate.GoldenWestlake)
            {
                tsmiHandOver.Visible = true;
            }
            else
            {
                tsmiHandOver.Visible = false;
            }
            picLogo.Image = oemImage;
            this.ApiServer = ApiServer;
            TimerUpdateTime = new System.Timers.Timer
            {
                Interval = 1000
            };
            TimerUpdateTime.Elapsed += TimerUpdateTime_Tick;
            StartTimer();
        }

        public void StartTimer()
        {
            TimerUpdateTime.Start();
            //timerUpdateSystemCount.Enabled = true;
        }
        public void StopTimer()
        {
            TimerUpdateTime.Stop();
            timerUpdateSystemCount.Enabled = false;
        }

        private void TimerUpdateTime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lblTime.IsHandleCreated && lblTime.InvokeRequired)
                {
                    lblTime.BeginInvoke(new Action(() =>
                    {
                        lblTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                        lblTime.Width = lblTime.PreferredWidth;
                        lblTime.Refresh();
                    }));
                    return;
                }
                else
                {
                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                    lblTime.Width = lblTime.PreferredWidth;
                    lblTime.Refresh();
                }
            }
            finally
            {
            }
        }
        private async void TimerUpdateSystemCount_Tick(object sender, EventArgs e)
        {
            return;
            //try
            //{
            //    timerUpdateSystemCount.Enabled = false;
            //    var eventCountDetail = await ApiServer!.ReportingService.Revenue.SummaryCountAsync() ?? new SumaryCountEvent();
            //    lblGotIn.Text = "Vào: " + eventCountDetail.TotalVehicleIn;
            //    lblGotOut.Text = "Ra: " + eventCountDetail.TotalVehicleOut;
            //    lblInPark.Text = "Trong Bãi: " + eventCountDetail.CountAllEventIn;
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
            //    timerUpdateSystemCount.Enabled = true;
            //}
        }

        public void DisableControlBox()
        {
            this.Invoke(new Action(() =>
            {
                minimizeBox.Enabled = false;
                maximizeBox.Enabled = false;
                closeBox.Enabled = false;
            }));
        }
        public void EnableControlBox()
        {
            this.Invoke(new Action(() =>
            {
                minimizeBox.Enabled = true;
                maximizeBox.Enabled = true;
                closeBox.Enabled = true;
            }));
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

            //bool isCloseApp = MessageBox.Show(KZUIStyles.CurrentResources.ProcessConfirmCloseApp, KZUIStyles.CurrentResources.InfoTitle,
            //                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //                  == DialogResult.Yes;
            //if (isCloseApp)
            //{
            //    Application.Exit();
            //    Environment.Exit(0);
            //}
        }

        private void tsmiLogOut_Click(object sender, EventArgs e)
        {
            OnRestart?.Invoke(sender, EventArgs.Empty);
        }
        private void closeBox_Click(object sender, EventArgs e)
        {
            //bool isCloseApp = MessageBox.Show(KZUIStyles.CurrentResources.ProcessConfirmCloseApp, KZUIStyles.CurrentResources.InfoTitle,
            //                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            //                 == DialogResult.Yes;
            //if (isCloseApp)
            {
                Application.Exit();
                //Environment.Exit(0);
            }
        }

        private void CheckPermission()
        {
            return;
            bool reportInPermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/entry/features/read") ?? false;
            bool reportOutPermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/exit/features/read") ?? false;
            bool revenuePermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/revenue/features/read") ?? false;
            bool accessKeyPermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/access_key/features/read") ?? false;
            bool customerPermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/customer/features/read") ?? false;
            bool vehiclePermission = StaticPool.SelectedUser.screenFeatures?.Contains("screens/access_key/features/read") ?? false;

            if (!reportInPermission)
            {
                miReport.DropDownItems.Remove(tsmiReportIn);
            }
            if (!reportOutPermission)
            {
                miReport.DropDownItems.Remove(tsmiReportOut);
            }
            if (!revenuePermission)
            {
                miReport.DropDownItems.Remove(tsmiRevenue);
            }

            if (!accessKeyPermission)
            {
                miData.DropDownItems.Remove(tsmiAccessKey);
            }
            if (!customerPermission)
            {
                miData.DropDownItems.Remove(tsmiCustomer);
            }
            if (!vehiclePermission)
            {
                miData.DropDownItems.Remove(tsmiVehicle);
            }
            miData.Visible = accessKeyPermission || customerPermission || vehiclePermission;
            miReport.Visible = reportInPermission || reportOutPermission || revenuePermission;
        }
    }
}
