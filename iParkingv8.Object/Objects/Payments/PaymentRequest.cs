using Kztek.Object;

namespace iParkingv8.Object.Objects.Payments
{
    public class PaymentRequest
    {
        public TargetType TargetType { get; set; }
        public string TargetId { get; set; }
        public OrderMethod Method { get; set; } = OrderMethod.CASH;
        public long Amount { get; set; }
        public string PosId { get; set; } = string.Empty;
        public string Epc { get; set; } = string.Empty;
        public string Description { get; set; }
        public PaymentRequest(string targetId, OrderMethod orderMethod, long amount, string description,
                             TargetType targetType = TargetType.ENTRY, string posId = "", string epc = "")
        {
            TargetId = targetId;
            Method = orderMethod;
            Amount = amount;
            TargetType = targetType;
            PosId = posId;
            Epc = epc;
            Description = description.Replace("-", " ")+" "+posId;
        }
    }

}
