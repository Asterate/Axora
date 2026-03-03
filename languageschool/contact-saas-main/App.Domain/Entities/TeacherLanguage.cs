namespace App.Domain.Entities;

public class TeacherLanguage : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;

    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = null!;

    public Guid LevelId { get; set; }
    public Level Level { get; set; } = null!;
}
