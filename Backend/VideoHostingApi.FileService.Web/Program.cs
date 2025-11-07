using Microsoft.EntityFrameworkCore;
using Minio;
using VideoHostingApi.FileService.Entities;
using VideoHostingApi.FileService.Context;
using VideoHostingApi.FileService.Repositories;
using VideoHostingApi.FileService.Repositories.Contracts;
using VideoHostingApi.FileService.Service;
using VideoHostingApi.FileService.Service.Contracts;

namespace VideoHostingApi.FileService.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<FileServiceContext>(opt => 
            opt.UseNpgsql(builder.Configuration.GetConnectionString("FileDbConnection")));

        builder.Services.AddScoped<IVideoService, VideoService>();

        var minioClient = new MinioClient()
            .WithEndpoint(builder.Configuration["Minio:PublicEndpoint"])
            .WithCredentials(builder.Configuration["Minio:AccessKey"], builder.Configuration["Minio:SecretKey"])
            .Build();
        
        builder.Services.AddSingleton(minioClient);

        builder.Services.AddScoped<IObjectStorageRepository<Video>>(sp =>
            new MinioRepository<Video>(minioClient, builder.Configuration["MinioBuckets:videos"]!));
        
        builder.Services.AddScoped<IObjectStorageRepository<Image>>(sp =>
            new MinioRepository<Image>(minioClient, builder.Configuration["MinioBuckets:images"]!));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //builder.WebHost.UseUrls("http://0.0.0.0:8080"); 


        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
       //app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}