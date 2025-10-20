using Microsoft.AspNetCore.Mvc;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Repositories;

namespace VideoHostingApi.Auth.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AuthContext dbContext, IUserRepository userRepository) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var user = new User{RoleId = Guid.Parse("e089fb28-fd0c-4a6e-8514-be392950a2a9"), Login = "aboba"};
        
        userRepository.Add(user); 
        await userRepository.SaveChanges(cancellationToken);
        
        return Ok();
    }
}