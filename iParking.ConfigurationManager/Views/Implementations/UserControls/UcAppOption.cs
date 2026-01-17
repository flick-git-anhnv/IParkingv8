using iParking.ConfigurationManager.Views.Interfaces;
using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Sounds;
using Kztek.Object;

namespace iParking.ConfigurationManager.UserControls
{
    public partial class UcAppOption : UserControl, IAppOptionView
    {
        public UcAppOption()
        {
            InitializeComponent();
            LoadPrintTemplate();
            LoadSoundMode();
            LoadLogType();

            cbPrintTemplate.SelectedIndex = 0;
            cbSound.SelectedIndex = 0;
            cbLogType.SelectedIndex = 0;
        }
        public void DisplayDevelopMode(bool isDisplay)
        {
            chbIsCheckKey.Visible = isDisplay;
        }

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

        public AppOption? GetConfig()
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
        public void SetConfig(AppOption? config)
        {
            if (config != null)
            {
                //Thời gian chờ sự kiện
                txtAllowOpenBarrieTime.Text = config.AllowBarrieDelayOpenTime.ToString();
                txtWaitSwipeCardTime.Value = config.MinDelayCardTime;
                txtLoopDelay.Value = config.LoopDelay;
                numCardDelayTakeImage.Value = config.CardTakeImageDelay;

                //Nhận dạng lại biển số
                chbIsRequiredDAILYPlateIn.Checked = config.IsRequiredDAILYPlateIn;

                //Loop ảo
                kryptonTextBox1.Text = config.MotionAlarmLevel.ToString();
                int selectedVirtualLoopMode = (int)config.VirtualLoopMode >= cbVirtualLoopMode.Items.Count ? -1 : (int)config.VirtualLoopMode;
                cbVirtualLoopMode.SelectedIndex = selectedVirtualLoopMode;
                chbIsUseVirtualLoop.Checked = config.IsUseVirtualLoop;

                //Log hệ thống
                txtLogKeepDays.Text = config.NumLogKeepDays.ToString();
                chbIsSaveLog.Checked = config.IsSaveLog;

                int selectedLogMode = (int)config.LogServiceType >= cbLogType.Items.Count ? -1 : (int)config.LogServiceType;
                cbLogType.SelectedIndex = selectedLogMode;

                //Tùy chọn
                int selectedPrintMode = config.PrintTemplate >= cbPrintTemplate.Items.Count ? -1 : (int)config.PrintTemplate;
                cbPrintTemplate.SelectedIndex = selectedPrintMode;

                txtUpdatePath.Text = config.CheckForUpdatePath;

                int selectedSoundMode = (int)config.SoundMode >= cbSound.Items.Count ? -1 : (int)config.SoundMode;
                cbSound.SelectedIndex = selectedSoundMode;

                chbDisplayCustomer.Checked = config.IsDisplayCustomerInfo;
                chbIsOpenAllBarieForCar.Checked = config.IsOpenAllBarrieForCar;
                chbSaveVehicleOnLoop.Checked = config.IsSaveVehicleOnLoop;

                chbIsDisplayImageByBase64.Checked = config.IsDisplayImageByBase64;
                chbIsCheckKey.Checked = config.IsCheckKey;

                chbIsSendInvoice.Checked = config.IsSendInvoice;

                int selectedCameraMode = (int)config.cameraSDk >= cbCameraSDK.Items.Count ? -1 : (int)config.cameraSDk;
                cbCameraSDK.SelectedIndex = selectedCameraMode;

                chbStartWithWindow.Checked = config.IsStartWithWindow;

                chbIsDisplayNotExistCardNotify.Checked = config.IsDisplayNotExistCardNotify;
                chbIsAutoRejectWarningPlate.Checked = config.IsAutoRejectWarningPlate;

                numTimeToDefaultConfig.Value = config.TimeToDefautUI;
            }
        }
    }
}
