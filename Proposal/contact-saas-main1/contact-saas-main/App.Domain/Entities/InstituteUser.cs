using App.Domain.Identity;

namespace App.Domain.Entities;

public class InstituteUser : BaseEntity
{
    public AppUser User { get; set; }  = default!;
    
    public Guid InstituteId { get; set; }
    public Institute Institute { get; set; } = default!;
    public ICollection<Certification> Certifications { get; set; } = new List<Certification>();
    public ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();
    public ICollection<ExperimentTask> ExperimentTasks { get; set; } = new List<ExperimentTask>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public EInstituteUserRole Role { get; set; } = EInstituteUserRole.Employee;

}