using Erp.Dtos.Response.Variant;

namespace Erp.Dto.ItemsResponse
{
     public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<VariantResponseDto> Variants { get; set; } = new();
    }
}
