using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;
using Base.Resources;

namespace App.Domain.Entities;

public class Institute : BaseEntity
{
    [Display(Name = "Name", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 2)]
    public string InstituteName { get; set; }  = default!;
    
    [Display(Name = "Country", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 2)]
    public string InstituteCountry {get; set;}  = default!;
    
    [Display(Name = "Address", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 5)]
    public string InstituteAddress { get; set; }  = default!;
    
    [Display(Name = "PhoneNumber", ResourceType = typeof(Base.Resources.Common))]
    [StringLength(128, MinimumLength = 5)]
    public string InstitutePhoneNumber { get; set; }   = default!;
    
    [Display(Name = "CreatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Display(Name = "UpdatedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? UpdatedAt { get; set; }
    
    [Display(Name = "DeletedAt", ResourceType = typeof(Base.Resources.Common))]
    public DateTime? DeletedAt { get; set; }
    
    [Display(Name = "Active", ResourceType = typeof(Base.Resources.Common))]
    public Boolean Active { get; set; } =  true;
    
    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteLab> InstituteLab { get; set; } = new List<InstituteLab>();
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();
    public ICollection<InstituteUser> InstituteUsers { get; set; } = new List<InstituteUser>();
}