using Cassandra;
using ecom_cassandra.Domain.Entities;

namespace ecom_cassandra.Infrastructure.Mappings;

public static class CassandraMappingUdts
{
    public static void RegisterUdts(ISession session)
    {
        session.UserDefinedTypes.Define(
            UdtMap.For<Address>("address")
                .Map(a => a.Id, "id")
                .Map(a => a.Street, "street")
                .Map(a => a.Number, "number")
                .Map(a => a.City, "city")
                .Map(a => a.State, "state")
                .Map(a => a.ZipCode, "zipcode")
                .Map(a => a.Country, "country")
        );
    }
}