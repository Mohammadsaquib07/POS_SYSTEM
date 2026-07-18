namespace Products_Crud.DTOs
{
    public class ProductSearchResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        
        // This helper formatted string matches your current UI display: "Track Pant (46 available)"
        public string DisplayText => $"{Name} ({StockQuantity} available)";
    }
}