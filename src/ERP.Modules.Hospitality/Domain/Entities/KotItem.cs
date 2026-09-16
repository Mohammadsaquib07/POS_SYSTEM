using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class KotItem
    {
        public int KotItemId { get; set; }
        public KotStatus Status { get; set; }

        public int KotId { get; set; }
        public KitchenOrderTicket Kot { get; set; }

        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
    }
}
