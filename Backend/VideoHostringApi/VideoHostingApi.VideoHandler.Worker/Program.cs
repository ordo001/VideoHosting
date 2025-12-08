using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Messaging;
using VideoHostingApi.VideoHandler.Worker.Extentions;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Worker;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        var fileConnectionString = builder.Configuration.GetConnectionString("FileDbConnection");
        builder.Services.AddDbContext<VideoHandlerContext>(x => x.UseNpgsql(fileConnectionString));
        
        builder.Services.ConfigureFileService();
        await builder.Services.AddRabbitMq(builder.Configuration);
        builder.Services.AddHostedService<VideoHostingApi.VideoHandler.Worker.Worker>();

        var host = builder.Build();
        await host.RunAsync();
    }
}