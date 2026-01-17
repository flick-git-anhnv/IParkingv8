using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using Kztek.Tool;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcImageView : UserControl
    {
        #region Properties
        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
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
                        this.guna2Elipse1.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS * 2;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        this.guna2Elipse1.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS * 2;
                        break;
                    case EmControlSizeMode.LARGE:
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        this.guna2Elipse1.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS * 2;
                        break;
                    default:
                        break;
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Title"), Description("Display Title")]
        public string KZUI_Title
        {
            get => lblTitle.Text;
            set
            {
                lblTitle.Text = value;
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI DefaultImage"), Description("Display Default Image")]
        public Image? KZUI_DefaultImage
        {
            get => pic.KZUI_DefaultImage;
            set
            {
                pic.KZUI_DefaultImage = value;
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Image"), Description("Display Image")]
        public Image? KZUI_Image
        {
            get => pic.KZUI_Image;
            set
            {
                pic.KZUI_Image = value;
            }
        }
        #endregion

        #region Constructor
        public KZUI_UcImageView()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Function
        public void ClearView()
        {
            this.KZUI_Image = this.KZUI_DefaultImage;
        }
        public void ShowImage(Image? image)
        {
            this.KZUI_Image = image;
        }

        public async Task ShowImageData(EventImageDto? vehicleImageData)
        {
            if (string.IsNullOrEmpty(vehicleImageData?.PresignedUrl))
            {
                ShowImage(KZUI_DefaultImage);
                return;
            }

            try
            {
                var image = await ImageHelper.GetImageFromUrlAsync(vehicleImageData.PresignedUrl);
                ShowImage(image);
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
