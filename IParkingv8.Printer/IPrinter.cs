using iParkingv8.Object.Objects.Reports;

namespace IParkingv8.Printer
{
    public interface IPrinter
    {
        void PrintPhieuThu(string baseContent, string cardName, string cardGroupName, Image? img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "");
        void PrintOffliceInvoice(string baseContent, string cardName, string cardGroupName, Image? img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "");
        void PrintOnlineInvoice(string baseContent, string cardName, string cardGroupName, Image? img,
                                      DateTime dateTimeIn, DateTime dateTimeOut,
                                      string plate = "", string moneyStr = "",
                                      long moneyInt = 0, string receiveBillCode = "");
        string PrintRevenue(string templatePath, SearchRevenueReportResponse revenue);
        string PrintShiftHandOver(string baseContent, ShiftHandOverReport report, string user, DateTime startTime, DateTime endTime, long realFeeSecurity);
    }
}
