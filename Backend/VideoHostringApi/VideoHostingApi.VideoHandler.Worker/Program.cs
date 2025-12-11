using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Common.Messaging;
using VideoHostingApi.VideoHandler.Worker.Extentions;
using VideoHostringApi.VideoHandler.Context;

namespace VideoHostingApi.VideoHandler.Worker;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        var fileConnectionString = builder.Configuration.GetConnectionString("FileDbConnection");
        builder.Services.AddDbContext<DbContext,VideoHandlerContext>(x => x.UseNpgsql(fileConnectionString));
        
        builder.Services.ConfigureVideoHandler(builder.Configuration);
        builder.Services.RegisterAutoMapper();
        await builder.Services.AddRabbitMq(builder.Configuration);
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        await host.RunAsync();
    }
}