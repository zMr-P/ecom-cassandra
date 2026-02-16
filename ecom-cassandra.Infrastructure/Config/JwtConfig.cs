namespace ecom_cassandra.Infrastructure.Config;

public class JwtConfig
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public int TokenExpirationMinutes { get; init; }
}