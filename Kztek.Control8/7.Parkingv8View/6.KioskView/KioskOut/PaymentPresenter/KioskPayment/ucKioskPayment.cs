using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskBase;
using Kztek.Control8.KioskOut.PaymentPresenter.KioskPayment;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class ucKioskPayment : UserControl, IKioskOutPaymentView
    {
        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }
        public event EventHandler OnCreateQRClicked
        {
            add
            {
                btnQR.Click += value;
                picQR.Click += value;
                panelQR.Click += value;
            }
            remove
            {
                btnQR.Click -= value;
                picQR.Click -= value;
                panelQR.Click -= value;
            }
        }
        public event EventHandler OnStartCashClick
        {
            add
            {
                btnCash.Click += value;
                picCash.Click += value;
                panelCash.Click += value;
            }
            remove
            {
                btnCash.Click -= value;
                picCash.Click -= value;
                panelCash.Click -= value;
            }
        }
        public event EventHandler OnStartVoucherClick
        {
            add
            {
                btnVoucher.Click += value;
                picVoucher.Click += value;
                panelVoucher.Click += value;
            }
            remove
            {
                btnVoucher.Click -= value;
                picVoucher.Click -= value;
                panelVoucher.Click -= value;
            }
        }

        private readonly UcLoading loadingView;
        private readonly UcConfirmKiosk ucNotify;

        public ucKioskPayment(UserControl parent)
        {
            InitializeComponent();
            this.ucNotify = new UcConfirmKiosk()
            {
                TargetControl = parent,
                BorderRadius = 24
            };
            loadingView = new UcLoading
            {
                TargetControl = parent,
                BorderRadius = 24
            };
        }
        #region KIOSK OUT PAYMENT

        public void CenterControlLocation()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CenterControlLocationInternal();
                }));
                return;
            }
            CenterControlLocationInternal();
        }
        public void CenterControlLocationInternal()
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
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

        public void DisplayEventInfo(ExitData exitData)
        {
            ClearView();
            DisplayEventInforInternal(exitData);
            CenterControlLocation();
            ShowDialog();
        }
        private void DisplayEventInforInternal(ExitData exitData)
        {
            lblAccessKeyName.SetText(exitData.AccessKey?.Name ?? "");
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

            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                {
                    btnCash.Enabled = picCash.Enabled = picCash.Enabled = remain > 0;
                    btnVoucher.Enabled = picVoucher.Enabled = panelVoucher.Enabled = remain > 0;
                    btnQR.Enabled = picQR.Enabled = panelQR.Enabled = remain > 0;
                }));
            }
            else
            {
                btnCash.Enabled = picCash.Enabled = picCash.Enabled = remain > 0;
                btnVoucher.Enabled = picVoucher.Enabled = panelVoucher.Enabled = remain > 0;
                btnQR.Enabled = picQR.Enabled = panelQR.Enabled = remain > 0;
            }

        }

        public void ShowDialog()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    ShowDialogInternal();
                }));
                return;
            }
            ShowDialogInternal();
        }
        private void ShowDialogInternal()
        {
            this.Visible = true;
        }

        public void CloseDialog()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CloseDialogInternal();
                }));
                return;
            }
            CloseDialogInternal();
        }
        private void CloseDialogInternal()
        {
            this.Visible = false;
        }

        public void ShowErrorMessage(string titleTag, string subTitleTag, string backTitleTag)
        {
            var request = new KioskDialogRequest(titleTag, subTitleTag, backTitleTag, EmImageDialogType.Error, null);
            this.ucNotify.Show(request);
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
        public void ShowCompletedPayment()
        {

        }

        public void AllowQRPayment(bool isAllow) => btnQR.Enabled = isAllow;
        public void AllowVoucherPayment(bool isAllow) => btnVoucher.Enabled = isAllow;
        public void AllowCashPayment(bool isAllow) => btnCash.Enabled = isAllow;

        public void ActiveCashMode()
        {
            panelCash.BorderColor = Color.FromArgb(242, 102, 51);
            panelVoucher.BorderColor = default;
            panelQR.BorderColor = default;
            //panelVisa.BorderColor = default;
        }
        public void ActiveVoucherMode()
        {
            panelCash.BorderColor = default;
            panelVoucher.BorderColor = Color.FromArgb(242, 102, 51);
            panelQR.BorderColor = default;
            //panelVisa.BorderColor = default;
        }
        public void ActiveQrMode()
        {
            panelCash.BorderColor = default;
            panelVoucher.BorderColor = default;
            panelQR.BorderColor = Color.FromArgb(242, 102, 51);
            //panelVisa.BorderColor = default;
        }
        public void DisablePaymentMode()
        {
            panelCash.BorderColor = default;
            panelVoucher.BorderColor = default;
            panelQR.BorderColor = default;
            //panelVisa.BorderColor = default;
        }

        #endregion

        private void picCash_Click(object sender, EventArgs e)
        {
        }
        public void SetTitleText(string text) => lblTitle.Text = text;

        public void SetAccessKeyNameTitleText(string text) => lblAccessKeyNameTitle.Text = text;
        public void SetTimeInTitleText(string text) => lblTimeInTitle.Text = text;
        public void SetPlateInTitleText(string text) => lblPlateInTitle.Text = text;
        public void SetVehicleTypeTitleText(string text) => lblVehicleTypeTitle.Text = text;
        public void SetTimeOutTitleText(string text) => lblTimeOutTitle.Text = text;
        public void SetPlateOutTitleText(string text) => lblPlateOutTitle.Text = text;

        public void SetFeeTitleText(string text) => lblFeeTitle.Text = text;
        public void SetPaidTitleText(string text) => lblPaidTitle.Text = text;
        public void SetRemainTitleText(string text) => lblRemainTitle.Text = text;

        public void SetChoosePaymentMethodTitleText(string text) => lblChoosePaymentMethodTitle.Text = text;

        public void SetCashButtonText(string text) => btnCash.Text = text;
        public void SetVoucherButtonText(string text) => btnVoucher.Text = text;
        public void SetQRButtonText(string text) => btnQR.Text = text;

        public void SetBtnBackToMainText(string text) => btnBack.Text = text;
    }
}
