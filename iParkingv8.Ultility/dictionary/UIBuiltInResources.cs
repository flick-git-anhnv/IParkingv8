using iParkingv8.Ultility.Style;
using System.Globalization;

namespace iParkingv8.Ultility.dictionary
{
    public abstract class UIBuiltInResources
    {
        public abstract CultureInfo CultureInfo { get; }
        public virtual string EventInfo { get; set; } = "Thông tin sự kiện";

        public virtual string MiSystem { get; set; } = "Hệ thống";
        public virtual string MiChangePassword { get; set; } = "Đổi mật khẩu";
        public virtual string MiLogOut { get; set; } = "Đăng xuất";
        public virtual string MiExit { get; set; } = "Thoát";

        public virtual string MiReport { get; set; } = "Báo cáo";
        public virtual string MiReportIn { get; set; } = "Xe đang gửi";
        public virtual string MiReportOut { get; set; } = "Xe đã ra";
        public virtual string MiReportRevenue { get; set; } = "Báo cáo doanh thu";
        public virtual string MiReportHandOver { get; set; } = "Báo cáo chốt ca";

        public virtual string MiData { get; set; } = "Dữ liệu";
        public virtual string MiAccessKeyList { get; set; } = "Danh sách định danh";
        public virtual string MiCustomerList { get; set; } = "Danh sách khách hàng";
        public virtual string MiVehicleList { get; set; } = "Danh sách phương tiện";

        public virtual string LicenseValid { get; set; } = "HSD";
        public virtual string Name { get; set; } = "Tên";
        public virtual string Day { get; set; } = "Ngày";
        public virtual string Quantity { get; set; } = "Số lượng";
        public virtual string Printer { get; set; } = "Máy in";

        public virtual string InfoTitle { get; set; } = "Thông báo";
        public virtual string SuccessTitle { get; set; } = "Thành công";
        public virtual string WarningTitle { get; set; } = "Cảnh báo";
        public virtual string ErrorTitle { get; set; } = "Lỗi";
        public virtual string AskTitle { get; set; } = "Câu hỏi";
        public virtual string InputTitle { get; set; } = "Nhập thông tin";
        public virtual string SelectTitle { get; set; } = "Chọn thông tin";
        public virtual string CloseAll { get; set; } = "Đóng tất cả";

        public virtual string Confirm { get; set; } = "Xác nhận";
        public virtual string Cancel { get; set; } = "Đóng";
        public virtual string Back { get; set; } = "Quay lại";
        public virtual string BackToMain { get; set; } = "Quay lại trang chủ";
        public virtual string Clear { get; set; } = "Xóa";
        public virtual string ProcessChecking { get; set; } = "Kiểm tra thông tin";
        public virtual string ProcessReadingPlate { get; set; } = "Nhận dạng biển số xe";
        public virtual string ProcessCheckIn { get; set; } = "Đang check in";
        public virtual string ProcessCheckOut { get; set; } = "Đang check out";
        public virtual string ProcessInvalidDailyVehicleIn { get; set; } = "Không rõ biển số! Bạn có muốn cho xe vào bãi?";
        public virtual string ProcessConfirmOpenbarrie { get; set; } = "Bạn có xác nhận mở barrie?";
        public virtual string ProcessConfirmRequest { get; set; } = "Yêu cầu xác nhận";
        public virtual string ProcessOpenBarrie { get; set; } = "Đang mở barrie";
        public virtual string ProcessSaveImage { get; set; } = "Lưu hình ảnh sự kiện";
        public virtual string ProccesNotConfirmVehicleEntry { get; set; } = "Không xác nhận cho xe vào bãi";
        public virtual string ProccesNotConfirmVehicleExit { get; set; } = "Không xác nhận cho xe ra khỏi bãi";
        public virtual string ProcessGetEntryInfor { get; set; } = "Đọc thông tin xe vào";
        public virtual string ProcessConfirmCloseApp { get; set; } = "Bạn có chắc chắn muốn đóng ứng dụng?";

        public virtual string StartTime { get; set; } = "Bắt đầu";
        public virtual string EndTime { get; set; } = "Kết thúc";

        public virtual string Search { get; set; } = "Tìm kiếm";
        public virtual string Register { get; set; } = "Đăng ký";
        public virtual string CheckFee { get; set; } = "Kiểm tra phí";
        public virtual string ExportExcel { get; set; } = "Xuất Excel";
        public virtual string Open { get; set; } = "Mở";
        public virtual string Save { get; set; } = "Lưu";
        public virtual string SaveConfigSuccess { get; set; } = "Lưu cấu hình thành công";
        public virtual string ClearConfigSuccess { get; set; } = "Xóa cấu hình thành công";
        public virtual string Config { get; set; } = "Cấu hình hệ thông";
        public virtual string All { get; set; } = "Tất cả";
        public virtual string Welcome { get; set; } = "Xin mời qua";
        public virtual string SeeYouAgain { get; set; } = "Hẹn gặp lại";
        public virtual string TakeMoney { get; set; } = "Thu tiền";
        public virtual string Keyword { get; set; } = "Từ khóa";
        public virtual string Status { get; set; } = "Trạng thái";
        public virtual string TitlePicMerge { get; set; } = "ẢNH VÀO RA";
        public virtual string TitlePicIn { get; set; } = "ẢNH VÀO";
        public virtual string TitlePanoramaIn { get; set; } = "TOÀN CẢNH VÀO";
        public virtual string TitleFaceIn { get; set; } = "FACE VÀO";
        public virtual string TitleOtherIn { get; set; } = "KHÁC";
        public virtual string TitleVehicleIn { get; set; } = "XE VÀO";

        public virtual string TitlePicOut { get; set; } = "ẢNH RA";
        public virtual string TitlePanoramaOut { get; set; } = "TOÀN CẢNH RA";
        public virtual string TitleVehicleOut { get; set; } = "XE RA";
        public virtual string TitleFaceOut { get; set; } = "FACE RA";
        public virtual string TitleOtherOut { get; set; } = "KHÁC";
        //Form
        public virtual string FrmLogin { get; set; } = "Đăng nhập hệ thống";
        public virtual string FrmLoading { get; set; } = "Tải cấu hình hệ thống";
        public virtual string FrmSelectLane { get; set; } = "Chọn làn xe hoạt động";
        public virtual string FrmChangePassword { get; set; } = "Đổi mật khẩu";
        public virtual string FrmVerifyPassword { get; set; } = "Xác thực mật khẩu";
        public virtual string FrmTestController { get; set; } = "Kiểm tra thiết bị";
        public virtual string FrmTestLed { get; set; } = "Kiểm tra bảng LED";
        public virtual string FrmSelectVehicle { get; set; } = "Xác thực phương tiện";
        public virtual string FrmSelectAccessKeyCollection { get; set; } = "Đổi nhóm định danh";
        public virtual string FrmAccessKeyList { get; set; } = "Danh sách định danh";
        public virtual string FrmCustomerList { get; set; } = "Danh sách khách hàng";
        public virtual string FrmVehicleList { get; set; } = "Danh sách phương tiện";
        public virtual string FrmEditPlate { get; set; } = "Cập nhật thông tin biển số xe";

        #region OBJECTS - USER
        public virtual string User { get; set; } = "Người dùng";
        public virtual string UserIn { get; set; } = "Giám sát vào";
        public virtual string UserOut { get; set; } = "Giám sát ra";
        public virtual string Username { get; set; } = "Tên đăng nhập";
        public virtual string Password { get; set; } = "Mật khẩu";
        public virtual string CurrentPassword { get; set; } = "Mật khẩu hiện tại";
        public virtual string NewPassword { get; set; } = "Mật khẩu mới";
        public virtual string ConfirmPassword { get; set; } = "Nhập lại mật khẩu mới";
        #endregion

        #region OBJECTS - ACCESS KEY
        public virtual string AccesskeyList { get; set; } = "Danh sách định danh";
        public virtual string AccesskeyName { get; set; } = "Vé xe";
        public virtual string AccesskeyCode { get; set; } = "Mã";
        public virtual string AccesskeyNote { get; set; } = "Ghi chú";

        public virtual string AccesskeyStatusLocked { get; set; } = "Khóa";
        public virtual string AccesskeyStatusNotUsed { get; set; } = "Chưa sử dụng";
        public virtual string AccesskeyStatusInUsed { get; set; } = "Đang sử dụng";

        public virtual string AccesskeyTypeVehicle { get; set; } = "Phương tiện";
        public virtual string AccesskeyTypeCard { get; set; } = "Thẻ";
        public virtual string AccesskeyTypeQR { get; set; } = "Mã QR";
        public virtual string AccesskeyTypeFinger { get; set; } = "Vân tay";
        public virtual string AccesskeyTypeFace { get; set; } = "Gương mặt";
        #endregion END OBJECTS - ACCESS KEY

        #region OBJECTS - LANE
        public virtual string Lane { get; set; } = "Làn";
        public virtual string LaneIn { get; set; } = "Làn vào";
        public virtual string LaneOut { get; set; } = "Làn ra";
        #endregion

        #region OBJECTS - INVOICE
        public virtual string InvoiceTemplate { get; set; } = "Mẫu hóa đơn";
        public virtual string InvoiceNo { get; set; } = "Số hóa đơn";
        #endregion

        #region OBJECTS - VEHICLE
        public virtual string Vehicles { get; set; } = "Xe";

        public virtual string VehicleList { get; set; } = "Danh sách phương tiện";
        public virtual string VehicleName { get; set; } = "Tên phương tiện";
        public virtual string VehicleCode { get; set; } = "Biển số đăng ký";
        public virtual string VehicleType { get; set; } = "Loại xe";
        public virtual string VehicleCodeAcronym { get; set; } = "Đăng ký";
        public virtual string VehicleExpiredDate { get; set; } = "Ngày hết hạn";

        public virtual string VehicleTypeCar { get; set; } = "Ô tô";
        public virtual string VehicleTypeMotor { get; set; } = "Xe máy";
        public virtual string VehicleTypeBike { get; set; } = "Xe đạp";
        public virtual string CurrentPlate { get; set; } = "Biển số hiện tại";
        public virtual string NewPlate { get; set; } = "Biển số mới";

        #endregion END OBJECTS - VEHICLE

        #region OBJECTS - CUSTOMER
        public virtual string CustomerList { get; set; } = "Danh sách khách hàng";
        public virtual string CustomerName { get; set; } = "Tên khách hàng";
        public virtual string CustomerCode { get; set; } = "Mã khách hàng";
        #endregion END OBJECTS - CUSTOMER

        #region OBJECTS - COLLECTION
        public virtual string AccessKeyCollectionList { get; set; } = "Danh sách nhóm thẻ";
        public virtual string AccessKeyCollection { get; set; } = "Nhóm định danh";
        public virtual string CustomerCollection { get; set; } = "Nhóm khách hàng";

        public virtual string CollectionVIP { get; set; } = "Xe VIP";
        public virtual string CollectionMonth { get; set; } = "Xe tháng";
        public virtual string CollectionDaily { get; set; } = "Xe lượt";

        #endregion END OBJECTS - COLLECTION

        #region OBJECTS - VOUCHER
        public virtual string VoucherList { get; set; } = "Danh sách voucher";
        public virtual string VoucherName { get; set; } = "Voucher";
        #endregion

        #region OBJECTS - TRANSACTION
        public virtual string TransactionId { get; set; } = "Mã giao dịch";
        #endregion

        #region OBJECTS - EVENT
        public virtual string CameraIn { get; set; } = "CAMERA LỐI VÀO";
        public virtual string CameraOut { get; set; } = "CAMERA LỐI RA";
        public virtual string Plate { get; set; } = "Biển số xe";

        public virtual string PlateIn { get; set; } = "Biển số vào";
        public virtual string PlateOut { get; set; } = "Biển số ra";
        public virtual string PlateDetected { get; set; } = "Biển số nhận dạng";
        public virtual string PlateDetectedAcronym { get; set; } = "Nhận dạng";

        public virtual string TimeIn { get; set; } = "Giờ vào";
        public virtual string TimeOut { get; set; } = "Giờ ra";
        public virtual string Duration { get; set; } = "Thời gian đỗ xe";

        public virtual string PicPanoramaIn { get; set; } = "Ảnh toàn cảnh vào";
        public virtual string PicVehicleIn { get; set; } = "Ảnh xe vào";
        public virtual string PicPlateIn { get; set; } = "Ảnh biển số vào";

        public virtual string PicPanoramaOut { get; set; } = "Ảnh toàn cảnh ra";
        public virtual string PicVehicleOut { get; set; } = "Ảnh xe ra";
        public virtual string PicPlateOut { get; set; } = "Ảnh biển số ra";

        public virtual string PaymentInfo { get; set; } = "Thông tin thanh toán";
        public virtual string PaymentSupport { get; set; } = "Hỗ trợ các hình thức thanh toán";

        public virtual string EditNoteInfor { get; set; } = "Sửa ghi chú";
        public virtual string EditPlateInfo { get; set; } = "Sửa biển số xe";

        public virtual string Fee { get; set; } = "Phí gửi xe";
        public virtual string Paid { get; set; } = "Đã trả";
        public virtual string Discount { get; set; } = "Giảm trừ";
        public virtual string RealFee { get; set; } = "Thực thu";
        public virtual string Remain { get; set; } = "Còn lại";
        public virtual string VoucherInUse { get; set; } = "Voucher đã dùng";
        #endregion END OBJECTS - EVENT

        #region OBJECTS - DEVICE
        public virtual string Camera { get; set; } = "Camera";
        public virtual string Angle { get; set; } = "Góc quay";
        public virtual string Device { get; set; } = "Thiết bị";
        public virtual string Connected { get; set; } = "Có kết nối";
        public virtual string Disconnected { get; set; } = "Mất kết nối";
        public virtual string StopMode { get; set; } = "Dừng hoạt động";
        public virtual string CardOutputFormat { get; set; } = "Định dạng";
        public virtual string CardOutputAdditional { get; set; } = "Bổ sung";
        public virtual string FormatToiGian { get; set; } = "Tối giản";
        public virtual string FormatToiThieu8KyTu { get; set; } = "Tối thiểu 8 ký tự";
        public virtual string FormatToiThieu10KyTu { get; set; } = "Tối thiểu 10 ký tự";
        #endregion

        #region OBJECTS - LED
        public virtual string Led { get; set; } = "Led";
        #endregion

        #region OBJECTS - Shortcut
        public virtual string Shortcut { get; set; } = "Phím tắt";
        #endregion

        #region OBJECTS - Display
        public virtual string DisplaySetting { get; set; } = "Hiển thị";
        #endregion

        #region OBJECTS - OptionSetting
        public virtual string OptionSetting { get; set; } = "Tùy chọn";
        #endregion

        #region OBJECTS - ControllerSetting
        public virtual string ControllerSetting { get; set; } = "Bộ điều khiển";
        #endregion

        #region TIMER
        public virtual string AutoConfirmAfter { get; set; } = "Tự động xác nhận sau";
        public virtual string AutoCancelAfter { get; set; } = "Tự động đóng sau";
        public virtual string AutoLoginAfter { get; set; } = "Tự động đăng nhập sau";
        public virtual string AutoOpenHomePage { get; set; } = "Tự động mở giao diện phần mềm sau";
        #endregion

        #region Payment
        public virtual string PaymentQrTitle { get; set; } = "Quét mã QR để thanh toán";
        public virtual string PaymentVisaTitle { get; set; } = "Quẹt thẻ để thanh toán";
        public virtual string PaymentVisaSubTitle { get; set; } = "Vui lòng kiểm tra đúng thông tin giao dịch và quét thẻ ngân hàng vào thiết bị thanh toán";

        public virtual string PaymentVoucherTitle { get; set; } = "Quét mã voucher vào vị trí quét mã giảm giá";
        public virtual string PaymentVoucherSubTitle { get; set; } = "Thiết bị không trả lại tiền thừa";

        public virtual string PaymentCashTitle { get; set; } = "Vui lòng đưa từng tờ tiền vào khe nhận tiền";
        public virtual string PaymentCashSubTitle { get; set; } = "Thiết bị không trả lại tiền thừa";
        #endregion

        public virtual string DailyVehicle { get; set; } = "Xe lượt";
        public virtual string MonthVehicle { get; set; } = "Xe tháng";

        //Button
        #region BUTTON
        public virtual string RetakeImage { get; set; } = "Chụp biển số"; //Detect Plate
        public virtual string Setting { get; set; } = "Cài đặt"; //Configuration
        public virtual string WriteIn { get; set; } = "Ghi vé vào";
        public virtual string WriteOut { get; set; } = "Ghi vé ra";
        public virtual string Print { get; set; } = "In vé";

        public virtual string Liveview { get; set; } = "Xem trực tiếp";
        public virtual string CarLprDetect { get; set; } = "Nhận dạng ô tô";
        public virtual string MotorLprDetect { get; set; } = "Nhận dạng xe máy";
        public virtual string DrawLPR { get; set; } = "Vẽ vùng biển số";
        public virtual string DrawMotion { get; set; } = "Vẽ vùng chuyển động";
        public virtual string OpenBarrie { get; set; } = "Mở barrie";
        public virtual string CollectCard { get; set; } = "Thu thẻ";
        public virtual string RejectCard { get; set; } = "Nhả thẻ";
        public virtual string ChangCollection { get; set; } = "Đổi loại thẻ";
        public virtual string PayParkingFee { get; set; } = "Thanh toán phí gửi xe";
        public virtual string Cash { get; set; } = "Tiền mặt";
        public virtual string Voucher { get; set; } = "Phiếu giảm giá";
        public virtual string QR { get; set; } = "Mã QR";
        public virtual string VISA { get; set; } = "Thẻ ngân hàng";
        public virtual string ShortCutSelectGuide { get; set; } = "Enter để tìm kiếm.\r\nKích đúp chuột hoặc bấm Xác Nhận để chọn.";
        public virtual string ShortCutGuide2Line { get; set; } = "Enter để xác nhận\r\nEsc để đóng";
        public virtual string ShortCutGuide1Line { get; set; } = "Enter : Xác Nhận | ESC: Đóng";
        public virtual string CreateQRView { get; set; } = "Tạo QR";
        public virtual string ReCreateQRView { get; set; } = "Tạo lại QR";
        #endregion

        #region CHECKBOX
        public virtual string RememberPassword { get; set; } = "Tự động đăng nhập";
        public virtual string SelectAll { get; set; } = "Chọn tất cả";
        #endregion

        #region REPORT
        public virtual string Total { get; set; } = "Tổng số: ";
        public virtual string EventList { get; set; } = "Danh sách sự kiện";
        public virtual string ColTime { get; set; } = "Thời gian";
        public virtual string ColEventType { get; set; } = "Sự kiện";
        public virtual string ColCardOrLoop { get; set; } = "Đầu đọc | Loop";
        public virtual string ColCardNumber { get; set; } = "Mã thẻ";

        public virtual string colAccessKeyKeyword { get; set; } = "Tên | Mã | Ghi chú";
        public virtual string colAccessKeyName { get; set; } = "Tên";
        public virtual string colAccessKeyCode { get; set; } = "Mã";
        public virtual string colType { get; set; } = "Loại";
        public virtual string colStatus { get; set; } = "Trạng thái";
        public virtual string colNote { get; set; } = "Ghi chú";
        public virtual string colCollection { get; set; } = "Nhóm";

        //Danh sách khách hàng
        public virtual string colCustomerKeyword { get; set; } = "Tên | Mã";
        public virtual string colCustomerName { get; set; } = "Tên";
        public virtual string colCustomerCode { get; set; } = "Mã";
        public virtual string colCustomerPhone { get; set; } = "Số điện thoại";
        public virtual string colCustomerCollection { get; set; } = "Nhóm";
        public virtual string colCustomerAddress { get; set; } = "Địa chỉ";

        //Danh sách phương tiện
        public virtual string colVehicleName { get; set; } = "Tên";
        public virtual string colVehicleCode { get; set; } = "Biển số xe";
        public virtual string colVehicleAccessKey { get; set; } = "Định danh";
        public virtual string colVehicleStatus { get; set; } = "Trạng thái";
        public virtual string colVehicleType { get; set; } = "Loại";
        public virtual string colVehicleCustomerName { get; set; } = "Khách hàng";
        public virtual string colVehicleCustomerCollection { get; set; } = "Nhóm khách hàng";
        public virtual string colVehicleCustomerPhone { get; set; } = "SĐT";
        public virtual string colVehicleCustomerAddress { get; set; } = "Địa chỉ";
        public virtual string colVehicleExpiredDate { get; set; } = "Ngày hết hạn";
        public virtual string colVehicleCollection { get; set; } = "Nhóm";

        #endregion

        //Lỗi chung
        public virtual string AccountInvalidPermission { get; set; } = "Tài khoản của bạn không có quyền thực hiện chức năng này!";
        public virtual string ServerConfigInvalid { get; set; } = "Không tìm thấy cấu hình server hoặc cấu hình không hợp lệ!";
        public virtual string ActiveLicenseError { get; set; } = "Kích hoạt không thành công!";
        public virtual string InvalidEventInfo { get; set; } = "Không có thông tin sự kiện!";
        public virtual string InvalidPrintTemplate { get; set; } = "Không tìm thấy mẫu in!";
        public virtual string SystemError { get; set; } = "Gặp lỗi trong quá trình xử lý!";
        public virtual string ServerDisconnected { get; set; } = "Mất kết nối đến máy chủ!";
        public virtual string TryAgain { get; set; } = "Vui lòng thử lại!";
        public virtual string TryAgainLater { get; set; } = "Vui lòng thử lại sau!";
        public virtual string UnreturnedAccessKey { get; set; } = "Xe còn nợ thẻ cũ chưa check out.\r\nYêu cầu TRẢ THẺ.";
        public virtual string InEntryWaitingTime { get; set; } = "Trong thời gian chờ vào";
        public virtual string InExitWaitingTime { get; set; } = "Trong thời gian chờ ra";
        public virtual string GetCameraImageError { get; set; } = "Gặp lỗi khi lấy hình cảnh từ camera";


        //Login Error
        public virtual string InvalidLogin { get; set; } = "Đăng nhập không thành công";
        public virtual string InvalidPassword { get; set; } = "Mật khẩu không chính xác!";

        //Lỗi cấu hình thiết bị
        public virtual string InvalidDeviceConfig { get; set; } = "Chưa có cấu hình thiết bị!";
        public virtual string InvalidReserverLaneConfig { get; set; } = "Không có cấu hình làn đảo";

        //Lỗi đầu đọc thẻ
        public virtual string InvalidReaderConfig { get; set; } = "Đầu đọc thẻ chưa được khai báo trong hệ thống";

        public virtual string InvalidReaderDailyTitle { get; set; } = "Bạn đang sử dụng thẻ lượt!";
        public virtual string InvalidReaderDailySubTitle { get; set; } = "Vui lòng đưa thẻ vào khe trả thẻ lượt";

        public virtual string InvalidReaderMonthlyTitle { get; set; } = "Bạn đang sử dụng thẻ tháng!";
        public virtual string InvalidReaderMonthlySubTitle { get; set; } = "Vui lòng quẹt thẻ vào đầu đọc thẻ tháng";

        //Lỗi định danh
        public virtual string InvalidPermission { get; set; } = "Sai quyền truy cập!";
        public virtual string AccessKeyNotFound { get; set; } = "Thẻ không có trong hệ thống.";
        public virtual string AccessKeyNotSupport { get; set; } = "Thẻ không được hỗ trợ, vui lòng kiểm tra lại!";
        public virtual string AccessKeyInWaitingTime { get; set; } = "Bạn đã quẹt thẻ quá nhanh, vui lòng thử lại sau giây lát!";
        public virtual string AccessKeyLocked { get; set; } = "Thẻ của bạn chưa được kích hoạt!";
        public virtual string AccessKeyExpired { get; set; } = "Phương tiện của bạn đã hết hạn sử dụng!";
        public virtual string AccessKeyMonthNoVehicle { get; set; } = "Thẻ tháng chưa đăng ký phương tiện!";
        public virtual string AccessKeyVipNoVehicle { get; set; } = "Thẻ VIP chưa đăng ký phương tiện!";
        public virtual string BlackedList { get; set; } = "Biển số đã bị CHẶN.";

        //Lỗi nhóm định danh
        public virtual string CollectionLocked { get; set; } = "Nhóm định danh chưa được kích hoạt!";

        //Lỗi phương tiện
        public virtual string VehicleNotFound { get; set; } = "Phương tiện của bạn chưa được đăng ký trong hệ thống!";
        public virtual string VehicleLocked { get; set; } = "Phương tiện của bạn đã bị khóa!";
        public virtual string VehicleNotAllowEntryByPlate { get; set; } = "Phương tiện không được phép vào bằng biển số!";
        public virtual string VehicleNotAllowExityByPlate { get; set; } = "Phương tiện không được phép vào bằng biển số!";

        //Lỗi Entry
        public virtual string EntryNotFound { get; set; } = "Xe chưa vào bãi.";
        public virtual string EntryDupplicated { get; set; } = "Xe đã vào bãi";

        //Lỗi nhận dạng biển số
        public virtual string InvalidPlateNumber { get; set; } = "Không nhận dạng được biển số!";
        public virtual string PlateInOutNotSame { get; set; } = "Biển số check out KHÔNG KHỚP biển số lúc check in.";
        public virtual string PlateNotMatchWithSystem { get; set; } = "Biển số không khớp với biển đăng ký!";

        //Lỗi kết nối thiết bị
        public virtual string DeviceDisconnected { get; set; } = "Không kết nối được tới thiết bị!";

        //Áp dụng voucher
        public virtual string VoucherInvalidType { get; set; } = "Sai loại voucher!";
        public virtual string VoucherNotFound { get; set; } = "Voucher chưa được đăng ký trong hệ thống!";
        public virtual string VoucherNotActived { get; set; } = "Voucher chưa được kích hoạt!";
        public virtual string VoucherExpired { get; set; } = "Voucher đã hết hạn sử dụng!";
        public virtual string VoucherInvalidTime { get; set; } = "VOUCHER không được phép sử dụng trong khung giờ này!";
        public virtual string VoucherlistEmpty { get; set; } = "Không có voucher có thể sử dụng cho phương tiện!";
        public virtual string VoucherApplyError { get; set; } = "Gặp lỗi trong quá trình áp dụng voucher, vui lòng thử lại!";
        public virtual string VoucherInUsedError { get; set; } = "Voucher đã được sử dụng!";

        //Đổi nhóm thẻ
        public virtual string ChangeCollectionError { get; set; } = "Gặp lỗi trong quá trình đổi nhóm thẻ, vui lòng thử lại!";

        //Lỗi tạo giao dịch
        public virtual string TransactionCreateError { get; set; } = "Không tạo được giao dịch!";

        //Customer Action
        public virtual string ActiveLicenseRequired { get; set; } = "Ứng dụng chưa được kích hoạt! Bạn có muốn kích hoạt phần mềm?";
        public virtual string CustomerCommandOpenBarrie { get; set; } = "Ra lệnh mở barrie";
        public virtual string CustomerCommandWriteIn { get; set; } = "Ra lệnh ghi vé vào";
        public virtual string CustomerCommandWriteOut { get; set; } = "Ra lệnh ghi vé ra";
        public virtual string CustomerCommandCapture { get; set; } = "Ra lệnh chụp lại";
        public virtual string CustomerCommandPrintTicket { get; set; } = "Ra lệnh in vé xe";
        public virtual string CustomerCommandUpdatePlate { get; set; } = "Ra lệnh cập nhật biển số";
        public virtual string CustomerCommandReserverLane { get; set; } = "Ra lệnh đảo làn";
        public virtual string CustomerCommandOpenSettingPage { get; set; } = "Ra lệnh mở màn hình cài đặt";

        public virtual string PlateSame { get; set; } = "Biển số trùng khớp!";
        public virtual string PaymentRequired { get; set; } = "Vui lòng thanh toán phí gửi xe!";
        public virtual string ChooseLaneRequired { get; set; } = "Vui lòng chọn làn hoạt động";
        public virtual string ChooseCameraRequired { get; set; } = "Vui lòng chọn camera";
        public virtual string ChooseVehicleEntry { get; set; } = "Vui lòng xác nhận phương tiện vào bãi";
        public virtual string ChooseVehicleExit { get; set; } = "Vui lòng xác nhận phương tiện ra khỏi bãi";
        public virtual string TakeCardRequired { get; set; } = "Thẻ đã được đưa ra, vui lòng lấy thẻ";
        public virtual string WaitForSecurityConfirm { get; set; } = "Vui lòng chờ nhân viên trực xác nhận";
        public virtual string ChoosePaymentMethodRequired { get; set; } = "Vui lòng chọn hình thức thanh toán";
        public virtual string InputPasswordRequired { get; set; } = "Vui lòng nhập mật khẩu để thực hiện chức năng này";
        public virtual string CreateQRViewRequired { get; set; } = "Hãy tạo mã QR thanh toán trước!";
        public virtual string ConfirmApplyVoucher { get; set; } = "Bạn có xác nhận sử dụng voucher?";
        public virtual string WaitAMoment { get; set; } = "Vui lòng chờ trong giây lát!";
        public virtual string ConfirmPlateRequired { get; set; } = "Xác nhận biển số!";

        //Loading
        public virtual string TransationCreating { get; set; } = "Đang tạo giao dịch";
        public virtual string LoadDeviceConfig { get; set; } = "Tải thông tin thiết bị";
        public virtual string LoadAccessKeyCollection { get; set; } = "Tải thông tin nhóm định danh";
        public virtual string ConnectToController { get; set; } = "Kết nối đến thiết bị";
        public virtual string InitLprEngine { get; set; } = "Khởi tạo LPR Engine";
        public virtual string InitView { get; set; } = "Khởi tạo giao diện";
        public virtual string InitTimer { get; set; } = "Khởi chạy bộ đếm thời gian thực";

        //Kiosk
        public virtual string KioskDailyCard { get; set; } = "Vé xe lượt"; //DailyCard
        public virtual string KioskMonthlyCard { get; set; } = "Vé xe tháng"; //MonthlyCard

        public virtual string KioskInDashboardTitle { get; set; } = "KIOSK KIỂM SOÁT LỐI VÀO";
        public virtual string KioskInDashboardSubTitle { get; set; } = "";
        public virtual string KioskInHaveAGoodDay { get; set; } = "Chúc quý khách có một ngày tốt lành";

        public virtual string KioskOutDashboardTitle { get; set; } = "KIOSK KIỂM SOÁT LỐI RA";
        public virtual string KioskOutDashboardSubTitle { get; set; } = "";
        public virtual string KioskOutValidEvent { get; set; } = "Quý khách đã hoàn tất thanh toán phí gửi xe";
        public virtual string KIOSK_IN_DAILY_CARD_SUBTITLE { get; set; } = "<center><span style = \"font-size: 24px;>Vui lòng bấm vào nút lấy thẻ<br/><strong style=\"color:rgb(242, 102, 51)\"> phía bên dưới</strong></span>";
        public virtual string KIOSK_OUT_DAILY_CARD_SUBTITLE { get; set; } = "<center><span style = \"font-size: 24px;>Vui lòng đưa thẻ vào khe trả thẻ<br/><strong style=\"color:rgb(242, 102, 51)\"> phía bên dưới</strong></span>";
        public virtual string KIOSK_OUT_MONTH_CARD_SUBTITLE { get; set; } = "<center><span style=\"font-size: 24px;>Vui lòng <strong style=\"color:rgb(242, 102, 51)\">Quẹt thẻ</strong> vào ô thẻ tháng</span><br/>hoặc nhấn nút<strong style=\"color:rgb(242, 102, 51)\"> Chụp biển số</strong>";
        public virtual string KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE { get; set; } = "<center><span style=\"font-size: 24px;>Vui lòng <strong style=\"color:rgb(242, 102, 51)\">Quẹt thẻ</strong> vào ô thẻ tháng";

        //OTHER
        public virtual string WaitConfirmOpenBarrie { get; set; } = "Xác nhận mở barrie";
        public virtual string SecurityNotConfirmOpenBarrie { get; set; } = "Nhân viên trực không xác nhận mở barrie";

        //

        public virtual string RevenueByAccessKeyCollection { get; set; } = "Theo nhóm định danh";
        public virtual string RevenueByLane { get; set; } = "Theo làn";
        public virtual string RevenueByUser { get; set; } = "Theo người dùng";

        public virtual string VerticalLeftToRight { get; set; } = "Dọc từ trái sang phải";
        public virtual string VerticalRightToLeft { get; set; } = "Dọc từ phải sang trái";
        public virtual string HorizontalLeftToRight { get; set; } = "Ngang từ trái sang phải";
        public virtual string HorizontalRightToLeft { get; set; } = "Ngang từ phải sang trái";

        public virtual string Vertical { get; set; } = "Dọc";
        public virtual string Horizontal { get; set; } = "Ngang";

        public virtual string DisplayDirection { get; set; } = "Chiều hiển thị";
        public virtual string CameraRegion { get; set; } = "Khu vực camera";
        public virtual string PicRegion { get; set; } = "Khu vực hình ảnh";
        public virtual string CameraPicRegion { get; set; } = "Khu vực camera - hình ảnh";
        public virtual string LprEventRegion { get; set; } = "Khu vực sự kiện";
        public virtual string EventRegion { get; set; } = "Loại giao diện";

        public virtual string AllowOpenBarrieManual { get; set; } = "Cho phép mở barrie thủ công";
        public virtual string AllowWriteTicketManual { get; set; } = "Cho phép ghi vé thủ công";
        public virtual string AllowRetakeImageManual { get; set; } = "Cho phép chụp lại thủ công";
        public virtual string DisplayTitle { get; set; } = "Hiển thị tiêu đề";


        public virtual string RegisterByPlateDaily { get; set; } = "Cho phép xe lượt vào bằng biển số";

        public virtual string WarningDialog { get; set; } = "Hộp thoại cảnh báo";
        public virtual string AutoCloseWarningAfter { get; set; } = "Tự động đóng cảnh báo sao";
        public virtual string AutoCloseResult { get; set; } = "Kết quả tự động đóng";
        public virtual string Use { get; set; } = "Sử dụng";

        public virtual string QRView { get; set; } = "QR View";
        public virtual string ComIp { get; set; } = "COM / IP";
        public virtual string BaudratePort { get; set; } = "Baudrate / Port";
        public virtual string AccountNumber { get; set; } = "Số tài khoản";
        public virtual string Bank { get; set; } = "Ngân hàng";

        public virtual string Other { get; set; } = "Khác";
        public virtual string AllowUseLoopImageForCardEvent { get; set; } = "Sử dụng hình ảnh loop gần nhất cho sự kiện quẹt thẻ";

        public virtual string ConnectingWithDevice { get; set; } = "Đang kết nối tới thiết bị";
        public virtual string DisconnectingWithDevice { get; set; } = "Đang ngắt kết nối tới thiết bị";
        public virtual string PaymentMethodNotActive { get; set; } = "Hình thức thanh toán chưa được kích hoạt";
        public virtual string ChooseOtherPaymentMethodRequired { get; set; } = "Vui lòng chọn hình thức thanh toán khác";

    }

    public static class UIBuiltInResourcesHelper
    {
        /// <summary>
        /// Lấy giá trị property theo tên
        /// </summary>
        public static string GetValue(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return string.Empty;
            }
            var prop = KZUIStyles.CurrentResources.GetType().GetProperty(propertyName);
            if (prop != null)
            {
                var value = prop.GetValue(KZUIStyles.CurrentResources);
                return value?.ToString() ?? string.Empty;
            }
            return propertyName;
        }
    }
}
