using Products_Crud.DTOs;
using Products_Crud.Model;

using Products_Crud.DTOs.ResponseDtos;

namespace Products_Crud.BL
{
    public interface IInvoiceService
    {
        System.Threading.Tasks.Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequest request);
        System.Threading.Tasks.Task<InvoiceResponseDto> GetInvoiceByIdAsync(int invoiceId);
        System.Threading.Tasks.Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();
    }
}
