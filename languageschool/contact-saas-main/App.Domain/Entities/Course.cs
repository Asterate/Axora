using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Course : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
    public Guid LanguageId { get; set; }
    public Language? Language { get; set; }
    public Guid LevelId { get; set; }
    public Level? Level { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string CourseName { get; set; } = default!;
    [StringLength(128, MinimumLength = 10)]
    public string? CourseDescription { get; set; }
    public DateTime CourseCreatedAt { get; set; } = DateTime.Now;
    public DateTime CourseUpdatedAt { get; set; }
    public DateTime CourseDeletedAt { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string? CourseSeason { get; set; }
}