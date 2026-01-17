using ILedv8.Objects;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Tool;

namespace ILedv8.UserControls
{
    public partial class ucLedLaneConfigItem : UserControl
    {
        public Led led;
        public int line = 1;

        public ucLedLaneConfigItem(Led led, int line)
        {
            InitializeComponent();
            this.led = led;
            this.line = line;
            lblLineName.Text = "Dòng " + line;
            chlbLanes.DisplayMember = "Value";
            chlbLanes.ValueMember = "Name";

            foreach (var item in AppData.Lanes)
            {
                ListItem li = new ListItem();
                li.Name = item.Id;
                li.Value = item.Name;
                chlbLanes.Items.Add(li);
            }

            chlbVehicleType.Items.Add("Ô Tô");
            chlbVehicleType.Items.Add("Xe Máy");
            chlbVehicleType.Items.Add("Xe đạp");

            var config = NewtonSoftHelper<LedLaneConfig>.DeserializeObjectFromPath(IparkingingPathManagement.ledLaneConfig(led.comport, line));
            if (config != null)
            {
                numMaxVehicle.Value = (int)config.MaxNumber;
                for (int i = 0; i < chlbLanes.Items.Count; i++)
                {
                    ListItem item = (ListItem)chlbLanes.Items[i];
                    if (config.LaneIds.Contains(item.Name))
                    {
                        chlbLanes.SetItemChecked(i, true);
                    }
                    else
                    {
                        chlbLanes.SetItemChecked(i, false);
                    }
                }
                List<int> configVehicleType = new List<int>();
                foreach (var item in config.SupportVehicles)
                {
                    configVehicleType.Add((int)item);
                }
                for (int i = 0; i < chlbVehicleType.Items.Count; i++)
                {
                    if (configVehicleType.Contains(i))
                    {
                        chlbVehicleType.SetItemChecked(i, true);
                    }
                }
            }
        }

        public LedLaneConfig GetConfig()
        {
            LedLaneConfig config = new LedLaneConfig();
            config.MaxNumber = (int)numMaxVehicle.Value;
            config.LedId = led.id;
            config.Line = line;
            config.LaneIds = new List<string>();
            foreach (ListItem item in chlbLanes.CheckedItems)
            {
                config.LaneIds.Add(item.Name);
            }
            foreach (var v in chlbVehicleType.CheckedItems)
            {
                config.SupportVehicles.Add((EmVehicleType)chlbVehicleType.Items.IndexOf(v));
            }
            return config;
        }
    }
}
