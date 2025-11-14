using AutoMapper;

namespace VideoHostingApi.Web.Extentions;

public static class AddConfigure
{
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
}