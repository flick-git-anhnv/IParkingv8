using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Enums.Sounds;
using Kztek.Object;

namespace iParkingv8.Object.ConfigObjects.AppConfigs
{
    public enum EmVirtualLoopMode
    {
        Overview,
        Lpr,
        Both
    }
    public class AppOption
    {
        #region Thời gian chờ sự kiện
        /// <summary>
        /// Thời gian cho phép mở barrie sau khi quẹt thẻ
        /// </summary>
        public int AllowBarrieDelayOpenTime { get; set; } = 5;
        /// <summary>
        /// Khoảng cách giữa 2 lần quẹt thẻ liên tiếp (s)
        /// </summary>
        public int MinDelayCardTime { get; set; } = 5;
        /// <summary>
        /// Sau khi có sự kiện loop thì chờ thêm 1 khoảng thời gian nữa mới bắt đầu xử lý (Để cho xe đi vào vị trí chụp hình)
        /// </summary>
        public int LoopDelay { get; set; } = 0;
        public int CardTakeImageDelay { get; set; } = 0;
        #endregion

        #region Hộp thoại cảnh báo
        public bool IsUseAlarmDialog { get; set; } = true;
        public int AutoRejectDialogTime { get; set; } = 0;
        public bool AutoRejectDialogResult { get; set; } = false;
        #endregion

        #region Nhận dạng lại biển số
        public bool IsRequiredDAILYPlateIn { get; set; } = false;
        #endregion

        #region Loop ảo
        public int MotionAlarmLevel = 5;
        public EmVirtualLoopMode VirtualLoopMode { get; set; } = EmVirtualLoopMode.Both;
        public bool IsUseVirtualLoop { get; set; } = false;
        #endregion

        #region Log hệ thống
        public EmLogServiceType LogServiceType { get; set; }
        public bool IsSaveLog { get; set; } = true;
        public int NumLogKeepDays { get; set; } = 10;
        #endregion

        #region Tùy chọn
        /// <summary>
        /// Mẫu phiếu in
        /// </summary>
        public int PrintTemplate { get; set; } = (int)EmPrintTemplate.BaseTemplate;
        public string CheckForUpdatePath { get; set; } = "";
        public bool IsDisplayCustomerInfo { get; set; } = false;
        public bool IsOpenAllBarrieForCar { get; set; } = false;
        public EmSoundMode SoundMode { get; set; } = EmSoundMode.PlaySoundFromPC;
        public bool CheckPlateRemainIn { get; set; } = false;
        public bool RejectBlackListVehicle { get; set; } = false;
        public bool IsSaveVehicleOnLoop { get; set; } = false;
        #endregion

        public bool IsCheckKey { get; set; } = true;
        public bool IsDisplayImageByBase64 { get; set; } = false;

        #region Invoide
        public bool IsSendInvoice { get; set; } = false;
        #endregion

        public int cameraSDk = 1;
        public bool IsStartWithWindow { get; set; }
        public bool IsDisplayNotExistCardNotify { get; set; } = true;
        public bool IsAutoRejectWarningPlate { get; set; } = false;

        public int TimeToDefautUI { get; set; } = 15;
    }
}
