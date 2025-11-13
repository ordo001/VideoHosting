using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Models;
using VideoHostingApi.Auth.Web.Models;

namespace VideoHostingApi.Auth.Web.Controllers;

/// <summary>
/// Контроллер аутентификации
/// </summary>
[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var userModel = mapper.Map<LoginModel>(loginRequest);
        var token = await authService.LoginAsync(userModel, cancellationToken);
        return Ok(token);
    }
}