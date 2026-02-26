using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities;

public class InstituteProject : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public Guid InstituteId { get; set; }
    public Institute Institute { get; set; } = default!;
    public Guid ProjectId  { get; set; }
    public Project Project { get; set; } = default!;
}