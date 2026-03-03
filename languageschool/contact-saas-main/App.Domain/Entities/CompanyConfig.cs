using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class CompanyConfig : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } =  default!;
    public bool CompanyConfigEnabled { get; set; } //???
    [StringLength(128, MinimumLength = 3)]
    public string CompanyConfigTimeZone { get; set; } =  default!;
    public bool CompanyConfigAllowOneOnOneSessions {get; set;} =  false;
    public bool CompanyConfigEnableMaterialTracking {get; set;} =  false;
    [StringLength(128, MinimumLength = 10)]
    public string CompanyConfigThemeColour { get; set; } =  default!;
    [StringLength(128, MinimumLength = 10)]
    public string CompanyConfigCompanyLogoPath { get; set; } =  default!;
    public DateTime CompanyConfigCreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompanyConfigUpdatedAt { get; set; }
    public int CompanyConfigMaxStudentsPerCourse { get; set; } =  default!;
    public bool CompanyConfigEnableCertificates { get; set; } =  false;
}