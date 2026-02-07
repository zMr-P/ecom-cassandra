using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Products.GetAll;

public class GetAllProductsRequest : IRequest<Result<List<GetAllProductsResponse>>>
{
    
}