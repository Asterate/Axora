using System.ComponentModel.DataAnnotations;

namespace App.Modules.Identity.Application.DTO;

public class LogoutInfo
{
    [MaxLength(128)]
    [Required]
    public string RefreshToken { get; set; } = default!;
}