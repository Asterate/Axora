namespace App.Domain.Entities;

public class TeacherLanguage : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }

    public Guid LanguageId { get; set; }
    public Language? Language { get; set; }

    public Guid LevelId { get; set; }
    public Level? Level { get; set; }
}
