using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMode Mode { get; set; }
        public DateTime PaidAt { get; set; }

        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
