namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class MenuItemVariant
    {
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public decimal PriceAdjustment { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
