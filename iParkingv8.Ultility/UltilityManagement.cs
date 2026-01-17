namespace iParkingv8.Ultility
{
    public static class UltilityManagement
    {
        public const string TIME_FORMAT = "HH:mm:ss";
        public const string DAY_FORMAT = "dd/MM/yyyy";
        public const string UTC_FORMAT = "yyyy-MM-ddTHH:mm:ss:ffff";
        public const string FULL_DAY_FORMAT = "dd/MM/yyyy HH:mm:ss";

        public static string ToVNTime(this DateTime? time)
        {
            return time?.ToString(FULL_DAY_FORMAT) ?? "";
        }
        public static string ToVNTime(this DateTime time)
        {
            return time.ToString(FULL_DAY_FORMAT) ?? "";
        }
    }
}
