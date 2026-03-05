using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CreateCourseViewModel
{
    // Course info
    public Guid? CourseId { get; set; }
    
    // Company info
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = default!;

    // Course fields
    public string CourseName { get; set; }  = default!;
    public string? CourseDescription { get; set; }  = default!;
    public string? CourseSeason { get; set; }  = default!;

    // Foreign keys
    public Guid LanguageId { get; set; }
    public Guid LevelId { get; set; }
    public Guid TeacherId { get; set; }

    // Select lists for dropdowns
    public List<SelectListItem>? Languages { get; set; } = default!;
    public List<SelectListItem>? Levels { get; set; } =  default!;
    public List<SelectListItem>? Teachers { get; set; } =  default!;
    
    // Language to level mapping for client-side filtering
    public Dictionary<Guid, List<SelectListItem>> LanguageLevelMap { get; set; } = new Dictionary<Guid, List<SelectListItem>>();
}