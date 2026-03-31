using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Orders.GetAll;

public class GetAllOrdersHandler(IOrderRepository orderRepository)
    : IRequestHandler<GetAllOrdersRequest, Result<List<GetAllOrdersResponse>>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Result<List<GetAllOrdersResponse>>> Handle(GetAllOrdersRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            var response = orders.Adapt<List<GetAllOrdersResponse>>();

            return new Result<List<GetAllOrdersResponse>>(response, true);
        }
        catch (Exception ex)
        {
            return new Result<List<GetAllOrdersResponse>>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}