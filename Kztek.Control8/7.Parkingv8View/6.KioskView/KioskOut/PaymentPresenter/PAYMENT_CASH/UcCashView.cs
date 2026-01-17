using Guna.UI2.WinForms;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskBase;
using Kztek.Control8.KioskOut.PaymentPresenter.CASH;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcCashView : UserControl, ICashView
    {
        Guna2Elipse guna2Elipse = new Guna2Elipse();

        private readonly Form dialogHost = new();
        private MaskedUserControl? masked;

        private UserControl? _TargetControl;

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

        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }
        private readonly UcLoading loadingView;

        public UcCashView(UserControl parent)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.Visible = false;
            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;

            loadingView = new UcLoading
            {
                TargetControl = parent,
                BorderRadius = 24
            };
        }

        public void ShowLoading(string title, string subTitle)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    loadingView.ShowLoadingIndicator(title, subTitle);
                }));
                return;
            }
            loadingView.ShowLoadingIndicator(title, subTitle);
        }
        public void UpdateLoadingMessage(string title, string subTitle)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    loadingView.UpdateLoadingIndicator(title, subTitle);
                }));
                return;
            }
            loadingView.UpdateLoadingIndicator(title, subTitle);
        }
        public void HideLoadingIndicator()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    loadingView.HideLoadingIndicator();
                }));
                return;
            }
            loadingView.HideLoadingIndicator();
        }

        public void ClearView()
        {
            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(KzDictionary.EMPTY_STRING);
            lblPlateIn.SetText(KzDictionary.EMPTY_STRING);
            lblVehicleType.SetText(KzDictionary.EMPTY_STRING);
            lblTimeOut.SetText(KzDictionary.EMPTY_STRING);
            lblPlateOut.SetText(KzDictionary.EMPTY_STRING);
            lblFee.SetText(KzDictionary.EMPTY_STRING);
            lblPaid.SetText(KzDictionary.EMPTY_STRING);
            lblRemain.SetText(KzDictionary.EMPTY_STRING);
        }
        public void HideView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Visible = false;
                    dialogHost.Hide();
                    masked.Hide();
                }));
                return;
            }
            this.Visible = false;
            dialogHost.Hide();
            masked.Hide();
        }
        public void ShowView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Visible = true;
                    dialogHost.Location = new Point(
                            this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                            this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                        );
                    dialogHost.Show(this.masked);
                    masked.Show();
                }));
                return;
            }
            this.Visible = true;
            dialogHost.Location = new Point(
                            this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                            this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                        );
            dialogHost.Show(this.masked);
            masked.Show();
        }

        public void DisplayEventInfo(ExitData exitData)
        {
            lblAccessKeyName.SetText(exitData.AccessKey?.Name ?? KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(exitData.Entry!.DateTimeIn.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateIn.SetText(string.IsNullOrEmpty(exitData.Entry.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.Entry.PlateNumber);

            lblVehicleType.SetText(exitData.AccessKey?.Collection?.Name ?? KzDictionary.EMPTY_STRING);
            lblTimeOut.SetText(exitData.DatetimeOut.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateOut.SetText(string.IsNullOrEmpty(exitData.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.PlateNumber);

            lblFee.SetText(exitData.Amount.ToString("N0"));

            long paid = exitData.DiscountAmount + exitData.Entry.Amount;
            lblPaid.SetText(Math.Min(paid, exitData.Amount).ToString("N0"));

            long remain = exitData.Amount - (exitData.DiscountAmount + exitData.Entry.Amount);
            lblRemain.SetText(Math.Max(remain, 0).ToString("N0"));
        }

        // THÊM: Triển khai các phương thức Set...Text từ ICashView
        public void SetInfoText(string text) => lblInfor.SetText(text);

        public void SetTitleText(string text) => lblTitle.SetText(text);
        public void SetSubTitleText(string text) => lblSubTitle.SetText(text);

        public void SetAccessKeyNameTitleText(string text) => lblAccessKeyNameTitle.SetText(text);
        public void SetTimeInTitleText(string text) => lblTimeInTitle.SetText(text);
        public void SetPlateInTitleText(string text) => lblPlateInTitle.SetText(text);
        public void SetVehicleTypeTitleText(string text) => lblVehicleTypeTitle.SetText(text);
        public void SetTimeOutTitleText(string text) => lblTimeOutTitle.SetText(text);
        public void SetPlateOutTitleText(string text) => lblPlateOutTitle.SetText(text);

        public void SetFeeTitleText(string text) => lblFeeTitle.SetText(text);
        public void SetPaidTitleText(string text) => lblPaidTitle.SetText(text);
        public void SetRemainTitleText(string text) => lblRemainTitle.SetText(text);
        public void SetBtnBackText(string text) => btnBack.SetText(text);
    }
}
