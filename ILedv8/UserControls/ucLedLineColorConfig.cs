using ILedv8.Objects;
using iParkingv5.LedDisplay.Enums;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace ILedv8.UserControls
{
    public partial class ucLedLineColorConfig : UserControl
    {
        public Led led;
        public int line = 1;

        public EmLedColor LedColor
        {
            get
            {
                switch (cbColor.SelectedIndex)
                {
                    case 0: return EmLedColor.RED;
                    case 1: return EmLedColor.GREEN;
                    case 2: return EmLedColor.YELLOW;
                    case 3: return EmLedColor.BLUE;
                    case 4: return EmLedColor.PURPLE;
                    case 5: return EmLedColor.GREEN_BLUE;
                    case 6: return EmLedColor.WHITE;
                    default: return EmLedColor.RED;
                }
            }
        }
        public EmLedColor ZeroLedColor
        {
            get
            {
                switch (cbZeroColor.SelectedIndex)
                {
                    case 0: return EmLedColor.RED;
                    case 1: return EmLedColor.GREEN;
                    case 2: return EmLedColor.YELLOW;
                    case 3: return EmLedColor.BLUE;
                    case 4: return EmLedColor.PURPLE;
                    case 5: return EmLedColor.GREEN_BLUE;
                    case 6: return EmLedColor.WHITE;
                    default: return EmLedColor.RED;
                }
            }
        }
        public EmFontSize LedFontSize
        {
            get
            {
                switch (cbFontSize.SelectedIndex)
                {
                    case 0: return EmFontSize.FontSize_7;
                    case 1: return EmFontSize.FontSize_8;
                    case 2: return EmFontSize.FontSize_10;
                    case 3: return EmFontSize.FontSize_12;
                    case 4: return EmFontSize.FontSize_13;
                    case 5: return EmFontSize.FontSize_14;
                    case 6: return EmFontSize.FontSize_16;
                    case 7: return EmFontSize.FontSize_23;
                    case 8: return EmFontSize.FontSize_24;
                    case 9: return EmFontSize.FontSize_25;
                    case 10: return EmFontSize.FontSize_26;
                    default: return EmFontSize.FontSize_13;
                }
            }
        }
        public ucLedLineColorConfig(Led led, int line)
        {
            InitializeComponent();
            this.led = led;
            this.line = line;
            lblLineName.Text = "Dòng " + line;
            var config = NewtonSoftHelper<LedColorConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledColorDisplayConfig(led.comport, line)) ?? new LedColorConfig();
            if (config != null)
            {
                numMaxCharacter.Value = config.NumOfCharacterDisplay;
                switch (config.FontSize)
                {
                    case EmFontSize.FontSize_7:
                        cbFontSize.SelectedIndex = 0;
                        break;
                    case EmFontSize.FontSize_8:
                        cbFontSize.SelectedIndex = 1;
                        break;
                    case EmFontSize.FontSize_10:
                        cbFontSize.SelectedIndex = 2;
                        break;
                    case EmFontSize.FontSize_12:
                        cbFontSize.SelectedIndex = 3;
                        break;
                    case EmFontSize.FontSize_13:
                        cbFontSize.SelectedIndex = 4;
                        break;
                    case EmFontSize.FontSize_14:
                        cbFontSize.SelectedIndex = 5;
                        break;
                    case EmFontSize.FontSize_16:
                        cbFontSize.SelectedIndex = 6;
                        break;
                    case EmFontSize.FontSize_23:
                        cbFontSize.SelectedIndex = 7;
                        break;
                    case EmFontSize.FontSize_24:
                        cbFontSize.SelectedIndex = 8;
                        break;
                    case EmFontSize.FontSize_25:
                        cbFontSize.SelectedIndex = 9;
                        break;
                    case EmFontSize.FontSize_26:
                        cbFontSize.SelectedIndex = 10;
                        break;
                    default:
                        cbFontSize.SelectedIndex = 0;
                        break;
                }
                switch (config.DisplayColor)
                {
                    case EmLedColor.RED:
                        cbColor.SelectedIndex = 0;
                        break;
                    case EmLedColor.GREEN:
                        cbColor.SelectedIndex = 1;
                        break;
                    case EmLedColor.YELLOW:
                        cbColor.SelectedIndex = 2;
                        break;
                    case EmLedColor.BLUE:
                        cbColor.SelectedIndex = 3;
                        break;
                    case EmLedColor.PURPLE:
                        cbColor.SelectedIndex = 4;
                        break;
                    case EmLedColor.GREEN_BLUE:
                        cbColor.SelectedIndex = 5;
                        break;
                    case EmLedColor.WHITE:
                        cbColor.SelectedIndex = 6;
                        break;
                    default:
                        cbColor.SelectedIndex = 0;
                        break;
                }
                switch (config.ZeroColor)
                {
                    case EmLedColor.RED:
                        cbZeroColor.SelectedIndex = 0;
                        break;
                    case EmLedColor.GREEN:
                        cbZeroColor.SelectedIndex = 1;
                        break;
                    case EmLedColor.YELLOW:
                        cbZeroColor.SelectedIndex = 2;
                        break;
                    case EmLedColor.BLUE:
                        cbZeroColor.SelectedIndex = 3;
                        break;
                    case EmLedColor.PURPLE:
                        cbZeroColor.SelectedIndex = 4;
                        break;
                    case EmLedColor.GREEN_BLUE:
                        cbZeroColor.SelectedIndex = 5;
                        break;
                    case EmLedColor.WHITE:
                        cbZeroColor.SelectedIndex = 6;
                        break;
                    default:
                        cbZeroColor.SelectedIndex = 0;
                        break;
                }

            }
        }
        public LedColorConfig GetConfig()
        {
            return new LedColorConfig()
            {
                LedId = this.led.id,
                Line = this.line,
                DisplayColor = this.LedColor,
                ZeroColor = this.ZeroLedColor,
                FontSize = this.LedFontSize,
                NumOfCharacterDisplay = (int)numMaxCharacter.Value
            };
        }
    }
}
