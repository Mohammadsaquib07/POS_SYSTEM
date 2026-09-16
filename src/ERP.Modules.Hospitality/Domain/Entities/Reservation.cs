using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        public int PartySize { get; set; }
        public ReservationStatus Status { get; set; }

        public int TableId { get; set; }
        public RestaurantTable Table { get; set; }
    }
}
