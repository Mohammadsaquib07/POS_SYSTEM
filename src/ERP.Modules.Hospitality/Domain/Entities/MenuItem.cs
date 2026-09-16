using ERP.Modules.Hospitality.Domain.Enums;

namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public bool IsAvailable { get; set; }
        public FoodType FoodType { get; set; }
        public SpiceLevel? SpiceLevel { get; set; }

        public int CategoryId { get; set; }
        public MenuCategory Category { get; set; }

        public ICollection<MenuItemVariant> Variants { get; set; }
    }
}
