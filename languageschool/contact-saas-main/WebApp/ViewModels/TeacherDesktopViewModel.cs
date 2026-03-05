using App.Domain.Entities;

namespace WebApp.ViewModels;

public class TeacherDesktopViewModel
{
    public List<Teacher> AllTeachers { get; set; } = new List<Teacher>();
    public Guid SelectedTeacherId { get; set; }
    public Teacher? SelectedTeacher { get; set; }
    public Guid CourseId { get; set; } = default!;
    public List<Availability> Availabilities { get; set; } = new List<Availability>();
    public List<TeacherCertificate> TeacherCertificates { get; set; } = new List<TeacherCertificate>();
    public string? NoAvailabilitiesMessage { get; set; }
    public string? NoCertificatesMessage { get; set; }
}
