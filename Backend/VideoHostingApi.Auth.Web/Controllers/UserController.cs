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

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var users = await userServices.GetUsers(cancellationToken);
        return Ok(mapper.Map<List<UserResponse>>(users));
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userServices.GetUserById(userId, cancellationToken);
        return Ok(mapper.Map<UserResponse>(user));
    }

    [HttpGet("By-login/{login}")]
    public async Task<IActionResult> GetUserByLogin(string login, CancellationToken cancellationToken)
    {
        var user = await userServices.GetUserByLogin(login, cancellationToken);
        return Ok(mapper.Map<UserResponse>(user));
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        await userServices.Remove(userId, cancellationToken);
        return Ok();
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest updateUserRequest, CancellationToken cancellationToken)
    {
        var userModel = mapper.Map<UpdateUserModel>(updateUserRequest);
        userModel.Id = userId;
        await userServices.Update(userModel, cancellationToken);
        return Ok();
    }


}