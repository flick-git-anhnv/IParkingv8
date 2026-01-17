using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcImageListInOut : UserControl, IKZUIEventImageListOut
    {
        public bool KZUI_IsDisplayTitle
        {
            get => kzuI_UcImageListIn.KZUI_IsDisplayTitle && kzuI_UcImageListOut.KZUI_IsDisplayTitle;
            set
            {
                kzuI_UcImageListIn.KZUI_IsDisplayTitle = value;
                kzuI_UcImageListOut.KZUI_IsDisplayTitle = value;
            }
        }
        public bool KZUI_IsDisplayPanorama { get; set; }
        public bool KZUI_IsDisplayVehicle { get; set; }
        public bool KZUI_IsDisplayFace { get; set; }
        public bool KZUI_IsDisplayOther { get; set; }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                foreach (var item in panelMain.Controls.OfType<KZUI_UcImageList>())
                {
                    item.KZUI_ControlSizeMode = value;
                }

                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        panelMain.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        panelMain.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.LARGE:
                        panelMain.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        break;
                    default:
                        break;
                }
            }
        }
        public KZUI_UcImageListInOut()
        {
            InitializeComponent();
            this.BackColor = ColorManagement.ControlBackgroud;
            panelMain.BackColor = ColorManagement.ControlBackgroud;
            panelMain.FillColor = ColorManagement.ControlBackgroud;
            this.DoubleBuffered = true;
        }
        public void Init(Image? defaultImage)
        {
            kzuI_UcImageListIn.Init(KZUIStyles.CurrentResources.TitlePicIn, KZUIStyles.CurrentResources.TitlePanoramaIn, 
                                    KZUIStyles.CurrentResources.TitleVehicleIn, KZUIStyles.CurrentResources.TitleFaceIn, KZUIStyles.CurrentResources.TitleOtherIn, defaultImage);
            kzuI_UcImageListOut.Init(KZUIStyles.CurrentResources.TitlePicOut, KZUIStyles.CurrentResources.TitlePanoramaOut, KZUIStyles.CurrentResources.TitleFaceOut,
                                     KZUIStyles.CurrentResources.TitleOtherOut, KZUIStyles.CurrentResources.TitleVehicleOut, defaultImage);
        }
        public void Init(EmControlSizeMode controlSizeMode)
        {
            this.KZUI_ControlSizeMode = controlSizeMode;
        }

        public void ClearView()
        {
            kzuI_UcImageListIn.ClearView();
            kzuI_UcImageListOut.ClearView();
        }

        public void DisplayEntryImage(Dictionary<EmImageType, Image?> images)
        {
            this.kzuI_UcImageListIn.DisplayImage(images);
        }
        public void DisplayExitImage(Dictionary<EmImageType, Image?> images)
        {
            this.kzuI_UcImageListOut.DisplayImage(images);
        }

        public void DisplayEntryImageData(List<EventImageDto> imageDatas)
        {
            this.kzuI_UcImageListIn.DisplayImageData(imageDatas);
        }
        public void DisplayExitImageData(List<EventImageDto> imageDatas)
        {
            this.kzuI_UcImageListOut.DisplayImageData(imageDatas);
        }
    }
}
