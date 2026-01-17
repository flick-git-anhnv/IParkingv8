using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Sounds;
using Kztek.Object;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcAppOption : UserControl
    {
        public UcAppOption(AppOption? appOption)
        {
            InitializeComponent();
            LoadPrintTemplate();
            LoadSoundMode();
            LoadLogType();

            cbPrintTemplate.SelectedIndex = 0;
            cbSound.SelectedIndex = 0;
            cbLogType.SelectedIndex = 0;
            if (appOption != null)
            {
                //Thời gian chờ sự kiện
                txtAllowOpenBarrieTime.Text = appOption.AllowBarrieDelayOpenTime.ToString();
                txtWaitSwipeCardTime.Value = appOption.MinDelayCardTime;
                txtLoopDelay.Value = appOption.LoopDelay;
                numCardDelayTakeImage.Value = appOption.CardTakeImageDelay;

                //Nhận dạng lại biển số
                chbIsRequiredDAILYPlateIn.Checked = appOption.IsRequiredDAILYPlateIn;

                //Loop ảo
                kryptonTextBox1.Text = appOption.MotionAlarmLevel.ToString();
                int selectedVirtualLoopMode = (int)appOption.VirtualLoopMode >= cbVirtualLoopMode.Items.Count ? -1 : (int)appOption.VirtualLoopMode;
                cbVirtualLoopMode.SelectedIndex = selectedVirtualLoopMode;
                chbIsUseVirtualLoop.Checked = appOption.IsUseVirtualLoop;

                //Log hệ thống
                txtLogKeepDays.Text = appOption.NumLogKeepDays.ToString();
                chbIsSaveLog.Checked = appOption.IsSaveLog;

                int selectedLogMode = (int)appOption.LogServiceType >= cbLogType.Items.Count ? -1 : (int)appOption.LogServiceType;
                cbLogType.SelectedIndex = selectedLogMode;

                //Tùy chọn
                int selectedPrintMode = appOption.PrintTemplate >= cbPrintTemplate.Items.Count ? -1 : (int)appOption.PrintTemplate;
                cbPrintTemplate.SelectedIndex = selectedPrintMode;

                txtUpdatePath.Text = appOption.CheckForUpdatePath;

                int selectedSoundMode = (int)appOption.SoundMode >= cbSound.Items.Count ? -1 : (int)appOption.SoundMode;
                cbSound.SelectedIndex = selectedSoundMode;

                chbDisplayCustomer.Checked = appOption.IsDisplayCustomerInfo;
                chbIsOpenAllBarieForCar.Checked = appOption.IsOpenAllBarrieForCar;
                chbSaveVehicleOnLoop.Checked = appOption.IsSaveVehicleOnLoop;

                chbIsDisplayImageByBase64.Checked = appOption.IsDisplayImageByBase64;
                chbIsCheckKey.Checked = appOption.IsCheckKey;

                chbIsSendInvoice.Checked = appOption.IsSendInvoice;

                int selectedCameraMode = (int)appOption.cameraSDk >= cbCameraSDK.Items.Count ? -1 : (int)appOption.cameraSDk;
                cbCameraSDK.SelectedIndex = selectedCameraMode;

                chbStartWithWindow.Checked = appOption.IsStartWithWindow;

                chbIsDisplayNotExistCardNotify.Checked = appOption.IsDisplayNotExistCardNotify;
                chbIsAutoRejectWarningPlate.Checked = appOption.IsAutoRejectWarningPlate;

                numTimeToDefaultConfig.Value = appOption.TimeToDefautUI;
            }
        }

        #region Private Function
        private void LoadPrintTemplate()
        {
            foreach (EmPrintTemplate item in Enum.GetValues(typeof(EmPrintTemplate)))
            {
                cbPrintTemplate.Items.Add(item.ToString());
            }
        }
        private void LoadSoundMode()
        {
            foreach (EmSoundMode item in Enum.GetValues(typeof(EmSoundMode)))
            {
                cbSound.Items.Add(item.ToDisplayString());
            }
        }
        private void LoadLogType()
        {
            foreach (EmLogServiceType item in Enum.GetValues(typeof(EmLogServiceType)))
            {
                cbLogType.Items.Add(item.ToString());
            }
        }
        #endregion End Private Funciton

        #region Public Function
        public AppOption GetAppOption()
        {
            _ = int.TryParse(txtAllowOpenBarrieTime.Text, out int allowOpenBarrieTime);
            _ = int.TryParse(kryptonTextBox1.Text, out int virtualLoopAlarmLevel);
            _ = int.TryParse(txtLogKeepDays.Text, out int logKeepDays);

            return new AppOption()
            {
                //Thời gian chờ sự kiện
                AllowBarrieDelayOpenTime = allowOpenBarrieTime,
                MinDelayCardTime = (int)txtWaitSwipeCardTime.Value,
                LoopDelay = (int)txtLoopDelay.Value,
                CardTakeImageDelay = (int)numCardDelayTakeImage.Value,

                //Nhận dạng lại biển số
                IsRequiredDAILYPlateIn = chbIsRequiredDAILYPlateIn.Checked,

                //Loop ảo
                MotionAlarmLevel = virtualLoopAlarmLevel,
                VirtualLoopMode = (EmVirtualLoopMode)cbVirtualLoopMode.SelectedIndex,
                IsUseVirtualLoop = chbIsUseVirtualLoop.Checked,

                //Log hệ thống
                NumLogKeepDays = logKeepDays,
                IsSaveLog = chbIsSaveLog.Checked,
                LogServiceType = (EmLogServiceType)cbLogType.SelectedIndex,

                //Tùy chọn
                PrintTemplate = cbPrintTemplate.SelectedIndex,
                CheckForUpdatePath = txtUpdatePath.Text,
                SoundMode = (EmSoundMode)cbSound.SelectedIndex,
                IsDisplayCustomerInfo = chbDisplayCustomer.Checked,
                IsOpenAllBarrieForCar = chbIsOpenAllBarieForCar.Checked,
                IsSaveVehicleOnLoop = chbSaveVehicleOnLoop.Checked,
                IsDisplayImageByBase64 = chbIsDisplayImageByBase64.Checked,
                IsCheckKey = chbIsCheckKey.Checked,

                //INVOICE
                IsSendInvoice = chbIsSendInvoice.Checked,
                cameraSDk = cbCameraSDK.SelectedIndex,
                IsStartWithWindow = chbStartWithWindow.Checked,
                IsDisplayNotExistCardNotify = chbIsDisplayNotExistCardNotify.Checked,
                IsAutoRejectWarningPlate = chbIsAutoRejectWarningPlate.Checked,
                TimeToDefautUI = (int)numTimeToDefaultConfig.Value,
            };
        }
        public void DisplayDevelopMode(bool isDisplay)
        {
            chbIsCheckKey.Visible = isDisplay;
        }
        #endregion
    }
}
