using iParkingv5.Controller;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using Kztek.Object;
using Kztek.Tool;
using System.Runtime.InteropServices;
using System.Text;
using static Kztek.Object.CardFormat;

namespace IParkingv8.RegisterCard
{
    public partial class Form1 : Form
    {
        #region Properties
        private iParkingv5.Controller.IController controller;
        private List<Collection> IdentityGroups = new List<Collection>();
        #endregion End Properties

        #region Forms
        public Form1()
        {
            InitializeComponent();
            var devs = new[]
           {
                new RAWINPUTDEVICE { usUsagePage = 0x01, usUsage = 0x06, dwFlags = RIDEV_INPUTSINK, hwndTarget = Handle } // Keyboard
            };
            if (!RegisterRawInputDevices(devs, (uint)devs.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }
        private async void frmMain_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Properties.Settings.Default.lastIndex;
            await GetDeviceConfig();
            await CreateUI();
            RegisterUIEvent();
        }
        private void FrmMain_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (this.controller != null)
            {
                this.controller.CardEvent -= Controller_CardEvent;
            }
            this.controller?.PollingStop();
            Application.Exit();
            Environment.Exit(0);
        }

        #endregion End Forms

        #region Controls In Form
        private async void BtnStart_Click(object? sender, EventArgs e)
        {
            string controllerId = ((ListItem)cbController.SelectedItem)?.Value ?? "";
            if (string.IsNullOrWhiteSpace(controllerId))
            {
                MessageBox.Show("Hãy chọn bộ điều khiển đăng ký định danh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string identityGroupId = ((ListItem)cbIdentityGroup.SelectedItem)?.Value ?? "";
            if (string.IsNullOrWhiteSpace(identityGroupId))
            {
                MessageBox.Show("Hãy chọn nhóm định danh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbController.SelectedIndex > 0)
            {
                var bdk = (from controller in AppData.Controllers
                           where controller.Id.ToString() == controllerId
                           select controller).FirstOrDefault()!;
                this.controller = ControllerFactory.CreateController(bdk.Id, bdk.Comport, bdk.Baudrate, bdk.Type, bdk.CommunicationType, bdk.Name, 500, ((EmPrintTemplate)AppData.AppConfig.PrintTemplate).ToString(), bdk.Code)!;
                bool isConnect = await this.controller.ConnectAsync();
                if (isConnect)
                {
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    this.controller.CardEvent += Controller_CardEvent;
                    this.controller.PollingStart();
                }
                else
                {
                    MessageBox.Show("Không kết nối được tới thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }
        private void BtnStop_Click(object? sender, EventArgs e)
        {
            btnStart.Enabled = true;
            if (this.controller != null)
            {
                this.controller.CardEvent -= Controller_CardEvent;
            }
            this.controller?.PollingStop();
        }
        #endregion End Controls In Form

        #region Private Function
        private async Task CreateUI()
        {
            //Load IdentityType
            foreach (var item in Enum.GetValues(typeof(EmAccessKeyType)))
            {
                cbIdentityType.Items.Add(item);
            }
            foreach (CardFormat.EmCardFormat item in Enum.GetValues(typeof(CardFormat.EmCardFormat)))
            {
                cbOutputFormat.Items.Add(CardFormat.ToString(item));
            }
            foreach (CardFormat.EmCardFormatOption item in Enum.GetValues(typeof(CardFormat.EmCardFormatOption)))
            {
                cbOption.Items.Add(CardFormat.ToString(item));
            }
            cbOutputFormat.SelectedIndexChanged += CbOutputFormat_SelectedIndexChanged;
            cbOption.SelectedIndexChanged += CbOption_SelectedIndexChanged;
            cbOutputFormat.SelectedIndex = Properties.Settings.Default.output_format;
            cbOption.SelectedIndex = Properties.Settings.Default.option;
            //Load IdentityGroups
            IdentityGroups = (await AppData.ApiServer.DataService.AccessKeyCollection.GetAllAsync())?.Item1 ?? new List<Collection>();
            foreach (var item in IdentityGroups)
            {
                ListItem li = new ListItem();
                li.Value = item.Id.ToString();
                li.Name = item.Name;
                cbIdentityGroup.Items.Add(li);
            }
            //Load Controllers
            cbController.Items.Add(new ListItem()
            {
                Value = "USB",
                Name = "USB",
            });
            foreach (var item in AppData.Controllers)
            {
                ListItem li = new ListItem();
                li.Value = item.Id.ToString();
                li.Name = item.Name;
                cbController.Items.Add(li);
            }

            cbIdentityGroup.ValueMember = "Value";
            cbIdentityGroup.DisplayMember = "Name";

            cbController.ValueMember = "Value";
            cbController.DisplayMember = "Name";

            cbController.SelectedIndex = cbController.Items.Count > 0 ? 0 : -1;
            cbIdentityGroup.SelectedIndex = cbIdentityGroup.Items.Count > 0 ? 0 : -1;
            cbIdentityType.SelectedIndex = cbIdentityType.Items.Count > 0 ? 0 : -1;
            cbFormat.SelectedIndex = 0;

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void CbOption_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.option = cbOption.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void CbOutputFormat_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Properties.Settings.Default.output_format = cbOutputFormat.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void RegisterUIEvent()
        {
            btnStart.Click += BtnStart_Click;
            btnStop.Click += BtnStop_Click;
            this.FormClosing += FrmMain_FormClosing;
        }
        private async void Controller_CardEvent(object sender, iParkingv5.Objects.Events.CardEventArgs e)
        {
            string collectionId = "";
            this.Invoke(new Action(() =>
            {
                collectionId = ((ListItem)cbIdentityGroup.SelectedItem)?.Value ?? "";
                if (string.IsNullOrWhiteSpace(collectionId))
                {
                    MessageBox.Show("Hãy chọn nhóm định danh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }));
            string code = "";
            this.Invoke(new Action(() =>
            {
                code = CardFactory.StandardlizedCardNumber(e.PreferCard, new CardFormatConfig()
                {
                    OutputFormat = (EmCardFormat)cbOutputFormat.SelectedIndex,
                    OutputOption = (EmCardFormatOption)cbOption.SelectedIndex,
                });
            }));
            AccessKey? accessKey = (await AppData.ApiServer.DataService.AccessKey.GetByCodeAsync(code))?.Item1;
            if (accessKey == null || string.IsNullOrEmpty(accessKey.Id))
            {
                int currentIndex = (int)numericUpDown1.Value + 1;
                string format = "";
                //Thêm mới
                accessKey = new AccessKey()
                {
                    Name = txtLetter.Text + currentIndex.ToString(format),
                    Code = code,
                    Collection = new Collection()
                    {
                        Id = collectionId
                    },
                };

                accessKey = (await AppData.ApiServer.DataService.AccessKey.CreateAsync(accessKey))?.Item1 ?? null;
                if (accessKey != null)
                {
                    this.Invoke(new Action(() =>
                    {
                        numericUpDown1.Value = currentIndex;
                        lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - Thêm mới");
                        Properties.Settings.Default.lastIndex = currentIndex;
                        Properties.Settings.Default.Save();
                    }));
                }
                else
                {
                    this.Invoke(new Action(() =>
                    {
                        numericUpDown1.Value = currentIndex;
                        lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - Thêm mới thất bại");
                    }));
                }
            }
            else
            {
                this.Invoke((Action)(() =>
                {
                    //Thông báo đã có
                    lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - đã tồn tại trong hệ thống");
                }));
            }
        }
        #endregion End Private Function

        private async void btnStart_Click_1(object sender, EventArgs e)
        {

        }
        public async Task<bool> GetDeviceConfig()
        {
            var deviceResponse = await AppData.ApiServer.DeviceService!.GetDeviceDataAsync();
            if (deviceResponse == null)
            {
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
                    if (AppData.Computer != null)
                    {
                        var lanes = deviceResponse.GetLanesByComputer(AppData.Computer);
                        var cameras = deviceResponse.GetCamerasByComputer(AppData.Computer);
                        var controllers = deviceResponse.GetBDKsByComputer(AppData.Computer);
                        var leds = deviceResponse.GetLedsByConputer(AppData.Computer);
                        var gates = deviceResponse.GetGatesByComputer(AppData.Computer);

                        AppData.Gate = gates.Count > 0 ? gates[0] : null;
                        AppData.Lanes = lanes;
                        AppData.Controllers = controllers;
                    }
                }
            }
            return true;
        }


        const int WM_INPUT = 0x00FF;
        const uint RIDEV_INPUTSINK = 0x00000100;
        const uint RIM_TYPEKEYBOARD = 1;

        [StructLayout(LayoutKind.Sequential)]
        struct RAWINPUTDEVICE { public ushort usUsagePage, usUsage; public uint dwFlags; public IntPtr hwndTarget; }

        [DllImport("User32.dll", SetLastError = true)]
        static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] p, uint n, uint cb);

        [DllImport("User32.dll")] static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);
        const uint RID_INPUT = 0x10000003;

        [StructLayout(LayoutKind.Sequential)]
        struct RAWINPUTHEADER { public uint dwType, dwSize; public IntPtr hDevice, wParam; }

        [StructLayout(LayoutKind.Sequential)] struct RAWMOUSE { uint d; }
        [StructLayout(LayoutKind.Sequential)] struct RAWHID { public uint SizeHid, Count; }
        [StructLayout(LayoutKind.Sequential)] struct RAWKEYBOARD { public ushort MakeCode, Flags, Reserved, VKey; public uint Message, ExtraInformation; }

        [StructLayout(LayoutKind.Explicit)]
        struct RAWINPUTUNION
        {
            [FieldOffset(0)] public RAWMOUSE mouse;
            [FieldOffset(0)] public RAWKEYBOARD keyboard;
            [FieldOffset(0)] public RAWHID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RAWINPUT { public RAWINPUTHEADER header; public RAWINPUTUNION data; }

        private readonly StringBuilder _buf = new StringBuilder();

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_INPUT)
            {
                uint size = 0;
                GetRawInputData(m.LParam, RID_INPUT, IntPtr.Zero, ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));
                IntPtr mem = Marshal.AllocHGlobal((int)size);
                try
                {
                    if (GetRawInputData(m.LParam, RID_INPUT, mem, ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == size)
                    {
                        var raw = Marshal.PtrToStructure<RAWINPUT>(mem);
                        if (raw.header.dwType == RIM_TYPEKEYBOARD)
                        {
                            var kb = raw.data.keyboard;
                            bool down = (kb.Message == 0x0100 /*WM_KEYDOWN*/ || kb.Message == 0x0104);
                            if (down)
                            {
                                var key = (Keys)kb.VKey;
                                if (key == Keys.Enter)
                                {
                                    var card = _buf.ToString();
                                    _buf.Clear();
                                    OnCardRead(card);
                                }
                                else
                                {
                                    char ch = VkToChar(key);
                                    if (ch != '\0') _buf.Append(ch);
                                }
                            }
                        }
                    }
                }
                finally { Marshal.FreeHGlobal(mem); }
            }
            base.WndProc(ref m);
        }

        private static char VkToChar(Keys key)
        {
            string s = key.ToString();
            if (s.Length == 1) return s[0];
            if (key >= Keys.D0 && key <= Keys.D9) return (char)('0' + (key - Keys.D0));
            if (key >= Keys.NumPad0 && key <= Keys.NumPad9) return (char)('0' + (key - Keys.NumPad0));
            if (key >= Keys.A && key <= Keys.Z) return (char)('A' + (key - Keys.A));
            return '\0';
        }

        private async void OnCardRead(string code)
        {
            code = code.Trim();
            this.Text = code;
            if (code.Length == 0) return;

            try
            {
                code = long.Parse(code).ToString("X");
                string collectionId = "";
                this.Invoke(new Action(() =>
                {
                    collectionId = ((ListItem)cbIdentityGroup.SelectedItem)?.Value ?? "";
                    if (string.IsNullOrWhiteSpace(collectionId))
                    {
                        MessageBox.Show("Hãy chọn nhóm định danh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }));
                this.Invoke(new Action(() =>
                {
                    code = CardFactory.StandardlizedCardNumber(code, new CardFormatConfig()
                    {
                        OutputFormat = (EmCardFormat)cbOutputFormat.SelectedIndex,
                        OutputOption = (EmCardFormatOption)cbOption.SelectedIndex,
                    });
                }));
                AccessKey? accessKey = (await AppData.ApiServer.DataService.AccessKey.GetByCodeAsync(code))?.Item1;
                if (accessKey == null || string.IsNullOrEmpty(accessKey.Id))
                {
                    int currentIndex = (int)numericUpDown1.Value + 1;
                    string format = "";
                    //Thêm mới
                    accessKey = new AccessKey()
                    {
                        Name = txtLetter.Text + currentIndex.ToString(format),
                        Code = code,
                        Collection = new Collection()
                        {
                            Id = collectionId
                        },
                    };

                    accessKey = (await AppData.ApiServer.DataService.AccessKey.CreateAsync(accessKey))?.Item1 ?? null;
                    if (accessKey != null)
                    {
                        this.Invoke(new Action(() =>
                        {
                            numericUpDown1.Value = currentIndex;
                            lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - Thêm mới");
                            Properties.Settings.Default.lastIndex = currentIndex;
                            Properties.Settings.Default.Save();
                        }));
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                        {
                            numericUpDown1.Value = currentIndex;
                            lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - Thêm mới thất bại");
                        }));
                    }
                }
                else
                {
                    this.Invoke((Action)(() =>
                    {
                        //Thông báo đã có
                        lsbShow.Items.Insert(0, DateTime.Now.ToString("HH:mm:ss") + " - " + code + " - đã tồn tại trong hệ thống");
                    }));
                }
            }
            catch (Exception)
            {
            }
            
        }
    }
}
