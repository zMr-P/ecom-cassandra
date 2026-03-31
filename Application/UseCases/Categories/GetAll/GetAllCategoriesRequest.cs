using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Categories.GetAll;

public class GetAllCategoriesRequest : IRequest<Result<List<GetAllCategoriesResponse>>>;