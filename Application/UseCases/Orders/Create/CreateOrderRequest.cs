using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Orders.Create;

public class CreateOrderRequest : IRequest<Result>
{
    public Guid UserId { get; set; }
    public CreateOrderAddressRequest ShippingAddress { get; set; } = new();
    public List<CreateOrderItemRequest> Items { get; set; } = [];
}