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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Modules.Retail.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ProductService _productService;
        public ItemController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _productService.GetAllItems();
            var response = items.Select(MapToResponseDto).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemsById(int id)
        {
            var item = await _productService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(MapToResponseDto(item));
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ProductDto productDto)
        {
            var createdItem = await _productService.AddAsync(productDto);
            var response = MapToResponseDto(createdItem);
            return CreatedAtAction(nameof(GetItemsById), new { id = createdItem.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                await _productService.UpdateAsync(id, productDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _productService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(item.Id);
            return NoContent();
        }

        private ProductResponseDto MapToResponseDto(Items item)
        {
            return new ProductResponseDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock,
                Variants = item.Variants?.Select(v => new VariantResponseDto
                {
                    Values = string.IsNullOrEmpty(v.ValuesJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(v.ValuesJson),
                    Sku = v.Sku,
                    PurchasePrice = v.PurchasePrice,
                    StockQty = v.StockQty,
                    Status = v.Status
                }).ToList() ?? new List<VariantResponseDto>()
            };
        }
    }
}
