using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using Kztek.Control8.KioskIn.ConfirmOpenBarrie;

namespace Kztek.Control8.KioskIn
{
    public partial class ucConfirmOpenBarrieKioskInView : UserControl, IConfirmOpenBarrieKioskInView
    {
        private readonly Image? defaultImage = null;
        public event EventHandler OnBackClicked
        {
            add { btnBack.Click += value; }
            remove { btnBack.Click -= value; }
        }

        public ucConfirmOpenBarrieKioskInView(Image? defaultImage)
        {
            InitializeComponent();
            this.defaultImage = defaultImage;

            lblTitle.Anchor = AnchorStyles.Top;
            btnBack.Anchor = AnchorStyles.Bottom;
        }
        public ucConfirmOpenBarrieKioskInView() : this(null) { }

        public void ClearView()
        {
            this.Invoke(this.SuspendLayout);
            picVehicle.SetImage(defaultImage);
            picPanorama.SetImage(defaultImage);

            lblAccessKeyName.SetText(KzDictionary.EMPTY_STRING);
            lblVehicleType.SetText(KzDictionary.EMPTY_STRING);
            lblPlateIn.SetText(KzDictionary.EMPTY_STRING);
            lblAccessKeyCollection.SetText(KzDictionary.EMPTY_STRING);
            lblLane.SetText(KzDictionary.EMPTY_STRING);
            lblTimeIn.SetText(KzDictionary.EMPTY_STRING);
            this.Invoke(this.ResumeLayout);
        }
        public void DisplayEventInfor(EntryData entryData, AccessKey accessKey, Lane lane,
                                      Image? vehicleImage, Image? panoramaImage, string message)
        {
            ClearView();

            DisplayEventInforInternal(entryData, accessKey, lane, vehicleImage, panoramaImage, message);

            CenterControlLocation();

            ShowDialog();
        }
        private void DisplayEventInforInternal(EntryData entryData, AccessKey accessKey, Lane lane, Image? vehicleImage,
                                               Image? panoramaImage, string errorMessage)
        {
            this.Invoke(this.SuspendLayout);
            lblTitle.SetText(errorMessage);

            picVehicle.SetImage(vehicleImage ?? defaultImage);
            picPanorama.SetImage(panoramaImage ?? defaultImage);

            lblAccessKeyName.SetText(accessKey?.Name ?? KzDictionary.EMPTY_STRING);
            lblVehicleType.SetText(accessKey?.Collection?.GetVehicleTypeName() ?? KzDictionary.EMPTY_STRING);
            lblPlateIn.SetText(string.IsNullOrEmpty(entryData.PlateNumber) ? KzDictionary.EMPTY_STRING : entryData.PlateNumber);

            lblAccessKeyCollection.SetText(accessKey?.Collection?.Name ?? KzDictionary.EMPTY_STRING);
            lblLane.SetText(lane.Name);
            lblTimeIn.SetText(entryData.DateTimeIn.ToString("dd/MM/yyyy HH:mm:ss"));
            this.Invoke(this.ResumeLayout);
        }

        public void CenterControlLocation()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CenterControlLocation();
                }));
                return;
            }
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblSubTitle.Left = (this.ClientSize.Width - lblSubTitle.Width) / 2;
            btnBack.Left = (this.ClientSize.Width - btnBack.Width) / 2;
        }

        public void ShowDialog()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    ShowDialog();
                }));
                return;
            }
            this.Visible = true;
            this.BringToFront();
        }
        public void CloseDialog()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    CloseDialog();
                }));
                return;
            }
            this.Visible = false;
            this.SendToBack();
        }

        public void SetTitleText(string text) => lblTitle.SetText(text);
        public void SetSubTitleText(string text) => lblSubTitle.SetText(text);

        public void SetAccessKeyNameTitleText(string text) => lblAccessKeyNameTitle.SetText(text);
        public void SetTimeInTitleText(string text) => lblTimeInTitle.SetText(text);
        public void SetPlateInTitleText(string text) => lblPlateInTitle.SetText(text);
        public void SetVehicleTypeTitleText(string text) => lblVehicleTypeTitle.SetText(text);
        public void SetAccesskeyCollectionTitle(string text) => lblIdentityGroupTitle.SetText(text);
        public void SetLaneTitle(string text) => lblLaneTitle.SetText(text);

        public void SetBtnBackToMainText(string text) => btnBack.SetText(text);
    }
}

