using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Shared.DTOs;
using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Application;


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