using iParkingv5.LedDisplay.Enums;
using iParkingv5.LedDisplay.LEDs;

namespace Kztek.Control8.UserControls.ConfigUcs
{
    public delegate void OnDeleteItemClick(object sender);
    public partial class ucLedLineConfig : UserControl
    {
        #region Properties
        private int order;
        private int numOfLine = 0;
        public int Order
        {
            get => order;
            set
            {
                this.order = value;
                if (lblStepName != null)
                {
                    lblStepName.Text = "Bước " + order;
                }
            }
        }
        public event OnDeleteItemClick? OnDeleteItemEvent;
        private List<ucLedLineConfigItem> ucLedLineConfigItems = new List<ucLedLineConfigItem>();
        #endregion End Properties

        #region Forms
        public ucLedLineConfig(int numOfLine, int order)
        {
            InitializeComponent();
            for (int i = 0; i < numOfLine; i++)
            {
                ucLedLineConfigItem item = new ucLedLineConfigItem();
                item.UpdateLineName(i + 1);
                this.Controls.Add(item);
                item.Dock = DockStyle.Top;

                ucLedLineConfigItems.Add(item);
                item.BringToFront();
            }
            this.order = order;
            lblStepName.Text = "Bước " + order;
            btnCancel1.Click += picDelete_Click;
        }
        #endregion End Forms

        #region Controls In Form
        private void picDelete_Click(object sender, EventArgs e)
        {
            OnDeleteItemEvent?.Invoke(this);
        }
        #endregion End Controls In Form

        #region Public Function
        public DisplayStepDetail GetConfig()
        {
            DisplayStepDetail displayStepDetail = new DisplayStepDetail();
            for (int i = 0; i < ucLedLineConfigItems.Count; i++)
            {
                displayStepDetail.DelayTime = (int)numDelayTime.Value;
                displayStepDetail.DisplayDatas.Add(ucLedLineConfigItems[i].lineIndex, new LineConfig()
                {
                    DisplayColor = ucLedLineConfigItems[i].LedColor,
                    FontSize = (int)ucLedLineConfigItems[i].LedFontSize,
                    DisplayValue = ucLedLineConfigItems[i].LedDisplayValue,
                    Data = ucLedLineConfigItems[i].FreeText,
                });
            }
            return displayStepDetail;
        }
        public void LoadOldConfig(DisplayStepDetail displayStepDetail)
        {
            numDelayTime.Value = displayStepDetail.DelayTime;
            for (int i = 0; i < ucLedLineConfigItems.Count; i++)
            {
                if (displayStepDetail.DisplayDatas.ContainsKey(ucLedLineConfigItems[i].lineIndex))
                {
                    LineConfig config = displayStepDetail.DisplayDatas[ucLedLineConfigItems[i].lineIndex];
                    ucLedLineConfigItems[i].LoadOldConfig((int)config.DisplayValue, config.DisplayColor, (EmFontSize)config.FontSize, config.Data);
                }
            }
        }
        #endregion End Public Function

    }
}
