using ecom_cassandra.Application.UseCases.Categories.Create;
using ecom_cassandra.Domain.Entities;
using Mapster;

namespace ecom_cassandra.Application.Mapping;

public class CategoriesMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, Category>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);
    }
}