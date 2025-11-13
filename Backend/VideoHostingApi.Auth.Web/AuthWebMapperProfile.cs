using AutoMapper;
using VideoHostingApi.Auth.Services.Contracts.Models;
using VideoHostingApi.Auth.Web.Models;

namespace VideoHostingApi.Auth.Web;

/// <summary>
/// Описание маппингов
/// </summary>
public class AuthWebMapperProfile : Profile
{
    public AuthWebMapperProfile()
    {
        CreateMap<AddUserRequest, AddUserModel>().ReverseMap();
        CreateMap<UserResponse, UserModel>(MemberList.Destination).ReverseMap();
        CreateMap<UpdateUserRequest, UpdateUserModel>(MemberList.Destination).ReverseMap();

        CreateMap<LoginRequest, LoginModel>(MemberList.Destination).ReverseMap();
    }
}