using System.Globalization;

namespace iParkingv8.Ultility.dictionary
{
    public class lo_LA_KZResources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => new CultureInfo("lo-LA");

        // ===== Menu / Header =====
        public override string EventInfo { get; set; } = "ຂໍ້ມູນເຫດການ";

        public override string MiSystem { get; set; } = "ລະບົບ";
        public override string MiChangePassword { get; set; } = "ປ່ຽນລະຫັດຜ່ານ";
        public override string MiLogOut { get; set; } = "ອອກຈາກລະບົບ";
        public override string MiExit { get; set; } = "ອອກ";

        public override string MiReport { get; set; } = "ລາຍງານ";
        public override string MiReportIn { get; set; } = "ລົດກຳລັງຈອດ";
        public override string MiReportOut { get; set; } = "ລົດອອກແລ້ວ";
        public override string MiReportRevenue { get; set; } = "ລາຍງານລາຍຮັບ";
        public override string MiReportHandOver { get; set; } = "ລາຍງານປິດກະ";

        public override string MiData { get; set; } = "ຂໍ້ມູນ";
        public override string MiAccessKeyList { get; set; } = "ລາຍການກະແຈເຂົ້າ/ບັດ";
        public override string MiCustomerList { get; set; } = "ລາຍການລູກຄ້າ";
        public override string MiVehicleList { get; set; } = "ລາຍການພາຫະນະ";

        public override string LicenseValid { get; set; } = "ອາຍຸໃຊ້";
        public override string Day { get; set; } = "ວັນ";
        public override string Quantity { get; set; } = "ຈຳນວນ";
        public override string Printer { get; set; } = "ເຄື່ອງພິມ";

        // ===== Titles =====
        public override string Name { get; set; } = "ຊື່";

        public override string InfoTitle { get; set; } = "ຂໍ້ມູນ";
        public override string SuccessTitle { get; set; } = "ສຳເລັດ";
        public override string WarningTitle { get; set; } = "ຄຳເຕືອນ";
        public override string ErrorTitle { get; set; } = "ຂໍ້ຜິດພາດ";
        public override string AskTitle { get; set; } = "ຄຳຖາມ";
        public override string InputTitle { get; set; } = "ປ້ອນຂໍ້ມູນ";
        public override string SelectTitle { get; set; } = "ເລືອກຂໍ້ມູນ";
        public override string CloseAll { get; set; } = "ປິດທັງໝົດ";

        // ===== Common Buttons =====
        public override string Confirm { get; set; } = "ຢືນຢັນ";
        public override string Cancel { get; set; } = "ປິດ";
        public override string Back { get; set; } = "ກັບຄືນ";
        public override string BackToMain { get; set; } = "ກັບໜ້າຫຼັກ";
        public override string Clear { get; set; } = "ລຶບ";

        // ===== Process =====
        public override string ProcessChecking { get; set; } = "ກຳລັງກວດສອບ";
        public override string ProcessReadingPlate { get; set; } = "ກຳລັງຈຳແນກປ້າຍ";
        public override string ProcessCheckIn { get; set; } = "ກຳລັງເຂົ້າ";
        public override string ProcessCheckOut { get; set; } = "ກຳລັງອອກ";
        public override string ProcessInvalidDailyVehicleIn { get; set; } = "ປ້າຍບໍ່ຖືກຕ້ອງ! ໃຫ້ເຂົ້າບໍ?";
        public override string ProcessConfirmOpenbarrie { get; set; } = "ຢືນຢັນເປີດດ່ານບໍ?";
        public override string ProcessConfirmRequest { get; set; } = "ຄຳຮ້ອງຂໍຢືນຢັນ";
        public override string ProcessOpenBarrie { get; set; } = "ກຳລັງເປີດດ່ານ";
        public override string ProcessSaveImage { get; set; } = "ບັນທຶກຮູບພາບ";

        public override string ProccesNotConfirmVehicleEntry { get; set; } = "ບໍ່ຢືນຢັນໃຫ້ລົດເຂົ້າບໍ";
        public override string ProccesNotConfirmVehicleExit { get; set; } = "ບໍ່ຢືນຢັນໃຫ້ລົດອອກບໍ";
        public override string ProcessGetEntryInfor { get; set; } = "ອ່ານຂໍ້ມູນລົດເຂົ້າ";
        public override string ProcessConfirmCloseApp { get; set; } = "ຢືນຢັນປິດໂປຣແກຣມບໍ?";

        public override string StartTime { get; set; } = "ເວລາເລີ່ມ";
        public override string EndTime { get; set; } = "ເວລາສິ້ນສຸດ";

        public override string CheckFee { get; set; } = "ກວດສອບຄ່າທຳນຽມ";
        public override string ExportExcel { get; set; } = "ສົ່ງອອກ Excel";

        // ===== Common Texts =====
        public override string Open { get; set; } = "ເປີດ";
        public override string Save { get; set; } = "ບັນທຶກ";
        public override string SaveConfigSuccess { get; set; } = "ບັນທຶກການຕັ້ງຄ່າສຳເລັດ";
        public override string ClearConfigSuccess { get; set; } = "ລຶບການຕັ້ງຄ່າສຳເລັດ";
        public override string Config { get; set; } = "ການຕັ້ງຄ່າລະບົບ";

        public override string All { get; set; } = "ທັງໝົດ";
        public override string Welcome { get; set; } = "ຍິນດີຕ້ອນຮັບ";
        public override string SeeYouAgain { get; set; } = "ພົບກັນໃໝ່";
        public override string TakeMoney { get; set; } = "ເກັບເງິນ";
        public override string Keyword { get; set; } = "ຄຳຄົ້ນຫາ";
        public override string Status { get; set; } = "ສະຖານະ";
        public override string TitlePicMerge { get; set; } = "ຮູບເຂົ້າ & ອອກ";
        public override string TitlePicIn { get; set; } = "ຮູບເຂົ້າ";
        public override string TitlePanoramaIn { get; set; } = "ພານໂນຣາມາເຂົ້າ";
        public override string TitleVehicleIn { get; set; } = "ປ້າຍເຂົ້າ";

        public override string TitlePicOut { get; set; } = "ຮູບອອກ";
        public override string TitlePanoramaOut { get; set; } = "ພານໂນຣາມາອອກ";
        public override string TitleVehicleOut { get; set; } = "ປ້າຍອອກ";

        // ===== Forms =====
        public override string FrmLogin { get; set; } = "ເຂົ້າລະບົບ";
        public override string FrmLoading { get; set; } = "ກຳລັງໂຫຼດການຕັ້ງຄ່າລະບົບ";
        public override string FrmSelectLane { get; set; } = "ເລືອກເສັ້ນທາງທຳງານ";
        public override string FrmChangePassword { get; set; } = "ປ່ຽນລະຫັດຜ່ານ";
        public override string FrmVerifyPassword { get; set; } = "ຢືນຢັນລະຫັດຜ່ານ";
        public override string FrmTestController { get; set; } = "ທົດສອບອຸປະກອນ";
        public override string FrmTestLed { get; set; } = "ທົດສອບປ້າຍ LED";
        public override string FrmSelectVehicle { get; set; } = "ຢືນຢັນພາຫະນະ";
        public override string FrmSelectAccessKeyCollection { get; set; } = "ປ່ຽນກຸ່ມບັດ/ກະແຈ";
        public override string FrmAccessKeyList { get; set; } = "ລາຍການບັດ/ກະແຈ";
        public override string FrmCustomerList { get; set; } = "ລາຍການລູກຄ້າ";
        public override string FrmVehicleList { get; set; } = "ລາຍການພາຫະນະ";
        public override string FrmEditPlate { get; set; } = "ປັບປຸງຂໍ້ມູນປ້າຍທະບຽນລົດ";

        // ===== OBJECTS - USER =====
        public override string User { get; set; } = "ຜູ້ໃຊ້";
        public override string UserIn { get; set; } = "ຜູ້ຄວບຄຸມເຂົ້າ";
        public override string UserOut { get; set; } = "ຜູ້ຄວບຄຸມອອກ";

        public override string Username { get; set; } = "ຊື່ຜູ້ໃຊ້";
        public override string Password { get; set; } = "ລະຫັດຜ່ານ";
        public override string CurrentPassword { get; set; } = "ລະຫັດປະຈຸບັນ";
        public override string NewPassword { get; set; } = "ລະຫັດໃໝ່";
        public override string ConfirmPassword { get; set; } = "ຢືນຢັນລະຫັດໃໝ່";

        // ===== OBJECTS - ACCESS KEY =====
        public override string AccesskeyList { get; set; } = "ລາຍການບັດ/ກະແຈ";
        public override string AccesskeyName { get; set; } = "ບັດເຂົ້າ";
        public override string AccesskeyCode { get; set; } = "ລະຫັດ";
        public override string AccesskeyNote { get; set; } = "ໝາຍເຫດ";

        // Trạng thái Access Key
        public override string AccesskeyStatusLocked { get; set; } = "ລັອກ";
        public override string AccesskeyStatusNotUsed { get; set; } = "ຍັງບໍ່ໄດ້ໃຊ້";
        public override string AccesskeyStatusInUsed { get; set; } = "ກຳລັງໃຊ້ງານ";

        // Loại Access Key
        public override string AccesskeyTypeVehicle { get; set; } = "ພາຫະນະ";
        public override string AccesskeyTypeCard { get; set; } = "ບັດ";
        public override string AccesskeyTypeQR { get; set; } = "ລະຫັດ QR";
        public override string AccesskeyTypeFinger { get; set; } = "ລາຍນິ້ວມື";
        public override string AccesskeyTypeFace { get; set; } = "ໃບໜ້າ";

        // ===== OBJECTS - LANE =====
        public override string Lane { get; set; } = "ເສັ້ນທາງ";
        public override string LaneIn { get; set; } = "ຊ່ອງເຂົ້າ";
        public override string LaneOut { get; set; } = "ຊ່ອງອອກ";

        #region OBJECTS - INVOICE
        public override string InvoiceTemplate { get; set; } = "ແບບຟອມໃບບິນ";
        public override string InvoiceNo { get; set; } = "ເລກໃບບິນ";
        #endregion

        // ===== OBJECTS - VEHICLE =====
        public override string Vehicles { get; set; } = "ຍານພາຫະນະ";

        public override string VehicleList { get; set; } = "ລາຍການພາຫະນະ";
        public override string VehicleName { get; set; } = "ຊື່ພາຫະນະ";
        public override string VehicleCode { get; set; } = "ປ້າຍທະບຽນ";
        public override string VehicleType { get; set; } = "ປະເພດພາຫະນະ";
        public override string VehicleCodeAcronym { get; set; } = "ທະບຽນ";
        public override string VehicleExpiredDate { get; set; } = "ວັນໝົດອາຍຸ";

        public override string VehicleTypeCar { get; set; } = "ລົດຍົນ";     // Ô tô
        public override string VehicleTypeMotor { get; set; } = "ລົດຈັກ";    // Xe máy
        public override string VehicleTypeBike { get; set; } = "ລົດຖີບ";     // Xe đạp

        public override string CurrentPlate { get; set; } = "ປ້າຍທະບຽນປັດຈຸບັນ";
        public override string NewPlate { get; set; } = "ປ້າຍທະບຽນໃໝ່";

        // ===== OBJECTS - CUSTOMER =====
        public override string CustomerList { get; set; } = "ລາຍການລູກຄ້າ";
        public override string CustomerName { get; set; } = "ຊື່ລູກຄ້າ";
        public override string CustomerCode { get; set; } = "ລະຫັດລູກຄ້າ";

        // ===== OBJECTS - COLLECTION =====
        public override string AccessKeyCollectionList { get; set; } = "ລາຍການກຸ່ມບັດ";
        public override string AccessKeyCollection { get; set; } = "ກຸ່ມບັດ";
        public override string CustomerCollection { get; set; } = "ກຸ່ມລູກຄ້າ";

        public override string CollectionVIP { get; set; } = "ລົດ VIP";        // Xe VIP
        public override string CollectionMonth { get; set; } = "ລົດເດືອນ";    // Xe tháng
        public override string CollectionDaily { get; set; } = "ລົດຕໍ່ເທື່ອ"; // Xe lượt


        // ===== OBJECTS - VOUCHER =====
        public override string VoucherList { get; set; } = "ລາຍການ Voucher";
        public override string VoucherName { get; set; } = "Voucher";

        // ===== OBJECTS - TRANSACTION =====
        public override string TransactionId { get; set; } = "ລະຫັດທຸລະກຳ";

        // ===== OBJECTS - EVENT =====
        public override string CameraIn { get; set; } = "ກ້ອງທາງເຂົ້າ";
        public override string CameraOut { get; set; } = "ກ້ອງທາງອອກ";

        public override string Plate { get; set; } = "ປ້າຍທະບຽນລົດ";
        public override string PlateIn { get; set; } = "ປ້າຍເຂົ້າ";
        public override string PlateOut { get; set; } = "ປ້າຍອອກ";
        public override string PlateDetected { get; set; } = "ປ້າຍທີ່ກວດພົບ";
        public override string PlateDetectedAcronym { get; set; } = "ກວດພົບ";

        public override string TimeIn { get; set; } = "ເວລາເຂົ້າ";
        public override string TimeOut { get; set; } = "ເວລາອອກ";
        public override string Duration { get; set; } = "ໄລຍະເວລາຈອດ";

        public override string PicPanoramaIn { get; set; } = "ຮູບພານໂນຣາມາເຂົ້າ";
        public override string PicVehicleIn { get; set; } = "ຮູບລົດເຂົ້າ";
        public override string PicPlateIn { get; set; } = "ຮູບປ້າຍເຂົ້າ";

        public override string PicPanoramaOut { get; set; } = "ຮູບພານໂນຣາມາອອກ";
        public override string PicVehicleOut { get; set; } = "ຮູບລົດອອກ";
        public override string PicPlateOut { get; set; } = "ຮູບປ້າຍອອກ";

        public override string PaymentInfo { get; set; } = "ຂໍ້ມູນການຊຳລະ";
        public override string PaymentSupport { get; set; } = "ຮອງຮັບວິທີການຊຳລະ";

        public override string EditNoteInfor { get; set; } = "ແກ້ໄຂບັນທຶກ";
        public override string EditPlateInfo { get; set; } = "ແກ້ໄຂປ້າຍທະບຽນລົດ";

        public override string Fee { get; set; } = "ຄ່າຈອດລົດ";
        public override string Paid { get; set; } = "ຈ່າຍແລ້ວ";

        public override string Discount { get; set; } = "ສ່ວນຫຼຸດ";
        public override string RealFee { get; set; } = "ຄ່າທຳນຽມຈິງ";

        public override string Remain { get; set; } = "ຍັງເຫຼືອ";
        public override string VoucherInUse { get; set; } = "Voucher ໃຊ້ແລ້ວ";

        // ===== OBJECTS - DEVICE =====
        public override string Camera { get; set; } = "ກ້ອງ";
        public override string Angle { get; set; } = "ມຸມກ້ອງ";
        public override string Device { get; set; } = "ອຸປະກອນ";
        public override string Connected { get; set; } = "ເຊື່ອມຕໍ່ແລ້ວ";
        public override string Disconnected { get; set; } = "ຂາດການເຊື່ອມຕໍ່";
        public override string StopMode { get; set; } = "ຢຸດການທຳງານ";
        public override string CardOutputFormat { get; set; } = "ຮູບແບບ";
        public override string CardOutputAdditional { get; set; } = "ເພີ່ມເຕີມ";
        public override string FormatToiGian { get; set; } = "ງ່າຍທີ່ສຸດ";
        public override string FormatToiThieu8KyTu { get; set; } = "ຢ່າງນ້ອຍ 8 ຕົວອັກສອນ";
        public override string FormatToiThieu10KyTu { get; set; } = "ຢ່າງນ້ອຍ 10 ຕົວອັກສອນ";


        // ===== OBJECTS - LED / Shortcut / Display / Options / Controller =====
        public override string Led { get; set; } = "LED";
        public override string Shortcut { get; set; } = "ປຸ່ມລັດ";
        public override string DisplaySetting { get; set; } = "ການສະແດງຜົນ";
        public override string OptionSetting { get; set; } = "ຕົວເລືອກ";
        public override string ControllerSetting { get; set; } = "ຕົວຄວບຄຸມ";

        // ===== TIMER =====
        public override string AutoConfirmAfter { get; set; } = "ຢືນຢັນອັດຕະໂນມັດຫຼັງຈາກ";
        public override string AutoCancelAfter { get; set; } = "ປິດອັດຕະໂນມັດຫຼັງຈາກ";
        public override string AutoLoginAfter { get; set; } = "ເຂົ້າລະບົບອັດຕະໂນມັດຫຼັງຈາກ";
        public override string AutoOpenHomePage { get; set; } = "ເປີດໜ້າຈໍແອັບຯ ອັດຕະໂນມັດຫຼັງຈາກ";

        // ===== Payment =====
        public override string PaymentQrTitle { get; set; } = "ສະແກນ QR ເພື່ອຊຳລະ";
        public override string PaymentVisaTitle { get; set; } = "ຮູດບັດເພື່ອຊຳລະ";
        public override string PaymentVisaSubTitle { get; set; } = "ກະລຸນາກວດກາທຸລະກຳ ແລະ ຮູດບັດທີ່ອຸປະກອນຊຳລະ";

        public override string PaymentVoucherTitle { get; set; } = "ສະແກນລະຫັດ Voucher ທີ່ຈຸດສະແກນສ່ວນຫຼຸດ";
        public override string PaymentVoucherSubTitle { get; set; } = "ເຄື່ອງນີ້ບໍ່ທອນເງິນທອນ";

        public override string PaymentCashTitle { get; set; } = "ໃສ່ເງິນເປັນໃບໆ ລົງໃນຊ່ອງຮັບເງິນ";
        public override string PaymentCashSubTitle { get; set; } = "ເຄື່ອງນີ້ບໍ່ທອນເງິນທອນ";

        // ===== Vehicle type =====
        public override string DailyVehicle { get; set; } = "ລົດລາຍວັນ";
        public override string MonthVehicle { get; set; } = "ລົດລາຍເດືອນ";

        // ===== Buttons =====
        public override string RetakeImage { get; set; } = "ຖ່າຍປ້າຍ";
        public override string Setting { get; set; } = "ຕັ້ງຄ່າ";
        public override string WriteIn { get; set; } = "ອອກບັດເຂົ້າ";
        public override string WriteOut { get; set; } = "ອອກບັດອອກ";
        public override string Print { get; set; } = "ພິມບັດ";
        public override string Search { get; set; } = "ຄົ້ນຫາ";
        public override string Register { get; set; } = "Register";

        public override string Liveview { get; set; } = "ຮູບສົດ";
        public override string CarLprDetect { get; set; } = "ຈຳແນກປ້າຍລົດຍົນ";
        public override string MotorLprDetect { get; set; } = "ຈຳແນກປ້າຍລົດຈັກ";
        public override string DrawLPR { get; set; } = "ກຳນົດເຂດປ້າຍ";
        public override string DrawMotion { get; set; } = "ກຳນົດເຂດເຄື່ອນໄຫວ";
        public override string OpenBarrie { get; set; } = "ເປີດດ່ານ";
        public override string CollectCard { get; set; } = "ເກັບບັດ";
        public override string RejectCard { get; set; } = "ຄາຍບັດ";
        public override string ChangCollection { get; set; } = "ປ່ຽນປະເພດບັດ";
        public override string PayParkingFee { get; set; } = "ຈ່າຍຄ່າຈອດລົດ";
        public override string Cash { get; set; } = "ເງິນສົດ";
        public override string Voucher { get; set; } = "Voucher";
        public override string QR { get; set; } = "QR";
        public override string VISA { get; set; } = "ບັດທະນາຄານ";
        public override string ShortCutSelectGuide { get; set; } = "Enter ເພື່ອຄົ້ນຫາ.\r\nDouble-click ຫຼື ກົດ ຢືນຢັນ ເພື່ອເລືອກ.";
        public override string ShortCutGuide2Line { get; set; } = "Enter: ຢືນຢັນ\r\nEsc: ປິດ";
        public override string ShortCutGuide1Line { get; set; } = "Enter: ຢືນຢັນ | ESC: ປິດ";
        public override string CreateQRView { get; set; } = "ສ້າງ QR";
        public override string ReCreateQRView { get; set; } = "ສ້າງ QR ໃຫມ່";

        // ===== CHECKBOX =====
        public override string RememberPassword { get; set; } = "ເຂົ້າລະບົບອັດຕະໂນມັດ";
        public override string SelectAll { get; set; } = "ເລືອກທັງໝົດ";

        // ===== REPORT =====
        public override string Total { get; set; } = "ລວມ: ";
        public override string EventList { get; set; } = "ລາຍການເຫດການ";
        public override string ColTime { get; set; } = "ເວລາ";
        public override string ColEventType { get; set; } = "ເຫດການ";
        public override string ColCardOrLoop { get; set; } = "ຜູ້ອ່ານ | ລູບ";
        public override string ColCardNumber { get; set; } = "ເລກບັດ";

        public override string colAccessKeyKeyword { get; set; } = "ຊື່ | ລະຫັດ | ໝາຍເຫດ";
        public override string colAccessKeyName { get; set; } = "ຊື່";
        public override string colAccessKeyCode { get; set; } = "ລະຫັດ";
        public override string colType { get; set; } = "ປະເພດ";
        public override string colStatus { get; set; } = "ສະຖານະ";
        public override string colNote { get; set; } = "ໝາຍເຫດ";
        public override string colCollection { get; set; } = "ກຸ່ມ";

        public override string colCustomerKeyword { get; set; } = "ຊື່ | ລະຫັດ";
        public override string colCustomerName { get; set; } = "ຊື່";
        public override string colCustomerCode { get; set; } = "ລະຫັດ";
        public override string colCustomerPhone { get; set; } = "ເບີໂທ";
        public override string colCustomerCollection { get; set; } = "ກຸ່ມ";
        public override string colCustomerAddress { get; set; } = "ທີ່ຢູ່";

        public override string colVehicleName { get; set; } = "ຊື່";
        public override string colVehicleCode { get; set; } = "ປ້າຍ";
        public override string colVehicleAccessKey { get; set; } = "Access Key";
        public override string colVehicleStatus { get; set; } = "ສະຖານະ";
        public override string colVehicleType { get; set; } = "ປະເພດ";
        public override string colVehicleCustomerName { get; set; } = "ລູກຄ້າ";
        public override string colVehicleCustomerCollection { get; set; } = "ກຸ່ມລູກຄ້າ";
        public override string colVehicleCustomerPhone { get; set; } = "ເບີໂທ";
        public override string colVehicleCustomerAddress { get; set; } = "ທີ່ຢູ່";
        public override string colVehicleExpiredDate { get; set; } = "ວັນໝົດອາຍຸ";
        public override string colVehicleCollection { get; set; } = "ກຸ່ມ";

        // ===== Errors - Common =====
        public override string AccountInvalidPermission { get; set; } = "ບັນຊີຂອງທ່ານບໍ່ມີສິດເຮັດວຽກນີ້!";

        public override string ServerConfigInvalid { get; set; } = "ບໍ່ພົບການຕັ້ງຄ່າເຊີບເວີ ຫຼື ບໍ່ຖືກຕ້ອງ!";
        public override string ActiveLicenseError { get; set; } = "ເປີດໃຊ້ບໍ່ສຳເລັດ!";
        public override string InvalidEventInfo { get; set; } = "ບໍ່ມີຂໍ້ມູນເຫດການ!";
        public override string InvalidPrintTemplate { get; set; } = "ບໍ່ພົບແບບຟອມພິມ!";
        public override string SystemError { get; set; } = "ເກີດຂໍ້ຜິດພາດໃນຂະບວນການ!";
        public override string ServerDisconnected { get; set; } = "ຂາດການເຊື່ອມຕໍ່ກັບເຊີບເວີ!";
        public override string TryAgain { get; set; } = "ກະລຸນາລອງໃໝ່!";
        public override string TryAgainLater { get; set; } = "ກະລຸນາລອງໃໝ່ພາຍຫຼັງ!";
        public override string UnreturnedAccessKey { get; set; } = "ຄ້າງບັດ";
        public override string InEntryWaitingTime { get; set; } = "ໃນເວລາລໍຖ້າເຂົ້າ";
        public override string InExitWaitingTime { get; set; } = "ໃນເວລາລໍຖ້າອອກ";
        public override string GetCameraImageError { get; set; } = "ເກີດຂໍ້ຜິດພາດໃນຂະນະດຶງຮູບພາບຈາກກ້ອງ";

        // ===== Login Errors =====
        public override string InvalidLogin { get; set; } = "ເຂົ້າລະບົບບໍ່ສຳເລັດ";
        public override string InvalidPassword { get; set; } = "ລະຫັດຜ່ານບໍ່ຖືກຕ້ອງ!";

        // ===== Device Config / Reader =====
        public override string InvalidDeviceConfig { get; set; } = "ບໍ່ມີການຕັ້ງຄ່າອຸປະກອນ!";
        public override string InvalidReserverLaneConfig { get; set; } = "ບໍ່ມີການຕັ້ງຄ່າເສັ້ນທາງສະຫຼັບ";

        public override string InvalidReaderConfig { get; set; } = "ຫົວອ່ານບັດຍັງບໍ່ໄດ້ລົງທະບຽນໃນລະບົບ";

        public override string InvalidReaderDailyTitle { get; set; } = "ທ່ານກຳລັງໃຊ້ບັດລາຍຄັ້ງ!";
        public override string InvalidReaderDailySubTitle { get; set; } = "ກະລຸນາໃສ່ບັດໃສ່ຊ່ອງບັດລາຍຄັ້ງ";

        public override string InvalidReaderMonthlyTitle { get; set; } = "ທ່ານກຳລັງໃຊ້ບັດລາຍເດືອນ!";
        public override string InvalidReaderMonthlySubTitle { get; set; } = "ກະລຸນາຮູດບັດທີ່ຫົວອ່ານບັດລາຍເດືອນ";

        // ===== Access Key Errors =====
        public override string InvalidPermission { get; set; } = "ສິດທິບໍ່ຖືກຕ້ອງ!";
        public override string AccessKeyNotFound { get; set; } = "ບັດຂອງທ່ານຍັງບໍ່ໄດ້ລົງທະບຽນ!";
        public override string AccessKeyNotSupport { get; set; } = "ບັດນີ້ບໍ່ຮອງຮັບ, ກະລຸນາກວດຄືນ!";
        public override string AccessKeyInWaitingTime { get; set; } = "ຮູດບັດໄວເກີນໄປ, ກະລຸນາລອງໃໝ່ໃນໄມ່ຊ້ານີ້!";
        public override string AccessKeyLocked { get; set; } = "ບັດຂອງທ່ານຍັງບໍ່ໄດ້ເປີດໃຊ້!";
        public override string AccessKeyExpired { get; set; } = "ພາຫະນະໝົດອາຍຸ!";
        public override string AccessKeyMonthNoVehicle { get; set; } = "ບັດລາຍເດືອນບໍ່ຜູກກັບພາຫະນະ!";
        public override string AccessKeyVipNoVehicle { get; set; } = "ບັດ VIP ບໍ່ຜູກກັບພາຫະນະ!";
        public override string BlackedList { get; set; } = "ປ້າຍໃນບັນຊີດຳ!";

        // ຄວາມຜິດພາດກ່ຽວກັບກຸ່ມການກຳນົດ
        public override string CollectionLocked { get; set; } = "ກຸ່ມການກຳນົດຍັງບໍ່ຖືກເປີດໃຊ້!";


        // ===== Vehicle Errors =====
        public override string VehicleNotFound { get; set; } = "ພາຫະນະບໍ່ຢູ່ໃນລະບົບ!";
        public override string VehicleLocked { get; set; } = "ພາຫະນະຖືກລັອກ!";
        public override string VehicleNotAllowEntryByPlate { get; set; } = "ບໍ່ອະນຸຍາດເຂົ້າດ້ວຍປ້າຍ!";
        public override string VehicleNotAllowExityByPlate { get; set; } = "ບໍ່ອະນຸຍາດອອກດ້ວຍປ້າຍ!";

        // ===== Entry Errors =====
        public override string EntryNotFound { get; set; } = "ລົດຍັງບໍ່ເຂົ້າລານ!";
        public override string EntryDupplicated { get; set; } = "ລົດເຂົ້າລານແລ້ວ!";

        // ===== Plate Errors =====
        public override string InvalidPlateNumber { get; set; } = "ບໍ່ສາມາດຈຳແນກເລກປ້າຍ!";
        public override string PlateInOutNotSame { get; set; } = "ປ້າຍເຂົ້າ-ອອກບໍ່ກົງກັນ!";
        public override string PlateNotMatchWithSystem { get; set; } = "ປ້າຍບໍ່ກົງກັບທີ່ລົງທະບຽນ!";

        // ===== Device Connection =====
        public override string DeviceDisconnected { get; set; } = "ບໍ່ສາມາດເຊື່ອມຕໍ່ອຸປະກອນ!";

        // ===== Voucher =====
        public override string VoucherInvalidType { get; set; } = "ປະເພດ Voucher ບໍ່ຖືກຕ້ອງ!";

        public override string VoucherNotFound { get; set; } = "Voucher ຍັງບໍ່ໄດ້ລົງທະບຽນໃນລະບົບ!";
        public override string VoucherNotActived { get; set; } = "Voucher ຍັງບໍ່ໄດ້ຖືກເປີດໃຊ້!";
        public override string VoucherExpired { get; set; } = "Voucher ໝົດອາຍຸແລ້ວ!";
        public override string VoucherInvalidTime { get; set; } = "Voucher ບໍ່ອະນຸຍາດໃຫ້ໃຊ້ໃນເວລານີ້!";

        public override string VoucherlistEmpty { get; set; } = "ບໍ່ມີ Voucher ທີ່ໃຊ້ໄດ້!";
        public override string VoucherApplyError { get; set; } = "ນຳໃຊ້ Voucher ບໍ່ສຳເລັດ, ກະລຸນາລອງໃໝ່!";
        public override string VoucherInUsedError { get; set; } = "Voucher ໄດ້ໃຊ້ແລ້ວ!";

        // ===== Change Collection =====
        public override string ChangeCollectionError { get; set; } = "ປ່ຽນກຸ່ມບັດບໍ່ສຳເລັດ, ກະລຸນາລອງໃໝ່!";

        // ===== Transaction =====
        public override string TransactionCreateError { get; set; } = "ບໍ່ສາມາດສ້າງທຸລະກຳ!";

        // ===== Customer Actions =====
        public override string ActiveLicenseRequired { get; set; } = "ແອັບຯຍັງບໍ່ໄດ້ເປີດໃຊ້! ຕ້ອງການເປີດໃຊ້ບໍ?";
        public override string CustomerCommandOpenBarrie { get; set; } = "ຄຳສັ່ງເປີດດ່ານ";
        public override string CustomerCommandWriteIn { get; set; } = "ຄຳສັ່ງອອກບັດເຂົ້າ";
        public override string CustomerCommandWriteOut { get; set; } = "ຄຳສັ່ງອອກບັດອອກ";
        public override string CustomerCommandCapture { get; set; } = "ຄຳສັ່ງຖ່າຍຮູບໃໝ່";
        public override string CustomerCommandPrintTicket { get; set; } = "ຄຳສັ່ງພິມບັດຈອດ";
        public override string CustomerCommandUpdatePlate { get; set; } = "ຄຳສັ່ງອັບເດດປ້າຍ";
        public override string CustomerCommandReserverLane { get; set; } = "ຄຳສັ່ງສະຫຼັບເສັ້ນທາງ";
        public override string CustomerCommandOpenSettingPage { get; set; } = "ຄຳສັ່ງເປີດໜ້າຕ່າງຕັ້ງຄ່າ";

        // ===== Prompts / Notices =====
        public override string PlateSame { get; set; } = "ປ້າຍກົງກັນ!";
        public override string PaymentRequired { get; set; } = "ກະລຸນາຊຳລະຄ່າຈອດລົດ!";
        public override string ChooseLaneRequired { get; set; } = "ກະລຸນາເລືອກເສັ້ນທາງ";
        public override string ChooseCameraRequired { get; set; } = "ກະລຸນາເລືອກກ້ອງ";
        public override string ChooseVehicleEntry { get; set; } = "ກະລຸນາຢືນຢັນລົດເຂົ້າ";
        public override string ChooseVehicleExit { get; set; } = "ກະລຸນາຢືນຢັນລົດອອກ";
        public override string TakeCardRequired { get; set; } = "ຄາຍບັດແລ້ວ, ກະລຸນາເອົາບັດ";
        public override string WaitForSecurityConfirm { get; set; } = "ກະລຸນາລໍຖ້າພະນັກງານຢືນຢັນ";
        public override string ChoosePaymentMethodRequired { get; set; } = "ກະລຸນາເລືອກວິທີຊຳລະ";
        public override string InputPasswordRequired { get; set; } = "ກະລຸນາໃສ່ລະຫັດຜ່ານເພື່ອດຳເນີນການ";
        public override string CreateQRViewRequired { get; set; } = "ກະລຸນາສ້າງ QR ຊຳລະກ່ອນ!";
        public override string ConfirmApplyVoucher { get; set; } = "ຢືນຢັນໃຊ້ Voucher ຫຼືບໍ?";
        public override string WaitAMoment { get; set; } = "ກະລຸນາລໍຖ້າສັກຄູ່!";
        public override string ConfirmPlateRequired { get; set; } = "ຢືນຢັນປ້າຍທະບຽນ!";

        // ===== Loading =====
        public override string TransationCreating { get; set; } = "ກຳລັງສ້າງທຸລະກຳ";
        public override string LoadDeviceConfig { get; set; } = "ກຳລັງໂຫຼດຂໍ້ມູນອຸປະກອນ!";
        public override string LoadAccessKeyCollection { get; set; } = "ກຳລັງໂຫຼດຂໍ້ມູນກຸ່ມບັດ!";
        public override string ConnectToController { get; set; } = "ກຳລັງເຊື່ອມຕໍ່ອຸປະກອນ!";
        public override string InitLprEngine { get; set; } = "ກຳລັງເລີ່ມຕົ້ນ LPR Engine!";
        public override string InitView { get; set; } = "ເລີ່ມຕົ້ນການສະແດງຜົນ";
        public override string InitTimer { get; set; } = "ເລີ່ມຕົ້ນຕົວນັບເວລາແທ້ຈິງ";

        // ===== Kiosk =====
        public override string KioskDailyCard { get; set; } = "ບັດລາຍຄັ້ງ";
        public override string KioskMonthlyCard { get; set; } = "ບັດລາຍເດືອນ";

        public override string KioskInDashboardTitle { get; set; } = "KIOSK ຄວບຄຸມທາງເຂົ້າ";
        public override string KioskInDashboardSubTitle { get; set; } = "";
        public override string KioskInHaveAGoodDay { get; set; } = "ຂໍໃຫ້ມີວັນທີ່ດີ";

        public override string KioskOutDashboardTitle { get; set; } = "KIOSK ຄວບຄຸມທາງອອກ";
        public override string KioskOutDashboardSubTitle { get; set; } = "";
        public override string KioskOutValidEvent { get; set; } = "ຊຳລະຄ່າຈອດລົດສຳເລັດແລ້ວ";
        public override string KIOSK_IN_DAILY_CARD_SUBTITLE { get; set; } =
            "<center><span style=\"font-size: 24px;\">ກະລຸນາກົດປຸ່ມ ຮັບບັດ<br/><strong style=\"color:rgb(242, 102, 51)\">ດ້ານລຸ່ມນີ້</strong></span>";
        public override string KIOSK_OUT_DAILY_CARD_SUBTITLE { get; set; } =
            "<center><span style=\"font-size: 24px;\">ກະລຸນາໃສ່ບັດໃສ່ຊ່ອງສົ່ງຄືນ<br/><strong style=\"color:rgb(242, 102, 51)\">ດ້ານລຸ່ມນີ້</strong></span>";
        public override string KIOSK_OUT_MONTH_CARD_SUBTITLE { get; set; } =
            "<center><span style=\"font-size: 24px;\">ກະລຸນາ <strong style=\"color:rgb(242, 102, 51)\">ຮູດບັດ</strong> ລາຍເດືອນ</span><br/>ຫຼື ກົດ <strong style=\"color:rgb(242, 102, 51)\">ຖ່າຍປ້າຍ</strong>";
        public override string KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE { get; set; } =
            "<center><span style=\"font-size: 24px;\">ກະລຸນາ <strong style=\"color:rgb(242, 102, 51)\">ຮູດບັດ</strong> ລາຍເດືອນ</span></center>";

        // ===== Other =====
        public override string WaitConfirmOpenBarrie { get; set; } = "ຢືນຢັນເປີດດ່ານ";
        public override string SecurityNotConfirmOpenBarrie { get; set; } = "ພະນັກງານບໍ່ອະນຸມັດເປີດດ່ານ";

        public override string RevenueByAccessKeyCollection { get; set; } = "ຕາມກຸ່ມການກຳນົດ";
        public override string RevenueByLane { get; set; } = "ຕາມຊ່ອງ";
        public override string RevenueByUser { get; set; } = "ຕາມຜູ້ໃຊ້";

        public override string VerticalLeftToRight { get; set; } = "ຕັ້ງຈາກຊ້າຍໄປຂວາ";
        public override string VerticalRightToLeft { get; set; } = "ຕັ້ງຈາກຂວາໄປຊ້າຍ";
        public override string HorizontalLeftToRight { get; set; } = "ນອນຈາກຊ້າຍໄປຂວາ";
        public override string HorizontalRightToLeft { get; set; } = "ນອນຈາກຂວາໄປຊ້າຍ";

        public override string Vertical { get; set; } = "ຕັ້ງ";
        public override string Horizontal { get; set; } = "ນອນ";

        public override string DisplayDirection { get; set; } = "ທິດທາງການສະແດງ";
        public override string CameraRegion { get; set; } = "ພື້ນທີ່ກ້ອງ";
        public override string PicRegion { get; set; } = "ພື້ນທີ່ຮູບພາບ";
        public override string CameraPicRegion { get; set; } = "ພື້ນທີ່ກ້ອງ - ຮູບພາບ";
        public override string LprEventRegion { get; set; } = "ພື້ນທີ່ເຫດການ";
        public override string EventRegion { get; set; } = "ປະເພດການສະແດງ";

        public override string AllowOpenBarrieManual { get; set; } = "ອະນຸຍາດໃຫ້ເປີດ barrie ດ້ວຍມື";
        public override string AllowWriteTicketManual { get; set; } = "ອະນຸຍາດໃຫ້ຂຽນປີ້ດ້ວຍມື";
        public override string AllowRetakeImageManual { get; set; } = "ອະນຸຍາດໃຫ້ຖ່າຍຮູບອີກຄັ້ງດ້ວຍມື";
        public override string DisplayTitle { get; set; } = "ສະແດງຫົວຂໍ້";

        public override string RegisterByPlateDaily { get; set; } = "ອະນຸຍາດໃຫ້ລົດລາຍວັນເຂົ້າໂດຍປ້າຍທະບຽນ";

        public override string WarningDialog { get; set; } = "ກ່ອງເຕືອນ";
        public override string AutoCloseWarningAfter { get; set; } = "ປິດກ່ອງເຕືອນອັດຕະໂນມັດຫຼັງຈາກ";
        public override string AutoCloseResult { get; set; } = "ຜົນການປິດອັດຕະໂນມັດ";
        public override string Use { get; set; } = "ໃຊ້";

        public override string QRView { get; set; } = "QR View";
        public override string ComIp { get; set; } = "COM / IP";
        public override string BaudratePort { get; set; } = "Baudrate / Port";
        public override string AccountNumber { get; set; } = "ເລກບັນຊີ";
        public override string Bank { get; set; } = "ທະນາຄານ";

        public override string Other { get; set; } = "ອື່ນໆ";
        public override string AllowUseLoopImageForCardEvent { get; set; } = "ໃຊ້ຮູບ loop ລ່າສຸດສຳລັບເຫດການສະແກນບັດ";
        public override string ConnectingWithDevice { get; set; } = "ກຳລັງເຊື່ອມຕໍ່ກັບອຸປະກອນ";
        public override string DisconnectingWithDevice { get; set; } = "ກຳລັງຕັດການເຊື່ອມຕໍ່ກັບອຸປະກອນ";
        public override string PaymentMethodNotActive { get; set; } = "ຮູບແບບການຈ່າຍເງິນຍັງບໍ່ໄດ້ເປີດໃຊ້ງານ";
        public override string ChooseOtherPaymentMethodRequired { get; set; } = "ກະລຸນາເລືອກຮູບແບບການຈ່າຍເງິນອື່ນ";

    }
}

