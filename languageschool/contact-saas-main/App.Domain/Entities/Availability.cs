namespace App.Domain.Entities;

public class Availability : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = default!;
    public DateTime AvailabilityStartDate { get; set; }
    public DateTime AvailabilityEndDate { get; set; }
    public DateTime AvailabilityCreatedAt { get; set; } = default!;
    public DateTime AvailabilityUpdatedAt { get; set; }
    public DateTime AvailabilityDeletedAt { get; set; }
}