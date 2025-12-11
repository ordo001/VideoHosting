using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.FileService.Repositories;

namespace VideoHostingApi.Common.Repositories;

public class VideoRepository(DbContext context) : WriteRepositoryBase<Video>(context), IVideoRepository, IFileRepositoryAnchor
{
    public async Task<Video?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<Video>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Video?> GetByName(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //return await context.Set<Video>().FirstOrDefaultAsync(x => x.Title == name, cancellationToken);
    }

    public async Task<IEnumerable<Video>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Set<Video>().ToListAsync(cancellationToken);
    }
}