using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Ultility;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs.ShortcutConfigs
{
    public partial class ucLaneInShortcutConfig : UserControl
    {
        #region Properties
        private string laneId;
        public LaneInShortcutConfig? ShortcutConfig { get; set; }
        #endregion End Properties

        #region Forms
        public ucLaneInShortcutConfig(string laneId)
        {
            InitializeComponent();
            this.laneId = laneId;
            this.Load += UcLaneInShortcutConfig_Load;
        }

        private void UcLaneInShortcutConfig_Load(object? sender, EventArgs e)
        {
            this.ShortcutConfig = NewtonSoftHelper<LaneInShortcutConfig>.DeserializeObjectFromPath(IparkingingPathManagement.laneShortcutConfigPath(laneId))
                                        ?? new LaneInShortcutConfig();

            if ((Keys)ShortcutConfig.ConfirmPlateKey != Keys.None)
            {
                lblConfirmPlateKey.Text = ((Keys)ShortcutConfig.ConfirmPlateKey).ToString();
            }
            else
            {
                lblConfirmPlateKey.Text = "Hãy cấu hình phím tắt";
            }

            if ((Keys)ShortcutConfig.WriteIn != Keys.None)
            {
                lblWriteInKey.Text = ((Keys)ShortcutConfig.WriteIn).ToString();
            }
            else
            {
                lblWriteInKey.Text = "Hãy cấu hình phím tắt";
            }

            if ((Keys)ShortcutConfig.ReserveLane != Keys.None)
            {
                lblReserveLaneKey.Text = ((Keys)ShortcutConfig.ReserveLane).ToString();
            }
            else
            {
                lblReserveLaneKey.Text = "Hãy cấu hình phím tắt";
            }

            if ((Keys)ShortcutConfig.ReSnapshotKey != Keys.None)
            {
                lblReSnapshotLaneKey.Text = ((Keys)ShortcutConfig.ReSnapshotKey).ToString();
            }
            else
            {
                lblReSnapshotLaneKey.Text = "Hãy cấu hình phím tắt";
            }

            picChangeConfirmPlateKey.Click +=picChangeConfirmPlateKey_Click;
            btnSetting1.Click +=picChangeWriteInKey_Click;
            picReserveLane.Click +=picChangeReserverLane_Click;
            picReSnapshot.Click +=PicReSnapshot_Click;
        }


        #endregion End Forms

        #region Controls In Form

        private void picChangeConfirmPlateKey_Click(object? sender, EventArgs e)
        {
            frmSetShortCutKey frm = new frmSetShortCutKey(Keys.Enter);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShortcutConfig!.ConfirmPlateKey = (int)frm.KeySet;
                if (frm.KeySet != Keys.None)
                {
                    lblConfirmPlateKey.Text = ((Keys)ShortcutConfig.ConfirmPlateKey).ToString();
                }
                else
                {
                    lblConfirmPlateKey.Text = "Hãy cấu hình phím tắt";
                }
            }
            frm.Dispose();
        }
        private void picChangeWriteInKey_Click(object? sender, EventArgs e)
        {
            frmSetShortCutKey frm = new frmSetShortCutKey(Keys.Enter);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShortcutConfig!.WriteIn = (int)frm.KeySet;


                if (frm.KeySet != Keys.None)
                {
                    lblWriteInKey.Text = ((Keys)ShortcutConfig.WriteIn).ToString();
                }
                else
                {
                    lblWriteInKey.Text = "Hãy cấu hình phím tắt";
                }
            }
        }
        private void picChangeReserverLane_Click(object? sender, EventArgs e)
        {
            frmSetShortCutKey frm = new frmSetShortCutKey(Keys.Enter);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShortcutConfig!.ReserveLane = (int)frm.KeySet;
                if (frm.KeySet != Keys.None)
                {
                    lblReserveLaneKey.Text = ((Keys)ShortcutConfig.ReserveLane).ToString();
                }
                else
                {
                    lblReserveLaneKey.Text = "Hãy cấu hình phím tắt";
                }
            }
        }
        private void PicReSnapshot_Click(object? sender, EventArgs e)
        {
            frmSetShortCutKey frm = new frmSetShortCutKey(Keys.Enter);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShortcutConfig!.ReSnapshotKey = (int)frm.KeySet;
                if (frm.KeySet != Keys.None)
                {
                    lblReSnapshotLaneKey.Text = ((Keys)ShortcutConfig.ReSnapshotKey).ToString();
                }
                else
                {
                    lblReSnapshotLaneKey.Text = "Hãy cấu hình phím tắt";
                }
            }
            frm.Dispose();
        }
        #endregion End Controls In Form
    }
}
