using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
    public static IServiceCollection ConfigureAuthService(this IServiceCollection services)
    {
        services.AddSingleton<Profile, AuthWebMapperProfile>();
        services.AddSingleton<Profile, AuthServicesMapperProfile>();
        
        services.RegisterAssemblyInterfacesAssignableTo<IAuthRepositoryAnchor>(ServiceLifetime.Scoped);
        services.RegisterAssemblyInterfacesAssignableTo<IAuthServiceAnchor>(ServiceLifetime.Scoped);
        
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