using ecom_cassandra.Application;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.DependencyInjection;

public static class Mapster
{
    public static IServiceCollection SetMapsterConfig(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(ApplicationMarker).Assembly);

        services.AddSingleton(config);
        
        return services;
    }
}