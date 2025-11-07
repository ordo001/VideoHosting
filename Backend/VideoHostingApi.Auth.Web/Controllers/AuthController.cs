using Microsoft.AspNetCore.Mvc;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Repositories;

namespace VideoHostingApi.Auth.Web.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthContext dbContext, IUserRepository userRepository) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> AddUser(CancellationToken cancellationToken)
    {
        
    }
}