using iParkingv8.Ultility.Style;

namespace iParkingv8.Object.Enums.ParkingEnums
{
    public enum EmAccessKeyStatus
    {
        LOCKED,
        IN_USE,
        UN_USED,
    }
    public static class AccessKeyStatus
    {
        public static string ToDisplayString(this EmAccessKeyStatus accessKeyStatus)
        {
            return accessKeyStatus switch
            {
                EmAccessKeyStatus.LOCKED => KZUIStyles.CurrentResources.AccesskeyStatusLocked,
                EmAccessKeyStatus.IN_USE => KZUIStyles.CurrentResources.AccesskeyStatusInUsed,
                EmAccessKeyStatus.UN_USED => KZUIStyles.CurrentResources.AccesskeyStatusNotUsed,
                _ => "",
            };
        }
    }
}
