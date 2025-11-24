using VideoHostingApi.Common.Repositories.Contracts;
using VideoHostingApi.FileService.Entities;

namespace VideoHostingApi.FileService.Repositories.Contracts;

public interface IVideoFileRepository : IWriteRepository<VideoFile>;