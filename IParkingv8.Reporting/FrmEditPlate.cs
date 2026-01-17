using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;

namespace iParkingv8.Reporting
{
    public partial class FrmEditPlate : Form, KzITranslate
    {
        #region Properties
        public string UpdatePlate { get => txtNewPlate.Text; }
        private string eventId = string.Empty;
        private bool isEventIn = false;
        private IAPIServer server;
        #endregion

        #region Form
        public FrmEditPlate(string currentPlate, string eventId, bool isEventIn, IAPIServer server)
        {
            InitializeComponent();

            Translate();
            InitUI(currentPlate);
            InitProperties(eventId, isEventIn, server);
        }
        private async void FrmEditPlate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPlate.Text = txtNewPlate.Text.ToUpper();

                bool isUpdateSuccess = isEventIn ?
                            await server.OperatorService.Entry.UpdatePlateAsync(this.eventId, txtNewPlate.Text, lblCurrentPlate.Text) :
                            await server.OperatorService.Exit.UpdatePlateAsync(this.eventId, txtNewPlate.Text, lblCurrentPlate.Text)
                            ;
                if (isUpdateSuccess)
                {
                    MessageBox.Show($"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.SuccessTitle}",
                                       KZUIStyles.CurrentResources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                else
                {
                    MessageBox.Show($"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.ErrorTitle}, {KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.TryAgain}",
                                       KZUIStyles.CurrentResources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion

        #region Controls In Form
        private async Task<bool> BtnConfirm_Click(object? sender)
        {
            txtNewPlate.Text = txtNewPlate.Text.ToUpper();
            bool isUpdateSuccess = isEventIn ?
                            await server.OperatorService.Entry.UpdatePlateAsync(this.eventId, txtNewPlate.Text.ToUpper(), lblCurrentPlate.Text) :
                            await server.OperatorService.Exit.UpdatePlateAsync(this.eventId, txtNewPlate.Text.ToUpper(), lblCurrentPlate.Text);
            if (isUpdateSuccess == true)
            {
                MessageBox.Show($"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.SuccessTitle}",
                                       KZUIStyles.CurrentResources.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                return true;
            }

            MessageBox.Show($"{KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.ErrorTitle}, {KZUIStyles.CurrentResources.CustomerCommandUpdatePlate} {KZUIStyles.CurrentResources.TryAgain}",
                               KZUIStyles.CurrentResources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        private async Task<bool> BtnCancel_Click(object? sender)
        {
            this.DialogResult = DialogResult.Cancel;
            return true;
        }
        #endregion

        public void Translate()
        {
            this.Text = KZUIStyles.CurrentResources.FrmEditPlate;
            lblCurrentPlateTitle.Text = KZUIStyles.CurrentResources.CurrentPlate;
            lblNewPlateTitle.Text = KZUIStyles.CurrentResources.NewPlate;

            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
        }
        private void InitUI(string currentPlate)
        {
            lblCurrentPlate.Text = currentPlate;

            btnConfirm.OnClickAsync += BtnConfirm_Click;
            btnCancel.OnClickAsync += BtnCancel_Click;
        }
        private void InitProperties(string eventId, bool isEventIn, IAPIServer server)
        {
            this.eventId = eventId;
            this.isEventIn = isEventIn;
            this.server = server;
        }
    }
}
