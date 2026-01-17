using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Objects;
using Kztek.Object;
using Kztek.Tool;

namespace IParkingv8.Forms.DataForms
{
    public partial class FrmCustomers : Form, KzITranslate
    {
        #region PROPERTIES
        private List<CustomerGroup> customerCollections = [];
        #endregion END PROPERTIES

        #region FORMS
        public FrmCustomers()
        {
            InitializeComponent();
            Translate();

            this.KeyPreview = true;
            this.KeyDown += FrmSelectCard_KeyDown;
            this.Load += FrmSelectCard_Load;
        }
        private async void FrmSelectCard_Load(object? sender, EventArgs e)
        {
            customerCollections = (await AppData.ApiServer.DataService.CustomerCollection.GetAllAsync())?.Item1 ?? [];
            InitUI();
        }

        private void FrmSelectCard_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            btnSearch.PerformClick();
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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Customer Report", ex, EmSystemActionType.ERROR));
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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Customer Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion END CONTROLS IN FORM

        private void DisplayCustomerGroupsOnCombobox()
        {
            cbCustomerGroup.Invoke(new Action(() =>
            {
                cbCustomerGroup.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            customerCollections = [.. customerCollections.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];
            cbCustomerGroup.Invoke(new Action(() =>
            {
                foreach (var item in customerCollections)
                {
                    ListItem customerGroupItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id
                    };
                    cbCustomerGroup.Items.Add(customerGroupItem);
                }
                cbCustomerGroup.SelectedIndex = 0;
            }));
        }
        private void Reset()
        {
            dgvData.Rows.Clear();
            dgvData.Refresh();
        }
        private async Task<bool> GetReportData(int pageIndex)
        {
            string customerCollectionId = cbCustomerGroup.SelectedItem == null ? "" : ((ListItem)cbCustomerGroup.SelectedItem)?.Value ?? "";

            var report = await AppData.ApiServer.DataService.Customer.GetByConditionAsync(txtKeyword.Text, customerCollectionId, pageIndex: pageIndex, pageSize: Filter.PAGE_SIZE);
            if (report == null)
            {
                string message = KZUIStyles.CurrentResources.SystemError + " " + KZUIStyles.CurrentResources.TryAgain;
                MessageBox.Show(message, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            var customers = report.Data ?? [];
            ucNavigator1.KZUI_MaxPage = report.TotalPage;
            ucNavigator1.KZUI_CurrentPage = pageIndex + 1;
            ucNavigator1.KZUI_TotalRecord = report.TotalCount;
            for (int i = 0; i < customers.Count; i++)
            {
                dgvData.Rows.Add(i + 1, customers[i].Name, customers[i].Code, customers[i].PhoneNumber,
                               customers[i].Collection?.Name ?? "", customers[i].Address);
            }
            customers.Clear();
            GC.Collect();

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

            this.Text = KZUIStyles.CurrentResources.FrmCustomerList;
            lblKeywordTitle.Text = KZUIStyles.CurrentResources.Keyword;
            lblCollectionTitle.Text = KZUIStyles.CurrentResources.CustomerCollection;

            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;

            col_customer_name.HeaderText = KZUIStyles.CurrentResources.colCustomerName;
            col_customer_code.HeaderText = KZUIStyles.CurrentResources.colCustomerCode;
            col_customer_phone.HeaderText = KZUIStyles.CurrentResources.colCustomerPhone;
            col_customer_collection.HeaderText = KZUIStyles.CurrentResources.colCustomerCollection;
            col_customer_address.HeaderText = KZUIStyles.CurrentResources.colCustomerAddress;

            txtKeyword.PlaceholderText = KZUIStyles.CurrentResources.colCustomerKeyword;
        }
        private void InitUI()
        {
            cbCustomerGroup.DisplayMember = "Name";
            cbCustomerGroup.ValueMember = "Value";
            DisplayCustomerGroupsOnCombobox();

            ucNavigator1.Reset();

            btnSearch.OnClickAsync += BtnSearch_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;

            ucNavigator1.onPageChangeEvent += UcNavigator1_onPageChangeEvent;
        }
    }
}
