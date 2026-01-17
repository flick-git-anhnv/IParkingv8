using iParkingv8.Object.Enums.ParkingEnums;
using iParkingv8.Ultility.Style;

namespace iParkingv8.Object.Objects.Parkings
{
    public class AccessKey
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public EmAccessKeyType Type { get; set; } = EmAccessKeyType.CARD;
        public EmAccessKeyStatus Status { get; set; } = EmAccessKeyStatus.IN_USE;
        public string Note { get; set; } = string.Empty;
        public List<AccessKey> Metrics { get; set; } = [];
        public string GetStatusName()
        {
            return Status switch
            {
                EmAccessKeyStatus.LOCKED => KZUIStyles.CurrentResources.AccesskeyStatusLocked,
                EmAccessKeyStatus.IN_USE => KZUIStyles.CurrentResources.AccesskeyStatusInUsed,
                EmAccessKeyStatus.UN_USED => KZUIStyles.CurrentResources.AccesskeyStatusNotUsed,
                _ => "",
            };
        }

        public string GetTypeName()
        {
            return this.Type switch
            {
                EmAccessKeyType.VEHICLE => KZUIStyles.CurrentResources.AccesskeyTypeVehicle,
                EmAccessKeyType.CARD => KZUIStyles.CurrentResources.AccesskeyTypeCard,
                EmAccessKeyType.QRCODE => KZUIStyles.CurrentResources.AccesskeyTypeQR,
                EmAccessKeyType.FINGER_PRINT => KZUIStyles.CurrentResources.AccesskeyTypeFinger,
                EmAccessKeyType.FACE_ID => KZUIStyles.CurrentResources.AccesskeyTypeFace,
                _ => this.Type.ToString(),
            };
        }

        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Chú ý Get thông tin thẻ sẽ không trả về thẳng thông tin khách hàng
        /// </summary>
        public CustomerDto? Customer { get; set; } = null;

        public string ExpiredUtc { get; set; } = string.Empty;
        public DateTime? ExpireTime
        {
            get
            {
                if (string.IsNullOrEmpty(ExpiredUtc))
                {
                    return null;
                }
                try
                {
                    if (ExpiredUtc.Contains('T'))
                    {
                        return DateTime.ParseExact(ExpiredUtc[.."yyyy-MM-ddTHH:mm:ss".Length], "yyyy-MM-ddTHH:mm:ss", null).AddHours(7);
                    }
                    else
                    {
                        return DateTime.Parse(ExpiredUtc).AddHours(7);
                    }
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }
        public AccessKey? GetVehicleInfo()
        {
            if (Type == EmAccessKeyType.VEHICLE)
            {
                return this;
            }
            else
            {
                if (Metrics != null && Metrics.Count > 0)
                {
                    return Metrics[0];
                }
            }
            return null;
        }

        public Collection? Collection { get; set; } = null;
    }

}
