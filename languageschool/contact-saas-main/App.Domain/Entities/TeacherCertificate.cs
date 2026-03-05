using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class TeacherCertificate : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public Guid LanguageId { get; set; }
    public Language? Language { get; set; }
    public Guid LevelId { get; set; }
    public Level? Level { get; set; }
    public DateTime TeacherCertificateGivenOut {get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherCertificateName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string TeacherCertificateDescription { get; set; } = default!;
}