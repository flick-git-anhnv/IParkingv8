using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Object.Objects.Users;

namespace Kztek.Control8.UserControls.DialogUcs
{
    public class ConfirmOutRequest
    {
        //Thông tin chung
        public User? User { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public EmAlarmCode AbnormalCode { get; set; } = EmAlarmCode.NONE;
        public string DetectedPlate { get; set; } = string.Empty;
        public string AlarmPlate { get; set; } = string.Empty;

        //Thông tin vào
        public Dictionary<EmImageType, Image?> EntryImages { get; set; } = [];
        public List<VoucherApply> VoucherApplies { get; set; } = new List<VoucherApply>();
       
        //Thông tin ra
        public Dictionary<EmImageType, Image?> ExitImages { get; set; } = [];
        public ExitData? EventOut { get; set; }
    }
}
