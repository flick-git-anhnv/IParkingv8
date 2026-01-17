using iParkingv8.Object.Objects.Licenses;
using iParkingv8.Object.Objects.Parkings;
using iParkingv8.Object.Objects.Users;

namespace iParkingv8.Object
{
    public static class StaticPool
    {
        public static User? SelectedUser = null;
        public static DateTime LoginTime = DateTime.Now;
        public static string LastUserId = string.Empty;
        public static List<SystemConfig> configs = new List<SystemConfig>();
        public static LicenseExpire? LicenseExpire;
    }
}
