using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Web;

public class FileWebMapperProfile : Profile
{
    public FileWebMapperProfile()
    {
        CreateMap<IFormFile, AddFileModel>()
            .ForMember(x => x.FileStream, opt =>
                opt.MapFrom(x => x.OpenReadStream()))
            .ForMember(x => x.FileName, opt =>
                opt.MapFrom(x => x.FileName))
            .ForMember(x => x.ContentType, opt =>
                opt.MapFrom(x => x.ContentType));
    }
}