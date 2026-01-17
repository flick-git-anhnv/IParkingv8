using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskOut.ConfirmPlatePresenter;

namespace Kztek.Control8.UserControls.DialogUcs.KioskOut
{
    public partial class ucConfirmPlateMonthlyKioskOut : UserControl, IConfirmPlateMonthlyView
    {
        private readonly Image? defaultImage = null;

        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }

        public ucConfirmPlateMonthlyKioskOut(Image? defaultImage)
        {
            InitializeComponent();
            this.defaultImage = defaultImage;
        }

        public void ClearView()
        {
            this.Invoke(this.SuspendLayout);
            picVehicleIn.SetImage(defaultImage);
            picVehicleOut.SetImage(defaultImage);

            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(KzDictionary.EMPTY_STRING);
            lblPlateIn.SetText(KzDictionary.EMPTY_STRING);
            lblRegisterPlate.SetText(KzDictionary.EMPTY_STRING);

            lblVehicleType.SetText(KzDictionary.EMPTY_STRING);
            lblTimeOut.SetText(KzDictionary.EMPTY_STRING);
            lblPlateOut.SetText(KzDictionary.EMPTY_STRING);
            lblCustomer.SetText(KzDictionary.EMPTY_STRING);
            this.Invoke(this.ResumeLayout);
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
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
        }

        public void DisplayEventInfo(ExitData exitData, Image? displayImage, string errorMessage, Control parent)
        {
            ClearView();
            DisplayEventInfoInternal(exitData, displayImage, errorMessage);
            CenterControlLocation();
            ShowDialog();
        }
        private void DisplayEventInfoInternal(ExitData exitData, Image? displayImage, string errorMessage)
        {
            this.Invoke(this.SuspendLayout);
            lblTitle.SetText(errorMessage);

            lblAccessKeyName.SetText(exitData.AccessKey?.Name ?? KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(exitData.Entry!.DateTimeIn.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateIn.SetText(string.IsNullOrEmpty(exitData.Entry.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.Entry.PlateNumber);
            lblRegisterPlate.SetText(exitData.AccessKey?.GetVehicleInfo()?.Code ?? "");

            lblVehicleType.SetText(exitData.AccessKey?.Collection?.Name ?? "");
            lblTimeOut.SetText(exitData.DatetimeOut.ToString("dd/MM/yyyy HH:mm:ss"));
            lblPlateOut.SetText(string.IsNullOrEmpty(exitData.PlateNumber) ? KzDictionary.EMPTY_STRING : exitData.PlateNumber);
            lblCustomer.SetText(exitData.AccessKey?.GetVehicleInfo()?.Customer?.Name ?? KzDictionary.EMPTY_STRING);

            EventImageDto? panoramaImageData = exitData.Entry!.Images?.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
            EventImageDto? vehicleImageData = exitData.Entry.Images?.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
            if (!string.IsNullOrEmpty(vehicleImageData?.PresignedUrl))
            {
                picVehicleIn.Invoke(new Action(() =>
                {
                    picVehicleIn.Load(vehicleImageData.PresignedUrl);
                }));
            }

            picVehicleOut.SetImage(displayImage ?? defaultImage);
            this.Invoke(this.ResumeLayout);
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

        //TEXT DISPLAY
        public void SetTitleText(string text) => lblTitle.SetText(text);
        public void SetSubTitleText(string text) => lblSubTitle.SetText(text);

        public void SetAccessKeyNameTitleText(string text) => lblAccessKeyNameTitle.SetText(text);
        public void SetTimeInTitleText(string text) => lblTimeInTitle.SetText(text);
        public void SetPlateInTitleText(string text) => lblPlateInTitle.SetText(text);
        public void SetVehicleTypeTitleText(string text) => lblVehicleTypeTitle.SetText(text);
        public void SetTimeOutTitleText(string text) => lblTimeOutTitle.SetText(text);
        public void SetPlateOutTitleText(string text) => lblPlateOutTitle.SetText(text);

        public void SetBtnBackToMainText(string text) => btnBack.SetText(text);
        public void SetRegisterPlateTitleText(string text) => lblRegisterPlateTitle.SetText(text);
        public void SetCustomerTitleText(string text) => lblCustomerTitle.SetText(text);
    }
}
