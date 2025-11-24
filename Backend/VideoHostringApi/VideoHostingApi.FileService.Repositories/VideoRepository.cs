using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Repositories;
using VideoHostingApi.FileService.Context;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;

namespace VideoHostingApi.FileService.Repositories;

public class VideoRepository(FileServiceContext context) : WriteRepositoryBase<Video>(context), IVideoRepository, IFileRepositoryAnchor
{
    public async Task<Video?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<Video>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Video?> GetByName(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //return await context.Set<Video>().FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Video>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<Video>().ToListAsync(cancellationToken);
    }
}