using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Repositories;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Models;
using VideoHostingApi.Auth.Web.Models;

namespace VideoHostingApi.Auth.Web.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[Route("[controller]")]
[ApiController]
public class UserController(IUserServices userServices, IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserRequest addUserRequest,CancellationToken cancellationToken)
    {
        var model = mapper.Map<AddUserModel>(addUserRequest);
        await userServices.Add(model, cancellationToken);
        return Ok();
    }
}