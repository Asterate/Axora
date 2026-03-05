namespace App.Domain.Entities;

public class Session : BaseEntity
{
    public Guid ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
    public Guid TeacherId { get; set; }
    public Teacher? Teacher { get; set; }
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public DateTime SessionCreatedAt { get; set; } = DateTime.Now;
    public DateTime SessionUpdatedAt { get; set; }
    public DateTime SessionDeletedAt { get; set; }
}