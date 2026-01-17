namespace Kztek.Object
{
    public class CardFormat
    {
        public enum EmCardFormat
        {
            DECIMA,
            HEXA,
            REHEXA,
            REDECIMA,
            XXX_XXXXX,
            PROXI_MA_SAU,
        }
        public enum EmCardFormatOption
        {
            Toi_Gian,
            Min_8,
            Min_10,
        }
        public static string ToString(EmCardFormat cardFormat) =>
            cardFormat == EmCardFormat.XXX_XXXXX ? "XXX:XXXXX" : cardFormat.ToString();

        public static string ToString(EmCardFormatOption option) =>
            option switch
            {
                EmCardFormatOption.Toi_Gian => "Tối Giản",
                EmCardFormatOption.Min_8 => "Tối Thiểu 8 Ký Tự",
                EmCardFormatOption.Min_10 => "Tối Thiểu 10 Ký Tự",
                _ => ""
            };
    }
}
