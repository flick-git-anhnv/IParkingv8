using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Tool;

namespace Kztek.Control8.Forms
{
    public partial class UcSelectVouchers : UserControl, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<Voucher?>? tcs;
        private readonly UcConfirm ucConfirm;
        private LaneOptionalConfig LaneOptionalConfig;

        private readonly Guna2Elipse guna2Elipse = new();

        private readonly Form dialogHost = new Form();
        private MaskedUserControl masked;

        private UserControl _TargetControl;
        public UserControl TargetControl
        {
            get => _TargetControl;
            set
            {
                this._TargetControl = value;
                masked?.Dispose();
                masked = new MaskedUserControl(value);
            }
        }

        public int BorderRadius
        {
            get => guna2Elipse.BorderRadius;
            set
            {
                guna2Elipse.BorderRadius = value;
            }
        }
        #endregion

        #region Forms
        public UcSelectVouchers()
        {
            InitializeComponent();
            ucConfirm = new UcConfirm
            {
                TargetControl = this,
                BorderRadius = 24,
            };

            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;

            Translate();
            btnCancel.OnClickAsync += BtnCancel1_Click;
        }
        #endregion

        #region Controls In Form
        private async void Uc_onSelectVoucher(Voucher voucher)
        {
            try
            {
                if (tcs is null)
                {
                    return;
                }
                bool isConfirm = await ucConfirm.IsConfirmAsync(KZUIStyles.CurrentResources.ConfirmApplyVoucher, this.LaneOptionalConfig);
                if (!isConfirm)
                {
                    return;
                }
                tcs.TrySetResult(voucher);
            }
            catch (Exception ex)
            {
                SystemLog.CreateApplicationProccess("Hiển thị thông báo xác nhận voucher lỗi", ex);
            }
        }
        private async Task<bool> BtnCancel1_Click(object? sender)
        {
            tcs?.TrySetResult(null);
            return true;
        }
        #endregion

        public async Task<Voucher?> SelectVoucherAsync(List<Voucher> vouchers, LaneOptionalConfig laneOptionalConfig)
        {
            this.LaneOptionalConfig = laneOptionalConfig;

            foreach (var control in flowLayoutPanel1.Controls.OfType<UcVoucher>())
            {
                control.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            foreach (var voucher in vouchers)
            {
                UcVoucher ucVoucher = new(voucher)
                {
                    Margin = new Padding(8)
                };
                flowLayoutPanel1.Controls.Add(ucVoucher);
                ucVoucher.OnSelectedVoucherEvent += Uc_onSelectVoucher;
            }

            Translate();

            dialogHost.Width = MathTool.GetMin(dialogHost.Width, masked.Width * 95 / 100, masked.Width);
            dialogHost.Height = MathTool.GetMin(dialogHost.Height, masked.Height * 95 / 100, masked.Height);
            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();

            tcs = new TaskCompletionSource<Voucher?>();
            var result = await tcs.Task;

            dialogHost.Hide();
            masked.Hide();
            return result;
        }

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            lblTitle.Text = KZUIStyles.CurrentResources.VoucherList;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
        }
    }
}
