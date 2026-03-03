using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class TeacherCertificate : BaseEntity
{
    public Guid TeacherId { get; set; } = default!;
    public Teacher Teacher { get; set; } = default!;
    public Guid LanguageId { get; set; } = default!;
    public Language Language { get; set; } = default!;
    public Guid LevelId { get; set; } = default!;
    public Level Level { get; set; } = default!;
    public DateTime TeacherCertificateGivenOut {get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherCertificateName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherCertificateDescription { get; set; } = default!;
}