using App.Modules.Identity.Domain;
using App.Shared.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IBaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<AppRefreshToken>? RefreshTokens { get; set; }
    public DateTimeOffset? LastSeen { get; set; }
}