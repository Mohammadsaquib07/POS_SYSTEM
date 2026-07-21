using Erp.Dto.Request.Dtos;
using Erp.Dto.Response.Dto;
using Erp.Dto.supplierDtos;

namespace Erp.Bl.IsupplierInterface
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAllAsync();
        Task<SupplierResponseDto> CreateAsync(CreateSupplierDto dto); 
    }
}