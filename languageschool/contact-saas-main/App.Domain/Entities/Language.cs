using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Language : BaseEntity
{
    public Guid CompanyId { get; set; }
    public virtual Company Company { get; set; } = default!;
    public Guid LevelId { get; set; }
    public virtual Level Level { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string LanguageName { get; set; } = default!;
    [StringLength(128, MinimumLength = 10)]
    public string LanguageDescription { get; set; } = default!;
    public DateTime LanguageCreatedAt { get; set; } = DateTime.Now;
    public DateTime LanguageUpdatedAt { get; set; }
    public DateTime LanguageDeletedAt { get; set; }
}