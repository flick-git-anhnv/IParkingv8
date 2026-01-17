using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Parkings;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Devices;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Users;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.API.Objects;
using IParkingv8.Printer;
using IParkingv8.Reporting;
using Kztek.Object;
using Kztek.Tool;
using System.ComponentModel;
using System.Data;

namespace iParkingv8.Reporting
{
    public partial class FrmReportInOut : Form, KzITranslate
    {
        #region Properties
        private long discountAmount = 0;

        private IEnumerable<Lane>? lanes = [];
        private List<Collection> collections = [];
        private List<User> users = [];
        private IAPIServer ApiServer;
        public Image? defaultImg = null;
        private IPrinter printer;
        private AppOption appOption;
        private List<CustomerGroup> customerGroups = [];
        #endregion End Properties

        #region Forms
        public FrmReportInOut(IAPIServer server, Image? defaultImg, IPrinter printer, AppOption appOption,
                              IEnumerable<Lane> lanes, List<Collection> collections)
        {
            InitializeComponent();
            this.KeyPreview = true;
            InitProperites(server, defaultImg, printer, appOption, lanes, collections);
            Translate();

            this.Load += FrmReportInOut_Load;
            this.KeyDown += FrmReportInOut_KeyDown;
            this.FormClosing += FrmReportInOut_FormClosing;
        }
        private async void FrmReportInOut_Load(object? sender, EventArgs e)
        {
            try
            {
                var userTask = ApiServer.UserService.GetUserDataAsync();
                var customerGroupTask = ApiServer.DataService.CustomerCollection.GetAllAsync();
                await Task.WhenAll(userTask, customerGroupTask);

                users = userTask?.Result?.Item1 ?? [];
                customerGroups = customerGroupTask?.Result?.Item1 ?? [];

                InitUI();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("ReportInOut", ex));
            }
            finally
            {
                GC.Collect();
            }
        }
        private void FrmReportInOut_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSearch.PerformClick();
                return;
            }
        }
        private void FrmReportInOut_FormClosing(object? sender, FormClosingEventArgs e)
        {
            dgvData.SelectionChanged -= DgvData_SelectionChanged;
            ClearData();
            this.Cursor = Cursors.Default;
        }
        #endregion End Forms

        #region Controls In Form
        private async void DgvData_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvData.CurrentCell == null)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string eventId = dgvData.CurrentRow?.Cells[colEventOutId.Name].Value.ToString() ?? "";
                if (string.IsNullOrEmpty(eventId))
                {
                    ShowErrorImage();
                    return;
                }

                var eventData = await this.ApiServer.ReportingService.Exit.GetExitByIdAsync(eventId);
                if (eventData == null || eventData.Images == null)
                {
                    ShowErrorImage();
                    return;
                }

                EventImageDto? overviewOutImageData = eventData.Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                EventImageDto? vehicleOutImageData = eventData.Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();

                EventImageDto? overviewInImageData = eventData.Entry?.Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                EventImageDto? vehicleInImageData = eventData.Entry?.Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
                picOverviewImageOut.ShowImage(await ImageHelper.GetImageFromUrlAsync(overviewOutImageData?.PresignedUrl) ?? defaultImg);
                picVehicleImageOut.ShowImage(await ImageHelper.GetImageFromUrlAsync(vehicleOutImageData?.PresignedUrl) ?? defaultImg);
                picOverviewImageIn.ShowImage(await ImageHelper.GetImageFromUrlAsync(overviewInImageData?.PresignedUrl) ?? defaultImg);
                picVehicleImageIn.ShowImage(await ImageHelper.GetImageFromUrlAsync(vehicleInImageData?.PresignedUrl) ?? defaultImg);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("ReportInOut", ex));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void DgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip ctx = new();
                ctx.Items.Add("Sửa ghi chú").Name = "UpdateNote";
                ctx.Items.Add("Sửa biển số vào").Name = "UpdatePlateIn";
                ctx.Items.Add("Sửa biển số ra").Name = "UpdatePlateOut";
                string pendingOrderId = dgvData.Rows[e.RowIndex].Cells[colInvoicePendingId.Name].Value.ToString() ?? "";
                if (!string.IsNullOrEmpty(pendingOrderId))
                {
                    ctx.Items.Add("Gửi hóa đơn").Name = "SendPendingEInvoice";
                }
                ctx.Font = new Font(dgvData.Font.Name, 16, FontStyle.Bold);
                ctx.BackColor = Color.DarkOrange;
                ctx.ItemClicked += (sender, ctx_e) =>
                {
                    string eventInId = dgvData.Rows[e.RowIndex].Cells[colEventInId.Name].Value.ToString() ?? "";
                    string eventOutId = dgvData.Rows[e.RowIndex].Cells[colEventOutId.Name].Value.ToString() ?? "";
                    string currentPlateIn = dgvData.Rows[e.RowIndex].Cells[colPlateIn.Name].Value.ToString() ?? "";
                    string currentPlateOut = dgvData.Rows[e.RowIndex].Cells[colPlateOut.Name].Value.ToString() ?? "";
                    string currentNote = dgvData.Rows[e.RowIndex].Cells[colNote.Name].Value.ToString() ?? "";
                    switch (ctx_e.ClickedItem?.Name?.ToString() ?? "")
                    {
                        case "UpdatePlateIn":
                            {
                                var frmUpdatePlate = new FrmEditPlate(currentPlateIn, eventInId, true, this.ApiServer);
                                if (frmUpdatePlate.ShowDialog() == DialogResult.OK)
                                {
                                    dgvData.Rows[e.RowIndex].Cells[colPlateIn.Name].Value = frmUpdatePlate.UpdatePlate;
                                    frmUpdatePlate.Dispose();
                                }
                            }
                            break;
                        case "UpdatePlateOut":
                            {
                                var frmUpdatePlate = new FrmEditPlate(currentPlateOut, eventOutId, false, this.ApiServer);
                                if (frmUpdatePlate.ShowDialog() == DialogResult.OK)
                                {
                                    dgvData.Rows[e.RowIndex].Cells[colPlateOut.Name].Value = frmUpdatePlate.UpdatePlate;
                                    frmUpdatePlate.Dispose();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                };
                var location = dgvData.PointToScreen(dgvData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location);
                ctx.Show(dgvData, new Point(location.X - splitContainer1.Location.X - this.Location.X, location.Y - splitContainer1.Location.Y - this.Location.Y));
            }
        }

        private void Pic_LoadCompleted(object? sender, AsyncCompletedEventArgs e)
        {
            PictureBox pictureBox = (sender as PictureBox)!;
            if (e.Error != null)
            {
                pictureBox.Image = defaultImg;
            }
        }

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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Event Out Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                dgvData.SelectionChanged += DgvData_SelectionChanged;
                this.Cursor = Cursors.Default;
            }
            return true;
        }

        private async Task<bool> GetReportData(int pageIndex)
        {
            string keyword = txtKeyword.Text;
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            string vehicleTypeId = cbVehicleType.SelectedItem == null ? "" : ((ListItem)cbVehicleType.SelectedItem).Value ?? "";
            string identityGroupId = cbIdentityGroup.SelectedItem == null ? "" : ((ListItem)cbIdentityGroup.SelectedItem).Value ?? "";
            string laneId = cbLane.SelectedItem == null ? "" : ((ListItem)cbLane.SelectedItem).Value ?? "";
            string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

            var report = await ApiServer.ReportingService.Exit.GetExitDataAsync(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId, user, true, pageIndex, Filter.PAGE_SIZE);
            if (report == null)
            {
                MessageBox.Show("Không tải được thông tin xe đã ra. Vui lòng thử lại");
                return false;
            }
            var eventOutData = report.Data;
            if (eventOutData == null)
            {
                MessageBox.Show("Không tải được thông tin xe đã ra. Vui lòng thử lại");
                return false;
            }
            ucNavigator1.KZUI_MaxPage = report.TotalPage;
            ucNavigator1.KZUI_CurrentPage = pageIndex + 1;
            ucNavigator1.KZUI_TotalRecord = report.TotalCount;
            discountAmount = report.AdditionalData!.TotalDiscountAmount;
            long prepaid = report.AdditionalData.TotalPrepaid;
            long realMoney = report.AdditionalData.TotalAmount - report.AdditionalData.TotalDiscountAmount - prepaid;
            if (realMoney < 0)
            {
                realMoney = 0;
            }
            lblTotalEvents.Visible = true;
            lblTotalEvents.Text = report.TotalCount + $" {KZUIStyles.CurrentResources.Vehicles}: " + report.AdditionalData.TotalAmount.ToString("N0") + $"; {KZUIStyles.CurrentResources.Discount}: " + discountAmount.ToString("N0") + $"; {KZUIStyles.CurrentResources.Paid}: " + prepaid.ToString("N0") + $"; {KZUIStyles.CurrentResources.RealFee}: " + realMoney.ToString("N0");
            lblTotalEvents.Refresh();

            DisplayEventOutData(eventOutData);
            eventOutData.Clear();
            return true;
        }

        private async Task<bool> BtnPrintPhieuThu_Click(object? sender)
        {
            try
            {
                if (dgvData.Rows.Count == 0)
                {
                    return false;
                }
                if (dgvData.CurrentRow == null)
                {
                    MessageBox.Show("Hãy chọn bản ghi cần in phiếu thu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                string printTemplatePath = IparkingingPathManagement.appPrintTemplateConfigPath(((EmPrintTemplate)this.appOption.PrintTemplate).ToString());

                if (File.Exists(printTemplatePath))
                {
                    bool isConfirm = MessageBox.Show("Bạn có muốn in phiếu thu?", "In phiếu thu", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;
                    if (!isConfirm)
                    {
                        return false;
                    }
                    string timeIn = dgvData.CurrentRow.Cells[colTimeIn.Name].Value.ToString() ?? "";
                    string timeOut = dgvData.CurrentRow.Cells[colTimeOut.Name].Value.ToString() ?? "";
                    string plate = dgvData.CurrentRow.Cells[colPlateOut.Name].Value.ToString() ?? "";
                    string chargeStr = dgvData.CurrentRow.Cells[colRealFee.Name].Value.ToString() ?? "";
                    string cardName = dgvData.CurrentRow.Cells[colAccessKeyCode.Name].Value.ToString() ?? "";
                    string cardGroupName = dgvData.CurrentRow.Cells[colAccessKeyCollection.Name].Value.ToString() ?? "";

                    printer.PrintPhieuThu(File.ReadAllText(printTemplatePath), cardName, cardGroupName, null,
                                                         DateTime.Parse(timeIn), DateTime.Parse(timeOut),
                                                         plate, TextFormatingTool.GetMoneyFormat(chargeStr.Replace(".", "").Trim()),
                                                         int.Parse(chargeStr.Replace(".", "").Trim()));
                }
                else
                {
                    MessageBox.Show("Không tìm thấy mẫu in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Print Ticket", ex, EmSystemActionType.ERROR));
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        private async Task<bool> BtnExportExcel_Click(object? sender)
        {
            string keyword = txtKeyword.Text;
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            string vehicleTypeId = cbVehicleType.SelectedItem == null ? "" : ((ListItem)cbVehicleType.SelectedItem).Value ?? "";
            string identityGroupId = cbIdentityGroup.SelectedItem == null ? "" : ((ListItem)cbIdentityGroup.SelectedItem).Value ?? "";
            string laneId = cbLane.SelectedItem == null ? "" : ((ListItem)cbLane.SelectedItem).Value ?? "";
            string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

            try
            {
                var report = await ApiServer.ReportingService.Exit.GetExitDataAsync(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId, user, false, 0, Filter.PAGE_SIZE, "", -1, isSaveLog: true);

                if (report != null)
                {
                    DataGridView dgvExport = new();
                    foreach (DataGridViewColumn column in dgvData.Columns)
                    {
                        DataGridViewColumn newColumn = (DataGridViewColumn)column.Clone();

                        dgvExport.Columns.Add(newColumn);
                    }
                    DisplayExportData(report.Data, dgvExport);

                    ExcelTools.CreatReportFile(dgvExport, KZUIStyles.CurrentResources.MiReportOut, new List<string>() { lblTotalEvents.Text });
                    report.Data.Clear();
                    dgvExport.Dispose();
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.Close();
            return true;
        }

        private async void UcPages1_OnpageSelect(int pageIndex)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Reset();
                await GetReportData(pageIndex - 1);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Event Out Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                dgvData.SelectionChanged += DgvData_SelectionChanged;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Private Function
        private void Reset()
        {
            dgvData.SelectionChanged -= DgvData_SelectionChanged;
            dgvData.CurrentCell = null;

            picOverviewImageOut.Image = defaultImg;
            picVehicleImageOut.Image = defaultImg;
            picOverviewImageIn.Image = defaultImg;
            picVehicleImageIn.Image = defaultImg;
        }
        private void DisplayEventOutData(List<ExitData> eventOutData)
        {
            try
            {
                dgvData.Rows.Clear();
                List<DataGridViewRow> rows = [];
                foreach (var item in eventOutData)
                {
                    DataGridViewRow row = new();
                    this.Invoke(new Action(() =>
                    {
                        row.CreateCells(dgvData);
                    }));
                    DateTime? timeIn = item.Entry?.DateTimeIn;
                    string moneyStr = TextFormatingTool.GetMoneyFormat(item.Amount.ToString()).Replace(" VND", "");

                    long totalDiscount = item.DiscountAmount;
                    string discountStr = TextFormatingTool.GetMoneyFormat(totalDiscount.ToString()).Replace(" VND", "");
                    var tra_truoc = item.Entry?.Amount ?? 0;
                    long realFee = Math.Max(0, item.Amount - totalDiscount - tra_truoc);
                    string realFeeStr = TextFormatingTool.GetMoneyFormat(realFee.ToString()).Replace(" VND", "");

                    row.Cells[dgvData.Columns[colEventOutId.Name].Index].Value = item.Id;
                    row.Cells[dgvData.Columns[colEventInId.Name].Index].Value = item.Entry?.Id ?? "";
                    row.Cells[dgvData.Columns[colInvoicePendingId.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colInvoiceId.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colFileKeyIn.Name].Index].Value = item.Entry?.Images == null ?
                                                                               "[]" : Newtonsoft.Json.JsonConvert.SerializeObject(item.Entry?.Images);
                    row.Cells[dgvData.Columns[colFileKeyOut.Name].Index].Value = item.Images == null ?
                                                                               "[]" : Newtonsoft.Json.JsonConvert.SerializeObject(item.Images);
                    row.Cells[dgvData.Columns[colIndex.Name].Index].Value = rows.Count + 1;
                    row.Cells[dgvData.Columns[colPlateIn.Name].Index].Value = item.Entry?.PlateNumber;
                    row.Cells[dgvData.Columns[colPlateOut.Name].Index].Value = item.PlateNumber;
                    row.Cells[dgvData.Columns[colTimeIn.Name].Index].Value = timeIn.ToVNTime();
                    row.Cells[dgvData.Columns[colTimeOut.Name].Index].Value = item.DatetimeOut.ToVNTime();
                    row.Cells[dgvData.Columns[colParkingTime.Name].Index].Value = item.DatetimeOut - item.Entry?.DateTimeIn;
                    row.Cells[dgvData.Columns[colAccessKeyCollection.Name].Index].Value = item.CollecionName;
                    row.Cells[dgvData.Columns[colFee.Name].Index].Value = moneyStr;
                    row.Cells[dgvData.Columns[colDiscount.Name].Index].Value = discountStr;
                    row.Cells[dgvData.Columns[colRealFee.Name].Index].Value = realFeeStr;
                    row.Cells[dgvData.Columns[colPaid.Name].Index].Value = item.Entry?.Amount ?? 0;

                    row.Cells[dgvData.Columns[colAccessKeyCode.Name].Index].Value = item.AccessKey?.Code ?? "";
                    row.Cells[dgvData.Columns[colAccessKeyName.Name].Index].Value = item.AccessKey?.Name ?? "";
                    row.Cells[dgvData.Columns[colUserIn.Name].Index].Value = item.Entry?.CreatedBy;
                    row.Cells[dgvData.Columns[colUserOut.Name].Index].Value = item.CreatedBy;
                    row.Cells[dgvData.Columns[colInvoiceTemplate.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colInvoiceNo.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colLaneIn.Name].Index].Value = item.Entry?.Device?.Name ?? "";
                    row.Cells[dgvData.Columns[colLaneOut.Name].Index].Value = item.Device?.Name ?? "";
                    row.Cells[dgvData.Columns[colNote.Name].Index].Value = item.Entry?.Note;
                    rows.Add(row);
                }
                this.Invoke(new Action(() =>
                {
                    dgvData.Rows.AddRange([.. rows]);
                    if (dgvData.Rows.Count > 0)
                    {
                        dgvData.CurrentCell = dgvData.Rows[0].Cells[colIndex.Name];
                    }
                    else
                    {
                        dgvData.CurrentCell = null;
                    }
                    DgvData_SelectionChanged(null, EventArgs.Empty);
                }));
                eventOutData.Clear();
            }
            catch (Exception)
            {
            }
        }
        private void DisplayExportData(List<ExitData> eventOutData, DataGridView dgv)
        {
            try
            {
                List<DataGridViewRow> rows = [];
                foreach (var item in eventOutData)
                {
                    DataGridViewRow row = new();
                    this.Invoke(new Action(() =>
                    {
                        row.CreateCells(dgv);
                    }));
                    DateTime? timeIn = item.Entry?.DateTimeIn;
                    string moneyStr = TextFormatingTool.GetMoneyFormat(item.Amount.ToString()).Replace(" VND", "");

                    long totalDiscount = item.DiscountAmount;
                    string discountStr = TextFormatingTool.GetMoneyFormat(totalDiscount.ToString()).Replace(" VND", "");
                    var tra_truoc = item.Entry?.Amount ?? 0;
                    long realFee = Math.Max(0, item.Amount - totalDiscount - tra_truoc);
                    string realFeeStr = TextFormatingTool.GetMoneyFormat(realFee.ToString()).Replace(" VND", "");

                    row.Cells[dgvData.Columns[colEventOutId.Name].Index].Value = item.Id;
                    row.Cells[dgvData.Columns[colEventInId.Name].Index].Value = item.Entry?.Id ?? "";
                    row.Cells[dgvData.Columns[colInvoicePendingId.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colInvoiceId.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colFileKeyIn.Name].Index].Value = item.Entry?.Images == null ?
                                                                               "[]" : Newtonsoft.Json.JsonConvert.SerializeObject(item.Entry?.Images);
                    row.Cells[dgvData.Columns[colFileKeyOut.Name].Index].Value = item.Images == null ?
                                                                               "[]" : Newtonsoft.Json.JsonConvert.SerializeObject(item.Images);
                    row.Cells[dgvData.Columns[colIndex.Name].Index].Value = rows.Count + 1;
                    row.Cells[dgvData.Columns[colPlateIn.Name].Index].Value = item.Entry?.PlateNumber;
                    row.Cells[dgvData.Columns[colPlateOut.Name].Index].Value = item.PlateNumber;
                    row.Cells[dgvData.Columns[colTimeIn.Name].Index].Value = timeIn.ToVNTime();
                    row.Cells[dgvData.Columns[colTimeOut.Name].Index].Value = item.DatetimeOut.ToVNTime();
                    row.Cells[dgvData.Columns[colParkingTime.Name].Index].Value = item.DatetimeOut - item.Entry?.DateTimeIn;
                    row.Cells[dgvData.Columns[colAccessKeyCollection.Name].Index].Value = item.CollecionName;
                    row.Cells[dgvData.Columns[colFee.Name].Index].Value = moneyStr;
                    row.Cells[dgvData.Columns[colDiscount.Name].Index].Value = discountStr;
                    row.Cells[dgvData.Columns[colRealFee.Name].Index].Value = realFeeStr;
                    row.Cells[dgvData.Columns[colPaid.Name].Index].Value = item.Entry?.Amount ?? 0;

                    row.Cells[dgvData.Columns[colAccessKeyCode.Name].Index].Value = item.AccessKey?.Code ?? "";
                    row.Cells[dgvData.Columns[colAccessKeyName.Name].Index].Value = item.AccessKey?.Name ?? "";
                    row.Cells[dgvData.Columns[colUserIn.Name].Index].Value = item.Entry?.CreatedBy;
                    row.Cells[dgvData.Columns[colUserOut.Name].Index].Value = item.CreatedBy;
                    row.Cells[dgvData.Columns[colInvoiceTemplate.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colInvoiceNo.Name].Index].Value = "";
                    row.Cells[dgvData.Columns[colLaneIn.Name].Index].Value = item.Entry?.Device?.Name ?? "";
                    row.Cells[dgvData.Columns[colLaneOut.Name].Index].Value = item.Device?.Name ?? "";
                    row.Cells[dgvData.Columns[colNote.Name].Index].Value = item.Entry?.Note;
                    rows.Add(row);
                }
                this.Invoke(new Action(() =>
                {
                    dgv.Rows.AddRange([.. rows]);
                    if (dgv.Rows.Count > 0)
                    {
                        dgv.CurrentCell = dgv.Rows[0].Cells[colIndex.Name];
                    }
                    else
                    {
                        dgv.CurrentCell = null;
                    }
                    DgvData_SelectionChanged(null, EventArgs.Empty);
                }));
                eventOutData.Clear();
            }
            catch (Exception)
            {
            }
        }

        private void ChangeSearchConditionEvent(object? sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

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
        private void DisplayCollectionsOnCombobox()
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
        private void DisplayLanesOnCombobox()
        {
            cbLane.Invoke(new Action(() =>
            {
                cbLane.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            lanes = [.. lanes.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];
            cbLane.Invoke(new Action(() =>
            {
                foreach (var item in lanes)
                {
                    ListItem laneItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id
                    };
                    cbLane.Items.Add(laneItem);
                }
                cbLane.SelectedIndex = 0;
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

            customerGroups = [.. customerGroups.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];
            cbCustomerGroup.Invoke(new Action(() =>
            {
                foreach (var item in customerGroups)
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
        #endregion End Private Function

        public void Translate()
        {
            //---Button
            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnPrintPhieuThu.Text = KZUIStyles.CurrentResources.Print;
            btnExportExcel.Text = KZUIStyles.CurrentResources.ExportExcel;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;

            //---Title search 
            lblKeywordTitle.Text = KZUIStyles.CurrentResources.Keyword;
            lblLaneTitle.Text = KZUIStyles.CurrentResources.Lane;
            lblUserTitle.Text = KZUIStyles.CurrentResources.User;
            lblCustomerCollectionTitle.Text = KZUIStyles.CurrentResources.CustomerCollection;
            lblStartTimeTitle.Text = KZUIStyles.CurrentResources.StartTime;
            lblEndTimeTitle.Text = KZUIStyles.CurrentResources.EndTime;
            lblAccessKeyCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
            lblVehicleTypeTitle.Text = KZUIStyles.CurrentResources.VehicleType;
            lblTotalEvents.Text = "";

            //---Datagridview
            colPlateIn.HeaderText = KZUIStyles.CurrentResources.PlateIn;
            colPlateOut.HeaderText = KZUIStyles.CurrentResources.PlateOut;

            colTimeIn.HeaderText = KZUIStyles.CurrentResources.TimeIn;
            colTimeOut.HeaderText = KZUIStyles.CurrentResources.TimeOut;
            colParkingTime.HeaderText = KZUIStyles.CurrentResources.Duration;

            colAccessKeyCollection.HeaderText = KZUIStyles.CurrentResources.AccessKeyCollection;
            colFee.HeaderText = KZUIStyles.CurrentResources.Fee;
            colDiscount.HeaderText = KZUIStyles.CurrentResources.Discount;
            colPaid.HeaderText = KZUIStyles.CurrentResources.Paid;
            colRealFee.HeaderText = KZUIStyles.CurrentResources.RealFee;

            colAccessKeyName.HeaderText = KZUIStyles.CurrentResources.AccesskeyName;
            colAccessKeyCode.HeaderText = KZUIStyles.CurrentResources.AccesskeyCode;
            colUserIn.HeaderText = KZUIStyles.CurrentResources.UserIn;
            colUserOut.HeaderText = KZUIStyles.CurrentResources.UserOut;

            colLaneIn.HeaderText = KZUIStyles.CurrentResources.LaneIn;
            colLaneOut.HeaderText = KZUIStyles.CurrentResources.LaneOut;

            colInvoiceTemplate.HeaderText = KZUIStyles.CurrentResources.InvoiceTemplate;
            colInvoiceNo.HeaderText = KZUIStyles.CurrentResources.InvoiceNo;

            colNote.HeaderText = KZUIStyles.CurrentResources.colNote;
        }
        private void InitUI()
        {
            try
            {
                this.SuspendLayout();
                var now = DateTime.Now;
                dtpStartTime.Value = new DateTime(now.Year, now.Month, now.Day, 7, 0, 0);
                dtpEndTime.Value = new DateTime(now.AddDays(1).Year, now.AddDays(1).Month, now.AddDays(1).Day, 6, 59, 59);

                picOverviewImageIn.Image = picOverviewImageIn.ErrorImage = defaultImg;
                picVehicleImageIn.Image = picVehicleImageIn.ErrorImage = defaultImg;
                picOverviewImageOut.Image = picOverviewImageOut.ErrorImage = defaultImg;
                picVehicleImageOut.Image = picVehicleImageOut.ErrorImage = defaultImg;

                cbVehicleType.DisplayMember = cbIdentityGroup.DisplayMember = cbLane.DisplayMember = cbUser.DisplayMember = cbCustomerGroup.DisplayMember = "Name";
                cbVehicleType.ValueMember = cbIdentityGroup.ValueMember = cbLane.ValueMember = cbUser.ValueMember = cbCustomerGroup.ValueMember = "Value";

                dgvData.Columns[colInvoiceNo.Name].Visible = false;
                dgvData.Columns[colInvoiceTemplate.Name].Visible = false;

                DisplayVehicleTypesOnCombobox();
                DisplayCollectionsOnCombobox();
                DisplayLanesOnCombobox();
                DisplayUsersOnCombobox();
                DisplayCustomerGroupsOnCombobox();
                ucNavigator1.Reset();
                this.ResumeLayout(false);
                this.PerformLayout();

                btnCancel.OnClickAsync += BtnCancel_Click;
                btnExportExcel.OnClickAsync += BtnExportExcel_Click;
                btnSearch.OnClickAsync += BtnSearch_Click;
                btnPrintPhieuThu.OnClickAsync += BtnPrintPhieuThu_Click;

                dgvData.SelectionChanged += DgvData_SelectionChanged;
                ucNavigator1.onPageChangeEvent += UcPages1_OnpageSelect;

                cbVehicleType.SelectedIndexChanged += ChangeSearchConditionEvent;
                cbIdentityGroup.SelectedIndexChanged += ChangeSearchConditionEvent;
                cbLane.SelectedIndexChanged += ChangeSearchConditionEvent;
                cbUser.SelectedIndexChanged += ChangeSearchConditionEvent;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Create UI", ex, EmSystemActionType.ERROR));
            }
        }
        private void InitProperites(IAPIServer ApiServer, Image? defaultImg, IPrinter printer, AppOption appOption,
                                    IEnumerable<Lane> lanes, List<Collection> collections)
        {
            this.ApiServer = ApiServer;
            this.defaultImg = defaultImg;
            this.printer = printer;
            this.appOption = appOption;
            this.lanes = lanes;
            this.collections = collections;
        }
        private void ClearData()
        {
            users?.Clear();
            customerGroups.Clear();
            dgvData.Rows.Clear();
        }
        private void ShowErrorImage()
        {
            picOverviewImageOut.ShowImage(defaultImg);
            picVehicleImageOut.ShowImage(defaultImg);
            picOverviewImageIn.ShowImage(defaultImg);
            picVehicleImageIn.ShowImage(defaultImg);
        }
    }
}
