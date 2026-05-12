using System.ComponentModel.DataAnnotations;
using App.Shared.Domain;

namespace App.Domain.Entities;

public class Institute : BaseEntity
{
    [StringLength(128, MinimumLength = 2)]
    public LangStr InstituteName { get; set; } = new();
    
    [StringLength(128, MinimumLength = 2)]
    public string InstituteCountry {get; set;}  = default!;
    
    [StringLength(128, MinimumLength = 5)]
    public LangStr InstituteAddress { get; set; } = new();
    
    [StringLength(128, MinimumLength = 5)]
    public string InstitutePhoneNumber { get; set; }   = default!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    
    public Boolean Active { get; set; } =  true;
    
    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();
}