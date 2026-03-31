using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Products.Create;

public class CreateProductHandler(IProductRepository productRepository) : IRequestHandler<CreateProductRequest, Result>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<Result> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = request.Adapt<Product>();

            await _productRepository.CreateAsync(product, cancellationToken);
            
            return new Result(true)
                .AddMessage(SuccessMessage.Created);
        }
        catch (Exception ex)
        {
            return new Result(false)
                .AddErrorMessage(ex.Message);
        }
    }
}