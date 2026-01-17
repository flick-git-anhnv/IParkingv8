using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using IParkingv8.QRScreenController;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.ConfigUcs.LaneConfigs
{
    public partial class UcLaneOptionalConfig : UserControl
    {
        private readonly string laneId;
        public UcLaneOptionalConfig(string laneId, LaneOptionalConfig config, List<Collection> collections)
        {
            InitializeComponent();
            this.laneId = laneId;
            int selectedIndex = -1;

            Translate();
            InitUI();

            foreach (EmStaticQRDevice item in Enum.GetValues(typeof(EmStaticQRDevice)))
            {
                cbStaticQRType.Items.Add(item.ToString());
            }
            cbStaticQRType.SelectedIndex = 0;
            foreach (EmNapasBankCode item in Enum.GetValues(typeof(EmNapasBankCode)))
            {
                cbStaticQRBank.Items.Add(item.ToString());
            }
            cbStaticQRBank.SelectedIndex = 0;

            cbAccessKeyCollection.DisplayMember = "Value";
            cbAccessKeyCollection.ValueMember = "Name";
            for (int i = 0; i < collections.Count; i++)
            {
                Collection? item = collections[i];
                ListItem li = new()
                {
                    Name = item.Id,
                    Value = item.Name,
                };
                cbAccessKeyCollection.Items.Add(li);
                if (config.TurnCollectionId.Equals(item.Id, StringComparison.CurrentCultureIgnoreCase))
                {
                    selectedIndex = i;
                }
            }

            chbIsRegisterTurnVehicle.Checked = config.IsRegisterTurnVehicle;
            chbIsUseLoopImageForCardEvent.Checked = config.IsUseLoopImageForCardEvent;
            if (cbAccessKeyCollection.Items.Count > selectedIndex)
            {
                cbAccessKeyCollection.SelectedIndex = selectedIndex;
            }

            //Hộp thoại cảnh báo
            chbIsUseAlarmDialog.Checked = config.IsUseAlarmDialog;
            txtAutoReturnDialogTime.Text = config.AutoRejectDialogTime.ToString();
            cbAutoReturnDialogResult.SelectedIndex = config.AutoRejectDialogResult ? 1 : 0;

            numStaticQRBaudrate.Value = config.StaticQRBaudrate;
            txtStaticQRComport.Text = config.StaticQRComport;
            cbStaticQRType.SelectedIndex = config.StaticQRType;
            txtStaticQRAccountNumber.Text = config.AccountNumber;
            cbStaticQRBank.SelectedIndex = cbStaticQRBank.FindStringExact(config.BankName);
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            NewtonSoftHelper<LaneOptionalConfig>.SaveConfig(GetConfig(), IparkingingPathManagement.laneOptionalConfig(this.laneId));
            MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public LaneOptionalConfig GetConfig()
        {
            _ = int.TryParse(txtAutoReturnDialogTime.Text, out int AutoReturnDialogTime);

            LaneOptionalConfig laneOptionalConfig = new()
            {
                IsRegisterTurnVehicle = chbIsRegisterTurnVehicle.Checked,
                TurnCollectionId = cbAccessKeyCollection.SelectedItem == null ? "" : ((ListItem)cbAccessKeyCollection.SelectedItem)?.Name ?? "",
                IsUseLoopImageForCardEvent = chbIsUseLoopImageForCardEvent.Checked,

                //Hộp thoại cảnh báo
                IsUseAlarmDialog = chbIsUseAlarmDialog.Checked,
                AutoRejectDialogTime = AutoReturnDialogTime,
                AutoRejectDialogResult = cbAutoReturnDialogResult.SelectedIndex == 1,

                StaticQRBaudrate = (int)numStaticQRBaudrate.Value,
                StaticQRComport = txtStaticQRComport.Text,
                StaticQRType = cbStaticQRType.SelectedIndex,
                AccountNumber = txtStaticQRAccountNumber.Text.Trim(),
                BankName = cbStaticQRBank.Text.Trim(),
            };
            return laneOptionalConfig;
        }

        public void Translate()
        {
            uiLineRegisterByPlateDaily.Text = KZUIStyles.CurrentResources.RegisterByPlateDaily;
            uiLineWarningDialog.Text = KZUIStyles.CurrentResources.WarningDialog;
            uiLineQRView.Text = KZUIStyles.CurrentResources.QRView;
            uiLineOther.Text = KZUIStyles.CurrentResources.Other;

            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
            chbIsRegisterTurnVehicle.Text = KZUIStyles.CurrentResources.RegisterByPlateDaily;

            lblAutoCloseWarningDialogAfterTitle.Text = KZUIStyles.CurrentResources.AutoCloseWarningAfter;
            lblAutoCloseResultTitle.Text = KZUIStyles.CurrentResources.AutoCloseResult;
            chbIsUseAlarmDialog.Text = KZUIStyles.CurrentResources.Use;

            lblComQRViewTitle.Text = KZUIStyles.CurrentResources.ComIp;
            lblBaudrateQRViewTitle.Text = KZUIStyles.CurrentResources.BaudratePort;
            lblTypeQRViewTitle.Text = KZUIStyles.CurrentResources.colType;
            lblAccountNumberTitle.Text = KZUIStyles.CurrentResources.AccountNumber;
            lblBankTitle.Text = KZUIStyles.CurrentResources.Bank;
            chbIsUseQRViewDevice.Text = KZUIStyles.CurrentResources.Use;

            chbIsUseLoopImageForCardEvent.Text = KZUIStyles.CurrentResources.AllowUseLoopImageForCardEvent;

            btnSave.Text = KZUIStyles.CurrentResources.Save;
        }
        private void InitUI()
        {
            cbAutoReturnDialogResult.Items.Clear();

            cbAutoReturnDialogResult.Items.Add(KZUIStyles.CurrentResources.Confirm);
            cbAutoReturnDialogResult.Items.Add(KZUIStyles.CurrentResources.Cancel);
        }
    }
}
