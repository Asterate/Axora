using System.ComponentModel.DataAnnotations;

namespace App.Modules.Identity.Application.DTO;

public class RegisterInstituteDto
{
    [StringLength(128, MinimumLength = 2)]
    [Required]
    public string InstituteName { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 2)]
    [Required]
    public string InstituteCountry { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 5)]
    [Required]
    public string InstituteAddress { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 5)]
    [Required]
    public string InstitutePhoneNumber { get; set; } = default!;
    
    [Required]
    public Guid InstituteTypeId { get; set; }
}