namespace App.Domain.Entities;

public class AttendanceRecord : BaseEntity
{
    public Guid EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }
    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; } = default!;
    public Guid ConfirmedById { get; set; }
    public Teacher ConfirmedBy { get; set; } = default!;
    public DateTime AttendanceRecordCreatedAt { get; set; } = DateTime.Now;
    public DateTime AttendanceRecordUpdatedAt { get; set; }
    public DateTime AttendanceRecordDeletedAt { get; set; }
    public bool Attendance { get; set; }
}