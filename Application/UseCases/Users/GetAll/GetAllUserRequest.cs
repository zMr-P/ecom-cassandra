using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Users.GetAll;

public class GetAllUserRequest : IRequest<Result<List<GetAllUserResponse>>>
{
}