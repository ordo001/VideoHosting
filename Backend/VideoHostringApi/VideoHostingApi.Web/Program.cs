using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using VideoHostingApi.Auth.Web.Extensions;
using VideoHostingApi.FileService.Web.Extentions;
using VideoHostingApi.Web.Middlewares;
using Microsoft.EntityFrameworkCore;
using VideoHostingApi.Auth.Context;
using VideoHostingApi.FileService.Context;
using VideoHostingApi.Web.Extentions;
using VideoHostingApi.Common.Messaging;


namespace VideoHostingApi.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var authConnectionString = builder.Configuration.GetConnectionString("AuthDbConnection");
        var fileConnectionString = builder.Configuration.GetConnectionString("FileDbConnection");

        builder.Services.AddDbContext<AuthContext>(x => x.UseNpgsql(authConnectionString));
        builder.Services.AddDbContext<FileServiceContext>(x => x.UseNpgsql(fileConnectionString));
        
        builder.Services.ConfigureFileService(builder.Configuration);
        builder.Services.ConfigureAuthService();
        builder.Services.RegisterAutoMapper();
        builder.Services.ConfigureAuth(builder.Configuration);
        await builder.Services.AddRabbitMq(builder.Configuration);
        
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

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        await app.RunAsync();
    }
}