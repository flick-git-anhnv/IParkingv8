using ILedv8.Objects;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;
using System.Data;

namespace ILedv8.UserControls
{
    public partial class ucLedLaneConfig : UserControl
    {
        public ucLedLaneConfig()
        {
            InitializeComponent();
            foreach (var item in AppData.Leds)
            {
                ListItem li = new ListItem()
                {
                    Name = item.id,
                    Value = item.name,
                };
                cbLeds.Items.Add(li);
            }

            cbLeds.DisplayMember = "Value";
            cbLeds.ValueMember = "Name";

            cbLeds.SelectedIndexChanged += CbLeds_SelectedIndexChanged;
            btnConfirm.Click += BtnConfirm_Click;

            if (cbLeds.Items.Count > 0)
            {
                cbLeds.SelectedIndex = 0;
            }
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (panelConfig.Controls.Count == 0)
            {
                return;
            }
            foreach (ucLedLaneConfigItem item in panelConfig.Controls)
            {
                var config = item.GetConfig();
                NewtonSoftHelper<LedLaneConfig>.SaveConfig(config, IparkingingPathManagement.ledLaneConfig(item.led.comport, item.line));
            }
            MessageBox.Show("Cấu hình đã được lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnCancel_Click(object? sender, EventArgs e)
        {
        }

        private void CbLeds_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnConfirm.PerformClick();

            panelConfig.Controls.Clear();

            ListItem? item = (ListItem)cbLeds.SelectedItem;
            if (item == null)
            {
                return;
            }

            Led? led = AppData.Leds.Where(e => e.id == item.Name).FirstOrDefault();
            if (led == null)
            {
                return;
            }

            for (int i = 0; i < led.row; i++)
            {
                ucLedLaneConfigItem uc = new ucLedLaneConfigItem(led, i + 1);
                panelConfig.Controls.Add(uc);
                uc.Dock = DockStyle.Top;
                uc.BringToFront();
            }
        }
    }
}
