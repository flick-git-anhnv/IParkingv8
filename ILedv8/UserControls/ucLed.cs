using ILedv8.Objects;
using iParkingv5.LedDisplay.Enums;
using Kztek.Object;
using Kztek.Tool;

namespace ILedv8.UserControls
{
    public partial class ucLed : UserControl
    {
        public Led led;
        public ucLed(Led led)
        {
            InitializeComponent();
            this.led = led;
            lblLedName.Text = led.name + " - " + led.comport;
            for (int i = 0; i < led.row; i++)
            {
                ucLedLine uc = new ucLedLine(i + 1);
                panelLines.Controls.Add(uc);
                uc.Dock = DockStyle.Top;
                uc.BringToFront();
            }
        }

        public async Task DisplayLed(Dictionary<int, int> lineData, List<LedColorConfig> ledColorConfigs)
        {
            lblTime.Invoke(new Action(() =>
            {
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                lblTime.Refresh();
            }));
            bool isAllFalse = true;
            foreach (KeyValuePair<int, int> item in lineData)
            {
                int lineIndex = item.Key;
                int count = item.Value;

                var config = ledColorConfigs.Where(e => e.LedId == led.id && e.Line == lineIndex).FirstOrDefault() ?? new LedColorConfig();
                string cmd = "";

                string format = "";
                for (int i = 0; i < config.NumOfCharacterDisplay; i++)
                {
                    format += "0";
                }
                EmLedColor displayColor = EmLedColor.RED;
                if (count > 0)
                {
                    displayColor = config.DisplayColor;
                    cmd = $"SetLineCurrent?/Line={lineIndex}/FontSize={GetFontsizeStr(config.FontSize)}/Text=<Colour={(int)config.DisplayColor}>{count.ToString(format)}";
                }
                else
                {
                    displayColor = config.ZeroColor;
                    cmd = $"SetLineCurrent?/Line={lineIndex}/FontSize={GetFontsizeStr(config.FontSize)}/Text=<Colour={(int)config.ZeroColor}>{count.ToString(format)}";
                }
                string response = UdpTools.ExecuteCommand_Ascii(led.comport, led.baudrate, cmd, 1000);
                bool isSuccess = response.Contains("OK");
                if (isSuccess)
                {
                    isAllFalse = false;
                }
                this.Invoke(new Action(() =>
                {
                    foreach (ucLedLine item in panelLines.Controls)
                    {
                        if (item.lineIndex == lineIndex)
                        {
                            string errorMessage = isSuccess ? "" : response;
                            item.UpdateCount(count, format, GetFontsizeStr(config.FontSize), displayColor, isSuccess, errorMessage);
                            break;
                        }
                    }
                    
                }));
                await Task.Delay(TimeSpan.FromMilliseconds(AppData.delayConfig.SendToLedRowDuration)); // Delay between commands to avoid flooding the LED
            }
            if (isAllFalse)
            {
                lblLedName.DisabledState.FillColor = Color.DarkRed;
                lblLedName.DisabledState.BorderColor = Color.DarkRed;
            }
            else
            {
                lblLedName.DisabledState.FillColor = Color.DarkBlue;
                lblLedName.DisabledState.BorderColor = Color.DarkBlue;
            }
        }

        public static int GetFontsizeStr(EmFontSize fontSize)
        {
            switch (fontSize)
            {
                case EmFontSize.FontSize_7:
                    return 7;
                case EmFontSize.FontSize_8:
                    return 8;
                case EmFontSize.FontSize_10:
                    return 10;
                case EmFontSize.FontSize_12:
                    return 12;
                case EmFontSize.FontSize_13:
                    return 13;
                case EmFontSize.FontSize_14:
                    return 14;
                case EmFontSize.FontSize_16:
                    return 16;
                case EmFontSize.FontSize_23:
                    return 23;
                case EmFontSize.FontSize_24:
                    return 24;
                case EmFontSize.FontSize_25:
                    return 25;
                case EmFontSize.FontSize_26:
                    return 26;
                default:
                    return 7;
            }
        }
    }
}
