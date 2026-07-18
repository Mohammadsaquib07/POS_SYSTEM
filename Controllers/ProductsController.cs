using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DTOs;
using Products_Crud.DAL;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly UserDbContext _context; // Your existing EF Core DbContext

    public ProductsController(UserDbContext context)
    {
        _context = context;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        // Guard clause: If user types nothing or only 1 letter, don't hit the DB.
        if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
        {
            return Ok(new List<ProductSearchResponseDto>());
        }

        // Perform case-insensitive search and select only what we need
        var matchedProducts = await _context.ProductsList
            .Where(p => p.Name.Contains(query)) 
            .AsNoTracking() // Read-only performance optimization
            .Take(10)       // Cap results so the network payload stays microscopic
            .Select(p => new ProductSearchResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = (decimal)p.Price, // Cast if your current DB model is still using double
                StockQuantity = p.StockQuantity
            })
            .ToListAsync();

        return Ok(matchedProducts);
    }
}