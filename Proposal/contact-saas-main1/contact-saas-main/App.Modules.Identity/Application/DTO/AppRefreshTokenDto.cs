namespace App.Modules.Identity.Application.DTO;

public class AppRefreshTokenResponse
{
    public Guid Id { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string RefreshToken { get; set; } = default!;
    public Guid UserId { get; set; }
}

public class CreateAppRefreshTokenRequest
{
    public Guid UserId { get; set; }
    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime? ExpiresAt { get; set; }
}

