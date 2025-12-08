using AutoMapper;
using VideoHostingApi.VideoHandler.Services.Contracts.Models;

namespace VideoHostingApi.VideoHandler.Services;

/// <summary>
/// Профиль для автомаппера сервисного слоя воркера обработки видео
/// </summary>
public class VideoHandlerServiceMapperProfile : Profile
{
    /// <summary>
    /// ctor
    /// </summary>
    public VideoHandlerServiceMapperProfile()
    {
        CreateMap<VideoProcessingMessage, VideoProcessingModel>().ReverseMap();
    }
}