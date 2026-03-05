using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Certificate : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Guid LanguageId { get; set; }
    public Language? Language { get; set; }
    public Guid LevelId { get; set; }
    public Level? Level { get; set; }
    public Guid EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }
    
    [StringLength(128, MinimumLength = 3)]
    public string CertificateName { get; set; } = default!;
    [StringLength(254, MinimumLength = 10)]
    public string? CertificateDescription { get; set; }
    public DateTime CertificateGivenOutAt { get; set; } =  default!;
}