using Guna.UI2.WinForms;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskOut.PaymentPresenter.CASH;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcVoucherView : UserControl, IVoucherView
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

        public UcVoucherView()
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

        public void ShowErrorMessage(string errorMessage)
        {
            lblErrorMessage.SetText(errorMessage);
        }

        public void DisplayEventInfo(ExitData exitData)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    DisplayEventInfoInternal(exitData);
                }));
            }
            else
                DisplayEventInfoInternal(exitData);
        }
        private void DisplayEventInfoInternal(ExitData exitData)
        {
            lblAccessKeyName.Text = exitData.AccessKey?.Name ?? "";
            lblTimeIn.Text = exitData.Entry!.DateTimeIn.ToString("dd/MM/yyyy HH:mm:ss");
            lblPlateIn.Text = string.IsNullOrEmpty(exitData.Entry.PlateNumber) ? "_" : exitData.Entry.PlateNumber;

            lblVehicleType.Text = exitData.AccessKey?.Collection?.Name ?? "";
            lblTimeOut.Text = exitData.DatetimeOut.ToString("dd/MM/yyyy HH:mm:ss");
            lblPlateOut.Text = string.IsNullOrEmpty(exitData.PlateNumber) ? "_" : exitData.PlateNumber;

            lblFee.Text = exitData.Amount.ToString("N0");

            long paid = exitData.DiscountAmount + exitData.Entry.Amount;
            lblPaid.Text = Math.Min(paid, exitData.Amount).ToString("N0");

            long remain = exitData.Amount - (exitData.DiscountAmount + exitData.Entry.Amount);
            lblRemain.Text = Math.Max(remain, 0).ToString("N0");
        }

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
