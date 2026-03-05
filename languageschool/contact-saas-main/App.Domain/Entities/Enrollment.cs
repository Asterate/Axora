using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = default!;
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = default!;
    public DateTime EnrollmentCreatedAt { get; set; } = DateTime.Now;
    public DateTime EnrollmentModifiedAt { get; set; }
    public DateTime EnrollmentDeletedAt { get; set; }
    [StringLength(128, MinimumLength = 3)]
    public string EnrollmentSeason {get; set;} = default!;
    public bool EnrollmentRepeat {get; set;}
    public int EnrollmentPayStatus {get; set;}
    public EEnrollmentPlan EnrollmentPlan { get; set; } = EEnrollmentPlan.Free;
}