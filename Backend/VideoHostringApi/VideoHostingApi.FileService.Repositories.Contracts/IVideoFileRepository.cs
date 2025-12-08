using VideoHostingApi.Common.Entities.Video;
using VideoHostingApi.Common.Repositories.Contracts;

namespace VideoHostingApi.FileService.Repositories.Contracts;

public interface IVideoFileRepository : IWriteRepository<VideoFile>;