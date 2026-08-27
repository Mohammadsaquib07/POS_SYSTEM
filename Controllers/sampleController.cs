// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Erp.interfaces.SampleRepository;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Products_Crud.BL;
// using Products_Crud.DTOs;
// using Products_Crud.Model;

// namespace Products_Crud.Controllers
// {
//     [Authorize]
//     [ApiController]
//     [Route("api/[controller]")]
//     public class sampleController : ControllerBase
//     {
//         private readonly ISampleProductRepository _repo;
//         public sampleController(ISampleProductRepository repo)
//         {
//             _repo = repo;
//         }
        
//         [HttpGet]
//         public async Task<IActionResult> GetAllItems()
//         {
//              return Ok(await _repo.GetAllAsync());
//         }

//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetItemsById(int id)
//         {
//             var item = await _repo.GetByIdAsync(id);
//             if (item == null)
//             {
//                 return NotFound();
//             }
//             return Ok(item);
//         }
//     }
// }