using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Owner.ViewModels;

public class CreateTeacherViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
    
    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string FirstName { get; set; } = default!;
    
    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string LastName { get; set; } = default!;
    
    [Required]
    [StringLength(50, MinimumLength = 8)]
    [Phone]
    public string TeacherPhone { get; set; } = default!;
    
    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string TeacherAddress { get; set; } = default!;
    
    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string TeacherNativeLanguage { get; set; } = default!;
    
    [Required]
    [StringLength(128, MinimumLength = 3)]
    public string TeacherNationality { get; set; } = default!;
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string TeacherGender { get; set; } = default!;
}
