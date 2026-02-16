using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Digite apenas o token JWT."
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            }
        );

        return services;
    }
}