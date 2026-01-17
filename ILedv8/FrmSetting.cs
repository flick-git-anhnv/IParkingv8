using ILedv8.UserControls;
using iParking.ConfigurationManager.UserControls;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
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

namespace ILedv8
{
    public partial class FrmSetting : Form
    {
        #region Properties
        ucLedLaneConfig ucLedLaneConfig;
        ucLedDisplayConfig? ucLedDisplayConfig;
        ucAppOption? ucAppOption;
        #endregion

        #region Forms
        public FrmSetting()
        {
            InitializeComponent();
            this.Load += FrmConnectionConfig_Load;
        }
        private void FrmConnectionConfig_Load(object? sender, EventArgs e)
        {
            AddTabDisplayConfig();
            AddTabLaneConfig();
            AddTabDelayConfig();
        }
        #endregion

        #region Private Function
        private void AddTabDelayConfig()
        {
            TabPage tabOption = new()
            {
                Text = "Tùy Chọn"
            };
            this.tabOption.TabPages.Add(tabOption);
            tabOption.BackColor = SystemColors.ButtonHighlight;

            ucAppOption = new ucAppOption();
            tabOption.Controls.Add(ucAppOption);
            ucAppOption.Dock = DockStyle.Fill;
            tabOption.AutoScroll = true;
        }

        private void AddTabDisplayConfig()
        {
            TabPage tabServerConfig = new()
            {
                Text = "Cấu hình hiển thị"
            };
            tabOption.TabPages.Add(tabServerConfig);
            tabServerConfig.BackColor = SystemColors.ButtonHighlight;

            ucLedDisplayConfig = new ucLedDisplayConfig();
            tabServerConfig.Controls.Add(ucLedDisplayConfig);
            ucLedDisplayConfig.Dock = DockStyle.Fill;
            tabServerConfig.AutoScroll = true;
        }
        private void AddTabLaneConfig()
        {
            TabPage tabLedLane = new()
            {
                Text = "Cấu hình LED - Làn"
            };
            tabOption.TabPages.Add(tabLedLane);
            tabLedLane.BackColor = SystemColors.ButtonHighlight;

            ucLedLaneConfig = new ucLedLaneConfig();
            tabLedLane.Controls.Add(ucLedLaneConfig);
            ucLedLaneConfig.Dock = DockStyle.Fill;
            tabLedLane.AutoScroll = true;
        }
        #endregion End Private Function
    }
}