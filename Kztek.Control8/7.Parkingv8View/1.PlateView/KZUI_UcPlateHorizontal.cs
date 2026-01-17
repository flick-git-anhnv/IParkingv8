using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using IParkingv8.API.Interfaces;
using Kztek.Tool;
using System.ComponentModel;

namespace Kztek.Control8.UserControls
{
    public partial class KZUI_UcPlateHorizontal : UserControl, IKZUI_UcPlate
    {
        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI Size Mode"), Description("Display Title")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        lblTitle.Font = new Font(lblTitle.Font.Name, 14, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Font = new Font(lblTitle.Font.Name, 24, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = 24;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        lblTitle.Font = new Font(lblTitle.Font.Name, 16, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Font = new Font(lblTitle.Font.Name, 24, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = 32;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.LARGE:
                        lblTitle.Font = new Font(lblTitle.Font.Name, 20, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Font = new Font(lblTitle.Font.Name, 24, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        txtPlate.Height = lblTitle.Height = 40;
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
                //value ??= KZUI_DefaultImage;
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

        private bool IsLeftoToRight = true;
        [Browsable(true)]
        [Category("★ KZUI"), DisplayName("★ KZUI IsRevert"), Description("Display Title")]
        public bool KZUI_IsLeftoToRight
        {
            get => IsLeftoToRight;
            set
            {
                IsLeftoToRight = value;
                if (value)
                {
                    panelPlate.Dock = DockStyle.Left;
                    lblTitle.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(false, false, true, false);
                    txtPlate.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(true, false, false, false);
                    pic.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(false, true, false, true);
                }
                else
                {
                    panelPlate.Dock = DockStyle.Right;
                    lblTitle.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(false, false, false, true);
                    txtPlate.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(false, true, false, false);
                    pic.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges(true, false, true, false);
                }
            }
        }

        public KZUI_UcPlateHorizontal()
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
            this.SizeChanged += KZUI_UcPlateHorizontal_SizeChanged;
        }

        private void KZUI_UcPlateHorizontal_SizeChanged(object? sender, EventArgs e)
        {
            panelPlate.Width = this.Width / 2;
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
            this.KZUI_Plate = newPlate.Replace(" ", "").Replace("-", "").Replace(".", "");
        }
        private void DisplayPlateInternal(string newPlate)
        {
            try
            {
                var displayPlate = newPlate.Replace(" ", "").Replace("-", "").Replace(".", "");
                txtPlate.Text = string.IsNullOrEmpty(displayPlate) ? "" : displayPlate;
                txtPlate.Refresh();
                var size = TextRenderer.MeasureText(displayPlate ?? string.Empty, txtPlate.Font);
                if (panelPlate.Width < (size.Width + 10))
                {
                    panelPlate.Width = size.Width + 10;
                }
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


