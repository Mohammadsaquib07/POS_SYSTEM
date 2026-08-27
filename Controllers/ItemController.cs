using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Erp.Dto.ItemsResponse;
using Erp.Dtos.Response.Variant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.Controllers
{
    // [Authorize]
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
        public async Task<IActionResult> CreateItem([FromBody] ProductDTO productDto)
        {
            var createdItem = await _productService.AddAsync(productDto);
            var response = MapToResponseDto(createdItem);
            return CreatedAtAction(nameof(GetItemsById), new { id = createdItem.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] ProductDTO productDto)
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

        private ProductResponseDTO MapToResponseDto(Items item)
        {
            return new ProductResponseDTO
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Stock = item.Stock,
                Variants = item.Variants?.Select(v => new VariantResponseDTO
                {
                    Values = string.IsNullOrEmpty(v.ValuesJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(v.ValuesJson),
                    Sku = v.Sku,
                    PurchasePrice = v.PurchasePrice,
                    StockQty = v.StockQty,
                    Status = v.Status
                }).ToList() ?? new List<VariantResponseDTO>()
            };
        }
    }
}