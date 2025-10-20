using VideoHosting.Auth.Repositories.Contracts;
using VideoHostingApi.Auth.Repositories;
using VideoHostingApi.Common.Web;

namespace VideoHostingApi.Auth.Web.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.RegisterAssemblyInterfacesAssignableTo<IAuthRepositoryAnchor>(ServiceLifetime.Scoped);
        //services.RegisterAssemblyInterfacesAssignableTo<S>();
        

        
        return services;    
    }
}