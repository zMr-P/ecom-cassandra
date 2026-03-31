using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;

public class GetByOrderRequest(Guid orderId) : IRequest<Result<GetByOrderResponse>>
{
    public Guid OrderId { get; set; } = orderId;
}