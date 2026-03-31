using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Entities;
using ecom_cassandra.Domain.Interfaces.Repositories;
using ecom_cassandra.Domain.Interfaces.Security;
using Mapster;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Users.Create;

public class CreateUserHandler(IUserRepository repository, IHashSecurity hashSecurity)
    : IRequestHandler<CreateUserRequest, Result>
{
    private readonly IUserRepository _userRepository = repository;
    private readonly IHashSecurity _hashSecurity = hashSecurity;

    public async Task<Result> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userExists = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (userExists is not null)
                return new Result(false)
                    .AddErrorMessage(ErrorMessage.UserAlreadyExists);

            var userToCreate = request.Adapt<User>();
            userToCreate.PasswordHash = await _hashSecurity.HashWordAsync(request.Password, cancellationToken);

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