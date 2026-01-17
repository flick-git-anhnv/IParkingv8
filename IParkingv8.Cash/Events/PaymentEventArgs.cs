using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IParkingv8.Cash.Events
{
    public delegate void OnPaymentComplete(object sender, PaymentEventArgs e);

    public class PaymentEventArgs
    {
        /// <summary>
        /// Thời guan hoàn thành giao dịch
        /// </summary>
        public DateTime PayTime { get; set; }
        /// <summary>
        /// Lượng tiền thanh toán
        /// </summary>
        public int PayValue { get; set; }
        /// <summary>
        /// Kết quả giao dịch
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Nguyên nhân lỗi hoặc thành công
        /// </summary>
        public EMPayMessage Message { get; set; }
    }

    public enum EMPayMessage
    {
        Success,
        Cancel,
        Error
    }
}
