using ecom_cassandra.Domain.Enums;
using MediatR;
using MrP.FluentResult.Artifacts;

namespace ecom_cassandra.Application.UseCases.Users.UpdateRole;

public class UpdateRoleRequest : IRequest<Result>
{
    public Guid UserId { get; set; }
    public UserRoles Role { get; set; }
}