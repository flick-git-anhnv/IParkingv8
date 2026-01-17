using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcImageList : UserControl, IKZUIEventImageListIn
    {
        public bool KZUI_IsDisplayTitle
        {
            get => lblTitle.Visible;
            set { lblTitle.Visible = value; }
        }

        public bool KZUI_IsDisplayPanorama { get => ucPanoramaImageView.Visible; set { ucPanoramaImageView.Visible = value; } }
        public bool KZUI_IsDisplayVehicle { get => ucVehicleImageView.Visible; set { ucVehicleImageView.Visible = value; } }
        public bool KZUI_IsDisplayFace { get => ucFaceImageView.Visible; set { ucFaceImageView.Visible = value; } }
        public bool KZUI_IsDisplayOther { get => ucOtherImageView.Visible; set { ucOtherImageView.Visible = value; } }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title"), Description("Display Title")]
        public string KZUI_Title
        {
            get => lblTitle.Text;
            set { lblTitle.Text = value; }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title Panorama"), Description("Display Title")]
        public string KZUI_TitlePanorama
        {
            get => ucPanoramaImageView?.KZUI_Title ?? "";
            set
            {
                if (ucPanoramaImageView != null)
                    ucPanoramaImageView.KZUI_Title = value;
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title Vehicle"), Description("Display Title")]
        public string KZUI_TitleVehicle
        {
            get => ucVehicleImageView?.KZUI_Title ?? "";
            set
            {
                if (ucVehicleImageView != null)
                {
                    ucVehicleImageView.KZUI_Title = value;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title Face"), Description("Display Title")]
        public string KZUI_TitleFace
        {
            get => ucFaceImageView?.KZUI_Title ?? "";
            set
            {
                if (ucFaceImageView != null)
                {
                    ucFaceImageView.KZUI_Title = value;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title Other"), Description("Display Title")]
        public string KZUI_TitleOther
        {
            get => ucOtherImageView?.KZUI_Title ?? "";
            set
            {
                if (ucOtherImageView != null)
                {
                    ucOtherImageView.KZUI_Title = value;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Space Between"), Description("Cài đặt chỉ áp dụng khoảng cách giữa các control")]
        public bool KZUI_SpaceBetween
        {
            get => panelImageList.SpaceBetween;
            set
            {
                panelImageList.SpaceBetween = value;
            }
        }

        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                foreach (var item in panelImageList.Controls.OfType<KZUI_UcImageView>())
                {
                    item.KZUI_ControlSizeMode = value;
                }
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        panelImageList.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        panelImageList.RefreshUI(SizeManagement.SMALL_BORDER_RADIUS);
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        panelImageList.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        panelImageList.RefreshUI(SizeManagement.MEDIUM_BORDER_RADIUS);
                        break;
                    case EmControlSizeMode.LARGE:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        panelMain.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        panelImageList.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        panelImageList.RefreshUI(SizeManagement.LARRGE_BORDER_RADIUS);
                        break;
                    default:
                        break;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Control Direction"), Description("Display Spacing")]
        public EmControlDirection KZUI_ControlDirection
        {
            get => panelImageList.KZUI_ControlDirection;
            set { panelImageList.KZUI_ControlDirection = value; }
        }


        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Default  Image"), Description("Display Title")]
        public Image? KZUI_DefaultImage
        {
            get => ucVehicleImageView?.KZUI_DefaultImage ?? null;
            set
            {
                if (ucVehicleImageView != null)
                {
                    ucVehicleImageView.KZUI_DefaultImage = value;
                }
                if (ucPanoramaImageView != null)
                {
                    ucPanoramaImageView.KZUI_DefaultImage = value;
                }
                if (ucFaceImageView != null)
                {
                    ucFaceImageView.KZUI_DefaultImage = value;
                }
                if (ucOtherImageView != null)
                {
                    ucOtherImageView.KZUI_DefaultImage = value;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Vehicle Image"), Description("Display Title")]
        public Image? KZUI_VehicleImage
        {
            get => ucVehicleImageView?.KZUI_Image ?? null;
            set
            {
                if (ucVehicleImageView != null)
                {
                    ucVehicleImageView.KZUI_Image = value;
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Panorama Image"), Description("Display Title")]
        public Image? KZUI_PanoramaImage
        {
            get => ucPanoramaImageView?.KZUI_Image ?? null;
            set
            {
                if (ucPanoramaImageView != null)
                {
                    ucPanoramaImageView.KZUI_Image = value;
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Face Image"), Description("Display Title")]
        public Image? KZUI_FaceImage
        {
            get => ucFaceImageView?.KZUI_Image ?? null;
            set
            {
                if (ucFaceImageView != null)
                {
                    ucFaceImageView.KZUI_Image = value;
                }
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Other Image"), Description("Display Title")]
        public Image? KZUI_OtherImage
        {
            get => ucOtherImageView?.KZUI_Image ?? null;
            set
            {
                if (ucOtherImageView != null)
                {
                    ucOtherImageView.KZUI_Image = value;
                }
            }
        }

        KZUI_UcImageView ucPanoramaImageView = new();
        KZUI_UcImageView ucVehicleImageView = new();
        KZUI_UcImageView ucFaceImageView = new();
        KZUI_UcImageView ucOtherImageView = new();

        public KZUI_UcImageList()
        {
            InitializeComponent();
            this.BackColor = ColorManagement.ControlBackgroud;
            panelMain.BackColor = ColorManagement.ControlBackgroud;
            panelMain.FillColor = ColorManagement.AppBackgroudColor;

            panelImageList.Controls.Add(ucPanoramaImageView);
            panelImageList.Controls.Add(ucVehicleImageView);
            panelImageList.Controls.Add(ucFaceImageView);
            panelImageList.Controls.Add(ucOtherImageView);

            ucPanoramaImageView.Dock = DockStyle.Top;
            ucPanoramaImageView.BringToFront();

            ucVehicleImageView.Dock = DockStyle.Top;
            ucVehicleImageView.BringToFront();

            ucFaceImageView.Dock = DockStyle.Top;
            ucFaceImageView.BringToFront();

            ucOtherImageView.Dock = DockStyle.Top;
            ucOtherImageView.BringToFront();

            this.DoubleBuffered = true;
        }

        public void Init(string title, string titlePanorama, string titleVehicle,
                         string titleFace, string titleOther, Image? defaultImage)
        {
            this.KZUI_Title = title;
            this.KZUI_TitlePanorama = titlePanorama;
            this.KZUI_TitleVehicle = titleVehicle;
            this.KZUI_TitleFace = titleFace;
            this.KZUI_TitleOther = titleOther;
            this.KZUI_DefaultImage = defaultImage;
            panelImageList.Refresh();
        }
        public void Init(EmControlSizeMode controlSizeMode, EmControlDirection controlDirection, bool isSpaceBetween)
        {
            this.KZUI_ControlSizeMode = controlSizeMode;
            this.KZUI_ControlDirection = controlDirection;
            this.KZUI_SpaceBetween = isSpaceBetween;
            panelImageList.Refresh();
        }

        #region Public Function
        public void ClearView()
        {
            if (ucPanoramaImageView.Visible)
                ucPanoramaImageView.ClearView();
            if (ucVehicleImageView.Visible)
                ucVehicleImageView.ClearView();
            if (ucFaceImageView.Visible)
                ucFaceImageView.ClearView();
            if (ucOtherImageView.Visible)
                ucOtherImageView.ClearView();
        }

        public void DisplayImage(Dictionary<EmImageType, Image?> images)
        {
            var panoramaImage = images.ContainsKey(EmImageType.PANORAMA) ? images[EmImageType.PANORAMA] : null;
            var vehicleImage = images.ContainsKey(EmImageType.VEHICLE) ? images[EmImageType.VEHICLE] : null;
            var faceImage = images.ContainsKey(EmImageType.FACE) ? images[EmImageType.FACE] : null;
            var otherImage = images.ContainsKey(EmImageType.OTHER) ? images[EmImageType.OTHER] : null;

            if (ucPanoramaImageView.Visible)
                ucPanoramaImageView.ShowImage(panoramaImage);

            if (ucVehicleImageView.Visible)
                ucVehicleImageView.ShowImage(vehicleImage);

            if (ucFaceImageView.Visible)
                ucFaceImageView.ShowImage(faceImage);

            if (ucOtherImageView.Visible)
                ucOtherImageView.ShowImage(otherImage);
        }
        public void DisplayImageData(List<EventImageDto> imageDatas)
        {
            var panoramaImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.PANORAMA);
            var vehicleImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.VEHICLE);
            var faceImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.FACE);
            var otherImageData = imageDatas.FirstOrDefault(e => e?.Type is not null && e.Type == EmImageType.OTHER);

            if (ucPanoramaImageView.Visible)
                _ = ucPanoramaImageView.ShowImageData(panoramaImageData);

            if (ucVehicleImageView.Visible)
                _ = ucVehicleImageView.ShowImageData(vehicleImageData);

            if (ucFaceImageView.Visible)
                _ = ucFaceImageView.ShowImageData(faceImageData);

            if (ucOtherImageView.Visible)
                _ = ucOtherImageView.ShowImageData(otherImageData);
        }
        #endregion
    }
}
