using Cassandra;
using Cassandra.Mapping;
using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Infrastructure.Mappings;

public static class CassandraMapping
{
    public static MappingConfiguration GetMappings()
    {
        var mappingConfig = new MappingConfiguration();

        mappingConfig.Define(
            new Map<User>()
                .TableName("users")
                .PartitionKey(u => u.Id)
                .Column(u => u.Name, cm => cm.WithName("name"))
                .Column(u => u.Email, cm => cm.WithName("email"))
                .Column(u => u.PasswordHash, cm => cm.WithName("password_hash"))
                .Column(u => u.CreatedAt, cm => cm.WithName("created_at"))
                .Column(u => u.UpdatedAt, cm => cm.WithName("updated_at"))
                .Column(u => u.Addresses, cm => cm.WithName("addresses"))
        );

        return mappingConfig;
    }
}