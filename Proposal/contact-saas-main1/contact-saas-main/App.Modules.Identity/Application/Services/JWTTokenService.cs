using System.Security.Claims;
using App.Modules.Identity.Application.Interfaces;

namespace App.Modules.Identity.Application.Services;

public class JwtTokenService : IJwtTokenService
{
    public string GenerateToken(IEnumerable<Claim> claims, DateTime expiresAt)
    {
        throw new NotImplementedException();
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}