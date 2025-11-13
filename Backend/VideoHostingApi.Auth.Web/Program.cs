using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.Auth.Web.Extensions;
using VideoHostingApi.Auth.Web.Middlewares;

namespace VideoHostingApi.Auth.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddDbContext<AuthContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("AuthDbConnection")));

        builder.Services.AddServices();
        builder.Services.RegisterAutoMapper();
        builder.Services.ConfigureAuth(builder.Configuration);
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Заголовок авторизации JWT с использованием схемы Bearer. \r\n\r\n Введите ваш токен в текстовое поле ниже.\r\n\r\nПример: \"eyJhbGciOiJI**********Eomy5nEqws\"",
                });

            options.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                            { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme, },
                        Scheme = "oauth2",
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    Array.Empty<string>()
                }
            });

            var dir = new DirectoryInfo(AppContext.BaseDirectory);
            foreach (var fi in dir.EnumerateFiles("*.xml"))
            {
                var doc = XDocument.Load(fi.FullName);
                options.IncludeXmlComments(() => new(doc.CreateReader()), true);
            }
        });

        //builder.WebHost.UseUrls("http://0.0.0.0:8080");


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AuthContext>();
            db.Database.Migrate();
        }

        app.UseMiddleware<ExceptionMiddleware>();

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