using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class KitchenOrderTicket
    {
        public int KotId { get; set; }
        public string KotNumber { get; set; }
        public KotStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public ICollection<KotItem> KotItems { get; set; }
    }
}
