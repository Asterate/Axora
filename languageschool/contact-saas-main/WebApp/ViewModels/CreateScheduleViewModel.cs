using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class CreateScheduleViewModel
{
    public Guid? ScheduleId { get; set; }
    public Guid CourseId { get; set; }
    public Guid? TeacherId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Availability is required")]
    [Display(Name = "Availability")]
    public Guid AvailabilityId { get; set; }
    
    [Required(ErrorMessage = "Schedule type is required")]
    [Display(Name = "Schedule Type")]
    public EScheduleType EScheduleType { get; set; } = EScheduleType.Office;
    
    [Required(ErrorMessage = "Start date and time is required")]
    [Display(Name = "Start At")]
    [DataType(DataType.DateTime)]
    public DateTime ScheduleStartAt { get; set; }
    
    [Required(ErrorMessage = "End date and time is required")]
    [Display(Name = "End At")]
    [DataType(DataType.DateTime)]
    public DateTime ScheduleEndAt { get; set; }
    
    [Required(ErrorMessage = "Environment is required")]
    [Display(Name = "Environment")]
    [StringLength(128, MinimumLength = 3, ErrorMessage = "Environment must be between 3 and 128 characters")]
    public string ScheduleEnvironment { get; set; } = string.Empty;
    
    public List<SelectListItem> Availabilities { get; set; } = new();
    public List<SelectListItem> ScheduleTypes { get; set; } = new();
}