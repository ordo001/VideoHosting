using AutoMapper;

namespace VideoHostingApi.Web.Extentions;

public static class AddConfigure
{
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}