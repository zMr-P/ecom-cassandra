using ecom_cassandra.Domain.Interfaces.Security;
using Hasher = BCrypt.Net.BCrypt;

namespace ecom_cassandra.Infrastructure.Security;

public class HashSecurity : IHashSecurity
{
    public Task<string> HashWordAsync(string text, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var hash = Hasher.HashPassword(text);
        return Task.FromResult(hash);
    }

    public Task<bool> VerifyHashAsync(string text, string hash, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var isValid = Hasher.Verify(text, hash);
        return Task.FromResult(isValid);
    }
}