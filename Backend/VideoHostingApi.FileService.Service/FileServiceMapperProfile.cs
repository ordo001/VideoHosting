using AutoMapper;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts.Models;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Профили маппера для сервисного слоя
/// </summary>
public class FileServiceMapperProfile : Profile
{
    public FileServiceMapperProfile()
    {
        CreateMap<Video, FileMetadata>(MemberList.Destination).ReverseMap();
        CreateMap<Image, FileMetadata>(MemberList.Destination).ReverseMap();
    }
}