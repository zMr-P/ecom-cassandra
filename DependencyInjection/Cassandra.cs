using Cassandra;
using Cassandra.Mapping;
using ecom_cassandra.Infrastructure.Config;
using ecom_cassandra.Infrastructure.Mappings;
using ecom_cassandra.Infrastructure.Session;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ecom_cassandra.DependencyInjection;

public static class Cassandra
{
    public static IServiceCollection SetCassandraConfig(this IServiceCollection services, IConfiguration config)
    {
        var cassandraSection = config.GetSection("ExternalServices:Cassandra");
        services.Configure<CassandraConfig>(cassandraSection);

        services.AddSingleton<CassandraSession>();
        services.AddSingleton<ISession>(sp =>
        {
            var cassandraSession = sp.GetRequiredService<CassandraSession>();
            return cassandraSession.GetSession();
        });

        services.AddSingleton<IMapper>(sp =>
        {
            var session = sp.GetRequiredService<ISession>();
            var mappingConfig = CassandraMapping.GetMappings();
            CassandraMappingUdts.RegisterUdts(session);
            return new Mapper(session, mappingConfig);
        });

        return services;
    }
}