using Minio;
using Minio.DataModel.Args;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Repositories.Contracts.Models;

namespace VideoHostingApi.FileService.Repositories;

/// <summary>
/// Репозиторий для доступа к S3 хранилищу Minio
/// </summary>
public class MinioRepository<T>(IMinioClient minioClient, string bucketName) : IMinioRepository<T> where T : class 
{
    
    public async Task<string> GetPresignedUploadUrl(string name)
    {
        var args = new PresignedPutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(name)
            .WithExpiry(3600); // TODO: Заменить на константу
        
        var url = await minioClient.PresignedPutObjectAsync(args);
        return url;
    }

    public async Task<string> GetPresignedDownloadUrl(string name)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(name)
            .WithExpiry(3600); // TODO: Заменить на константу
        
        var url = await minioClient.PresignedGetObjectAsync(args);
        
        return url;
    }

    public async Task UploadFile(string name, Stream fileStream, string contentType, CancellationToken cancellationToken)
    {
        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(name)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);
        
        await minioClient.PutObjectAsync(args, cancellationToken);
    }

    public async Task<FileDbModel> DownloadFile(string name, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        var args = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithCallbackStream(memoryStream =>
            {
                 memoryStream.CopyTo(stream);
            })
            .WithObject(name);
        
        await minioClient.GetObjectAsync(args, cancellationToken);
        
        var statObjectArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(name);
        
        var stat = await minioClient.StatObjectAsync(statObjectArgs, cancellationToken);
        
        stream.Position = 0;
        return new FileDbModel
        {
            FileStream = stream,
            ContentType = stat.ContentType,
            FileName = stat.ObjectName
        };
    }

    public async Task<List<string>> GetListObjects(CancellationToken cancellationToken)
    {
        var fileNames = new List<string>();

        var observable = minioClient.ListObjectsEnumAsync(
            new ListObjectsArgs().WithBucket(bucketName).WithRecursive(true), cancellationToken
        );

        await foreach (var item in observable)
        {
            fileNames.Add(item.Key);
        }

        return fileNames;
    }

    public async Task DeleteFile(string name, CancellationToken cancellationToken)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(name);

        await minioClient.RemoveObjectAsync(args, cancellationToken);
    }

    public async Task EnsureBucketExistsAsync(CancellationToken cancellationToken)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);
        
        var result = await minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
        if (!result)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(bucketName);
            await minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }
    }
}