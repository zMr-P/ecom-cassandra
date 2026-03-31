using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.OrderItems.GetByOrder;

public class GetByOrderHandler(IOrderItemRepository orderItemRepository)
    : IRequestHandler<GetByOrderRequest, Result<GetByOrderResponse>>
{
    private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;

    public async Task<Result<GetByOrderResponse>> Handle(GetByOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);

            var response = orderItems.Adapt<GetByOrderResponse>();

            return new Result<GetByOrderResponse>(response, true);
        }
        catch (Exception ex)
        {
            return new Result<GetByOrderResponse>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}