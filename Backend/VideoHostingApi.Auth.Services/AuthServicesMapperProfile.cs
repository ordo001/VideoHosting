
using AutoMapper;
using VideoHosting.Auth.Repositories.Contracts.Models;
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
        CreateMap<UserDbModel,UserModel>(MemberList.Destination).ReverseMap();
        CreateMap<UserDbModel, User>(MemberList.Destination)
            .ForMember(x => x.RoleId, opt =>
                opt.MapFrom(x => x.Role!.Id));
        CreateMap<AddUserModel,User>(MemberList.Destination).ReverseMap();
        CreateMap<UserDbModel, GenerateTokenModel>(MemberList.Destination);
        
        CreateMap<RoleDbModel, Role>(MemberList.Destination).ReverseMap();
    }
}