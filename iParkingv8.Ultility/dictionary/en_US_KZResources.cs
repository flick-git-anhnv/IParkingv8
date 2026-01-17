using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParkingv8.Ultility.dictionary
{
    public class en_US_KZResources : UIBuiltInResources
    {
        public override CultureInfo CultureInfo => new CultureInfo("en-US");

        public override string EventInfo { get; set; } = "Event information";

        public override string MiSystem { get; set; } = "System";
        public override string MiChangePassword { get; set; } = "Change password";
        public override string MiLogOut { get; set; } = "Log out";
        public override string MiExit { get; set; } = "Exit";

        public override string MiReport { get; set; } = "Reports";
        public override string MiReportIn { get; set; } = "Currently parked";
        public override string MiReportOut { get; set; } = "Exited";
        public override string MiReportRevenue { get; set; } = "Revenue report";
        public override string MiReportHandOver { get; set; } = "Shift closing report";

        public override string MiData { get; set; } = "Data";
        public override string MiAccessKeyList { get; set; } = "Access key list";
        public override string MiCustomerList { get; set; } = "Customer list";
        public override string MiVehicleList { get; set; } = "Vehicle list";

        public override string LicenseValid { get; set; } = "Valid";
        public override string Day { get; set; } = "Day";
        public override string Quantity { get; set; } = "Quantity";
        public override string Printer { get; set; } = "Printer";

        public override string InfoTitle { get; set; } = "Notification";
        public override string SuccessTitle { get; set; } = "Success";
        public override string WarningTitle { get; set; } = "Warning";
        public override string ErrorTitle { get; set; } = "Error";
        public override string AskTitle { get; set; } = "Question";
        public override string InputTitle { get; set; } = "Enter information";
        public override string SelectTitle { get; set; } = "Select information";
        public override string CloseAll { get; set; } = "Close all";

        public override string Confirm { get; set; } = "Confirm";
        public override string Cancel { get; set; } = "Close";
        public override string Back { get; set; } = "Back";
        public override string BackToMain { get; set; } = "Back to Home";
        public override string Clear { get; set; } = "Clear";
        public override string ProcessChecking { get; set; } = "Checking information";
        public override string ProcessReadingPlate { get; set; } = "Reading license plate";
        public override string ProcessCheckIn { get; set; } = "Checking in";
        public override string ProcessCheckOut { get; set; } = "Checking out";
        public override string ProcessInvalidDailyVehicleIn { get; set; } = "Invalid plate! Allow the vehicle to enter?";
        public override string ProcessConfirmOpenbarrie { get; set; } = "Do you confirm opening the barrier?";
        public override string ProcessConfirmRequest { get; set; } = "Confirm request";
        public override string ProcessOpenBarrie { get; set; } = "Opening barrier...";
        public override string ProcessSaveImage { get; set; } = "Saving Image...";
        public override string ProccesNotConfirmVehicleEntry { get; set; } = "Do not confirm vehicle entry";
        public override string ProccesNotConfirmVehicleExit { get; set; } = "Do not confirm vehicle exit";
        public override string ProcessGetEntryInfor { get; set; } = "Reading entry information";
        public override string ProcessConfirmCloseApp { get; set; } = "Are you sure you want to close the application?";

        public override string StartTime { get; set; } = "Start time";
        public override string EndTime { get; set; } = "End time";

        public override string Search { get; set; } = "Search";
        public override string Register { get; set; } = "Register";
        public override string CheckFee { get; set; } = "Check fee";
        public override string ExportExcel { get; set; } = "Export to Excel";
        public override string Name { get; set; } = "Name";

        public override string Open { get; set; } = "Open";
        public override string Save { get; set; } = "Save";
        public override string SaveConfigSuccess { get; set; } = "Configuration saved successfully";
        public override string ClearConfigSuccess { get; set; } = "Configuration cleared successfully";
        public override string Config { get; set; } = "System configuration";

        public override string All { get; set; } = "All";
        public override string Welcome { get; set; } = "Welcome";
        public override string SeeYouAgain { get; set; } = "See you again";
        public override string TakeMoney { get; set; } = "Collect cash";
        public override string Keyword { get; set; } = "Keyword";
        public override string Status { get; set; } = "Status";
        public override string TitlePicMerge { get; set; } = "ENTRY & EXIT IMAGES";
        public override string TitlePicIn { get; set; } = "ENTRY IMAGE";
        public override string TitlePanoramaIn { get; set; } = "ENTRY PANORAMA";
        public override string TitleVehicleIn { get; set; } = "ENTRY PLATE";

        public override string TitlePicOut { get; set; } = "EXIT IMAGE";
        public override string TitlePanoramaOut { get; set; } = "EXIT PANORAMA";
        public override string TitleVehicleOut { get; set; } = "EXIT PLATE";

        // Form
        public override string FrmLogin { get; set; } = "Sign in";
        public override string FrmLoading { get; set; } = "Loading system configuration";
        public override string FrmSelectLane { get; set; } = "Select active lane";
        public override string FrmChangePassword { get; set; } = "Change password";
        public override string FrmVerifyPassword { get; set; } = "Verify password";
        public override string FrmTestController { get; set; } = "Test devices";
        public override string FrmTestLed { get; set; } = "Test LED board";
        public override string FrmSelectVehicle { get; set; } = "Verify vehicle";
        public override string FrmSelectAccessKeyCollection { get; set; } = "Change access key collection";
        public override string FrmAccessKeyList { get; set; } = "Access key list";
        public override string FrmCustomerList { get; set; } = "Customer list";
        public override string FrmVehicleList { get; set; } = "Vehicle list";
        public override string FrmEditPlate { get; set; } = "Update license plate information";

        #region OBJECTS - USER
        public override string User { get; set; } = "User";
        public override string UserIn { get; set; } = "Entry supervisor";
        public override string UserOut { get; set; } = "Exit supervisor";

        public override string Username { get; set; } = "Username";
        public override string Password { get; set; } = "Password";
        public override string CurrentPassword { get; set; } = "Current password";
        public override string NewPassword { get; set; } = "New password";
        public override string ConfirmPassword { get; set; } = "Confirm new password";
        #endregion

        #region OBJECTS - ACCESS KEY
        public override string AccesskeyList { get; set; } = "Access key list";
        public override string AccesskeyName { get; set; } = "Entry ticket";
        public override string AccesskeyCode { get; set; } = "Code";
        public override string AccesskeyNote { get; set; } = "Note";
        public override string AccesskeyStatusLocked { get; set; } = "Locked";
        public override string AccesskeyStatusNotUsed { get; set; } = "Not In Use";
        public override string AccesskeyStatusInUsed { get; set; } = "In Use";

        public override string AccesskeyTypeVehicle { get; set; } = "Vehicle";
        public override string AccesskeyTypeCard { get; set; } = "Card";
        public override string AccesskeyTypeQR { get; set; } = "Qr Code";
        public override string AccesskeyTypeFinger { get; set; } = "Finger print";
        public override string AccesskeyTypeFace { get; set; } = "Face ID";

        #endregion

        #region OBJECTS - LANE
        public override string Lane { get; set; } = "Lane";
        public override string LaneIn { get; set; } = "Entry lane";
        public override string LaneOut { get; set; } = "Exit lane";
        #endregion

        #region OBJECTS - INVOICE
        public override string InvoiceTemplate { get; set; } = "Invoice template";
        public override string InvoiceNo { get; set; } = "Invoice number";
        #endregion


        #region OBJECTS - VEHICLE
        public override string Vehicles { get; set; } = "Vehicles";

        public override string VehicleList { get; set; } = "Vehicle list";
        public override string VehicleName { get; set; } = "Vehicle name";
        public override string VehicleCode { get; set; } = "License plate";
        public override string VehicleType { get; set; } = "Vehicle type";
        public override string VehicleCodeAcronym { get; set; } = "Plate";
        public override string VehicleExpiredDate { get; set; } = "Expiry date";

        public override string VehicleTypeCar { get; set; } = "Car";
        public override string VehicleTypeMotor { get; set; } = "Motorbike";
        public override string VehicleTypeBike { get; set; } = "Bike";

        public override string CurrentPlate { get; set; } = "Current license plate";
        public override string NewPlate { get; set; } = "New license plate";

        #endregion

        #region OBJECTS - CUSTOMER
        public override string CustomerList { get; set; } = "Customer list";
        public override string CustomerName { get; set; } = "Customer name";
        public override string CustomerCode { get; set; } = "Customer code";
        #endregion

        #region OBJECTS - COLLECTION
        public override string AccessKeyCollectionList { get; set; } = "Access key group list";
        public override string AccessKeyCollection { get; set; } = "Access key group";
        public override string CustomerCollection { get; set; } = "Customer group";

        // Collection
        public override string CollectionVIP { get; set; } = "VIP Vehicle";
        public override string CollectionMonth { get; set; } = "Monthly Vehicle";
        public override string CollectionDaily { get; set; } = "Daily Vehicle";
        #endregion

        #region OBJECTS - VOUCHER
        public override string VoucherList { get; set; } = "Voucher list";
        public override string VoucherName { get; set; } = "Voucher";
        #endregion

        #region OBJECTS - TRANSACTION
        public override string TransactionId { get; set; } = "Transaction ID";
        #endregion

        #region OBJECTS - EVENT
        public override string CameraIn { get; set; } = "ENTRY CAMERA";
        public override string CameraOut { get; set; } = "EXIT CAMERA";
        public override string Plate { get; set; } = "License plate";
        public override string PlateIn { get; set; } = "Entry plate";
        public override string PlateOut { get; set; } = "Exit plate";
        public override string PlateDetected { get; set; } = "Detected plate";
        public override string PlateDetectedAcronym { get; set; } = "Detected";

        public override string TimeIn { get; set; } = "Time in";
        public override string TimeOut { get; set; } = "Time out";
        public override string Duration { get; set; } = "Parking duration";

        public override string PicPanoramaIn { get; set; } = "Entry panorama";
        public override string PicVehicleIn { get; set; } = "Entry vehicle image";
        public override string PicPlateIn { get; set; } = "Entry plate image";

        public override string PicPanoramaOut { get; set; } = "Exit panorama";
        public override string PicVehicleOut { get; set; } = "Exit vehicle image";
        public override string PicPlateOut { get; set; } = "Exit plate image";

        public override string PaymentInfo { get; set; } = "Payment information";
        public override string PaymentSupport { get; set; } = "Supported payment methods";
        public override string EditNoteInfor { get; set; } = "Edit note";
        public override string EditPlateInfo { get; set; } = "Edit license plate";

        public override string Fee { get; set; } = "Parking fee";
        public override string Paid { get; set; } = "Paid";

        public override string Discount { get; set; } = "Discount";
        public override string RealFee { get; set; } = "Actual fee";

        public override string Remain { get; set; } = "Remaining";
        public override string VoucherInUse { get; set; } = "Voucher applied";
        #endregion

        #region OBJECTS - DEVICE
        public override string Camera { get; set; } = "Camera";
        public override string Angle { get; set; } = "Angle";
        public override string Device { get; set; } = "Device";
        public override string Connected { get; set; } = "Connected";
        public override string Disconnected { get; set; } = "Disconnected";
        public override string StopMode { get; set; } = "Stopped";
        public override string CardOutputFormat { get; set; } = "Format";
        public override string CardOutputAdditional { get; set; } = "Additional";

        public override string FormatToiGian { get; set; } = "Minimal";
        public override string FormatToiThieu8KyTu { get; set; } = "At least 8 characters";
        public override string FormatToiThieu10KyTu { get; set; } = "At least 10 characters";

        #endregion

        #region OBJECTS - LED
        public override string Led { get; set; } = "LED";
        #endregion

        #region OBJECTS - Shortcut
        public override string Shortcut { get; set; } = "Shortcut";
        #endregion

        #region OBJECTS - Display
        public override string DisplaySetting { get; set; } = "Display";
        #endregion

        #region OBJECTS - OptionSetting
        public override string OptionSetting { get; set; } = "Options";
        #endregion

        #region OBJECTS - ControllerSetting
        public override string ControllerSetting { get; set; } = "Controller";
        #endregion

        #region TIMER
        public override string AutoConfirmAfter { get; set; } = "Auto-confirm after";
        public override string AutoCancelAfter { get; set; } = "Auto-close after";
        public override string AutoLoginAfter { get; set; } = "Auto-login after";
        public override string AutoOpenHomePage { get; set; } = "Auto-open app UI after";
        #endregion

        #region Payment
        public override string PaymentQrTitle { get; set; } = "Scan QR to pay";
        public override string PaymentVisaTitle { get; set; } = "Swipe card to pay";
        public override string PaymentVisaSubTitle { get; set; } = "Please verify the transaction details and swipe your bank card on the payment device";

        public override string PaymentVoucherTitle { get; set; } = "Scan the voucher code at the discount scanner";
        public override string PaymentVoucherSubTitle { get; set; } = "The device does not return change";

        public override string PaymentCashTitle { get; set; } = "Insert cash one by one into the machine";
        public override string PaymentCashSubTitle { get; set; } = "The device does not return change";
        #endregion

        public override string DailyVehicle { get; set; } = "Daily vehicle";
        public override string MonthVehicle { get; set; } = "Monthly vehicle";

        // Button
        #region BUTTON
        public override string RetakeImage { get; set; } = "Capture plate";
        public override string Setting { get; set; } = "Settings";
        public override string WriteIn { get; set; } = "Write entry ticket";
        public override string WriteOut { get; set; } = "Write exit ticket";
        public override string Print { get; set; } = "Print";

        public override string Liveview { get; set; } = "Live view";
        public override string CarLprDetect { get; set; } = "Car plate recognition";
        public override string MotorLprDetect { get; set; } = "Motorbike plate recognition";
        public override string DrawLPR { get; set; } = "Draw plate region";
        public override string DrawMotion { get; set; } = "Draw motion region";
        public override string OpenBarrie { get; set; } = "Open barrier";
        public override string CollectCard { get; set; } = "Collect card";
        public override string RejectCard { get; set; } = "Eject card";
        public override string ChangCollection { get; set; } = "Change card type";
        public override string PayParkingFee { get; set; } = "Pay parking fee";
        public override string Cash { get; set; } = "Cash";
        public override string Voucher { get; set; } = "Voucher";
        public override string QR { get; set; } = "QR code";
        public override string VISA { get; set; } = "Bank card";
        public override string ShortCutSelectGuide { get; set; } = "Press Enter to search.\r\nDouble-click or press Confirm to select.";
        public override string ShortCutGuide2Line { get; set; } = "Enter to confirm\r\nEsc to close";
        public override string ShortCutGuide1Line { get; set; } = "Enter: Confirm | ESC: Close";
        public override string CreateQRView { get; set; } = "Create QR";
        public override string ReCreateQRView { get; set; } = "Recreate QR";
        #endregion

        #region CHECKBOX
        public override string RememberPassword { get; set; } = "Auto sign-in";
        public override string SelectAll { get; set; } = "Select all";
        #endregion

        #region REPORT
        public override string Total { get; set; } = "Total: ";
        public override string EventList { get; set; } = "Event list";
        public override string ColTime { get; set; } = "Time";
        public override string ColEventType { get; set; } = "Event";
        public override string ColCardOrLoop { get; set; } = "Reader | Loop";
        public override string ColCardNumber { get; set; } = "Card number";

        public override string colAccessKeyKeyword { get; set; } = "Name | Code | Note";
        public override string colAccessKeyName { get; set; } = "Name";
        public override string colAccessKeyCode { get; set; } = "Code";
        public override string colType { get; set; } = "Type";
        public override string colStatus { get; set; } = "Status";
        public override string colNote { get; set; } = "Note";
        public override string colCollection { get; set; } = "Group";

        // Customer list
        public override string colCustomerKeyword { get; set; } = "Name | Code";
        public override string colCustomerName { get; set; } = "Name";
        public override string colCustomerCode { get; set; } = "Code";
        public override string colCustomerPhone { get; set; } = "Phone";
        public override string colCustomerCollection { get; set; } = "Group";
        public override string colCustomerAddress { get; set; } = "Address";

        // Vehicle list
        public override string colVehicleName { get; set; } = "Name";
        public override string colVehicleCode { get; set; } = "Plate number";
        public override string colVehicleAccessKey { get; set; } = "Access key";
        public override string colVehicleStatus { get; set; } = "Status";
        public override string colVehicleType { get; set; } = "Type";
        public override string colVehicleCustomerName { get; set; } = "Customer";
        public override string colVehicleCustomerCollection { get; set; } = "Customer group";
        public override string colVehicleCustomerPhone { get; set; } = "Phone";
        public override string colVehicleCustomerAddress { get; set; } = "Address";
        public override string colVehicleExpiredDate { get; set; } = "Expiry date";
        public override string colVehicleCollection { get; set; } = "Group";
        #endregion

        // Common errors
        public override string AccountInvalidPermission { get; set; } = "Your account does not have permission to perform this function!";

        public override string ServerConfigInvalid { get; set; } = "Server configuration not found or invalid!";
        public override string ActiveLicenseError { get; set; } = "Activation failed!";
        public override string InvalidEventInfo { get; set; } = "No event information!";
        public override string InvalidPrintTemplate { get; set; } = "Print template not found!";
        public override string SystemError { get; set; } = "An error occurred during processing!";
        public override string ServerDisconnected { get; set; } = "Disconnected from server!";
        public override string TryAgain { get; set; } = "Please try again!";
        public override string TryAgainLater { get; set; } = "Please try again later!";
        public override string UnreturnedAccessKey { get; set; } = "Unreturned card";
        public override string InEntryWaitingTime { get; set; } = "In entry waiting time";
        public override string InExitWaitingTime { get; set; } = "In exit waiting time";
        public override string GetCameraImageError { get; set; } = "Error occurred while retrieving image from camera";

        // Login error
        public override string InvalidLogin { get; set; } = "Login failed";
        public override string InvalidPassword { get; set; } = "Incorrect password!";

        // Device config
        public override string InvalidDeviceConfig { get; set; } = "No device configuration!";
        public override string InvalidReserverLaneConfig { get; set; } = "No reverse-lane configuration";

        // Reader
        public override string InvalidReaderConfig { get; set; } = "Reader is not registered in the system";
        public override string InvalidReaderDailyTitle { get; set; } = "You are using a daily card!";
        public override string InvalidReaderDailySubTitle { get; set; } = "Please insert the card into the return slot";
        public override string InvalidReaderMonthlyTitle { get; set; } = "You are using a monthly card!";
        public override string InvalidReaderMonthlySubTitle { get; set; } = "Please swipe the card at the monthly reader";

        // Access Key
        public override string InvalidPermission { get; set; } = "Invalid permission!";
        public override string AccessKeyNotFound { get; set; } = "Your card is not registered!";
        public override string AccessKeyNotSupport { get; set; } = "Card not supported, please check!";
        public override string AccessKeyInWaitingTime { get; set; } = "You swiped too quickly, please try again in a moment!";
        public override string AccessKeyLocked { get; set; } = "Your card is not activated!";
        public override string AccessKeyExpired { get; set; } = "Your vehicle has expired!";
        public override string AccessKeyMonthNoVehicle { get; set; } = "Monthly card not linked to a vehicle!";
        public override string AccessKeyVipNoVehicle { get; set; } = "VIP card not linked to a vehicle!";
        public override string BlackedList { get; set; } = "Blacklisted plate!";

        // Collection error
        public override string CollectionLocked { get; set; } = "The collection has not been activated!";

        // Vehicle
        public override string VehicleNotFound { get; set; } = "Your vehicle is not registered in the system!";
        public override string VehicleLocked { get; set; } = "Your vehicle is locked!";
        public override string VehicleNotAllowEntryByPlate { get; set; } = "Vehicle not allowed to enter by plate!";
        public override string VehicleNotAllowExityByPlate { get; set; } = "Vehicle not allowed to enter by plate!";

        // Entry
        public override string EntryNotFound { get; set; } = "Vehicle has not entered the lot!";
        public override string EntryDupplicated { get; set; } = "Vehicle already entered!";

        // Plate
        public override string InvalidPlateNumber { get; set; } = "Unable to read the plate!";
        public override string PlateInOutNotSame { get; set; } = "Entry and exit plates do not match!";
        public override string PlateNotMatchWithSystem { get; set; } = "Plate does not match the registered plate!";

        // Device connection
        public override string DeviceDisconnected { get; set; } = "Cannot connect to device!";

        // Voucher
        public override string VoucherInvalidType { get; set; } = "Invalid voucher type!";

        public override string VoucherNotFound { get; set; } = "Voucher has not been registered in the system!";
        public override string VoucherNotActived { get; set; } = "Voucher has not been activated!";
        public override string VoucherExpired { get; set; } = "Voucher has expired!";
        public override string VoucherInvalidTime { get; set; } = "Voucher is not allowed to be used during this time frame!";

        public override string VoucherlistEmpty { get; set; } = "No voucher available for this vehicle!";
        public override string VoucherApplyError { get; set; } = "Error applying voucher, please try again!";
        public override string VoucherInUsedError { get; set; } = "Voucher has already been used!";

        // Change group
        public override string ChangeCollectionError { get; set; } = "Error changing card group, please try again!";

        // Transaction
        public override string TransactionCreateError { get; set; } = "Failed to create transaction!";

        // Customer actions
        public override string ActiveLicenseRequired { get; set; } = "The application is not activated! Do you want to activate it now?";
        public override string CustomerCommandOpenBarrie { get; set; } = "Command: Open barrier";
        public override string CustomerCommandWriteIn { get; set; } = "Command: Write entry ticket";
        public override string CustomerCommandWriteOut { get; set; } = "Command: Write exit ticket";
        public override string CustomerCommandCapture { get; set; } = "Command: Capture again";
        public override string CustomerCommandPrintTicket { get; set; } = "Command: Print ticket";
        public override string CustomerCommandUpdatePlate { get; set; } = "Command: Update plate";
        public override string CustomerCommandReserverLane { get; set; } = "Command: Reverse lane";
        public override string CustomerCommandOpenSettingPage { get; set; } = "Command: Open settings";

        public override string PlateSame { get; set; } = "Plate matches!";
        public override string PaymentRequired { get; set; } = "Please pay the parking fee!";
        public override string ChooseLaneRequired { get; set; } = "Please select an active lane";
        public override string ChooseCameraRequired { get; set; } = "Please select a camera";
        public override string ChooseVehicleEntry { get; set; } = "Please confirm the vehicle entry";
        public override string ChooseVehicleExit { get; set; } = "Please confirm the vehicle exit";
        public override string TakeCardRequired { get; set; } = "Card has been ejected, please take it";
        public override string WaitForSecurityConfirm { get; set; } = "Please wait for the operator to confirm";
        public override string ChoosePaymentMethodRequired { get; set; } = "Please select a payment method";
        public override string InputPasswordRequired { get; set; } = "Please enter password to perform this action";
        public override string CreateQRViewRequired { get; set; } = "Please create the payment QR first!";
        public override string ConfirmApplyVoucher { get; set; } = "Do you confirm using the voucher?";
        public override string WaitAMoment { get; set; } = "Please wait a moment!";
        public override string ConfirmPlateRequired { get; set; } = "Confirm the plate!";

        // Loading
        public override string TransationCreating { get; set; } = "Creating transaction";
        public override string LoadDeviceConfig { get; set; } = "Loading device info";
        public override string LoadAccessKeyCollection { get; set; } = "Loading access groups";
        public override string ConnectToController { get; set; } = "Connecting to device";
        public override string InitLprEngine { get; set; } = "Initializing LPR engine";
        public override string InitView { get; set; } = "Initializing UI";
        public override string InitTimer { get; set; } = "Starting real-time timer";

        // Kiosk
        public override string KioskDailyCard { get; set; } = "Daily ticket";
        public override string KioskMonthlyCard { get; set; } = "Monthly ticket";

        public override string KioskInDashboardTitle { get; set; } = "ENTRY CONTROL KIOSK";
        public override string KioskInDashboardSubTitle { get; set; } = "";
        public override string KioskInHaveAGoodDay { get; set; } = "Have a great day";

        public override string KioskOutDashboardTitle { get; set; } = "EXIT CONTROL KIOSK";
        public override string KioskOutDashboardSubTitle { get; set; } = "";
        public override string KioskOutValidEvent { get; set; } = "Your parking fee has been paid";
        public override string KIOSK_IN_DAILY_CARD_SUBTITLE { get; set; } = "<center><span style = \"font-size: 24px;\">Please press the <strong style=\"color:rgb(242, 102, 51)\">Get Card</strong> button<br/>below</span>";
        public override string KIOSK_OUT_DAILY_CARD_SUBTITLE { get; set; } = "<center><span style = \"font-size: 24px;\">Please insert your card into the return slot<br/><strong style=\"color:rgb(242, 102, 51)\">below</strong></span>";
        public override string KIOSK_OUT_MONTH_CARD_SUBTITLE { get; set; } = "<center><span style=\"font-size: 24px;\">Please <strong style=\"color:rgb(242, 102, 51)\">swipe your card</strong> at the monthly reader</span><br/>or press <strong style=\"color:rgb(242, 102, 51)\">Capture plate</strong>";
        public override string KIOSK_OUT_MONTH_CARD_NO_LOOP_SUBTITLE { get; set; } = "<center><span style=\"font-size: 24px;\">Please <strong style=\"color:rgb(242, 102, 51)\">swipe your card</strong> at the monthly reader</span>";

        // OTHER
        public override string WaitConfirmOpenBarrie { get; set; } = "Confirm opening barrier";
        public override string SecurityNotConfirmOpenBarrie { get; set; } = "Operator did not approve opening the barrier";

        public override string RevenueByAccessKeyCollection { get; set; } = "By access key collection";
        public override string RevenueByLane { get; set; } = "By lane";
        public override string RevenueByUser { get; set; } = "By user";

        public override string VerticalLeftToRight { get; set; } = "Vertical left to right";
        public override string VerticalRightToLeft { get; set; } = "Vertical right to left";
        public override string HorizontalLeftToRight { get; set; } = "Horizontal left to right";
        public override string HorizontalRightToLeft { get; set; } = "Horizontal right to left";

        public override string Vertical { get; set; } = "Vertical";
        public override string Horizontal { get; set; } = "Horizontal";

        public override string DisplayDirection { get; set; } = "Display direction";
        public override string CameraRegion { get; set; } = "Camera region";
        public override string PicRegion { get; set; } = "Image region";
        public override string CameraPicRegion { get; set; } = "Camera - image region";
        public override string LprEventRegion { get; set; } = "Event region";
        public override string EventRegion { get; set; } = "Interface type";

        public override string AllowOpenBarrieManual { get; set; } = "Allow manual barrier opening";
        public override string AllowWriteTicketManual { get; set; } = "Allow manual ticket writing";
        public override string AllowRetakeImageManual { get; set; } = "Allow manual image retake";
        public override string DisplayTitle { get; set; } = "Display title";

        public override string RegisterByPlateDaily { get; set; } = "Allow daily vehicles to enter by license plate";

        public override string WarningDialog { get; set; } = "Warning dialog";
        public override string AutoCloseWarningAfter { get; set; } = "Auto close warning after";
        public override string AutoCloseResult { get; set; } = "Auto close result";
        public override string Use { get; set; } = "Use";

        public override string QRView { get; set; } = "QR View";
        public override string ComIp { get; set; } = "COM / IP";
        public override string BaudratePort { get; set; } = "Baudrate / Port";
        public override string AccountNumber { get; set; } = "Account number";
        public override string Bank { get; set; } = "Bank";

        public override string Other { get; set; } = "Other";
        public override string AllowUseLoopImageForCardEvent { get; set; } = "Use the latest loop image for card event";

        public override string ConnectingWithDevice { get; set; } = "Connecting to device";
        public override string DisconnectingWithDevice { get; set; } = "Disconnecting from device";
        public override string PaymentMethodNotActive { get; set; } = "Payment method not activated";
        public override string ChooseOtherPaymentMethodRequired { get; set; } = "Please choose another payment method";


    }
}
