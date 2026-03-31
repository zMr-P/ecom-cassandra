using ecom_cassandra.Infrastructure.Config;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.DependencyInjection;

public static class RabbitMq
{
    public static IServiceCollection SetRabbitMqConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddMassTransit(x =>
        {
            var rabbitSection = config.GetSection("ExternalServices:RabbitMq");
            services.Configure<RabbitMqConfig>(rabbitSection);

            var settings = rabbitSection.Get<RabbitMqConfig>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{settings?.Host}:{settings?.Port}/"), h  =>
                {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });
            });
        });

        services.AddMassTransitHostedService();
        return services;
    }
}