using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Material : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string MaterialName { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string MaterialDescription { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string MaterialVersion { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string MaterialEnvironment { get; set; } = default!;
}