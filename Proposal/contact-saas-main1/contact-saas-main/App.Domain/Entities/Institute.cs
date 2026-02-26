using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Identity;

namespace App.Domain.Entities;

public class Institute : BaseEntity
{
    [StringLength(128, MinimumLength = 3)]
    public string InstituteName { get; set; }  = default!;
    [StringLength(128, MinimumLength = 3)]
    public string InstituteCountry {get; set;}  = default!;
    [StringLength(128, MinimumLength = 10)]
    public string InstituteAddress { get; set; }  = default!;
    [StringLength(128, MinimumLength = 10)]
    public string InstitutePhoneNumber { get; set; }   = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Boolean Active { get; set; } =  true;
    
    public Guid InstituteTypeId { get; set; }
    public InstituteType InstituteType { get; set; } = default!;
    public ICollection<InstituteLab> InstituteLab { get; set; } = new List<InstituteLab>();
    public ICollection<InstituteProject> InstituteProjects { get; set; } = new List<InstituteProject>();
    public ICollection<InstituteUser> InstituteUsers { get; set; } = new List<InstituteUser>();
}