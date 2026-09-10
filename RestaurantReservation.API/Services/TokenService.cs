using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantReservation.API.Services;

public interface ITokenService
{
    /// <summary>Issues a signed bearer token for the given user.</summary>
    (string Token, DateTime ExpiresAtUtc) GenerateToken(string username);

    /// <summary>Checks the demo credentials configured under the <c>Auth</c> section.</summary>
    bool AreCredentialsValid(string username, string password);
}

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiresAtUtc) GenerateToken(string username)
    {
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key is missing.");

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        var expiresAtUtc = DateTime.UtcNow.AddHours(
            _configuration.GetValue<int?>("Jwt:ExpiryHours") ?? 1);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAtUtc);
    }

    public bool AreCredentialsValid(string username, string password)
    {
        var expectedUsername = _configuration["Auth:Username"];
        var expectedPassword = _configuration["Auth:Password"];

        if (string.IsNullOrEmpty(expectedUsername) || string.IsNullOrEmpty(expectedPassword))
        {
            throw new InvalidOperationException(
                "Demo credentials are not configured. Set Auth:Username and Auth:Password.");
        }

        // Fixed-time comparison keeps the check from leaking the expected values.
        return CryptographicOperations.FixedTimeEquals(
                   Encoding.UTF8.GetBytes(username), Encoding.UTF8.GetBytes(expectedUsername))
               && CryptographicOperations.FixedTimeEquals(
                   Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(expectedPassword));
    }
}
