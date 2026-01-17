namespace IParkingv8.API.Interfaces
{
    public interface IAPIServer
    {
        /// <summary>
        /// Login (token, ..)
        /// </summary>
        IAuth Auth { get; set; }
        /// <summary>
        /// CRUD Data (thẻ, xe nhóm định danh)
        /// </summary>
        IDataService DataService { get; set; }
        /// <summary>
        /// Báo cáo 
        /// </summary>
        IReportService ReportingService { get; set; }
        /// <summary>
        /// Thanh toán (offline, online, voucher)
        /// </summary>
        IPaymentService PaymentService { get; set; }
    }
}
