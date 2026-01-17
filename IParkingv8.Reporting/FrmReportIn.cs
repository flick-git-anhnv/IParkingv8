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
using IParkingv8.Reporting;
using Kztek.Object;
using Kztek.Tool;
using System.ComponentModel;
using System.Data;

namespace iParkingv8.Reporting
{
    public partial class FrmReportIn : Form, KzITranslate
    {
        #region Properties
        private IEnumerable<Lane> lanes = [];
        private List<Collection> identityGroups = [];
        private List<User> users = [];
        private List<CustomerGroup> customerGroups = [];
        private Image? defaultImg = null;
        public IAPIServer ApiServer;

        private bool isAllowSelect = false;

        public string selectedEventId = string.Empty;
        public string selectedIdentityId = string.Empty;
        public string selectedPlateNumber = string.Empty;
        string lastSelectedId = "";
        #endregion End Properties

        #region Forms
        public FrmReportIn(Image? defaultImage, IAPIServer ApiServer, IEnumerable<Lane> lanes, bool isAllowSelect = false)
        {
            InitializeComponent();

            this.KeyPreview = true;
            InitPropeties(defaultImage, ApiServer, lanes, isAllowSelect);
            Translate();

            this.Load += FrmReportIn_Load;
            this.KeyDown += FrmReportIn_KeyDown;
            this.FormClosing += FrmReportIn_FormClosing;
        }
        private async void FrmReportIn_Load(object? sender, EventArgs e)
        {
            var userTask = ApiServer.UserService.GetUserDataAsync();
            var identityGroupTask = ApiServer.DataService.AccessKeyCollection.GetAllAsync();
            var customerGroupTask = ApiServer.DataService.CustomerCollection.GetAllAsync();
            await Task.WhenAll(userTask, identityGroupTask, customerGroupTask);

            users = userTask?.Result?.Item1 ?? [];
            identityGroups = identityGroupTask?.Result?.Item1 ?? [];
            customerGroups = customerGroupTask?.Result?.Item1 ?? [];

            InitUI();

            if (this.isAllowSelect)
            {
                dgvData1.CellDoubleClick += DgvData_CellDoubleClick;
            }

            picOverviewImageIn.LoadCompleted += Pic_LoadCompleted;
            picVehicleImageIn.LoadCompleted += Pic_LoadCompleted;

            cbVehicleType.SelectedIndexChanged += ChangeSearchConditionEvent;
            cbIdentityGroupType.SelectedIndexChanged += ChangeSearchConditionEvent;
            cbLane.SelectedIndexChanged += ChangeSearchConditionEvent;
            cbUser.SelectedIndexChanged += ChangeSearchConditionEvent;
        }
        private void FrmReportIn_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSearch.PerformClick();
                return;
            }
        }
        private void FrmReportIn_FormClosing(object? sender, FormClosingEventArgs e)
        {
            dgvData1.SelectionChanged -= DgvData_SelectionChanged;
            ClearData();
            this.Cursor = Cursors.Default;
        }
        #endregion End Forms

        #region Controls In Form
        private async void DgvData_SelectionChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvData1.CurrentCell == null)
                {
                    return;
                }

                string eventId = dgvData1.CurrentRow?.Cells[colEventId.Name].Value.ToString() ?? "";
                if (string.IsNullOrEmpty(eventId))
                {
                    ShowErrorImage();
                    return;
                }
                if (lastSelectedId == eventId)
                {
                    return;
                }
                lastSelectedId = eventId;
                var eventData = await this.ApiServer.ReportingService.Entry.GetEntryByIdAsync(eventId);
                if (eventData == null || eventData.Images == null)
                {
                    ShowErrorImage();
                    return;
                }

                EventImageDto? overviewImageData = eventData.Images.Where(e => e.Type == EmImageType.PANORAMA).FirstOrDefault();
                EventImageDto? vehicleImageData = eventData.Images.Where(e => e.Type == EmImageType.VEHICLE).FirstOrDefault();
                picOverviewImageIn.ShowImage(await ImageHelper.GetImageFromUrlAsync(overviewImageData?.PresignedUrl) ?? defaultImg);
                picVehicleImageIn.ShowImage(await ImageHelper.GetImageFromUrlAsync(vehicleImageData?.PresignedUrl) ?? defaultImg);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Display Selected Event In", ex, EmSystemActionType.ERROR));
            }
            finally
            {
            }
        }
        private void DgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip ctx = new();
                ctx.Items.Add(KZUIStyles.CurrentResources.EditNoteInfor).Name = "UpdateNote";
                ctx.Items.Add(KZUIStyles.CurrentResources.EditPlateInfo).Name = "UpdatePlateIn";

                ctx.Font = new Font(dgvData1.Font.Name, 16, FontStyle.Bold);
                ctx.BackColor = Color.DarkOrange;
                ctx.ItemClicked += (sender, ctx_e) =>
                {
                    string id = dgvData1.Rows[e.RowIndex].Cells[colEventId.Name].Value.ToString() ?? "";
                    string currentPlateIn = dgvData1.Rows[e.RowIndex].Cells[colPlateIn.Name].Value?.ToString() ?? "";
                    string currentNote = dgvData1.Rows[e.RowIndex].Cells[colNote.Name].Value?.ToString() ?? "";
                    string timeIn = dgvData1.Rows[e.RowIndex].Cells[colTimeIn.Name].Value?.ToString() ?? "";
                    string identityName = dgvData1.Rows[e.RowIndex].Cells[colAccessKeyName.Name].Value?.ToString() ?? "";
                    switch (ctx_e?.ClickedItem?.Name ?? "".ToString())
                    {
                        case "UpdatePlateIn":
                            {
                                var frmUpdatePlate = new FrmEditPlate(currentPlateIn, id, true, this.ApiServer);
                                if (frmUpdatePlate.ShowDialog() == DialogResult.OK)
                                {
                                    dgvData1.Rows[e.RowIndex].Cells[colPlateIn.Name].Value = frmUpdatePlate.UpdatePlate;
                                    frmUpdatePlate.Dispose();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                };
                var location = dgvData1.PointToScreen(dgvData1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location);
                ctx.Show(dgvData1, new Point(location.X - splitContainer1.Location.X - this.Location.X, location.Y - splitContainer1.Location.Y - this.Location.Y));
                ctx.Show();
            }

        }
        private void DgvData_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            this.selectedIdentityId = dgvData1.Rows[e.RowIndex].Cells[colAccessKeyId.Name].Value?.ToString() ?? "";
            this.selectedPlateNumber = dgvData1.Rows[e.RowIndex].Cells[colPlateIn.Name].Value?.ToString() ?? "";

            this.DialogResult = DialogResult.OK;
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
                await GetReportData(0);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Event In Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                dgvData1.SelectionChanged += DgvData_SelectionChanged;
                this.Cursor = Cursors.Default;
            }
            return true;
        }
        private async Task<bool> BtnFee_Click(object? sender)
        {
            if (dgvData1.CurrentCell == null)
            {
                return false;
            }

            string eventId = dgvData1.CurrentRow?.Cells[colEventId.Name].Value.ToString() ?? "";
            if (string.IsNullOrEmpty(eventId))
            {
                ShowErrorImage();
                return false;
            }

            var eventData = await this.ApiServer.ReportingService.Entry.GetEntryByIdAsync(eventId);
            if (eventData == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(eventData.CollecionID))
            {
                return false;
            }
            var fee = await this.ApiServer.OperatorService.Entry.CheckFeeAsync(eventData.CollecionID, eventData.DateTimeIn);
            MessageBox.Show($"{KZUIStyles.CurrentResources.Fee}: {fee:N0}", KZUIStyles.CurrentResources.InfoTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private async Task<bool> BtnExportExcel_Click(object? sender)
        {
            string keyword = txtKeyword.Text;
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            string vehicleTypeId = cbVehicleType.SelectedItem == null ? "" : ((ListItem)cbVehicleType.SelectedItem).Value;
            string identityGroupId = cbIdentityGroupType.SelectedItem == null ? "" : ((ListItem)cbIdentityGroupType.SelectedItem).Value;
            string laneId = cbLane.SelectedItem == null ? "" : ((ListItem)cbLane.SelectedItem).Value;
            string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;
            string customerCollectionId = cbCustomerGroup.SelectedItem == null ? "" : ((ListItem)cbCustomerGroup.SelectedItem)?.Value ?? "";

            try
            {
                var report = await ApiServer.ReportingService.Entry.GetEntryDataAsync(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId, user, customerCollectionId, false,
                                                                          pageIndex: 0, pageSize: Filter.PAGE_SIZE, "", -1, isSaveLog: true);
                if (report != null)
                {
                    DataGridView dgvExport = new();
                    foreach (DataGridViewColumn column in dgvData1.Columns)
                    {
                        DataGridViewColumn newColumn = (DataGridViewColumn)column.Clone();

                        dgvExport.Columns.Add(newColumn);
                    }
                    DisplayExportData(report.Data, dgvExport);

                    ExcelTools.CreatReportFile(dgvExport, KZUIStyles.CurrentResources.MiReportIn, new List<string>() { lblTotalEvents.Text });
                    report.Data.Clear();
                    dgvExport.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                GC.Collect();
            }
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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Event In Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                dgvData1.SelectionChanged += DgvData_SelectionChanged;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion End Controls In Form

        #region Private Function
        private void Reset()
        {
            lastSelectedId = "";

            dgvData1.SelectionChanged -= DgvData_SelectionChanged;
            dgvData1.CurrentCell = null;

            picOverviewImageIn.Image = defaultImg;
            picVehicleImageIn.Image = defaultImg;
        }
        private async Task<bool> GetReportData(int pageIndex)
        {
            string keyword = txtKeyword.Text;
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            string vehicleTypeId = cbVehicleType.SelectedItem == null ? "" : ((ListItem)cbVehicleType.SelectedItem)?.Value ?? "";
            string identityGroupId = cbIdentityGroupType.SelectedItem == null ? "" : ((ListItem)cbIdentityGroupType.SelectedItem)?.Value ?? "";
            string customerCollectionId = cbCustomerGroup.SelectedItem == null ? "" : ((ListItem)cbCustomerGroup.SelectedItem)?.Value ?? "";
            string laneId = cbLane.SelectedItem == null ? "" : ((ListItem)cbLane.SelectedItem)?.Value ?? "";
            string user = cbUser.SelectedItem == null ? "" : string.IsNullOrEmpty(((ListItem)cbUser.SelectedItem).Value) ? "" : cbUser.Text;

            var report = await ApiServer.ReportingService.Entry.GetEntryDataAsync(keyword, startTime, endTime, identityGroupId, vehicleTypeId, laneId,
                                                                                  user, customerCollectionId, true, pageIndex, Filter.PAGE_SIZE);
            if (report == null)
            {
                MessageBox.Show("Không tải được thông tin xe trong bãi. Vui lòng thử lại");
                return false;
            }

            var eventInReports = report.Data ?? [];

            ucNavigator1.KZUI_MaxPage = report.TotalPage;
            ucNavigator1.KZUI_CurrentPage = pageIndex + 1;
            ucNavigator1.KZUI_TotalRecord = report.TotalCount;

            lblTotalEvents.Visible = true;
            lblTotalEvents.Text = $"{KZUIStyles.CurrentResources.Total}: {report.TotalCount:N0}";
            lblTotalEvents.Refresh();

            DisplayEventInData(eventInReports);
            eventInReports.Clear();
            return true;
        }
        private void DisplayEventInData(List<EntryData> eventInReports)
        {
            try
            {
                dgvData1.Rows.Clear();
                dgvData1.CurrentCell = null;
                List<DataGridViewRow> rows = [];
                foreach (var eventInfo in eventInReports)
                {
                    DataGridViewRow row = new();
                    row.CreateCells(dgvData1);

                    AccessKey? registerVehicle = eventInfo.AccessKey?.GetVehicleInfo();
                    CustomerDto? customer = registerVehicle?.Customer;

                    row.Cells[dgvData1.Columns[colEventId.Name].Index].Value = eventInfo.Id;
                    row.Cells[dgvData1.Columns[colAccessKeyId.Name].Index].Value = eventInfo.AccessKey?.Id ?? "";
                    row.Cells[dgvData1.Columns[colLaneInId.Name].Index].Value = eventInfo.Device?.Id ?? "";
                    row.Cells[dgvData1.Columns[colFileKeys.Name].Index].Value = eventInfo.Images == null ?
                                                                                    "[]" :
                                                                                    Newtonsoft.Json.JsonConvert.SerializeObject(eventInfo.Images);
                    row.Cells[dgvData1.Columns[colCustomer.Name].Index].Value = customer?.Id;
                    row.Cells[dgvData1.Columns[colVehicleId.Name].Index].Value = registerVehicle?.Id;
                    row.Cells[dgvData1.Columns[colIndex.Name].Index].Value = (rows.Count + 1).ToString();
                    row.Cells[dgvData1.Columns[colPlateIn.Name].Index].Value = eventInfo.PlateNumber;
                    row.Cells[dgvData1.Columns[colTimeIn.Name].Index].Value = eventInfo.DateTimeIn.ToVNTime();
                    row.Cells[dgvData1.Columns[colNote.Name].Index].Value = eventInfo.Note;
                    row.Cells[dgvData1.Columns[colAccessKeyCollection.Name].Index].Value = eventInfo.CollecionName;
                    row.Cells[dgvData1.Columns[colUser.Name].Index].Value = eventInfo.CreatedBy;
                    row.Cells[dgvData1.Columns[colPaid.Name].Index].Value = eventInfo.Amount.ToString("N0");
                    row.Cells[dgvData1.Columns[colLaneInName.Name].Index].Value = eventInfo.Device?.Name ?? "";
                    row.Cells[dgvData1.Columns[colAccessKeyName.Name].Index].Value = eventInfo.AccessKey?.Name ?? "";
                    row.Cells[dgvData1.Columns[colAccessKeyCode.Name].Index].Value = eventInfo.AccessKey?.Code ?? "";
                    row.Cells[dgvData1.Columns[colVehicleCode.Name].Index].Value = registerVehicle?.Code ?? "";
                    row.Cells[dgvData1.Columns[colCustomer.Name].Index].Value = customer?.Name ?? "";

                    rows.Add(row);
                }

                dgvData1.Rows.AddRange([.. rows]);
                if (dgvData1.Rows.Count > 0)
                {
                    dgvData1.CurrentCell = dgvData1.Rows[0].Cells[colIndex.Name];
                }
                else
                {
                    dgvData1.CurrentCell = null;
                }
                DgvData_SelectionChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Display Event In Report", ex, EmSystemActionType.ERROR));
            }
        }
        private void DisplayExportData(List<EntryData> eventInReports, DataGridView dgv)
        {
            try
            {
                dgv.CurrentCell = null;
                List<DataGridViewRow> rows = [];
                foreach (var eventInfo in eventInReports)
                {
                    DataGridViewRow row = new();
                    this.Invoke(new Action(() =>
                    {
                        row.CreateCells(dgvData1);
                    }));

                    AccessKey? registerVehicle = eventInfo.AccessKey?.GetVehicleInfo();
                    CustomerDto? customer = registerVehicle?.Customer;
                    row.Cells[dgvData1.Columns[colEventId.Name].Index].Value = eventInfo.Id;
                    row.Cells[dgvData1.Columns[colAccessKeyId.Name].Index].Value = eventInfo.AccessKey?.Id ?? "";
                    row.Cells[dgvData1.Columns[colLaneInId.Name].Index].Value = eventInfo.Device?.Id ?? "";
                    row.Cells[dgvData1.Columns[colFileKeys.Name].Index].Value = eventInfo.Images == null ?
                                                                                "[]" :
                                                                                Newtonsoft.Json.JsonConvert.SerializeObject(eventInfo.Images);
                    row.Cells[dgvData1.Columns[colCustomer.Name].Index].Value = customer?.Id;
                    row.Cells[dgvData1.Columns[colVehicleId.Name].Index].Value = registerVehicle?.Id;
                    row.Cells[dgvData1.Columns[colIndex.Name].Index].Value = (rows.Count + 1).ToString();
                    row.Cells[dgvData1.Columns[colPlateIn.Name].Index].Value = eventInfo.PlateNumber;
                    row.Cells[dgvData1.Columns[colTimeIn.Name].Index].Value = eventInfo.DateTimeIn.ToVNTime();
                    row.Cells[dgvData1.Columns[colNote.Name].Index].Value = eventInfo.Note;
                    row.Cells[dgvData1.Columns[colAccessKeyCollection.Name].Index].Value = eventInfo.CollecionName;
                    row.Cells[dgvData1.Columns[colUser.Name].Index].Value = eventInfo.CreatedBy;
                    row.Cells[dgvData1.Columns[colPaid.Name].Index].Value = eventInfo.Amount.ToString("N0");
                    row.Cells[dgvData1.Columns[colLaneInName.Name].Index].Value = eventInfo.Device?.Name ?? "";
                    row.Cells[dgvData1.Columns[colAccessKeyName.Name].Index].Value = eventInfo.AccessKey?.Name ?? "";
                    row.Cells[dgvData1.Columns[colAccessKeyCode.Name].Index].Value = eventInfo.AccessKey?.Code ?? "";
                    row.Cells[dgvData1.Columns[colVehicleCode.Name].Index].Value = registerVehicle?.Code ?? "";
                    row.Cells[dgvData1.Columns[colCustomer.Name].Index].Value = customer?.Name ?? "";

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
                }));
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Display Event In Report", ex, EmSystemActionType.ERROR));
            }
        }

        private void ChangeSearchConditionEvent(object? sender, EventArgs e)
        {
            btnSearch.PerformClick();
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
        private void DisplayCustomerCollectionOnCombobox()
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
        private void DisplayAccessKeyCollectionsOnCombobox()
        {
            cbIdentityGroupType.Invoke(new Action(() =>
            {
                cbIdentityGroupType.Items.Add(new ListItem()
                {
                    Name = KZUIStyles.CurrentResources.All,
                    Value = ""
                });
            }));

            identityGroups = [.. identityGroups.OrderBy(x => x.Name).ThenBy(x => x.Name.Length)];

            cbIdentityGroupType.Invoke(new Action(() =>
            {
                foreach (var item in identityGroups)
                {
                    ListItem identityGroupItem = new()
                    {
                        Name = item.Name,
                        Value = item.Id.ToString()
                    };
                    cbIdentityGroupType.Items.Add(identityGroupItem);
                }
                cbIdentityGroupType.SelectedIndex = 0;
            }));
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
        #endregion End Private Function

        private void InitPropeties(Image? defaultImage, IAPIServer ApiServer, IEnumerable<Lane> lanes, bool isAllowSelect = false)
        {
            this.defaultImg = defaultImage;
            this.ApiServer = ApiServer;
            this.isAllowSelect = isAllowSelect;
            this.lanes = lanes;
        }
        public void Translate()
        {
            //---Button
            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnFee.Text = KZUIStyles.CurrentResources.CheckFee;
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
            lblTotalEvents.Text = KZUIStyles.CurrentResources.Total;

            //---Datagridview
            colPlateIn.HeaderText = KZUIStyles.CurrentResources.Plate;
            colTimeIn.HeaderText = KZUIStyles.CurrentResources.TimeIn;
            colAccessKeyCollection.HeaderText = KZUIStyles.CurrentResources.AccessKeyCollection;
            colUser.HeaderText = KZUIStyles.CurrentResources.User;
            colPaid.HeaderText = KZUIStyles.CurrentResources.Paid;
            colLaneInName.HeaderText = KZUIStyles.CurrentResources.Lane;
            colAccessKeyName.HeaderText = KZUIStyles.CurrentResources.AccesskeyName;
            colAccessKeyCode.HeaderText = KZUIStyles.CurrentResources.AccesskeyCode;
            colVehicleCode.HeaderText = KZUIStyles.CurrentResources.VehicleCode;
            colCustomer.HeaderText = KZUIStyles.CurrentResources.CustomerName;
            colNote.HeaderText = KZUIStyles.CurrentResources.colNote;
        }
        private void InitUI()
        {
            try
            {
                this.SuspendLayout();

                dtpStartTime.Value = new DateTime(2024, 1, 1, 0, 0, 0);
                dtpEndTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                picOverviewImageIn.Image = picOverviewImageIn.ErrorImage = defaultImg;
                picVehicleImageIn.Image = picVehicleImageIn.ErrorImage = defaultImg;

                cbVehicleType.DisplayMember = cbIdentityGroupType.DisplayMember = cbLane.DisplayMember = cbUser.DisplayMember = cbCustomerGroup.DisplayMember = "Name";
                cbVehicleType.ValueMember = cbIdentityGroupType.ValueMember = cbLane.ValueMember = cbUser.ValueMember = cbCustomerGroup.ValueMember = "Value";
                cbVehicleType.MaxDropDownItems = cbIdentityGroupType.MaxDropDownItems = cbLane.MaxDropDownItems = cbUser.MaxDropDownItems = cbCustomerGroup.MaxDropDownItems = 12;

                DisplayVehicleTypesOnCombobox();
                DisplayAccessKeyCollectionsOnCombobox();
                DisplayLanesOnCombobox();
                DisplayUsersOnCombobox();
                DisplayCustomerCollectionOnCombobox();

                ucNavigator1.Reset();
                this.ResumeLayout(false);
                this.PerformLayout();

                btnCancel.OnClickAsync += BtnCancel_Click;
                btnExportExcel.OnClickAsync += BtnExportExcel_Click;
                btnSearch.OnClickAsync += BtnSearch_Click;
                btnFee.OnClickAsync += BtnFee_Click;

                dgvData1.SelectionChanged += DgvData_SelectionChanged;
                ucNavigator1.onPageChangeEvent += UcPages1_OnpageSelect;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Create UI", ex, EmSystemActionType.ERROR));
            }
        }

        private void ClearData()
        {
            identityGroups?.Clear();
            users?.Clear();
            customerGroups.Clear();
            dgvData1.Rows.Clear();
        }
        private void ShowErrorImage()
        {
            picOverviewImageIn.ShowImage(defaultImg);
            picVehicleImageIn.ShowImage(defaultImg);
        }
    }
}
