using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Users.Create;

public class CreateUserHandler(IUserRepository repository) : IRequestHandler<CreateUserRequest, Result>
{
    private readonly IUserRepository _userRepository = repository;

    public async Task<Result> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userToCreate = request.Adapt<User>();
            await _userRepository.CreateAsync(userToCreate, cancellationToken);
            
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