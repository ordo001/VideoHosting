using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace VideoHostingApi.Gateway.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        builder.Services.AddOcelot();
        
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

       // app.UseHttpsRedirection();

        app.UseAuthorization();
        app.MapControllers();
        
        await app.UseOcelot();
        await app.RunAsync();
    }
}