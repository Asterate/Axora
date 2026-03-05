using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CreateTeacherCertificateViewModel
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public List<SelectListItem>? Teachers { get; set; }
    
    [Required(ErrorMessage = "Language is required")]
    [Display(Name = "Language")]
    public Guid LanguageId { get; set; }
    
    [Required(ErrorMessage = "Level is required")]
    [Display(Name = "Level")]
    public Guid LevelId { get; set; }
    
    [Required(ErrorMessage = "Certificate name is required")]
    [Display(Name = "Certificate Name")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Certificate name must be between 3 and 128 characters")]
    public string TeacherCertificateName { get; set; } = default!;
    
    [Display(Name = "Certificate Description")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Certificate description must be between 3 and 128 characters")]
    public string TeacherCertificateDescription { get; set; } = default!;
    
    [Required(ErrorMessage = "Issue date is required")]
    [Display(Name = "Issue Date")]
    public DateTime TeacherCertificateGivenOut { get; set; } = DateTime.Now;
    
    public List<SelectListItem>? Languages { get; set; }
    public List<SelectListItem>? Levels { get; set; }
}
