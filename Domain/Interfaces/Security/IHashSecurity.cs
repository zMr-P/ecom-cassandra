namespace ecom_cassandra.Domain.Interfaces.Security;

public interface IHashSecurity
{
    Task<string> HashWordAsync(string text, CancellationToken ct);
    Task<bool> VerifyHashAsync(string text, string hash, CancellationToken ct);
}