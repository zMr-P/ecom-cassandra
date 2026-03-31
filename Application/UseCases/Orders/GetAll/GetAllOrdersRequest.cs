using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Orders.GetAll;

public class GetAllOrdersRequest : IRequest<Result<List<GetAllOrdersResponse>>>
{
    
}