using AutoMapper;
using Microsoft.AspNetCore.Http;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Web;

/// <summary>
/// Пофиль для маппера
/// </summary>
public class FileWebMapperProfile : Profile
{
    public FileWebMapperProfile()
    {
        CreateMap<IFormFile, AddFileModel>()
            .ForMember(x => x.FileStream, opt =>
                opt.MapFrom(x => x.OpenReadStream()))
            .ForMember(x => x.Name, opt =>
                opt.MapFrom(x => x.FileName))
            .ForMember(x => x.Size, opt =>
                opt.MapFrom(x => x.Length))
            .ForMember(x => x.ContentType, opt =>
                opt.MapFrom(x => x.ContentType));
        
        CreateMap<FormFile, AddFileModel>()
            .IncludeBase<IFormFile, AddFileModel>();
    }
}