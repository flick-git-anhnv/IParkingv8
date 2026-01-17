namespace iParkingv8.Object.ConfigObjects.LaneConfigs
{
    public class LaneOptionalConfig
    {
        public string TurnCollectionId { get; set; } = string.Empty;
        public bool IsRegisterTurnVehicle { get; set; } = false;
        public bool IsUseLoopImageForCardEvent { get; set; } = false;

        #region Hộp thoại cảnh báo
        public bool IsUseAlarmDialog { get; set; } = true;
        public int AutoRejectDialogTime { get; set; } = 0;
        public bool AutoRejectDialogResult { get; set; } = false;
        #endregion

        #region QR tĩnh
        public string StaticQRComport { get; set; } = string.Empty;
        public int StaticQRBaudrate { get; set; } = 115200;
        public int StaticQRType { get; set; } = 0;

        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        #endregion
    }
}
