using AutoMapper;
using Microsoft.AspNetCore.Http;
using VideoHostingApi.FileService.Service.Contracts.Models;
using VideoHostingApi.FileService.Web.Models;

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
            .ForMember(x => x.ContentType, opt =>
                opt.MapFrom(x => x.ContentType));
        
        CreateMap<FormFile, AddFileModel>()
            .IncludeBase<IFormFile, AddFileModel>();

        CreateMap<UploadVideoRequest, UploadVideoModel>();
        CreateMap<UploadFileVideoRequest, AddFileModel>();
    }
}