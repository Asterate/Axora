using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CreateCertificateViewModel
{
    public Guid TeacherId { get; set; }
    public string? TeacherName { get; set; }
    
    [Required(ErrorMessage = "Course is required")]
    [Display(Name = "Course")]
    public Guid CourseId { get; set; }
    
    [Required(ErrorMessage = "Student is required")]
    [Display(Name = "Student")]
    public Guid EnrollmentId { get; set; }
    
    [Required(ErrorMessage = "Certificate name is required")]
    [Display(Name = "Certificate Name")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Certificate name must be between 3 and 128 characters")]
    public string CertificateName { get; set; } = default!;
    
    [Display(Name = "Certificate Description")]
    [StringLength(254, MinimumLength = 10, ErrorMessage = "Certificate description must be between 10 and 254 characters")]
    public string? CertificateDescription { get; set; }
    
    [Required(ErrorMessage = "Issue date is required")]
    [Display(Name = "Issue Date")]
    public DateTime CertificateGivenOutAt { get; set; } = DateTime.Now;
    
    public List<SelectListItem>? Courses { get; set; }
    public List<SelectListItem>? Enrollments { get; set; }
}
