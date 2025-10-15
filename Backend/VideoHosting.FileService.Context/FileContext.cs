using Microsoft.EntityFrameworkCore;
using VideoHosting.Entities.Configuration;
using VideoHostingApi.Common.Context;

namespace VideoHosting.FileService.Context;

public class FileContext(DbContextOptions options) : DbContextBase<IFileEntityConfiguration>(options);