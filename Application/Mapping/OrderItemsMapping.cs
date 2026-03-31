using ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;
using ecom_cassandra.Domain.Entities;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class OrderItemsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<List<OrderItem>, GetByOrderResponse>()
            .Map(dest => dest.OrderItems, src => src);
    }
}