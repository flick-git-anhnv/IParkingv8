using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Object;
using Kztek.Tool;
using Filter = IParkingv8.API.Objects.Filter;

namespace IParkingv8.Forms.DataForms
{
    public partial class FrmVehicles : Form, KzITranslate
    {
        private List<Collection> accessKeyCollections = [];
        private List<CustomerGroup> customerCollections = [];

        #region FORMS
        public FrmVehicles()
        {
            InitializeComponent();
            Translate();

            this.KeyPreview = true;
            this.KeyDown += FrmSelectCard_KeyDown;
            this.Load += FrmSelectCard_Load;
        }
        private void FrmSelectCard_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            btnSearch.PerformClick();
        }
        private async void FrmSelectCard_Load(object? sender, EventArgs e)
        {
            var identityGroupTask = AppData.ApiServer.DataService.AccessKeyCollection.GetAllAsync();
            var customerGroupTask = AppData.ApiServer.DataService.CustomerCollection.GetAllAsync();
            await Task.WhenAll(identityGroupTask, customerGroupTask);

            accessKeyCollections = identityGroupTask?.Result?.Item1 ?? [];
            customerCollections = customerGroupTask?.Result?.Item1 ?? [];
            InitUI();
        }
        #endregion END FORMS

        #region CONTROLS IN FORM
        private async Task<bool> BtnSearch_Click(object? sender)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ucNavigator1.Reset();
                Reset();
                return await GetReportData(0);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Vehicle Report", ex, EmSystemActionType.ERROR));
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.DialogResult = DialogResult.Cancel;
            return true;
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            FrmRegisterMonthlyVehicle frm = new FrmRegisterMonthlyVehicle();
            frm.ShowDialog();
            btnSearch.PerformClick();
        }
        private async void UcNavigator1_onPageChangeEvent(int pageIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Reset();
                await GetReportData(pageIndex - 1);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Vehicle Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion END CONTROLS IN FORM

        #region Private Function
        private void DisplayVehicleTypesOnCombobox()
        {
            cbVehicleType.Invoke(new Action(() =>
            {
                cbVehicleType.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            cbVehicleType.Invoke(new Action(() =>
            {
                foreach (EmVehicleType item in Enum.GetValues(typeof(EmVehicleType)))
                {
                    ListItem vehicleTypeItem = new()
                    {
                        Name = VehicleType.ToDisplayString(item),
                        Value = ((int)item).ToString()
                    };
                    cbVehicleType.Items.Add(vehicleTypeItem);
                }
                cbVehicleType.SelectedIndex = 0;
            }));
        }
        private void DisplayCustomerGroupsOnCombobox()
        {
            cbCustomerCollection.Invoke(new Action(() =>
            {
                cbCustomerCollection.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

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
            cbCollection.Invoke(new Action(() =>
            {
                cbCollection.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

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
        #endregion

        private void Reset()
        {
            dgvData.Rows.Clear();
            dgvData.Refresh();
        }
        private async Task<bool> GetReportData(int pageIndex)
        {
            string collectionId = cbCollection.SelectedItem == null ? "" : ((ListItem)cbCollection.SelectedItem)?.Value ?? "";
            string customerCollectionId = cbCustomerCollection.SelectedItem == null ? "" : ((ListItem)cbCustomerCollection.SelectedItem)?.Value ?? "";
            EmVehicleType? vehicleType = null;
            if (cbVehicleType.SelectedIndex > 0)
            {
                vehicleType = (EmVehicleType)(cbVehicleType.SelectedIndex - 1);
            }

            var report = await AppData.ApiServer.DataService.RegisterVehicle.GetByConditionAsync(txtKeyword.Text, collectionId, vehicleType,
                                                                    customerCollectionId, pageIndex: pageIndex, pageSize: Filter.PAGE_SIZE);
            if (report == null)
            {
                string message = KZUIStyles.CurrentResources.SystemError + " " + KZUIStyles.CurrentResources.TryAgain;
                MessageBox.Show(message, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var vehicles = report.Data ?? [];
            ucNavigator1.KZUI_MaxPage = report.TotalPage;
            ucNavigator1.KZUI_CurrentPage = pageIndex + 1;
            ucNavigator1.KZUI_TotalRecord = report.TotalCount;
            if (vehicles != null)
            {
                for (int i = 0; i < vehicles.Count; i++)
                {
                    string cardNo = "";
                    string status = "";
                    if (vehicles[i].Metrics != null && vehicles[i].Metrics.Count > 0)
                    {
                        cardNo = vehicles[i].Metrics[0].Name;
                        status = vehicles[i].Metrics[0].GetStatusName();
                    }
                    dgvData.Rows.Add(i + 1, vehicles[i].Name, vehicles[i].Code, cardNo, status, vehicles[i].Collection?.GetVehicleTypeName() ?? "",
                                  vehicles[i].Customer?.Name ?? "", vehicles[i].Customer?.Collection?.Name ?? "", vehicles[i].Customer?.PhoneNumber, vehicles[i].Customer?.Address,
                                  vehicles[i].ExpireTime == null ? "-" : vehicles[i].ExpireTime.ToVNTime(), vehicles[i].Collection?.Name ?? "");
                }
                vehicles.Clear();
                GC.Collect();
            }

            if (dgvData.Rows.Count > 0)
            {
                dgvData.CurrentCell = dgvData.Rows[0].Cells[0];
            }

            dgvData.Refresh();
            return true;
        }

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            this.Text = KZUIStyles.CurrentResources.FrmVehicleList;
            lblKeywordTitle.Text = KZUIStyles.CurrentResources.Keyword;
            lblAccesskeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
            lblVehicleType.Text = KZUIStyles.CurrentResources.VehicleType;
            lblCustomerCollectionTitle.Text = KZUIStyles.CurrentResources.CustomerCollection;

            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;

            col_vehicle_name.HeaderText = KZUIStyles.CurrentResources.colVehicleName;
            col_vehicle_code.HeaderText = KZUIStyles.CurrentResources.colVehicleCode;
            col_vehicle_access_key.HeaderText = KZUIStyles.CurrentResources.colVehicleAccessKey;
            col_vehicle_status.HeaderText = KZUIStyles.CurrentResources.colVehicleStatus;
            col_vehicle_type.HeaderText = KZUIStyles.CurrentResources.colVehicleType;
            col_vehicle_customer_name.HeaderText = KZUIStyles.CurrentResources.colVehicleCustomerName;
            col_vehicle_customer_collection.HeaderText = KZUIStyles.CurrentResources.colVehicleCustomerCollection;
            col_vehicle_customer_phone.HeaderText = KZUIStyles.CurrentResources.colVehicleCustomerPhone;
            col_vehicle_customer_address.HeaderText = KZUIStyles.CurrentResources.colVehicleCustomerAddress;
            col_vehicle_expired_date.HeaderText = KZUIStyles.CurrentResources.colVehicleExpiredDate;
            col_vehicle_collection.HeaderText = KZUIStyles.CurrentResources.colVehicleCollection;
        }
        private void InitUI()
        {
            try
            {
                btnRegister.Visible = AppData.AppConfig.PrintTemplate == (int)EmPrintTemplate.TNG_MINHCAU ||
                                      AppData.AppConfig.PrintTemplate == (int)EmPrintTemplate.TNG_SONGCONG ||
                                      AppData.AppConfig.PrintTemplate == (int)EmPrintTemplate.TNG_VIETDUC;
                cbVehicleType.DisplayMember = cbCollection.DisplayMember = cbCustomerCollection.DisplayMember = "Name";
                cbVehicleType.ValueMember = cbCollection.ValueMember = cbCustomerCollection.ValueMember = "Value";
                DisplayVehicleTypesOnCombobox();
                DisplayCollectionsOnCombobox();
                DisplayCustomerGroupsOnCombobox();

                ucNavigator1.Reset();

                btnSearch.OnClickAsync += BtnSearch_Click;
                btnCancel.OnClickAsync += BtnCancel_Click;
                ucNavigator1.onPageChangeEvent += UcNavigator1_onPageChangeEvent;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Create UI", ex, EmSystemActionType.ERROR));
            }
        }
    }
}
