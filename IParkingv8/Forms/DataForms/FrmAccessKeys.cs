using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Object.Objects.Bases;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Objects;
using Kztek.Object;
using Kztek.Tool;

namespace IParkingv8.Forms.DataForms
{
    public partial class FrmAccessKeys : Form, KzITranslate
    {
        #region PROPERTIES
        private List<Collection> accessKeyCollections = [];
        #endregion END PROPERTIES

        #region FORMS
        public FrmAccessKeys()
        {
            InitializeComponent();

            Translate();

            this.KeyPreview = true;
            this.KeyDown += FrmSelectCard_KeyDown;
            this.Load += FrmSelectCard_Load;
        }
        private async void FrmSelectCard_Load(object? sender, EventArgs e)
        {
            accessKeyCollections = (await AppData.ApiServer.DataService.AccessKeyCollection.GetAllAsync())?.Item1 ?? [];
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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Access Key Report", ex, EmSystemActionType.ERROR));
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
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess("Get Access Key Report", ex, EmSystemActionType.ERROR));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion END CONTROLS IN FORM

        private void DisplayAccessKeyCollectionsOnCombobox()
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
        private void Reset()
        {
            dgvData.Rows.Clear();
            dgvData.Refresh();
        }
        private async Task<bool> GetReportData(int pageIndex)
        {
            string collectionId = cbCollection.SelectedItem == null ? "" : ((ListItem)cbCollection.SelectedItem)?.Value ?? "";
            EmAccessKeyStatus? accessKeyStatus = null;
            if (cbStatus.SelectedIndex > 0)
            {
                accessKeyStatus = (EmAccessKeyStatus)(cbStatus.SelectedIndex - 1);
            }
            var report = await AppData.ApiServer.DataService.AccessKey.GetByConditionAsync(txtKeyword.Text, collectionId, accessKeyStatus,
                                                                                           pageIndex: pageIndex, pageSize: Filter.PAGE_SIZE);
            if (report == null)
            {
                string message = $"{KZUIStyles.CurrentResources.SystemError} {KZUIStyles.CurrentResources.TryAgain}";
                MessageBox.Show(message, KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var identities = report.Data ?? [];
            ucNavigator1.KZUI_MaxPage = report.TotalPage;
            ucNavigator1.KZUI_CurrentPage = pageIndex + 1;
            ucNavigator1.KZUI_TotalRecord = report.TotalCount;

            for (int i = 0; i < identities.Count; i++)
            {
                dgvData.Rows.Add(i + 1, identities[i].Name, identities[i].Code, identities[i].GetTypeName(),
                                 identities[i].GetStatusName(), identities[i].Note,
                                 identities[i].Collection!.Name, identities[i].Id);
            }
            identities.Clear();
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

            this.Text = KZUIStyles.CurrentResources.FrmAccessKeyList;
            lblKeywordTitle.Text = KZUIStyles.CurrentResources.Keyword;
            lblCollectionTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollection;
            lblStatusTitle.Text = KZUIStyles.CurrentResources.Status;

            btnSearch.Text = KZUIStyles.CurrentResources.Search;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;

            col_access_key_name.HeaderText = KZUIStyles.CurrentResources.colAccessKeyName;
            col_access_key_code.HeaderText = KZUIStyles.CurrentResources.colAccessKeyCode;
            col_access_key_type.HeaderText = KZUIStyles.CurrentResources.colType;
            col_access_key_status.HeaderText = KZUIStyles.CurrentResources.colStatus;
            col_access_key_note.HeaderText = KZUIStyles.CurrentResources.colNote;
            col_access_key_collection.HeaderText = KZUIStyles.CurrentResources.colCollection;

            txtKeyword.PlaceholderText = KZUIStyles.CurrentResources.colAccessKeyKeyword;
        }
        private void InitUI()
        {
            cbStatus.Items.Add(KZUIStyles.CurrentResources.All);
            foreach (EmAccessKeyStatus item in Enum.GetValues(typeof(EmAccessKeyStatus)))
            {
                cbStatus.Items.Add(item.ToDisplayString());
            }

            cbCollection.DisplayMember = "Name";
            cbCollection.ValueMember = "Value";
            DisplayAccessKeyCollectionsOnCombobox();

            ucNavigator1.Reset();

            btnSearch.OnClickAsync += BtnSearch_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
            ucNavigator1.onPageChangeEvent += UcNavigator1_onPageChangeEvent;
            this.ActiveControl = btnSearch;
        }
    }
}
