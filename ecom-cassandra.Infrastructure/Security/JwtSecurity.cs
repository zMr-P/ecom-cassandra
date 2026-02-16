using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ecom_cassandra.Domain.Enums;
using ecom_cassandra.Domain.Interfaces.Security;
using ecom_cassandra.Infrastructure.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ecom_cassandra.Infrastructure.Security;

public class JwtSecurity(IOptions<JwtConfig> config) : IJwtSecurity
{
    private readonly JwtConfig _config = config.Value;

    public Task<string> GenerateTokenAsync(Guid userId, string userRole, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var id = userId.ToString();
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userRole)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.TokenExpirationMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Task.FromResult(tokenString);
    }
}