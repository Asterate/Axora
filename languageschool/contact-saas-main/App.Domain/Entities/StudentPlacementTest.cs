namespace App.Domain.Entities;

public class StudentPlacementTest : BaseEntity
{
    public Guid StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid PlacementTestId { get; set; }
    public PlacementTest? PlacementTest { get; set; }
    public DateTime StudentPlacementTestCreatedAt { get; set; } = DateTime.Now;
    public DateTime StudentPlacementTestUpdatedAt { get; set; }
    public DateTime StudentPlacementTestDeletedAt { get; set; }
}