using ILedv8.Objects;
using iParkingv8.Ultility;
using Kztek.Tool;

namespace ILedv8.UserControls
{
    public partial class ucAppOption : UserControl
    {
        public ucAppOption()
        {
            InitializeComponent();

            var delayConfig = NewtonSoftHelper<DelayConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledDelayConfig()) ?? new DelayConfig();

            numSendToLedRowDuration.Value = delayConfig.SendToLedRowDuration;
            numCheckCountDuration.Value = delayConfig.CheckCountDuration;
        }

        public DelayConfig GetDelayConfig()
        {
            return new DelayConfig
            {
                SendToLedRowDuration = (int)numSendToLedRowDuration.Value,
                CheckCountDuration = (int)numCheckCountDuration.Value
            };
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            NewtonSoftHelper<DelayConfig>.SaveConfig(GetDelayConfig(), IparkingingPathManagement.ledDelayConfig());
        }
    }
}
