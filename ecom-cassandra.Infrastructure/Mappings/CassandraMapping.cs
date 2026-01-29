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
                .TableName("Users")
                .PartitionKey(u => u.Id)
        );
        
        return mappingConfig;
    }
}