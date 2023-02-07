using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products_Catalog.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Products_Catalog.Services;

public class JwtService : ITokenService
{
    private readonly JwtSettings _settings;

    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public SignInResponse CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddHours(_settings.ExpirationHours);
        var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);
        var tokenHandler = new JwtSecurityTokenHandler();
        return new SignInResponse
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration,
        };
    }

    private Claim[] CreateClaims(User user) =>
        new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _settings.Subject!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
        };

    private SigningCredentials CreateSigningCredentials() =>
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key!)), SecurityAlgorithms.HmacSha256);

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
        new(_settings.Issuer, _settings.Audience, claims, expires: expiration, signingCredentials: credentials);
}
