using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Repositories;
using VideoHostingApi.Auth.Services;
using VideoHostingApi.Auth.Services.Contracts;
using VideoHostingApi.Auth.Services.Contracts.Models;
using VideoHostingApi.Common.Web;

namespace VideoHostingApi.Auth.Web.Extensions;

public static class DependencyInjection
{
    /// <summary>
    /// Конфигурация зависимостей
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.RegisterAssemblyInterfacesAssignableTo<IAuthRepositoryAnchor>(ServiceLifetime.Scoped);
        //services.RegisterAssemblyInterfacesAssignableTo<S>();
        
        return services;    
    }
    
    /// <summary>
    /// Конфигурация авторизации
    /// </summary>
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("JwtGeneratorOptions").Get<TokenGeneratorOptions>()!;
        services.AddSingleton(settings);
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
            };
        });
        return services;
    }
}