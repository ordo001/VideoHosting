using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Minio;
using VideoHosting.FileSerivce.Entities;
using VideoHosting.FileService.Context;
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

        /*builder.Services.AddDbContext<FileContext>(opt => 
            opt.UseNpgsql(builder.Configuration.GetConnectionString("FileDbConnection")));*/

        builder.Services.AddScoped<IVideoService, VideoService>();

        var minioClient = new MinioClient()
            .WithEndpoint(builder.Configuration["Minio:Endpoint"])
            .WithCredentials(builder.Configuration["Minio:AccessKey"], builder.Configuration["Minio:SecretKey"])
            .Build();
        
        builder.Services.AddSingleton(minioClient);

        builder.Services.AddScoped<IMinioRepository<Video>>(sp =>
            new MinioRepository<Video>(minioClient, builder.Configuration["MinioBuckets:videos"]!));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.WebHost.UseUrls("http://0.0.0.0:8081"); 


        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}