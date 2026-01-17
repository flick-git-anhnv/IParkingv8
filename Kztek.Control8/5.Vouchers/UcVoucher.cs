using iParkingv8.Object.Objects.Payments;

namespace Kztek.Control8.UserControls
{
    public partial class UcVoucher : UserControl
    {
        #region Properties
        public delegate void OnSelectVoucherEventHandler(Voucher voucher);
        public event OnSelectVoucherEventHandler? OnSelectedVoucherEvent;
        public Voucher voucher;
        #endregion

        #region Forms
        public UcVoucher(Voucher voucher)
        {
            InitializeComponent();
            this.voucher = voucher;
            btnName.Text = voucher.Name;
            btnName.Click += OnClickEventHandler;
        }
        #endregion

        #region Controls In Form
        private void OnClickEventHandler(object? sender, EventArgs e)
        {
            OnSelectedVoucherEvent?.Invoke(this.voucher);
        }
        #endregion
    }
}
