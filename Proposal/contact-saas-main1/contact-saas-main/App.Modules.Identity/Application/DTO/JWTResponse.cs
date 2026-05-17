namespace App.Modules.Identity.Application.DTO;

public class JwtResponse
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}