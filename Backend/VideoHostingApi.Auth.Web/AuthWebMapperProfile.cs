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
    }
}