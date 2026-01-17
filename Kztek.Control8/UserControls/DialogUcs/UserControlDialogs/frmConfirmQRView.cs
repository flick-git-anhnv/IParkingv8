using iParkingv8.Object.ConfigObjects.LaneConfigs;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Ultility;
using iParkingv8.Ultility.dictionary;
using iParkingv8.Ultility.Style;
using IParkingv8.QRScreenController;
using Kztek.Tool;

namespace Kztek.Control8.UserControls.DialogUcs.UserControlDialogs
{
    public partial class FrmConfirmQRView : Form, KzITranslate
    {
        #region PROPERTIES
        private bool isGeneratedQR;
        private readonly IQRDevice qRViewDevice;
        private readonly LaneOptionalConfig config;
        private readonly ExitData exitData;
        #endregion

        #region Forms
        public FrmConfirmQRView(ExitData exit, IQRDevice qRViewDevice, LaneOptionalConfig config)
        {
            InitializeComponent();
            this.exitData = exit;
            this.qRViewDevice = qRViewDevice;
            this.config = config;
            btnConfirm.Enabled = false;

            Translate();

            this.Load += FrmConfirmQRView_Load;
            this.FormClosing += FrmConfirmQRView_FormClosing;
        }
        private async void FrmConfirmQRView_Load(object? sender, EventArgs e)
        {
            await this.qRViewDevice.OpenHomePageAsync();
            await Task.Delay(100);
            var entry = this.exitData.Entry!;
            lblTimeIn.Text = entry.DateTimeIn.ToVNTime();
            lblTimeOut.Text = this.exitData.DatetimeOut.ToVNTime();

            var ParkingTime = this.exitData.DatetimeOut - entry.DateTimeIn;
            string formattedTime;
            if (ParkingTime.TotalDays > 1)
            {
                formattedTime = string.Format("{0} ngày {1} giờ {2} phút {3} giây", ParkingTime.Days, ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
            }
            else
            {
                formattedTime = string.Format("{0} giờ {1} phút {2} giây", ParkingTime.Hours, ParkingTime.Minutes, ParkingTime.Seconds);
            }

            lblParkingTime.Text = formattedTime;

            long fee = this.exitData.Amount - this.exitData.DiscountAmount - entry.Amount;
            fee = Math.Max(fee, 0);

            txtPlateOut.Text = this.exitData.PlateNumber;
            lblAlarmPlate.Text = entry.PlateNumber;
            lblParkingFee.Text = TextFormatingTool.GetMoneyFormat(fee.ToString());

            isGeneratedQR = await qRViewDevice.DisplayQRAsync(this.config.BankName, this.config.AccountNumber, fee, $"{this.exitData.AccessKey.Code} {this.exitData.DatetimeOut:ddMMyyyy} {this.exitData.DatetimeOut:HHmmss}");
            if (isGeneratedQR)
            {
                btnConfirm.Enabled = true;
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = KZUIStyles.CurrentResources.SuccessTitle;
                btnGenerateQR.Text = KZUIStyles.CurrentResources.ReCreateQRView;
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = KZUIStyles.CurrentResources.ErrorTitle;
            }
        }
        private async void FrmConfirmQRView_FormClosing(object? sender, FormClosingEventArgs e)
        {
            await this.qRViewDevice.OpenHomePageAsync();
        }
        #endregion

        #region Controls In Form
        private async void BtnGenerateQR_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "_";
            btnGenerateQR.Enabled = false;
            try
            {
                var fee = this.exitData.Amount - this.exitData.DiscountAmount - this.exitData.Entry!.Amount;
                isGeneratedQR = await qRViewDevice.DisplayQRAsync(this.config.BankName, this.config.AccountNumber, fee, $"{this.exitData.AccessKey.Code} {this.exitData.DatetimeOut:ddMMyyyy} {this.exitData.DatetimeOut:HHmmss}");
                if (isGeneratedQR)
                {
                    btnConfirm.Enabled = true;
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = KZUIStyles.CurrentResources.SuccessTitle;
                    btnGenerateQR.Text = KZUIStyles.CurrentResources.ReCreateQRView;
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = KZUIStyles.CurrentResources.ErrorTitle;
                }
            }
            catch (Exception)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = KZUIStyles.CurrentResources.ErrorTitle;
            }
            finally
            {
                btnGenerateQR.Enabled = true;
            }
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (!isGeneratedQR)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = KZUIStyles.CurrentResources.CreateQRViewRequired;
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        public void Translate()
        {
            if (this.IsHandleCreated && this.InvokeRequired)
            {
                this.Invoke(Translate);
                return;
            }
            lblPaymentInfoTitle.Text = KZUIStyles.CurrentResources.PaymentInfo;
            lblPlateOutTitle.Text = KZUIStyles.CurrentResources.PlateOut;
            lblPlateAlarmTitle.Text = KZUIStyles.CurrentResources.PlateIn;
            lblTimeOutTitle.Text = KZUIStyles.CurrentResources.TimeOut;
            lblTimeInTitle.Text = KZUIStyles.CurrentResources.TimeIn;
            lblDurationTitle.Text = KZUIStyles.CurrentResources.Duration;
            lblFeeTitle.Text = KZUIStyles.CurrentResources.Fee;
            btnCancel.Text = KZUIStyles.CurrentResources.Cancel;
            btnConfirm.Text = KZUIStyles.CurrentResources.Confirm;
            btnGenerateQR.Text = KZUIStyles.CurrentResources.CreateQRView;
        }
    }
}
