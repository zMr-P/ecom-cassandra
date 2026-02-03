using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Users.GetAll;

public class GetAllUserHandler(IUserRepository repository)
    : IRequestHandler<GetAllUserRequest, Result<List<GetAllUserResponse>>>
{
    private readonly IUserRepository _repository = repository;

    public async Task<Result<List<GetAllUserResponse>>> Handle(GetAllUserRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var dataUsers = await _repository.GetAllAsync(cancellationToken);

            var response = dataUsers.Adapt<List<GetAllUserResponse>>();

            return new Result<List<GetAllUserResponse>>(response, true);
        }
        catch (Exception ex)
        {
            return new Result<List<GetAllUserResponse>>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}