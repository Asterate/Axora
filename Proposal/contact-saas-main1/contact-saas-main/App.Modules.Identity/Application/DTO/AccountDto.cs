using System.Security.Claims;
using App.Domain.Identity;

namespace App.Modules.Identity.Application.DTO;

public class LoginResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public ClaimsPrincipal? ClaimsPrincipal { get; set; }
    public string? RefreshToken { get; set; }
}
public class RegisterUserResult
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public AppUser? User { get; set; }
    public ClaimsPrincipal? ClaimsPrincipal { get; set; }
    public string? RefreshToken { get; set; }
}