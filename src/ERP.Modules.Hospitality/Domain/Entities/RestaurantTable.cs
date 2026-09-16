using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class RestaurantTable
    {
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public int BranchId { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
