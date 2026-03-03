using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities;

public class MaterialDistribution : BaseEntity
{
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = default!;
    public Guid MaterialId { get; set; }
    public Material Material { get; set; } = default!;
    public DateTime MaterialDistributionCreatedAt { get; set; } = DateTime.Now;
    public DateTime MaterialDistributionModifiedAt { get; set; }
    public DateTime MaterialDistributionDeletedAt { get; set; }
}