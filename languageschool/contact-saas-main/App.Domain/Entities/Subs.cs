namespace App.Domain.Entities;

public class Subs : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } =  default!;
    public int StandardPrice { get; set; } = 220;
    public int PremiumPrice { get; set; } =  280;
    public DateTime SubsCreatedAt { get; set; } = DateTime.Now;
    public DateTime? SubsUpdatedAt { get; set; }
}