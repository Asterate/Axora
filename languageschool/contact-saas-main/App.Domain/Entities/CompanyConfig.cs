using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class CompanyConfig : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; } 
    [StringLength(128)]
    public string? CompanyConfigTimeZone { get; set; }
    public bool CompanyConfigAllowOneOnOneSessions {get; set;}
    public bool CompanyConfigEnableMaterialTracking {get; set;}
    [StringLength(128)]
    public string? CompanyConfigThemeColour { get; set; }
    [StringLength(128)]
    public string? CompanyConfigCompanyLogoPath { get; set; }
    public DateTime CompanyConfigCreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompanyConfigUpdatedAt { get; set; }
    public int CompanyConfigMaxStudentsPerCourse { get; set; } = 6;
    public bool CompanyConfigEnableCertificates { get; set; }
    public ECompanyConfigVisibility CompanyConfigVisibility { get; set; } = ECompanyConfigVisibility.Private;
}