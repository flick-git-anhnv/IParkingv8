using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using IParkingv8.API.Interfaces;
using Kztek.Tool;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcPlateVertical : UserControl, IKZUI_UcPlate
    {
        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Size Mode"), Description("Display Size Mode")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        txtPlate.Font = lblTitle.Font = new Font(lblTitle.Font.Name, 14, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        txtPlate.Font = lblTitle.Font = new Font(lblTitle.Font.Name, 16, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.LARGE:
                        txtPlate.Font = lblTitle.Font = new Font(lblTitle.Font.Name, 20, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
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
        [Category("★ KZUI"), DisplayName("★ KZUI Plate"), Description("Display Plate")]
        public string KZUI_Plate
        {
            get => txtPlate.Text;
            set
            {
                if (this.IsHandleCreated && this.InvokeRequired)
                {
                    this.BeginInvoke(new Action<string>(DisplayPlateInternal), value);
                }
                else
                {
                    DisplayPlateInternal(value);
                }
            }
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Default Image"), Description("Display Default Image")]
        public Image? KZUI_DefaultImage
        {
            get;
            set;
        }

        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Display Image"), Description("Display Display Image")]
        public Image? KZUI_Image
        {
            get => pic.Image;
            set
            {
                Image? displayImage = null;
                if (value is null)
                {
                    displayImage = KZUI_DefaultImage;
                }
                else
                {
                    displayImage = (Image?)value.Clone();
                }
                var resizedImage = ImageHelper.ResizeImage(displayImage, this.Width, this.Height);

                if (this.IsHandleCreated && this.InvokeRequired)
                {
                    this.BeginInvoke(new Action<Image?>(DisplayImageInternal), resizedImage);
                }
                else
                {
                    DisplayImageInternal(resizedImage);
                }
            }
        }

        public KZUI_UcPlateVertical()
        {
            InitializeComponent();
            this.KZUI_ControlSizeMode = EmControlSizeMode.MEDIUM;
            this.DoubleBuffered = true;
        }

        public void Init(string title, Image? defaultImage)
        {
            this.KZUI_DefaultImage = defaultImage;
            this.KZUI_Image = defaultImage;
            lblTitle.Text = title;
        }

        public void DisplayLprResult(string plate, Image? lprImage)
        {
            this.KZUI_Image = lprImage;
            this.KZUI_Plate = plate;
        }

        public async Task DisplayLprResultData(string plate, EventImageDto? lprImageData)
        {
            Image? displayImage = await ImageHelper.GetImageFromUrlAsync(lprImageData?.PresignedUrl);
            DisplayLprResult(plate, displayImage);
        }
        public void ClearView()
        {
            DisplayLprResult("", null);
        }

        public async Task<bool?> UpdatePlate(string eventId, IAPIServer apiServer, bool isEntry, string currentPlate)
        {
            if (!txtPlate.Focused) return null;
            if (isEntry)
            {
                return await apiServer.OperatorService.Entry.UpdatePlateAsync(eventId, txtPlate.Text, currentPlate);
            }
            else
            {
                return await apiServer.OperatorService.Exit.UpdatePlateAsync(eventId, txtPlate.Text, currentPlate);
            }
        }
        public void DisplayNewPlate(string newPlate)
        {
            this.KZUI_Plate = newPlate;
        }

        private void DisplayPlateInternal(string newPlate)
        {
            try
            {
                string displayPlate = newPlate.Replace(" ", "").Replace("-", "").Replace(".", "");
                txtPlate.Text = string.IsNullOrEmpty(displayPlate) ? "" : displayPlate;
                txtPlate.Refresh();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Show lpr image error", ex));
            }
        }
        private void DisplayImageInternal(Image? img)
        {
            try
            {
                pic.Image = img;
                pic.Refresh();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Show lpr image error", ex));
            }
        }
    }
}
