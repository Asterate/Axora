using App.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class CreateMaterialViewModel
{
    public Guid CourseId { get; set; }
    public string? CourseName { get; set; }
    
    [Required(ErrorMessage = "Material name is required")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Material name must be between 3 and 128 characters")]
    public string MaterialName { get; set; } = default!;
    
    [Required(ErrorMessage = "Material description is required")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Material description must be between 3 and 128 characters")]
    public string MaterialDescription { get; set; } = default!;
    
    [Required(ErrorMessage = "Material version is required")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Material version must be between 3 and 128 characters")]
    public string MaterialVersion { get; set; } = "1.0";
    
    [Required(ErrorMessage = "Material environment is required")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Material environment must be between 3 and 128 characters")]
    public string MaterialEnvironment { get; set; } = default!;
}
