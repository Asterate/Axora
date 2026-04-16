using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class InstituteChoiceViewModel
{
    [Required]
    [Display(Name = "Choose Option")]
    public int InstituteSelection { get; set; } // 0 = Select Existing, 1 = Create New
    
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
}

public class LookupItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}