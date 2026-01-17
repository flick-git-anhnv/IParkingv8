using ILedv8.Objects;
using ILedv8.UserControls;
using iParkingv5.LedDisplay.Enums;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;
using System.Data;
using System.Windows.Forms;

namespace ILedv8
{
    public partial class Form1 : Form
    {
        #region Properties
        List<LedLaneConfig> ledLaneConfigs = [];
        List<LedColorConfig> ledColorConfigs = [];
        private bool isInSettingMode = false;
        System.Timers.Timer? timerSendToLed;
        List<ucLed> ucs = [];

        #endregion

        #region Forms
        public Form1()
        {
            InitializeComponent();

            ledLaneConfigs.Clear();
            ledColorConfigs.Clear();
            foreach (var led in AppData.Leds)
            {
                for (int i = 0; i < led.row; i++)
                {
                    int lineIndex = i + 1;
                    string comport = led.comport;

                    var colorConfig = NewtonSoftHelper<LedColorConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledColorDisplayConfig(comport, lineIndex));
                    var ledLaneConfig = NewtonSoftHelper<LedLaneConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledLaneConfig(comport, lineIndex));

                    if (ledLaneConfig != null)
                        ledLaneConfigs.Add(ledLaneConfig);
                    if (colorConfig != null)
                        ledColorConfigs.Add(colorConfig);
                }
            }

            foreach (var item in AppData.Leds)
            {
                ucLed ucLed = new ucLed(item);
                ucs.Add(ucLed);
            }

            var uc = new ucGridview<ucLed>(ucs, 350, 1.5, 8, 16, 8);
            panel1.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;

            timerSendToLed = new System.Timers.Timer
            {
                Interval = AppData.delayConfig.SendToLedRowDuration
            };
            timerSendToLed.Elapsed += timerSendToLed_Tick;
            timerSendToLed.Start();
            this.FormClosing += Form1_FormClosing;
        }
        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion

        #region Controls In Form
        private void TsmiSetting_Click(object sender, EventArgs e)
        {
            timerSendToLed.Enabled = false;
            isInSettingMode = true;
            new FrmSetting().ShowDialog();

            ledLaneConfigs.Clear();
            ledColorConfigs.Clear();
            foreach (var led in AppData.Leds)
            {
                for (int i = 0; i < led.row; i++)
                {
                    int lineIndex = i + 1;
                    string comport = led.comport;

                    var colorConfig = NewtonSoftHelper<LedColorConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledColorDisplayConfig(comport, lineIndex));
                    var ledLaneConfig = NewtonSoftHelper<LedLaneConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledLaneConfig(comport, lineIndex));

                    if (ledLaneConfig != null)
                        ledLaneConfigs.Add(ledLaneConfig);
                    if (colorConfig != null)
                        ledColorConfigs.Add(colorConfig);
                }
            }

            timerSendToLed.Interval = AppData.delayConfig.SendToLedRowDuration;
            isInSettingMode = false;
            timerSendToLed.Enabled = true;
        }
        private void TsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }
        #endregion

        private async void timerSendToLed_Tick(object? sender, EventArgs e)
        {
            try
            {
                timerSendToLed.Enabled = false;
                var data = await AppData.ApiServer.ReportingService.Revenue.GetSumarizeTraffic(1);

                //<Led,<LineNumber, Count>>
                Dictionary<Led, Dictionary<int, int>> ledDisplayData = new Dictionary<Led, Dictionary<int, int>>();
                foreach (var led in AppData.Leds)
                {
                    Dictionary<int, int> lineData = new Dictionary<int, int>();
                    for (int i = 0; i < led.row; i++)
                    {
                        int lineIndex = i + 1;
                        lineData.Add(lineIndex, 0);
                        var config = this.ledLaneConfigs.Where(e => e.LedId == led.id && e.Line == lineIndex).FirstOrDefault();
                        if (config == null)
                        {
                            continue;
                        }
                        var laneIds = config.LaneIds;
                        var vehicleTypes = config.SupportVehicles;
                        var sumarizeData = data?.SumarizeData() ?? [];
                        int vehicleInParkingCount = 0;

                        if (sumarizeData.Count == 0)
                        {
                            vehicleInParkingCount = 0;
                        }
                        else
                        {
                            foreach (var laneId in laneIds)
                            {
                                if (!sumarizeData.ContainsKey(laneId))
                                {
                                    continue;
                                }
                                foreach (var vehicleType in vehicleTypes)
                                {
                                    if (!sumarizeData[laneId].ContainsKey(vehicleType))
                                    {
                                        continue;
                                    }
                                    vehicleInParkingCount += sumarizeData[laneId][vehicleType];
                                }
                            }
                        }

                        lineData[lineIndex] = Math.Max(0, config.MaxNumber - vehicleInParkingCount);
                    }

                    ledDisplayData.Add(led, lineData);
                }
                string a = Newtonsoft.Json.JsonConvert.SerializeObject(ledDisplayData, Newtonsoft.Json.Formatting.Indented);
                List<Task> tasks = new List<Task>();
                foreach (var led in ledDisplayData.Keys)
                {
                    var ucLed = ucs.Where(e => e.led.id == led.id).First();
                    if (ucLed != null)
                    {
                        tasks.Add(ucLed.DisplayLed(ledDisplayData[led], this.ledColorConfigs));
                    }
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (!isInSettingMode)
                {
                    timerSendToLed.Enabled = true;
                }
            }
        }


    }
}
