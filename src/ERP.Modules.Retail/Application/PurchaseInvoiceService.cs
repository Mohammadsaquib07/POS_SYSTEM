using Erp.Dto.PurchaseService;
using Erp.Dtos.CreatePurchase;
using Erp.Dtos.PurchaseInvoiceResponse;
using Erp.interfaces.Purchase;
using Products_Crud.Common.Enums;
using Erp.Model.PuchaseInvoicEntities;
using Erp.Model.PurchaseInvoiceItemEntities;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;

public class PurchaseInvoiceService : IPurchaseInvoiceService
{
    private readonly UserDbContext _context;
    private readonly IPurchaseInvoiceRepository _repository;

    public PurchaseInvoiceService(UserDbContext context, IPurchaseInvoiceRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<List<PurchaseInvoice>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // ======================================================================
    // CREATE
    // ======================================================================
    public async Task<List<PurchaseInvoiceResponseDto>> CreateAsync(CreatePurchaseInvoiceDto dto, string? attachmentPath)
    {
        if (dto.Items == null || dto.Items.Count == 0)
        {
            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = false, Message = "At least one item is required." }
            };
        }

        if (dto.Items.Any(i => i.Quantity <= 0 || i.UnitPrice < 0))
        {
            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = false, Message = "Quantity must be positive and price cannot be negative." }
            };
        }

        if (dto.Items.Any(i => i.DiscountPercent < 0 || i.DiscountPercent > 100 || i.TaxPercent < 0 || i.TaxPercent > 100))
        {
            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = false, Message = "Discount and tax percentages must be between 0 and 100." }
            };
        }

        var isDuplicateInvoice = await _context.PurchaseInvoices
            .AnyAsync(p => p.SupplierId == dto.SupplierId && p.InvoiceNumber == dto.InvoiceNumber);

        if (isDuplicateInvoice)
        {
            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = false, Message = "This invoice number already exists for this supplier." }
            };
        }

        var productIds = dto.Items.Select(i => i.ProductId).Distinct().ToList();
        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync();

        if (products.Count != productIds.Count)
        {
            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = false, Message = "One or more products were not found." }
            };
        }

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var invoice = new PurchaseInvoice
            {
                InvoiceNumber = dto.InvoiceNumber,
                SupplierId = dto.SupplierId,
                InvoiceDate = dto.InvoiceDate,
                DueDate = dto.DueDate,
                PaymentMode = dto.PaymentMode,
                AttachmentPath = attachmentPath,
                Items = new List<PurchaseInvoiceItem>(),
                CreatedDate = DateTime.UtcNow
            };

            decimal subTotal = 0, totalDiscount = 0, totalTax = 0;

            foreach (var itemDto in dto.Items)
            {
                var lineGross = itemDto.Quantity * itemDto.UnitPrice;
                var lineDiscount = lineGross * (itemDto.DiscountPercent / 100m);
                var lineTaxable = lineGross - lineDiscount;
                var lineTax = lineTaxable * (itemDto.TaxPercent / 100m);
                var lineTotal = lineTaxable + lineTax;

                subTotal += lineGross;
                totalDiscount += lineDiscount;
                totalTax += lineTax;

                invoice.Items.Add(new PurchaseInvoiceItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    DiscountPercent = itemDto.DiscountPercent,
                    TaxPercent = itemDto.TaxPercent,
                    BatchNumber = itemDto.BatchNumber,
                    ExpiryDate = itemDto.ExpiryDate,
                    TotalPrice = lineTotal
                });

                var product = products.First(p => p.Id == itemDto.ProductId);
                var stockBefore = product.StockQuantity;
                var newStock = stockBefore + itemDto.Quantity;

                if (newStock > 0)
                {
                    product.CostPrice = ((product.CostPrice * stockBefore) + (itemDto.UnitPrice * itemDto.Quantity)) / newStock;
                }
                product.StockQuantity = newStock;
            }

            var grandTotal = subTotal - totalDiscount + totalTax;
            var amountPaid = Math.Clamp(dto.AmountPaid, 0, grandTotal);
            var balanceDue = grandTotal - amountPaid;

            PurchaseInvoiceStatus status;
            if (grandTotal > 0 && balanceDue <= 0) status = PurchaseInvoiceStatus.Paid;
            else if (amountPaid > 0) status = PurchaseInvoiceStatus.Partial;
            else status = PurchaseInvoiceStatus.Unpaid;

            invoice.SubTotal = subTotal;
            invoice.TotalDiscount = totalDiscount;
            invoice.TotalTax = totalTax;
            invoice.TotalAmount = grandTotal;
            invoice.AmountPaid = amountPaid;
            invoice.BalanceDue = balanceDue;
            invoice.Status = status;

            _context.PurchaseInvoices.Add(invoice);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new List<PurchaseInvoiceResponseDto> {
                new PurchaseInvoiceResponseDto { Success = true, Message = "Purchase invoice created.", Id = invoice.Id }
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // ======================================================================
    // DELETE — reverses stock and (approximately) reverses cost price
    // ======================================================================
    public async Task<PurchaseInvoiceResponseDto> DeleteAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var invoice = await _repository.GetByIdWithItemsAndProductsAsync(id);

            if (invoice == null)
            {
                return new PurchaseInvoiceResponseDto { Success = false, Message = "Purchase invoice not found." };
            }

            foreach (var item in invoice.Items)
            {
                var product = item.Product;
                if (product == null) continue;

                var stockAfterReversal = product.StockQuantity - item.Quantity;

                if (stockAfterReversal < 0)
                {
                    await transaction.RollbackAsync();
                    return new PurchaseInvoiceResponseDto
                    {
                        Success = false,
                        Message = $"Cannot delete: '{product.Name}' only has {product.StockQuantity} in stock, " +
                                  $"but this purchase added {item.Quantity}. Some of it has likely already been sold."
                    };
                }

                // Approximate reversal of the weighted-average cost — exact
                // only if no other purchase of this product happened since
                // this invoice was created. Good enough to undo a mistaken
                // entry made minutes ago; not a guaranteed perfect undo for
                // an old invoice with purchases layered on top of it since.
                if (stockAfterReversal > 0)
                {
                    var totalValueBefore = product.CostPrice * product.StockQuantity;
                    var valueBeingRemoved = item.UnitPrice * item.Quantity;
                    product.CostPrice = (totalValueBefore - valueBeingRemoved) / stockAfterReversal;
                }

                product.StockQuantity = stockAfterReversal;
            }

            _context.PurchaseInvoices.Remove(invoice);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new PurchaseInvoiceResponseDto { Success = true, Message = "Purchase invoice deleted and stock reversed." };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<PurchaseInvoice?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
