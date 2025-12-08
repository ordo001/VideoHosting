using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.FileService.Context;
using VideoHostingApi.FileService.Repositories.Contracts;

namespace VideoHostingApi.FileService.Repositories;

public class VideoFileRepository(FileServiceContext context) : WriteRepositoryBase<VideoFile>(context), IVideoFileRepository, IFileRepositoryAnchor
{
    
}