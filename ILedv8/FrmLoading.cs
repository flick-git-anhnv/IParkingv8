using ILedv8;
using Kztek.Tool;

namespace ILedv8.Forms
{
    public partial class FrmLoading : Form
    {
        #region Properties
        private readonly Dictionary<string, Func<Task<bool>>> loadingWorks = [];
        private List<string> displayMessages = [];
        private bool isWaiting = false;
        private int currentDisplayIndex = 0;
        #endregion End Properties

        #region Forms
        public FrmLoading()
        {
            InitializeComponent();

            loadingWorks.Add("Tải thông tin thiết bị", GetDeviceConfig);

            this.Shown += FrmLoading_Shown;
            this.FormClosing += FrmLoading_FormClosing;
        }
        private void FrmLoading_Shown(object? sender, EventArgs e)
        {
            timerLoading.Enabled = true;
        }
        private void FrmLoading_FormClosing(object? sender, FormClosingEventArgs e)
        {
            this.FormClosing -= FrmLoading_FormClosing;
            timerDisplayLoadingMessage.Enabled = false;
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion End Forms

        #region Private Function
        public async Task<bool> GetDeviceConfig()
        {
            lblMessage.Text = "Tải thông tin thiết bị";
            lblMessage.Refresh();
            StartTimerDisplayLoadingMessage();
            var deviceResponse = await AppData.ApiServer.DeviceService!.GetDeviceDataAsync();
            if (deviceResponse == null)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                await GetDeviceConfig();
            }
            else
            {
                List<string> validIps = NetWorkTools.GetLocalIPAddress();
                validIps.Add(Environment.MachineName.ToUpper());
                var computer = deviceResponse.GetComputerByIp(validIps);
                if (computer == null) await GetDeviceConfig();
                else
                {
                    AppData.Computer = computer;
                    var lanes = deviceResponse.GetAllLanes();
                    var leds = deviceResponse.GetLedsByConputer(AppData.Computer);
                    AppData.Lanes = lanes;
                    AppData.Leds = leds;
                }
            }
            StopTimerDisplayLoadingMessage();
            return true;
        }
        private static List<string> CreateDisplayListMessage(string message)
        {
            List<string> result =
                    [
                        message,
                        message + " .",
                        message + " ..",
                        message + " ..."
                    ];
            return result;
        }
        #endregion

        #region Timer
        private async void TimerLoading_Tick(object sender, EventArgs e)
        {
            timerLoading.Enabled = false;
            foreach (KeyValuePair<string, Func<Task<bool>>> actions in loadingWorks)
            {
                bool isSuccess = await actions.Value();
                if (!isSuccess)
                {
                    lblMessage!.Text = "Gặp Lỗi Khi " + actions.Key + ". Vui Lòng Khởi Động Lại Ứng Dụng Và Thử Lại";
                    lblMessage.ForeColor = Color.DarkRed;
                    lblMessage.Refresh();
                    return;
                }
            }
            timerDisplayLoadingMessage.Enabled = false;
            this.FormClosing -= FrmLoading_FormClosing;

            Form1 frm = new()
            {
                Owner = this.Owner
            };
            frm.Show();
            this.Close();
            GC.Collect();
        }

        private void StartTimerDisplayLoadingMessage()
        {
            displayMessages = CreateDisplayListMessage(lblMessage.Text);
            this.currentDisplayIndex = 0;
            this.isWaiting = true;
            timerDisplayLoadingMessage.Enabled = true;
        }
        private void StopTimerDisplayLoadingMessage()
        {
            this.currentDisplayIndex = 0;
            this.isWaiting = false;
            timerDisplayLoadingMessage.Enabled = false;
        }
        private void TimerDisplayLoadingMessage_Tick(object sender, EventArgs e)
        {
            if (!isWaiting)
            {
                return;
            }

            lblMessage!.Text = displayMessages[currentDisplayIndex];
            lblMessage.Refresh();
            currentDisplayIndex++;
            if (currentDisplayIndex > displayMessages.Count - 1)
            {
                currentDisplayIndex = 0;
            }
        }
        #endregion
    }
}
