using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcImageListInOutMerge : UserControl, IKZUIEventImageListOut
    {
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Display Title")]
        public bool KZUI_IsDisplayTitle
        {
            get => lblTitle.Visible;
            set { lblTitle.Visible = value; }
        }

        private bool isDisplayPanorama = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Panorama Pic"), Description("Display Title")]
        public bool KZUI_IsDisplayPanorama
        {
            get => isDisplayPanorama;
            set
            {
                isDisplayPanorama = value;
                ucPanoramaImageViewIn.Visible = value;
                ucPanoramaImageViewOut.Visible = value;
                kzuI_DirectionPanel1.RefreshUI();
                kzuI_DirectionPanel2.RefreshUI();
            }
        }

        private bool isDisplayVehicle = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Vehicle Pic"), Description("Display Title")]
        public bool KZUI_IsDisplayVehicle
        {
            get => isDisplayVehicle;
            set
            {
                isDisplayVehicle = value;
                ucVehicleImageViewIn.Visible = value;
                ucVehicleImageViewOut.Visible = value;
                kzuI_DirectionPanel1.RefreshUI();
                kzuI_DirectionPanel2.RefreshUI();
            }
        }

        private bool isDisplayFace = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Face Pic"), Description("Display Title")]
        public bool KZUI_IsDisplayFace
        {
            get => isDisplayFace;
            set
            {
                isDisplayFace = value;
                ucFaceImageViewIn.Visible = value;
                ucFaceImageViewOut.Visible = value;
                kzuI_DirectionPanel1.RefreshUI();
                kzuI_DirectionPanel2.RefreshUI();
            }
        }


        private bool isDisplayOther = true;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Other Pic"), Description("Display Title")]
        public bool KZUI_IsDisplayOther
        {
            get => isDisplayOther;
            set
            {
                isDisplayOther = value;
                ucOtherImageViewIn.Visible = value;
                ucOtherImageViewOut.Visible = value;
                kzuI_DirectionPanel1.RefreshUI();
                kzuI_DirectionPanel2.RefreshUI();
            }
        }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        //tableLayoutPanel1.Padding = new Padding(SizeManagement.SMALL_BORDER_RADIUS);
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        //tableLayoutPanel1.Padding = new Padding(SizeManagement.MEDIUM_BORDER_RADIUS);
                        break;
                    case EmControlSizeMode.LARGE:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        //tableLayoutPanel1.Padding = new Padding(SizeManagement.LARRGE_BORDER_RADIUS);
                        break;
                    default:
                        break;
                }
                ucPanoramaImageViewIn.KZUI_ControlSizeMode = value;
                ucVehicleImageViewIn.KZUI_ControlSizeMode = value;
                ucPanoramaImageViewOut.KZUI_ControlSizeMode = value;
                ucVehicleImageViewOut.KZUI_ControlSizeMode = value;
            }
        }

        public KZUI_UcImageListInOutMerge()
        {
            InitializeComponent();
            this.BackColor = ColorManagement.ControlBackgroud;
            this.DoubleBuffered = true;
            kzuI_DirectionPanel1.RefreshUI();
            kzuI_DirectionPanel2.RefreshUI();
        }
        public void Init(Image? defaultImage)
        {
            ucPanoramaImageViewIn.KZUI_DefaultImage = defaultImage;
            ucVehicleImageViewIn.KZUI_DefaultImage = defaultImage;
            ucFaceImageViewIn.KZUI_DefaultImage = defaultImage;
            ucOtherImageViewIn.KZUI_DefaultImage = defaultImage;

            ucPanoramaImageViewOut.KZUI_DefaultImage = defaultImage;
            ucVehicleImageViewOut.KZUI_DefaultImage = defaultImage;
            ucFaceImageViewOut.KZUI_DefaultImage = defaultImage;
            ucOtherImageViewOut.KZUI_DefaultImage = defaultImage;

            ucPanoramaImageViewIn.KZUI_Image = defaultImage;
            ucVehicleImageViewIn.KZUI_Image = defaultImage;
            ucFaceImageViewIn.KZUI_Image = defaultImage;
            ucOtherImageViewIn.KZUI_Image = defaultImage;

            ucPanoramaImageViewOut.KZUI_Image = defaultImage;
            ucVehicleImageViewOut.KZUI_Image = defaultImage;
            ucFaceImageViewOut.KZUI_Image = defaultImage;
            ucOtherImageViewOut.KZUI_Image = defaultImage;

            ucPanoramaImageViewIn.KZUI_Title = KZUIStyles.CurrentResources.TitlePanoramaIn;
            ucVehicleImageViewIn.KZUI_Title = KZUIStyles.CurrentResources.TitleVehicleIn;
            ucFaceImageViewIn.KZUI_Title = KZUIStyles.CurrentResources.TitleFaceIn;
            ucOtherImageViewIn.KZUI_Title = KZUIStyles.CurrentResources.TitleOtherIn;

            ucPanoramaImageViewOut.KZUI_Title = KZUIStyles.CurrentResources.TitlePanoramaOut;
            ucVehicleImageViewOut.KZUI_Title = KZUIStyles.CurrentResources.TitleVehicleOut;
            ucFaceImageViewOut.KZUI_Title = KZUIStyles.CurrentResources.TitleFaceOut;
            ucOtherImageViewOut.KZUI_Title = KZUIStyles.CurrentResources.TitleOtherOut;

            lblTitle.Text = KZUIStyles.CurrentResources.TitlePicMerge;
            kzuI_DirectionPanel1.RefreshUI();
            kzuI_DirectionPanel2.RefreshUI();
        }

        public void Init(EmControlSizeMode controlSizeMode)
        {
            this.KZUI_ControlSizeMode = controlSizeMode;
        }

        public void ClearView()
        {
            ucPanoramaImageViewIn.ClearView();
            ucVehicleImageViewIn.ClearView();
            ucPanoramaImageViewOut.ClearView();
            ucVehicleImageViewOut.ClearView();
        }

        public void DisplayEntryImage(Dictionary<EmImageType, Image?> images)
        {
            var panoramaImage = images.ContainsKey(EmImageType.PANORAMA) ? images[EmImageType.PANORAMA] : null;
            var vehicleImage = images.ContainsKey(EmImageType.VEHICLE) ? images[EmImageType.VEHICLE] : null;
            var faceImage = images.ContainsKey(EmImageType.FACE) ? images[EmImageType.FACE] : null;
            var otherImage = images.ContainsKey(EmImageType.OTHER) ? images[EmImageType.OTHER] : null;

            if (ucPanoramaImageViewIn.Visible)
                ucPanoramaImageViewIn.ShowImage(panoramaImage);

            if (ucVehicleImageViewIn.Visible)
                ucVehicleImageViewIn.ShowImage(vehicleImage);

            if (ucFaceImageViewIn.Visible)
                ucFaceImageViewIn.ShowImage(faceImage);

            if (ucOtherImageViewIn.Visible)
                ucOtherImageViewIn.ShowImage(otherImage);
        }
        public void DisplayExitImage(Dictionary<EmImageType, Image?> images)
        {
            var panoramaImage = images.ContainsKey(EmImageType.PANORAMA) ? images[EmImageType.PANORAMA] : null;
            var vehicleImage = images.ContainsKey(EmImageType.VEHICLE) ? images[EmImageType.VEHICLE] : null;
            var faceImage = images.ContainsKey(EmImageType.FACE) ? images[EmImageType.FACE] : null;
            var otherImage = images.ContainsKey(EmImageType.OTHER) ? images[EmImageType.OTHER] : null;

            if (ucPanoramaImageViewOut.Visible)
                ucPanoramaImageViewOut.ShowImage(panoramaImage);

            if (ucVehicleImageViewOut.Visible)
                ucVehicleImageViewOut.ShowImage(vehicleImage);

            if (ucFaceImageViewOut.Visible)
                ucFaceImageViewOut.ShowImage(faceImage);

            if (ucOtherImageViewOut.Visible)
                ucOtherImageViewOut.ShowImage(otherImage);
        }

        public void DisplayEntryImageData(List<EventImageDto> imageDatas)
        {
            var panoramaImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.PANORAMA);
            var vehicleImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.VEHICLE);
            var faceImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.FACE);
            var otherImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.OTHER);

            if (ucPanoramaImageViewIn.Visible)
                _ = ucPanoramaImageViewIn.ShowImageData(panoramaImageData);

            if (ucVehicleImageViewIn.Visible)
                _ = ucVehicleImageViewIn.ShowImageData(vehicleImageData);

            if (ucFaceImageViewIn.Visible)
                _ = ucFaceImageViewIn.ShowImageData(faceImageData);

            if (ucOtherImageViewIn.Visible)
                _ = ucOtherImageViewIn.ShowImageData(otherImageData);
        }
        public void DisplayExitImageData(List<EventImageDto> imageDatas)
        {
            var panoramaImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.PANORAMA);
            var vehicleImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.VEHICLE);
            var faceImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.FACE);
            var otherImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.OTHER);

            if (ucPanoramaImageViewOut.Visible)
                _ = ucPanoramaImageViewOut.ShowImageData(panoramaImageData);

            if (ucVehicleImageViewOut.Visible)
                _ = ucVehicleImageViewOut.ShowImageData(vehicleImageData);

            if (ucFaceImageViewOut.Visible)
                _ = ucFaceImageViewOut.ShowImageData(faceImageData);

            if (ucOtherImageViewOut.Visible)
                _ = ucOtherImageViewOut.ShowImageData(otherImageData);
        }
    }
}

