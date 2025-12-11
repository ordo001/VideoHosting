using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.FileService.Repositories;

namespace VideoHostingApi.Common.Repositories;

public class VideoFileRepository(DbContext context) : WriteRepositoryBase<VideoFile>(context), IVideoFileRepository, IFileRepositoryAnchor
{
    
}