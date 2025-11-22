using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Context;
using VideoHostingApi.FileService.Entities.Configuration;

namespace VideoHostingApi.FileService.Context;

public class FileServiceContext(DbContextOptions<FileServiceContext> options) : DbContextBase<IFileServiceEntityConfiguration>(options)
{
}