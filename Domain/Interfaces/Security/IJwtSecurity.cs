using ecom_cassandra.Domain.Enums;

namespace ecom_cassandra.Domain.Interfaces.Security;

public interface IJwtSecurity
{
    Task<string> GenerateTokenAsync(Guid userId, string userRole, CancellationToken ct);
}