using System.ComponentModel.DataAnnotations;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class CreateAvailabilityViewModel
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public List<SelectListItem>? Teachers { get; set; }
    
    [Required(ErrorMessage = "Start date and time is required")]
    [Display(Name = "Start Date & Time")]
    public DateTime AvailabilityStartDate { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "End date and time is required")]
    [Display(Name = "End Date & Time")]
    public DateTime AvailabilityEndDate { get; set; } = DateTime.Now.AddHours(1);
}
