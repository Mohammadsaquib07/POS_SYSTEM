using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? TableId { get; set; }
        public RestaurantTable Table { get; set; }

        public int? CustomerId { get; set; }
        public int StaffId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Bill Bill { get; set; }
    }
}
