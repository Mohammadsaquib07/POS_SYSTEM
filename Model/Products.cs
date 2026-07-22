using System.ComponentModel.DataAnnotations.Schema;

namespace Products_Crud.Model
{
    [Table("Products")]
public class ProductsList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? Category { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedOn { get; set; }
}
}