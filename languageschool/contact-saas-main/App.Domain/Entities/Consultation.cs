using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Consultation : BaseEntity
{
    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = default!;
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = default!;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    [StringLength(128, MinimumLength = 3)]
    public string ConsultationName { get; set; } = default!;
    [StringLength(128, MinimumLength = 10)]
    public string ConsultationDescription { get; set; } = default!;
    public DateTime ConsultationCreatedAt { get; set; } = DateTime.Now;
    public DateTime ConsultationUpdatedAt { get; set; }
    public DateTime ConsultationDeletedAt { get; set; }
}