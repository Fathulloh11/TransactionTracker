using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyExpenseTracker.Helpers;

public static class JwtAuthHelper
{
    private static readonly string Secret = "MySuperSecretKey1234567777777777!";

    public static string GenerateToken(string userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}