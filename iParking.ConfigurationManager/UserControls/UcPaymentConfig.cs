using iParkingv8.Object.Objects.Payments;
using IParkingv8.QRScreenController;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcPaymentConfig : UserControl
    {
        public UcPaymentConfig(PaymentKioskConfig? paymentKioskConfig)
        {
            InitializeComponent();

            cbQRSupply.Items.Clear();
            foreach (EmQRProvider item in Enum.GetValues(typeof(EmQRProvider)))
            {
                cbQRSupply.Items.Add(item.ToString());
            }
           
            if (paymentKioskConfig != null)
            {
                txtCashCOM.Text = paymentKioskConfig.CashComport;
                txtVisaDeviceId.Text = paymentKioskConfig.VisaId;
                txtQRDeviceId.Text = paymentKioskConfig.QRPosId;

                int selectedQRProvider = (int)paymentKioskConfig.QRProvider >= cbQRSupply.Items.Count ? -1 : (int)paymentKioskConfig.QRProvider;
                cbQRSupply.SelectedIndex = selectedQRProvider;
                chbIsUseCash.Checked = paymentKioskConfig.IsUseCash;
                chbIsUseQR.Checked = paymentKioskConfig.IsUseQR;
                chbIsUseVisa.Checked = paymentKioskConfig.IsUseVisa;

                txtVoucherComport.Text = paymentKioskConfig.VoucherComport;
                numVoucherBaudrate.Value = paymentKioskConfig.VoucherBaudrate;

                int selectedVoucherDeviceType = paymentKioskConfig.VoucherDeviceType >= cbVoucherDeviceType.Items.Count ? -1 : paymentKioskConfig.VoucherDeviceType;
                cbVoucherDeviceType.SelectedIndex = selectedVoucherDeviceType;
                chbIsUseVoucher.Checked = paymentKioskConfig.IsUseVoucher;
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
    }
}
