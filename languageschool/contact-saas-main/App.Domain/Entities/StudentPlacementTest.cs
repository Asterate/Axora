namespace App.Domain.Entities;

public class StudentPlacementTest : BaseEntity
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public Guid PlacementTestId { get; set; }
    public PlacementTest PlacementTest { get; set; } = default!;
    public DateTime StudentPlacementTestCreatedAt { get; set; } = default!;
    public DateTime StudentPlacementTestUpdatedAt { get; set; }
    public DateTime StudentPlacementTestDeletedAt { get; set; }
}