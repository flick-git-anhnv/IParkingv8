using System.Globalization;

namespace IParkingv8.Cash.Model
{
    public static class ConvertText
    {
        public static string Reset_MoneyPayment = "0 VND";
    
        public static string FormatMoney(long money)
        {
            return money.ToString("N0", new CultureInfo("vi-VN"));
        }
    }
}
