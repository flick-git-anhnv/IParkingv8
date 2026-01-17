using iParking.ConfigurationManager.Views.Interfaces;
using iParkingv8.Object.Objects.Payments;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcPaymentConfig : UserControl, IPaymentConfigView
    {
        public UcPaymentConfig()
        {
            InitializeComponent();

            cbQRSupply.Items.Clear();
            foreach (EmQRProvider item in Enum.GetValues(typeof(EmQRProvider)))
            {
                cbQRSupply.Items.Add(item.ToString());
            }
        }

        public PaymentKioskConfig GetConfig()
        {
            return new PaymentKioskConfig
            {
                CashComport = txtCashCOM.Text.Trim(),
                VisaId = txtVisaDeviceId.Text.Trim(),
                QRPosId = txtQRDeviceId.Text.Trim(),
                QRProvider = (EmQRProvider)cbQRSupply.SelectedIndex,

                IsUseCash = chbIsUseCash.Checked,
                IsUseQR = chbIsUseQR.Checked,
                IsUseVisa = chbIsUseVisa.Checked,

                VoucherComport = txtVoucherComport.Text.Trim(),
                VoucherBaudrate = (int)numVoucherBaudrate.Value,
                VoucherDeviceType = (int)cbVoucherDeviceType.SelectedIndex,
                IsUseVoucher = chbIsUseVoucher.Checked,
            };
        }
        public void SetConfig(PaymentKioskConfig? config)
        {
            if (config == null)
            {
                return;
            }
            txtCashCOM.Text = config.CashComport;
            txtVisaDeviceId.Text = config.VisaId;
            txtQRDeviceId.Text = config.QRPosId;

            int selectedQRProvider = (int)config.QRProvider >= cbQRSupply.Items.Count ? -1 : (int)config.QRProvider;
            cbQRSupply.SelectedIndex = selectedQRProvider;
            chbIsUseCash.Checked = config.IsUseCash;
            chbIsUseQR.Checked = config.IsUseQR;
            chbIsUseVisa.Checked = config.IsUseVisa;

            txtVoucherComport.Text = config.VoucherComport;
            numVoucherBaudrate.Value = config.VoucherBaudrate;

            int selectedVoucherDeviceType = config.VoucherDeviceType >= cbVoucherDeviceType.Items.Count ? -1 : config.VoucherDeviceType;
            cbVoucherDeviceType.SelectedIndex = selectedVoucherDeviceType;
            chbIsUseVoucher.Checked = config.IsUseVoucher;
        }
    }
}
