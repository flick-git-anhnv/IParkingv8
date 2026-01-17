
using iParkingv8.Object.Enums.Bases;

namespace iParkingv8.Object.Objects.Events
{
    public class BaseEvent
    {
        public List<string> ErrorMessages { get; set; } = [];
        public List<EmAlarmCode> GetAlarmCode()
        {
            if (ErrorMessages.Count == 0) return [];
            var result = new List<EmAlarmCode>();
            foreach (var message in ErrorMessages)
            {
                switch (message)
                {
                    case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_SYSTEM":
                        result.Add(EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM);
                        break;
                    case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_ENTRY":
                        result.Add(EmAlarmCode.PLATE_NUMBER_INVALID);
                        break;
                    case "ERROR.ENTITY.VALIDATION.BLACKLISTED":
                        result.Add(EmAlarmCode.PLATE_NUMBER_BLACKLISTED);
                        break;
                    default:
                        result.Add(EmAlarmCode.SYSTEM_ERROR);
                        break;
                }
            }
            return result;
        }

        public List<string> ToVI()
        {
            if (ErrorMessages.Count == 0) return [];

            var result = new List<string>();
            foreach (var message in ErrorMessages)
            {
                switch (message)
                {
                    case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_SYSTEM":
                        result.Add("Biển số không khớp với biển đăng ký");
                        break;
                    case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_ENTRY":
                        result.Add("Biển số vào ra không khớp");
                        break;
                    case "ERROR.ENTITY.VALIDATION.BLACKLISTED":
                        break;
                    default:
                        result.Add(message);
                        break;
                }
            }

            return result;
        }
    }
}
