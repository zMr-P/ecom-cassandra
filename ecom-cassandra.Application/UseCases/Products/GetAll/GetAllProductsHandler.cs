using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Products.GetAll;

public class GetAllProductsHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsRequest, Result<List<GetAllProductsResponse>>>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result<List<GetAllProductsResponse>>> Handle(GetAllProductsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetAllProducts(cancellationToken);

            var response = products.Adapt<List<GetAllProductsResponse>>();

            return new Result<List<GetAllProductsResponse>>(response, true);
        }
        catch (Exception ex)
        {
            return new Result<List<GetAllProductsResponse>>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}