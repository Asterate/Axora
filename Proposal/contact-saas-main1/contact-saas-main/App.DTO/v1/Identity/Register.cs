using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1.Identity;

public enum InstituteSelectionType
{
    SelectExisting = 0,
    CreateNew = 1
}

public class Register
{
    [MaxLength(256)]
    [EmailAddress]
    [Required]
    public string Email { get; set; } = default!;
        
    [MinLength(6)]
    [MaxLength(100)]
    [Required]
    public string Password { get; set; } = default!;
    
    /// <summary>
    /// Indicates whether user wants to select existing institute or create new one
    /// </summary>
    [Required]
    public InstituteSelectionType InstituteSelection { get; set; } = InstituteSelectionType.SelectExisting;
    
    /// <summary>
    /// Institute ID to join (when InstituteSelection is SelectExisting)
    /// </summary>
    public Guid? InstituteId { get; set; }
    
    /// <summary>
    /// New institute details (when InstituteSelection is CreateNew)
    /// </summary>
    public RegisterInstituteDto? NewInstitute { get; set; }
}