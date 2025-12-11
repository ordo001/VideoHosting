using AutoMapper;
using VideoHostingApi.Common.Repositories.Contracts.Models;
using VideoHostingApi.FileService.Service.Contracts.Models;

namespace VideoHostingApi.FileService.Service;

/// <summary>
/// Профили для маппера
/// </summary>
public class FileServiceProfile : Profile
{
    public FileServiceProfile()
    {
        CreateMap<FileDbModel, HlsModel>();
    }
}