using Guna.UI2.WinForms;
using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Tool;

namespace Kztek.Control8.Forms
{
    public partial class UcSelectAccessKeyCollection : UserControl, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<Collection?>? tcs;
        private UcConfirm ucConfirm;
        private LaneOptionalConfig LaneOptionalConfig;

        private readonly Guna2Elipse guna2Elipse = new();

        private readonly Form dialogHost = new Form();
        private MaskedUserControl masked;

        private UserControl _TargetControl;
        public UserControl TargetControl
        {
            get => _TargetControl;
            set
            {
                this._TargetControl = value;
                masked?.Dispose();
                masked = new MaskedUserControl(value);
            }
        }

        public int BorderRadius
        {
            get => guna2Elipse.BorderRadius;
            set
            {
                guna2Elipse.BorderRadius = value;
            }
        }
        #endregion

        #region Forms
        public UcSelectAccessKeyCollection()
        {
            InitializeComponent();
            InitUI();
            Translate();
        }
        #endregion

        #region Controls In Form
        private void Uc_onIdentityGroupClicked(Collection collection)
        {
            tcs?.TrySetResult(collection);
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            tcs?.TrySetResult(null);
            return true;
        }
        #endregion

        public async Task<Collection?> SelectCollectionAsync(List<Collection> collections, LaneOptionalConfig laneOptionalConfig)
        {
            this.LaneOptionalConfig = laneOptionalConfig;

            foreach (var control in flowLayoutPanel1.Controls.OfType<UcVoucher>())
            {
                control.Dispose();
            }
            flowLayoutPanel1.Controls.Clear();

            foreach (var collection in collections)
            {
                UcAccessKeyCollection ucAccessKeyCollection = new(collection)
                {
                    Margin = new Padding(8)
                };
                flowLayoutPanel1.Controls.Add(ucAccessKeyCollection);
                ucAccessKeyCollection.OnSelectedCollectionEvent += Uc_onIdentityGroupClicked;
            }

            Translate();

            dialogHost.Width = MathTool.GetMin(dialogHost.Width, masked.Width * 95 / 100, masked.Width);
            dialogHost.Height = MathTool.GetMin(dialogHost.Height, masked.Height * 95 / 100, masked.Height);
            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();

            tcs = new TaskCompletionSource<Collection?>();
            var result = await tcs.Task;

            dialogHost.Hide();
            masked.Hide();
            return result;
        }

        private void InitUI()
        {
            ucConfirm = new UcConfirm
            {
                TargetControl = this,
                BorderRadius = 24,
            };

            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }
        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            lblTitle.Text = KZUIStyles.CurrentResources.AccessKeyCollectionList;
        }
    }
}
