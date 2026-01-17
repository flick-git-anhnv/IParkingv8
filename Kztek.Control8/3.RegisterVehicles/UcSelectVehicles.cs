using Guna.UI2.WinForms;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using Kztek.Control8.UserControls;
using Kztek.Control8.UserControls.DialogUcs;
using Kztek.Tool;

namespace Kztek.Control8.Forms
{
    public partial class UcSelectVehicles : UserControl, KzITranslate
    {
        #region Properties
        private TaskCompletionSource<AccessKey?>? tcs;
        private readonly Guna2Elipse guna2Elipse = new();

        private readonly Form dialogHost = new();
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

        private AccessKey? selectedVehicle;
        private bool isLaneIn = false;
        #endregion

        #region Forms
        public UcSelectVehicles(bool isLaneIn)
        {
            InitializeComponent();
            InitProperties(isLaneIn);

            Translate();
            InitUI();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Return)
            {
                btnConfirm.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btnCancel.PerformClick();
                return true;
            }
            if (keyData == Keys.Up || keyData == Keys.Down ||
                keyData == Keys.Left || keyData == Keys.Right)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion End Forms

        #region Controls In Form
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            if (selectedVehicle == null)
            {
                MessageBox.Show(isLaneIn ? KZUIStyles.CurrentResources.ChooseVehicleEntry :
                                           KZUIStyles.CurrentResources.ChooseVehicleExit, 
                                KZUIStyles.CurrentResources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            tcs?.SetResult(selectedVehicle);
            return true;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            tcs?.SetResult(null);
            selectedVehicle = null;
            return true;
        }
        private void RadioButton_OnSelectedEvent(UcVehicle sender)
        {
            btnConfirm.Enabled = true;
            btnConfirm.Focus();
            this.ActiveControl = btnConfirm;
            this.selectedVehicle = sender.vehicle;
            foreach (var item in flowVehicle.Controls.OfType<UcVehicle>())
            {
                if (item == sender)
                {
                    continue;
                }
                item.KZUI_Checked = false;
            }
        }
        #endregion End Controls In Form

        #region Public Function
        public async Task<AccessKey?> SelectVehicleAsync(List<AccessKey> vehicles)
        {
            foreach (var control in flowVehicle.Controls.OfType<UcVehicle>())
            {
                control.Dispose();
            }
            flowVehicle.Controls.Clear();

            foreach (var vehicle in vehicles)
            {
                UcVehicle radioButton = new(vehicle)
                {
                    Margin = new Padding(8)
                };
                flowVehicle.Controls.Add(radioButton);
                radioButton.OnSelectedEvent += RadioButton_OnSelectedEvent;
            }

            selectedVehicle = null;
            btnConfirm.Enabled = false;

            Translate();

            dialogHost.Width = MathTool.GetMin(dialogHost.Width, masked.Width * 95 / 100, masked.Width);
            dialogHost.Height = MathTool.GetMin(dialogHost.Height, masked.Height * 95 / 100, masked.Height);
            dialogHost.Location = new Point(
                       this.masked.Left + (this.masked.Width - dialogHost.Width) / 2,
                       this.masked.Top + (this.masked.Height - dialogHost.Height) / 2
                   );
            dialogHost.Show(this.masked);
            masked.Show();

            tcs = new TaskCompletionSource<AccessKey?>();
            var result = await tcs.Task;

            dialogHost.Hide();
            masked.Hide();
            return result;
        }
        #endregion

        private void InitProperties(bool isLaneIn)
        {
            this.isLaneIn = isLaneIn;
        }
        public void Translate()
        {
            if (this.InvokeRequired && this.IsHandleCreated)
            {
                this.Invoke(Translate);
                return;
            }
            lblTitle.Text = KZUIStyles.CurrentResources.VehicleList;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
        }
        private void InitUI()
        {
            dialogHost.FormBorderStyle = FormBorderStyle.None;
            dialogHost.StartPosition = FormStartPosition.Manual;
            dialogHost.Size = this.Size;
            dialogHost.BackColor = Color.White;
            dialogHost.ShowInTaskbar = false;
            dialogHost.Controls.Add(this);

            this.Dock = DockStyle.Fill;
            guna2Elipse.TargetControl = dialogHost;

            btnConfirm.OnClickAsync += BtnConfirm_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }
    }
}
