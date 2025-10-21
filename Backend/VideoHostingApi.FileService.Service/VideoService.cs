using VideoHosting.FileSerivce.Entities;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Service;

public class VideoService(IMinioRepository<Video> minioRepository) : IVideoService
{
    public Task UploadFile(string name, Stream stream)
    {
        throw new NotImplementedException();
    }

    public Task<string> DownloadFile(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetListObjects()
    {
        throw new NotImplementedException();
    }

    public Task DeleteFile(string name)
    {
        throw new NotImplementedException();
    }
}