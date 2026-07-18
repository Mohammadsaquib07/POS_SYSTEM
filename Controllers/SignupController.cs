using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DTOs;
using Products_Crud.Model;
using Products_Crud.Services;


[ApiController]
[Route("api/auth")]
public class SignupController : ControllerBase
{
    private readonly IAuthService _authService;

    public SignupController(IAuthService authService) => _authService = authService;

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto request)
    {
        var result = await _authService.SignupAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}