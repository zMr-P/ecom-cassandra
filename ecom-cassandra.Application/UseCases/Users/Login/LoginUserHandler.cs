using ecom_cassandra.CrossCutting.Constants;
using ecom_cassandra.Domain.Interfaces.Repositories;
using ecom_cassandra.Domain.Interfaces.Security;
using MediatR;
using MrP.FluentResult.Artifacts;
using MrP.FluentResult.FluentExtensions;

namespace ecom_cassandra.Application.UseCases.Users.Login;

public class LoginUserHandler(IUserRepository userRepository, IHashSecurity hashSecurity, IJwtSecurity jwtSecurity)
    : IRequestHandler<LoginUserRequest, Result<string>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHashSecurity _hashSecurity = hashSecurity;
    private readonly IJwtSecurity _jwtSecurity = jwtSecurity;

    public async Task<Result<string>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userData = await _userRepository.GetByEmailAsync(request.UserEmail, cancellationToken);
            if (userData == null)
                return new Result<string>(false)
                    .AddErrorMessage(ErrorMessage.InvalidEmail);

            var isPasswordValid = await _hashSecurity
                .VerifyHashAsync(request.UserPassword, userData.PasswordHash, cancellationToken);

            if (!isPasswordValid)
                return new Result<string>(false)
                    .AddErrorMessage(ErrorMessage.InvalidPassword);

            var token = await _jwtSecurity.GenerateTokenAsync(userData.Id, userData.Role, cancellationToken);

            return new Result<string>(token, true);
        }
        catch (Exception ex)
        {
            return new Result<string>(false)
                .AddErrorMessage(ex.Message);
        }
    }
}