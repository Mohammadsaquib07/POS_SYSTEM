using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using MyApp.Models;

namespace Products_Crud.Controllers
{

    //[Route("api/[controller]")]
    //public class UsersController : ControllerBase
    //{
    //    private readonly IUserService _service;

    //    public UsersController(IUserService service)
    //    {
    //        _service = service;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetAll()
    //    {
    //        var users = await _service.GetAllAsync();
    //        return Ok(users);
    //    }

    //    [HttpGet("{id:int}")]
    //    public async Task<IActionResult> Get(int id)
    //    {
    //        var user = await _service.GetByIdAsync(id);
    //        if (user is null) return NotFound();
    //        return Ok(user);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Create([FromBody] User user)
    //    {
    //        var created = await _service.CreateAsync(user);
    //        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    //    }

    //    [HttpPut("{id:int}")]
    //    public async Task<IActionResult> Update(int id, [FromBody] User user)
    //    {
    //        if (id != user.Id) return BadRequest();
    //        await _service.UpdateAsync(user);
    //        return NoContent();
    //    }

    //    [HttpDelete("{id:int}")]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var existing = await _service.GetByIdAsync(id);
    //        if (existing is null) return NotFound();
    //        var userId = new User();
    //        userId.Id = id;
    //        await _service.DeleteAsync(userId);
    //        return NoContent();
    //    }
    //}
}