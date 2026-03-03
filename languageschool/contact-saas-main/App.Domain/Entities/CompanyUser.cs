using App.Domain.Identity;

namespace App.Domain.Entities;

public class CompanyUser : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; } 
    public Guid AppUserId { get; set; }
    public virtual AppUser AppUser { get; set; } = default!;
}