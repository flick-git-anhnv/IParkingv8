using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Users;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.Printer;
using IParkingv8.Reporting;
using Kztek.Object;
using Kztek.Tool;

namespace iParkingv8.Reporting
{
    public partial class FrmRevenueByIdentityGroup : Form, KzITranslate
    {
        #region Properties
        private List<Collection> collections = [];
        private List<User> users = [];
        private IAPIServer apiServer;
        public Image? defaultImg = null;
        private IPrinter printer;
        private AppOption appOption;
        #endregion

        #region Forms
        public FrmRevenueByIdentityGroup(IAPIServer server, IPrinter printer, AppOption appOption, List<Collection> accessKeyCollection)
        {
            InitializeComponent();

            InitProperties(server, printer, appOption, accessKeyCollection);
            Translate();

            this.KeyPreview = true;
            this.Load += FrmRevenueByIdentityGroup_Load;
            this.KeyDown += FrmRevenueByIdentityGroup_KeyDown;
        }
        private async void FrmRevenueByIdentityGroup_Load(object? sender, EventArgs e)
        {
            var userTask = this.apiServer.UserService.GetUserDataAsync();
            users = (await userTask)?.Item1 ?? [];
            InitUI();
        }
        private void FrmRevenueByIdentityGroup_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSearch.PerformClick();
            }
        }
        #endregion

        #region Controls In Form
        private void CbMode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }
        private async Task<bool> BtnSearch_Click(object? sender)
        {
            try
            {
                string identityGroupId = cbIdentityGroup.SelectedItem == null ? "" : ((ListItem)cbIdentityGroup.SelectedItem).Value ?? "";
                string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

                var data = await this.apiServer.ReportingService.Revenue.GetRevenueDetail(dtpStartTime.Value, dtpEndTime.Value, (int)cbMode.SelectedIndex,
                                                                                          user, identityGroupId, "");
                if (data == null)
                {
                    MessageBox.Show("Không tải được thông tin doanh thu. Vui lòng thử lại");
                    return false;
                }
                dgvData.Rows.Clear();
                Dictionary<string, ReportForSecurity> report = new Dictionary<string, ReportForSecurity>();
                foreach (var item in data.Data)
                {
                    if (report.ContainsKey(item.Identifier))
                    {
                        report[item.Identifier].TotalCount += item.Count;
                        report[item.Identifier].TotalAmount += item.Amount;
                        report[item.Identifier].TotalDiscount += item.Discount;
                        report[item.Identifier].TotalPaid += item.EntryAmount;
                        report[item.Identifier].RealMoney += Math.Max(item.Amount - item.Discount - item.EntryAmount, 0);
                    }
                    else
                    {
                        report.Add(item.Identifier, new ReportForSecurity()
                        {
                            TotalCount = item.Count,
                            TotalAmount = item.Amount,
                            TotalDiscount = item.Discount,
                            TotalPaid = item.EntryAmount,
                            RealMoney = Math.Max(item.Amount - item.Discount - item.EntryAmount, 0)
                        });
                    }
                }

                foreach (KeyValuePair<string, ReportForSecurity> item in report.OrderBy(r => r.Key))
                {
                    dgvData.Rows.Add(dgvData.RowCount + 1, item.Key, item.Value.TotalCount,
                                     item.Value.TotalAmount.ToString("N0"), item.Value.TotalDiscount.ToString("N0"),
                                     item.Value.TotalPaid.ToString("N0"), item.Value.RealMoney.ToString("N0"));
                }
                dgvData.Rows.Add("", KZUIStyles.CurrentResources.Total, data.TotalCount, data.TotalAmount.ToString("N0"), data.TotalDiscount.ToString("N0"), data.TotalEntryAmount.ToString("N0"), data.TotalExitAmount.ToString("N0"));
                dgvData.Rows[dgvData.RowCount - 1].DefaultCellStyle.Font = new Font(dgvData.DefaultCellStyle.Font, FontStyle.Bold);
                dgvData.CurrentCell = dgvData.Rows[dgvData.RowCount - 1].Cells[0];
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private async Task<bool> BtnExcel_Click(object sender)
        {
            ExcelTools.CreatReportFile(dgvData, "", []);
            return true;
        }
        private async Task<bool> BtnPrint_Click(object sender)
        {
            try
            {
                string identityGroupId = cbIdentityGroup.SelectedItem == null ? "" : ((ListItem)cbIdentityGroup.SelectedItem).Value ?? "";
                string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

                var data = await this.apiServer.ReportingService.Revenue.GetRevenueDetail(dtpStartTime.Value, dtpEndTime.Value, (int)cbMode.SelectedIndex,
                                                                                          user, identityGroupId, "");

                string printTemplatePath = IparkingingPathManagement.appPrintTemplateRevenuePath(((EmPrintTemplate)this.appOption.PrintTemplate).ToString());

                if (File.Exists(printTemplatePath))
                {
                    string printContent = printer.PrintRevenue(File.ReadAllText(printTemplatePath), data);
                    new frmPrintPreview(printContent, 1).ShowDialog();

                }
                else
                {
                    MessageBox.Show(KZUIStyles.CurrentResources.InvalidPrintTemplate, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Print Revenue", ex, EmSystemActionType.ERROR));
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
        #endregion

        #region Private Functions
        private void DisplayAccessKeyCollectionsOnCombobox()
        {
            cbIdentityGroup.Invoke(new Action(() =>
            {
                cbIdentityGroup.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            collections = [.. collections.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];

            cbIdentityGroup.Invoke(new Action(() =>
            {
                foreach (var item in collections)
                {
                    ListItem identityGroupItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id.ToString()
                    };
                    cbIdentityGroup.Items.Add(identityGroupItem);
                }
                cbIdentityGroup.SelectedIndex = 0;
            }));
        }
        private void DisplayUsersOnCombobox()
        {
            cbUser.Invoke(new Action(() =>
            {
                cbUser.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            users = [.. users.OrderBy(x => x.Upn).ThenBy(x => x.Upn.Length)];
            cbUser.Invoke(new Action(() =>
            {
                foreach (var item in users)
                {
                    ListItem laneItem = new()
                    {
                        Name = item.Upn,
                        Value = item.Id
                    };
                    cbUser.Items.Add(laneItem);
                }
                cbUser.SelectedIndex = 0;
            }));
        }
        #endregion

        public void Translate()
        {
            lblUserTitle.Text = KZUIStyles.CurrentResources.User;
            lblCollection.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
            lblStartTime.Text = KZUIStyles.CurrentResources.StartTime;
            lblEndTime.Text = KZUIStyles.CurrentResources.EndTime;
            lblGroupFilter.Text = KZUIStyles.CurrentResources.colType;

            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnExcel.Text = KZUIStyles.CurrentResources.ExportExcel;
            btnPrint.Text = KZUIStyles.CurrentResources.Print;

            colName.HeaderText = KZUIStyles.CurrentResources.Name;
            colQuantity.HeaderText = KZUIStyles.CurrentResources.Quantity;
            colFee.HeaderText = KZUIStyles.CurrentResources.Fee;
            colDiscount.HeaderText = KZUIStyles.CurrentResources.Discount;
            colPaid.HeaderText = KZUIStyles.CurrentResources.Paid;
            colRealFee.HeaderText = KZUIStyles.CurrentResources.RealFee;
        }

        private void InitUI()
        {
            cbIdentityGroup.DisplayMember = cbUser.DisplayMember = "Name";
            cbIdentityGroup.ValueMember = cbUser.ValueMember = "Value";
            dtpStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEndTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            foreach (EmRevenueMode mode in Enum.GetValues(typeof(EmRevenueMode)))
            {
                cbMode.Items.Add(GetModeDisplayString(mode));
            }
            cbMode.SelectedIndex = 0;

            DisplayAccessKeyCollectionsOnCombobox();
            DisplayUsersOnCombobox();

            btnSearch.OnClickAsync += BtnSearch_Click;
            btnPrint.OnClickAsync += BtnPrint_Click;
            btnExcel.OnClickAsync += BtnExcel_Click;
            cbMode.SelectedIndexChanged += CbMode_SelectedIndexChanged;
        }
        private void InitProperties(IAPIServer ApiServer, IPrinter printer, AppOption appOption,
                                    List<Collection> accessKeyCollection)
        {
            this.apiServer = ApiServer;
            this.collections = accessKeyCollection;
            this.printer = printer;
            this.appOption = appOption;
        }

      

        private static string GetModeDisplayString(EmRevenueMode mode)
        {
            return mode switch
            {
                EmRevenueMode.ByAccessKeyCollection => KZUIStyles.CurrentResources.RevenueByAccessKeyCollection,
                EmRevenueMode.ByLane => KZUIStyles.CurrentResources.RevenueByLane,
                EmRevenueMode.ByUser => KZUIStyles.CurrentResources.RevenueByUser,
                _ => "",
            };
        }
    }
}
