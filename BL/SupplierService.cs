using Erp.Bl.IsupplierInterface;
using Erp.Dto.Request.Dtos;
using Erp.Dto.Response.Dto;
using Erp.Dto.supplierDtos;
using Erp.interfaces.SupplierRepo;
using Erp.Model.Entities;


namespace Erp.Bl.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _suplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            _suplierRepository = supplierRepository;
        }

        public async Task<List<SupplierDto>> GetAllAsync()
        {
            var supplier = await _suplierRepository.GetAllAsync();
            return supplier.Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Phone = s.Phone,
                GstNumber = s.GstNumber,
                PaymentMode = s.PaymentMode.ToString()
            }).ToList();
        }


        public async Task<SupplierResponseDto> CreateAsync(CreateSupplierDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Phone))
            {
                return new SupplierResponseDto { Success = false, Message = "Name and Phone are required" };
            }

            var supplier = new Supplier
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                GstNumber = dto.GstNumber,
                PaymentMode = Enum.TryParse<SupplierPaymentMode>(dto.PaymentMode, out var mode) ? mode : SupplierPaymentMode.Cash,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            var id = await _suplierRepository.CreateAsync(supplier);
            return new SupplierResponseDto {Success = true,Message ="Supplier Created.",Id=id};
        }
    }

}