using App.Domain.Identity;
using App.Shared.Domain;

namespace App.Modules.Identity.Domain;

public class AppRefreshToken : BaseEntity
{
    public string TokenHash { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? LastUsedAt { get; set; }

    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? RevocationReason { get; set; }

    public string? ReplacedByTokenHash { get; set; }

    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
}

