using System.ComponentModel.DataAnnotations;
using App.DTO.v1.Identity;

namespace WebApp.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = default!;

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = default!;
    
    [Required]
    [Display(Name = "Choose Option")]
    public InstituteSelectionType InstituteSelection { get; set; } = InstituteSelectionType.SelectExisting;
    
    [Display(Name = "Select Institute")]
    public Guid? InstituteId { get; set; }
    
    // New institute fields
    [Display(Name = "Institute Name")]
    public string? InstituteName { get; set; }
    
    [Display(Name = "Country")]
    public string? InstituteCountry { get; set; }
    
    [Display(Name = "Address")]
    public string? InstituteAddress { get; set; }
    
    [Display(Name = "Phone Number")]
    public string? InstitutePhoneNumber { get; set; }
    
    [Display(Name = "Institute Type")]
    public Guid? InstituteTypeId { get; set; }
    
    // Lists for dropdowns
    public List<LookupItem> Institutes { get; set; } = new();
    public List<LookupItem> InstituteTypes { get; set; } = new();

    public class LookupItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}