using iParkingv5.Controller.KztekDevices.MT166_CardDispenser;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IController = iParkingv5.Controller.IController;

namespace Kztek.Control8.Forms
{
    public partial class FrmTestController : Form, KzITranslate
    {
        private IController controller;

        #region Forms
        public FrmTestController(IController icontroller)
        {
            InitializeComponent();

            InitProperties(icontroller);
            Translate();
            this.Load += FrmTestController_Load;
            this.FormClosing += FrmTestController_FormClosing;
        }
        private void FrmTestController_Load(object? sender, EventArgs e)
        {
            InitUI();
        }
        private void FrmTestController_FormClosing(object? sender, FormClosingEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.controller.CardEvent -= Controller_CardEvent;
            this.controller.InputEvent -= Controller_InputEvent;
        }
        #endregion

        #region Controls In Form
        private async Task<bool> BtnOpenBarrie_Click(object? sender)
        {
            lblResult.Text = string.Empty;
            lblResult.Visible = false;

            bool isOpenSuccess = await this.controller.OpenDoor(500, (int)numRelay.Value);

            lblResult.Text = isOpenSuccess ? KZUIStyles.CurrentResources.SuccessTitle : KZUIStyles.CurrentResources.ErrorTitle;
            lblResult.ForeColor = isOpenSuccess ? Color.Green : Color.Red;
            lblResult.Visible = true;
            return true;
        }
        private async Task<bool> BtnCollectCard_Click(object? sender)
        {
            lblResult.Text = string.Empty;
            lblResult.Visible = false;
            bool isOpenSuccess = controller is ICardDispenser
                ? await ((ICardDispenser)controller).CollectCard()
                : await ((ICardDispenserv2)controller).CollectCard();
            lblResult.Text = isOpenSuccess ? KZUIStyles.CurrentResources.SuccessTitle : KZUIStyles.CurrentResources.ErrorTitle;
            lblResult.ForeColor = isOpenSuccess ? Color.Green : Color.Red;
            lblResult.Visible = true;
            return true;
        }
        #endregion

        #region Event
        private void Controller_InputEvent(object sender, iParkingv5.Objects.Events.InputEventArgs e)
        {
            dgvEvent.Invoke(new Action(() =>
            {
                dgvEvent.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), e.InputType, e.InputIndex, "");
            }));
        }
        private void Controller_CardEvent(object sender, iParkingv5.Objects.Events.CardEventArgs e)
        {
            dgvEvent.Invoke(new Action(() =>
            {
                dgvEvent.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), "Card Event", e.ReaderIndex, e.PreferCard);
            }));
        }
        #endregion

        private void InitProperties(IController icontroller)
        {
            this.controller = icontroller;
        }
        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }

            this.Text = KZUIStyles.CurrentResources.FrmTestController;
            lblDevice.Text = KZUIStyles.CurrentResources.Device;
            btnOpenBarrie.Text = KZUIStyles.CurrentResources.OpenBarrie;
            btnCollectCard.Text = KZUIStyles.CurrentResources.CollectCard;
            lblEventList.Text = KZUIStyles.CurrentResources.EventList;

            colTime.HeaderText = KZUIStyles.CurrentResources.ColTime;
            colEventType.HeaderText = KZUIStyles.CurrentResources.ColEventType;
            colReaderOrLoop.HeaderText = KZUIStyles.CurrentResources.ColCardOrLoop;
            colCardNumber.HeaderText = KZUIStyles.CurrentResources.ColCardNumber;
        }
        private void InitUI()
        {
            lblControllerName.Text = this.controller.ControllerInfo.Name + " - " + this.controller.ControllerInfo.Comport + " : " + this.controller.ControllerInfo.Baudrate;

            if (controller is not ICardDispenser && controller is not ICardDispenserv2)
            {
                btnCollectCard.Visible = false;
            }
            btnOpenBarrie.OnClickAsync += BtnOpenBarrie_Click;
            btnCollectCard.OnClickAsync += BtnCollectCard_Click;

            this.controller.CardEvent += Controller_CardEvent;
            this.controller.InputEvent += Controller_InputEvent;
        }
    }
}
