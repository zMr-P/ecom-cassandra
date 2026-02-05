using ecom_cassandra.Application.UseCases.Orders.Create;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Enums;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class OrdersMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderRequest, Order>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Status, src => OrderStatus.Pending)
            .Map(dest => dest.TotalAmount, src
                => src.Items.Sum(item => item.Quantity * item.UnitPrice))
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
            .Map(dest => dest.ShippingAddress, src => src.ShippingAddress);

        config.NewConfig<CreateOrderAddressRequest, Address>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Street, src => src.Street)
            .Map(dest => dest.City, src => src.City)
            .Map(dest => dest.State, src => src.State)
            .Map(dest => dest.Country, src => src.Country)
            .Map(dest => dest.ZipCode, src => src.ZipCode);

        config.NewConfig<CreateOrderItemRequest, OrderItem>()
            .Map(dest => dest.ProductId, src => src.ProductId)
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice);
    }
}