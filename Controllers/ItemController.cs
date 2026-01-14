using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.Controllers
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
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemsById(int id)
        {
            var item = await _productService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ProductDTO productDto)
        {
            var createdItem = await _productService.AddAsync(productDto);
            return CreatedAtAction(nameof(GetItemsById), new { id = createdItem.Id }, createdItem);
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
    }
}