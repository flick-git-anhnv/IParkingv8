using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.Style;
using Kztek.Tool;
using System.ComponentModel;
using System.Globalization;
using static Kztek.Control8.UserControls.ucDataGridViewInfo.DisplayOptions;

namespace Kztek.Control8.UserControls.ucDataGridViewInfo
{
    public partial class ucExitInfor : UserControl, IDataInfo
    {
        private Font font_title = new Font("Arial", SizeManagement.SMALL_FONT_SIZE, FontStyle.Bold);
        private Font font_value = new Font("Arial", SizeManagement.SMALL_FONT_SIZE, FontStyle.Regular);
        private Font font_money = new Font("Arial", SizeManagement.SMALL_FONT_SIZE, FontStyle.Bold);

        public Em_DisplayTemplate templates;
        public EmLaneType laneTypes;
        private Dictionary<string, Label> fieldToLabelMap;
        private EmControlSizeMode sizeMode = EmControlSizeMode.MEDIUM;

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Title"), Description("Display Title")]
        public string KZUI_Title
        {
            get => lblTitle.Text;
            set
            {
                if (this.IsHandleCreated && this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        lblTitle.Text = value;
                    }));
                    return;
                }
                lblTitle.Text = value;
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Size Mode"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public EmControlSizeMode KZUI_ControlSizeMode
        {
            get => sizeMode;
            set
            {
                sizeMode = value;
                float fontSize = SizeManagement.MEDIUM_FONT_SIZE;

                switch (sizeMode)
                {
                    case EmControlSizeMode.SMALL:
                        fontSize = SizeManagement.SMALL_FONT_SIZE;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.SMALL_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        panel1.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.Height = SizeManagement.SMALL_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        guna2Panel1.BorderRadius = SizeManagement.SMALL_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.MEDIUM:
                        fontSize = SizeManagement.MEDIUM_FONT_SIZE;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.MEDIUM_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        panel1.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.Height = SizeManagement.MEDIUM_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        guna2Panel1.BorderRadius = SizeManagement.MEDIUM_BORDER_RADIUS;
                        break;
                    case EmControlSizeMode.LARGE:
                        fontSize = SizeManagement.LARRGE_FONT_SIZE;
                        lblTitle.Font = new Font(lblTitle.Font.Name, SizeManagement.LARRGE_FONT_SIZE, lblTitle.Font.Style, GraphicsUnit.Pixel);
                        panel1.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.Height = SizeManagement.LARRGE_HEIGHT;
                        lblTitle.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        guna2Panel1.BorderRadius = SizeManagement.LARRGE_BORDER_RADIUS;
                        break;
                    default:
                        break;
                }

                /// Cập nhật kích cỡ money
                SetPropertiesMoney(fontSize);
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Money"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public bool KZUI_IsDisplayMoney
        {
            get => pnlMoney.Visible;
            set
            {
                if (this.IsHandleCreated && this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        pnlMoney.Visible = value;
                    }));
                    return;
                }
                pnlMoney.Visible = value;
            }
        }

        [Browsable(true)]
        [Category("KZUI"), DisplayName("★ KZUI Is Display Title"), Description("Chế độ hiển thị -  SMALL : 24px -  MEDIUM : 32px -  LARGE : 40 px")]
        public bool KZUI_IsDisplayTitle
        {
            get => dgvData.Columns.Count == 0 ? false : dgvData.Columns[0].Visible;
            set
            {
                if (dgvData.Columns.Count ==0)
                {
                    return;
                }
                dgvData.Columns[0].Visible = value;
            }
        }

        public ucExitInfor(Lane lane)
        {
            InitializeComponent();
            this.KZUI_Title = KZUIStyles.CurrentResources.EventInfo;
            KZUI_ControlSizeMode = EmControlSizeMode.LARGE;
        }
        public ucExitInfor()
        {
            InitializeComponent();
        }

        private void SetPropertiesMoney(float fontSize)
        {
            int padding = this.Height / 12;
            lblMoney.Font = new Font(lblMoney.Font.FontFamily, fontSize + padding, FontStyle.Bold, GraphicsUnit.Pixel);
            pnlMoney.AutoSize = true;
            lblMoney.Text = "0";
        }

        public void DisplayEventInfo(DateTime? datetimein, DateTime? datetimeout, AccessKey? accessKey, Collection? collection, AccessKey? vehicle,
                                     long fee, string note, bool isDisplayTitle = true, bool isDisplayCustomerInfo = true)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() => DisplayEventInfo(datetimein, datetimeout, accessKey, collection, vehicle,
                                                              fee, note, isDisplayTitle, isDisplayCustomerInfo)));
                return;
            }
            var culture = CultureInfo.GetCultureInfo("vi-VN");
            string formatted2 = fee.ToString("N0", culture) + " đ";
            lblMoney.Text = formatted2;

            try
            {
                //DataInfoModel dataShow = new DataInfoModel();
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Display Entry Info"));
                dgvData.Rows.Clear();

                // Tên định danh: Tên mã thẻ + mã code
                if (accessKey != null)
                {
                    string accessKeyCode = accessKey.Code;
                    string accessKeyName = accessKey.Name;

                    dgvData.Rows.Add(KZUIStyles.CurrentResources.AccesskeyName, $"{accessKeyName}/{accessKeyCode}");
                    dgvData.Rows.Add(KZUIStyles.CurrentResources.VehicleType, collection?.Name ?? "");
                }

                // Thời gian vào
                if (datetimein != null)
                {
                    dgvData.Rows.Add(KZUIStyles.CurrentResources.TimeIn, datetimein.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                // Thời gian ra
                if (datetimeout != null)
                {
                    dgvData.Rows.Add(KZUIStyles.CurrentResources.TimeOut, datetimeout.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                }

                // Thông tin khách hàng
                CustomerDto? customer = vehicle?.Customer ?? accessKey?.Customer;
                if (isDisplayCustomerInfo && customer != null)
                {
                    string customerInfo = customer.Name ?? "";
                    if (!string.IsNullOrEmpty(customer.Address))
                    {
                        customerInfo += " / " + customer.Address;
                    }
                    dgvData.Rows.Add(KZUIStyles.CurrentResources.CustomerName, customerInfo);
                }

                // Biển số đăng ký
                if (vehicle != null)
                {
                    dgvData.Rows.Add(KZUIStyles.CurrentResources.VehicleCodeAcronym, vehicle.Code ?? "");
                    if (vehicle.ExpireTime != null)
                    {
                        dgvData.Rows.Add(KZUIStyles.CurrentResources.VehicleExpiredDate, vehicle.ExpireTime.Value.ToString("dd/MM/yyyy HH:mm:ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Display Event Info", ex));
            }
        }

        public void ClearView()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(ClearView);
                return;
            }
            dgvData.Rows.Clear();
            var culture = CultureInfo.GetCultureInfo("vi-VN");
            string formatted2 = 0.ToString("N0", culture) + " đ";
            lblMoney.Text = formatted2;

            lblDuration.Text = "";
        }
        public void SetDuration(long duration)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    SetDuration(duration);
                }));
            }
            lblDuration.Text = duration + "ms";
        }
    }
}
