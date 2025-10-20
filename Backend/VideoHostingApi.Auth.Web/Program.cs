using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Auth.Context;

namespace VideoHostingApi.Auth.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        

        builder.Services.AddDbContext<AuthContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDbConnection")));
        
        

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
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