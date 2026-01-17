using iParkingv8.Object;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Users;
using iParkingv8.Ultility;
using IParkingv8.API.Interfaces;
using IParkingv8.Printer;
using Kztek.Object;
using Kztek.Tool;
using System.Data;

namespace IParkingv8.Reporting
{
    public partial class frmShiftHandOver : Form
    {
        private List<User> users = [];
        private readonly IAPIServer apiServer;
        public Image? defaultImg = null;
        private readonly IPrinter printer;
        private readonly AppOption appOption;
        private List<string> collectionName = new List<string>();
        #region Forms
        public frmShiftHandOver(IAPIServer ApiServer, IPrinter printer, AppOption appOption, List<Collection> collections)
        {
            InitializeComponent();
            this.apiServer = ApiServer;
            this.printer = printer;
            this.appOption = appOption;

            foreach (var item in collections)
            {
                collectionName.Add(item.Name);
            }

            this.KeyPreview = true;
            this.KeyDown += FrmRevenueByIdentityGroup_KeyDown;
            dtpStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            btnSearch.Click += BtnSearch_Click;
            this.Load += FrmRevenueByIdentityGroup_Load;
        }
        private async void FrmRevenueByIdentityGroup_Load(object? sender, EventArgs e)
        {
            dtpStartTime.Value = StaticPool.LoginTime;
            dtpEndTime.Value = DateTime.Now;
            cbUser.DisplayMember = "Name";
            cbUser.ValueMember = "Value";

            var userTask = this.apiServer.UserService.GetUserDataAsync();
            users = (await userTask)?.Item1 ?? [];
            LoadUsers();
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
        private async void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                if (appOption.PrintTemplate == (int)EmPrintTemplate.GoldenWestlake)
                {
                    StaticPool.LoginTime = DateTime.Now;
                }
                string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

                var startTime = dtpStartTime.Value;
                var endTime = dtpEndTime.Value;

                var data = await this.apiServer.ReportingService.Revenue.GetShiftHandOverReport(this.collectionName, startTime, endTime, user);
                if (data == null)
                {
                    MessageBox.Show("Không tải được thông tin doanh thu. Vui lòng thử lại");
                    return;
                }

                dgvData.Rows.Clear();

                long totalVao = 0;
                long totalRa = 0;
                long totalAmount = 0;
                long totalRealFee = 0;
                long totalDiscount = 0;
                foreach (var item in data.Report)
                {
                    totalVao += item.Value.Vao;
                    totalRa += item.Value.Ra;

                    totalAmount += item.Value.Amount;
                    totalRealFee += item.Value.RealFee;
                    totalDiscount += item.Value.Discount;

                    dgvData.Rows.Add(dgvData.Rows.Count + 1, item.Key, item.Value.Vao, item.Value.Ra,
                                                             item.Value.Amount, item.Value.RealFee, item.Value.Discount);
                }
                dgvData.Rows.Add("", "Tổng", totalVao, totalRa, totalAmount, totalRealFee, totalDiscount);
            }
            catch (Exception)
            {
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExcelTools.CreatReportFile(dgvData, "Báo cáo doanh thu", new List<string>() { "Thực thu bảo vệ : " + numRealFee.Value.ToString("N0") });
        }
        private async void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnPrint.Enabled = false;
                string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

                var startTime = dtpStartTime.Value;
                var endTime = dtpEndTime.Value;

                var data = await this.apiServer.ReportingService.Revenue.GetShiftHandOverReport(this.collectionName, startTime, endTime, user);
                this.Cursor = Cursors.Default;
                btnPrint.Enabled = true;
                if (data == null)
                {
                    MessageBox.Show("Không tải được thông tin doanh thu. Vui lòng thử lại");
                    return;
                }
                string printTemplatePath = IparkingingPathManagement.appShiftHandOver(((EmPrintTemplate)this.appOption.PrintTemplate).ToString());

                if (File.Exists(printTemplatePath))
                {
                    //bool isConfirm = MessageBox.Show("Bạn có muốn in doanh thu", "In doanh thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
                    //if (!isConfirm)
                    //{
                    //    return;
                    //}

                    string printContent = printer.PrintShiftHandOver(File.ReadAllText(printTemplatePath), data, user, startTime, endTime, (long)numRealFee.Value);
                    new frmPrintPreview(printContent, 1).ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mẫu in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Print Revenue", ex, EmSystemActionType.ERROR));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnPrint.Enabled = true;
            }
        }
        #endregion

        #region Private Functions
        private void LoadUsers()
        {
            //cbUser.Invoke(new Action(() =>
            //{
            //    cbUser.Items.Add(new ListItem()
            //    {
            //        Name = "Tất cả",
            //        Value = ""
            //    });
            //}));

            users = [.. users.OrderBy(x => x.Upn).ThenBy(x => x.Upn.Length)];
            cbUser.Invoke(new Action(() =>
            {
                foreach (var item in users)
                {
                    if (item.Upn != StaticPool.SelectedUser.Upn)
                    {
                        continue;
                    }
                    ListItem laneItem = new()
                    {
                        Name = item.Upn,
                        Value = item.Id
                    };
                    cbUser.Items.Add(laneItem);
                }
                cbUser.SelectedIndex = cbUser.FindStringExact(StaticPool.SelectedUser?.Upn ?? "");
            }));
        }
        #endregion


    }
}