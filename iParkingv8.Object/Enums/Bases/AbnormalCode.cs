using System.Drawing;

namespace iParkingv8.Object.Enums.Bases
{
    public enum EmAlarmCode
    {
        // Mã thẻ không có trong hệ thông
        ACCESS_KEY_NOT_FOUND,
        // Mã thẻ không hợp lệ
        ACCESS_KEY_INVALID,
        //Định danh bị khóa
        ACCESS_KEY_LOCKED,
        //Định danh hết hạn
        ACCESS_KEY_EXPIRED,
        //Sai quyền truy cập
        ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE,
        // Biển số khác với biển đăng ký
        PLATE_NUMBER_DIFFERENCE_SYSTEM,
        // Biển số vào ra không khớp
        PLATE_NUMBER_INVALID,
        // Biển số đen
        PLATE_NUMBER_BLACKLISTED,
        // Mở barrie bằng bàn phím
        BARRIER_OPENED_BY_KEYBOARD,
        // Mở barrie bằng nút nhấn
        BARRIER_OPENED_BY_BUTTON,
        // Ghi vé
        ENTRY_CREATED_MANUALLY,
        // Xe đã vào bãi
        ENTRY_DUPLICATED,
        // Xe chưa vào bãi
        ENTRY_NOT_FOUND,
        // Xe đè vòng loop
        VEHICLE_ON_LOOP,
        // Xóa sự kiện xe đang trong bãi
        ENTRY_DELETED,
        UNRETURNED_CARD,
        ACCESS_KEY_COLLECTION_CHANGED,
        // Lỗi hệ thống
        SYSTEM_ERROR,
        NONE,
    }

    public static class AlarmCodeExtension
    {
        public static string ToString(this EmAlarmCode code)
        {
            switch (code)
            {
                case EmAlarmCode.ACCESS_KEY_NOT_FOUND:
                    return "Mã thẻ không có trong hệ thống";
                case EmAlarmCode.ACCESS_KEY_INVALID:
                    return "Thẻ / Xe không hợp lệ";
                case EmAlarmCode.ACCESS_KEY_LOCKED:
                    return "Định danh bị khóa";
                case EmAlarmCode.ACCESS_KEY_EXPIRED:
                    return "Phương tiện hết hạn sử dụng";
                case EmAlarmCode.ACCESS_KEY_COLLECTION_NOT_ALLOWED_FOR_LANE:
                    return "Nhóm định danh không được phép sử dụng với làn";
                case EmAlarmCode.PLATE_NUMBER_DIFFERENCE_SYSTEM:
                    return "Biển số khác với biển đăng ký";
                case EmAlarmCode.PLATE_NUMBER_INVALID:
                    return "Biển số vào ra không khớp";
                case EmAlarmCode.PLATE_NUMBER_BLACKLISTED:
                    return "Biển số đen";
                case EmAlarmCode.BARRIER_OPENED_BY_KEYBOARD:
                    return "Mở barrie bằng bàn phím";
                case EmAlarmCode.BARRIER_OPENED_BY_BUTTON:
                    return "Mở barrie bằng nút nhấn";
                case EmAlarmCode.ENTRY_CREATED_MANUALLY:
                    return "Ghi vé";
                case EmAlarmCode.ENTRY_DUPLICATED:
                    return "Xe đã vào bãi";
                case EmAlarmCode.ENTRY_NOT_FOUND:
                    return "Xe chưa vào bãi";
                case EmAlarmCode.VEHICLE_ON_LOOP:
                    return "Xe đè vòng loop";
                case EmAlarmCode.ENTRY_DELETED:
                    return "Xóa sự kiện xe trong bãi";
                case EmAlarmCode.UNRETURNED_CARD:
                    return "Nợ thẻ";
                case EmAlarmCode.ACCESS_KEY_COLLECTION_CHANGED:
                        return "Đổi nhóm thẻ";
                default:
                    return "";
            }
        }
    }
}
