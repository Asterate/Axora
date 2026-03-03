using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Level : BaseEntity
{
    [StringLength(128,MinimumLength = 3)]
    public string LevelName { get; set; } = default!;
    [StringLength(128,MinimumLength = 3)]
    public string LevelDescription { get; set; } = default!;
}