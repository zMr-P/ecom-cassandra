using ecom_cassandra.Application.UseCases.Products.Create;
using ecom_cassandra.Domain.Entities;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class ProductsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, Product>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.StockQuantity, src => src.StockQuantity)
            .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);
    }
}