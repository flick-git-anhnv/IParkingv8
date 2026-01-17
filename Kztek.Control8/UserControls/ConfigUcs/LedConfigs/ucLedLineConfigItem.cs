using iParkingv5.LedDisplay.Enums;
using iParkingv5.LedDisplay.LEDs;

namespace Kztek.Control8.UserControls.ConfigUcs
{
    public partial class ucLedLineConfigItem : UserControl
    {
        #region Properties
        private static Dictionary<int, int> preferFontByLine = new Dictionary<int, int>();
        private static Dictionary<int, int> preferColorByLine = new Dictionary<int, int>();
        public int lineIndex = 0;
        public EmLedDisplayValue LedDisplayValue
        {
            get => (EmLedDisplayValue)cbDisplayMode.SelectedIndex;
        }
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
        public string FreeText { get => txtDisplaytext.Text; }
        #endregion End Properties

        #region Forms
        public ucLedLineConfigItem()
        {
            InitializeComponent();
        }
        #endregion End Forms

        #region Controls In Form
        private void CbFontSize_SelectedIndexChanged(object? sender, EventArgs e)
        {
            preferFontByLine[lineIndex] = cbFontSize.SelectedIndex;
        }
        private void CbColor_SelectedIndexChanged(object? sender, EventArgs e)
        {
            preferColorByLine[lineIndex] = cbColor.SelectedIndex;
        }
        #endregion End Controls In Form

        #region Public Function
        public void UpdateLineName(int lineIndex)
        {
            lblLineName.Text = "Dòng: " + lineIndex.ToString();
            this.lineIndex = lineIndex;

            cbDisplayMode.SelectedIndex = 0;
            if (preferFontByLine.ContainsKey(lineIndex))
            {
                cbFontSize.SelectedIndex = preferFontByLine[lineIndex];
            }
            else
            {
                cbFontSize.SelectedIndex = 0;
                preferFontByLine.Add(lineIndex, 0);
            }

            if (preferColorByLine.ContainsKey(lineIndex))
            {
                cbColor.SelectedIndex = preferColorByLine[lineIndex];
            }
            else
            {
                cbColor.SelectedIndex = 0;
                preferColorByLine.Add(lineIndex, 0);
            }

            cbColor.SelectedIndexChanged += CbColor_SelectedIndexChanged;
            cbFontSize.SelectedIndexChanged += CbFontSize_SelectedIndexChanged;

        }
        public void LoadOldConfig(int displayValueIndex, EmLedColor color, EmFontSize fontsizeIndex, string freeText)
        {
            cbDisplayMode.SelectedIndex = displayValueIndex;
            txtDisplaytext.Text = freeText;
            switch (fontsizeIndex)
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
            switch (color)
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
        }
        #endregion End Public Function

        private void cbDisplayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDisplaytext.Visible = cbDisplayMode.SelectedIndex == cbDisplayMode.Items.Count - 1;
            if (!txtDisplaytext.Visible)
            {
                txtDisplaytext.Text = "";
            }
        }
    }
}
