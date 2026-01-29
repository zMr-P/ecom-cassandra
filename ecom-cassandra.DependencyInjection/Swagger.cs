using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace ecom_cassandra.DependencyInjection;

public static class Swagger
{
    public static IServiceCollection SetSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ecom-cassandra ecommerce API",
                Version = "v1",
                Description = "API documentation for ecom-cassandra"
            });
            
            c.EnableAnnotations();
        });

        return services;
    }
}