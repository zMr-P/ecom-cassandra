using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.DependencyInjection;

public static class MediatR
{
    public static IServiceCollection SetMediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Application.ApplicationMarker).Assembly));

        return services;
    }
}