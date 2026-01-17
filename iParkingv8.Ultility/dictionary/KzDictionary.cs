using Kztek.Object;
using Kztek.Tool;

namespace iParkingv8.Ultility.dictionary
{
    public static class KzDictionary
    {
        public const string EMPTY_STRING = "-";

        public const string KZTEK_MASTER_PASSWORD = "Kztek123456";
        public const string FUTECH_MASTER_PASSWORD = "Futech123456";

        public static bool IsMasterPassword(string password)
        {
            return password == KZTEK_MASTER_PASSWORD || password == FUTECH_MASTER_PASSWORD;
        }

        //public static string IN_UC_EVENT_IMAGE_TITLE() => "ẢNH VÀO";
        //public static string OUT_UC_EVENT_IMAGE_TITLE() => "ẢNH RA";
        //public static string IN_UC_APP_FUNCTION_TITLE() => "CHỨC NĂNG";
        //public static string IN_UC_PLATE_TITLE() => "BIỂN SỐ VÀO";
        //public static string OUT_UC_PLATE_TITLE() => "BIỂN SỐ RA";

        //public static string IN_UC_EVENT_PANORAMA_TITLE() => "TC Vào";
        //public static string OUT_UC_EVENT_PANORAMA_TITLE() => "TC RA";
        //public static string IN_UC_EVENT_VEHICLE_TITLE() => "BS Vào";
        //public static string MERGE_TITLE() => "ẢNH VÀO RA";
        //public static string OUT_UC_EVENT_VEHICLE_TITLE() => "BS RA";

        //public static string KIOSK_OUT_MONTH_CARD_SUBTITLE_VI = "<center><span style=\"font-size: 24px;>Vui lòng <strong style=\"color:rgb(242, 102, 51)\">Quẹt thẻ</strong> vào ô thẻ tháng</span><br/>hoặc nhấn nút<strong style=\"color:rgb(242, 102, 51)\"> Chụp biển số</strong>";
        //public static string KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE_VI = "<center><span style=\"font-size: 24px;>Vui lòng <strong style=\"color:rgb(242, 102, 51)\">Quẹt thẻ</strong> vào ô thẻ tháng";

        //public static string KIOSK_OUT_MONTH_CARD_SUBTITLE_EN = "<center><span style=\"font-size: 24px;>Please <strong style=\"color:rgb(242, 102, 51)\">Swipe your card</strong> on the monthly card reader Or press the <strong style=\"color:rgb(242, 102, 51)\">Detect plate</strong> button </span>";
        //public static string KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE_EN = "<center><span style=\"font-size: 24px;>Please <strong style=\"color:rgb(242, 102, 51)\">Swipe your card</strong> on the monthly card reader";

        //public static string KIOSK_IN_DAILY_CARD_SUBTITLE_VI = "<center><span style = \"font-size: 24px;>Vui lòng bấm vào nút lấy thẻ<br/><strong style=\"color:rgb(242, 102, 51)\"> phía bên dưới</strong></span>";
        //public static string KIOSK_IN_DAILY_CARD_SUBTITLE_EN = "<center><span style = \"font-size: 24px;>Please press <strong style=\"color:rgb(242, 102, 51)\"> the button below</strong><br/>to get the ticket</span>";

        //public static string KIOSK_OUT_DAILY_CARD_SUBTITLE_VI = "<center><span style = \"font-size: 24px;>Vui lòng đưa thẻ vào khe trả thẻ<br/><strong style=\"color:rgb(242, 102, 51)\"> phía bên dưới</strong></span>";
        //public static string KIOSK_OUT_DAILY_CARD_SUBTITLE_EN = "<center><span style = \"font-size: 24px;>Please insert your card into <br/><strong style=\"color:rgb(242, 102, 51)\">the card return slot below</strong></span>";
    }
}
