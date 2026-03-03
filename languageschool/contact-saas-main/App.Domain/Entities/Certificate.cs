using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Certificate : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = default!;
    public Guid LevelId { get; set; }
    public Level Level { get; set; } = default!;
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = default!;
    
    [StringLength(128, MinimumLength = 3)]
    public string CertificateName { get; set; } = default!;
    [StringLength(254, MinimumLength = 10)]
    public string CertificateDescription { get; set; } = default!;
    public DateTime CertificateGivenOutAt { get; set; } =  default!;
}