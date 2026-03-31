using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Categories.Create;

public class CreateCategoryRequest : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}