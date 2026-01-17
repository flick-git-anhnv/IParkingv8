namespace IParkingv8.Cash.Model
{
    public class CashResult
    {
        public CashResult()
        {

        }
        public CashResult(long money)
        {
            MoneyValue = money;
        }
        // Trạng thái kết nối
        private bool isConnect = false;
        public bool IsConnect { get => isConnect; set => isConnect = value; }

        // Trangj thái cảnh báo
        private bool isWarning = false;
        public bool IsWarning { get => isWarning; set => isWarning = value; }
        
        // Mệnh giá tiền
        private long moneyValue = 0;
        public long MoneyValue { get => moneyValue; set => moneyValue = value; }

        public bool cho_phep_nuot_tien { get; set; }


        // Tổng tiền đã trả
        private long moneyPaid = 0;
        public long MoneyPaid{ get => moneyPaid; set => moneyPaid = value; }

        // Tiền cần thanh toán
        private long moneyPayment = 0;
        public long MoneyPayment { get => moneyPayment; set => moneyPayment = value; }
        
        // Trạng thái đang từ chối nhận tiền mặt
        private bool isRejecting = false;
        public bool IsRejecting { get => isRejecting; set => isRejecting = value; }

        // Trạng thái đã từ chối tiền mặt
        private bool isRejected = false;
        public bool IsRejected { get => isRejected; set => isRejected = value; }

        // Trạng thái xác nhận tiền hợp lệ
        private bool isValidMoney = false;
        public bool IsValidMoney { get => isValidMoney; set => isValidMoney = value; }   

        //// Số lượt trả tiền của 1 khách hàng 
        //private int turnMoney = 0;
        //public int TurnMoney { get => turnMoney; set => turnMoney = value; }

        // Kiểm tra key hợp lệ
        private bool isEncryptionKey = false;
        public bool IsEncryptionKey { get => isEncryptionKey; set => isEncryptionKey = value; }

        // Biển số khách hàng
        private string plate = "";
        public string Plate { get => plate; set => plate = value; }

        private DateTime datetimein = DateTime.Now;
        public DateTime DateTimeIn { get => datetimein; set => datetimein = value; }

        private DateTime datetimeout = DateTime.Now;
        public DateTime DateTimeOut { get => datetimeout; set => datetimeout = value; }

        // Trạng thái tiền đang đưa xuống kho chứa
        private bool isSuccessStatcked = false;
        public bool IsSuccessStatcked { get => isSuccessStatcked; set => isSuccessStatcked = value; }

        // Trạng thái tiền đã đưa xuống ngăn chứa 2 
        private bool isNoteStacking = false;
        public bool IsNoteStacking { get => isNoteStacking; set => isNoteStacking = value; }

        // Trạng thái hoàn tất tất cả thanh toán
        private bool isCompletePayment = false;
        public bool IsCompletePayment { get => isCompletePayment; set => isCompletePayment = value; }

        // Trạng thái gọi api thanh toán thành công
        private bool isCallAPIPaymentSuccess = false;
        public bool IsCallAPIPaymentSuccess { get => isCallAPIPaymentSuccess; set => isCallAPIPaymentSuccess = value; }

        // Trạng thái miêu tả 
        private string describe = "";
        public string Describe { get => describe; set => describe = value; }

        // Trạng thái ngăn chứa đầy tiền
        private bool isStackerFull = false;
        public bool IsStackerFull { get => isStackerFull; set => isStackerFull = value; }

        //private bool isStop = false;
        //public bool IsStop { get => isStop; set => isStop = value; }
    }
}
