using iParkingv8.Object.Enums.Bases;
using iParkingv8.Object.Objects.Parkings;

namespace IParkingv8.Helpers.CardProcess
{
    public class BaseCardValidate
    {
        public EmAlarmCode CardValidateType { get; set; } = EmAlarmCode.NONE;
        public AccessKey? AccessKey { get; set; }
        public string AlarmDescription { get; set; } = string.Empty;
        public string DisplayAlarmMessageTag { get; set; } = string.Empty;
        public static BaseCardValidate CreateDefault() => new() { AccessKey = null, CardValidateType = EmAlarmCode.NONE };
    }
}
