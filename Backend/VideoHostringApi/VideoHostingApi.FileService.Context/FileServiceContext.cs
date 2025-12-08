using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Context;
using VideoHostingApi.Common.Entities.Configuration.Video;

namespace VideoHostingApi.FileService.Context;

public class FileServiceContext(DbContextOptions<FileServiceContext> options) : DbContextBase<IFileServiceEntityConfiguration>(options)
{
}