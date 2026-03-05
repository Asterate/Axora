using App.Domain.Identity;

namespace App.Domain.Entities;

public class CompanyUser : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; } 
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ECompanyRoles Roles { get; set; } = ECompanyRoles.None;
    
}