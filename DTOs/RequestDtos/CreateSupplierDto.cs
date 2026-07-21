namespace Erp.Dto.Request.Dtos
{
    public class CreateSupplierDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? GstNumber { get; set; }
        public string? PaymentMode { get; set; }
    }
}