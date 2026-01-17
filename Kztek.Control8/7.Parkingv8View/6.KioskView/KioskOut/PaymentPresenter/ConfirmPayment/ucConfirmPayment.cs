using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskOut.PaymentPresenter.ConfirmPayment;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class UcConfirmPayment : UserControl, IKioskOutConfirmPaymentView
    {
        private readonly Image? defaultImage = null;
        private ExitData? exitData;
        private TaskCompletionSource<bool>? tcs;
        private EmKioskUserType kioskUserType = EmKioskUserType.Customer;

        public event EventHandler OnBackClicked
        {
            add { btnBackToMain.Click += value; }
            remove { btnBackToMain.Click -= value; }
        }

        public event EventHandler OnPaymentClicked
        {
            add { btnPayment.Click += value; }
            remove { btnPayment.Click -= value; }
        }

        public UcConfirmPayment(Image? defaultImage)
        {
            InitializeComponent();
            this.defaultImage = defaultImage;
        }

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
        }

        public void ClearView()
        {
            picVehicleIn.SetImage(defaultImage);
            picVehicleOut.SetImage(defaultImage);

            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(KzDictionary.EMPTY_STRING);
            lblPlateIn.SetText(KzDictionary.EMPTY_STRING);
            lblVehicleType.SetText(KzDictionary.EMPTY_STRING);
            lblTimeOut.SetText(KzDictionary.EMPTY_STRING);
            lblPlateOut.SetText(KzDictionary.EMPTY_STRING);
            lblFee.SetText(KzDictionary.EMPTY_STRING);
        }
        public void DisplayEventInfo(ExitData exitData, Image? displayImage)
        {
            ClearView();
            DisplayEventInforInternal(exitData, displayImage);
            CenterControlLocation();
            ShowDialog();
        }

        private void DisplayEventInforInternal(ExitData exitData, Image? displayImage)
        {
            lblAccessKeyName.SetText(exitData.AccessKey?.Name ?? KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(exitData.Entry!.DateTimeIn.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateIn.SetText(string.IsNullOrEmpty(exitData.Entry.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.Entry.PlateNumber);

            lblVehicleType.SetText(exitData.AccessKey?.Collection?.Name ?? KzDictionary.EMPTY_STRING);
            lblTimeOut.SetText(exitData.DatetimeOut.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateOut.SetText(string.IsNullOrEmpty(exitData.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.PlateNumber);

            EventImageDto? panoramaImageData = exitData.Entry!.Images?.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
            EventImageDto? vehicleImageData = exitData.Entry.Images?.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
            if (!string.IsNullOrEmpty(vehicleImageData?.PresignedUrl))
            {
                picVehicleIn.Invoke(new Action(() =>
                {
                    try
                    {
                        picVehicleIn.Load(vehicleImageData.PresignedUrl);
                    }
                    catch (Exception)
                    {
                    }
                }));
            }

            picVehicleOut.SetImage(displayImage);

            long amount = exitData.Amount - exitData.DiscountAmount - exitData.Entry.Amount;
            lblFee.SetText(Math.Max(0, amount).ToString("N0"));
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

        public void SetTitleText(string text) => lblTitle.SetText(text);

        public void SetAccessKeyNameTitleText(string text) => lblAccessKeyNameTitle.SetText(text);
        public void SetTimeInTitleText(string text) => lblTimeInTitle.SetText(text);
        public void SetPlateInTitleText(string text) => lblPlateInTitle.SetText(text);
        public void SetVehicleTypeTitleText(string text) => lblVehicleTypeTitle.SetText(text);
        public void SetTimeOutTitleText(string text) => lblTimeOutTitle.SetText(text);
        public void SetPlateOutTitleText(string text) => lblPlateOutTitle.SetText(text);

        public void SetFeeTitleText(string text) => lblFeeTitle.SetText(text);

        public void SetBtnBackToMainText(string text) => btnBackToMain.SetText(text);
        public void SetBtnPaymentText(string text) => btnPayment.SetText(text);
    }
}
