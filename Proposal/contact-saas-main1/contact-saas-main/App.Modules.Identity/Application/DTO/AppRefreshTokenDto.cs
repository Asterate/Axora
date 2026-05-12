public class AppRefreshTokenListResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class AppRefreshTokenResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreateAppRefreshTokenRequest
{
    public Guid Id { get; set; }
    public DateTime Expiration { get; set; } = DateTime.UtcNow.AddDays(7);
    public string RefreshToken { get; set; } = Guid.NewGuid().ToString();
    public Guid UserId { get; set; }

}

public class UpdateAppRefreshTokenRequest
{
    public Guid Id { get; set; }
}