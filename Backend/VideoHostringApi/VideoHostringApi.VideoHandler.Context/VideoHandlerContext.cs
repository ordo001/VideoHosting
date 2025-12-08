using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Context;
using VideoHostingApi.Common.Entities.Configuration.Video;

namespace VideoHostringApi.VideoHandler.Context;

public class VideoHandlerContext(DbContextOptions<VideoHandlerContext> options) : DbContextBase<IFileServiceEntityConfiguration>(options);