using iParkingv8.Object.Enums.Bases;
using iParkingv8.Ultility.Style;

namespace iParkingv8.Object.Objects.Bases
{
    public class BaseErrorData
    {
        const string NOT_FOUND_ERROR = "ERROR.ENTITY.NOT_FOUND";

        const string VALIDATE_NOTFOUND_ERROR = "ERROR.ENTITY.VALIDATION.FIELD_NOT_FOUND";
        const string VALIDATE_DUPLICATED_ERROR = "ERROR.ENTITY.VALIDATION.FIELD_DUPLICATED";
        const string VALIDATE_INVALID_ERROR = "ERROR.ENTITY.VALIDATION.FIELD_INVALID";
        const string VALIDATE_NOT_ACTIVE_ERROR = "ERROR.ENTITY.VALIDATION.FIELD_NOT_ACTIVE";

        public string Route { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string DetailCode { get; set; } = string.Empty;
        public List<ErrorDescription> Fields { get; set; } = [];
        public Dictionary<string, object>? Payload = [];

        public override string ToString()
        {
            if (Fields == null || Fields.Count == 0)
            {
                if (Route.Equals("access-keys", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (ErrorCode)
                    {
                        case NOT_FOUND_ERROR: return KZUIStyles.CurrentResources.AccessKeyNotFound;
                        default:
                            break;
                    }
                }
                return string.Empty;
            }
            else
            {
                string name = Fields[0].Name;
                string errorCode = Fields[0].ErrorCode;

                if (Route.Equals("discounts", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (name.Equals("VoucherCode", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case VALIDATE_DUPLICATED_ERROR:
                                return KZUIStyles.CurrentResources.VoucherInUsedError;
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_FOUND":
                                return KZUIStyles.CurrentResources.VoucherNotFound;
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_ACTIVE":
                                return KZUIStyles.CurrentResources.VoucherNotActived;
                            default:
                                break;
                        }
                    }
                    else if (name.Equals("FromDate", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (errorCode == "ERROR.ENTITY.VALIDATION.FIELD_INVALID")
                        {
                            return KZUIStyles.CurrentResources.VoucherNotActived;
                        }
                    }
                    else if (name.Equals("ToDate", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (errorCode == "ERROR.ENTITY.VALIDATION.FIELD_INVALID")
                        {
                            return KZUIStyles.CurrentResources.VoucherExpired;
                        }
                    }
                    else if (name.Equals("TimeOfDay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (errorCode == "ERROR.ENTITY.VALIDATION.FIELD_INVALID")
                        {
                            return KZUIStyles.CurrentResources.VoucherInvalidTime;
                        }
                    }
                    else if (name.Equals("AccessKeyCode", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_FOUND":
                                return KZUIStyles.CurrentResources.AccessKeyNotFound;
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_ACTIVE":
                                return KZUIStyles.CurrentResources.AccessKeyLocked;
                            default:
                                break;
                        }
                    }
                    else if (name.Equals("Collection", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_ACTIVE":
                                return KZUIStyles.CurrentResources.CollectionLocked;
                            default:
                                break;
                        }
                    }
                    else if (name.Equals("collectionId", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_ATTRIBUTES":
                                return KZUIStyles.CurrentResources.VoucherInvalidType;
                            default:
                                break;
                        }
                    }
                    else if (name.Equals("Entry", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case "ERROR.ENTITY.VALIDATION.FIELD_NOT_FOUND":
                                return KZUIStyles.CurrentResources.EntryNotFound;
                            case "ERROR.ENTITY.VALIDATION.FIELD_DUPLICATED":
                                return KZUIStyles.CurrentResources.VoucherInUsedError;
                            default:
                                break;
                        }
                    }
                    else if (name.Equals("Exit", StringComparison.CurrentCultureIgnoreCase))
                    {
                        switch (errorCode)
                        {
                            case "ERROR.ENTITY.VALIDATION.FIELD_DUPLICATED":
                                return KZUIStyles.CurrentResources.VoucherInUsedError;
                            default:
                                break;
                        }
                    }
                }
                else if (name.Equals("AccessKeyId", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case VALIDATE_NOTFOUND_ERROR:
                            if (Route.Equals("entries", StringComparison.CurrentCultureIgnoreCase))
                            {
                                return KZUIStyles.CurrentResources.AccessKeyNotFound;
                            }
                            if (Route.Equals("exits", StringComparison.CurrentCultureIgnoreCase))
                            {
                                return KZUIStyles.CurrentResources.EntryNotFound;
                            }
                            break;
                        case VALIDATE_DUPLICATED_ERROR:
                            if (Route.Equals("entries", StringComparison.CurrentCultureIgnoreCase))
                                return KZUIStyles.CurrentResources.EntryDupplicated;
                            break;
                        case VALIDATE_NOT_ACTIVE_ERROR:
                            return KZUIStyles.CurrentResources.AccessKeyLocked;
                        default:
                            break;
                    }
                }
                else if (name.Equals("Metrics", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (errorCode == VALIDATE_NOT_ACTIVE_ERROR)
                    {
                        return KZUIStyles.CurrentResources.AccessKeyExpired;
                    }
                }
                else if (name.Equals("DeviceId", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case VALIDATE_NOTFOUND_ERROR:
                            return "Làn không có trong hệ thống";
                        case VALIDATE_INVALID_ERROR:
                            return KZUIStyles.CurrentResources.InvalidPermission;
                        default:
                            break;
                    }
                }
                else if (name.Equals("PlateNumber", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_SYSTEM":
                            return KZUIStyles.CurrentResources.PlateNotMatchWithSystem.ToUpper();
                        case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_ENTRY":
                            return KZUIStyles.CurrentResources.PlateInOutNotSame.ToUpper();
                        case "ERROR.ENTITY.VALIDATION.BLACKLISTED":
                            return KZUIStyles.CurrentResources.BlackedList.ToUpper();
                        default:
                            break;
                    }
                }
                else if (name.Equals("Exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case "ERROR.ENTITY.VALIDATION.EVENT_IN_COOLDOWN":
                            return KZUIStyles.CurrentResources.InExitWaitingTime.ToUpper();
                    }
                }
                else if (name.Equals("Entry", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case "ERROR.ENTITY.VALIDATION.EVENT_IN_COOLDOWN":
                            return KZUIStyles.CurrentResources.InEntryWaitingTime.ToUpper();
                    }
                }

                return name + " - " + errorCode;
            }
        }
        public EmAlarmCode GetAbnormalCode()
        {
            if (Fields == null || Fields.Count == 0)
            {
                if (Route.Equals("access-keys", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (ErrorCode)
                    {
                        case NOT_FOUND_ERROR: return EmAlarmCode.ACCESS_KEY_NOT_FOUND;
                        default:
                            break;
                    }
                }
                return EmAlarmCode.SYSTEM_ERROR;
            }
            else
            {
                string name = Fields[0].Name;
                string errorCode = Fields[0].ErrorCode;

                if (name.Equals("AccessKeyId", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case VALIDATE_NOTFOUND_ERROR:
                            if (Route.Equals("entries", StringComparison.CurrentCultureIgnoreCase))
                            {
                                return EmAlarmCode.ACCESS_KEY_NOT_FOUND;
                            }
                            if (Route.Equals("exits", StringComparison.CurrentCultureIgnoreCase))
                            {
                                return EmAlarmCode.ENTRY_NOT_FOUND;
                            }
                            break;
                        case VALIDATE_DUPLICATED_ERROR:
                            if (Route.Equals("entries", StringComparison.CurrentCultureIgnoreCase))
                                return EmAlarmCode.ENTRY_DUPLICATED;
                            break;
                        case VALIDATE_NOT_ACTIVE_ERROR:
                            return EmAlarmCode.ACCESS_KEY_LOCKED;
                        default:
                            break;
                    }
                }
                else if (name.Equals("Metrics", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (errorCode == VALIDATE_NOT_ACTIVE_ERROR)
                    {
                        return EmAlarmCode.ACCESS_KEY_EXPIRED;
                    }
                }
                else if (name.Equals("DeviceId", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case VALIDATE_NOTFOUND_ERROR:
                            return EmAlarmCode.ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE;
                        case VALIDATE_INVALID_ERROR:
                            return EmAlarmCode.ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE;
                        default:
                            break;
                    }
                }
                else if (name.Equals("PlateNumber", StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (errorCode)
                    {
                        case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_SYSTEM":
                            return EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM;
                        case "ERROR.ENTITY.VALIDATION.FIELD_NOT_MATCH_WITH_ENTRY":
                            return EmAlarmCode.PLATE_NUMBER_INVALID;
                        case "ERROR.ENTITY.VALIDATION.BLACKLISTED":
                            return EmAlarmCode.PLATE_NUMBER_BLACKLISTED;
                        default:
                            break;
                    }
                }

                return EmAlarmCode.SYSTEM_ERROR;
            }
        }
    }
    public class ErrorDescription
    {
        public string Name { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
    }
}
