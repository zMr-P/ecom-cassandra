using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Users.Login;

public class LoginUserRequest : IRequest<Result<string>>
{
    public string UserEmail { get; set; } = string.Empty;
    public string UserPassword { get; set; } = string.Empty;
}