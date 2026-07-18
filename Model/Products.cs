namespace Products_Crud.Model
{
    public class ProductsList
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Use decimal for currency/money values!
        public decimal Price { get; set; } 
        
        // Add this to track supermarket stock levels
        public int StockQuantity { get; set; } 
        
        // Optional: Add it now so scanner integration is effortless later
        public string? Barcode { get; set; } 
    }
}