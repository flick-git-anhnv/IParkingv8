using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using Kztek.Object;
using Kztek.Tool;
using System.Data;

namespace IParkingv8.Forms.DataForms
{
    public partial class FrmRegisterMonthlyVehicle : Form
    {
        public bool isHaveChange = false;
        private List<Collection> accessKeyCollections = [];
        private List<CustomerGroup> customerCollections = [];

        public FrmRegisterMonthlyVehicle()
        {
            InitializeComponent();
            this.Load += FrmRegisterMonthlyVehicle_Load; ;
        }

        private async void FrmRegisterMonthlyVehicle_Load(object? sender, EventArgs e)
        {
            var identityGroupTask = AppData.ApiServer.DataService.AccessKeyCollection.GetAllAsync();
            var customerGroupTask = AppData.ApiServer.DataService.CustomerCollection.GetAllAsync();
            await Task.WhenAll(identityGroupTask, customerGroupTask);

            accessKeyCollections = identityGroupTask?.Result?.Item1 ?? [];
            customerCollections = customerGroupTask?.Result?.Item1 ?? [];
            InitUI();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVehiclePlate.Text))
            {
                return;
            }
            var responseVehicles = await AppData.ApiServer.DataService.RegisterVehicle.GetByConditionAsync(txtVehiclePlate.Text, "", null, "");
            AccessKey? responseVehicle = responseVehicles?.Data?.FirstOrDefault();
            if (responseVehicle == null )
            {
                MessageBox.Show($"Biển số {txtVehiclePlate.Text} chưa đăng ký phương tiện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ShowNotify($"Biển số đã đăng ký phương tiện", Color.Green);

                txtVehicleName.Text = responseVehicle?.Name ?? "";
                txtCustomerName.Text = responseVehicle?.Customer?.Name ?? "";
                txtCustomerCode.Text = responseVehicle?.Customer?.Code ?? "";
            }
        }
        private async Task<bool> BtnRegister_Click(object? sender)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVehiclePlate.Text))
                {
                    MessageBox.Show($"Biển số không được để trống");
                    return false;
                }

                // Kiem tra ton tai phuong tien - Plate 
                var searchVehicle = await AppData.ApiServer.DataService.RegisterVehicle.GetByPlateNumberAsync(txtVehiclePlate.Text);
                if (searchVehicle?.Item1 != null)
                {
                    ShowNotify($"BIỂN SỐ ĐÃ ĐƯỢC ĐĂNG KÝ PHƯƠNG TIỆN", Color.Red);
                    MessageBox.Show("Đăng ký không thành công! Biển số đã được đăng ký", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return false;
                }

                string prefix = "KH";
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string result = prefix + timestamp;

                // Kiem tra ton tai khach hang - Code
                var searchCustomer = (await AppData.ApiServer.DataService.Customer.GetByConditionAsync(txtCustomerName.Text, ""))?.Data?.FirstOrDefault(e => e.Code == txtCustomerCode.Text);
                if (searchCustomer != null)
                {
                    if (MessageBox.Show("Khách hàng đã tồn tại! Tiếp tục sử dụng khách hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return false;
                }
                else
                {
                    // Đki khách hàng
                    var custom = new CustomerDto()
                    {
                        Code = txtCustomerCode.Text,
                        Name = txtCustomerName.Text,
                        Collection = new CustomerCollectionDto() { Id = ((ListItem?)cbCustomerCollection.SelectedItem)?.Value ?? "" },
                    };

                    searchCustomer = (await AppData.ApiServer.DataService.Customer.CreateAsync(custom))?.Item1;
                    if (searchCustomer == null)
                    {
                        MessageBox.Show("Đăng ký không thành công, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }

                var vehicle = new AccessKey()
                {
                    Name = txtVehicleName.Text,
                    Code = txtVehiclePlate.Text,
                    Collection = new Collection() { Id = ((ListItem?)cbCollection.SelectedItem)?.Value ?? "" },
                    CustomerId = searchCustomer.Id,
                    Type = iParkingv8.Object.Enums.ParkingEnums.EmAccessKeyType.VEHICLE,
                };

                var responseVehicle = await AppData.ApiServer.DataService.AccessKey.CreateAsync(vehicle);
                if (responseVehicle == null || responseVehicle.Item1 == null || string.IsNullOrEmpty(responseVehicle.Item1.Id))
                {
                    ShowNotify($"ĐĂNG KÝ PHƯƠNG TIỆN KHÔNG THÀNH CÔNG!", Color.DarkRed);
                    MessageBox.Show("Đăng ký không thành công, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    ClearView();
                    ShowNotify($"ĐĂNG KÝ PHƯƠNG TIỆN THÀNH CÔNG!", Color.Green);
                    isHaveChange = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.DialogResult = DialogResult.Cancel;
            return true;
        }

        private void InitUI()
        {
            try
            {
                cbCollection.DisplayMember = cbCustomerCollection.DisplayMember = "Name";
                cbCollection.ValueMember = cbCustomerCollection.ValueMember = "Value";
                DisplayCollectionsOnCombobox();
                DisplayCustomerGroupsOnCombobox();

                btnRegister.OnClickAsync += BtnRegister_Click;
                btnCancel.OnClickAsync += BtnCancel_Click;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Create UI", ex, EmSystemActionType.ERROR));
            }
        }

        private void DisplayCustomerGroupsOnCombobox()
        {
            customerCollections = [.. customerCollections.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];
            cbCustomerCollection.Invoke(new Action(() =>
            {
                foreach (var item in customerCollections)
                {
                    ListItem customerGroupItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id
                    };
                    cbCustomerCollection.Items.Add(customerGroupItem);
                }
                cbCustomerCollection.SelectedIndex = 0;
            }));
        }
        private void DisplayCollectionsOnCombobox()
        {
            accessKeyCollections = [.. accessKeyCollections.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];

            cbCollection.Invoke(new Action(() =>
            {
                foreach (var item in accessKeyCollections)
                {
                    ListItem identityGroupItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id.ToString()
                    };
                    cbCollection.Items.Add(identityGroupItem);
                }
                cbCollection.SelectedIndex = 0;
            }));
        }
        private void ShowNotify(string content, Color color)
        {
            lblNotify.Text = content;
            lblNotify.ForeColor = color;
        }

        private void btnClearView_Click(object sender, EventArgs e)
        {
            ClearView();
        }
        private void ClearView()
        {
            txtVehicleName.Text = "";
            txtVehiclePlate.Text = "";
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            lblNotify.Text = "";
        }

    }
}
