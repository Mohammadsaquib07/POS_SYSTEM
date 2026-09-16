using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime GeneratedAt { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
