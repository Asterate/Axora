using System.Security.Claims;

namespace App.Modules.Identity.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(IEnumerable<Claim> claims, DateTime expiresAt);
    bool ValidateToken(string token);
}