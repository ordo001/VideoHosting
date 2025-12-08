using AutoMapper;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Профили маппера для сервисного слоя
/// </summary>
public class FileServiceMapperProfile : Profile
{
    public FileServiceMapperProfile()
    {
        CreateMap<VideoFile, FileMetadata>(MemberList.Destination).ReverseMap();
    }
}