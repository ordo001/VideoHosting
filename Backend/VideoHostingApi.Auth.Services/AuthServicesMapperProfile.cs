
using AutoMapper;
using VideoHostingApi.Auth.Entities;
using VideoHostingApi.Auth.Services.Contracts.Models;

namespace VideoHostingApi.Auth.Services;

/// <summary>
/// Описание маппингов
/// </summary>
public class AuthServicesMapperProfile : Profile
{
    /// <summary>
    /// Инициализацию профиля маппера
    /// </summary>
    public AuthServicesMapperProfile()
    {
        CreateMap<User,UserModel>(MemberList.Destination).ReverseMap();
        CreateMap<AddUserModel,User>(MemberList.Destination).ReverseMap();
    }
}