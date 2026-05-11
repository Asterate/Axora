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
}

public class UpdateAppRefreshTokenRequest
{
    public Guid Id { get; set; }
}