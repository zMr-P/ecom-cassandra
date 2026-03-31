using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Categories.Create;

public class CreateCategoryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryRequest, Result>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category, cancellationToken);

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