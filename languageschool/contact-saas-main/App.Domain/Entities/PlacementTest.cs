using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class PlacementTest : BaseEntity
{
    public Guid LanguageId { get; set; }
    public Language Language { get; set; } = default!;
    public Guid LevelId { get; set; }
    public Level Level { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string PlacementTestName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string PlacementTestDescription { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string? PlacementTestGrade { get; set; }
    public bool? PlacementTestPassed { get; set; }
}