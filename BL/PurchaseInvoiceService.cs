using System.Net.NetworkInformation;
using Erp.Dto.PurchaseInvoiceList;
using Erp.Dto.PurchaseService;
using Erp.Dtos.CreatePurchase;
using Erp.Dtos.PurchaseInvoiceResponse;
using Erp.interfaces.Purchase;
using Erp.Model.Enums;
using Erp.Model.PuchaseInvoicEntities;
using Erp.Model.PurchaseInvoiceItemEntities;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;

namespace Erp.Bl.PuchaseInvoice
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepo;
        private readonly ILogger<PurchaseInvoiceService> _logger;
        private readonly UserDbContext _context;

        public PurchaseInvoiceService(IPurchaseInvoiceRepository repository, UserDbContext context,ILogger<PurchaseInvoiceService> logger)
        {
            _purchaseInvoiceRepo = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<List<PurchaseInvoiceListDto>> GetAllAsync()
        {
            var invoices = await _purchaseInvoiceRepo.GetAllAsync();
            return invoices.Select(pi => new PurchaseInvoiceListDto
            {
                Id = pi.Id,
                InvoiceNumber = pi.InvoiceNumber,
                SupplierName = pi.Supplier?.Name,
                SupplierId = pi.SupplierId,
                InvoiceDate = pi.InvoiceDate,
                TotalAmount = pi.TotalAmount,
                Status = pi.Status.ToString()
            }).ToList();
        }

        public async Task<List<PurchaseInvoiceResponseDto>> CreateAsync(CreatePurchaseInvoiceDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
            {
                return new List<PurchaseInvoiceResponseDto> { new PurchaseInvoiceResponseDto { Success = false, Message = "At least one item is required." } };
            }
            if (dto.Items.Any(i => i.Quantity <= 0 || i.UnitPrice < 0))
                return new List<PurchaseInvoiceResponseDto> { new PurchaseInvoiceResponseDto { Success = false, Message = "Quantity must be positive and price cannot be negative." } };

            var productIds = dto.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _context.ProductsList
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            if (products.Count != productIds.Count)
                return new List<PurchaseInvoiceResponseDto> { new PurchaseInvoiceResponseDto { Success = false, Message = "One or more products were not found." } };

            if (!Enum.TryParse<PurchaseInvoiceStatus>(dto.Status, out var status))
                status = PurchaseInvoiceStatus.Pending;

            var invoice = new PurchaseInvoice
            {
                InvoiceNumber = dto.InvoiceNumber,
                SupplierId = dto.SupplierId,
                InvoiceDate = dto.InvoiceDate,
                DueDate = dto.DueDate,
                Status = status,
                Items = new List<PurchaseInvoiceItem>(),
                CreatedDate = DateTime.UtcNow
            };

            decimal runningTotal = 0;

            foreach (var itemDto in dto.Items)
            {
                var totalPrice = itemDto.Quantity * itemDto.UnitPrice;
                runningTotal += totalPrice;

                invoice.Items.Add(new PurchaseInvoiceItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    TotalPrice = totalPrice
                });

                var product = products.First(p => p.Id == itemDto.ProductId);
                product.StockQuantity += itemDto.Quantity;
            }

            invoice.TotalAmount = runningTotal;

            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync();

            return new List<PurchaseInvoiceResponseDto> { new PurchaseInvoiceResponseDto { Success = true, Message = "Purchase invoice created.", Id = invoice.Id } };
        }

        public async Task<PurchaseInvoiceListDto?> GetByIdAsync(int Id)
        {
            var invoice = await _purchaseInvoiceRepo.GetByIdAsync(Id);
            if (invoice == null)
            {
                _logger.LogWarning("Purchase invoice {Id} not found",Id);
                return null;
            }

            return new PurchaseInvoiceListDto
            {
                Id = invoice.Id,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceNumber = invoice.InvoiceNumber,
                SupplierName = invoice.Supplier?.Name ?? "N/A",
                TotalAmount = invoice.TotalAmount,
                Status = invoice.Status.ToString()
            };
        }

        public async Task<bool> DeleteAsync(int Id){
            var delete = await _purchaseInvoiceRepo.DeleteAsync(Id); 
            if(!delete){
                _logger.LogWarning("Attempted to delete non-existent invoice {Id} ",Id);
            }
            return delete;
        }
    }
}