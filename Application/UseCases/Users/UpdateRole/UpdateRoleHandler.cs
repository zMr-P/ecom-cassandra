using ecom_cassandra.Domain.Interfaces.Repositories;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Users.UpdateRole;

public class UpdateRoleHandler(IUserRepository userRepository) : IRequestHandler<UpdateRoleRequest, Result>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        var dataUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (dataUser is null)
            return new Result(false)
                .AddErrorMessage("User not found");
        
        await _userRepository.UpdateRoleAsync(
            request.UserId,
            request.Role.ToString(),
            cancellationToken);

        return new Result(true)
            .AddMessage("Success on update user role");
    }
}