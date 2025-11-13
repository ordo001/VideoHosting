using System.Text;
using AutoMapper;
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
        //services.AddSingleton<TokenGeneratorOptions>();
        services.AddSingleton<Profile, AuthWebMapperProfile>();
        services.AddSingleton<Profile, AuthServicesMapperProfile>();
        
        services.RegisterAssemblyInterfacesAssignableTo<IAuthRepositoryAnchor>(ServiceLifetime.Scoped);
        services.RegisterAssemblyInterfacesAssignableTo<IAuthServiceAnchor>(ServiceLifetime.Scoped);
        
        return services;    
    }

    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(provider =>
        {
            var profiles = provider.GetServices<Profile>().ToList();
        
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return config.CreateMapper();
        });
        
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