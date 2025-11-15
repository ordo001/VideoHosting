using AutoMapper;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Профили маппера для сервисного слоя
/// </summary>
public class FileServiceMapperProfile : Profile
{
    public FileServiceMapperProfile()
    {
        CreateMap<Video, FileDbModel>().ReverseMap();
        CreateMap<Image, FileDbModel>().ReverseMap();
    }
}