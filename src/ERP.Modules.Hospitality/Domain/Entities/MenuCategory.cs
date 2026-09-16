namespace ERP.Modules.Hospitality.Domain.Entities
{
    public class MenuCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
