using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Application
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Items>> GetAllItems()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Items?> GetItemById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Items> AddAsync(ProductDto dto)
        {
            var item = new Items
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                Variants = dto.Variants?.Select(v => new ProductVariant
                {
                    ValuesJson = JsonSerializer.Serialize(v.Values),
                    Sku = v.Sku,
                    PurchasePrice = v.PurchasePrice ?? dto.Price,
                    StockQty = v.StockQty ?? dto.Stock,
                    Status = v.Status ?? "Active"
                }).ToList() ?? new List<ProductVariant>()
            };

            return await _repo.AddAsync(item);
        }

        public async Task<Items?> UpdateAsync(int id, ProductDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return null;
            existing.Name = dto.Name;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;

            return await _repo.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null)
                return false;
            await _repo.DeleteAsync(item);
            return true;
        }
    }
}
