using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Users.Create;

public class CreateUserRequest : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}