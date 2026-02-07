using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Categories.GetAll;

public class GetAllCategoriesHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetAllCategoriesRequest, Result<List<GetAllCategoriesResponse>>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<Result<List<GetAllCategoriesResponse>>> Handle(GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            var response = categories.Adapt<List<GetAllCategoriesResponse>>();

            return new Result<List<GetAllCategoriesResponse>>(response, true);
        }
        catch (Exception ex)
        {
            return new Result<List<GetAllCategoriesResponse>>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}